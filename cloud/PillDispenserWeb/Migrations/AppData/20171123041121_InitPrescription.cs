using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PillDispenserWeb.Migrations.AppData
{
    public partial class InitPrescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    PrescriptionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: true),
                    PrescribingDoctorDoctorId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.PrescriptionId);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Doctors_PrescribingDoctorDoctorId",
                        column: x => x.PrescribingDoctorDoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recurrence",
                columns: table => new
                {
                    RecurrenceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    End = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Interval = table.Column<TimeSpan>(type: "time", nullable: false),
                    PrescriptionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Start = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recurrence", x => x.RecurrenceId);
                    table.ForeignKey(
                        name: "FK_Recurrence_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "PrescriptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PatientId",
                table: "Prescriptions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PrescribingDoctorDoctorId",
                table: "Prescriptions",
                column: "PrescribingDoctorDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Recurrence_PrescriptionId",
                table: "Recurrence",
                column: "PrescriptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recurrence");

            migrationBuilder.DropTable(
                name: "Prescriptions");
        }
    }
}
