using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;
using LingvoNET;
using CommonMethods.Documents;

namespace Admission.ViewModel.Documents
{
	internal class DocumentEnrollmentAgreementClaim : WordDocument
	{
		public DocumentEnrollmentAgreementClaim(Claim claim) 
			: base("EnrollmentAgreementClaim")
		{

			string eduForm = null;
			string direction = null;
			string finSource = null;

			try
			{
				var cond = (from condition in claim.ClaimConditions
							where condition.Priority == 1
							select condition).ToList().First();
				eduForm = cond.CompetitiveGroup.EducationForm.Name;
				direction = string.Format("{0} {1}, {2}", cond.CompetitiveGroup.Direction.Code,
					cond.CompetitiveGroup.Direction.Name,
					cond.CompetitiveGroup.EducationProgramType.Name);
				finSource = cond.CompetitiveGroup.FinanceSource.NameInDocument;

				//склоняем прилагательное
				var adj = Adjectives.FindOne(eduForm);
				if (adj != null)
				{
					var str = adj[Case.Dative, LingvoNET.Gender.F];
					if (str.Length > 0)
					{
						eduForm = str;
					}
				}
				else
				{
					eduForm = eduForm.Remove(eduForm.Length - 2);
					eduForm = eduForm.Insert(eduForm.Length, "ой");
				}
			}
			catch (Exception)
			{

			}

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
					Name = "Citizenship",
					Value = claim.IdentityDocuments.First().Citizenship.Name
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "Birthdate",
					Value = ((DateTime)claim.Person.BirthDate).ToString("dd.MM.yyyy")
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "IdentityDocumentType",
					Value = claim.IdentityDocuments.FirstOrDefault().IdentityDocumentType.Name
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "IdentityDocumentSeries",
					Value = string.Format("{0}   {1}", claim.IdentityDocuments.FirstOrDefault().Series,
						claim.IdentityDocuments.FirstOrDefault().Number)
				});


			BookmarkFields.Add(
				new DocumentField
				{
					Name = "IdentityDocumentInfo",
					Value = string.Format("{0}, {1}", claim.IdentityDocuments.FirstOrDefault().Organization,
						((DateTime)claim.IdentityDocuments.FirstOrDefault().Date).ToString("dd.MM.yyyy"))
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "EducationForm",
					Value = eduForm
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "Direction",
					Value = direction
				});

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "FinanceSource",
					Value = string.Format("на {0}", finSource)
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
					Value = DateTime.Now.ToString("dd MMMM yyyy г.")
				});

			FillByBookmarks();
		}
	}
}
