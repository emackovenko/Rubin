using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;
using CommonMethods.Documents;

namespace Admission.ViewModel.Documents
{
	internal class DocumentVoucher: WordDocument
	{
		public DocumentVoucher(Claim claim)
			: base("Voucher")
		{

			#region EduDocs
												   
			string eduDocType = null;
			string eduDocInfo = null;
			string eduDocDate = null;
			string eduDocIsOriginal = null;

			if (claim.SchoolCertificateDocuments.Count > 0)
			{
				eduDocIsOriginal = (claim.SchoolCertificateDocuments.First().OriginalReceivedDate != null) ? 
					"оригинал" : "копия";		 
				eduDocType = "Аттестат о СОО";
				eduDocInfo = string.Format("{0} {1}, от {2}",
						claim.SchoolCertificateDocuments.First().Series,
						claim.SchoolCertificateDocuments.First().Number,
						((DateTime)claim.SchoolCertificateDocuments.First().Date).ToString("dd.MM.yyyy г."));
			}
			else
			{
				if (claim.MiddleEducationDiplomaDocuments.Count > 0)
				{
					eduDocIsOriginal = (claim.MiddleEducationDiplomaDocuments.First().OriginalReceivedDate != null) ? 
						"оригинал" : "копия";
					eduDocType = "Диплом о СПО";
					eduDocInfo = string.Format("{0} {1}, от {2}",
							claim.MiddleEducationDiplomaDocuments.First().Series,
							claim.MiddleEducationDiplomaDocuments.First().Number,
						((DateTime)claim.MiddleEducationDiplomaDocuments.First().Date).ToString("dd.MM.yyyy г."));
				}
				else
				{
					if (claim.HighEducationDiplomaDocuments.Count > 0)
					{				
						eduDocIsOriginal = (claim.HighEducationDiplomaDocuments.First().OriginalReceivedDate != null) ? 
							"оригинал" : "копия";																 
						eduDocType = "Диплом о ВО";
						eduDocInfo = string.Format("{0} {1}, от {2}",
								claim.HighEducationDiplomaDocuments.First().Series,
								claim.HighEducationDiplomaDocuments.First().Number,
						((DateTime)claim.HighEducationDiplomaDocuments.First().Date).ToString("dd.MM.yyyy г."));  
					}
				}
			}
			string eduDoc = string.Format("{0} {1} {2} - {3}", eduDocType, eduDocInfo, eduDocDate, eduDocIsOriginal);

			#endregion

			#region entrancetests

			string entranceTestSubject1 = null;
			string entranceTestConsult1 = null;
			string entrancetestDate1 = null;
			string entranceTestSubject2 = null;
			string entranceTestConsult2 = null;
			string entrancetestDate2 = null;
			string entranceTestSubject3 = null;
			string entranceTestConsult3 = null;
			string entrancetestDate3 = null;


			try
			{
				int i = 0;
				if (claim.EntranceTestResults.Count > 0)
				{
					foreach (var test in claim.EntranceTestResults)
					{
						i++;
						if (i == 1)
						{	
							entranceTestSubject1 = test.EntranceTest.ExamSubject.Name;
							entranceTestConsult1 = string.Format("{0} {1}",
								((DateTime)test.EntranceTest.ConsultationDate).ToString("dd.MM.yyyy"),
								((TimeSpan)test.EntranceTest.ConsultationTime).ToString());
							entrancetestDate1 = string.Format("{0} {1}",
								((DateTime)test.EntranceTest.ExaminationDate).ToString("dd.MM.yyyy"),
								((TimeSpan)test.EntranceTest.ExaminationTime).ToString()); 	   
						}
						if (i == 2)
						{
							entranceTestSubject2= test.EntranceTest.ExamSubject.Name;
							entranceTestConsult2 = string.Format("{0} {1}",
								((DateTime)test.EntranceTest.ConsultationDate).ToString("dd.MM.yyyy"),
								((TimeSpan)test.EntranceTest.ConsultationTime).ToString());
							entrancetestDate2 = string.Format("{0} {1}",
								((DateTime)test.EntranceTest.ExaminationDate).ToString("dd.MM.yyyy"),
								((TimeSpan)test.EntranceTest.ExaminationTime).ToString()); 
						}
						if (i == 3)
						{
							entranceTestSubject3 = test.EntranceTest.ExamSubject.Name;
							entranceTestConsult3 = string.Format("{0} {1}",
								((DateTime)test.EntranceTest.ConsultationDate).ToString("dd.MM.yyyy"),
								((TimeSpan)test.EntranceTest.ConsultationTime).ToString());
							entrancetestDate3 = string.Format("{0} {1}",
								((DateTime)test.EntranceTest.ExaminationDate).ToString("dd.MM.yyyy"),
								((TimeSpan)test.EntranceTest.ExaminationTime).ToString());
						}
					}
				}
			}
			catch (Exception)
			{
			}

			#endregion


			#region Issued Documents

			string issuedDocs = null;
			string lostDocs = null;

			issuedDocs = "Заявление; ";
			issuedDocs += "Документ об образовании: ";
			issuedDocs += eduDoc;
			issuedDocs += "; ";

			//на очке
			if (claim.ClaimConditions.First().CompetitiveGroup.EducationFormId == 1)
			{
				if (claim.OtherRequiredDocuments.First().Fluorography ?? false)
				{
					issuedDocs += "флюорография; ";
				}
				else
				{
					lostDocs += "флюорография; ";
				}

				if (claim.OtherRequiredDocuments.First().MedicinePermission ?? false)
				{
					issuedDocs += "медицинская справка №086-у; ";
				}
				else
				{
					lostDocs += "медицинская справка №086-у; ";
				}

				if (claim.OtherRequiredDocuments.First().Photos ?? false)
				{
					issuedDocs += "4 фотографии 3х4; ";
				}
				else
				{
					lostDocs += "4 фотографии 3х4; ";
				}

				if (claim.OtherRequiredDocuments.First().Certificate ?? false)
				{
					issuedDocs += "сертификат о прививках; ";
				}
				else
				{
					lostDocs += "сертификат о прививках; ";
				}  
			}
			else
			{
				if (claim.OtherRequiredDocuments.First().Fluorography ?? false)
				{
					issuedDocs += "флюорография; ";
				}
				else
				{
					lostDocs += "флюорография; ";
				}

				if (claim.OtherRequiredDocuments.First().MedicinePermission ?? false)
				{
					issuedDocs += "медицинская справка №086-у; ";
				}	

				if (claim.OtherRequiredDocuments.First().Photos ?? false)
				{
					issuedDocs += "6 фотографий 3х4; ";
				}
				else
				{
					lostDocs += "6 фотографий 3х4; ";
				}

				if (claim.OtherRequiredDocuments.First().Certificate ?? false)
				{
					issuedDocs += "сертификат о прививках; ";
				}
			}


			#endregion


			BookmarkFields.Add(
				new DocumentField
				{
					Name = "ClaimNumber",
					Value = claim.Number
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "Entrant",
					Value = string.Format("{0} {1} {2}", claim.Entrants.First().LastName,
						claim.Entrants.First().FirstName,
						claim.Entrants.First().Patronymic)
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "IssuedDocuments",
					Value = issuedDocs
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "LostDocuments",
					Value = lostDocs
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "FirstExamName",
					Value = entranceTestSubject1
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "FirstExamConsult",
					Value = entranceTestConsult1
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "FirstExamDate",
					Value = entrancetestDate1
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "SecondExamName",
					Value = entranceTestSubject2
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "SecondExamConsult",
					Value = entranceTestConsult2
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "SecondExamDate",
					Value = entrancetestDate2
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "ThirdExamName",
					Value = entranceTestSubject3
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "ThirdExamConsult",
					Value = entranceTestConsult3
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "ThirdExamDate",
					Value = entrancetestDate3
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "Date",
					Value = DateTime.Now.ToString("dd MMMM yyyy г.")
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "IdentityDocument",
					Value = string.Format("{0} {1} {2}, выдан {3} {4}",
					 claim.IdentityDocuments.FirstOrDefault().IdentityDocumentType.Name,
					 claim.IdentityDocuments.FirstOrDefault().Series,
					 claim.IdentityDocuments.FirstOrDefault().Number,
					 claim.IdentityDocuments.FirstOrDefault().Organization,
					 ((DateTime)claim.IdentityDocuments.FirstOrDefault().Date).ToString("dd.MM.yyyy"))
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "Entrant2",
					Value = string.Format("{0} {1} {2}", claim.Entrants.First().LastName,
						claim.Entrants.First().FirstName,
						claim.Entrants.First().Patronymic)
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "RegistrationDate",
					Value = ((DateTime)claim.RegistrationDate).ToString("dd.MM.yyyy")
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "IsOriginal",
					Value = claim.IsOriginal ? "оригинал" : "копия"
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "EduDocument2",
					Value = eduDoc
				});

			string maxDate = null;

			var compgroup = claim.ClaimConditions.First().CompetitiveGroup;

			maxDate = (((DateTime)compgroup.AdmissionFirstStageEndDate).ToString("dd.MM.yyyy")).ToString();

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "MaxDate",
					Value = maxDate
				});

			FillByBookmarks();
		}
	}
}
