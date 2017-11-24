using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PillDispenserWeb.Migrations.AppData
{
    public partial class InitDose : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dose",
                columns: table => new
                {
                    DoseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssociatedRecurrenceRecurrenceId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TimeTaken = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dose", x => x.DoseId);
                    table.ForeignKey(
                        name: "FK_Dose_Recurrence_AssociatedRecurrenceRecurrenceId",
                        column: x => x.AssociatedRecurrenceRecurrenceId,
                        principalTable: "Recurrence",
                        principalColumn: "RecurrenceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dose_AssociatedRecurrenceRecurrenceId",
                table: "Dose",
                column: "AssociatedRecurrenceRecurrenceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dose");
        }
    }
}
