using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Data.Astu;
using Model.Admission;
using System.Data.OracleClient;
using CommonMethods.TypeExtensions.exDateTime;

namespace Admission.ViewModel.Workspaces.Enrollment
{
    public class AdmissionToAstuTransferViewModel : ViewModelBase
    {
        AdmissionDatabase _admission = new AdmissionDatabase();

        public StudentGroup SelectedAdmissionGroup { get; set; }

        public List<StudentGroup> AdmissionGroups
        {
            get
            {
                return _admission.StudentGroups.OrderBy(sg => sg.Name).ToList();
            }
        }

        StringBuilder _log = new StringBuilder();

        public string Log
        {
            get
            {
                return _log.ToString();
            }
            set
            {
                _log.AppendLine(value);
                RaisePropertyChanged("Log");
            }
        }

        public RelayCommand DoItCommand { get => new RelayCommand(DoIt); }

        void DoIt()
        {
            _log.Clear();
            RaisePropertyChanged("Log");

            // авторизуемся в Astu
            if (AstuAuth())
            {
                Log = "Авторизация в БД rating.astu прошла успешно.";
            }
            else
            {
                Log = "Не удалось авторизоваться в БД rating.astu. Операция прервана.";
            }


            foreach (var group in AdmissionGroups.Where(g => g.GroupFormingOrderId == 3))
            {

                // Ищем такую группу в Astu
                var astuGroup = Astu.Groups.FirstOrDefault(g => g.Name == group.Name);
                if (astuGroup == null)
                {
                    Log = string.Format("Группа {0} не найдена. Создаю новые уч. план и группу:", group.Name);

                    var eduPlan = new EducationPlan
                    {
                        FacultyId = group.Faculty.AstuId,
                        DirectionId = group.ClaimStudentGroupRelationships.First().Claim.EnrollmentProtocol.Direction.AstuId,
                        Year = DateTime.Now.Year,
                        EducationFormId = group.ClaimStudentGroupRelationships.First().Claim.EnrollmentProtocol.EducationForm.AstuId
                    };
                    eduPlan.Save();
                    Astu.EducationPlans.Add(eduPlan);
                    Log = string.Format("\tУчебный план создан ({0}).", eduPlan.Id);

                    astuGroup = new Group
                    {
                        Name = group.Name,
                        IsGraduated = false,
                        EducationPlan = eduPlan
                    };
                    astuGroup.Save();
                    Astu.Groups.Add(astuGroup);
                    group.AstuId = astuGroup.Id;
                    group.PlanId = eduPlan.Id;
                    Log = string.Format("\tГруппа создана ({0}).", astuGroup.Id);
                }
                else
                {
                    Log = "Группа найдена. Обрабатываю студентов.\n";
                }
                group.AstuId = astuGroup.Id;

                // получаем список абитуриентов
                foreach (var csgr in group.ClaimStudentGroupRelationships)
                {
                    if (string.IsNullOrWhiteSpace(csgr.Claim.AstuStudentId))
                    {
                        Log = string.Format("{0} - не найден. Создаю запись:", csgr.Claim.Person.FullName);
                        var claim = csgr.Claim;
                        var student = new Student
                        {
                            FullName = claim.Person.FullName,
                            RegistrationNumber = claim.Number,
                            BirthDate = claim.Person.BirthDate,
                            Gender = claim.Person.GenderId == 1 ? "м" : "ж",
                            IsNeedHostel = claim.IsHostelNeed ?? false,
                            PhoneNumber = claim.Person.Phone,
                            IdentityDocumentSeries = claim.IdentityDocuments.OrderByDescending(id => id.Date).First().Series,
                            IdentityDocumentNumber = claim.IdentityDocuments.OrderByDescending(id => id.Date).First().Number,
                            IdentityDocumentDate = claim.IdentityDocuments.OrderByDescending(id => id.Date).First().Date,
                            IdentityDocumentOrganization = claim.IdentityDocuments.OrderByDescending(id => id.Date).First().Organization,
                            Group = astuGroup,
                            Course = 1,
                            AdmissionYear = DateTime.Now.Year,
                            FinanceSourceId = claim.EnrollmentProtocol.FinanceSource.AstuIdKob,
                            EducationFormId = claim.EnrollmentProtocol.EducationForm.AstuId,
                            DirectionId = claim.EnrollmentProtocol.Direction.AstuId,
                            FacultyId = claim.EnrollmentProtocol.Faculty.AstuId,
                            CitizenshipId = claim.IdentityDocuments.First().Citizenship.AstuId,
                            StatusId = "0001"
                        };

                        // документ об образовании

                        var eduDoc = new EducationDocument();

                        if (claim.SchoolCertificateDocuments.Count > 0)
                        {
                            student.EducationDocumentTypeId = "0001";
                            student.GraduationDocumentSeries = claim.SchoolCertificateDocuments.First().Series;
                            student.GraduationDocumentNumber = claim.SchoolCertificateDocuments.First().Number;
                            student.GraduationDocumentDate = claim.SchoolCertificateDocuments.First().Date;
                            eduDoc.EducationDocumentTypeId = student.EducationDocumentTypeId;
                            eduDoc.Organization = claim.SchoolCertificateDocuments.First().EducationOrganization.Name;
                            eduDoc.DocumentTypeId = 3;
                        }
                        if (claim.MiddleEducationDiplomaDocuments.Count > 0)
                        {
                            student.EducationDocumentTypeId = "0009";
                            student.GraduationDocumentSeries = claim.MiddleEducationDiplomaDocuments.First().Series;
                            student.GraduationDocumentNumber = claim.MiddleEducationDiplomaDocuments.First().Number;
                            student.GraduationDocumentDate = claim.MiddleEducationDiplomaDocuments.First().Date;
                            eduDoc.Organization = claim.MiddleEducationDiplomaDocuments.First().EducationOrganization.Name;
                            eduDoc.DocumentTypeId = 5;
                        }
                        if (claim.HighEducationDiplomaDocuments.Count > 0)
                        {
                            student.EducationDocumentTypeId = "0052";
                            student.GraduationDocumentSeries = claim.HighEducationDiplomaDocuments.First().Series;
                            student.GraduationDocumentNumber = claim.HighEducationDiplomaDocuments.First().Number;
                            student.GraduationDocumentDate = claim.HighEducationDiplomaDocuments.First().Date;
                            eduDoc.Organization = claim.HighEducationDiplomaDocuments.First().EducationOrganization.Name;
                            eduDoc.DocumentTypeId = 4;
                        }

                        student.Save();
                        Astu.Students.Add(student);
                        claim.AstuStudentId = student.Id;
                        Log = string.Format("\t{0} внесен в БД.", student.FullName);

                        eduDoc.Student = student;
                        eduDoc.IsArchival = false;
                        eduDoc.Series = student.GraduationDocumentSeries;
                        eduDoc.Number = student.GraduationDocumentNumber;
                        eduDoc.Date = student.GraduationDocumentDate;
                        eduDoc.Save();
                        Astu.EducationDocuments.Add(eduDoc);
                        Log = string.Format("\tДобавлен документ об образовании ({0}).", eduDoc.Id);

                        // документ, удостоверяющий личность
                        foreach (var doc in claim.IdentityDocuments)
                        {
                            var astuDoc = new Data.Astu.IdentityDocument
                            {
                                Series = doc.Series,
                                Number = doc.Number,
                                Date = doc.Date,
                                BirthDate = claim.Person.BirthDate,
                                BirthPlace = claim.Person.BirthPlace,
                                FirstName = doc.FirstName,
                                LastName = doc.LastName,
                                Patronimyc = doc.Patronymic,
                                CitizenshipId = doc.Citizenship.AstuId,
                                Gender = claim.Person.GenderId == 1 ? "м" : "ж",
                                IdentityDocumentTypeId = int.Parse(doc.IdentityDocumentType.AstuId),
                                Student = student
                            };
                            astuDoc.Save();
                            Astu.IdentityDocuments.Add(astuDoc);
                            Log = string.Format("\tДобавлен документ, удостоверяющий личность ({0}).", astuDoc.Id);
                        }

                        // приказ на зачисление 
                        var enrollmentOrder = new Data.Astu.Orders.History.EnrollmentOrder
                        {
                            Number = claim.EnrollmentOrder.Number,
                            Date = claim.EnrollmentOrder.Date,
                            Comment = string.Format("Зачислен на 1 курс на {0}", claim.EnrollmentProtocol.FinanceSource.Name),
                            DirectionId = claim.EnrollmentProtocol.Direction.AstuId,
                            Group = astuGroup,
                            FacultyId = csgr.StudentGroup.Faculty.AstuId,
                            EducationFormId = claim.EnrollmentProtocol.EducationForm.AstuId,
                            FinanseSourceId = claim.EnrollmentProtocol.FinanceSource.AstuId,
                            IsFromAdmission = true,
                            NewCourse = 1,
                            StartDate = claim.EnrollmentProtocol.TrainingBeginDate,
                            Student = student
                        };
                        enrollmentOrder.Save();
                        Astu.EnrollmentOrders.Add(enrollmentOrder);
                        Log = string.Format("\tДобавлен приказ в карточку: №{0} от {1}", enrollmentOrder.Number, enrollmentOrder.Date.Format());


                    }
                }

                Session.DataModel.SaveChanges();
            }
        }


        bool AstuAuth()
        {
            var connection = new OracleConnection
            {
                ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.0.1.3)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=astumrsk)));User Id = mackovenko_e; Password = trustno1;"
            };
            return Astu.Auth(connection);
        }
    }
}
