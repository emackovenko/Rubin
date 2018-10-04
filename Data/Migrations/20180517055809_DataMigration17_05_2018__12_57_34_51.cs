using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Data.Migrations
{
    public partial class DataMigration17_05_2018__12_57_34_51 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Countries_CountryId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Districts_DistrictId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Localities_LocalityId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Regions_RegionId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Streets_StreetId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Towns_TownId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_CommandPermissions_Commands_CommandId",
                table: "CommandPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_CommandPermissions_Roles_RoleId",
                table: "CommandPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Directions_EducationLevels_EducationLevelId",
                table: "Directions");

            migrationBuilder.DropForeignKey(
                name: "FK_Directions_EducationProgramTypes_EducationProgramTypeId",
                table: "Directions");

            migrationBuilder.DropForeignKey(
                name: "FK_Directions_EducationStandartTypes_EducationStandartTypeId",
                table: "Directions");

            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Regions_RegionId",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlanGraphics_EducationPlanItems_EducationPlanItemId",
                table: "EducationPlanGraphics");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlanItems_EducationPlanCompoments_ComponentId",
                table: "EducationPlanItems");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlanItems_DisciplineCycles_DisciplineCycleId",
                table: "EducationPlanItems");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlanItems_Disciplines_DisciplineId",
                table: "EducationPlanItems");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlanItems_EducationCompetences_EducationCompetenceId",
                table: "EducationPlanItems");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlanItems_EducationPlans_EducationPlanId",
                table: "EducationPlanItems");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlans_Cathedras_CathedraId",
                table: "EducationPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlans_Directions_DirectionId",
                table: "EducationPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlans_EducationForms_EducationFormId",
                table: "EducationPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlans_EducationProgramTypes_EducationProgramTypeId",
                table: "EducationPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlans_Faculties_FacultyId",
                table: "EducationPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_EducationForms_EducationFormId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_EducationPlans_EducationPlanId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Faculties_FacultyId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityDocuments_Citizenships_CitizenshipId",
                table: "IdentityDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityDocuments_IdentityOrganizations_OrganizationId",
                table: "IdentityDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityDocuments_Persons_PersonId",
                table: "IdentityDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityDocuments_IdentityTypes_TypeId",
                table: "IdentityDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_InterfacePermissions_InterfaceElements_InterfaceElementId",
                table: "InterfacePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_InterfacePermissions_Roles_RoleId",
                table: "InterfacePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Localities_Districts_DistrictId",
                table: "Localities");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Addresses_AddressId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Genders_GenderId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Regions_Countries_CountryId",
                table: "Regions");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_FinanceSources_FinanceSourceId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Persons_PersonId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_StudentStates_StudentStateId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Cathedras_CathedraId",
                table: "Teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Persons_PersonId",
                table: "Teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_Towns_Regions_RegionId",
                table: "Towns");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Countries_CountryId",
                table: "Addresses",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Districts_DistrictId",
                table: "Addresses",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Localities_LocalityId",
                table: "Addresses",
                column: "LocalityId",
                principalTable: "Localities",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Regions_RegionId",
                table: "Addresses",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Streets_StreetId",
                table: "Addresses",
                column: "StreetId",
                principalTable: "Streets",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Towns_TownId",
                table: "Addresses",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CommandPermissions_Commands_CommandId",
                table: "CommandPermissions",
                column: "CommandId",
                principalTable: "Commands",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CommandPermissions_Roles_RoleId",
                table: "CommandPermissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Directions_EducationLevels_EducationLevelId",
                table: "Directions",
                column: "EducationLevelId",
                principalTable: "EducationLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Directions_EducationProgramTypes_EducationProgramTypeId",
                table: "Directions",
                column: "EducationProgramTypeId",
                principalTable: "EducationProgramTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Directions_EducationStandartTypes_EducationStandartTypeId",
                table: "Directions",
                column: "EducationStandartTypeId",
                principalTable: "EducationStandartTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Regions_RegionId",
                table: "Districts",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlanGraphics_EducationPlanItems_EducationPlanItemId",
                table: "EducationPlanGraphics",
                column: "EducationPlanItemId",
                principalTable: "EducationPlanItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlanItems_EducationPlanCompoments_ComponentId",
                table: "EducationPlanItems",
                column: "ComponentId",
                principalTable: "EducationPlanCompoments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlanItems_DisciplineCycles_DisciplineCycleId",
                table: "EducationPlanItems",
                column: "DisciplineCycleId",
                principalTable: "DisciplineCycles",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlanItems_Disciplines_DisciplineId",
                table: "EducationPlanItems",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlanItems_EducationCompetences_EducationCompetenceId",
                table: "EducationPlanItems",
                column: "EducationCompetenceId",
                principalTable: "EducationCompetences",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlanItems_EducationPlans_EducationPlanId",
                table: "EducationPlanItems",
                column: "EducationPlanId",
                principalTable: "EducationPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlans_Cathedras_CathedraId",
                table: "EducationPlans",
                column: "CathedraId",
                principalTable: "Cathedras",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlans_Directions_DirectionId",
                table: "EducationPlans",
                column: "DirectionId",
                principalTable: "Directions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlans_EducationForms_EducationFormId",
                table: "EducationPlans",
                column: "EducationFormId",
                principalTable: "EducationForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlans_EducationProgramTypes_EducationProgramTypeId",
                table: "EducationPlans",
                column: "EducationProgramTypeId",
                principalTable: "EducationProgramTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlans_Faculties_FacultyId",
                table: "EducationPlans",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_EducationForms_EducationFormId",
                table: "Groups",
                column: "EducationFormId",
                principalTable: "EducationForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_EducationPlans_EducationPlanId",
                table: "Groups",
                column: "EducationPlanId",
                principalTable: "EducationPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Faculties_FacultyId",
                table: "Groups",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityDocuments_Citizenships_CitizenshipId",
                table: "IdentityDocuments",
                column: "CitizenshipId",
                principalTable: "Citizenships",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityDocuments_IdentityOrganizations_OrganizationId",
                table: "IdentityDocuments",
                column: "OrganizationId",
                principalTable: "IdentityOrganizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityDocuments_Persons_PersonId",
                table: "IdentityDocuments",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityDocuments_IdentityTypes_TypeId",
                table: "IdentityDocuments",
                column: "TypeId",
                principalTable: "IdentityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_InterfacePermissions_InterfaceElements_InterfaceElementId",
                table: "InterfacePermissions",
                column: "InterfaceElementId",
                principalTable: "InterfaceElements",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_InterfacePermissions_Roles_RoleId",
                table: "InterfacePermissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Localities_Districts_DistrictId",
                table: "Localities",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Addresses_AddressId",
                table: "Persons",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Genders_GenderId",
                table: "Persons",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_Countries_CountryId",
                table: "Regions",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_FinanceSources_FinanceSourceId",
                table: "Students",
                column: "FinanceSourceId",
                principalTable: "FinanceSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Persons_PersonId",
                table: "Students",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_StudentStates_StudentStateId",
                table: "Students",
                column: "StudentStateId",
                principalTable: "StudentStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Cathedras_CathedraId",
                table: "Teachers",
                column: "CathedraId",
                principalTable: "Cathedras",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Persons_PersonId",
                table: "Teachers",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Towns_Regions_RegionId",
                table: "Towns",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Countries_CountryId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Districts_DistrictId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Localities_LocalityId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Regions_RegionId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Streets_StreetId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Towns_TownId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_CommandPermissions_Commands_CommandId",
                table: "CommandPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_CommandPermissions_Roles_RoleId",
                table: "CommandPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Directions_EducationLevels_EducationLevelId",
                table: "Directions");

            migrationBuilder.DropForeignKey(
                name: "FK_Directions_EducationProgramTypes_EducationProgramTypeId",
                table: "Directions");

            migrationBuilder.DropForeignKey(
                name: "FK_Directions_EducationStandartTypes_EducationStandartTypeId",
                table: "Directions");

            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Regions_RegionId",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlanGraphics_EducationPlanItems_EducationPlanItemId",
                table: "EducationPlanGraphics");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlanItems_EducationPlanCompoments_ComponentId",
                table: "EducationPlanItems");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlanItems_DisciplineCycles_DisciplineCycleId",
                table: "EducationPlanItems");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlanItems_Disciplines_DisciplineId",
                table: "EducationPlanItems");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlanItems_EducationCompetences_EducationCompetenceId",
                table: "EducationPlanItems");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlanItems_EducationPlans_EducationPlanId",
                table: "EducationPlanItems");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlans_Cathedras_CathedraId",
                table: "EducationPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlans_Directions_DirectionId",
                table: "EducationPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlans_EducationForms_EducationFormId",
                table: "EducationPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlans_EducationProgramTypes_EducationProgramTypeId",
                table: "EducationPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlans_Faculties_FacultyId",
                table: "EducationPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_EducationForms_EducationFormId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_EducationPlans_EducationPlanId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Faculties_FacultyId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityDocuments_Citizenships_CitizenshipId",
                table: "IdentityDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityDocuments_IdentityOrganizations_OrganizationId",
                table: "IdentityDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityDocuments_Persons_PersonId",
                table: "IdentityDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityDocuments_IdentityTypes_TypeId",
                table: "IdentityDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_InterfacePermissions_InterfaceElements_InterfaceElementId",
                table: "InterfacePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_InterfacePermissions_Roles_RoleId",
                table: "InterfacePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Localities_Districts_DistrictId",
                table: "Localities");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Addresses_AddressId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Genders_GenderId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Regions_Countries_CountryId",
                table: "Regions");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_FinanceSources_FinanceSourceId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Persons_PersonId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_StudentStates_StudentStateId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Cathedras_CathedraId",
                table: "Teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Persons_PersonId",
                table: "Teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_Towns_Regions_RegionId",
                table: "Towns");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Countries_CountryId",
                table: "Addresses",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Districts_DistrictId",
                table: "Addresses",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Localities_LocalityId",
                table: "Addresses",
                column: "LocalityId",
                principalTable: "Localities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Regions_RegionId",
                table: "Addresses",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Streets_StreetId",
                table: "Addresses",
                column: "StreetId",
                principalTable: "Streets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Towns_TownId",
                table: "Addresses",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommandPermissions_Commands_CommandId",
                table: "CommandPermissions",
                column: "CommandId",
                principalTable: "Commands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommandPermissions_Roles_RoleId",
                table: "CommandPermissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Directions_EducationLevels_EducationLevelId",
                table: "Directions",
                column: "EducationLevelId",
                principalTable: "EducationLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Directions_EducationProgramTypes_EducationProgramTypeId",
                table: "Directions",
                column: "EducationProgramTypeId",
                principalTable: "EducationProgramTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Directions_EducationStandartTypes_EducationStandartTypeId",
                table: "Directions",
                column: "EducationStandartTypeId",
                principalTable: "EducationStandartTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Regions_RegionId",
                table: "Districts",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlanGraphics_EducationPlanItems_EducationPlanItemId",
                table: "EducationPlanGraphics",
                column: "EducationPlanItemId",
                principalTable: "EducationPlanItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlanItems_EducationPlanCompoments_ComponentId",
                table: "EducationPlanItems",
                column: "ComponentId",
                principalTable: "EducationPlanCompoments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlanItems_DisciplineCycles_DisciplineCycleId",
                table: "EducationPlanItems",
                column: "DisciplineCycleId",
                principalTable: "DisciplineCycles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlanItems_Disciplines_DisciplineId",
                table: "EducationPlanItems",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlanItems_EducationCompetences_EducationCompetenceId",
                table: "EducationPlanItems",
                column: "EducationCompetenceId",
                principalTable: "EducationCompetences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlanItems_EducationPlans_EducationPlanId",
                table: "EducationPlanItems",
                column: "EducationPlanId",
                principalTable: "EducationPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlans_Cathedras_CathedraId",
                table: "EducationPlans",
                column: "CathedraId",
                principalTable: "Cathedras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlans_Directions_DirectionId",
                table: "EducationPlans",
                column: "DirectionId",
                principalTable: "Directions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlans_EducationForms_EducationFormId",
                table: "EducationPlans",
                column: "EducationFormId",
                principalTable: "EducationForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlans_EducationProgramTypes_EducationProgramTypeId",
                table: "EducationPlans",
                column: "EducationProgramTypeId",
                principalTable: "EducationProgramTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlans_Faculties_FacultyId",
                table: "EducationPlans",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_EducationForms_EducationFormId",
                table: "Groups",
                column: "EducationFormId",
                principalTable: "EducationForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_EducationPlans_EducationPlanId",
                table: "Groups",
                column: "EducationPlanId",
                principalTable: "EducationPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Faculties_FacultyId",
                table: "Groups",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityDocuments_Citizenships_CitizenshipId",
                table: "IdentityDocuments",
                column: "CitizenshipId",
                principalTable: "Citizenships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityDocuments_IdentityOrganizations_OrganizationId",
                table: "IdentityDocuments",
                column: "OrganizationId",
                principalTable: "IdentityOrganizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityDocuments_Persons_PersonId",
                table: "IdentityDocuments",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityDocuments_IdentityTypes_TypeId",
                table: "IdentityDocuments",
                column: "TypeId",
                principalTable: "IdentityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterfacePermissions_InterfaceElements_InterfaceElementId",
                table: "InterfacePermissions",
                column: "InterfaceElementId",
                principalTable: "InterfaceElements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterfacePermissions_Roles_RoleId",
                table: "InterfacePermissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Localities_Districts_DistrictId",
                table: "Localities",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Addresses_AddressId",
                table: "Persons",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Genders_GenderId",
                table: "Persons",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_Countries_CountryId",
                table: "Regions",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_FinanceSources_FinanceSourceId",
                table: "Students",
                column: "FinanceSourceId",
                principalTable: "FinanceSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Persons_PersonId",
                table: "Students",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_StudentStates_StudentStateId",
                table: "Students",
                column: "StudentStateId",
                principalTable: "StudentStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Cathedras_CathedraId",
                table: "Teachers",
                column: "CathedraId",
                principalTable: "Cathedras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Persons_PersonId",
                table: "Teachers",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Towns_Regions_RegionId",
                table: "Towns",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
