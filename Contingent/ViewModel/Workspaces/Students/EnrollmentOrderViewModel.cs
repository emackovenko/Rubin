using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Model.Astu;
using Contingent.DialogService;
using GalaSoft.MvvmLight;

namespace Contingent.ViewModel.Workspaces.Students
{
    public class EnrollmentOrderViewModel : ViewModelBase
    {
        public EnrollmentOrderViewModel()
        {

        }

        public EnrollmentOrderViewModel(EnrollmentOrder order)
        {
            _order = order;
        }

        EnrollmentOrder _order;

        public EnrollmentOrder Order
        {
            get
            {
                return _order;
            }
            set
            {
                _order = value;
                RaisePropertyChanged("Order");
            }
        }

        public IEnumerable<Group> Groups { get => Astu.Groups.Where(g => !g.IsGraduated).OrderBy(g => g.Name); }

        public IEnumerable<Faculty> Faculties { get =>  Astu.Faculties.OrderBy(f => f.Name); }

        public IEnumerable<Direction> Directions { get => Astu.Directions.OrderBy(d => d.DisplayName); }

        public IEnumerable<FinanceSource> FinanceSources { get => Astu.FinanceSources.OrderBy(fs => fs.Name); }

        public IEnumerable<EducationForm> EducationForms { get => Astu.EducationForms.OrderBy(ef => ef.Name); }

        public IEnumerable<UnenrollmentReason> UnenrollmentReasons { get => Astu.UnenrollmentReasons.Where(ur => !ur.IsArchival).OrderBy(ur => ur.Name); }

        public IEnumerable<AcademicVacationReason> AcademicVacationReasons { get => Astu.AcademicVacationReasons.OrderBy(a => a.Name); }

    }
}
