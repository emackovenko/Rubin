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
    public class AdmissionStatistic : OpenXmlDocument
    {
        public AdmissionStatistic(EducationForm educationForm, bool isTodayStatistic = false)
        {
            _educationForm = educationForm;
            _isTodayStatistic = isTodayStatistic;
        }

        EducationForm _educationForm;
        bool _isTodayStatistic;

        public override void CreatePackage(string fileName)
        {
            // Извлекаем шаблон из вспомогательной сборки и создаем объект экселя
            var ef = ExcelFile.Load(DocumentTemplate.ExtractXls("AdmissionStatistic"));
            var ws = ef.Worksheets[0];

            // готовим шляпу
            string reportTitle = string.Empty;
            if (_isTodayStatistic)
            {
                reportTitle = string.Format("Статистика приёма за {0}", 
                    DateTime.Now.ToString("dd.MM.yyyy г."));
            }
            else
            {
                reportTitle = string.Format("Статистика приёма на {0}", 
                    DateTime.Now.ToString("dd.MM.yyyy г."));
            }
            ws.FindAndReplaceText("ReportTitle", reportTitle);

            ws.FindAndReplaceText("EducationForm", 
                _educationForm.Name.BeginWithUpper() + " форма обучения");

			/****************************************************************************
			 * ПОДГОТОВКА ДАННЫХ
			 ***************************************************************************/
			// Копируем 7-ю строку как шаблон
			int i = 7;
			var templateRow = ws.Rows[i];


			// Получить направления из конкурсных групп по по форме обучения
			var directions = (from compGroup in Session.DataModel.CompetitiveGroups
							  where compGroup.EducationForm.Id == _educationForm.Id &&
							  compGroup.Campaign.CampaignStatus.Id == 2
							  select compGroup.Direction).ToList();

			// Удалить дубликаты из коллекции, отсортировать по коду
			directions = directions.DistinctBy(d => d.Code).ToList();
			directions = directions.OrderBy(d => d.Code).ToList();

			int kcpBudgetTotal = 0, kcpExtrabudgetTotal = 0, kcpQuotaTotal = 0,
				kcpTotalTotal = 0, countBudgetFactTotal = 0, countBudgetOriginalTotal = 0,
				countExtrabudgetFactTotal = 0, countExtrabudgetOriginalTotal = 0,
				countQuotaFactTotal = 0, countQuotaOriginalTotal = 0,
				countTotalFactTotal = 0, countTotalOriginalTotal = 0;

			foreach (var direction in directions)
			{
				string directionName = string.Format("{0} {1}",
					direction.Code, direction.Name);
				int kcpBudget = 0, kcpExtrabudget = 0, kcpQuota = 0,
					kcpTotal = 0, countBudgetFact = 0, countBudgetOriginal = 0,
					countExtrabudgetFact = 0, countExtrabudgetOriginal = 0,
					countQuotaFact = 0, countQuotaOriginal = 0, 
					countTotalFact = 0, countTotalOriginal = 0;

				// Выбрать конкурсные группы по бюджету, направлению и форме обучения
				var competitiveGroupCollection = (from compGroup in direction.CompetitiveGroups
												  where compGroup.EducationForm.Id == _educationForm.Id &&
													compGroup.FinanceSource.Id == 1 &&
													compGroup.Campaign.CampaignStatus.Id == 2
												  select compGroup);
				// Если есть хоть одна такая, выдираем данные
				if (competitiveGroupCollection.Count() > 0)
				{
					var currentCompetitiveGroup = competitiveGroupCollection.First();

					// Заполняем КЦП
					kcpBudget = currentCompetitiveGroup.PlaceCount ?? 0;
					CalculateClaimCountInCompetitiveGroup(currentCompetitiveGroup,
						out countBudgetFact, out countBudgetOriginal);
				}

				// Выбрать конкурсные группы по внебюджету, направлению и форме обучения
				competitiveGroupCollection = (from compGroup in direction.CompetitiveGroups
											  where compGroup.EducationForm.Id == _educationForm.Id &&
												compGroup.FinanceSource.Id == 2 &&
												compGroup.Campaign.CampaignStatus.Id == 2
											  select compGroup);
				// Если есть хоть одна такая, выдираем данные
				if (competitiveGroupCollection.Count() > 0)
				{
					var currentCompetitiveGroup = competitiveGroupCollection.First();

					// Заполняем КЦП
					kcpExtrabudget = currentCompetitiveGroup.PlaceCount ?? 0;
					CalculateClaimCountInCompetitiveGroup(currentCompetitiveGroup,
						out countExtrabudgetFact, out countExtrabudgetOriginal);
				}

				// Выбрать конкурсные группы по льготе, направлению и форме обучения
				competitiveGroupCollection = (from compGroup in direction.CompetitiveGroups
											  where compGroup.EducationForm.Id == _educationForm.Id &&
												compGroup.FinanceSource.Id == 3 &&
												compGroup.Campaign.CampaignStatus.Id == 2
											  select compGroup);
				// Если есть хоть одна такая, выдираем данные
				if (competitiveGroupCollection.Count() > 0)
				{
					var currentCompetitiveGroup = competitiveGroupCollection.First();

					// Заполняем КЦП
					kcpQuota = currentCompetitiveGroup.PlaceCount ?? 0;
					CalculateClaimCountInCompetitiveGroup(currentCompetitiveGroup,
						out countQuotaFact, out countQuotaOriginal);
				}

				// Считаем итоговые значения
				kcpTotal = kcpBudget + kcpExtrabudget;
				countTotalFact = countBudgetFact + countExtrabudgetFact +
					countQuotaFact;
				countTotalOriginal = countBudgetOriginal + countExtrabudgetOriginal +
					countQuotaOriginal;

				// Совсем итоговые
				kcpBudgetTotal += kcpBudget;
				kcpExtrabudgetTotal += kcpExtrabudget;
				kcpQuotaTotal += kcpQuota;
				kcpTotalTotal += kcpTotal;
				countBudgetFactTotal += countBudgetFact;
				countBudgetOriginalTotal += countBudgetOriginal;
				countExtrabudgetFactTotal += countExtrabudgetFact;
				countExtrabudgetOriginalTotal += countExtrabudgetOriginal;
				countQuotaFactTotal += countQuotaFact;
				countQuotaOriginalTotal += countQuotaOriginal;
				countTotalFactTotal += countTotalFact;
				countTotalOriginalTotal += countTotalOriginal;


				/***************************************************************************
				 * ДАННЫЕ ЗАГРУЖЕНЫ, ЗАНИМАЕМСЯ ЗАГРУЗКОЙ В ДОКУМЕНТ
				 **************************************************************************/

				// Подготовка строки
				ws.Rows.InsertCopy(i, templateRow);
				var currentRow = ws.Rows[i];
				i++;

				// Вставляем данные
				currentRow.Cells[0].Value = directionName;
				currentRow.Cells[1].Value = kcpBudget;
				currentRow.Cells[2].Value = kcpQuota;
				currentRow.Cells[3].Value = kcpExtrabudget;
				currentRow.Cells[4].Value = kcpTotal;
				currentRow.Cells[5].Value = countBudgetFact;
				currentRow.Cells[6].Value = countBudgetOriginal;
				currentRow.Cells[7].Value = countQuotaFact;
				currentRow.Cells[8].Value = countQuotaOriginal;
				currentRow.Cells[9].Value = countExtrabudgetFact;
				currentRow.Cells[10].Value = countExtrabudgetOriginal;
				currentRow.Cells[11].Value = countTotalFact;
				currentRow.Cells[12].Value = countTotalOriginal;
			}

			// Вставляем самые итоговые итоги
			ws.Rows.InsertCopy(i, templateRow);
			var totalRow = ws.Rows[i];
			totalRow.Cells[0].Value = "ИТОГО";
			totalRow.Cells[1].Value = kcpBudgetTotal;
			totalRow.Cells[2].Value = kcpQuotaTotal;
			totalRow.Cells[3].Value = kcpExtrabudgetTotal;
			totalRow.Cells[4].Value = kcpTotalTotal;
			totalRow.Cells[5].Value = countBudgetFactTotal;
			totalRow.Cells[6].Value = countBudgetOriginalTotal;
			totalRow.Cells[7].Value = countQuotaFactTotal;
			totalRow.Cells[8].Value = countQuotaOriginalTotal;
			totalRow.Cells[9].Value = countExtrabudgetFactTotal;
			totalRow.Cells[10].Value = countExtrabudgetOriginalTotal;
			totalRow.Cells[11].Value = countTotalFactTotal;
			totalRow.Cells[12].Value = countTotalOriginalTotal;

			ef.Save(fileName);
        }

		/// <summary>
		/// Считает количество заявлений в конкурсной группе
		/// </summary>
		/// <param name="currentCompetitiveGroup">Конкурсная группа</param>
		/// <param name="countFact">Возвращаемое кол-во всех заявлений</param>
		/// <param name="countOriginal">Возвращаемое кол-во заявлений с оригиналами документов</param>
		void CalculateClaimCountInCompetitiveGroup(CompetitiveGroup currentCompetitiveGroup,
			out int countFact, out int countOriginal)
		{
			countFact = 0;
			countOriginal = 0;


			// Считаем количество заявлений в конкурсной группе
			// Все заявления
			// На сегодня
			if (_isTodayStatistic)
			{
				countFact = (from condition in currentCompetitiveGroup.ClaimConditions
								   where condition.Claim != null &&
									condition.Priority == 1 &&
									(condition.Claim.ClaimStatus.Id == 1 || condition.Claim.ClaimStatus.Id == 2 || condition.Claim.ClaimStatus.Id == 3) &&
									condition.Claim.RegistrationDate == DateTime.Now.Date
								   select condition.Claim).Count();
			}
			// Общее
			else
			{
				countFact = (from condition in currentCompetitiveGroup.ClaimConditions
								   where condition.Claim != null &&
									condition.Priority == 1 &&
									(condition.Claim.ClaimStatus.Id == 1 || condition.Claim.ClaimStatus.Id == 2 || condition.Claim.ClaimStatus.Id == 3)
								   select condition.Claim).Count();
			}

			// Оригиналы
			// На сегодня
			if (_isTodayStatistic)
			{
				countOriginal = (from condition in currentCompetitiveGroup.ClaimConditions
								   where condition.Claim != null &&
									condition.Priority == 1 &&
									(condition.Claim.ClaimStatus.Id == 1 || condition.Claim.ClaimStatus.Id == 2 || condition.Claim.ClaimStatus.Id == 3) &&
									condition.Claim.RegistrationDate == DateTime.Now.Date
								   select condition.Claim).Where(c => c.IsOriginal).Count();
			}

			// Общее
			else
			{
				countOriginal = (from condition in currentCompetitiveGroup.ClaimConditions
								   where condition.Claim != null &&
									condition.Priority == 1 &&
									(condition.Claim.ClaimStatus.Id == 1 || condition.Claim.ClaimStatus.Id == 2 || condition.Claim.ClaimStatus.Id == 3)
								   select condition.Claim).Where(c => c.IsOriginal).Count();
			}
		}

    }
}
