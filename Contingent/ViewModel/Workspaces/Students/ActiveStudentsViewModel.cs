using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Model.Astu;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Contingent.DialogService;

namespace Contingent.ViewModel.Workspaces.Students
{
    public class ActiveStudentsViewModel: ViewModelBase
    {
        #region Fields

        Student _selectedStudent;

        ObservableCollection<Student> _students;

        #endregion


        #region Properties

        public Student SelectedStudent
        {
            get
            {
                if (_selectedStudent == null)
                {
                    _selectedStudent = Students.FirstOrDefault();
                }
                return _selectedStudent;
            }
            set
            {
                _selectedStudent = value;
                RaisePropertyChanged("SelectedStudent");
            }
        }

        public ObservableCollection<Student> Students
        {
            get
            {
                if (_students == null)
                {
                    var activeStatuses = new string[] {"0001", "0002", "0007", "0008"};
                    var students = Astu.Students.OrderBy(s => s.Course).OrderBy(s => s.Name).OrderBy(s => s.Group?.Name);
                    _students = new ObservableCollection<Student>(students);
                }
                return _students;
            }
            set
            {
                _students = value;
                RaisePropertyChanged("Students");
            }
        }

        #endregion


        #region Logic

        #region Commands

        public RelayCommand EditStudentCommand
        {
            get
            {
                return new RelayCommand(EditStudent, EditStudentCanExecute);
            }
        }

        #endregion

        #region Methods

        void EditStudent()
        {
            EditorInvoker.ShowEditor(SelectedStudent);
        }

        #endregion

        #region Checks

        bool EditStudentCanExecute()
        {
            return SelectedStudent != null;
        }

        #endregion

        #endregion
    }
}
