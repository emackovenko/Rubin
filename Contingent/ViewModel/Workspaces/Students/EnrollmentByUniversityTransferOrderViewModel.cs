using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Model.Astu;
using Contingent.DialogService;
using GalaSoft.MvvmLight;
using Model.Astu.Orders.History;

namespace Contingent.ViewModel.Workspaces.Students
{
    public class EnrollmentByUniversityTransferOrderViewModel: ViewModelBase
    {
        public EnrollmentByUniversityTransferOrderViewModel(EnrollmentByUniversityTransferOrder order)
        {
            _order = order;
        }

        EnrollmentByUniversityTransferOrder _order;

        public EnrollmentByUniversityTransferOrder Order
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

        public ObservableCollection<Group> Groups
        {
            get
            {
                return new ObservableCollection<Group>(Astu.Groups.Where(g => !g.IsGraduated).OrderBy(g => g.Name));
            }
        }

        public ObservableCollection<Faculty> Faculties
        {
            get
            {
                return new ObservableCollection<Faculty>(Astu.Faculties.OrderBy(f => f.Name));
            }
        }

        public ObservableCollection<Direction> Directions
        {
            get
            {
                return new ObservableCollection<Direction>(Astu.Directions.OrderBy(d => d.Name));
            }
        }

        public ObservableCollection<FinanceSource> FinanceSources
        {
            get
            {
                return new ObservableCollection<FinanceSource>(Astu.FinanceSources.OrderBy(fs => fs.Name));
            }
        }

    }
}
