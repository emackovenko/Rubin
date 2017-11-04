using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Admission;
using System.IO;
using Admission.DialogService;
using System.Windows.Forms;
using System.Threading;

namespace Admission.ViewModel.Workspaces.Examinations
{
	public class EgeResultCheckingViewModel: ViewModelBase
	{

		#region Обрабатываемые сущности

		const char SPLIT_SYMBOL = '%';

		string _csvFileName;

		public string CsvFileName
		{
			get
			{
				return _csvFileName;
			}
			set
			{
				_csvFileName = value;
				RaisePropertyChanged("CsvFileName");
			}
		}

		string _logText;

		public string LogText
		{
			get
			{
				return _logText;
			}
			set
			{
				_logText = value;
				RaisePropertyChanged("LogText");
			}
		}

		#endregion

		#region Структуры, типы данных, внутренние классы

		public struct EgeCheckingResult
		{
			public string EntrantLastName { get; set; }
			public string EntrantFirstName { get; set; }
			public string EntrantPatronymic { get; set; }
			public string Series { get; set; }
			public string Number { get; set; }
			public string ExamSubjectName { get; set; }
			public int Value { get; set; }
			public int Year { get; set; }
			public string Status { get; set; }
		}

		#endregion

		#region Логика

		#region Команды

		public RelayCommand ShowOpenFileDialogCommand
		{
			get
			{
				return new RelayCommand(ShowOpenFileDialog);
			}
		}

		public RelayCommand ApplyCheckingResultCommand
		{
			get
			{
				return new RelayCommand(ApplyCheckingResult, ApplyCheckingResultCanExecute);
			}
		}

		#endregion

		#region Методы

		void ShowOpenFileDialog()
		{
			var dlg = new OpenFileDialog();
			dlg.Filter = "CSV файлы|*.csv";
			dlg.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
			dlg.ShowDialog();
			CsvFileName = dlg.FileName;
		}

        /// <summary>
        /// Выполняет и применяет проверку в отдельном потоке
        /// </summary>
		void ApplyCheckingResult()
		{
			RunCheckingResult();
		}

