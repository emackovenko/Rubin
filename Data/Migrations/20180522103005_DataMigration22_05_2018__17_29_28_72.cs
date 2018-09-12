using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Data.Migrations
{
    public partial class DataMigration22_05_2018__17_29_28_72 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAstuAccessRequired",
                table: "Roles",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsWorkOkAccessRequired",
                table: "Roles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAstuAccessRequired",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IsWorkOkAccessRequired",
                table: "Roles");
        }
    }
}
