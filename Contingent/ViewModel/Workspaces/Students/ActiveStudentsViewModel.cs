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
    public class ActiveStudentsViewModel: ViewModelBase
    {
        #region Fields

        ObservableCollection<Student> _students;

        #endregion


        #region Properties

        public ObservableCollection<Student> Students
        {
            get
            {
                if (_students == null)
                {
                    _students = new ObservableCollection<Student>(Astu.Students.OrderBy(s => s.Group.Name));
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


        #endregion
    }
}
