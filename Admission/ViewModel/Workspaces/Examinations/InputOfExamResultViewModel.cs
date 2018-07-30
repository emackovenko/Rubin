using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Admission;


namespace Admission.ViewModel.Workspaces.Examinations
{
    class InputOfExamResultViewModel: ViewModelBase
    {
        public InputOfExamResultViewModel()
        {
            ExaminationDate = DateTime.Now;
        }
        #region Переменные
        public DateTime ExaminationDate { get; set; }

        public ExamSubject SelectedSubject { get; set; }

        public ObservableCollection<ExamSubject> Subjects
        {
            get
            {
                return new ObservableCollection<ExamSubject>(Session.DataModel.ExamSubjects);
            }
        }

        List<EntranceTestResult> _examList;

        public List<EntranceTestResult> ExamList
        {
            get
            {

                return _examList;
            }
            set
            {
                _examList = value;
                RaisePropertyChanged("ExamList");
            }
        }


       

       

        #endregion

        #region Команды

        #region Commands

        public RelayCommand FormExamListCommand { get => new RelayCommand(FormExamList, CanFormExamList); }

        public RelayCommand SaveEntranceTestResultCommand { get => new RelayCommand(SaveEntranceTestResult, CanFormExamList); }

        public RelayCommand ResetCommand { get => new RelayCommand(Reset, CanFormExamList); }

        #endregion

        #region Methods

        void FormExamList()
        {
            // загружаем список экзаменов на активные кампании и выбираем нужные нам по фильтру 
            var exams = Session.DataModel.EntranceTestResults 
                .Where(et => et.EntranceTest.Campaign.CampaignStatusId == 2)
                .Where(et => et.EntranceTest.ExamSubjectId == SelectedSubject.Id)
                .Where(et => et.EntranceTest.ExaminationDate == ExaminationDate)
                .ToList();

            //Сортируем
            exams = exams.OrderBy(et => et.Claim.Person.FullName).ToList();

            //Загружаем их в таблицу
            ExamList = exams;
     
        }
        
        void SaveEntranceTestResult()
        {
            Session.DataModel.SaveChanges();     
            System.Windows.MessageBox.Show("Сохранено.");
        }

        void Reset()
        {
            //Перезагружаем таблицу из базы
            // ExamList.ForEach(Session.RefreshEntityItem);

            foreach (var entry in Session.DataModel.ChangeTracker.Entries())
            {
                if (entry.State == System.Data.Entity.EntityState.Modified)
                    entry.State = System.Data.Entity.EntityState.Unchanged; 
            }

            FormExamList();
            System.Windows.MessageBox.Show("Сброс выполнен.");
        }

       

        #endregion

        #region Checks
        bool CanFormExamList()
        {
            return ((SelectedSubject != null) && (ExaminationDate != null));
        }
        #endregion
        #endregion
    }
}
