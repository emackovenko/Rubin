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
    public class EnrollmentProtocolDocument : OpenXmlDocument
    {
        public EnrollmentProtocolDocument(EnrollmentProtocol protocol)
        {
            _protocol = protocol;
        }

        EnrollmentProtocol _protocol;

        public override void CreatePackage(string fileName)
        {
            // Извлекаем шаблон из вспомогательной сборки и создаем объект экселя
            var ef = ExcelFile.Load(DocumentTemplate.ExtractXls("EnrollmentProtocol"));
            var ws = ef.Worksheets[0];

            // готовим шляпу
            string title = string.Format("Протокол зачисления приёмной комиссии № {0} от {1}",
                _protocol.Number, ((DateTime)_protocol.Date).ToString("dd.MM.yyyy г."));
            ws.FindAndReplaceText("Title", title);

            //string enrollString = string.Format("Зачислить в число студентов РИИ АлтГТУ с {0}",
            //    ((DateTime)_protocol.TrainingBeginDate).ToString("dd.MM.yyyy г."));
            string enrollString = string.Format("Зачислить в число студентов РИИ АлтГТУ с 01.09.2018 г.");
            ws.FindAndReplaceText("EnrollmentString", enrollString);

            string cgString = string.Format("на направление подготовки (бакалавриат) ВО {0}\"{1}\" ({2}) ({3} форма обучения)",
                _protocol.Direction.Code, _protocol.Direction.Name, _protocol.CompetitiveGroup.EducationProgramType.Name,
                _protocol.EducationForm.Name);
            ws.FindAndReplaceText("CompetitiveGroupString", cgString);

            if (_protocol.EnrollmentClaims.Where(ec => ec.Claim != null).Count() > 1)
            {
                ws.FindAndReplaceText("NextEntrantsString", "следующих поступающих согласно списку:");
            }
            else
            {
                ws.FindAndReplaceText("NextEntrantsString", "следующего поступающего:");
            }
            // Шаблоны строк
            var tableStringTemplate = ws.Rows[9];
            var statisticStringTemplate = ws.Rows[11];
            int i = 9;

            // ПОДГОТОВКА ДАННЫХ В ТАБЛИЦУ
            int j = 1;

            // Получаем заявления, сортируем по фамилии
            var claims = (from es in _protocol.EnrollmentClaims
                          where es.Claim != null
                          select es.Claim).ToList();
            claims = claims.OrderBy(c => c.Person.FullName).ToList();
            foreach (var claim in claims)
            {
                // Вставляем в таблицу
                ws.Rows.InsertCopy(i, tableStringTemplate);
                var currentRow = ws.Rows[i];
                currentRow.Cells[0].Value = j;
                currentRow.Cells[1].Value = claim.Person.FullName;
                currentRow.Cells[2].Value = claim.GetExamResultBySubjectId(3);
                currentRow.Cells[3].Value = claim.GetExamResultBySubjectId(2);
                currentRow.Cells[4].Value = claim.GetExamResultBySubjectId(1);
                currentRow.Cells[5].Value = claim.IndividualAchievementsScore;
                currentRow.Cells[6].Value = claim.TotalScore;
                currentRow.Cells[7].Value = claim.MiddleMark;
                currentRow.Cells[8].Value = "на общих основаниях";//claim.FinanceSource.Name;
                currentRow.Cells[9].Value = claim.Number;
                i++;
                j++;
            }

            // Как всегда костыль: скрываем лишнюю ебаную строку
            ws.Rows[i].Hidden = true;

            ef.Save(fileName);
        }
    }
}
