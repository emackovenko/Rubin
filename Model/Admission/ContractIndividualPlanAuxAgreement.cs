//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.Admission
{
    using System;
    using System.Collections.Generic;
    
    public partial class ContractIndividualPlanAuxAgreement
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string TrainingPeriod { get; set; }
        public Nullable<double> YearPrice { get; set; }
        public Nullable<double> FullPrice { get; set; }
        public Nullable<int> EntrantContractId { get; set; }
    
        public virtual EntrantContract EntrantContract { get; set; }
    }
}
