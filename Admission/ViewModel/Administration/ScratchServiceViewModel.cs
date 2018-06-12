using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using Microsoft.Win32;

namespace Admission.ViewModel.Administration
{
    public class ScratchServiceViewModel: ViewModelBase
    {
        bool canSave = false;

        public string FileName { get; set; }

        StringBuilder _log = new StringBuilder();

        public string Log
        {
            get
            {
                return _log.ToString();
            }
            set
            {
                _log.AppendLine(value);
                RaisePropertyChanged("Log");
            }
        }

        public RelayCommand OpenFileCommand { get => new RelayCommand(OpenFile); }

        public RelayCommand ImportCommand { get => new RelayCommand(Import, CanImport); }

        public RelayCommand SaveCommand { get => new RelayCommand(Save, CanSave); }

        void OpenFile()
        {
            var openDialog = new OpenFileDialog
            {
                Filter = "Файлы XML (*.xml)|*.xml" + "|Все файлы (*.*)|*.* ",
                CheckFileExists = true,
                Multiselect = false
            };

            if (openDialog.ShowDialog() ?? false)
            {
                FileName = openDialog.FileName;
            }
            canSave = false;
            RaisePropertyChanged("FileName");
            RaisePropertyChanged("ImportCommand");
            RaisePropertyChanged("SaveCommand");
        }

        void Import()
        {
            _log.Clear();
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(FileName);

            var compGroups = xmlDoc.SelectSingleNode("Root")
                .SelectSingleNode("PackageData")
                .SelectSingleNode("AdmissionInfo")
                .SelectSingleNode("CompetitiveGroups")
                .SelectNodes("CompetitiveGroup");

            if (compGroups.Count == 0)
            {
                Log = "Не найдено ни одного узла";
            }
            else
            {
                Log = string.Format("Найдено {0} конкурсных групп.", compGroups.Count);
            }

            foreach (XmlNode node in compGroups)
            {
                var group = new CompetitiveGroup();
                group.CampaignId = int.Parse(node.SelectSingleNode("CampaignUID")?.InnerText);
                group.Name = node.SelectSingleNode("Name")?.InnerText;
                group.ExportCode = node.SelectSingleNode("UID")?.InnerText;
                string exportCode = node.SelectSingleNode("EducationLevelID").InnerText;
                group.EducationLevelId = Session.DataModel.EducationLevels.FirstOrDefault(el => el.ExportCode == exportCode)?.Id;
                exportCode = node.SelectSingleNode("EducationFormID").InnerText;
                group.EducationFormId = Session.DataModel.EducationForms.FirstOrDefault(el => el.ExportCode == exportCode)?.Id;
                exportCode = node.SelectSingleNode("EducationSourceID").InnerText;
                group.FinanceSourceId = Session.DataModel.FinanceSources.FirstOrDefault(el => el.ExportCode == exportCode)?.Id;
                exportCode = node.SelectSingleNode("DirectionID").InnerText;
                group.DirectionId = Session.DataModel.Directions.FirstOrDefault(el => el.ExportCode == exportCode)?.Id;
                group.RegistrationNumberMemberPart = "1";
                group.EducationProgramTypeId = 1;
                int placeCount = int.Parse(node.SelectSingleNode("CompetitiveGroupItem").SelectSingleNode("Number").InnerText);
                group.PlaceCount = placeCount;
                Session.DataModel.CompetitiveGroups.Add(group);
                Log = string.Format("Добавлена КГ: {0}.", group.Name);
            }
            canSave = true;
            RaisePropertyChanged("SaveCommand");
        }

        bool CanImport()
        {
            return !(string.IsNullOrWhiteSpace(FileName));
        }

        void Save()
        {
            Session.DataModel.SaveChanges();
        }

        bool CanSave()
        {
            return canSave;
        }

    }
}
