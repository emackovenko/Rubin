using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceLibrary.Documents;
using Model.Admission;
using CommonMethods.Documents;

namespace Admission.ViewModel.Documents
{
	internal class DocumentClaimMiddleEducation : WordDocument
	{						 

		public DocumentClaimMiddleEducation(Claim claim) 
			: base("EntrantClaimMiddleEducation")
		{


			#region EduDocs

			int? graduationYear = DateTime.Now.Year;
			string eduDocType = null;
			string eduDocInfo = null;					 

			if (claim.SchoolCertificateDocuments.Count > 0)
			{
				graduationYear = claim.SchoolCertificateDocuments.First().GraduationYear;
				eduDocType = "Аттестат о СОО";
				eduDocInfo = string.Format("{0} {1}",
						claim.SchoolCertificateDocuments.First().Series,
						claim.SchoolCertificateDocuments.First().Number);
			}
			else
			{
				if (claim.MiddleEducationDiplomaDocuments.Count > 0)
				{
					graduationYear = claim.MiddleEducationDiplomaDocuments.First().GraduationYear;
					eduDocType = "Диплом о СПО";
					eduDocInfo = string.Format("{0} {1}",
							claim.MiddleEducationDiplomaDocuments.First().Series,
							claim.MiddleEducationDiplomaDocuments.First().Number);
				}
				else
				{
					if (claim.HighEducationDiplomaDocuments.Count > 0)
					{
						graduationYear = claim.HighEducationDiplomaDocuments.First().GraduationYear;
						eduDocType = "Диплом о ВО";
						eduDocInfo = string.Format("{0} {1}",
								claim.HighEducationDiplomaDocuments.First().Series,
								claim.HighEducationDiplomaDocuments.First().Number);	 
					}
				}
			}

			#endregion


			#region fieldsFilling

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
					Value = ((DateTime)claim.Person.BirthDate).ToString("dd.MM.yyyy")
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
					Name = "GraduationYear",
					Value = graduationYear.ToString()
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
					Name = "IsNeedHostel",
					Value = hostelNeed
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "ReturningType",
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

			#endregion

			FillByBookmarks();		   
		}	   
	}
}
