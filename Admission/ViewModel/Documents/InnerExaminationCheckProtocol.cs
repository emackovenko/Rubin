using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;
using CommonMethods.Documents;
using CommonMethods.TypeExtensions.exGemBox;
using ResourceLibrary.Documents;
using GemBox.Spreadsheet;
using MoreLinq;

namespace Admission.ViewModel.Documents
{
	public class InnerExaminationCheckProtocol : OpenXmlDocument
	{

		public InnerExaminationCheckProtocol(EntranceExaminationsCheckProtocol protocol)
		{
			_protocol = protocol;
		}

		EntranceExaminationsCheckProtocol _protocol;

		public override void CreatePackage(string fileName)
		{
			// Извлекаем шаблон из вспомогательной сборки и создаем объект экселя
			var ef = ExcelFile.Load(DocumentTemplate.ExtractXls("InnerExaminationCheckProtocol"));
			var ws = ef.Worksheets[0];

			// Заполнение шляпы
			string protocolName = string.Format("№ {0} от {1}", 
				_protocol.Number, ((DateTime)_protocol.Date).ToString("dd MMMM yyyy г."));
			ws.FindAndReplaceText("ProtocolName", protocolName);
						
			string subjectString = string.Format("по предмету: {0}", 
				_protocol.EntranceTests.First().ExamSubject.Name);
			ws.FindAndReplaceText("SubjectString", subjectString);

			string examinationDate = string.Format("Дата экзамена: {0}",
				((DateTime)_protocol.EntranceTests.First().ExaminationDate).ToString("dd MMMM yyyy г."));
			ws.FindAndReplaceText("ExaminationDate", examinationDate);

			// Подготовка данных
			// Получаем результаты по протоколу
			var results = new List<EntranceTestResult>();
			foreach (var exam in _protocol.EntranceTests)
			{
				foreach (var result in exam.EntranceTestResult)
				{
					if (result.Claim != null)
					{
						results.Add(result);
					}
				}
			}
			
			// Сортируем результаты по ФИО абитуриента
			results = results.OrderBy(res => res.Claim.Person.FullName).ToList();

			// Выгружаем в документ строки таблицы
			int i = 9;
			int j = 1;
			var tableStringTemplate = ws.Rows[i];
			foreach (var result in results)
			{
				ws.Rows.InsertCopy(i, tableStringTemplate);
				var currentRow = ws.Rows[i];
				currentRow.Cells[0].Value = j;
				currentRow.Cells[1].Value = result.Claim.Person.FullName;
				currentRow.Cells[2].Value = result.Value > 0 ? result.Value.ToString() : "н/я";
				i++;
				j++;
			}

			// Костыль
			ws.Rows[i].Hidden = true;

			ef.Save(fileName);
		}
		
	}
}
