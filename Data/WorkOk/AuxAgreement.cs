using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.WorkOk
{
    [TableName("platn.dopsogl")]
    public class AuxAgreement: Entity
    {
        public AuxAgreement()
        {
            Date = DateTime.Now;
        }

        [PrimaryKey]
        [DbFieldInfo("pin", DbFieldType.Integer)]
        public int Id { get; set; }

        [DbFieldInfo("nom", DbFieldType.Integer)]
        public int ContractId { get; set; }

        public StudentContract Contract
        {
            get => Context.StudentContracts.FirstOrDefault(e => e.Id == ContractId);
            set => ContractId = value.Id;
        }

        [DbFieldInfo("ndops", DbFieldType.Integer)]
        public int? Number { get; set; }

        [DbFieldInfo("dat", DbFieldType.DateTime)]
        public DateTime Date { get; set; }

        [DbFieldInfo("vid", DbFieldType.Integer)]
        public int AgreementTypeId { get; set; }

        public AgreementType AgreementType
        {
            get => Context.AgreementTypes.FirstOrDefault(e => e.Id == AgreementTypeId);
            set => AgreementTypeId = value.Id;
        }
    }
}
