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
                    var activeStatuses = new string[] {"0001", "0002", "0007", "0008"};
                    var students = Astu.Students.Where(s => activeStatuses.Contains(s.StatusId));
                    students = students.OrderBy(s => s.Course).OrderBy(s => s.Name).OrderBy(s => s.Group?.Name);
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


        #endregion
    }
}
