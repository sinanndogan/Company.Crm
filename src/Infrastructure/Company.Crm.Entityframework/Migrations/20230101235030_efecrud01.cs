using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.Crm.Entityframework.Migrations
{
    public partial class efecrud01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Notification",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 1, 23, 50, 30, 470, DateTimeKind.Utc).AddTicks(4798),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 10, 11, 6, 12, 50, DateTimeKind.Utc).AddTicks(9464));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Notification",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 10, 11, 6, 12, 50, DateTimeKind.Utc).AddTicks(9464),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 1, 23, 50, 30, 470, DateTimeKind.Utc).AddTicks(4798));
        }
    }
}
