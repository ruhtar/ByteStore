using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteStore.Infrastructure.Migrations
{
    public partial class PurchaseHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PurchaseHistory",
                table: "UserAggregates",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseHistory",
                table: "UserAggregates");
        }
    }
}
