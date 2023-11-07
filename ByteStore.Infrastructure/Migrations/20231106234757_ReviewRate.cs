using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteStore.Infrastructure.Migrations
{
    public partial class ReviewRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "Reviews",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Reviews");
        }
    }
}
