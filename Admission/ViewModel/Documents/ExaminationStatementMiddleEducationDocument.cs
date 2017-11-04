using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;
using CommonMethods.Documents;
using CommonMethods.TypeExtensions.exNumeric;
using GemBox.Document;
using ResourceLibrary.Documents;
using CommonMethods.TypeExtensions.exGemBox;

namespace Admission.ViewModel.Documents
{
	public class ExaminationStatementMiddleEducationDocument : OpenXmlDocument
	{
		public ExaminationStatementMiddleEducationDocument(Claim claim)
		{
			_claim = claim;
			DocumentType = OpenXmlDocumentType.Document;
		}

		Claim _claim;

		public override void CreatePackage(string fileName)
		{
			var document = DocumentModel.Load(DocumentTemplate.ExtractDoc("ExaminationStatementMiddleEducation"));

			// Подготовка стилей
			var underlinedText = new CharacterFormat
			{
				FontName = "Times New Roman",
				Size = 12,
				UnderlineColor = Color.Black,
				UnderlineStyle = UnderlineType.Single
			};
			var underlinedBoldText = new CharacterFormat
			{
				FontName = "Times New Roman",
				Size = 12,
				UnderlineColor = Color.Black,
				UnderlineStyle = UnderlineType.Single,
				Bold = true
			};

			document.InsertToBookmark("ClaimNumber", _claim.Number, underlinedText);

			document.InsertToBookmark("CurrentUser", 
				string.Format("{0} {1}.{2}.", Session.CurrentUser.LastName,
				Session.CurrentUser.FirstName[0], Session.CurrentUser.Patronymic[0]));

			document.InsertToBookmark("Date", 
				DateTime.Now.ToString("«dd» MMMM yyyy г."), underlinedText);

			document.InsertToBookmark("EntrantName", 
				_claim.Person.FullName);

			string middleMark = string.Format("{0}({1} балла)", 
				_claim.MiddleMark, 
				RusCurrency.Str(_claim.MiddleMark, true, "целый", "целых", "целых", "сотый", "сотых", "сотых"));

			document.InsertToBookmark("MiddleMark", middleMark);
			document.InsertToBookmark("ProtocolInfo", 
				string.Format("{0} №{1}",
				((DateTime)_claim.SchoolCertificateDocuments.First().EntranceExaminationsCheckProtocol.Date).ToString("dd.MM.yyyy г."),
				_claim.SchoolCertificateDocuments.First().EntranceExaminationsCheckProtocol.Number), underlinedBoldText);

			document.Save(fileName, SaveOptions.DocxDefault);
		}
	}
}