		/// <summary>
		/// Выставляет абитуриентам проверенные результаты
		/// </summary>
		void RunCheckingResult()
		{
            // счетчики
            int changedCount = 0, acceptCount = 0, skipedCount = 0, invalidCount = 0, notFoundCount = 0,
                uncheckedCount = 0;

			// Загружаем результаты в коллекцию и выводим их кол-во
			var results = LoadEgeCheckingResultsFromFile();

			// Считаем кол-во проверяемых абитуриентов
			var entrantCount = (from result in results
								select string.Format("{0}{1}{2}{3}{4}",
								result.EntrantLastName, result.EntrantFirstName, result.EntrantPatronymic,
								result.Series, result.Number)).Distinct().Count();

			LogText = string.Format("Количество абитуриентов в файле: {0}.", entrantCount);
			LogText += string.Format("\nНайдено проверенных результатов: {0}. \nИз них:", results.Count());

			// - действительные результаты
			var validResults = (from result in results
								where result.Status == "Действующий"
								orderby result.EntrantLastName, result.EntrantFirstName, result.EntrantPatronymic
								select result);

			LogText += string.Format("\n\t- действительных результатов: {0}.", validResults.Count());

			// - недействительные результаты
			var invalidResults = (from result in results
								where result.Status != "Действующий"
								orderby result.EntrantLastName, result.EntrantFirstName, result.EntrantPatronymic
								select result);

			LogText += string.Format("\n\t- недействительных результатов: {0}.\n\n\n", invalidResults.Count());

            // Поиск по дествительным:
            LogText += string.Format("\n\n\nПоиск по действительным результатам:\n");

            foreach (var res in validResults)
			{
				// Находим заявление
				Claim claim = null;
				var claimCollection = (from verifiableEntrant in Session.DataModel.Entrants
									   where verifiableEntrant.LastName.ToLower() == res.EntrantLastName.ToLower() &&
									   verifiableEntrant.FirstName.ToLower() == res.EntrantFirstName.ToLower() &&
									   verifiableEntrant.Patronymic.ToLower() == res.EntrantPatronymic.ToLower()
									   select verifiableEntrant.Claim).Where(c => c != null).ToList();

				foreach (var cl in claimCollection)
				{
					foreach (var id in cl.IdentityDocuments)
					{
						if (id.Series == res.Series && id.Number == res.Number)
						{
							claim = cl;
						}
					}
				}

				if (claim == null)
				{
					claim = (from verifiableEntrant in Session.DataModel.Entrants
							 where verifiableEntrant.LastName.ToLower() == res.EntrantLastName.ToLower() &&
							 verifiableEntrant.FirstName.ToLower() == res.EntrantFirstName.ToLower() &&
							 verifiableEntrant.Patronymic.ToLower() == res.EntrantPatronymic.ToLower() &&
							 verifiableEntrant.Claim.IdentityDocuments.FirstOrDefault().Series == res.Series &&
							 verifiableEntrant.Claim.IdentityDocuments.FirstOrDefault().Number == res.Number
							 select verifiableEntrant.Claim).FirstOrDefault();
				}
				
				// Если не найдено, то пишем об этом в лог, переходим к следующему
				if (claim == null)
				{
					LogText += string.Format("\n\nПРОПУСК\t{0} {1} {2} не был найден базе данных.", 
						res.EntrantLastName, res.EntrantFirstName, res.EntrantPatronymic);
                    skipedCount++;
					continue;
				}

				// Получаем Ф.И.О. абитуриента и его самого для удобства
				var entrant = claim.Entrants.First();
				var entrantName = string.Format("{0} {1} {2}", entrant.LastName, entrant.FirstName, entrant.Patronymic); 

				// Ищем свидетельство ЕГЭ и по нему находим экзамен
				var egeDoc = (from doc in claim.EgeDocuments
							  where doc.Year == res.Year
							  select doc).FirstOrDefault();
				if (egeDoc == null)
				{
					LogText += string.Format("\n\nПРОПУСК\tУ заявления №{0} ({1}) не найдено свидетельств о результатх ЕГЭ за {2} год.",
						claim.Number, entrantName, res.Year);
                    skipedCount++;
					continue;
				}

				var exam = (from egeRes in egeDoc.EgeResults
							where egeRes.ExamSubject.Name.ToLower() == res.ExamSubjectName.ToLower()
							select egeRes).FirstOrDefault();
				if (exam == null)
				{
					LogText += string.Format("\n\nПРОПУСК\tВ заявлении №{0} ({1}) у свидетельства о результатах ЕГЭ за {2} год на найдено резльтата по предмету \"{3}\".",
						claim.Number, entrantName, egeDoc.Year, res.ExamSubjectName);
                    skipedCount++;
					continue;
				}

				// Если результат в БД и в проверке соответствует, то выводим сообщение о верности и помечаем результат как проверенный. 
				if (exam.Value == res.Value)
				{
					LogText += string.Format("\n\nСООТВЕТСТВИЕ\tВ заявлении №{0} ({1}) по предмету \"{2}\" балл «{3}» соответствует проверенному.",
						claim.Number, entrantName, exam.ExamSubject.Name, exam.Value);
					exam.IsChecked = true;
                    acceptCount++;
				}
				// Иначе выводим сообщение об изменении и изменяем значение, помечаем как проверенный.
				else
				{
					LogText += string.Format("\n\nИЗМЕНЕНИЕ\tВ заявлении №{0} ({1}) по предмету \"{2}\" балл «{3}» изменен на «{4}».",
						claim.Number, entrantName, exam.ExamSubject.Name, exam.Value, res.Value);
					exam.Value = res.Value;
					exam.IsChecked = true;
                    changedCount++;
				}
			}

            // обработка недействительных
            // Сначала по тем, у кого статус не равен "Не найдено"
            LogText += string.Format("\n\n\nПоиск по недействительным:");

            foreach (var res in invalidResults.Where(c => c.Status != "Не найдено"))
            {
                // Находим заявление
                var claim = (from verifiableEntrant in Session.DataModel.Entrants
                             where verifiableEntrant.LastName.ToLower() == res.EntrantLastName.ToLower() &&
                             verifiableEntrant.FirstName.ToLower() == res.EntrantFirstName.ToLower() &&
                             verifiableEntrant.Patronymic.ToLower() == res.EntrantPatronymic.ToLower() &&
                             verifiableEntrant.Claim.IdentityDocuments.FirstOrDefault().Series == res.Series &&
                             verifiableEntrant.Claim.IdentityDocuments.FirstOrDefault().Number == res.Number
                             select verifiableEntrant.Claim).FirstOrDefault();

                // Если не найдено, то пишем об этом в лог, переходим к следующему
                if (claim == null)
                {
                    LogText += string.Format("\n\nПРОПУСК\t{0} {1} {2} не был найден базе данных.",
                        res.EntrantLastName, res.EntrantFirstName, res.EntrantPatronymic);
                    skipedCount++;
                    continue;
                }

                // Получаем Ф.И.О. абитуриента и его самого для удобства
                var entrant = claim.Entrants.First();
                var entrantName = string.Format("{0} {1} {2}", entrant.LastName, entrant.FirstName, entrant.Patronymic);

                // Пишем сообщение в лог о невалидности результата
                LogText += string.Format("\n\nОШИБКА\tВ заявлении №{0} ({1}) результат проверки по предмету \"{2}\" имеет статус \"{3}\".",
                    claim.Number, entrantName, res.ExamSubjectName, res.Status);
                invalidCount++;
            }

            // Дальше по тем невалидным, у кого статус "Не найдено"
            foreach (var res in invalidResults.Where(r => r.Status == "Не найдено"))
            {
                // Находим заявление
                var claim = (from verifiableEntrant in Session.DataModel.Entrants
                             where verifiableEntrant.LastName.ToLower() == res.EntrantLastName.ToLower() &&
                             verifiableEntrant.FirstName.ToLower() == res.EntrantFirstName.ToLower() &&
                             verifiableEntrant.Patronymic.ToLower() == res.EntrantPatronymic.ToLower() &&
                             verifiableEntrant.Claim.IdentityDocuments.FirstOrDefault().Series == res.Series &&
                             verifiableEntrant.Claim.IdentityDocuments.FirstOrDefault().Number == res.Number
                             select verifiableEntrant.Claim).FirstOrDefault();

                // Если не найдено, то пишем об этом в лог, переходим к следующему
                if (claim == null)
                {
                    LogText += string.Format("\n\nПРОПУСК\t{0} {1} {2} не был найден базе данных.",
                        res.EntrantLastName, res.EntrantFirstName, res.EntrantPatronymic);
                    skipedCount++;
                    continue;
                }

                // Получаем Ф.И.О. абитуриента и его самого для удобства
                var entrant = claim.Entrants.First();
                var entrantName = string.Format("{0} {1} {2}", entrant.LastName, entrant.FirstName, entrant.Patronymic);

                // Пишем сообщение в лог о невалидности результата
                LogText += string.Format("\n\nНЕ НАЙДЕНО\tВ заявлении №{0} ({1}) не найдено результатов в базе данных \"ФИС ЕГЭ и Приёма\". Необходимы дополнительные данные о серии и номере паспорта, выданного ранее.",
                    claim.Number, entrantName);
                notFoundCount++;
            }

            // Подведение итогов и сохранение данных.
            LogText += "\n\n\n\n\n\nИТОГИ";
            Session.DataModel.SaveChanges();

            uncheckedCount = (from result in Session.DataModel.EgeResults
                                  where result.EgeDocument.Claim != null &&
                                  result.IsChecked != true
                                  select result).Count();

            LogText += string.Format("\nНайдено соответствий значений в базе данных: {0}.", acceptCount);
            LogText += string.Format("\nПропущено результатов: {0}.", skipedCount);
            LogText += string.Format("\nИзменено значений в базе данных: {0}.", changedCount);
            LogText += string.Format("\nНедействительных результатов: {0}.", invalidCount);
            LogText += string.Format("\nНе найдено абитуриентов в базе данных \"ФИС ЕГЭ и Приёма\": {0}.", notFoundCount);
            LogText += string.Format("\nОсталось непроверенных результатов в базе данных приёмной комиссии: {0}.", uncheckedCount);

            // сохраняем лог
			string str = "Применение результатов проверки ЕГЭ.\n\nТекст проверки:\n";
			str += LogText;
			LogService.WriteLog(str);
		}

