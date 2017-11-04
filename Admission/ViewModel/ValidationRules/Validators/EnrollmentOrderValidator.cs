using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;

namespace Admission.ViewModel.ValidationRules.Validators
{
    public class EnrollmentOrderValidator : IEntityValidator
    {
        public EnrollmentOrderValidator(EnrollmentOrder order)
        {
            _order = order;
        }

        EnrollmentOrder _order;

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
            if (string.IsNullOrWhiteSpace(_order.Number))
            {
                ErrorList.Add("Не указан номер приказа");
                result = false;
            }

            // Дата
            if (_order.Date == null)
            {
                ErrorList.Add("Не указана дата приказа");
                result = false;
            }

            // Протоколы
            // количество
            if (!(_order.EnrollmentProtocols.Count > 0))
            {
                ErrorList.Add("Количество протоколов в приказе должно быть больше нуля");
                result = false;
            }

            // заявления в протоколах
            foreach (var protocol in _order.EnrollmentProtocols)
            {
                if (!(protocol.EnrollmentClaims.Count > 0))
                {
                    ErrorList.Add(string.Format("В протоколе №{0}  от {1} не указано ни одного заявления", 
                        protocol.Number, ((DateTime)protocol.Date).ToString("dd.MM.yyyy г.")));
                    result = false;
                }
            }            

            return result;
        }
    }
}
