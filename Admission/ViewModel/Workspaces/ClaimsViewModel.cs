using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data.Entity;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Admission;
using System.Windows.Input;
using Admission.ViewModel.Editors;
using Admission.DialogService;
using Admission.ViewModel.Documents;
using System.Threading;
using Admission.ViewModel.ValidationRules.Validators;

namespace Admission.ViewModel.Workspaces
{
	public class ClaimsViewModel : ViewModelBase
	{

		#region Parent Entities   

		Claim _selectedClaim;

		public Claim SelectedClaim
		{
			get
			{
				if (_selectedClaim == null)
				{
					_selectedClaim = ClaimList.FirstOrDefault();
				}
				return _selectedClaim;
			}
			set
			{
				_selectedClaim = value;
				RaisePropertyChanged("SelectedClaim");
			}
		}

		ObservableCollection<Claim> _claimList;

		public ObservableCollection<Claim> ClaimList
		{
			get
			{
                if (_claimList == null)
                {
                    _claimList = new ObservableCollection<Claim>(from claim in Session.DataModel.Claims
                                                                 where (claim.ClaimStatusId == 1 || claim.ClaimStatusId == 2 || claim.ClaimStatusId == 3)
                                                                 && claim.Campaign.CampaignStatusId == 2
                                                                 orderby claim.RegistrationDate, claim.Id
                                                                 select claim);
                }
				return _claimList;
			}
			set
			{
				_claimList = value;
				RaisePropertyChanged("ClaimList");
			}
		}

		#endregion

		#region CRUD Commands													 

		#region Commands

		public RelayCommand AddCommand
		{
			get
			{
				return new RelayCommand(Add);
			}
		}
		public RelayCommand EditCommand
		{
			get
			{
				return new RelayCommand(Edit);
			}
		}
		public RelayCommand RemoveCommand
		{
			get
			{

				return new RelayCommand(null);
			}
		}
		public RelayCommand UpdateCommand
		{
			get
			{
				return new RelayCommand(Refresh);
			}
		}

		#endregion


		#region Methods

		void Add()
		{
			var address = new Address()
			{
				Country = Session.DataModel.Countries.FirstOrDefault()
			};

			var entrant = new Entrant()
			{
				Address = address,
				Gender = Session.DataModel.Genders.First(),
                MarritalStatus = Session.DataModel.MarritalStatuses.First()
			};

			var ord = new OtherRequiredDocument
			{
				Fluorography = false,
				Certificate = false,
				Photos = false,
				MedicinePermission = false
			};
            

			var claim = new Claim()
			{
				RegistrationDate = DateTime.Now,
				IsHostelNeed = false,
				PersonalReturning = true,
				ClaimStatus = Session.DataModel.ClaimStatuses.FirstOrDefault()
			};

			claim.Entrants.Add(entrant);
			claim.OtherRequiredDocuments.Add(ord);

			Session.DataModel.OtherRequiredDocuments.Add(ord);
			Session.DataModel.Addresses.Add(address);
			Session.DataModel.Entrants.Add(entrant);
			Session.DataModel.Claims.Add(claim);

			var vm = new ClaimEditorViewModel(claim);

			var validator = new ClaimValidator(claim);

			if (DialogLayer.ShowEditor(EditingContent.ClaimEditor, vm, validator))
			{
				Session.DataModel.SaveChanges();
				RaisePropertyChanged("ClaimList");
			}
			else
			{
				Session.DataModel.Claims.Remove(claim);
				Session.DataModel.SaveChanges();
			}
		}

		void Edit()
		{
			var vm = new ClaimEditorViewModel(_selectedClaim);
			var validator = new ClaimValidator(_selectedClaim);
			if (DialogLayer.ShowEditor(EditingContent.ClaimEditor, vm, validator))
			{
				Session.DataModel.SaveChanges();
				RaisePropertyChanged("ClaimList");
			}
		}

