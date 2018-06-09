using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Admission.DialogService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Admission;
using Admission.ViewModel.ValidationRules.Validators;

namespace Admission.ViewModel.Editors
{			  
	public class ClaimEditorViewModel : ViewModelBase
	{		 

		#region Constructors

		public ClaimEditorViewModel()
		{
			_claim = new Claim();
			_claim.Entrants.Add(new Entrant());
		}

		public ClaimEditorViewModel(Claim claim)
		{
			_claim = claim;
		}

		#endregion

		#region Parent entities	   

		Claim _claim;
		public Claim EditedClaim
		{
			get
			{
				return _claim;
			}
			set
			{
				_claim = value;
				RaisePropertyChanged("EditedClaim");
			}
		}
									   

		OtherRequiredDocument _otherRequiredDocument;

		public OtherRequiredDocument OtherRequiredDocument
		{
			get
			{
				if (_otherRequiredDocument == null)
				{
					_otherRequiredDocument = EditedClaim.OtherRequiredDocuments.FirstOrDefault();
				}
				return _otherRequiredDocument;
			}
			set
			{
				_otherRequiredDocument = value;
				RaisePropertyChanged("SelectedOtherRequiredDocument");
			}
		}


		#endregion

		#region Entrant

		#region Entities  

		Entrant _entrant;
		public Entrant EditedEntrant
		{
			get
			{
				if (_entrant == null)
				{
					_entrant = _claim.Entrants.First();
				}
				return _entrant;
			}
			set
			{
				_entrant = value;
				RaisePropertyChanged("EditedEntrant");
			}
		}	   

		#endregion

		#region Additional collections

		public ObservableCollection<Gender> Genders
		{
			get
			{
				return new ObservableCollection<Gender>(Session.DataModel.Genders.ToList());
			}
		}

		public ObservableCollection<ForeignLanguage> Languages
		{
			get
			{
				return new ObservableCollection<ForeignLanguage>(Session.DataModel.ForeignLanguages);
			}
		}

		public ObservableCollection<IdentityDocumentType> IdentityDocumentTypes
		{
			get
			{
				return new ObservableCollection<IdentityDocumentType>(Session.DataModel.IdentityDocumentTypes);
			}
		}

		public ObservableCollection<Citizenship> Citizenships
		{
			get
			{
				return new ObservableCollection<Citizenship>(Session.DataModel.Citizenships);
			}
		}

		public ObservableCollection<string> IdentityDocumentIssuingOrganizations
		{
			get
			{
				var list = (from doc in Session.DataModel.IdentityDocuments
							orderby doc.Organization
							select doc.Organization).Distinct();
				return new ObservableCollection<string>(list);
			}
		}

		#endregion

		#region Logic

		#region Commands	   

		public RelayCommand EditBirthPlaceCommand
		{
			get
			{
				return new RelayCommand(EditBirthPlace);
			}
		}

		public RelayCommand SelectAddressCommand
		{
			get
			{		 
				return new RelayCommand(EditAddress);
			}
		}

		#endregion

		#region Methods		 
			
		void EditBirthPlace()
		{
			var address = new Address
			{
				Country = Session.DataModel.Countries.FirstOrDefault()
			};

			var vm = new AddressSelectorViewModel(address);

			if (DialogLayer.ShowEditor(EditingContent.AddressSelector, vm))
			{
				RaisePropertyChanged("IdentityDocument");
			}
		}							   

		void EditAddress()
		{
			if (DialogService.DialogLayer.ShowEditor(EditingContent.AddressSelector,
				new AddressSelectorViewModel(_entrant.Address)))
			{
				RaisePropertyChanged("EditedEntrant");
			}
		}

        #endregion

        #endregion

        #endregion

        #region Identity Documents

        public IdentityDocument SelectedIdentityDocument { get; set; }

        ObservableCollection<IdentityDocument> _identityDocuments;

        public ObservableCollection<IdentityDocument> IdentityDocuments
        {
            get
            {
                if (_identityDocuments == null)
                {
                    _identityDocuments = new ObservableCollection<IdentityDocument>(EditedClaim.IdentityDocuments.ToList());
                }
                return _identityDocuments;
            }
            set
            {
                EditedClaim.IdentityDocuments = value;
                RaisePropertyChanged("IdentityDocuments");
            }
        }

