using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;

namespace Admission.ViewModel.ValidationRules.Validators
{
    public class EnrollmentProtocolValidator : IEntityValidator
    {
        public EnrollmentProtocolValidator(EnrollmentProtocol protocol)
        {
            _protocol = protocol;
        }

        EnrollmentProtocol _protocol;

        List<string> _errorList = new List<string>();

        public List<string> ErrorList
        {
            get
            {
                return _errorList;
            }

            set
            {
                _errorList = value;
            }
        }

        public bool IsValid
        {
            get
            {
                return Check();
            }
        }

        bool Check()
        {
            bool result = true;
            ErrorList.Clear();

            // Номер
            if (string.IsNullOrWhiteSpace(_protocol.Number))
            {
                ErrorList.Add("Не указан номер протокола");
                result = false;
            }

            // Дата
            if (_protocol.Date == null)
            {
                ErrorList.Add("Не указана дата протокола");
                result = false;
            }

            // Конкурсная группа
            if (_protocol.CompetitiveGroup == null)
            {
                ErrorList.Add("Не найдена конкурсная группа по текущим условиям фильтра");
                result = false;
            }

            // Дата окончания образовательной программы
            if (_protocol.TrainingEndDate == null)
            {
                ErrorList.Add("Не указана дата окончания образовательной программы");
                result = false;
            }

            // Срок обучения
            if (_protocol.TrainingTime == null)
            {
                ErrorList.Add("Не указан срок обучения");
                result = false;
            }

            // Факультет
            if (_protocol.Faculty == null)
            {
                ErrorList.Add("Не указан факультет");
                result = false;
            }

            return result;
        }
    }
}