		void Refresh()
		{
			Session.RefreshAll();
            _claimList = null;
			RaisePropertyChanged("ClaimList");
		}

		#endregion

		#endregion

		#region PrintCommands

		#region Commands		

		public RelayCommand PrintDocumentPackageCommand
		{
			get
			{
				return new RelayCommand(PrintDocumentPackage);
			}
		}
		
		public RelayCommand PrintVoucherCommand
		{
			get
			{
				return new RelayCommand(PrintVoucher);
			}
		}

		public RelayCommand PrintClaimCommand
		{
			get
			{
				return new RelayCommand(PrintClaim);
			}
		}

		public RelayCommand PrintEnrollAgreementClaimCommand
		{
			get
			{
				return new RelayCommand(PrintEnrollAgreementClaim);
			}
		}

		public RelayCommand PrintTitlePageCommand
		{
			get
			{
				return new RelayCommand(PrintTitlePage);
			}
		}

		public RelayCommand PrintHostelClaimCommand
		{
			get
			{
				return new RelayCommand(PrintHostelClaim, PrintHostelClaimCanExecute);
			}
		}

		public RelayCommand PrintInventoryListCommand
		{
			get
			{
				return new RelayCommand(PrintInventoryList);
			}
		}

		public RelayCommand PrintIndividualAchievementsProtocolCommand
		{
			get
			{
				return new RelayCommand(PrintIndividualAchievementsProtocol, PrintIndividualAchievementsProtocolCanExecute);
			}
		}

		public RelayCommand PrintExaminationStatementCommand
		{
			get
			{
				return new RelayCommand(PrintExaminationStatement);
			}
		}

		public RelayCommand PrintEnrollmentOrderStatementCommand
		{
			get
			{
				return new RelayCommand(PrintEnrollmentOrderStatement, PrintEnrollmentOrderStatementCanExecute);
			}
		}

		public RelayCommand PrintEnrollmentDisagreementClaimCommand
		{
			get
			{
				return new RelayCommand(PrintEnrollmentDisagreementClaim, PrintEnrollmentDisagreementClaimCanExecute);
			}
		}

		#endregion

		#region Methods		  

		void PrintVoucher()
		{
			var doc = new DocumentVoucher(SelectedClaim);
			doc.Show();
		}

		void PrintClaim()
		{
			// если идет на вышку
			if (SelectedClaim.ClaimConditions.First().CompetitiveGroup.EducationLevel.Id == 1)
			{
				var doc = new DocumentClaim(SelectedClaim);
				doc.Show();
			}
			//если на спо
			else
			{
				var doc = new DocumentClaimMiddleEducation(SelectedClaim);
				doc.Show();
			}
		}

		void PrintEnrollAgreementClaim()
		{
			var doc = new DocumentEnrollmentAgreementClaim(SelectedClaim);
			doc.Show();
		}

		void PrintTitlePage()
		{
			var doc = new DocumentTitlePage(SelectedClaim);
			doc.Show();
		}

		void PrintHostelClaim()
		{
			var doc = new DocumentHostelClaim(SelectedClaim);
			doc.Show();
		}

		void PrintInventoryList()
		{
			var doc = new DocumentInventoryList(SelectedClaim);
			doc.Show();
		}

		void PrintIndividualAchievementsProtocol()
		{
			var doc = new DocumentIndividualAchievementsProtocol(SelectedClaim);
			doc.Show();
		}

		void PrintDocumentPackage()
		{
			var printThread = new Thread(new ParameterizedThreadStart(DoPrintDocumentPackage));
			printThread.Start(SelectedClaim);
		}

		void DoPrintDocumentPackage(object claimObj)
		{

			var claim = (Claim)claimObj;
			string filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData)
			+ "\\Admission\\TemporaryDocuments";
			string fileNamePart = string.Format("{0}_{1}", claim.Number, DateTime.Now.ToString("dd.MM.yyyy hh-mm-ss"));