        public RelayCommand AddIdentityDocumentCommand
		{
			get
			{
				return new RelayCommand(AddIdentityDocument);
			}
		}

		void AddIdentityDocument()
		{
            var doc = new IdentityDocument();
			var vm = new IdentityDocumentEditorViewModel(doc);
			if (DialogLayer.ShowEditor(EditingContent.IdentityDocumentEditor, vm))
			{
				IdentityDocuments.Add(doc);
                SelectedIdentityDocument = doc;
                RaisePropertyChanged("SelectedIdentityDocument");
                RaisePropertyChanged("SelectedIdentityDocument");
			}
		}

        public RelayCommand EditIdentityDocumentCommand { get => new RelayCommand(EditIdentityDocument); }

        void EditIdentityDocument()
        {
            
			var vm = new IdentityDocumentEditorViewModel(SelectedIdentityDocument);
			if (DialogLayer.ShowEditor(EditingContent.IdentityDocumentEditor, vm))
			{
                RaisePropertyChanged("IdentityDocuments");
                RaisePropertyChanged("SelectedIdentityDocument");
			}
        }

        public RelayCommand DeleteIdentityDocumentCommand { get => new RelayCommand(DeleteIdentityDocument); }

        void DeleteIdentityDocument()
        {
            if (Messenger.RemoveQuestion())
            {
                IdentityDocuments.Remove(SelectedIdentityDocument);
            }
        }

        #endregion

        #region ClaimConditions

        #region Entities

        ClaimCondition _selectedClaimCondition;

		public ClaimCondition SelectedClaimCondition
		{
			get
			{
				if (_selectedClaimCondition == null)
				{
					_selectedClaimCondition = _claim.ClaimConditions.FirstOrDefault();
				}
				return _selectedClaimCondition;
			}
			set
			{
				_selectedClaimCondition = value;
				RaisePropertyChanged("SelectedClaimCondition");
			}
		}

		ObservableCollection<ClaimCondition> _claimConditions;

		public ObservableCollection<ClaimCondition> ClaimConditions
		{
			get
			{	 				
				_claimConditions = new ObservableCollection<ClaimCondition>(_claim.ClaimConditions.ToList().OrderBy(c => c.Priority)); 
				return _claimConditions;
			}
			set
			{
				_claimConditions = value;
				RaisePropertyChanged("ClaimConditions");
			}
		}

		#endregion

		#region Commands

		#region Commands

		public RelayCommand NewClaimConditionCommand
		{
			get
			{
				return new RelayCommand(NewClaimCondition, NewClaimConditionCanExecute);
			}
		}

		public RelayCommand EditClaimConditionCommand
		{
			get
			{
				return new RelayCommand(EditClaimCondition);
			}
		}

		public RelayCommand RemoveClaimConditionCommand
		{
			get
			{
				return new RelayCommand(RemoveClaimCondition);
			}
		}


		#endregion

		#region Methods	   

		void NewClaimCondition()
		{
			var newClaimCondition = new ClaimCondition();
			newClaimCondition.CompetitiveGroup = Session.DataModel.CompetitiveGroups.FirstOrDefault();
			newClaimCondition.Priority = GetClaimConditionPriority();
			if (DialogLayer.ShowEditor(EditingContent.ClaimConditionEditor, 
				new ClaimConditionEditorViewModel(newClaimCondition)))
			{
				_claim.ClaimConditions.Add(newClaimCondition);
				ClaimConditions.Add(newClaimCondition);			   
				SelectedClaimCondition = newClaimCondition;	
				RaisePropertyChanged("ClaimConditions");
				GenerateRegistrationNumber();

				// ВНИМАНИЕ 
				// Чтобы избежать ситуевины с дублированием номера в тех случаях, когда два пользователя одновременно добавляют 
				// абитуриентов в одному регистрационную группу, сохраняем данные в рамках контекста. Это, конечно же, хуево и 
				// много чего непонятного может из этого говна всплыть, поэтому захерачим это дело только на первый приоритет.

				if (_claim.ClaimConditions.Count == 1)
				{
					Session.DataModel.SaveChanges();
				}	  
			}
		}

