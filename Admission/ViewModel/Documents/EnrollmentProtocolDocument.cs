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

            string enrollString = string.Format("Зачислить в число студентов РИИ АлтГТУ с {0} по {1} форме обучения",
                ((DateTime)_protocol.TrainingBeginDate).ToString("dd.MM.yyyy г."),
                ChangeToAccusative(_protocol.CompetitiveGroup.EducationForm.Name));
            ws.FindAndReplaceText("EnrollmentString", enrollString);

            string cgString = string.Format("на направление подготовки {0} {1}",
                _protocol.Direction.Code, _protocol.Direction.Name);
            ws.FindAndReplaceText("CompetitiveGroupString", cgString);

            string countPrograms = "программа бакалавриата";
            if (_protocol.Direction.DirectionProfiles.Count() > 1)
            {
                countPrograms = "совокупность программ бакалавриата";
            }

            string programNames = string.Empty;
            foreach (var profile in _protocol.Direction.DirectionProfiles)
            {
                programNames += string.Format("«{0}», ", profile.Name);
            }
            programNames = programNames.Trim(' ');
            programNames = programNames.Trim(',');

            string nextEntrantsString = string.Format("{0} {1}", countPrograms, programNames);
            ws.FindAndReplaceText("NextEntrantsString", nextEntrantsString);

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
                currentRow.Cells[8].Value = claim.FinanceSource.EnrollmentReason;
                currentRow.Cells[9].Value = claim.Number;
                i++;
                j++;
            }

            // Как всегда костыль: скрываем лишнюю ебаную строку
            ws.Rows[i].Hidden = true;

            ef.Save(fileName);
        }


        /// <summary>
        /// Возвращает имя формы обучения в винительном (теперь предложном) падеже
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
