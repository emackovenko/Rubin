using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.ExportData;
using Model.Admission;
using System.Xml.Serialization;
using System.IO;
using System.Data.Entity;

namespace Admission.ViewModel.Export
{
	public class Exporter
	{
		private const string DATE_FORMAT = "yyyy-MM-dd";
		private const string DATE_TIME_FORMAT = "yyyy-MM-ddT00:00:00";

		public static void GeneratePackage()
		{
			var exporter = new Exporter();

			var root = new Root();

			exporter.FillPackage(root);

			Microsoft.Win32.SaveFileDialog save = new Microsoft.Win32.SaveFileDialog();
			save.Filter = "XML files|*.xml";
			string fileName = string.Format("Applications_{0}.xml", 
				DateTime.Now.ToString("dd.MM.yyyy"));				
			if (save.ShowDialog() ?? false)
			{
				fileName = save.FileName;

				using (Stream saver = new FileStream(fileName, FileMode.Create, FileAccess.Write))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(Root));
                    serializer.Serialize(saver, root);
				}
			}
		}

		public void FillPackage(Root root)
		{
			#region блок авторизации

			var auth = new AuthData
			{
				Login = "cpk@agtu.secna.ru",
				Pass = "PtVET8k",
				InstitutionID = "2536"
			};

			#endregion

			#region импортируемые данные

			var package = new PackageData();


			#region заявления

			var apps = new List<Application>();


			#region заявление

			var context = new AdmissionDatabase();

			context.Claims.Load();

            var collection = (from claim in context.Claims
                              where claim.ClaimStatus.Id == 1 || claim.ClaimStatus.Id == 2 || claim.ClaimStatus.Id == 4
                              select claim).ToList();

            collection = collection.Where(c => c.Campaign.CampaignStatusId == 2).ToList();

			foreach (var claim in collection)
			{
				var app = new Application()
				{
					UID = string.Format("RiiApplication_{0}", claim.Id),
					ApplicationNumber = claim.Number,
					RegistrationDate = ConvertDateTime(claim.RegistrationDate),
					NeedHostel = claim.IsHostelNeed ?? false,
					StatusID = claim.ClaimStatus.ExportCode
				};

				#region абитуриент

				var entrant = claim.Entrants.First();

				var entrantFis = new Model.ExportData.Entrant()
				{
					UID = string.Format("RiiEntrant_{0}", entrant.Id),
					LastName = entrant.LastName,
					FirstName = entrant.FirstName,
					MiddleName = entrant.Patronymic,
					GenderID = entrant.Gender.Id,
					CustomInformation = "---"
				};

				var email = new EmailOrMailAddress()
				{
					Email = entrant.Email ?? "---"
				};

				entrantFis.EmailOrMailAddress = email;
				app.Entrant = entrantFis;

				#endregion

				#region условия приема

				var finSourceAndEduForms = new List<FinSourceEduForm>();

				foreach (var condition in claim.ClaimConditions)
				{
					var finSourceAndEduForm = new FinSourceEduForm()
					{
						CompetitiveGroupUID = condition.CompetitiveGroup.ExportCode
					};

					// Согласие на зачисление
					if (claim.EnrollmentClaims.Where(ec => ec.EnrollmentExceptionOrder == null).Count() > 0)
					{
						if (claim.EnrollmentClaims.Where(ec => ec.EnrollmentExceptionOrder == null).First().EnrollmentProtocol.CompetitiveGroup.Id ==
							condition.CompetitiveGroup.Id)
						{
							if (claim.SchoolCertificateDocuments.Count > 0)
							{
								if (claim.SchoolCertificateDocuments.First().OriginalReceivedDate.HasValue)
								{
									finSourceAndEduForm.IsAgreedDate = ((DateTime)claim.SchoolCertificateDocuments.First().OriginalReceivedDate).ToString(DATE_TIME_FORMAT);
								}
								else
								{
									finSourceAndEduForm.IsAgreedDate = ConvertDateTime(claim.RegistrationDate);
								}
							}
							if (claim.MiddleEducationDiplomaDocuments.Count > 0)
							{
								if (claim.MiddleEducationDiplomaDocuments.First().OriginalReceivedDate.HasValue)
								{
									finSourceAndEduForm.IsAgreedDate = ((DateTime)claim.MiddleEducationDiplomaDocuments.First().OriginalReceivedDate).ToString(DATE_TIME_FORMAT);
								}
								else
								{
									finSourceAndEduForm.IsAgreedDate = ConvertDateTime(claim.RegistrationDate);
								}
							}
							if (claim.HighEducationDiplomaDocuments.Count > 0)
							{
								if (claim.HighEducationDiplomaDocuments.First().OriginalReceivedDate.HasValue)
								{
									finSourceAndEduForm.IsAgreedDate = ((DateTime)claim.HighEducationDiplomaDocuments.First().OriginalReceivedDate).ToString(DATE_TIME_FORMAT);
								}
								else
								{
									finSourceAndEduForm.IsAgreedDate = ConvertDateTime(claim.RegistrationDate);
								}
							}
						}
					}

					finSourceAndEduForms.Add(finSourceAndEduForm);
				}

				app.FinSourceAndEduForms = finSourceAndEduForms;

				#endregion

				#region документы, приложенные к заявлению

				var docs = new ApplicationDocuments();

				#region свидетельства ЕГЭ

				if (claim.EgeDocuments.Count > 0)
				{
					var egeDocs = new List<Model.ExportData.EgeDocument>();

					foreach (var egeDoc in claim.EgeDocuments)
					{
						var doc = new Model.ExportData.EgeDocument()
						{
							UID = string.Format("RiiEgeDocument_{0}", egeDoc.Id),
							DocumentYear = (int)egeDoc.Year
						};

						#region предметы

						var results = new List<SubjectData>();

						foreach (var res in egeDoc.EgeResults)
						{
							var subjectData = new SubjectData()
							{
								SubjectID = res.ExamSubject.ExportCode,
								Value = (int)res.Value
							};

							results.Add(subjectData);
						}

						doc.Subjects = results;

						#endregion

						egeDocs.Add(doc);
					}

					docs.EgeDocuments = egeDocs;

				}
				#endregion

				#region удостоверение личности

				var idDoc = new Model.ExportData.IdentityDocument()
				{
					UID = string.Format("RiiIdentityDocument_{0}", claim.IdentityDocuments.First().Id),
					DocumentSeries = claim.IdentityDocuments.First().Series,
					DocumentNumber = claim.IdentityDocuments.First().Number,
					SubdivisionCode = claim.IdentityDocuments.First().SubdivisionCode,
					DocumentDate = ConvertDate(claim.IdentityDocuments.First().Date),
					DocumentOrganization = claim.IdentityDocuments.First().Organization,
					IdentityDocumentTypeID = claim.IdentityDocuments.First().IdentityDocumentType.ExportCode,
					NationalityTypeID = claim.IdentityDocuments.First().Citizenship.ExportCode,
					LastName = entrantFis.LastName,
					FirstName = entrantFis.FirstName,
					MiddleName = entrantFis.MiddleName,
					GenderID = entrantFis.GenderID,
					BirthDate = ConvertDate(claim.Person.BirthDate),
					BirthPlace = claim.Person.BirthPlace
				};

				docs.IdentityDocument = idDoc;

				#region otherIdentitiDocuments

				if (claim.IdentityDocuments.Count > 1)
				{
					var idDocs = claim.IdentityDocuments.ToList();
					idDocs.Remove(claim.IdentityDocuments.First());

					docs.OtherIdentityDocuments = new List<Model.ExportData.IdentityDocument>();

					foreach (var oIddoc in idDocs)
					{
						var eOIdDoc = new Model.ExportData.IdentityDocument
						{
							UID = string.Format("RiiIdentityDocument_{0}", oIddoc.Id),
							DocumentSeries = oIddoc.Series,
							DocumentNumber = oIddoc.Number,
							SubdivisionCode = claim.IdentityDocuments.First().SubdivisionCode,
							DocumentDate = ConvertDate(oIddoc.Date),
							DocumentOrganization = claim.IdentityDocuments.First().Organization,
							IdentityDocumentTypeID = claim.IdentityDocuments.First().IdentityDocumentType.ExportCode,
							NationalityTypeID = claim.IdentityDocuments.First().Citizenship.ExportCode,
							LastName = entrantFis.LastName,
							FirstName = entrantFis.FirstName,
							MiddleName = entrantFis.MiddleName,
							GenderID = entrantFis.GenderID,
							BirthDate = ConvertDate(claim.Person.BirthDate),
							BirthPlace = claim.Person.BirthPlace
						};
						docs.OtherIdentityDocuments.Add(eOIdDoc);
					}
				}

				#endregion

				#endregion

				#region документы об образовании

				var eduDocs = new List<EduDocument>();

				#region аттестат

				if (claim.SchoolCertificateDocuments.Count > 0)
				{
					var ourDoc = claim.SchoolCertificateDocuments.First();
					var attestat = new Model.ExportData.SchoolCertificateDocument()
					{
						UID = string.Format("RiiSchoolCertificateDocument_{0}", ourDoc.Id),
						OriginalReceivedDate = ConvertDate(ourDoc.OriginalReceivedDate),
						DocumentSeries = ourDoc.Series,
						DocumentNumber = ourDoc.Number,
						DocumentDate = ConvertDate(ourDoc.Date),
						DocumentOrganization = ourDoc.EducationOrganization.Name,
						GPA = ourDoc.MiddleMark,
						EndYear = (int)(((DateTime)ourDoc.Date).Year)
					};

					var eduDoc = new EduDocument()
					{
						SchoolCertificateDocument = attestat
					};

					eduDocs.Add(eduDoc);
				}

				#endregion

				#region диплом СПО  

				if (claim.MiddleEducationDiplomaDocuments.Count > 0)
				{
					var ourDoc = claim.MiddleEducationDiplomaDocuments.First();
					var diploma = new Model.ExportData.MiddleEduDiplomaDocument()
					{
						UID = string.Format("RiiMiddleEduDiplomaDocument_{0}", ourDoc.Id),
						OriginalReceivedDate = ConvertDate(ourDoc.OriginalReceivedDate),
						DocumentSeries = ourDoc.Series,
						DocumentNumber = ourDoc.Number,
						DocumentDate = ConvertDate(ourDoc.Date),
						DocumentOrganization = ourDoc.EducationOrganization.Name,
						GPA = ourDoc.MiddleMark,
						EndYear = (int)(((DateTime)ourDoc.Date).Year)
					};

					var eduDoc = new EduDocument()
					{
						MiddleEduDiplomaDocument = diploma
					};

					eduDocs.Add(eduDoc);
				}

				#endregion

				#region Диплом ВО	

				if (claim.HighEducationDiplomaDocuments.Count > 0)
				{
					var ourDoc = claim.HighEducationDiplomaDocuments.First();
					var diploma = new Model.ExportData.HighEduDiplomaDocument()
					{
						UID = string.Format("RiiHighEduDiplomaDocument_{0}", ourDoc.Id),
						OriginalReceivedDate = ConvertDate(ourDoc.OriginalReceivedDate),
						DocumentSeries = ourDoc.Series,
						DocumentNumber = ourDoc.Number,
						DocumentDate = ConvertDate(ourDoc.Date),
						DocumentOrganization = ourDoc.EducationOrganization.Name,
						GPA = ourDoc.MiddleMark,
						EndYear = (int)(((DateTime)ourDoc.Date).Year)
					};

					var eduDoc = new EduDocument()
					{
						HighEduDiplomaDocument = diploma
					};

					eduDocs.Add(eduDoc);
				}

				#endregion

				#endregion

				docs.EduDocuments = eduDocs;

				app.ApplicationDocuments = docs;

				#endregion

				#region вступительные испытания

				app.EntranceTestResults = new List<Model.ExportData.EntranceTestResult>();

				#region егэ

				foreach (var cond in claim.ClaimConditions)
				{
					foreach (var egeDoc in claim.EgeDocuments)
					{
						foreach (var egeResult in egeDoc.EgeResults)
						{
							if (egeResult.Value > 0 && (egeResult.IsChecked ?? false))
							{
								var egeSubj = new EntranceTestSubject
								{
									SubjectID = int.Parse(egeResult.ExamSubject.ExportCode)
								};
								var resultDoc = new ResultDocument
								{
									EgeDocumentID = string.Format("RiiEgeDocument_{0}", egeDoc.Id)
								};
								var egeRes = new Model.ExportData.EntranceTestResult
								{
									UID = string.Format("EgeResult_{0}_{1}", egeResult.Id, cond.Id),
									ResultValue = egeResult.Value ?? 0,
									ResultSourceTypeID = 1,
									EntranceTestSubject = egeSubj,
									EntranceTestTypeID = 1,
									CompetitiveGroupUID = cond.CompetitiveGroup.ExportCode,
									ResultDocument = resultDoc
								};
								app.EntranceTestResults.Add(egeRes);
							}
						}
					}
				}

				#endregion

				#region СЭ

				foreach (var cond in claim.ClaimConditions)
				{
					foreach (var examResult in claim.EntranceTestResults)
					{
						if (examResult.Value > 0 && (examResult.EntranceTest.EntranceExaminationsCheckProtocol != null))
						{
							var examSubj = new EntranceTestSubject
							{
								SubjectID = int.Parse(examResult.EntranceTest.ExamSubject.ExportCode)
							};
							var resultDoc = new ResultDocument
							{
								InstitutionDocument = new InstitutionDocument
								{
									DocumentNumber = examResult.EntranceTest.EntranceExaminationsCheckProtocol.Number,
									DocumentDate = ((DateTime)examResult.EntranceTest.EntranceExaminationsCheckProtocol.Date).ToString(DATE_FORMAT),
									DocumentTypeID = 1
								}
							};
							var exportResult = new Model.ExportData.EntranceTestResult
							{
								UID = string.Format("ExaminationResult_{1}_{0}", examResult.Id, cond.Id),
								ResultValue = examResult.Value ?? 0,
								ResultSourceTypeID = 2,
								EntranceTestSubject = examSubj,
								EntranceTestTypeID = 1,
								CompetitiveGroupUID = cond.CompetitiveGroup.ExportCode,
								ResultDocument = resultDoc
							};
							app.EntranceTestResults.Add(exportResult);
						}
					}
				}

				if (app.EntranceTestResults.Count == 0)
				{
					app.EntranceTestResults = null;
				}

				#endregion

				#endregion

				#region льготы



				#endregion

				apps.Add(app);
			}

			#endregion


			package.Applications = apps;

			#endregion


			#endregion

			root.AuthData = auth;
			root.PackageData = package;

		}

		public void FillEnrolledPackage(Root root)
		{
			#region блок авторизации

			var auth = new AuthData
			{
				Login = "cpk@agtu.secna.ru",
				Pass = "PtVET8k",
				InstitutionID = "2536"
			};

			#endregion

			#region импортируемые данные

			var package = new PackageData();


			#region заявления

			var apps = new List<Application>();


			#region заявление

			var context = new AdmissionDatabase();

			context.Claims.Load();

			var collection = (from ec in context.EnrollmentClaims
							  where ec.EnrollmentProtocol.EnrollmentOrder.Number[0] == '9' &&
							  ec.Claim != null
							  select ec.Claim).ToList();

			foreach (var claim in collection)
			{
				var app = new Application()
				{
					UID = string.Format("RiiApplication_{0}", claim.Id),
					ApplicationNumber = claim.Number,
					RegistrationDate = ConvertDateTime(claim.RegistrationDate),
					NeedHostel = claim.IsHostelNeed ?? false,
					StatusID = claim.ClaimStatus.ExportCode
				};

				#region абитуриент

				var entrant = claim.Entrants.First();

				var entrantFis = new Model.ExportData.Entrant()
				{
					UID = string.Format("RiiEntrant_{0}", entrant.Id),
					LastName = entrant.LastName,
					FirstName = entrant.FirstName,
					MiddleName = entrant.Patronymic,
					GenderID = entrant.Gender.Id,
					CustomInformation = "---"
				};

				var email = new EmailOrMailAddress()
				{
					Email = entrant.Email ?? "---"
				};

				entrantFis.EmailOrMailAddress = email;
				app.Entrant = entrantFis;

				#endregion

				#region условия приема

				var finSourceAndEduForms = new List<FinSourceEduForm>();

				foreach (var condition in claim.ClaimConditions)
				{
					var finSourceAndEduForm = new FinSourceEduForm()
					{
						CompetitiveGroupUID = condition.CompetitiveGroup.ExportCode
					};

					// Дата согласия на зачисление
					if (claim.EnrollmentClaims.Count > 0)
					{
						var enrollmentCompGroup = claim.EnrollmentClaims.First().EnrollmentProtocol.CompetitiveGroup;
						if (condition.CompetitiveGroup.Id == enrollmentCompGroup.Id)
						{
							if (claim.SchoolCertificateDocuments.Count > 0)
							{
								finSourceAndEduForm.IsAgreedDate =
									((DateTime)claim.SchoolCertificateDocuments.First().OriginalReceivedDate).ToString(DATE_TIME_FORMAT);
							}
							if (claim.MiddleEducationDiplomaDocuments.Count > 0)
							{
								finSourceAndEduForm.IsAgreedDate =
									((DateTime)claim.MiddleEducationDiplomaDocuments.First().OriginalReceivedDate).ToString(DATE_TIME_FORMAT);
							}
							if (claim.HighEducationDiplomaDocuments.Count > 0)
							{
								finSourceAndEduForm.IsAgreedDate =
									((DateTime)claim.HighEducationDiplomaDocuments.First().OriginalReceivedDate).ToString(DATE_TIME_FORMAT);
							}
						}
					}

					finSourceAndEduForms.Add(finSourceAndEduForm);
				}

				app.FinSourceAndEduForms = finSourceAndEduForms;

				#endregion

				#region документы, приложенные к заявлению

				var docs = new ApplicationDocuments();

				#region свидетельства ЕГЭ

				if (claim.EgeDocuments.Count > 0)
				{
					var egeDocs = new List<Model.ExportData.EgeDocument>();

					foreach (var egeDoc in claim.EgeDocuments)
					{
						var doc = new Model.ExportData.EgeDocument()
						{
							UID = string.Format("RiiEgeDocument_{0}", egeDoc.Id),
							DocumentYear = (int)egeDoc.Year
						};

						#region предметы

						var results = new List<SubjectData>();

						foreach (var res in egeDoc.EgeResults)
						{
							var subjectData = new SubjectData()
							{
								SubjectID = res.ExamSubject.ExportCode,
								Value = (int)res.Value
							};

							results.Add(subjectData);
						}

						doc.Subjects = results;

						#endregion

						egeDocs.Add(doc);
					}

					docs.EgeDocuments = egeDocs;

				}
				#endregion

				#region удостоверение личности

				var idDoc = new Model.ExportData.IdentityDocument()
				{
					UID = string.Format("RiiIdentityDocument_{0}", claim.IdentityDocuments.First().Id),
					DocumentSeries = claim.IdentityDocuments.First().Series,
					DocumentNumber = claim.IdentityDocuments.First().Number,
					SubdivisionCode = claim.IdentityDocuments.First().SubdivisionCode,
					DocumentDate = ConvertDate(claim.IdentityDocuments.First().Date),
					DocumentOrganization = claim.IdentityDocuments.First().Organization,
					IdentityDocumentTypeID = claim.IdentityDocuments.First().IdentityDocumentType.ExportCode,
					NationalityTypeID = claim.IdentityDocuments.First().Citizenship.ExportCode,
					LastName = entrantFis.LastName,
					FirstName = entrantFis.FirstName,
					MiddleName = entrantFis.MiddleName,
					GenderID = entrantFis.GenderID,
					BirthDate = ConvertDate(claim.Person.BirthDate),
					BirthPlace = claim.Person.BirthPlace
				};

				docs.IdentityDocument = idDoc;

				#endregion

				#region документы об образовании

				var eduDocs = new List<EduDocument>();

				#region аттестат

				if (claim.SchoolCertificateDocuments.Count > 0)
				{
					var ourDoc = claim.SchoolCertificateDocuments.First();
					var attestat = new Model.ExportData.SchoolCertificateDocument()
					{
						UID = string.Format("RiiSchoolCertificateDocument_{0}", ourDoc.Id),
						OriginalReceivedDate = ConvertDate(ourDoc.OriginalReceivedDate),
						DocumentSeries = ourDoc.Series,
						DocumentNumber = ourDoc.Number,
						DocumentDate = ConvertDate(ourDoc.Date),
						DocumentOrganization = ourDoc.EducationOrganization.Name,
						GPA = ourDoc.MiddleMark,
						EndYear = (int)(((DateTime)ourDoc.Date).Year)
					};

					var eduDoc = new EduDocument()
					{
						SchoolCertificateDocument = attestat
					};

					eduDocs.Add(eduDoc);
				}

				#endregion

				#region диплом СПО  

				if (claim.MiddleEducationDiplomaDocuments.Count > 0)
				{
					var ourDoc = claim.MiddleEducationDiplomaDocuments.First();
					var diploma = new Model.ExportData.MiddleEduDiplomaDocument()
					{
						UID = string.Format("RiiMiddleEduDiplomaDocument_{0}", ourDoc.Id),
						OriginalReceivedDate = ConvertDate(ourDoc.OriginalReceivedDate),
						DocumentSeries = ourDoc.Series,
						DocumentNumber = ourDoc.Number,
						DocumentDate = ConvertDate(ourDoc.Date),
						DocumentOrganization = ourDoc.EducationOrganization.Name,
						GPA = ourDoc.MiddleMark,
						EndYear = (int)(((DateTime)ourDoc.Date).Year)
					};

					var eduDoc = new EduDocument()
					{
						MiddleEduDiplomaDocument = diploma
					};

					eduDocs.Add(eduDoc);
				}

				#endregion

				#region Диплом ВО	

				if (claim.HighEducationDiplomaDocuments.Count > 0)
				{
					var ourDoc = claim.HighEducationDiplomaDocuments.First();
					var diploma = new Model.ExportData.HighEduDiplomaDocument()
					{
						UID = string.Format("RiiHighEduDiplomaDocument_{0}", ourDoc.Id),
						OriginalReceivedDate = ConvertDate(ourDoc.OriginalReceivedDate),
						DocumentSeries = ourDoc.Series,
						DocumentNumber = ourDoc.Number,
						DocumentDate = ConvertDate(ourDoc.Date),
						DocumentOrganization = ourDoc.EducationOrganization.Name,
						GPA = ourDoc.MiddleMark,
						EndYear = (int)(((DateTime)ourDoc.Date).Year)
					};

					var eduDoc = new EduDocument()
					{
						HighEduDiplomaDocument = diploma
					};

					eduDocs.Add(eduDoc);
				}

				#endregion

				#endregion

				docs.EduDocuments = eduDocs;

				app.ApplicationDocuments = docs;

				#endregion

				#region вступительные испытания

				app.EntranceTestResults = new List<Model.ExportData.EntranceTestResult>();

				#region егэ

				foreach (var egeDoc in claim.EgeDocuments)
				{
					foreach (var egeResult in egeDoc.EgeResults)
					{
						if (egeResult.Value > 0 && (egeResult.IsChecked ?? false))
						{
							var egeSubj = new EntranceTestSubject
							{
								SubjectID = int.Parse(egeResult.ExamSubject.ExportCode)
							};
							var resultDoc = new ResultDocument
							{
								EgeDocumentID = string.Format("RiiEgeDocument_{0}", egeDoc.Id)
							};
							var egeRes = new Model.ExportData.EntranceTestResult
							{
								UID = string.Format("EgeResult_{0}", egeResult.Id),
								ResultValue = egeResult.Value ?? 0,
								ResultSourceTypeID = 1,
								EntranceTestSubject = egeSubj,
								EntranceTestTypeID = 1,
								CompetitiveGroupUID = claim.GetCompetitiveGroupByPriority(1).ExportCode,
								ResultDocument = resultDoc
							};
							app.EntranceTestResults.Add(egeRes);
						}
					}
				}

				#endregion

				#region СЭ

				foreach (var examResult in claim.EntranceTestResults)
				{
					if (examResult.Value > 0 && (examResult.EntranceTest.EntranceExaminationsCheckProtocol != null))
					{
						var examSubj = new EntranceTestSubject
						{
							SubjectID = int.Parse(examResult.EntranceTest.ExamSubject.ExportCode)
						};
						var resultDoc = new ResultDocument
						{
							InstitutionDocument = new InstitutionDocument
							{
								DocumentNumber = examResult.EntranceTest.EntranceExaminationsCheckProtocol.Number,
								DocumentDate = ((DateTime)examResult.EntranceTest.EntranceExaminationsCheckProtocol.Date).ToString(DATE_FORMAT),
								DocumentTypeID = 1
							}
						};
						var exportResult = new Model.ExportData.EntranceTestResult
						{
							UID = string.Format("EgeResult_{0}", examResult.Id),
							ResultValue = examResult.Value ?? 0,
							ResultSourceTypeID = 2,
							EntranceTestSubject = examSubj,
							EntranceTestTypeID = 1,
							CompetitiveGroupUID = claim.GetCompetitiveGroupByPriority(1).ExportCode,
							ResultDocument = resultDoc
						};
						app.EntranceTestResults.Add(exportResult);
					}
				}
				if (app.EntranceTestResults.Count == 0)
				{
					app.EntranceTestResults = null;
				}

				#endregion

				#endregion

				#region льготы



				#endregion

				apps.Add(app);
			}

			#endregion


			package.Applications = apps;

			#endregion


			#endregion

			root.AuthData = auth;
			root.PackageData = package;

		}

		public void FillOrderPackage(Root root)
		{
			#region блок авторизации

			var auth = new AuthData
			{
				Login = "cpk@agtu.secna.ru",
				Pass = "PtVET8k",
				InstitutionID = "2536"
			};

			#endregion

			#region импортируемые данные

			var context = new AdmissionDatabase();

			var orders = new Orders
			{
				OrdersOfAdmission = new List<OrderOfAdmission>(),
				Applications = new List<OrderApplication>()
			};
			var collection = context.EnrollmentOrders.Where(o => o.Number == "1057").ToList();
			foreach (var order in collection)
			{
				var admissionOrder = new OrderOfAdmission
				{
					CampaignUID = "RII_2017_Campaign_1",
					EducationFormID = 10,
					FinanceSourceID = 15,
					EducationLevelID = 2,
					OrderOfAdmissionUID = string.Format("EnrollmentOrder_{0}", order.Id),
					OrderDate = ((DateTime)order.Date).ToString(DATE_FORMAT),
					OrderName = "О зачислении на 1 курс",
					OrderNumber = order.Number,
					Stage = 0
				};

				orders.OrdersOfAdmission.Add(admissionOrder);

				foreach (var protocol in order.EnrollmentProtocols)
				{
					foreach (var ec in protocol.EnrollmentClaims)
					{
						var app = new OrderApplication
						{
							ApplicationUID = string.Format("RiiApplication_{0}", ec.Claim.Id),
							OrderTypeID = 1,
							OrderUID = string.Format("EnrollmentOrder_{0}", order.Id),
							CompetitiveGroupUID = protocol.CompetitiveGroup.ExportCode
						};
						orders.Applications.Add(app);
					}
				}

			}

			#endregion

			root.Orders = orders;

			root.AuthData = auth;

		}


		public void FillExceptionEnrolledPackage(Root root)
		{
			#region блок авторизации

			var auth = new AuthData
			{
				Login = "cpk@agtu.secna.ru",
				Pass = "PtVET8k",
				InstitutionID = "2536"
			};

			#endregion

			#region импортируемые данные

			var package = new PackageData();


			#region заявления

			var apps = new List<Application>();


			#region заявление

			var context = new AdmissionDatabase();

			context.Claims.Load();

			var collection = (from ec in context.EnrollmentClaims
							  where ec.EnrollmentProtocol.EnrollmentOrder.Number == "862" &&
							  ec.Claim != null
							  select ec.Claim).ToList();

			foreach (var claim in collection)
			{
				var app = new Application()
				{
					UID = string.Format("RiiApplication_{0}", claim.Id),
					ApplicationNumber = claim.Number,
					RegistrationDate = ConvertDateTime(claim.RegistrationDate),
					NeedHostel = claim.IsHostelNeed ?? false,
					StatusID = claim.ClaimStatus.ExportCode
				};

				#region абитуриент

				var entrant = claim.Entrants.First();

				var entrantFis = new Model.ExportData.Entrant()
				{
					UID = string.Format("RiiEntrant_{0}", entrant.Id),
					LastName = entrant.LastName,
					FirstName = entrant.FirstName,
					MiddleName = entrant.Patronymic,
					GenderID = entrant.Gender.Id,
					CustomInformation = "---"
				};

				var email = new EmailOrMailAddress()
				{
					Email = entrant.Email ?? "---"
				};

				entrantFis.EmailOrMailAddress = email;
				app.Entrant = entrantFis;

				#endregion

				#region условия приема

				var finSourceAndEduForms = new List<FinSourceEduForm>();

				foreach (var condition in claim.ClaimConditions)
				{
					var finSourceAndEduForm = new FinSourceEduForm()
					{
						CompetitiveGroupUID = condition.CompetitiveGroup.ExportCode
					};

					// Дата согласия на зачисление
					if (claim.EnrollmentClaims.Count > 0)
					{
						var enrollmentCompGroup = claim.EnrollmentClaims.First().EnrollmentProtocol.CompetitiveGroup;
						if (condition.CompetitiveGroup.Id == enrollmentCompGroup.Id)
						{
							if (claim.SchoolCertificateDocuments.Count > 0)
							{
								finSourceAndEduForm.IsAgreedDate =
									((DateTime)claim.SchoolCertificateDocuments.First().OriginalReceivedDate).ToString(DATE_TIME_FORMAT);
							}
							if (claim.MiddleEducationDiplomaDocuments.Count > 0)
							{
								finSourceAndEduForm.IsAgreedDate =
									((DateTime)claim.MiddleEducationDiplomaDocuments.First().OriginalReceivedDate).ToString(DATE_TIME_FORMAT);
							}
							if (claim.HighEducationDiplomaDocuments.Count > 0)
							{
								finSourceAndEduForm.IsAgreedDate =
									((DateTime)claim.HighEducationDiplomaDocuments.First().OriginalReceivedDate).ToString(DATE_TIME_FORMAT);
							}
						}
					}

					finSourceAndEduForms.Add(finSourceAndEduForm);
				}

				app.FinSourceAndEduForms = finSourceAndEduForms;

				#endregion

				#region документы, приложенные к заявлению

				var docs = new ApplicationDocuments();

				#region свидетельства ЕГЭ

				if (claim.EgeDocuments.Count > 0)
				{
					var egeDocs = new List<Model.ExportData.EgeDocument>();

					foreach (var egeDoc in claim.EgeDocuments)
					{
						var doc = new Model.ExportData.EgeDocument()
						{
							UID = string.Format("RiiEgeDocument_{0}", egeDoc.Id),
							DocumentYear = (int)egeDoc.Year
						};

						#region предметы

						var results = new List<SubjectData>();

						foreach (var res in egeDoc.EgeResults)
						{
							var subjectData = new SubjectData()
							{
								SubjectID = res.ExamSubject.ExportCode,
								Value = (int)res.Value
							};

							results.Add(subjectData);
						}

						doc.Subjects = results;

						#endregion

						egeDocs.Add(doc);
					}

					docs.EgeDocuments = egeDocs;

				}
				#endregion

				#region удостоверение личности

				var idDoc = new Model.ExportData.IdentityDocument()
				{
					UID = string.Format("RiiIdentityDocument_{0}", claim.IdentityDocuments.First().Id),
					DocumentSeries = claim.IdentityDocuments.First().Series,
					DocumentNumber = claim.IdentityDocuments.First().Number,
					SubdivisionCode = claim.IdentityDocuments.First().SubdivisionCode,
					DocumentDate = ConvertDate(claim.IdentityDocuments.First().Date),
					DocumentOrganization = claim.IdentityDocuments.First().Organization,
					IdentityDocumentTypeID = claim.IdentityDocuments.First().IdentityDocumentType.ExportCode,
					NationalityTypeID = claim.IdentityDocuments.First().Citizenship.ExportCode,
					LastName = entrantFis.LastName,
					FirstName = entrantFis.FirstName,
					MiddleName = entrantFis.MiddleName,
					GenderID = entrantFis.GenderID,
					BirthDate = ConvertDate(claim.Person.BirthDate),
					BirthPlace = claim.Person.BirthPlace
				};

				docs.IdentityDocument = idDoc;

				#endregion

				#region документы об образовании

				var eduDocs = new List<EduDocument>();

				#region аттестат

				if (claim.SchoolCertificateDocuments.Count > 0)
				{
					var ourDoc = claim.SchoolCertificateDocuments.First();
					var attestat = new Model.ExportData.SchoolCertificateDocument()
					{
						UID = string.Format("RiiSchoolCertificateDocument_{0}", ourDoc.Id),
						OriginalReceivedDate = ConvertDate(ourDoc.OriginalReceivedDate),
						DocumentSeries = ourDoc.Series,
						DocumentNumber = ourDoc.Number,
						DocumentDate = ConvertDate(ourDoc.Date),
						DocumentOrganization = ourDoc.EducationOrganization.Name,
						GPA = ourDoc.MiddleMark,
						EndYear = (int)(((DateTime)ourDoc.Date).Year)
					};

					var eduDoc = new EduDocument()
					{
						SchoolCertificateDocument = attestat
					};

					eduDocs.Add(eduDoc);
				}

				#endregion

				#region диплом СПО  

				if (claim.MiddleEducationDiplomaDocuments.Count > 0)
				{
					var ourDoc = claim.MiddleEducationDiplomaDocuments.First();
					var diploma = new Model.ExportData.MiddleEduDiplomaDocument()
					{
						UID = string.Format("RiiMiddleEduDiplomaDocument_{0}", ourDoc.Id),
						OriginalReceivedDate = ConvertDate(ourDoc.OriginalReceivedDate),
						DocumentSeries = ourDoc.Series,
						DocumentNumber = ourDoc.Number,
						DocumentDate = ConvertDate(ourDoc.Date),
						DocumentOrganization = ourDoc.EducationOrganization.Name,
						GPA = ourDoc.MiddleMark,
						EndYear = (int)(((DateTime)ourDoc.Date).Year)
					};

					var eduDoc = new EduDocument()
					{
						MiddleEduDiplomaDocument = diploma
					};

					eduDocs.Add(eduDoc);
				}

				#endregion

				#region Диплом ВО	

				if (claim.HighEducationDiplomaDocuments.Count > 0)
				{
					var ourDoc = claim.HighEducationDiplomaDocuments.First();
					var diploma = new Model.ExportData.HighEduDiplomaDocument()
					{
						UID = string.Format("RiiHighEduDiplomaDocument_{0}", ourDoc.Id),
						OriginalReceivedDate = ConvertDate(ourDoc.OriginalReceivedDate),
						DocumentSeries = ourDoc.Series,
						DocumentNumber = ourDoc.Number,
						DocumentDate = ConvertDate(ourDoc.Date),
						DocumentOrganization = ourDoc.EducationOrganization.Name,
						GPA = ourDoc.MiddleMark,
						EndYear = (int)(((DateTime)ourDoc.Date).Year)
					};

					var eduDoc = new EduDocument()
					{
						HighEduDiplomaDocument = diploma
					};

					eduDocs.Add(eduDoc);
				}

				#endregion

				#endregion

				docs.EduDocuments = eduDocs;

				app.ApplicationDocuments = docs;

				#endregion

				#region вступительные испытания

				app.EntranceTestResults = new List<Model.ExportData.EntranceTestResult>();

				#region егэ

				foreach (var egeDoc in claim.EgeDocuments)
				{
					foreach (var egeResult in egeDoc.EgeResults)
					{
						if (egeResult.Value > 0 && (egeResult.IsChecked ?? false))
						{
							var egeSubj = new EntranceTestSubject
							{
								SubjectID = int.Parse(egeResult.ExamSubject.ExportCode)
							};
							var resultDoc = new ResultDocument
							{
								EgeDocumentID = string.Format("RiiEgeDocument_{0}", egeDoc.Id)
							};
							var egeRes = new Model.ExportData.EntranceTestResult
							{
								UID = string.Format("EgeResult_{0}", egeResult.Id),
								ResultValue = egeResult.Value ?? 0,
								ResultSourceTypeID = 1,
								EntranceTestSubject = egeSubj,
								EntranceTestTypeID = 1,
								CompetitiveGroupUID = claim.GetCompetitiveGroupByPriority(1).ExportCode,
								ResultDocument = resultDoc
							};
							app.EntranceTestResults.Add(egeRes);
						}
					}
				}

				#endregion

				#region СЭ

				foreach (var examResult in claim.EntranceTestResults)
				{
					if (examResult.Value > 0 && (examResult.EntranceTest.EntranceExaminationsCheckProtocol != null))
					{
						var examSubj = new EntranceTestSubject
						{
							SubjectID = int.Parse(examResult.EntranceTest.ExamSubject.ExportCode)
						};
						var resultDoc = new ResultDocument
						{
							InstitutionDocument = new InstitutionDocument
							{
								DocumentNumber = examResult.EntranceTest.EntranceExaminationsCheckProtocol.Number,
								DocumentDate = ((DateTime)examResult.EntranceTest.EntranceExaminationsCheckProtocol.Date).ToString(DATE_FORMAT),
								DocumentTypeID = 1
							}
						};
						var exportResult = new Model.ExportData.EntranceTestResult
						{
							UID = string.Format("EgeResult_{0}", examResult.Id),
							ResultValue = examResult.Value ?? 0,
							ResultSourceTypeID = 2,
							EntranceTestSubject = examSubj,
							EntranceTestTypeID = 1,
							CompetitiveGroupUID = claim.GetCompetitiveGroupByPriority(1).ExportCode,
							ResultDocument = resultDoc
						};
						app.EntranceTestResults.Add(exportResult);
					}
				}
				if (app.EntranceTestResults.Count == 0)
				{
					app.EntranceTestResults = null;
				}

				#endregion

				#endregion

				#region льготы



				#endregion

				apps.Add(app);
			}

			#endregion


			package.Applications = apps;

			#endregion


			#endregion

			root.AuthData = auth;
			root.PackageData = package;

		}

		public void FillExceptionOrderPackage(Root root)
		{
			#region блок авторизации

			var auth = new AuthData
			{
				Login = "cpk@agtu.secna.ru",
				Pass = "PtVET8k",
				InstitutionID = "2536"
			};

			#endregion

			#region импортируемые данные

			var context = new AdmissionDatabase();

			var orders = new Orders
			{
				OrdersOfException = new List<OrderOfException>(),
				Applications = new List<OrderApplication>()
			};
			var collection = context.EnrollmentExceptionOrders.ToList();
			foreach (var order in collection)
			{
				var admissionOrder = new OrderOfException
				{
					CampaignUID = "RII_2017_Campaign_1",
					EducationFormID = int.Parse(order.EnrollmentOrder.EducationForm.ExportCode),
					FinanceSourceID = int.Parse(order.EnrollmentOrder.FinanceSource.ExportCode),
					EducationLevelID = 2,
					OrderOfExceptionUID = string.Format("EnrollmentExceptionOrder_{0}", order.Id),
					OrderDate = ((DateTime)order.Date).ToString(DATE_FORMAT),
					OrderName = "Об иссключении из приказа о зачислении на 1 курс",
					OrderNumber = order.Number,
					Stage = 1
				};

				orders.OrdersOfException.Add(admissionOrder);
				
				foreach (var ec in order.EnrollmentClaims)
				{
					var app = new OrderApplication
					{
						ApplicationUID = string.Format("RiiApplication_{0}", ec.Claim.Id),
						OrderTypeID = 2,
						OrderUID = string.Format("EnrollmentExceptionOrder_{0}", order.Id),
						CompetitiveGroupUID = ec.EnrollmentProtocol.CompetitiveGroup.ExportCode,
						OrderIdLevelBudget = "1",
						IsDisagreedDate = "2017-08-07T00:00:00"
					};
					orders.Applications.Add(app);
				}

			}

			#endregion

			root.Orders = orders;

			root.AuthData = auth;

		}


		string ConvertDate(DateTime? date)
		{
			string str = null;

			if (date.HasValue)
			{
				str = ((DateTime)date).ToString(DATE_FORMAT);
			}

			return str;
		}

		string ConvertDateTime(DateTime? date)
		{
			string str = null;

			if (date.HasValue)
			{
				str = ((DateTime)date).ToString(DATE_TIME_FORMAT);
			}

			return str;
		}

	}
}