		void EditClaimCondition()
		{
			DialogLayer.ShowEditor(EditingContent.ClaimConditionEditor,
				new ClaimConditionEditorViewModel(SelectedClaimCondition));
			RaisePropertyChanged("ClaimConditions");
		}

		void RemoveClaimCondition()
		{
			if (Messenger.RemoveQuestion())
			{
				_claim.ClaimConditions.Remove(_selectedClaimCondition);
				ClaimConditions.Remove(_selectedClaimCondition);
				RaisePropertyChanged("ClaimConditions");
			}
		}

		#endregion

		#region CheckMethods

		bool NewClaimConditionCanExecute()
		{
			return EditedClaim.ClaimConditions.Count < 3;
		}

		#endregion

		#endregion		

		#region AdditionalLogic	   

		void GenerateRegistrationNumber()
		{
			//получаем конкурсную группу 
			CompetitiveGroup compGroup = null;
			try
			{
				compGroup = (from condition in EditedClaim.ClaimConditions
							 where condition.Priority == 1
							 select condition.CompetitiveGroup).Single();
			}
			catch (Exception)
			{
				compGroup = null;
			}

			// генерируем номер, если он еще не сгенерирован и если абитуриент записан 
			// на какую-либо конкурсную группу с первым приоритетом
			if (EditedClaim.Number == null && compGroup != null)
			{
				//составные части регистрационного номера
				string yearPart = string.Empty;
				string educationFormPart = string.Empty;
				string competitiveGroupPart = string.Empty;
				string sequenceNumberPart = string.Empty;

				//заполняем части номера
				yearPart = DateTime.Now.Year.ToString().Remove(0, 2);
				educationFormPart = compGroup.EducationForm.RegistrationNumberMemberPart;
				competitiveGroupPart = compGroup.RegistrationNumberMemberPart;

				string sql = string.Format("SELECT IF(RIGHT(MAX(c.Number), 4) IS NOT NULL, RIGHT(MAX(c.Number), 4), 0) + 1 FROM Claims c WHERE LEFT(c.Number, 4) = {0}{1}{2};",
					yearPart, educationFormPart, competitiveGroupPart);
				sequenceNumberPart = (Session.DataModel.Database.SqlQuery<string>(sql)).First();

				//забиваем порядковый номер нулями до длины 4 символов
				while (sequenceNumberPart.Length < 4)
				{
					sequenceNumberPart = string.Format("0{0}", sequenceNumberPart);
				}

				//все	  
				EditedClaim.Number = string.Format("{0}{1}{2}{3}", yearPart, educationFormPart, competitiveGroupPart, sequenceNumberPart);
				RaisePropertyChanged("EditedClaim");
			}
		}

		int GetClaimConditionPriority()
		//крайней ебанутости методы я умею писать - ПЕРЕПИСАТЬ
		{
			if (EditedClaim.ClaimConditions.Count > 0)
			{
				bool first = true;
				bool second = true;
				bool third = true;
				var priorities = (from condition in EditedClaim.ClaimConditions
								  orderby condition.Priority
								  select condition.Priority);
				foreach (var item in priorities)
				{
					if (item == 1)
					{
						first = false; 
					}
					else
					{
						if (item == 2)
						{
							second = false;
						}
						else
						{
							if (item == 3)
							{
								third = false;
							}
						}
					}
				}

				if (first)
				{
					return 1;
				}
				else
				{
					if (second)
					{
						return 2;
					}
					else
					{
						if (third)
						{
							return 3;
						}
						else
						{
							return 0;
						}
					}
				}
			}
			else
			{
				return 1;
			}
		}

		#endregion

		#endregion
				   
		#region OrphanDocuments

		OrphanDocument _selectedOrphanDocument;

		public OrphanDocument SelectedOrphanDocument
		{
			get
			{
				if (_selectedOrphanDocument == null)
				{
					_selectedOrphanDocument = _claim.OrphanDocuments.FirstOrDefault();
				}
				return _selectedOrphanDocument;
			}
			set
			{
				_selectedOrphanDocument = value;
				RaisePropertyChanged("SelectedOrphanDocument");
			}
		}


		ObservableCollection<OrphanDocument> _orphanDocuments;

