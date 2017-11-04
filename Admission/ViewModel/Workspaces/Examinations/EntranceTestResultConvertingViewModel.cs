using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Threading;

namespace Admission.ViewModel.Workspaces.Examinations
{
    public class EntranceTestResultConvertingViewModel: ViewModelBase
    {
        #region Обрабатываемые сущности

        ExamSubject _selectedExamSubject;

        public ExamSubject SelectedExamSubject
        {
            get
            {
                return _selectedExamSubject;
            }
            set
            {
                _selectedExamSubject = value;
                RaisePropertyChanged("SelectedExamSubject");
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

        #region Вспомогательные коллекции

        ObservableCollection<ExamSubject> _examSubjects;

        public ObservableCollection<ExamSubject> ExamSubjects
        {
            get
            {
                if (_examSubjects == null)
                {
                    _examSubjects = new ObservableCollection<ExamSubject>(Session.DataModel.ExamSubjects.OrderBy(s => s.Name));
                }
                return _examSubjects;
            }
            set
            {
                _examSubjects = value;
                RaisePropertyChanged("ExamSubjects");
            }
        }

        #endregion

        #region Логика

        #region Команды

        public RelayCommand ConvertPrimaryMarkToExamResultCommand
        {
            get
            {
                return new RelayCommand(ConvertPrimaryMarkToExamResult, ConvertPrimaryMarkToExamResultCanExecute);
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Выполняет конвертирование баллов абитуриентов в отдельном потоке
        /// </summary>
        void ConvertPrimaryMarkToExamResult()
        {
            var thread = new Thread(new ThreadStart(Convert));
            thread.Start();
        }

        /// <summary>
        /// Переводит первичные баллы внутренних вступительных испытаний абитуриентов в основные баллы
        /// </summary>
        void Convert()
        {
            //  Пишем в лог количество название предмета
            LogText = string.Format("{0} - Выбранный предмет: {1}\n",
                DateTime.Now.ToString("hh:mm:ss"), _selectedExamSubject.Name);

            //  Получаем коллекцию результатов вступительных испытаний по условиям:
            // - предмет равен указанному предмету
            // - первичный балл больше основного
            var convertingResults = (from result in Session.DataModel.EntranceTestResults
                                     where result.EntranceTest.ExamSubject.Id == _selectedExamSubject.Id &&
                                     result.PrimaryMark > result.Value && result.Claim != null
                                     orderby result.Claim.Number
                                     select result);

            //  Пишем в лог количество конвертируемых элементов
            LogText += string.Format("\n{0} - Количество найденных результатов вступительных испытаний для конвертирования: {1}\n",
                DateTime.Now.ToString("hh:mm:ss"), convertingResults.Count());

            //  Для каждого результата выставляем основной балл равный первичному и обнуляем первичный
            foreach (var result in convertingResults)
            {
                result.Value = result.PrimaryMark;
                result.PrimaryMark = 0;

                // Пишем в отчет сведения о заявлении и конвертированном балле
                LogText += string.Format("\n{0} - В заявлении №{1} указан балл {2}",
                    DateTime.Now.ToString("hh:mm:ss"), result.Claim.Number, result.Value);
            }

            // Сохраняем данные в БД
            Session.DataModel.SaveChanges();

            // Пишем в отчет, что все сделано
            LogText += string.Format("\n\n{0} - Готово.", DateTime.Now.ToString("hh:mm:ss"));

            string str = "Конвертирование первичных баллов в основные.\n\nВывод операции:\n";
            str += LogText;
            LogService.WriteLog(str);
        }

        #endregion

        #region Проверки

        bool ConvertPrimaryMarkToExamResultCanExecute()
        {
            return SelectedExamSubject != null;
        }

        #endregion

        #endregion
    }
}
