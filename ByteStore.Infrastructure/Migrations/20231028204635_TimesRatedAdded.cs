using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteStore.Infrastructure.Migrations
{
    public partial class TimesRatedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimesRated",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimesRated",
                table: "Products");
        }
    }
}
