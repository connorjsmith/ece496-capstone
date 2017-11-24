using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PillDispenserWeb.Migrations.AppData
{
    public partial class PrescriptionAddMedicationField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MedicationId",
                table: "Prescriptions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_MedicationId",
                table: "Prescriptions",
                column: "MedicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Medications_MedicationId",
                table: "Prescriptions",
                column: "MedicationId",
                principalTable: "Medications",
                principalColumn: "MedicationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Medications_MedicationId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_MedicationId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "MedicationId",
                table: "Prescriptions");
        }
    }
}
