using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.Crm.Entityframework.Migrations
{
    public partial class UpdateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LST",
                table: "TaskStatus",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "SaleAmount",
                table: "Sale",
                type: "decimal(12,2)",
                precision: 12,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Notification",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 7, 20, 33, 25, 30, DateTimeKind.Utc).AddTicks(6805),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 5, 21, 21, 7, 738, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.CreateIndex(
                name: "IX_Customer_GenderId",
                table: "Customer",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Gender_GenderId",
                table: "Customer",
                column: "GenderId",
                principalSchema: "LST",
                principalTable: "Gender",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Gender_GenderId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_GenderId",
                table: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LST",
                table: "TaskStatus",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<decimal>(
                name: "SaleAmount",
                table: "Sale",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,2)",
                oldPrecision: 12,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Notification",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 5, 21, 21, 7, 738, DateTimeKind.Utc).AddTicks(2326),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 7, 20, 33, 25, 30, DateTimeKind.Utc).AddTicks(6805));
        }
    }
}
