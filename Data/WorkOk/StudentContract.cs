using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.WorkOk
{
    [TableName("platn.accdog")]
    public class StudentContract: Entity
    {
        public StudentContract()
        {
            ContragentId = -1;
        }

        [PrimaryKey]
        [DbFieldInfo("pin", DbFieldType.Integer)]
        public int Id { get; set; }

        [DbFieldInfo("acc", DbFieldType.Integer)]
        public int StudentId { get; set; }

        public Student Student
        {
            get => Context.Students.FirstOrDefault(s => s.Id == StudentId);
            set => StudentId = value.Id;
        }

        [DbFieldInfo("nomdog", DbFieldType.String)]
        public string Number { get; set; }

        [DbFieldInfo("datedog", DbFieldType.DateTime)]
        public DateTime? Date { get; set; }

        [DbFieldInfo("srokd", DbFieldType.DateTime)]
        public DateTime? ExpiryDate { get; set; }

        [DbFieldInfo("summ", DbFieldType.Double)]
        public double Sum { get; set; }

        [DbFieldInfo("begdog", DbFieldType.DateTime)]
        public DateTime? PaymentBeginDate { get; set; }

        [DbFieldInfo("enddog", DbFieldType.DateTime)]
        public DateTime? PaymentEndDate { get; set; }

        [DbFieldInfo("rastorg", DbFieldType.Integer)]
        private int IsArchive { get; set; }

        public bool IsActive
        {
            get => IsArchive != 1;
            set
            {
                if (value)
                {
                    IsArchive = 0;
                }
                else
                {
                    IsArchive = 1;
                }
                Raise();
            }
        }

        [DbFieldInfo("kurs", DbFieldType.Integer)]
        public int? Course { get; set; }

        [DbFieldInfo("spotd", DbFieldType.Integer)]
        public int? EducationFormId { get; set; }

        public EducationForm EducationForm
        {
            get => Context.EducationForms.FirstOrDefault(e => e.Id == EducationFormId);
            set => EducationFormId = value?.Id;
        }
        
        [DbFieldInfo("spfak", DbFieldType.Integer)]
        public int? FacultyId { get; set; }

        public Faculty Faculty
        {
            get => Context.Faculties.FirstOrDefault(e => e.Id == FacultyId);
            set => FacultyId = value?.Id;
        }
        
        [DbFieldInfo("spspec", DbFieldType.Integer)]
        public int? DirectionId { get; set; }

        public Direction Direction
        {
            get => Context.Directions.FirstOrDefault(e => e.Id == DirectionId);
            set => DirectionId = value?.Id;
        }

        [DbFieldInfo("period", DbFieldType.Integer)]
        public int? PaymentPeriodTypeId { get; set; }

        public PaymentPeriodType PaymentPeriodType
        {
            get => Context.PaymentPeriodTypes.FirstOrDefault(e => e.Id == PaymentPeriodTypeId);
            set => PaymentPeriodTypeId = value?.Id;
        }

        [DbFieldInfo("plat")]
        public int? ContragentId { get; set; }

        public Contragent Contragent
        {
            get
            {
                if (ContragentId > 0)
                {
                    if (Context.PersonContragents.FirstOrDefault(pc => pc.Id == ContragentId) != null)
                    {
                        return Context.PersonContragents.FirstOrDefault(pc => pc.Id == ContragentId); 
                    }
                    else
                    {
                        return Context.OrganizationContragents.FirstOrDefault(oc => oc.Id == ContragentId);
                    }
                }
                else
                {
                    return new PersonContragent
                    {
                        Name = Student.FullName,
                        Address = Student.Info.Address,
                        PhoneNumber = Student.Info.PhoneNumber,
                        IdentityDocumentSeries = null,
                        IdentityDocumentNumber = Student.Info.IdentityDocumentProps,
                        IdentityDocumentDate = Student.Info.IdentityDocumentDate,
                        IdentityDocumentOrganization = Student.Info.IdentityDocumentOrganization
                    };
                }
            }
            set
            {
                if (value == null)
                {
                    ContragentId = -1;
                }
                else
                {
                    ContragentId = value?.Id;
                }
                Raise();
            }
        }

        public IEnumerable<AuxAgreement> Agreements
        {
            get
            {
                var collection = new List<AuxAgreement>();
                collection.AddRange(Context.AuxAgreements.Where(aa => aa.ContractId == Id));
                //collection.AddRange(Context.StudentNameChangingAgreements.Where(a => a.ContractId == Id));
                //collection.AddRange(Context.PriceChangingAgreements.Where(a => a.ContractId == Id));
                //collection.AddRange(Context.ContractPeriodChangingAgreements.Where(a => a.ContractId == Id));
                //collection.AddRange(Context.ContractPriceAndPeriodChangingAgreements.Where(a => a.ContractId == Id));
                //collection.AddRange(Context.ContractTerminationAgreements.Where(a => a.ContractId == Id));
                //collection.AddRange(Context.ContractTerminationWithObligationAgreements.Where(a => a.ContractId == Id));
                return collection;
            }
        }

    }
}
