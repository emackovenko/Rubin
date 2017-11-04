using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Model.Admission;

namespace Admission.ViewModel.Editors
{
    public class DirectionEditorViewModel : ViewModelBase
    {

        public DirectionEditorViewModel(Direction direction)
        {
            _direction = direction;
        }

        Direction _direction;
        public Direction Direction
        {
            get
            {
                return _direction;
            }
            set
            {
                _direction = value;
                RaisePropertyChanged("Direction");
            }
        }
    }
}
