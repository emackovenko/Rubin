using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Astu
{
    internal interface IEntitySet: IEnumerable
    {
        void Reset();
    }
}
