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
	public class PublicEntrantList : OpenXmlDocument
	{
		public PublicEntrantList(EducationForm educationForm)
		{
			_educationForm = educationForm;
		}

		EducationForm _educationForm;

		public override void CreatePackage(string fileName)
		{
			// Извлекаем шаблон из вспомогательной сборки и создаем объект экселя
			var ef = ExcelFile.Load(DocumentTemplate.ExtractXls("PublicEntrantList"));
			var ws = ef.Worksheets[0];

			// готовим шляпу
			string reportTitle = string.Format("Списки поступающих на {0} - {1} форма обучения",
				DateTime.Now.ToString("dd.MM.yyyy г."), _educationForm.Name);
			ws.FindAndReplaceText("ReportTitle", reportTitle);

			// Подготовка данных
			var directionNameTemplate = ws.Rows[0];
			var headTableTemplate = ws.Rows[2];
			var itemTableTemplate = ws.Rows[3];
			var i = 7;
			// Получаем список направлений
			var directions = (from compGroup in Session.DataModel.CompetitiveGroups
							  where compGroup.EducationForm.Id == _educationForm.Id &&
							  compGroup.Campaign.CampaignStatus.Id == 2
							  orderby compGroup.EducationLevel.Id, compGroup.Direction.Code
							  select compGroup.Direction).ToList();

			// Удалить дубликаты из коллекции, отсортировать по коду
			directions = directions.DistinctBy(d => d.Code).ToList();

			foreach (var direction in directions)
			{
				// Прописываем направление 
				ws.Rows.InsertCopy(i, directionNameTemplate);
				ws.Rows[i].Cells[0].Value = string.Format("{0} {1}", 
					direction.Code, direction.Name);
				i += 2;

				ws.Rows.InsertCopy(i, headTableTemplate);
				i++;
				int j = 1;

				// Получаем конкурсные группы по направлению и форме обучения
				var actualCompetitiveGroups = (from compGroup in direction.CompetitiveGroups
											   where compGroup.EducationForm.Id == _educationForm.Id &&
											   compGroup.Campaign.CampaignStatus.Id == 2
											   orderby compGroup.FinanceSource.SortNumber ascending
											   select compGroup).ToList();
				int flag = i;
				foreach (var compGroup in actualCompetitiveGroups)
				{
					// Получить список условий приёма
					var conditions = (from cond in compGroup.ClaimConditions
									  where cond.Claim != null &&
									  (cond.Claim.ClaimStatus.Id == 1 || cond.Claim.ClaimStatus.Id == 2)
									  select cond).OrderByDescending(c => c.Claim.TotalScore).ToList();
					foreach (var condition in conditions)
					{
						ws.Rows.InsertCopy(i, itemTableTemplate);
						var currentRow = ws.Rows[i];
						currentRow.Cells[0].Value = j;
						currentRow.Cells[1].Value = condition.Claim.Person.FullName;
						currentRow.Cells[2].Value = condition.Claim.Number;
						currentRow.Cells[3].Value = condition.Claim.IsOriginal ? "оригинал" : "копия";
						currentRow.Cells[4].Value = condition.CompetitiveGroup.FinanceSource.Name;
						currentRow.Cells[5].Value = condition.Priority;
						currentRow.Cells[6].Value = condition.Claim.TotalScore;
						i++;
						j++;
					}
				}

				// КОСТЫЛЬ: Если нет абитуриентов по направлению, то скрываем ко всем херам
				if (flag == i)
				{
					ws.Rows[i - 3].Hidden = true;
					ws.Rows[i - 2].Hidden = true;
					ws.Rows[i - 1].Hidden = true;
					ws.Rows[i].Hidden = true;
					continue;
				}
				i += 2;
			}

			// Скрываем шаблоны
			directionNameTemplate.Hidden = true;
			headTableTemplate.Hidden = true;
			itemTableTemplate.Hidden = true;
			ws.Rows[1].Hidden = true;
			ws.Rows[4].Hidden = true;

			ef.Save(fileName);
		}
	}
}
