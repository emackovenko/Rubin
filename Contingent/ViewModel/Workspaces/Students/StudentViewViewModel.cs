using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Model.Astu;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MoreLinq;
using Contingent.DialogService;


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

        public RelayCommand<Entity> RemoveChildEntityCommand
        {
            get
            {
                return new RelayCommand<Entity>(RemoveChildEntity, EditChildEntityCanExecute);
            }
        }

        public RelayCommand<Entity> EditChildEntityCommand
        {
            get
            {
                return new RelayCommand<Entity>(EditChildEntity, EditChildEntityCanExecute);
            }
        }
        
        public RelayCommand AddEnrollmentOrderCommand
        {
            get
            {
                return new RelayCommand(AddEnrollmentOrder, AddChildEntityCanExecute);
            }
        }
        
        public RelayCommand AddEnrollmentByUniversityTransferOrderCommand
        {
            get
            {
                return new RelayCommand(AddEnrollmentByUniversityTransferOrder, AddChildEntityCanExecute);
            }
        }

        public RelayCommand AddIdentityDocumentCommand
        {
            get
            {
                return new RelayCommand(AddIdentityDocument, AddChildEntityCanExecute);
            }
        }

        #endregion

        #region Methods

        void RemoveChildEntity(Entity entity)
        {
            if (Messenger.RemoveQuestion())
            {
                entity.Delete();
                entity.Save();
                RaisePropertyChanged("Student");
            }
        }

        void EditChildEntity(Entity entity)
        {
            ViewInvoker.ShowEditor(entity);
            RaisePropertyChanged("Student");
        }

        void AddIdentityDocument()
        {
            var doc = new IdentityDocument
            {
                StudentId = Student.Id,
                FirstName = Student.FirstName,
                LastName = Student.LastName,
                Patronimyc = Student.Patronimyc,
                BirthDate = Student.BirthDate,
                Gender = Student.Gender,
                CitizenshipId = Student.CitizenshipId
            };
            if (ViewInvoker.ShowEditor(doc))
            {
                Astu.IdentityDocuments.Add(doc);
            }
            RaisePropertyChanged("Student");
        }

        void AddEnrollmentOrder()
        {
            var order = new EnrollmentOrder()
            {
                StudentId = Student.Id,
                FacultyId = Student.FacultyId
            };

            if (ViewInvoker.ShowEditor(order))
            {
                Astu.EnrollmentOrders.Add(order);
                order.Save();
            }
            RaisePropertyChanged("Student");
        }

        void AddEnrollmentByUniversityTransferOrder()
        {
            var order = new EnrollmentByUniversityTransferOrder()
            {
                StudentId = Student.Id,
                FacultyId = Student.FacultyId
            };

            if (ViewInvoker.ShowEditorWithoutSaving(order))
            {
                Astu.EnrollmentByUniversityTransferOrders.Add(order);
            }
            RaisePropertyChanged("Student");
        }

        #endregion

        #region Checks

        bool AddChildEntityCanExecute()
        {
            return Student.EntityState != EntityState.New;
        }

        bool EditChildEntityCanExecute(Entity entity)
        {
            return entity != null;
        }

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
