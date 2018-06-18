using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceLibrary.Documents;
using Model.Admission;
using CommonMethods.Documents;
using CommonMethods.TypeExtensions.exDateTime;

namespace Admission.ViewModel.Documents
{
	internal class DocumentClaim : WordDocument
	{						 

		public DocumentClaim(Claim claim) 
			: base("EntrantClaim")
		{	
			#region EduDocs

			int? graduationYear = DateTime.Now.Year;
			string eduDocType = null;
			string eduDocInfo = null;
			string bachelour = "Подтверждаю, что не имею";
            string grad = string.Empty;
			if (claim.SchoolCertificateDocuments.Count > 0)
			{
                grad = "среднее (полное) образование";
				graduationYear = claim.SchoolCertificateDocuments.First().GraduationYear;
				eduDocType = "Аттестат о СОО";
				eduDocInfo = string.Format("{0} {1} дата выдачи: {2}",
						claim.SchoolCertificateDocuments.First().Series,
						claim.SchoolCertificateDocuments.First().Number,
                        claim.SchoolCertificateDocuments.First().Date.Format());
			}
			else
			{
				if (claim.MiddleEducationDiplomaDocuments.Count > 0)
                {
                    grad = "среднее профессиональное образование";
                    graduationYear = claim.MiddleEducationDiplomaDocuments.First().GraduationYear;
					eduDocType = "Диплом о СПО";
					eduDocInfo = string.Format("{0} {1} дата выдачи: {2}",
							claim.MiddleEducationDiplomaDocuments.First().Series,
							claim.MiddleEducationDiplomaDocuments.First().Number,
                            claim.MiddleEducationDiplomaDocuments.First().Date.Format());
				}
				else
				{
					if (claim.HighEducationDiplomaDocuments.Count > 0)
                    {
                        grad = "высшее образование";
                        graduationYear = claim.HighEducationDiplomaDocuments.First().GraduationYear;
						eduDocType = "Диплом о ВО";
						eduDocInfo = string.Format("{0} {1} дата выдачи: {2}",
								claim.HighEducationDiplomaDocuments.First().Series,
								claim.HighEducationDiplomaDocuments.First().Number,
                                claim.HighEducationDiplomaDocuments.First().Date.Format());
						bachelour = "Сообщаю, что имею";
					}
				}
			}

            #endregion

            #region conditions	

            string quotaDirection = string.Empty;
            string quotaDocs = string.Empty;

            // Получаем условия приёма (конкурсные группы) на льготные места
            var quotaConditions = (from condition in claim.ClaimConditions
                                   where condition.CompetitiveGroup.FinanceSource.Id == 3 &&
                                    condition.Priority == 1
                                   orderby condition.Priority
                                   select condition.CompetitiveGroup);

            // Если таковые есть, то херачим в квоту данные об них
            if (quotaConditions.Count() > 0)
            {
                // Указываем направления подготовки, разделяя их точкой с запятой
                foreach (var competitiveGroup in quotaConditions)
                {
                    quotaDirection += string.Format("{0}, программа бакалавриата \"{1}\", {2} форма обучения; ", competitiveGroup.Direction.Name, 
                        competitiveGroup.Direction.DirectionProfiles.First().Name, 
                        competitiveGroup.EducationForm.Name);
                }

                // Указываем сведения о документах, подтверждающих такое право 
                if (claim.OrphanDocuments.Count > 0)
                {
                    var doc = claim.OrphanDocuments.First();
                    quotaDocs += string.Format("{0} - №{1} {2}, выдано {3}, {4}",
                        doc.OrphanDocumentType.Name, doc.Series, doc.Number,
                        doc.Organization, ((DateTime)doc.Date).ToString("dd.MM.yyyy г."));
                }                
            }
            											

			string cond1 = null;
			try
			{
				var conditions = (from cond in claim.ClaimConditions
                                  where cond.CompetitiveGroup.FinanceSource.Id != 3 
                                  orderby cond.Priority                         
							 select cond);
                foreach (var condition in conditions)
                {
                    cond1 += string.Format("{0}, программа бакалавриата \"{1}\", {2} форма обучения; ", condition.CompetitiveGroup.Direction.Name,
                        condition.CompetitiveGroup.Direction.DirectionProfiles.First().Name,
                        condition.CompetitiveGroup.EducationForm.Name);
                }
				
			}
			catch (Exception)
			{
				cond1 = null;
			}

			#endregion

			#region ege				

			string egeSubj1 = null;
			string egeMark1 = null;

			string egeSubj2 = null;
			string egeMark2 = null;

			string egeSubj3 = null;
			string egeMark3 = null;
			try
			{
				List<string> egeSubjects = new List<string>();
				List<string> egeMarks = new List<string>();
				foreach (var doc in claim.EgeDocuments)
				{
					foreach (var egeResult in doc.EgeResults)
					{
						egeSubjects.Add(egeResult.ExamSubject.Name);
						egeMarks.Add(egeResult.Value.ToString());
					}
				}

				if (egeSubjects.Count > 0)
				{
					egeSubj1 = egeSubjects[0];
					egeMark1 = egeMarks[0];
					if (egeSubjects.Count > 1)
					{
						egeSubj2 = egeSubjects[1];
						egeMark2 = egeMarks[1];
						if (egeSubjects.Count > 2)
						{
							egeSubj3 = egeSubjects[2];
							egeMark3 = egeMarks[2];
						}
					}
				}
			}
			catch (Exception)
			{				
			}

			#endregion

			#region indAchs
											 
			string individualAchievements = null;
			try
			{
				foreach (var item in claim.EntranceIndividualAchievements)
				{
					individualAchievements += item.CampaignIndividualAchievement.Name;
					individualAchievements = individualAchievements.Insert(individualAchievements.Length, "; ");
				}
				individualAchievements = individualAchievements.Remove(individualAchievements.Length - 2);
			}
			catch (Exception)
			{	  
			}

			#endregion

			#region entrancetests

			string entranceTestsReason = null;
			string entranceTestSubjects = null;

			try
			{
				if (claim.EntranceTestResults.Count > 0)
				{
					if (claim.IdentityDocuments.FirstOrDefault().CitizenshipId != 1)
					{
						entranceTestsReason = "иностранным гражданам";
					}
					else
					{
						if (claim.MiddleEducationDiplomaDocuments.Count > 0 || claim.HighEducationDiplomaDocuments.Count > 0)
						{
							entranceTestsReason = "лицам, получившим ранее профессиональное образование";
						}
						else
						{
							entranceTestsReason = "лицам, прошедшим ГИА не в форме ЕГЭ";
						}
					}

					foreach (var item in claim.EntranceTestResults)
					{
						entranceTestSubjects += item.EntranceTest.ExamSubject.Name;
						entranceTestSubjects = entranceTestSubjects.Insert(entranceTestSubjects.Length, ", ");
					}
					entranceTestSubjects = entranceTestSubjects.Remove(entranceTestSubjects.Length - 2);

				}
			}
			catch (Exception)
			{	   
			}

            #endregion

            #region fieldsFilling

            BookmarkFields.Add(
                new DocumentField
                {
                    Name = "QuotaDirection",
                    Value = quotaDirection
                });

            BookmarkFields.Add(
                new DocumentField
                {
                    Name = "EducationLevel",
                    Value = grad
                });

            BookmarkFields.Add(
                new DocumentField
                {
                    Name = "QuotaDocs",
                    Value = quotaDocs
                });

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "Number",
					Value = claim.Number
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "EntrantName",
					Value = string.Format("{0} {1} {2}", claim.Entrants.First().LastName, 
						claim.Entrants.First().FirstName, 
						claim.Entrants.First().Patronymic)
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "BirthDate",
					Value = ((DateTime)claim.IdentityDocuments.First().BirthDate).ToString("dd.MM.yyyy")
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "Citizenship",
					Value = claim.IdentityDocuments.First().Citizenship.Name
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "IdentityDocumentType",
					Value = claim.IdentityDocuments.FirstOrDefault().IdentityDocumentType.NameInDocument
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "IdentityDocumentSeries",
					Value = claim.IdentityDocuments.FirstOrDefault().Series
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "IdentityDocumentNumber",
					Value = claim.IdentityDocuments.FirstOrDefault().Number
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "IdentityDocumentIssued",
					Value = string.Format("{0}, {1}", claim.IdentityDocuments.FirstOrDefault().Organization,
						((DateTime)claim.IdentityDocuments.FirstOrDefault().Date).ToString("dd.MM.yyyy"))
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "MailAddress",
					Value = claim.Entrants.First().Address.MailString
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "Email",
					Value = claim.Entrants.First().Email
				});
				

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "EducationDocumentType",
					Value = eduDocType
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "EdcationDocumentInfo",
					Value = eduDocInfo
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "Conditions",
					Value = cond1
				});
            

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "EgeSubjectFirst",
					Value = egeSubj1
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "EgeMarkFirst",
					Value = egeMark1
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "EgeSubjectSecond",
					Value = egeSubj2
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "EgeMarkSecond",
					Value = egeMark2
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "EgeSubjectThird",
					Value = egeSubj3
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "EgeMarkThird",
					Value = egeMark3
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "EntranceTestReason",
					Value = entranceTestsReason
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "EntranceTestSubjects",
					Value = entranceTestSubjects
				});

			string hostelNeed = "не нуждаюсь";
			if (claim.IsHostelNeed ?? false)
			{
				hostelNeed = "нуждаюсь";
			}

			string personalReturning = "по почте";
			if (claim.PersonalReturning ?? false)
			{
				personalReturning = "лично";
			}

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "HostelNeed",
					Value = hostelNeed
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "PersonalReturning",
					Value = personalReturning
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "CurrentUser",
					Value = string.Format("{0} {1}.{2}.", Session.CurrentUser.LastName,
						Session.CurrentUser.FirstName[0], 
						Session.CurrentUser.Patronymic[0])
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "RegistrationDate",
					Value = ((DateTime)claim.RegistrationDate).ToString("dd MMMM yyyy г.")
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "AdvantageRight",
					Value = null
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "IndividualAchievements",
					Value = individualAchievements
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "IsBachelour",
					Value = bachelour
				});

			#endregion

			FillByBookmarks();		   
		}	   
	}
}
