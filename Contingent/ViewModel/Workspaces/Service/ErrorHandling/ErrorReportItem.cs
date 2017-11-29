using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Astu;

namespace Contingent.ViewModel.Workspaces.Service.ErrorHandling
{
    /// <summary>
    /// Элемент отчета для контроля ошибок в данных студента
    /// </summary>
    public class ErrorReportItem
    {
        public ErrorReportItem(Student student)
        {
            Student = student;
            Errors = ErrorReportItem.GetErrors(student);
        }

        public Student Student { get; set; }

        public StringBuilder Errors { get; set; }

        /// <summary>
        /// Возвращает ложь, если была найдена хотя бы одна ошибка в данных
        /// </summary>
        /// <param name="student">Проверяемый студент</param>
        /// <returns></returns>
        public static bool Validate(Student student)
        {
            throw new NotImplementedException();
        }

        static StringBuilder GetErrors(Student student)
        {
            var sb = new StringBuilder();
            sb.AppendLine(NoUnenrollmentOrderError(student));
            return sb;
        }

        /// <summary>
        /// Возвращает строку ошибки отсутсвия необходимого приказа об отчислении
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        static string NoUnenrollmentOrderError(Student student)
        {
            if (student.StatusId != "0003" && student.StatusId != "0005")
            {
                return string.Empty;
            }

            int ordersCount = Astu.UnenrollmentOrders.Where(o => o.StudentId == student.Id).Count() + Astu.GraduationOrders.Where(o => o.StudentId == student.Id).Count();
            if (ordersCount == 0)
            {
                return "Отсутствует приказ об отчислении";
            }
            return string.Empty;
        }

    }
}
