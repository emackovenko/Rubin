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
using CommonMethods.TypeExtensions.exNumeric;

namespace Admission.ViewModel.Documents
{
	public class ExaminationStatement : OpenXmlDocument
	{
		public ExaminationStatement(Claim claim)
		{
			_claim = claim;
			DocumentType = OpenXmlDocumentType.Document;
		}


		Claim _claim;

		public override void CreatePackage(string fileName)
		{
			// Загружаем документ
			var document = DocumentModel.Load(DocumentTemplate.ExtractDoc("ExaminationStatement"));

			// Подготовка стилей
			var paragraphStyle = (ParagraphStyle)Style.CreateStyle(StyleTemplateType.Title, document);
			paragraphStyle.ParagraphFormat = new ParagraphFormat
			{
				Alignment = HorizontalAlignment.Center,
				SpaceBefore = 0.0,
				LeftIndentation = 0.0,
				SpecialIndentation = 0.0
			};
			paragraphStyle.CharacterFormat = new CharacterFormat
			{
				FontName = "Times New Roman",
				Size = 12.0,
				FontColor = Color.Black
			};
			document.Styles.Add(paragraphStyle);

			// Вставляем текст на закладки
			document.InsertToBookmark("ClaimNumber", _claim.Number);
			document.InsertToBookmark("EntrantName", _claim.Person.FullName);
			document.InsertToBookmark("ExamMark", _claim.TestScore.ToString());
			document.InsertToBookmark("ExamMarkStr", _claim.TestScore.ScoreString());
			document.InsertToBookmark("IaMark", _claim.IndividualAchievementsScore.ToString());
			document.InsertToBookmark("IaMarkStr", _claim.IndividualAchievementsScore.ScoreString());
			document.InsertToBookmark("TotalMark", _claim.TotalScore.ToString());
			document.InsertToBookmark("TotalMarkStr", _claim.TotalScore.ScoreString());

			// Оформляем табличку
			var table = document.GetChildElements(true, ElementType.Table).Cast<Table>().FirstOrDefault();
			int i = 1;
			// Получаем вступительные испытания
			// СЭ
			foreach (var exam in _claim.EntranceTestResults)
			{
				var row = new TableRow(document);

				var numberCell = new TableCell(document, new Paragraph(document, i.ToString())
				{
					ParagraphFormat = new ParagraphFormat
					{
						Style = paragraphStyle
					}
				});
				numberCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
				row.Cells.Add(numberCell);

				var subjectCell = new TableCell(document, new Paragraph(document, exam.EntranceTest.ExamSubject.Name + " - СЭ")
				{
					ParagraphFormat = new ParagraphFormat
					{
						Style = paragraphStyle
					}
				});
				subjectCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
				row.Cells.Add(subjectCell);

				var scoreCell = new TableCell(document, new Paragraph(document,
					string.Format("{0} ({1})", exam.Value, ((int)exam.Value).ScoreString()))
				{
					ParagraphFormat = new ParagraphFormat
					{
						Style = paragraphStyle
					}
				});
				scoreCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
				row.Cells.Add(scoreCell);

				var protocolCell = new TableCell(document, new Paragraph(document,
					string.Format("{0} от {1}", exam.EntranceTest.EntranceExaminationsCheckProtocol.Number,
					((DateTime)exam.EntranceTest.EntranceExaminationsCheckProtocol.Date).ToString("dd.MM.yyyy")))
				{
					ParagraphFormat = new ParagraphFormat
					{
						Style = paragraphStyle
					}
				});
				protocolCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
				row.Cells.Add(protocolCell);

				table.Rows.Add(row);
				i++;
			}

			// ЕГЭ
			foreach (var egeDoc in _claim.EgeDocuments)
			{
				foreach (var exam in egeDoc.EgeResults)
				{

					var row = new TableRow(document);

					var numberCell = new TableCell(document, new Paragraph(document, i.ToString())
					{
						ParagraphFormat = new ParagraphFormat
						{
							Style = paragraphStyle
						}
					});
					numberCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
					row.Cells.Add(numberCell);

					var subjectCell = new TableCell(document, new Paragraph(document, exam.ExamSubject.Name + " - ЕГЭ")
					{
						ParagraphFormat = new ParagraphFormat
						{
							Style = paragraphStyle
						}
					});
					subjectCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
					row.Cells.Add(subjectCell);

					var scoreCell = new TableCell(document, new Paragraph(document,
						string.Format("{0} ({1})", exam.Value, ((int)exam.Value).ScoreString()))
					{
						ParagraphFormat = new ParagraphFormat
						{
							Style = paragraphStyle
						}
					});
					scoreCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
					row.Cells.Add(scoreCell);

					var protocolCell = new TableCell(document, new Paragraph(document,
						string.Format("{0} от {1}", exam.EntranceExaminationsCheckProtocol.Number,
						((DateTime)exam.EntranceExaminationsCheckProtocol.Date).ToString("dd.MM.yyyy")))
					{
						ParagraphFormat = new ParagraphFormat
						{
							Style = paragraphStyle
						}
					});
					protocolCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
					row.Cells.Add(protocolCell);

					table.Rows.Add(row);
					i++;
				}
			}

			document.Save(fileName, SaveOptions.DocxDefault);

		}
	}
}
