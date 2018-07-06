using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Admission.ViewModel.Documents;
using CommonMethods.Documents;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Admission;

namespace Admission.ViewModel.Workspaces.Examinations
{
    public class ExaminatedEntrantListViewModel: ViewModelBase
    {

        public ExaminatedEntrantListViewModel()
        {
            ExaminationDate = DateTime.Now;
        }

        public DateTime ExaminationDate { get; set; }

        public ExamSubject SelectedSubject { get; set; }

        public ObservableCollection<ExamSubject> Subjects
        {
            get
            {
                return new ObservableCollection<ExamSubject>(Session.DataModel.ExamSubjects);
            }
        }

        public OpenXmlDocument Document { get; set; }


        public RelayCommand FormExamListCommand { get => new RelayCommand(FormExamList, CanFormExamList); }

        void FormExamList()
        {
            // загружаем список экзаменов на активные кампании и выбираем нужные нам по фильтру
            var exams = Session.DataModel.EntranceTests
                .Where(et => et.Campaign.CampaignStatusId == 2)
                .Where(et => et.ExamSubjectId == SelectedSubject.Id)
                .Where(et => et.ExaminationDate == ExaminationDate)
                .ToList();

            // создаем документ
            Document = new ExaminatedEntrantList(exams);
            RaisePropertyChanged("Document");
        }
        
        bool CanFormExamList()
        {
            return ((SelectedSubject != null) && (ExaminationDate != null));
        }

    }
}