		public ObservableCollection<OrphanDocument> OrphanDocuments
		{
			get
			{
				_orphanDocuments = new ObservableCollection<OrphanDocument>(_claim.OrphanDocuments);
				return _orphanDocuments;
			}
			set
			{
				_orphanDocuments = value;
				RaisePropertyChanged("OrphanDocuments");
			}
		}


		public RelayCommand AddOrphanDocumentCommand
		{
			get
			{
				return new RelayCommand(AddOrphanDocument);
			}
		}
						   
		void AddOrphanDocument()
		{
			var newDoc = new OrphanDocument();
			if (DialogLayer.ShowEditor(EditingContent.OrphanDocumentEditor,
				new OrphanDocumentEditorViewModel(newDoc)))
			{
				_claim.OrphanDocuments.Add(newDoc);
				RaisePropertyChanged("OrphanDocuments");
			}
		}


		public RelayCommand EditOrphanDocumentCommand
		{
			get
			{
				return new RelayCommand(EditOrphanDocument);
			}
		}

		void EditOrphanDocument()
		{
			if (DialogLayer.ShowEditor(EditingContent.OrphanDocumentEditor,
				new OrphanDocumentEditorViewModel(_selectedOrphanDocument)))
			{
				RaisePropertyChanged("OrphanDocuments");
			}
		}



		public RelayCommand RemoveOrphanDocumentCommand
		{
			get
			{
				return new RelayCommand(RemoveOrphanDocument);
			}
		}

		void RemoveOrphanDocument()
		{
			if (Messenger.RemoveQuestion())
			{
				_claim.OrphanDocuments.Remove(_selectedOrphanDocument);
				RaisePropertyChanged("OrphanDocuments");
			}
		}

		#endregion

		#region SchoolCertificateDocuments 

		#region ParentEntities	 

		SchoolCertificateDocument _selectedSchoolCertificateDocument;

		public SchoolCertificateDocument SelectedSchoolCertificateDocument
		{
			get
			{
				if (_selectedSchoolCertificateDocument == null)
				{
					_selectedSchoolCertificateDocument = _claim.SchoolCertificateDocuments.FirstOrDefault();
				}
				return _selectedSchoolCertificateDocument;
			}
			set
			{
				_selectedSchoolCertificateDocument = value;
				RaisePropertyChanged("SelectedSchoolCertificateDocument");
			}
		}

		public ObservableCollection<SchoolCertificateDocument> SchoolCertificateDocuments
		{
			get
			{
				return new ObservableCollection<SchoolCertificateDocument>(_claim.SchoolCertificateDocuments);
			}
			set
			{
				_claim.SchoolCertificateDocuments = value;
				RaisePropertyChanged("SchoolCertificateDocuments");
			}
		}

		#endregion

		#region Commands


		#region Commands


		public RelayCommand AddSchoolCertificateDocumentCommand
		{
			get
			{
				return new RelayCommand(AddSchoolCertificateDocument);
			}
		}

		public RelayCommand EditSchoolCertificateDocumentCommand
		{
			get
			{
				return new RelayCommand(EditSchoolCertificateDocument);
			}
		}

		public RelayCommand RemoveSchoolCertificateDocumentCommand
		{
			get
			{
				return new RelayCommand(RemoveSchoolCertificateDocument);
			}
		}

		#endregion


		#region Methods	   	   

		void AddSchoolCertificateDocument()
		{
			var doc = new SchoolCertificateDocument();
			doc.EducationOrganization = Session.DataModel.EducationOrganizations.FirstOrDefault();
			if (DialogLayer.ShowEditor(EditingContent.EducationDocumentEditor,
					new SchoolCertificateDocumentEditorViewModel(doc)))
			{
				EditedClaim.SchoolCertificateDocuments.Add(doc);
				RaisePropertyChanged("SchoolCertificateDocuments");
			}
		}
									  
		void EditSchoolCertificateDocument()
		{
			
			if (DialogLayer.ShowEditor(EditingContent.EducationDocumentEditor,
					new SchoolCertificateDocumentEditorViewModel(_selectedSchoolCertificateDocument)))
			{		
				RaisePropertyChanged("SchoolCertificateDocuments");
			}
		}
											  
		void RemoveSchoolCertificateDocument()
		{
			if (Messenger.RemoveQuestion())
			{
				_claim.SchoolCertificateDocuments.Remove(_selectedSchoolCertificateDocument);
				Session.DataModel.SchoolCertificateDocuments.Remove(_selectedSchoolCertificateDocument);
				RaisePropertyChanged("SchoolCertificateDocuments");
			}
		}

