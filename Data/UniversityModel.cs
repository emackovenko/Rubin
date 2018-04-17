using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UniversityModel: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=192.168.0.48;UserId=emackovenko;Password=trustno1;database=University;Character Set=utf8;");
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Cathedra> Cathedras { get; set; }
        public DbSet<Citizenship> Citizenships { get; set; }
        public DbSet<Direction> Directions { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<DisciplineCycle> DisciplineCycles { get; set; }
        public DbSet<EducationCompetence> EducationCompetences { get; set; }
        public DbSet<EducationForm> EducationForms { get; set; }
        public DbSet<EducationLevel> EducationLevels { get; set; }
        public DbSet<EducationPlan> EducationPlans { get; set; }
        public DbSet<EducationProgramType> EducationProgramTypes { get; set; }
        public DbSet<EducationStandartType> EducationStandartTypes { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<FinanceSource> FinanceSources { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<IdentityDocument> IdentityDocuments { get; set; }
        public DbSet<IdentityType> IdentityTypes { get; set; }
        public DbSet<IdentityOrganization> IdentityOrganizations { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentState> StudentStates { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}
