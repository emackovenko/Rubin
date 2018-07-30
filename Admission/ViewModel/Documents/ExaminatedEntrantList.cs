using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GemBox.Spreadsheet;
using Model.Admission;
using CommonMethods.Documents;
using CommonMethods.TypeExtensions.exGemBox;
using CommonMethods.TypeExtensions.exDateTime;
using ResourceLibrary.Documents;

namespace Admission.ViewModel.Documents
{
    public class ExaminatedEntrantList : OpenXmlDocument
    {
        public ExaminatedEntrantList(IEnumerable<EntranceTestResult> tests)
        {
            exams = tests;
            DocumentType = OpenXmlDocumentType.Spreadsheet;
        }

        IEnumerable<EntranceTestResult> exams;

        public override void CreatePackage(string fileName)
        {
            var ef = ExcelFile.Load(DocumentTemplate.ExtractXls("ExaminatedEntrantList"));
            var ws = ef.Worksheets[0];

            // заполняем статичные данные
            string examName = exams.FirstOrDefault()?.EntranceTest.ExamSubject.Name;
            string examDate = exams.FirstOrDefault()?.EntranceTest.ExaminationDate.Value.ToString("dd.MM.yyyy");
            ws.FindAndReplaceText("SubjectName", examName);
            ws.FindAndReplaceText("ExaminationDate", examDate);

            // заполняем список

            // получаем абитуриентов на все выбранные экзамены в коллекцию
            var claims = new List<Claim>();
            claims.AddRange(exams.Where(cc => cc.Claim != null)
                .Where(cc => cc.Claim.ClaimStatusId != 3 && cc.Claim.ClaimStatusId != 4)
                .Select(cc => cc.Claim));

            claims = claims.OrderBy(c => c.Person.FullName).ToList();
            claims = claims.Distinct().ToList();
            
            // вставляем их в документ
            int i = 1;
            foreach (var claim in claims)
            {
                ws.Rows[i+3].Cells[0].Value = string.Format("{0}.", i);
                ws.Rows[i+3].Cells[1].Value = claim.Person.FullName;
                i++;
            }

            ef.Save(fileName);
        }
    }
}
