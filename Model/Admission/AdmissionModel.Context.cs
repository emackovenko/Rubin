﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AdmissionDatabase : DbContext
    {
        public AdmissionDatabase()
            : base("name=AdmissionDatabase")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AdmissionVolume> AdmissionVolumes { get; set; }
        public virtual DbSet<BudgetLevel> BudgetLevels { get; set; }
        public virtual DbSet<CampaignIndividualAchievement> CampaignIndividualAchievements { get; set; }
        public virtual DbSet<Campaign> Campaigns { get; set; }
        public virtual DbSet<CampaignStatus> CampaignStatuses { get; set; }
        public virtual DbSet<CampaignType> CampaignTypes { get; set; }
        public virtual DbSet<Citizenship> Citizenships { get; set; }
        public virtual DbSet<ClaimCondition> ClaimConditions { get; set; }
        public virtual DbSet<Claim> Claims { get; set; }
        public virtual DbSet<ClaimStatus> ClaimStatuses { get; set; }
        public virtual DbSet<CommandPermission> CommandPermissions { get; set; }
        public virtual DbSet<Command> Commands { get; set; }
        public virtual DbSet<CompetitionEntranceTest> CompetitionEntranceTests { get; set; }
        public virtual DbSet<CompetitiveGroupItem> CompetitiveGroupItems { get; set; }
        public virtual DbSet<CompetitiveGroup> CompetitiveGroups { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Direction> Directions { get; set; }
        public virtual DbSet<DistributedAdmissionVolume> DistributedAdmissionVolumes { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<EducationForm> EducationForms { get; set; }
        public virtual DbSet<EducationLevel> EducationLevels { get; set; }
        public virtual DbSet<EducationOrganization> EducationOrganizations { get; set; }
        public virtual DbSet<EducationOrganizationType> EducationOrganizationTypes { get; set; }
        public virtual DbSet<EgeDocument> EgeDocuments { get; set; }
        public virtual DbSet<EgeResult> EgeResults { get; set; }
        public virtual DbSet<EnrollmentAgreementClaim> EnrollmentAgreementClaims { get; set; }
        public virtual DbSet<EnrollmentDisagreementClaim> EnrollmentDisagreementClaims { get; set; }
        public virtual DbSet<EntranceTestResult> EntranceTestResults { get; set; }
        public virtual DbSet<EntranceTest> EntranceTests { get; set; }
        public virtual DbSet<Entrant> Entrants { get; set; }
        public virtual DbSet<ExamSubject> ExamSubjects { get; set; }
        public virtual DbSet<FinanceSource> FinanceSources { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<HighEducationDiplomaDocument> HighEducationDiplomaDocuments { get; set; }
        public virtual DbSet<IdentityDocument> IdentityDocuments { get; set; }
        public virtual DbSet<IdentityDocumentType> IdentityDocumentTypes { get; set; }
        public virtual DbSet<IndividualAchievementCategory> IndividualAchievementCategories { get; set; }
        public virtual DbSet<Locality> Localities { get; set; }
        public virtual DbSet<MiddleEducationDiplomaDocument> MiddleEducationDiplomaDocuments { get; set; }
        public virtual DbSet<OrphanDocument> OrphanDocuments { get; set; }
        public virtual DbSet<OrphanDocumentType> OrphanDocumentTypes { get; set; }
        public virtual DbSet<PreviousEducationLevel> PreviousEducationLevels { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SchoolCertificateDocument> SchoolCertificateDocuments { get; set; }
        public virtual DbSet<Street> Streets { get; set; }
        public virtual DbSet<Town> Towns { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<BuildVersion> BuildVersions { get; set; }
        public virtual DbSet<WorkspacePermission> WorkspacePermissions { get; set; }
        public virtual DbSet<Workspace> Workspaces { get; set; }
        public virtual DbSet<EntranceIndividualAchievement> EntranceIndividualAchievements { get; set; }
        public virtual DbSet<Classroom> Classrooms { get; set; }
        public virtual DbSet<ConsultationsLocation> ConsultationsLocations { get; set; }
        public virtual DbSet<ExaminationsLocation> ExaminationsLocations { get; set; }
        public virtual DbSet<OtherRequiredDocument> OtherRequiredDocuments { get; set; }
        public virtual DbSet<EducationProgramType> EducationProgramTypes { get; set; }
        public virtual DbSet<ForeignLanguage> ForeignLanguages { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<EntranceExaminationsCheckProtocol> EntranceExaminationsCheckProtocols { get; set; }
        public virtual DbSet<EnrollmentClaim> EnrollmentClaims { get; set; }
        public virtual DbSet<EnrollmentOrder> EnrollmentOrders { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<ContragentOrganization> ContragentOrganizations { get; set; }
        public virtual DbSet<ContragentType> ContragentTypes { get; set; }
        public virtual DbSet<MotherCapitalCertificates> MotherCapitalCertificates { get; set; }
        public virtual DbSet<ContragentPerson> ContragentPersons { get; set; }
        public virtual DbSet<EnrollmentExceptionOrders> EnrollmentExceptionOrders { get; set; }
        public virtual DbSet<EntrantContract> EntrantContracts { get; set; }
        public virtual DbSet<EnrollmentProtocol> EnrollmentProtocols { get; set; }
        public virtual DbSet<ContractIndividualPlanAuxAgreement> ContractIndividualPlanAuxAgreements { get; set; }
    }
}
