using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Model.Astu;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MoreLinq;


namespace Contingent.ViewModel.Workspaces.Students
{
    public class StudentViewViewModel: ViewModelBase
    {
        public StudentViewViewModel()
        {

        }

        public StudentViewViewModel(Student student)
        {
            Student = student;
        }

        #region Fields

        Student _student;

        #endregion

        #region Properties

        public Student Student
        {
            get
            {
                return _student;
            }
            set
            {
                _student = value;
                RaisePropertyChanged("Student");
            }
        }

        #endregion

        #region Logic


        #region Commands



        #endregion

        #region Methods



        #endregion

        #region Checks



        #endregion

        #endregion



        #region Additional collections

        public ObservableCollection<StudentStatus> StudentStatuses
        {
            get
            {
                return new ObservableCollection<StudentStatus>(Astu.StudentStatuses);
            }
        }


        public ObservableCollection<Citizenship> Citizenships
        {
            get
            {
                return new ObservableCollection<Citizenship>(Astu.Citizenships);
            }
        }

        public ObservableCollection<EducationDocumentType> GraduationDocumentTypes
        {
            get
            {
                return new ObservableCollection<EducationDocumentType>(Astu.EducationDocumentTypes);
            }
        }

        public ObservableCollection<string> IdentityDocumentOrganizations
        {
            get
            {
                var collection = new List<string>();
                var strings = Astu.Students.Select(s => s.IdentityDocumentOrganization).Distinct().OrderBy(s => s);
                foreach (var s in strings)
                {
                    collection.Add(s);
                }
                return new ObservableCollection<string>(collection);
            }
        }

        public ObservableCollection<ForeignLanguage> ForeignLanguages
        {
            get
            {
                return new ObservableCollection<ForeignLanguage>(Astu.ForeignLanguages);
            }
        }

        public ObservableCollection<Group> StudentGroups
        {
            get
            {
                return new ObservableCollection<Group>(Astu.Groups.OrderBy(g => g.Name).OrderBy(g => g.IsGraduated));
            }
        }

        public ObservableCollection<Faculty> Faculties
        {
            get
            {
                return new ObservableCollection<Faculty>(Astu.Faculties);
            }
        }

        public ObservableCollection<Direction> Directions
        {
            get
            {
                return new ObservableCollection<Direction>(Astu.Directions);
            }
        }

        public ObservableCollection<EducationForm> EducationForms
        {
            get
            {
                return new ObservableCollection<EducationForm>(Astu.EducationForms);
            }
        }

        public ObservableCollection<FinanceSource> FinanceSources
        {
            get
            {
                return new ObservableCollection<FinanceSource>(Astu.FinanceSources);
            }
        }

        public ObservableCollection<GrantType> GrantTypes

        {
            get
            {
                return new ObservableCollection<GrantType>(Astu.GrantTypes);
            }
        }

        #endregion


    }
}
