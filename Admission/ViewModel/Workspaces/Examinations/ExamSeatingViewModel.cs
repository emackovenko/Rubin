using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using Admission.ViewModel.Documents;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Admission;


namespace Admission.ViewModel.Workspaces.Examinations
{
    [Magic]
    public class ExamSeatingViewModel: ViewModelBase
    {
        public ExamSeatingViewModel()
        {
            // Рассадку делаем только на текущую СЕРВЕРНУЮ дату. Сделано так, чоб не было желания ни у кого
            // узнать чей-то вариант заранее (нехуй было так нагло продавать ответы, дешевые вы проститутки)
            _examinationDate = Session.DataModel.Database.SqlQuery<DateTime>("SELECT CURDATE()").SingleOrDefault();
            Classroom = "213";
            LinesCount = 6;
            PlacesCount = 10;
            VariantsCount = 5;
        }


        public ExaminationRegister ExaminationRegister { get; set; }

        public EducationForm EducationForm { get; set; }

        public IEnumerable<EducationForm> EducationForms { get => Session.DataModel.EducationForms.ToList(); }

        public FinanceSource FinanceSource { get; set; }

        public IEnumerable<FinanceSource> FinanceSources { get => Session.DataModel.FinanceSources.ToList(); }

        private ExamSubject _subject;

        [NoMagic]
        public ExamSubject Subject
        {
            get
            {
                if (_subject == null && Subjects.Count() > 0)
                {
                    _subject = Subjects.FirstOrDefault();
                }
                return _subject;
            }
            set
            {
                _subject = value;
                RaisePropertyChanged("Subject");
            }
        }

        public IEnumerable<ExamSubject> Subjects
        {
            get => Session.DataModel.ExamSubjects.ToList();
        }

        DateTime _examinationDate;

        public string Classroom { get; set; }

        public int LinesCount { get; set; }

        public int PlacesCount { get; set; }

        public int VariantsCount { get; set; }

        public RelayCommand ResetFilterCommand { get => new RelayCommand(ResetFilter); }

        void ResetFilter()
        {
            EducationForm = null;
            FinanceSource = null;
            Subject = null;
        }

        public RelayCommand GenerateSeatingCommand
        {
            get => new RelayCommand(GenerateSeating);
        }

        void GenerateSeating()
        {
            bool check = true;

            if (_subject == null)
            {
                check = false;
            }

            if (string.IsNullOrWhiteSpace(Classroom) || LinesCount <= 0 || PlacesCount <= 0 || VariantsCount <= 0)
            {
                check = false;
            }

            if (!check)
            {
                MessageBox.Show("Вы не заполнили необходимые поля. Пожалуйста, попробуйте еще раз.");
                return;
            }
            

            if (Session.DataModel.EntranceTests.Where(et => et.ExaminationDate == _examinationDate && et.ExamSubjectId == Subject.Id).Count() == 0)
            {
                MessageBox.Show("Сегодня нет экзаменов по данному предмету. Пожалуйста, проверьте, верно ли вы указали предмет.");
            }

            ExaminationRegister = new ExaminationRegister(Subject, _examinationDate,
                Classroom, LinesCount, PlacesCount, VariantsCount, EducationForm, FinanceSource);

        }
    }
}
