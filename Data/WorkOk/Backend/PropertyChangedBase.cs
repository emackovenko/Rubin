using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Data.WorkOk
{
    [Magic]
    public abstract class PropertyChangedBase : INotifyPropertyChanged
    {
        protected virtual void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        [MethodImpl(MethodImplOptions.NoInlining)] // to preserve method call 
        protected static void Raise() { }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
