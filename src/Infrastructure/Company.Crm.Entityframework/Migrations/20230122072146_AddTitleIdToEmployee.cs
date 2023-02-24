using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.Crm.Entityframework.Migrations
{
    public partial class AddTitleIdToEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TitleId",
                table: "Employee",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleId",
                table: "Employee");
        }
    }
}
