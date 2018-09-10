using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Data.Migrations
{
    public partial class EducationPlanModelReady : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disciplines_Cathedras_CathedraId",
                table: "Disciplines");

            migrationBuilder.DropForeignKey(
                name: "FK_Disciplines_DisciplineCycles_DisciplineCycleId",
                table: "Disciplines");

            migrationBuilder.DropForeignKey(
                name: "FK_Disciplines_EducationCompetences_EducationCompetenceId",
                table: "Disciplines");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlans_EducationLevels_EducationLevelId",
                table: "EducationPlans");

            migrationBuilder.DropIndex(
                name: "IX_Disciplines_CathedraId",
                table: "Disciplines");

            migrationBuilder.DropIndex(
                name: "IX_Disciplines_DisciplineCycleId",
                table: "Disciplines");

            migrationBuilder.DropIndex(
                name: "IX_Disciplines_EducationCompetenceId",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "IsAcceleratedLearning",
                table: "EducationPlans");

            migrationBuilder.DropColumn(
                name: "CathedraId",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "DisciplineCycleId",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "EducationCompetenceId",
                table: "Disciplines");

            migrationBuilder.RenameColumn(
                name: "EducationLevelId",
                table: "EducationPlans",
                newName: "EducationProgramTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_EducationPlans_EducationLevelId",
                table: "EducationPlans",
                newName: "IX_EducationPlans_EducationProgramTypeId");

            migrationBuilder.AddColumn<string>(
                name: "TrainingPeriod",
                table: "EducationPlans",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Disciplines",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EducationPlanCompoments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationPlanCompoments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationPlanItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    ComponentId = table.Column<int>(nullable: true),
                    DisciplineCycleId = table.Column<int>(nullable: true),
                    DisciplineId = table.Column<int>(nullable: true),
                    EducationCompetenceId = table.Column<int>(nullable: true),
                    EducationPlanComponentId = table.Column<int>(nullable: true),
                    EducationPlanId = table.Column<int>(nullable: true),
                    IsChecked = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationPlanItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationPlanItems_EducationPlanCompoments_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "EducationPlanCompoments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationPlanItems_DisciplineCycles_DisciplineCycleId",
                        column: x => x.DisciplineCycleId,
                        principalTable: "DisciplineCycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationPlanItems_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationPlanItems_EducationCompetences_EducationCompetenceId",
                        column: x => x.EducationCompetenceId,
                        principalTable: "EducationCompetences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationPlanItems_EducationPlans_EducationPlanId",
                        column: x => x.EducationPlanId,
                        principalTable: "EducationPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EducationPlanGraphics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CourseWorkTest = table.Column<bool>(nullable: true),
                    CreditTest = table.Column<bool>(nullable: true),
                    DiffCreditTest = table.Column<bool>(nullable: true),
                    EducationPlanItemElementId = table.Column<int>(nullable: true),
                    EducationPlanItemId = table.Column<int>(nullable: true),
                    ExaminationTest = table.Column<bool>(nullable: true),
                    IndependentWorkHours = table.Column<int>(nullable: false),
                    LaboratoryHours = table.Column<int>(nullable: false),
                    LectionHours = table.Column<int>(nullable: false),
                    PracticeHours = table.Column<int>(nullable: false),
                    SemesterNo = table.Column<int>(nullable: false),
                    SettlementWorkTest = table.Column<bool>(nullable: true),
                    Zet = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationPlanGraphics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationPlanGraphics_EducationPlanItems_EducationPlanItemId",
                        column: x => x.EducationPlanItemId,
                        principalTable: "EducationPlanItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationPlanGraphics_EducationPlanItemId",
                table: "EducationPlanGraphics",
                column: "EducationPlanItemId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationPlanItems_ComponentId",
                table: "EducationPlanItems",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationPlanItems_DisciplineCycleId",
                table: "EducationPlanItems",
                column: "DisciplineCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationPlanItems_DisciplineId",
                table: "EducationPlanItems",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationPlanItems_EducationCompetenceId",
                table: "EducationPlanItems",
                column: "EducationCompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationPlanItems_EducationPlanId",
                table: "EducationPlanItems",
                column: "EducationPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlans_EducationProgramTypes_EducationProgramTypeId",
                table: "EducationPlans",
                column: "EducationProgramTypeId",
                principalTable: "EducationProgramTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlans_EducationProgramTypes_EducationProgramTypeId",
                table: "EducationPlans");

            migrationBuilder.DropTable(
                name: "EducationPlanGraphics");

            migrationBuilder.DropTable(
                name: "EducationPlanItems");

            migrationBuilder.DropTable(
                name: "EducationPlanCompoments");

            migrationBuilder.DropColumn(
                name: "TrainingPeriod",
                table: "EducationPlans");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Disciplines");

            migrationBuilder.RenameColumn(
                name: "EducationProgramTypeId",
                table: "EducationPlans",
                newName: "EducationLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_EducationPlans_EducationProgramTypeId",
                table: "EducationPlans",
                newName: "IX_EducationPlans_EducationLevelId");

            migrationBuilder.AddColumn<bool>(
                name: "IsAcceleratedLearning",
                table: "EducationPlans",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CathedraId",
                table: "Disciplines",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DisciplineCycleId",
                table: "Disciplines",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EducationCompetenceId",
                table: "Disciplines",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplines_Cathedras_CathedraId",
                table: "Disciplines",
                column: "CathedraId",
                principalTable: "Cathedras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplines_DisciplineCycles_DisciplineCycleId",
                table: "Disciplines",
                column: "DisciplineCycleId",
                principalTable: "DisciplineCycles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplines_EducationCompetences_EducationCompetenceId",
                table: "Disciplines",
                column: "EducationCompetenceId",
                principalTable: "EducationCompetences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlans_EducationLevels_EducationLevelId",
                table: "EducationPlans",
                column: "EducationLevelId",
                principalTable: "EducationLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