		#endregion


		#region CheckMethods

		#endregion

		#endregion

		#endregion

		#region MiddleEducationDiplomaDocuments	 

		#region ParentEntities	

		MiddleEducationDiplomaDocument _selectedMiddleEducationDiplomaDocument;

		public MiddleEducationDiplomaDocument SelectedMiddleEducationDiplomaDocument
		{
			get
			{
				if (_selectedMiddleEducationDiplomaDocument == null)
				{
					_selectedMiddleEducationDiplomaDocument = _claim.MiddleEducationDiplomaDocuments.FirstOrDefault();
				}
				return _selectedMiddleEducationDiplomaDocument;
			}

			set
			{
				_selectedMiddleEducationDiplomaDocument = value;
				RaisePropertyChanged("SelectedMiddleEducationDiplomaDocument");
			}
		}

		public ObservableCollection<MiddleEducationDiplomaDocument> MiddleEducationDiplomaDocuments
		{
			get
			{
				return new ObservableCollection<MiddleEducationDiplomaDocument>(_claim.MiddleEducationDiplomaDocuments.ToList());
			}
			set
			{
				_claim.MiddleEducationDiplomaDocuments = value;
				RaisePropertyChanged("MiddleEducationDiplomaDocuments");
			}
		} 							   

		#endregion

		#region Commands


		#region Commands


		public RelayCommand AddMiddleEducationDiplomaDocumentCommand
		{
			get
			{
				return new RelayCommand(AddMiddleEducationDiplomaDocument);
			}
		}

		public RelayCommand EditMiddleEducationDiplomaDocumentCommand
		{
			get
			{
				return new RelayCommand(EditMiddleEducationDiplomaDocument);
			}
		}

		public RelayCommand RemoveMiddleEducationDiplomaDocumentCommand
		{
			get
			{
				return new RelayCommand(RemoveMiddleEducationDiplomaDocument);
			}
		}

		#endregion


		#region Methods	   	   

		void AddMiddleEducationDiplomaDocument()
		{
			var doc = new MiddleEducationDiplomaDocument();
			doc.EducationOrganization = Session.DataModel.EducationOrganizations.FirstOrDefault();
			if (DialogLayer.ShowEditor(EditingContent.EducationDocumentEditor,
					new MiddleEducationDiplomaDocumentEditorViewModel(doc)))
			{
				EditedClaim.MiddleEducationDiplomaDocuments.Add(doc);
				RaisePropertyChanged("MiddleEducationDiplomaDocuments");
			}
		}

		void EditMiddleEducationDiplomaDocument()
		{

			if (DialogLayer.ShowEditor(EditingContent.EducationDocumentEditor,
					new MiddleEducationDiplomaDocumentEditorViewModel(_selectedMiddleEducationDiplomaDocument)))
			{
				RaisePropertyChanged("MiddleEducationDiplomaDocuments");
			}
		}

		void RemoveMiddleEducationDiplomaDocument()
		{
			if (Messenger.RemoveQuestion())
			{
				_claim.MiddleEducationDiplomaDocuments.Remove(_selectedMiddleEducationDiplomaDocument);
				Session.DataModel.MiddleEducationDiplomaDocuments.Remove(_selectedMiddleEducationDiplomaDocument);
				RaisePropertyChanged("MiddleEducationDiplomaDocuments");
			}
		}

		#endregion


		#region CheckMethods

		#endregion

		#endregion

		#endregion

		#region HighEducationDiplomaDocuments 	 

		#region ParentEntities		

		HighEducationDiplomaDocument _selectedHighEducationDiplomaDocument;

		public HighEducationDiplomaDocument SelectedHighEducationDiplomaDocument
		{
			get
			{
				if (_selectedHighEducationDiplomaDocument == null)
				{
					_selectedHighEducationDiplomaDocument = _claim.HighEducationDiplomaDocuments.FirstOrDefault();
				}
				return _selectedHighEducationDiplomaDocument;
			}

			set
			{
				_selectedHighEducationDiplomaDocument = value;
				RaisePropertyChanged("SelectedHighEducationDiplomaDocument");
			}
		}

