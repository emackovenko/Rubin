using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Admission;
using Admission.ViewModel.Documents;

namespace Admission.ViewModel.Workspaces.ReportGeneration
{
    public class AdmissionStatisticViewModel: ViewModelBase
    {

        #region Сущности

        EducationForm _selectedEducationForm;

        public EducationForm SelectedEducationForm
        {
            get
            {
                return _selectedEducationForm;
            }
            set
            {
                _selectedEducationForm = value;
                RaisePropertyChanged("EducationForm");
            }
        }

        bool _isTodayStatistic = false;

        public bool IsTodayStatistic
        {
            get
            {
                return _isTodayStatistic;
            }
            set
            {
                _isTodayStatistic = value;
                RaisePropertyChanged("IsTodayStatistic");
            }
        }

        AdmissionStatistic _doc;

        public AdmissionStatistic Document
        {
            get
            {
                return _doc;
            }
            set
            {
                _doc = value;
                RaisePropertyChanged("Document");
            }
        }

        #endregion

        #region Вспомогательные коллекции

        public ObservableCollection<EducationForm> EducationForms
        {
            get
            {
                return new ObservableCollection<EducationForm>(Session.DataModel.EducationForms);
            }
        }

        #endregion

        #region Логика

        #region Команды 

        public RelayCommand GenerateDocumentCommand
        {
            get
            {
                return new RelayCommand(GenerateDocument, CanGenerateDocument);
            }
        }

        #endregion

        #region Методы

        void GenerateDocument()
        {
            var thread = new Thread(new ThreadStart(RunGenerateDocument));
            thread.Start();
        }

        void RunGenerateDocument()
        {
            Document = null;
            Document = new AdmissionStatistic(SelectedEducationForm, IsTodayStatistic);
            LogService.WriteLog("Сгенерирован статистика приёма.");
        }

        #endregion

        #region Проверки

        bool CanGenerateDocument()
        {
            return SelectedEducationForm != null;
        }

        #endregion

        #endregion
    }
}
