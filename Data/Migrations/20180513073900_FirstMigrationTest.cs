using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Data.Migrations
{
    public partial class FirstMigrationTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cathedras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cathedras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Citizenships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizenships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Commands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FiasCode = table.Column<string>(nullable: true),
                    KladrCode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineCycles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineCycles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationCompetences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationCompetences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationForms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationProgramTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationProgramTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationStandartTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationStandartTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinanceSources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsBudget = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityOrganizations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityOrganizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InterfaceElements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterfaceElements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Streets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FiasCode = table.Column<string>(nullable: true),
                    KladrCode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    ParentLevel = table.Column<int>(nullable: false),
                    Prefix = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CountryId = table.Column<int>(nullable: true),
                    FiasCode = table.Column<string>(nullable: true),
                    KladrCode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Prefix = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CathedraId = table.Column<int>(nullable: true),
                    DisciplineCycleId = table.Column<int>(nullable: true),
                    EducationCompetenceId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplines_Cathedras_CathedraId",
                        column: x => x.CathedraId,
                        principalTable: "Cathedras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Disciplines_DisciplineCycles_DisciplineCycleId",
                        column: x => x.DisciplineCycleId,
                        principalTable: "DisciplineCycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Disciplines_EducationCompetences_EducationCompetenceId",
                        column: x => x.EducationCompetenceId,
                        principalTable: "EducationCompetences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Directions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    EducationLevelId = table.Column<int>(nullable: true),
                    EducationProgramTypeId = table.Column<int>(nullable: true),
                    EducationStandartTypeId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Directions_EducationLevels_EducationLevelId",
                        column: x => x.EducationLevelId,
                        principalTable: "EducationLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Directions_EducationProgramTypes_EducationProgramTypeId",
                        column: x => x.EducationProgramTypeId,
                        principalTable: "EducationProgramTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Directions_EducationStandartTypes_EducationStandartTypeId",
                        column: x => x.EducationStandartTypeId,
                        principalTable: "EducationStandartTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommandPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CommandId = table.Column<int>(nullable: true),
                    RoleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandPermissions_Commands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Commands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommandPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InterfacePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InterfaceElementId = table.Column<int>(nullable: true),
                    RoleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterfacePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterfacePermissions_InterfaceElements_InterfaceElementId",
                        column: x => x.InterfaceElementId,
                        principalTable: "InterfaceElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterfacePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    Patronimyc = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FiasCode = table.Column<string>(nullable: true),
                    KladrCode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Prefix = table.Column<string>(nullable: true),
                    RegionId = table.Column<int>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Towns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FiasCode = table.Column<string>(nullable: true),
                    KladrCode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Prefix = table.Column<string>(nullable: true),
                    RegionId = table.Column<int>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Towns_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EducationPlans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CathedraId = table.Column<int>(nullable: true),
                    DirectionId = table.Column<int>(nullable: true),
                    EducationFormId = table.Column<int>(nullable: true),
                    EducationLevelId = table.Column<int>(nullable: true),
                    FacultyId = table.Column<int>(nullable: true),
                    IsAcceleratedLearning = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationPlans_Cathedras_CathedraId",
                        column: x => x.CathedraId,
                        principalTable: "Cathedras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationPlans_Directions_DirectionId",
                        column: x => x.DirectionId,
                        principalTable: "Directions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationPlans_EducationForms_EducationFormId",
                        column: x => x.EducationFormId,
                        principalTable: "EducationForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationPlans_EducationLevels_EducationLevelId",
                        column: x => x.EducationLevelId,
                        principalTable: "EducationLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationPlans_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Localities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DistrictId = table.Column<int>(nullable: true),
                    FiasCode = table.Column<string>(nullable: true),
                    KladrCode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Prefix = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localities_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AdmissionYear = table.Column<int>(nullable: false),
                    EducationFormId = table.Column<int>(nullable: true),
                    EducationPlanId = table.Column<int>(nullable: true),
                    FacultyId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_EducationForms_EducationFormId",
                        column: x => x.EducationFormId,
                        principalTable: "EducationForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Groups_EducationPlans_EducationPlanId",
                        column: x => x.EducationPlanId,
                        principalTable: "EducationPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Groups_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Apartment = table.Column<string>(nullable: true),
                    Bulding = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    DisctrictId = table.Column<int>(nullable: true),
                    DistrictId = table.Column<int>(nullable: true),
                    LocalityId = table.Column<int>(nullable: true),
                    RegionId = table.Column<int>(nullable: true),
                    StreetId = table.Column<int>(nullable: true),
                    TownId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_Localities_LocalityId",
                        column: x => x.LocalityId,
                        principalTable: "Localities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_Streets_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Streets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<int>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    GenderId = table.Column<int>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Patronimyc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persons_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdentityDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CitizenshipId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: true),
                    Patronimyc = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: true),
                    Series = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityDocuments_Citizenships_CitizenshipId",
                        column: x => x.CitizenshipId,
                        principalTable: "Citizenships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdentityDocuments_IdentityOrganizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "IdentityOrganizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdentityDocuments_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdentityDocuments_IdentityTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "IdentityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FinanceSourceId = table.Column<int>(nullable: true),
                    GroupId = table.Column<int>(nullable: true),
                    PersonFileNumber = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: true),
                    StudentStateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_FinanceSources_FinanceSourceId",
                        column: x => x.FinanceSourceId,
                        principalTable: "FinanceSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_StudentStates_StudentStateId",
                        column: x => x.StudentStateId,
                        principalTable: "StudentStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CathedraId = table.Column<int>(nullable: true),
                    PersonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Cathedras_CathedraId",
                        column: x => x.CathedraId,
                        principalTable: "Cathedras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teachers_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId",
                table: "Addresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_DistrictId",
                table: "Addresses",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_LocalityId",
                table: "Addresses",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_RegionId",
                table: "Addresses",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StreetId",
                table: "Addresses",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_TownId",
                table: "Addresses",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandPermissions_CommandId",
                table: "CommandPermissions",
                column: "CommandId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandPermissions_RoleId",
                table: "CommandPermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Directions_EducationLevelId",
                table: "Directions",
                column: "EducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Directions_EducationProgramTypeId",
                table: "Directions",
                column: "EducationProgramTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Directions_EducationStandartTypeId",
                table: "Directions",
                column: "EducationStandartTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_CathedraId",
                table: "Disciplines",
                column: "CathedraId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_DisciplineCycleId",
                table: "Disciplines",
                column: "DisciplineCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_EducationCompetenceId",
                table: "Disciplines",
                column: "EducationCompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_RegionId",
                table: "Districts",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationPlans_CathedraId",
                table: "EducationPlans",
                column: "CathedraId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationPlans_DirectionId",
                table: "EducationPlans",
                column: "DirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationPlans_EducationFormId",
                table: "EducationPlans",
                column: "EducationFormId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationPlans_EducationLevelId",
                table: "EducationPlans",
                column: "EducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationPlans_FacultyId",
                table: "EducationPlans",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_EducationFormId",
                table: "Groups",
                column: "EducationFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_EducationPlanId",
                table: "Groups",
                column: "EducationPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_FacultyId",
                table: "Groups",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityDocuments_CitizenshipId",
                table: "IdentityDocuments",
                column: "CitizenshipId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityDocuments_OrganizationId",
                table: "IdentityDocuments",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityDocuments_PersonId",
                table: "IdentityDocuments",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityDocuments_TypeId",
                table: "IdentityDocuments",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InterfacePermissions_InterfaceElementId",
                table: "InterfacePermissions",
                column: "InterfaceElementId");

            migrationBuilder.CreateIndex(
                name: "IX_InterfacePermissions_RoleId",
                table: "InterfacePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Localities_DistrictId",
                table: "Localities",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_AddressId",
                table: "Persons",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_GenderId",
                table: "Persons",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CountryId",
                table: "Regions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FinanceSourceId",
                table: "Students",
                column: "FinanceSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupId",
                table: "Students",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_PersonId",
                table: "Students",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentStateId",
                table: "Students",
                column: "StudentStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_CathedraId",
                table: "Teachers",
                column: "CathedraId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_PersonId",
                table: "Teachers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Towns_RegionId",
                table: "Towns",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandPermissions");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropTable(
                name: "IdentityDocuments");

            migrationBuilder.DropTable(
                name: "InterfacePermissions");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Commands");

            migrationBuilder.DropTable(
                name: "DisciplineCycles");

            migrationBuilder.DropTable(
                name: "EducationCompetences");

            migrationBuilder.DropTable(
                name: "Citizenships");

            migrationBuilder.DropTable(
                name: "IdentityOrganizations");

            migrationBuilder.DropTable(
                name: "IdentityTypes");

            migrationBuilder.DropTable(
                name: "InterfaceElements");

            migrationBuilder.DropTable(
                name: "FinanceSources");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "StudentStates");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "EducationPlans");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Cathedras");

            migrationBuilder.DropTable(
                name: "Directions");

            migrationBuilder.DropTable(
                name: "EducationForms");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "Localities");

            migrationBuilder.DropTable(
                name: "Streets");

            migrationBuilder.DropTable(
                name: "Towns");

            migrationBuilder.DropTable(
                name: "EducationLevels");

            migrationBuilder.DropTable(
                name: "EducationProgramTypes");

            migrationBuilder.DropTable(
                name: "EducationStandartTypes");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
