using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Model.Astu;

namespace Contingent.Themes.Selectors
{
    public class StudentDocumentTemplateSelector: DataTemplateSelector
    {
        public DataTemplate IdentityDocumentTemplate { get; set; }

        public DataTemplate EducationDocumentTemplate { get; set; }

        public DataTemplate OrphanTicketTemplate { get; set; }

        public DataTemplate DisabilityTicketTemplate { get; set; }

        public DataTemplate DefaultDocumentTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item.GetType() == typeof(IdentityDocument))
            {
                return IdentityDocumentTemplate;
            }
            if (item.GetType() == typeof(EducationDocument))
            {
                return EducationDocumentTemplate;
            }
            if (item.GetType() == typeof(OrphanTicket))
            {
                return OrphanTicketTemplate;
            }
            if (item.GetType() == typeof(DisabilityTicket))
            {
                return DisabilityTicketTemplate;
            }
            return DefaultDocumentTemplate;
        }
    }
}
