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
        public DataTemplate AcademicVacationOrderDataTemplate { get; set; }

        public DataTemplate ChildrenFussVacationOrderDataTemplate { get; set; }

        public DataTemplate DefaultOrderDataTemplate { get; set; }

        public DataTemplate DirectionChangingOrderDataTemplate { get; set; }

        public DataTemplate EnrollmentOrderDataTemplate { get; set; }

        public DataTemplate FinanceSourceChangingOrderDataTemplate { get; set; }

        public DataTemplate GroupTransferOrderDataTemplate { get; set; }

        public DataTemplate NextCourseTransferOrderDataTemplate { get; set; }

        public DataTemplate StudentNameChangingOrderDataTemplate { get; set; }

        public DataTemplate UnenrollmentOrderDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {

            if (item.GetType() == typeof(AcademicVacationOrder))
            {
                return AcademicVacationOrderDataTemplate;
            }

            if (item.GetType() == typeof(ChildrenFussVacationOrder))
            {
                return ChildrenFussVacationOrderDataTemplate;
            }

            if (item.GetType() == typeof(DirectionChangingOrder))
            {
                return DirectionChangingOrderDataTemplate;
            }

            if (item.GetType() == typeof(EnrollmentByUniversityTransferOrder))
            {
                return EnrollmentOrderDataTemplate;
            }

            if (item.GetType() == typeof(EnrollmentOrder))
            {
                return EnrollmentOrderDataTemplate;
            }

            if (item.GetType() == typeof(FinanceSourceChangingOrder))
            {
                return FinanceSourceChangingOrderDataTemplate;
            }

            if (item.GetType() == typeof(GraduationOrder))
            {
                return UnenrollmentOrderDataTemplate;
            }

            if (item.GetType() == typeof(GroupTransferOrder))
            {
                return GroupTransferOrderDataTemplate;
            }

            if (item.GetType() == typeof(NextCourseTransferOrder))
            {
                return NextCourseTransferOrderDataTemplate;
            }

            if (item.GetType() == typeof(ReinstatementOrder))
            {
                return EnrollmentOrderDataTemplate;
            }

            if (item.GetType() == typeof(StudentNameChangingOrder))
            {
                return StudentNameChangingOrderDataTemplate;
            }

            if (item.GetType() == typeof(UnenrollmentOrder))
            {
                return UnenrollmentOrderDataTemplate;
            }

            if (item.GetType() == typeof(AcademicVacationExitOrder))
            {
                return EnrollmentOrderDataTemplate;
            }

            if (item.GetType() == typeof(ChildrenFussVacationExitOrder))
            {
                return EnrollmentOrderDataTemplate;
            }

            return DefaultOrderDataTemplate;
        }
    }
}