		public ObservableCollection<HighEducationDiplomaDocument> HighEducationDiplomaDocuments
		{
			get
			{
				return new ObservableCollection<HighEducationDiplomaDocument>(_claim.HighEducationDiplomaDocuments.ToList());
			}
			set
			{
				_claim.HighEducationDiplomaDocuments = value;
				RaisePropertyChanged("HighEducationDiplomaDocuments");
			}
		}													  

		#endregion

		#region Commands


		#region Commands


		public RelayCommand AddHighEducationDiplomaDocumentCommand
		{
			get
			{
				return new RelayCommand(AddHighEducationDiplomaDocument);
			}
		}

		public RelayCommand EditHighEducationDiplomaDocumentCommand
		{
			get
			{
				return new RelayCommand(EditHighEducationDiplomaDocument);
			}
		}

		public RelayCommand RemoveHighEducationDiplomaDocumentCommand
		{
			get
			{
				return new RelayCommand(RemoveHighEducationDiplomaDocument);
			}
		}

		#endregion


		#region Methods	   	   

		void AddHighEducationDiplomaDocument()
		{
			var doc = new HighEducationDiplomaDocument();
			doc.EducationOrganization = Session.DataModel.EducationOrganizations.FirstOrDefault();
			if (DialogLayer.ShowEditor(EditingContent.EducationDocumentEditor,
					new HighEducationDiplomaDocumentEditorViewModel(doc)))
			{
				EditedClaim.HighEducationDiplomaDocuments.Add(doc);
				RaisePropertyChanged("HighEducationDiplomaDocuments");
			}
		}

		void EditHighEducationDiplomaDocument()
		{

			if (DialogLayer.ShowEditor(EditingContent.EducationDocumentEditor,
					new HighEducationDiplomaDocumentEditorViewModel(_selectedHighEducationDiplomaDocument)))
			{
				RaisePropertyChanged("HighEducationDiplomaDocuments");
			}
		}

		void RemoveHighEducationDiplomaDocument()
		{
			if (Messenger.RemoveQuestion())
			{
				_claim.HighEducationDiplomaDocuments.Remove(_selectedHighEducationDiplomaDocument);
				Session.DataModel.HighEducationDiplomaDocuments.Remove(_selectedHighEducationDiplomaDocument);
				RaisePropertyChanged("HighEducationDiplomaDocuments");
			}
		}

		#endregion


		#region CheckMethods

		#endregion


		#endregion 

		#endregion

		#region EgeDocuments

		ObservableCollection<EgeResult> _egeResults;
		
		public ObservableCollection<EgeResult> EgeResults
		{
			get
			{
				//добавляем результаты ЕГЭ по всем свидетельствам
				_egeResults = new ObservableCollection<EgeResult>();
				var docList = _claim.EgeDocuments;
				foreach (var doc in docList)
				{
					var resList = doc.EgeResults;
					foreach (var res in resList)
					{
						_egeResults.Add(res);
					}
				}
				return _egeResults;
			}
		}


		public RelayCommand ShowEgeDocumentsEditorCommand
		{
			get
			{
				return new RelayCommand(ShowEgeDocumentsEditor);
			}
		}

		void ShowEgeDocumentsEditor()
		{
			var validator = new EgeDocumentValidator(_claim);

			if(DialogLayer.ShowEditor(EditingContent.EgeDocumentsEditor, 
				new EgeDocumentsEditorViewModel(_claim), validator))
			{
				RaisePropertyChanged("EgeResults");
			}
		}


		#endregion

		#region EntranceTestResults

		EntranceTestResult _selectedEntranceTestResult;

		public EntranceTestResult SelectedEntranceTestResult
		{
			get
			{
				if (_selectedEntranceTestResult == null)
				{
					_selectedEntranceTestResult = _claim.EntranceTestResults.FirstOrDefault();
				}
				return _selectedEntranceTestResult;
			}
			set
			{
				_selectedEntranceTestResult = value;
				RaisePropertyChanged("SelectedEntranceTestResult");
			}
		}


		ObservableCollection<EntranceTestResult> _entranceTestResults;

