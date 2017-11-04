using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Model.Admission;

namespace Admission.ViewModel.Editors
{
    public class FinanceSourcesEditiorViewModel : ViewModelBase
    {
        public FinanceSourcesEditiorViewModel(FinanceSource financeSource)
        {
            _financeSource = financeSource;
        }

        FinanceSource _financeSource;
        public FinanceSource FinanceSource
        {
            get
            {
                return _financeSource;
            }
            set
            {
                _financeSource = value;
                RaisePropertyChanged("FinanceSource");
            }
        }
       
    }
}
