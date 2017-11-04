using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;
using GemBox.Document;
using GemBox.Document.Tables;
using CommonMethods.Documents;
using ResourceLibrary.Documents;
using CommonMethods.TypeExtensions.exGemBox;
using CommonMethods.TypeExtensions.exString;

namespace Admission.ViewModel.Documents
{
	public class EnrollmentOrderStatement : OpenXmlDocument
	{
		public EnrollmentOrderStatement(Claim claim)
		{
			_claim = claim;
			DocumentType = OpenXmlDocumentType.Document;
		}


		Claim _claim;

		public override void CreatePackage(string fileName)
		{
			// Загружаем документ
			var document = DocumentModel.Load(DocumentTemplate.ExtractDoc("EnrollmentOrderStatement"));

			// Подготовка стилей
			var underlinedText = new CharacterFormat
			{
				UnderlineColor = Color.Black,
				UnderlineStyle = UnderlineType.Single
			};

			var ec = _claim.EnrollmentClaims.Where(e => e.EnrollmentExceptionOrder == null).FirstOrDefault();

			if (ec == null)
			{
				throw new Exception("Нет приказа о зачислении из которого этот чувак не был бы исключен.");
			}

			// Вставляем текст на закладки
			document.InsertToBookmark("OrderDate", 
				((DateTime)ec.EnrollmentProtocol.EnrollmentOrder.Date).ToString("«dd» MMMM yyyy г."));
			document.InsertToBookmark("OrderNumber",
				ec.EnrollmentProtocol.EnrollmentOrder.Number,
				underlinedText);
			document.InsertToBookmark("TrainingBeginDate",
				((DateTime)ec.EnrollmentProtocol.TrainingBeginDate).ToString("d MMMM yyyy"));
			document.InsertToBookmark("TrainingEndDate",
				((DateTime)ec.EnrollmentProtocol.TrainingEndDate).ToString("d MMMM yyyy г."));		   
			document.InsertToBookmark("EducationFormName", 
				ChangeToAccusative(_claim.EnrollmentClaims.First().EnrollmentProtocol.CompetitiveGroup.EducationForm.Name));
			document.InsertToBookmark("FacultyName",
				ec.EnrollmentProtocol.Faculty.Name);
			document.InsertToBookmark("DirectionString",
				string.Format("Направление подготовки ({0}): {1} {2}",
				ec.EnrollmentProtocol.CompetitiveGroup.EducationProgramType.Name,
				ec.EnrollmentProtocol.CompetitiveGroup.Direction.Code,
				ec.EnrollmentProtocol.CompetitiveGroup.Direction.Name));
			document.InsertToBookmark("TableStringNumber", 
				ec.StringNumber.ToString());
			document.InsertToBookmark("StudentName", 
				_claim.Person.FullName);
			document.InsertToBookmark("EnrollmentReason",
				ec.EnrollmentProtocol.CompetitiveGroup.FinanceSource.EnrollmentReason);
			document.InsertToBookmark("ClaimNumber", 
				_claim.Number);
            string totalScore;
            if (_claim.TotalScore > 0)
            {
                totalScore = _claim.TotalScore.ToString();
            }
            else
            {
                totalScore = _claim.MiddleMark.ToString();
            }
			document.InsertToBookmark("TotalScore", 
				totalScore);
			document.InsertToBookmark("ProtocolInfo", 
				string.Format("№ {0} от {1}",
				ec.EnrollmentProtocol.Number,
				((DateTime)ec.EnrollmentProtocol.Date).ToString("dd.MM.yyyy г.")));
			document.InsertToBookmark("StatementDate",
				((DateTime)ec.EnrollmentProtocol.EnrollmentOrder.Date).ToString("dd.MM.yyyy г."));

            document.InsertToBookmark("TrainingPeriod", 
                _claim.EnrollmentClaims.First().EnrollmentProtocol.TrainingTime.AsPeriod());

			document.Save(fileName, SaveOptions.DocxDefault);
		}

		/// <summary>
		/// Возвращает имя формы обучения в винительном падеже
		/// </summary>
		/// <param name="educationFormName">имя в оригинале</param>
		/// <returns></returns>
		private string ChangeToAccusative(string educationFormName)
		{
			string str = educationFormName;
			str = str.Replace("ая", "ую");
			return str;
		}

	}
}
