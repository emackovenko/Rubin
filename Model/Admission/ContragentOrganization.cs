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
    
    public partial class ContragentOrganization
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ContragentOrganization()
        {
            this.EntrantContracts = new HashSet<EntrantContract>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> AddressId { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string PayNumber { get; set; }
        public string BankName { get; set; }
        public string BankBik { get; set; }
        public string DirectorName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    
        public virtual Address Address { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EntrantContract> EntrantContracts { get; set; }
    }
}
