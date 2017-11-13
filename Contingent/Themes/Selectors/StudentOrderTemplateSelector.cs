using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Model.Astu;

namespace Contingent.Themes.Selectors
{
    public class StudentOrderTemplateSelector: DataTemplateSelector
    {
        public DataTemplate EnrollmentOrderDataTemplate { get; set; }

        public DataTemplate DefaultOrderDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item.GetType() == typeof(EnrollmentOrder))
            {
                return EnrollmentOrderDataTemplate;
            }
            else
            {
                return DefaultOrderDataTemplate;
            }
        }
    }
}
