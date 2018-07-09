using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;
using CommonMethods.Documents;
using GemBox.Spreadsheet;
using ResourceLibrary.Documents;
using CommonMethods.TypeExtensions.exGemBox;
using CommonMethods.TypeExtensions.exString;
using MoreLinq;

namespace Admission.ViewModel.Documents
{
    public class AdmissionDynamic : OpenXmlDocument
    {
        public AdmissionDynamic(DateTime selectedDate)
        {
            _selectedDate = selectedDate;
        }

        DateTime _selectedDate;

        public override void CreatePackage(string fileName)
        {
            // Извлекаем шаблон из вспомогательной сборки и создаем объект экселя
            var ef = ExcelFile.Load(DocumentTemplate.ExtractXls("AdmissionDynamic"));
            var ws = ef.Worksheets[0];

            // готовим шляпу
            string reportTitle = string.Empty;
            
            reportTitle = string.Format("Динамика приёма на {0}",
                _selectedDate.ToString("dd.MM.yyyy г."));
            
            ws.FindAndReplaceText("ReportTitle", reportTitle);

            //Заполняем 5 последних дат в шапке от заданной
            for (int j=1; j<=5; j++)
            {
                ws.Rows[4].Cells[j].Value = _selectedDate.AddDays(j - 5).ToString("dd.MM.yyyy г.");
            }


            // ****************************************************************************
            // * ПОДГОТОВКА ДАННЫХ
            // ***************************************************************************/
            // Копируем 5-ю строку как шаблон
            int i = 5;
            var templateRow = ws.Rows[i];

            // Получить направления из конкурсных групп по по форме
            var directions = (from compGroup in Session.DataModel.CompetitiveGroups
                              where compGroup.Campaign.CampaignStatus.Id == 2
                              select compGroup.Direction).ToList();

            // Удалить дубликаты из коллекции, отсортировать по коду
            directions = directions.DistinctBy(d => d.Code).ToList();
            directions = directions.OrderBy(d => d.Code).ToList();

            int[] totalCountClaims = new int[5] { 0, 0, 0, 0, 0 };

            foreach (var direction in directions)
            {
                // Подготовка строки
                ws.Rows.InsertCopy(i, templateRow);
                var currentRow = ws.Rows[i];
                i++;

                string directionName = string.Format("{0} {1}",
                    direction.Code, direction.Name);

                //Вывести направление подготовки
                currentRow.Cells[0].Value = directionName;

                int countClaims = 0;

                //Выбрать заявления поданные до каждого из пяти дней
                for (int j = 0; j < 5; j++)
                {
                    DateTime tempDate = _selectedDate.AddDays(j - 4);

                    var claimsCollection = (from claim in Session.DataModel.Claims
                                            where claim.RegistrationDate <= tempDate
                                            select claim).ToList();

                    //Выборка заявлений только из активной приёмки
                    claimsCollection = claimsCollection.Where(c => c.Campaign.CampaignStatusId == 2).ToList();
                    //Выборка заявлений для текущего направления
                    claimsCollection = claimsCollection.Where(a => a.FirstDirection == direction || a.SecondDirection == direction || a.ThirdDirection == direction).ToList();
                    countClaims = claimsCollection.Count();

                    // Вставляем данные
                    currentRow.Cells[j + 1].Value = countClaims;

                    totalCountClaims[j] += countClaims;
                }
            }

            // Вставляем самые итоговые итоги
            ws.Rows.InsertCopy(i, templateRow);
            var totalRow = ws.Rows[i];
            totalRow.Cells[0].Value = "ИТОГО";
            for(int j=0; j<5; j++)
            {
                totalRow.Cells[j+1].Value = totalCountClaims[j];
            }

            ef.Save(fileName);
        }
    }
}