			System.IO.Directory.CreateDirectory(filePath);

			var commonDoc = new MissingDocument();

			var titlePage = new DocumentTitlePage(claim);
			string titlePageStr = string.Format("{0}\\titlePage_{1}.doc", filePath, fileNamePart);
			titlePage.Save(titlePageStr);
			commonDoc.InsertFromFile(titlePageStr);
			commonDoc.InsertBreak();

			var enrollmentAgreement = new DocumentEnrollmentAgreementClaim(claim);
			string enrollmentAgreementStr = string.Format("{0}\\enrollmentAgreement_{1}.doc", filePath, fileNamePart);
			enrollmentAgreement.Save(enrollmentAgreementStr);
			commonDoc.InsertFromFile(enrollmentAgreementStr);
			commonDoc.InsertBreak();

			var voucher = new DocumentVoucher(claim);
			string voucherStr = string.Format("{0}\\voucher_{1}.doc", filePath, fileNamePart);
			voucher.Save(voucherStr);
			commonDoc.InsertFromFile(voucherStr);
			commonDoc.InsertBreak();

			if (claim.IsHostelNeed ?? false)
			{
				var hostelClaim = new DocumentHostelClaim(claim);
				string hostelClaimStr = string.Format("{0}\\hostelClaim_{1}.doc", filePath, fileNamePart);
				hostelClaim.Save(hostelClaimStr);
				commonDoc.InsertFromFile(hostelClaimStr);
				commonDoc.InsertBreak();
			}

			if (claim.EntranceIndividualAchievements.Count > 0)
			{
				var indAch = new DocumentIndividualAchievementsProtocol(claim);
				string indAchStr = string.Format("{0}\\indAch_{1}.doc", filePath, fileNamePart);
				indAch.Save(indAchStr);
				commonDoc.InsertFromFile(indAchStr);
				commonDoc.InsertBreak();
			}
			commonDoc.Show();
		}

		void PrintExaminationStatement()
		{
			if (SelectedClaim.EducationLevel.Id == 1)
			{
				var doc = new ExaminationStatement(SelectedClaim);
				DialogLayer.ShowDocument(doc);
			}
			else
			{
				if (SelectedClaim.SchoolCertificateDocuments.First().EntranceExaminationsCheckProtocol != null)
				{
					var doc = new ExaminationStatementMiddleEducationDocument(SelectedClaim);
					DialogLayer.ShowDocument(doc);
				}
			}			
		}

		void PrintEnrollmentOrderStatement()
		{
			var doc = new EnrollmentOrderStatement(SelectedClaim);
			DialogLayer.ShowDocument(doc);
		}

		void PrintEnrollmentDisagreementClaim()
		{
			var doc = new EnrollmentDisagreementClaimDocument(SelectedClaim);
			DialogLayer.ShowDocument(doc);
		}

		#endregion

		#region CheckMethods

		bool PrintHostelClaimCanExecute()
		{
			return SelectedClaim.IsHostelNeed ?? false;
		}

		bool PrintIndividualAchievementsProtocolCanExecute()
		{
			return SelectedClaim.EntranceIndividualAchievements.Count > 0;
		}

		bool PrintEnrollmentOrderStatementCanExecute()
		{
			return (SelectedClaim.ClaimStatus.Id == 3) && 
				(SelectedClaim.EnrollmentClaims.Where(ec => ec.EnrollmentProtocol != null).Count() > 0);
		}

		bool PrintEnrollmentDisagreementClaimCanExecute()
		{
			bool result = false;
			if (SelectedClaim != null)
			{
				if (SelectedClaim.EnrollmentClaims.Where(ec => ec.EnrollmentExceptionOrder != null).Count() > 0)
				{
					result = true;
				}
			}
			return result;
		}

		#endregion

		#endregion

		#region Search Commands

		#region Commands

