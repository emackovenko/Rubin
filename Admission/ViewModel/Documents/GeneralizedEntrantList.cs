using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;
using GemBox.Spreadsheet;
using CommonMethods.Documents;
using CommonMethods.TypeExtensions.exString;
using CommonMethods.TypeExtensions.exGemBox;
using ResourceLibrary.Documents;

namespace Admission.ViewModel.Documents
{
    public class GeneralizedEntrantList : OpenXmlDocument
    {
        public GeneralizedEntrantList(EducationForm educationForm)
        {
            _educationForm = educationForm;
        }

        EducationForm _educationForm;

        public override void CreatePackage(string fileName)
        {
            // Извлекаем шаблон из вспомогательной сборки и создаем объект экселя
            string templateName = "GeneralizedEntrantList";
            ExcelFile ef = ExcelFile.Load(DocumentTemplate.ExtractXls(templateName));
            ExcelWorksheet worksheet = ef.Worksheets[0];

            // Оформляем шляпу
            int rowIndex, columnIndex;
            if (worksheet.Cells.FindText("[ReportTitle]", true, true, out rowIndex, out columnIndex))
            {
                worksheet.Cells[rowIndex, columnIndex].Value = string.Format("Список абитуриентов на {0}",
                    DateTime.Now.ToString("dd.MM.yyyy г."));
            }
            
            worksheet.FindAndReplaceText("EducationFormName", _educationForm.Name.BeginWithUpper() + " форма обучения");

            // Заполняем таблицу

            // Копируем 6-ую строку как шаблон
            int i = 6;
            var templateRow = worksheet.Rows[i];

            // Выбираем заявления со статусом новое или принято и фильтруем по форме обучения
            var notPreparedClaims = Session.DataModel.Claims.ToList();
            var actualClaims = notPreparedClaims
                    .Where(c => c.ClaimStatusId == 1 || c.ClaimStatusId == 2 || c.ClaimStatus.Id == 3)
                    .Where(c => c.EducationForm.Id == _educationForm.Id)
                    .Where(c => c.RegistrationDate.Value.Year == DateTime.Now.Year);

            // Сортируем заявления
            actualClaims = actualClaims.OrderBy(c => c.FinanceSource.SortNumber).ThenByDescending(c => c.TotalScore);

            foreach (var claim in actualClaims)
            {
                // Подготовка данных
                var entrant = claim.Entrants.First();
                string entrantName = string.Format("{0} {1} {2}", 
                    entrant.LastName, entrant.FirstName, entrant.Patronymic);
                string docType = claim.IsOriginal ? "ориг." : "копия";
                string finSource = claim.FinanceSource != null ? claim.FinanceSource.Name : string.Empty;
                string fisrtDirection = claim.FirstDirection != null ? claim.FirstDirection.ShortName : string.Empty;
                string secondDirection = claim.SecondDirection != null ? claim.SecondDirection.ShortName : string.Empty;
                string thirdDirection = claim.ThirdDirection != null ? claim.ThirdDirection.ShortName : string.Empty;
                string examType = string.Empty;
                if (claim.EgeDocuments.Count > 0)
                {
                    examType = "ЕГЭ";
                }
                else
                {
                    if (claim.EntranceTestResults.Count > 0)
                    {
                        examType = "СЭ";
                    }
                }
                if (claim.EgeDocuments.Count > 0 && claim.EntranceTestResults.Count > 0)
                {
                    examType = "ЕГЭ/СЭ";
                }
                if (claim.EgeDocuments.Count == 0 && claim.EntranceTestResults.Count == 0)
                {
                    examType = "аттестат";
                }

                double totalScore = 0.0;

				if (claim.ClaimConditions.First().CompetitiveGroup.EducationLevel.Id == 2)
				{
					if (claim.SchoolCertificateDocuments.Count > 0)
					{
						totalScore = claim.SchoolCertificateDocuments.First().MiddleMark;
					}
				}
				else
                {
					totalScore = Convert.ToDouble(claim.TotalScore);
                }

                string phone = string.Format("{0};{1}", entrant.Phone, entrant.MobilePhone);

				// Подготовка строки
				worksheet.Rows.InsertCopy(i, templateRow);
				var currentRow = worksheet.Rows[i];
				i++;

                // Вставка данных в документ
                currentRow.Cells[0].Value = entrantName;
                currentRow.Cells[1].Value = docType;
                currentRow.Cells[2].Value = finSource;
                currentRow.Cells[3].Value = fisrtDirection;
                currentRow.Cells[4].Value = secondDirection;
                currentRow.Cells[5].Value = thirdDirection;
                currentRow.Cells[6].Value = examType;
                currentRow.Cells[7].Value = totalScore;
                currentRow.Cells[8].Value = phone;
			}

            // Сохраняем документ
            ef.Save(fileName);

        }
    }
}
