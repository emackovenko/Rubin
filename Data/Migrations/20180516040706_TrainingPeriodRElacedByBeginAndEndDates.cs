using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Data.Migrations
{
    public partial class TrainingPeriodRElacedByBeginAndEndDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrainingPeriod",
                table: "EducationPlans");

            migrationBuilder.AddColumn<DateTime>(
                name: "BeginDate",
                table: "EducationPlans",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "EducationPlans",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeginDate",
                table: "EducationPlans");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "EducationPlans");

            migrationBuilder.AddColumn<string>(
                name: "TrainingPeriod",
                table: "EducationPlans",
                nullable: true);
        }
    }
}
