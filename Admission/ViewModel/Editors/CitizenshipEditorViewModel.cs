using GalaSoft.MvvmLight;
using Model.Admission;

namespace Admission.ViewModel.Editors
{
    public class CitizenshipEditorViewModel : ViewModelBase
    {
        public CitizenshipEditorViewModel(Citizenship citizenship)
        {
            _citizenship = citizenship;
        }
        Citizenship _citizenship;
        public Citizenship Citizenship {
            get
            {
                return _citizenship;
            }
            set
            {
                _citizenship = value;
                RaisePropertyChanged("Citizenship");
            }
        }
    }
}