		public RelayCommand<string> SearchClaimByEntrantNameCommand
		{
			get
			{
				return new RelayCommand<string>(SearchClaimByEntrantName);
			}
		}

		#endregion

		#region Methods


		void SearchClaimByEntrantName(string entrantName)
		{
			if (entrantName.Length > 0)
			{
				var searchingResult = GetClaimByEntrantName(entrantName);
				if (searchingResult != null)
				{
					SelectedClaim = searchingResult;
				}
			}
		}


		/// <summary>
		/// Возвращает заявление, в котором Ф.И.О. абитуриента содержит входной параметр (регистронезависимо)
		/// </summary>
		/// <param name="entrantName">Ф.И.О. абитуриента</param>
		/// <returns></returns>
		Claim GetClaimByEntrantName(string entrantName)
		{
			string searchedValue = entrantName.ToLower();

			Claim searchResult = null;

			foreach (var claim in ClaimList)
			{
				var entrant = claim.Entrants.First();

				string currentValue = string.Format("{0} {1} {2}",
					entrant.LastName, entrant.FirstName, entrant.Patronymic);
				currentValue = currentValue.ToLower();

				if (currentValue.Contains(searchedValue))
				{
					searchResult = claim;
					break;
				}
			}

			return searchResult;
		}

		#endregion

		#endregion

		#region HandleCommands

		#region Commands

		public RelayCommand SendClaimToArchiveCommand
		{
			get
			{
				return new RelayCommand(SendClaimToArchive);
			}
		}