		public ObservableCollection<EntranceTestResult> EntranceTestResults
		{
			get
			{	
				_entranceTestResults = new ObservableCollection<EntranceTestResult>(_claim.EntranceTestResults);  
				return _entranceTestResults;
			}
			set
			{
				_entranceTestResults = value;
				RaisePropertyChanged("EntranceTestResults");
			}
		}

		
		public RelayCommand AddEntranceTestResultCommand
		{
			get
			{
				return new RelayCommand(AddEntranceTestResult);
			}
		}
		  
		void AddEntranceTestResult()
		{	  
			var value = new EntranceTestResult();
			value.Claim = _claim;
			_claim.EntranceTestResults.Add(value);
			EntranceTestResults.Add(value);
			SelectedEntranceTestResult = value;
			RaisePropertyChanged("EntranceTestResults");
		}

		
		public RelayCommand RemoveEntranceTestResultCommand
		{
			get
			{
				return new RelayCommand(RemoveEntranceTestResult);
			}
		}

		void RemoveEntranceTestResult()
		{
			if (Messenger.RemoveQuestion())
			{
				_claim.EntranceTestResults.Remove(_selectedEntranceTestResult);
				EntranceTestResults.Remove(_selectedEntranceTestResult);
				RaisePropertyChanged("EntranceTestResults");
			}
		}

		#endregion

		#region IndividualAchievements

		EntranceIndividualAchievement _selectedIndividualAchievement;

		public EntranceIndividualAchievement SelectedIndividualAchievement
		{
			get
			{
				if (_selectedIndividualAchievement == null)
				{
					_selectedIndividualAchievement = _claim.EntranceIndividualAchievements.FirstOrDefault();
				}
				return _selectedIndividualAchievement;
			}
			set
			{
				_selectedIndividualAchievement = value;
				RaisePropertyChanged("SelectedIndividualAchievement");
			}
		}


		ObservableCollection<EntranceIndividualAchievement> _individualAchievements;

		public ObservableCollection<EntranceIndividualAchievement> IndividualAchievements
		{
			get
			{
				_individualAchievements = new ObservableCollection<EntranceIndividualAchievement>(_claim.EntranceIndividualAchievements);
				return _individualAchievements;
			}
			set
			{
				_individualAchievements = value;
				RaisePropertyChanged("IndividualAchievements");
			}
		}

		public ObservableCollection<CampaignIndividualAchievement> AvailableIndividualAchievements
		{
			get
			{
				return new ObservableCollection<CampaignIndividualAchievement>(Session.DataModel.CampaignIndividualAchievements);
			}
		}


		public RelayCommand AddIndividualAchievementCommand
		{
			get
			{
				return new RelayCommand(AddIndividualAchievement);
			}
		}

		public RelayCommand EditIndividualAchievementCommand
		{
			get
			{
				return new RelayCommand(EditIndividualAchievement);
			}
		}

		public RelayCommand RemoveIndividualAchievementCommand
		{
			get
			{
				return new RelayCommand(RemoveIndividualAchievement);
			}
		}


		void AddIndividualAchievement()
		{
			_selectedIndividualAchievement = new EntranceIndividualAchievement();
			if (DialogLayer.ShowEditor(EditingContent.EntranceIndividualAchievementEditor,
				this))
			{
				_claim.EntranceIndividualAchievements.Add(_selectedIndividualAchievement);
				RaisePropertyChanged("IndividualAchievements");
			}
		}

		void EditIndividualAchievement()
		{
			if (DialogLayer.ShowEditor(EditingContent.EntranceIndividualAchievementEditor, this))
			{
				RaisePropertyChanged("IndividualAchievements");
			}
		}

		void RemoveIndividualAchievement()
		{
			if (Messenger.RemoveQuestion())
			{
				_claim.EntranceIndividualAchievements.Remove(_selectedIndividualAchievement);
				RaisePropertyChanged("IndividualAchievements");
			}
		}

		#endregion

		#region AdditionalCollections 
	  						
		public ObservableCollection<ClaimStatus> ClaimStatuses
		{
			get
			{																																																														  
				return new ObservableCollection<ClaimStatus>(Session.DataModel.ClaimStatuses.ToList());
			}	 
		}

        public ObservableCollection<MarritalStatus> MarritalStatuses
        {
            get => new ObservableCollection<MarritalStatus>(Session.DataModel.MarritalStatuses.ToList());
        }

							  
		#endregion
							
	} 
}  