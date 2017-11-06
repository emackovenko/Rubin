using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Model.Astu;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;


namespace Contingent.ViewModel.Workspaces.Students
{
    public class StudentViewViewModel: ViewModelBase
    {
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

        #endregion


    }
}
