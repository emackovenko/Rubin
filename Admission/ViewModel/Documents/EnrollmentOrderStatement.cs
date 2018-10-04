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
using CommonMethods.TypeExtensions.exDateTime;

namespace Admission.ViewModel.Documents
{
	public class EnrollmentOrderStatement : OpenXmlDocument
	{
		public EnrollmentOrderStatement(Claim claim)
		{
			_claim = claim;

            _order = _claim.EnrollmentOrder;

			DocumentType = OpenXmlDocumentType.Document;
		}

        EnrollmentOrder _order;

		Claim _claim;

		public override void CreatePackage(string fileName)
		{

            // Извлекаем шаблон
            var document = DocumentModel.Load(DocumentTemplate.ExtractDoc("EnrollmentOrderStatement"));

            // Подготовка стилей
            var underlinedCharacterFormat = new CharacterFormat
            {
                UnderlineColor = Color.Black,
                UnderlineStyle = UnderlineType.Single,
                Size = 12.0
            };

            var simpleTextFormat = new CharacterFormat
            {
                Size = 12.0,
                FontName = "Times New Roman"
            };


            // Вставляем текст на закладки
            document.InsertToBookmark("OrderDate",
                ((DateTime)_order.Date).ToString("«dd» MMMM yyyy г."), simpleTextFormat);
            document.InsertToBookmark("OrderNumber", _order.Number, underlinedCharacterFormat);
            document.InsertToBookmark("TrainingBeginDate",
                ((DateTime)_order.EnrollmentProtocols.First().TrainingBeginDate).ToString("dd.MM.yyyy"));
            document.InsertToBookmark("EducationForm", ChangeToAccusative(_order.EducationForm.Name), simpleTextFormat);

            string auxOrderReason = "оригинал документа об образовании";
            if (_order.FinanceSource.Id == 2)
            {
                auxOrderReason = "договоры об оказании платных образовательных услуг";
            }
            document.InsertToBookmark("AuxOrderReason", auxOrderReason);

            if (_order.FinanceSource.Id == 2)
            {
                document.InsertToBookmark("IsPaylineEducation", "не ", simpleTextFormat);
            }

            // Получаем строку с протоклами
            string namesProtocol = string.Empty;
            foreach (var p in _order.EnrollmentProtocols.OrderBy(p => p.Number))
            {
                namesProtocol += string.Format(" № {0} от {1},", p.Number,
                    ((DateTime)p.Date).ToString("dd.MM.yyyy г."));
            }
            namesProtocol = namesProtocol.TrimEnd(',');

            document.InsertToBookmark("EnrollmentProtocols", namesProtocol, simpleTextFormat);
         
            // Вставляем таблицы по протоколам и заполняем их
            int tableIndex = 2;
            foreach (var protocol in _order.EnrollmentProtocols.Where(ep => ep.EnrollmentClaims.Contains(_claim.EnrollmentClaims.FirstOrDefault())))
            {
                var table = document.Sections[tableIndex].GetChildElements(true, ElementType.Table).Cast<Table>().FirstOrDefault();

                // Направление подготовки
                string dirStrHelper = "программа бакалавриата";
                if (protocol.CompetitiveGroup.Direction.DirectionProfiles.Count() > 1)
                {
                    dirStrHelper = "совокупность программ бакалавриата";
                }

                string directionString = string.Format("Направление подготовки\n{0} {1}, {2}\n",
                    protocol.CompetitiveGroup.Direction.Code, protocol.CompetitiveGroup.Direction.Name,
                    dirStrHelper);
                foreach (var profile in protocol.CompetitiveGroup.Direction.DirectionProfiles)
                {
                    directionString += string.Format("«{0}», ", profile.Name);
                }
                directionString = directionString.Trim(' ');
                directionString = directionString.Trim(',');

                table.Rows[0].Cells[0].Content.LoadText(directionString, simpleTextFormat);

                // Срок освоения
                string trainingTime = string.Format("Срок получения образования по программе: {0}", protocol.TrainingTime.AsPeriod());
                table.Rows[1].Cells[0].Content.LoadText(trainingTime, simpleTextFormat);

                // Срок окончания
                string trainingEndDate = string.Format("Срок окончания обучения по образовательной программе: {0}",
                    ((DateTime)protocol.TrainingEndDate).ToString("dd MMMM yyyy г."));

                //ебучий костыль: строчка, что выше, выводится в документ только у очников
                if (_order.EducationForm.Id == 2)
                {
                    trainingEndDate = string.Empty;
                }
                table.Rows[2].Cells[0].Content.LoadText(trainingEndDate, simpleTextFormat);

                // Список
                var rowTemplate = table.Rows[4];
                foreach (var claim in protocol.EnrollmentClaims.Where(ec => ec.ClaimId == _claim.Id).OrderBy(ec => ec.StringNumber))
                {
                    var tableRow = rowTemplate.Clone(true);

                    // №
                    tableRow.Cells[0].Content.LoadText(claim.StringNumber.ToString(), simpleTextFormat);

                    // ФИО
                    tableRow.Cells[1].Content.LoadText(claim.Claim.Person.FullName, simpleTextFormat);

                    // Основание
                    tableRow.Cells[2].Content.LoadText(protocol.CompetitiveGroup.FinanceSource.EnrollmentReason, simpleTextFormat);

                    // Рег. номер
                    tableRow.Cells[3].Content.LoadText(claim.Claim.Number, simpleTextFormat);

                    // Сумма баллов	 
                    tableRow.Cells[4].Content.LoadText(claim.Claim.TotalScore.ToString(), simpleTextFormat);
                    tableRow.Cells[0].CellFormat = new TableCellFormat
                    {
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    table.Rows.Add(tableRow);
                }

                // Итог
                var resultRow = table.Rows[3].Clone(true);
                resultRow.Cells[0].Content.LoadText("Итого:", simpleTextFormat);
                resultRow.Cells[1].Content.LoadText(string.Format("{0} - {1}",
                    protocol.CompetitiveGroup.FinanceSource.EnrollmentReason, 1), simpleTextFormat);
                table.Rows.Add(resultRow);
                table.Rows.Remove(table.Rows[3]);

                tableIndex++;
            }

            document.InsertToBookmark("StatementDate", _order.Date.Format());

            document.Save(fileName);
        }

        /// <summary>
        /// Возвращает имя формы обучения в винительном падеже
        /// </summary>
        /// <param name="educationFormName">имя в оригинале</param>
        /// <returns></returns>
        private string ChangeToAccusative(string educationFormName)
		{
			string str = educationFormName;
			str = str.Replace("ая", "ой");
			return str;
		}

	}
}
