using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Admission;
using Admission.ViewModel.Documents;

namespace Admission.ViewModel.Workspaces.ReportGeneration
{
    public class AdmissionDynamicViewModel: ViewModelBase
    {
        #region Сущности

        DateTime _selectedDate;

        public DateTime SelectedDate
        {
            get
            {
                return _selectedDate;
                //return System.DateTime.Now;
            }
            set
            {
                _selectedDate = value;
                RaisePropertyChanged("SelectedDate");
            }
        }


        AdmissionDynamic _doc;

        public AdmissionDynamic Document
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

        #region Логика

        public AdmissionDynamicViewModel()
        {
            _selectedDate = System.DateTime.Now;
        }

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
            Document = new AdmissionDynamic(SelectedDate);
            LogService.WriteLog("Сгенерирован динамика приёма.");
        }

        #endregion

        #region Проверки

        bool CanGenerateDocument()
        {
            return SelectedDate != null;
        }

        #endregion

        #endregion
    }
}