		/// <summary>
		/// Возвращает структурированные данные из файла
		/// </summary>
		IEnumerable<EgeCheckingResult> LoadEgeCheckingResultsFromFile()
		{
			//  Создаем возвращаемую коллекцию
			var resultItems = new List<EgeCheckingResult>();

			//  Считываем файл в массив
			string[] fileStrings = File.ReadAllLines(CsvFileName, Encoding.UTF8);

			//  Для каждой строки:
			foreach (var item in fileStrings)
			{
				//  Разделяем строку по символу разделителя
				string [] str = item.Split(SPLIT_SYMBOL);

				//  Создаем новый элемент возвращаемой коллекции
				var result = new EgeCheckingResult
				{
					EntrantLastName = str[0],
					EntrantFirstName = str[1],
					EntrantPatronymic = str[2],
					Series = str[3],
					Number = str[4],
					ExamSubjectName = str[5],
					Value = int.Parse(str[6]),
					Year = int.Parse(str[7]),
					Status = str[9]					
				};
				resultItems.Add(result);
			}

			//  Возвращаем коллекцию
			return resultItems;
		}

		#endregion

		#region Проверки

		bool ApplyCheckingResultCanExecute()
		{
			return File.Exists(CsvFileName);
		}


		#endregion

		#endregion
	}
}