		public RelayCommand CopyAndArchiveClaimCommand
		{
			get
			{
				return new RelayCommand(CopyAndArchiveClaim);
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Процедура отправки заявления в архив (возврат документов абитуриенту) (выставляет текущему заявлению дату возврата как текущую и все документы помечает как копии).
		/// </summary>
		void SendClaimToArchive()
		{
			var claim = SelectedClaim;
			// Переспрашиваем пользователя, вдруг он нечаянно
			if (!Messenger.SendClaimToArchiveQuestion())
			{
				return;
			}

			// Перебираем условия приёма, определяя наличие заявлений о согласии на зачисление
			foreach (var condition in claim.ClaimConditions)
			{
				// Если присутствует незакрытое заявление о согласии на зачисление, то закрываем его, добавляя к нему заявление об отказе на зачисление
				if (condition.EnrollmentAgreementDate != null)
				{
					condition.EnrollmentDisclaimerDate = DateTime.Now;
				}
			}

			// Убираем дату предоставления оригиналов аттестатов о среднем образовании
			foreach (var doc in claim.SchoolCertificateDocuments)
			{
				doc.OriginalReceivedDate = null;
			}

			// Убираем дату предоставления оригиналов дипломов СПО
			foreach (var doc in claim.MiddleEducationDiplomaDocuments)
			{
				doc.OriginalReceivedDate = null;
			}

			// Убираем дату предоставления оригиналов дипловом ВО
			foreach (var doc in claim.HighEducationDiplomaDocuments)
			{
				doc.OriginalReceivedDate = null;
			}

			// Устанавливаем дату возврата документов на текущую
			claim.ReturnDate = DateTime.Now;

			// Устанавливаем статус заявления в архивное
			claim.ClaimStatusId = 4;

			// Сохраняем данные и обновляем коллекцию заявлений
			Session.DataModel.SaveChanges();
			RaisePropertyChanged("ClaimList");

		}

		/// <summary>
		/// Отправляет заявление в архив и создает на его основе новое с такми же данными, но с опущенными номером
		/// заявления, условиями приёма, внутренними вступительными испытаниями. 
		/// Также выставляет текущую дату регистрации заявления.
		/// </summary>
		/// <param name="existingClaim">Существующее заявление, отправляемое в архив</param>
		void CopyAndArchiveClaim()
		{
			var existingClaim = SelectedClaim;
            // Копируем текущие данные в новое заявление
            var newClaim = new Claim
            {
                RegistrationDate = DateTime.Now,
                ClaimStatus = existingClaim.ClaimStatus,
                IsHostelNeed = existingClaim.IsHostelNeed,
                PersonalReturning = existingClaim.PersonalReturning
            };
            newClaim.ClaimStatus = existingClaim.ClaimStatus;

			// Документы о результатах ЕГЭ
			foreach (var egeDoc in existingClaim.EgeDocuments)
			{
				var newEgeDoc = new EgeDocument
				{
					OriginalReceivedDate = egeDoc.OriginalReceivedDate,
					Number = egeDoc.Number,
					Date = egeDoc.Date,
					Year = egeDoc.Year
				};

				// Сами результаты ЕГЭ
				foreach (var result in egeDoc.EgeResults)
				{
					var newResult = new EgeResult
					{
						ExamSubject = result.ExamSubject,
						Value = result.Value,
						IsChecked = result.IsChecked
					};

					newEgeDoc.EgeResults.Add(newResult);
				}
				newClaim.EgeDocuments.Add(newEgeDoc);
			}

			// Индивидуальные достижения
			foreach (var individualAchievement in existingClaim.EntranceIndividualAchievements)
			{
				var newIndAch = new EntranceIndividualAchievement
				{
					CampaignIndividualAchievement = individualAchievement.CampaignIndividualAchievement
				};
				newClaim.EntranceIndividualAchievements.Add(newIndAch);
			}

			// Документы об образовании
			// Аттестаты среднего образования
			foreach (var eduDoc in existingClaim.SchoolCertificateDocuments)
			{
				var newEduDoc = new SchoolCertificateDocument
				{
					OriginalReceivedDate = eduDoc.OriginalReceivedDate,
					Series = eduDoc.Series,
					Number = eduDoc.Number,
					Date = eduDoc.Date,
					SubdivisionCode = eduDoc.SubdivisionCode,
					EducationOrganization = eduDoc.EducationOrganization,
					FiveCount = eduDoc.FiveCount,
					FourCount = eduDoc.FourCount,
					ThreeCount = eduDoc.ThreeCount
				};
				newClaim.SchoolCertificateDocuments.Add(newEduDoc);
			}

			// Дипломы СПО
			foreach (var eduDoc in existingClaim.MiddleEducationDiplomaDocuments)
			{
				var newEduDoc = new MiddleEducationDiplomaDocument
				{
					OriginalReceivedDate = eduDoc.OriginalReceivedDate,
					Series = eduDoc.Series,
					Number = eduDoc.Number,
					Date = eduDoc.Date,
					SubdivisionCode = eduDoc.SubdivisionCode,
					EducationOrganization = eduDoc.EducationOrganization,
					FiveCount = eduDoc.FiveCount,
					FourCount = eduDoc.FourCount,
					ThreeCount = eduDoc.ThreeCount
				};
				newClaim.MiddleEducationDiplomaDocuments.Add(newEduDoc);
			}

			// Дипломы ВО
			foreach (var eduDoc in existingClaim.HighEducationDiplomaDocuments)
			{
				var newEduDoc = new HighEducationDiplomaDocument
				{
					OriginalReceivedDate = eduDoc.OriginalReceivedDate,
					Series = eduDoc.Series,
					Number = eduDoc.Number,
					Date = eduDoc.Date,
					SubdivisionCode = eduDoc.SubdivisionCode,
					EducationOrganization = eduDoc.EducationOrganization,
					FiveCount = eduDoc.FiveCount,
					FourCount = eduDoc.FourCount,
					ThreeCount = eduDoc.ThreeCount
				};
				newClaim.HighEducationDiplomaDocuments.Add(newEduDoc);
			}

			// Документы
			// Документы, подтверждающие льготу
			foreach (var quotaDoc in existingClaim.OrphanDocuments)
			{
				var newQuotaDoc = new OrphanDocument
				{
					OriginalReceivedDate = quotaDoc.OriginalReceivedDate,
					OrphanDocumentType = quotaDoc.OrphanDocumentType,
					Series = quotaDoc.Series,
					Number = quotaDoc.Number,
					Date = quotaDoc.Date,
					Organization = quotaDoc.Organization
				};
				newClaim.OrphanDocuments.Add(newQuotaDoc);
			}

			// Другие истребуемые документы
			var oldOtherRequiredDoc = existingClaim.OtherRequiredDocuments.First();
			var newOtherRequiredDoc = new OtherRequiredDocument
			{
				Certificate = oldOtherRequiredDoc.Certificate,
				Photos = oldOtherRequiredDoc.Photos,
				MedicinePermission = oldOtherRequiredDoc.MedicinePermission,
				Fluorography = oldOtherRequiredDoc.Fluorography
			};
			newClaim.OtherRequiredDocuments.Add(newOtherRequiredDoc);

			// Личные данные абитуриента
			var oldEntrant = existingClaim.Entrants.First();
			var newEntrant = new Entrant
			{
				LastName = oldEntrant.LastName,
				FirstName = oldEntrant.FirstName,
				Patronymic = oldEntrant.Patronymic,
				Gender = oldEntrant.Gender,
				CustomInformation = oldEntrant.CustomInformation,
				Email = oldEntrant.Email,
				Address = oldEntrant.Address,
				Phone = oldEntrant.Phone,
				MobilePhone = oldEntrant.MobilePhone,
				FatherName = oldEntrant.FatherName,
				FatherPhone = oldEntrant.FatherPhone,
				FatherJob = oldEntrant.FatherJob,
				MotherName = oldEntrant.MotherName,
				MotherPhone = oldEntrant.MotherPhone,
				MotherJob = oldEntrant.MotherJob,
				JobPost = oldEntrant.JobPost,
				JobOrganization = oldEntrant.JobOrganization,
				JobStage = oldEntrant.JobStage,
				ForeignLanguage = oldEntrant.ForeignLanguage
			};
			newClaim.Entrants.Add(newEntrant);

			// Документ, удостоверяющий личность
			foreach (var identityDoc in existingClaim.IdentityDocuments)
			{
				var newIdentityDoc = new IdentityDocument
				{
					OriginalReceivedDate = identityDoc.OriginalReceivedDate,
					Series = identityDoc.Series,
					Number = identityDoc.Number,
					Date = identityDoc.Date,
					SubdivisionCode = identityDoc.SubdivisionCode,
					Organization = identityDoc.Organization,
					Citizenship = identityDoc.Citizenship,
					IdentityDocumentType = identityDoc.IdentityDocumentType,
					BirthDate = identityDoc.BirthDate,
					BirthPlace = identityDoc.BirthPlace
				};
				newClaim.IdentityDocuments.Add(newIdentityDoc);
			}

			Session.DataModel.Claims.Add(newClaim);

			// Открываем редактор нового заявления 
			var vm = new ClaimEditorViewModel(newClaim);
			var validator = new ClaimValidator(newClaim);
			if (DialogLayer.ShowEditor(EditingContent.ClaimEditor, vm, validator))
			{
				Session.DataModel.SaveChanges();

				// Производим возврат текущего заявления
				SendClaimToArchive();

				// Обновляем список заявлений
				Session.RefreshAll();
				RaisePropertyChanged("ClaimList");
			}
		}

		#endregion

		#region Checks


		#endregion

		#endregion

		#region OtherCommands

		public RelayCommand ShowFastStatisticCommand
		{
			get
			{
				return new RelayCommand(ShowFastStatistic);
			}
		}

		void ShowFastStatistic()
		{
			DialogLayer.ShowInfoBox(InfoContent.FastStatistic, 
				new Admission.ViewModel.Workspaces.EntrantClaims.InfoBoxes.FastAdmissionStatisticViewModel());
		}

		#endregion

	}
}
