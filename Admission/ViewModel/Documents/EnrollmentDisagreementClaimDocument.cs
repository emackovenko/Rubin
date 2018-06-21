using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GemBox.Document;
using Model.Admission;
using CommonMethods.Documents;
using CommonMethods.TypeExtensions.exGemBox;
using CommonMethods.TypeExtensions.exNumeric;
using ResourceLibrary.Documents;

namespace Admission.ViewModel.Documents
{
	public class EnrollmentDisagreementClaimDocument : OpenXmlDocument
	{

		public EnrollmentDisagreementClaimDocument(Claim claim)
		{
			_claim = claim;
			DocumentType = OpenXmlDocumentType.Document;
		}

		Claim _claim;

		public override void CreatePackage(string fileName)
		{
			var document = DocumentModel.Load(DocumentTemplate.ExtractDoc("EnrollmentDisagreementClaim"));

			// Подготовка стилей
			var underlinedText = new CharacterFormat
			{
				UnderlineColor = Color.Black,
				UnderlineStyle = UnderlineType.Single
			};

			// Вставляем текст на закладки

			string direction = string.Format("{0} {1} ({2}), {3} форма обучения",
				 _claim.EnrollmentClaims.Where(ec => ec.EnrollmentExceptionOrder != null).First().EnrollmentProtocol.CompetitiveGroup.Direction.Code,
				  _claim.EnrollmentClaims.Where(ec => ec.EnrollmentExceptionOrder != null).First().EnrollmentProtocol.CompetitiveGroup.Direction.Name,
				   _claim.EnrollmentClaims.Where(ec => ec.EnrollmentExceptionOrder != null).First().EnrollmentProtocol.CompetitiveGroup.EducationProgramType.Name,
					_claim.EnrollmentClaims.Where(ec => ec.EnrollmentExceptionOrder != null).First().EnrollmentProtocol.CompetitiveGroup.EducationForm.Name);

			string direction1 = string.Format("{4}, {0} {1} ({2}), {3} форма обучения",
				 _claim.EnrollmentClaims.Where(ec => ec.EnrollmentExceptionOrder != null).First().EnrollmentProtocol.CompetitiveGroup.Direction.Code,
				  _claim.EnrollmentClaims.Where(ec => ec.EnrollmentExceptionOrder != null).First().EnrollmentProtocol.CompetitiveGroup.Direction.Name,
				   _claim.EnrollmentClaims.Where(ec => ec.EnrollmentExceptionOrder != null).First().EnrollmentProtocol.CompetitiveGroup.EducationProgramType.Name,
					_claim.EnrollmentClaims.Where(ec => ec.EnrollmentExceptionOrder != null).First().EnrollmentProtocol.CompetitiveGroup.EducationForm.Name,
					_claim.EnrollmentClaims.Where(ec => ec.EnrollmentExceptionOrder != null).First().EnrollmentProtocol.Faculty.Name);

			document.InsertToBookmark("Date", DateTime.Now.ToString("«dd» MMMM yyyy г."), underlinedText);
			document.InsertToBookmark("Date1", DateTime.Now.ToString("«dd» MMMM yyyy г."), 
				new CharacterFormat
				{
					UnderlineColor = Color.Black,
					UnderlineStyle = UnderlineType.Single,
					Size = 16,
					FontName = "Times New Roman",
					FontColor = Color.Black
				});
			document.InsertToBookmark("BirthDate", ((DateTime)_claim.Person.BirthDate).ToString("dd.MM.yyyy г."));
			document.InsertToBookmark("Citizenship", _claim.IdentityDocuments.First().Citizenship.Name);
			document.InsertToBookmark("CurrentUser", string.Format("{0} {1}.{2}.",
				Session.CurrentUser.LastName, Session.CurrentUser.FirstName[0], Session.CurrentUser.Patronymic[0]));
			document.InsertToBookmark("Direction", direction);
			document.InsertToBookmark("DocIssueInfo", string.Format("{0}, {1}",
				_claim.IdentityDocuments.First().Organization,
				((DateTime)_claim.IdentityDocuments.First().Date).ToString("dd.MM.yyyy г.")));
			document.InsertToBookmark("DocNumber",
				_claim.IdentityDocuments.First().Number);
			document.InsertToBookmark("DocSeries",
				_claim.IdentityDocuments.First().Series);
			document.InsertToBookmark("DocumentName",
				_claim.IdentityDocuments.First().IdentityDocumentType.Name);
			document.InsertToBookmark("EntrantName",
				_claim.Person.FullName);
			document.InsertToBookmark("EnrollmentOrder", 
				string.Format("№{0} от {1}",
				_claim.EnrollmentClaims.Where(ec => ec.EnrollmentExceptionOrder != null).First().EnrollmentProtocol.EnrollmentOrder.Number,
				((DateTime)_claim.EnrollmentClaims.Where(ec => ec.EnrollmentExceptionOrder != null).First().EnrollmentProtocol.EnrollmentOrder.Date).ToString("dd.MM.yyyy г.")),
				new CharacterFormat
				{
					UnderlineColor = Color.Black,
					UnderlineStyle = UnderlineType.Single,
					Size = 16,
					FontName = "Times New Roman",
					FontColor = Color.Black
				});
			document.InsertToBookmark("EntrantName1",
				_claim.Person.FullName);
			document.InsertToBookmark("EntrantName2",
				_claim.Person.FullName);
			document.InsertToBookmark("FinanceSource",
				_claim.EnrollmentClaims.Where(ec => ec.EnrollmentExceptionOrder != null).First().EnrollmentProtocol.CompetitiveGroup.FinanceSource.NameInDocument);
			document.InsertToBookmark("FacultyName", direction1);

			// Сохраняем в файл
			document.Save(fileName);

		}
	}
}
