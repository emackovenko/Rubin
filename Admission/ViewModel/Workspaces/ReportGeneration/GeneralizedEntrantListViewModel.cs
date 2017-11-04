using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Admission.ViewModel.Documents;
using Model.Admission;
using System.Threading;

namespace Admission.ViewModel.Workspaces.ReportGeneration
{
    public class GeneralizedEntrantListViewModel: ViewModelBase
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

        GeneralizedEntrantList _doc;

        public GeneralizedEntrantList Document
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
            Document = new GeneralizedEntrantList(SelectedEducationForm);
            LogService.WriteLog("Сгенерирован Обобщенный список поступающих.");
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
