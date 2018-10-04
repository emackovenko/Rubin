using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Data.University
{
    public class UniversityModel: DbContext
    {

        public UniversityModel()
            : base()
        {
        }

        public UniversityModel(bool forcedLoading = false)
        {
            if (forcedLoading)
            {
                // todo Разрешить ситуацию с ленивой загрузкой данных
                LoadEntities();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.SetNull;
            }

            base.OnModelCreating(modelbuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=192.168.0.48;UserId=emackovenko;Password=trustno1;database=University;characterset=utf8;");
        }

        

        protected void LoadEntities()
        {
            Addresses.Load();
            Cathedras.Load();
            Citizenships.Load();
            CommandPermissions.Load();
            Commands.Load();
            Countries.Load();
            Directions.Load();
            DisciplineCycles.Load();
            Disciplines.Load();
            Districts.Load();
            EducationCompetences.Load();
            EducationForms.Load();
            EducationLevels.Load();
            EducationPlanCompoments.Load();
            EducationPlanGraphics.Load();
            EducationPlanItems.Load();
            EducationPlans.Load();
            EducationProgramTypes.Load();
            EducationStandartTypes.Load();
            Faculties.Load();
            FinanceSources.Load();
            Genders.Load();
            Groups.Load();
            IdentityDocuments.Load();
            IdentityOrganizations.Load();
            IdentityTypes.Load();
            InterfaceElements.Load();
            InterfacePermissions.Load();
            Localities.Load();
            Persons.Load();
            Regions.Load();
            Roles.Load();
            Streets.Load();
            StudentStates.Load();
            Students.Load();
            Teachers.Load();
            Towns.Load();
            Users.Load();
        }


        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cathedra> Cathedras { get; set; }
        public DbSet<Citizenship> Citizenships { get; set; }
        public DbSet<Command> Commands { get; set; }
        public DbSet<CommandPermission> CommandPermissions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Direction> Directions { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<DisciplineCycle> DisciplineCycles { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<EducationCompetence> EducationCompetences { get; set; }
        public DbSet<EducationForm> EducationForms { get; set; }
        public DbSet<EducationLevel> EducationLevels { get; set; }
        public DbSet<EducationPlan> EducationPlans { get; set; }
        public DbSet<EducationPlanCompoment> EducationPlanCompoments { get; set; }
        public DbSet<EducationPlanGraphic> EducationPlanGraphics { get; set; }
        public DbSet<EducationPlanItem> EducationPlanItems { get; set; }
        public DbSet<EducationProgramType> EducationProgramTypes { get; set; }
        public DbSet<EducationStandartType> EducationStandartTypes { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<FinanceSource> FinanceSources { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<IdentityDocument> IdentityDocuments { get; set; }
        public DbSet<IdentityOrganization> IdentityOrganizations { get; set; }
        public DbSet<IdentityType> IdentityTypes { get; set; }
        public DbSet<InterfaceElement> InterfaceElements { get; set; }
        public DbSet<InterfacePermission> InterfacePermissions { get; set; }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentState> StudentStates { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
