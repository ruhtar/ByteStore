using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenticationService.Migrations
{
    public partial class OrderItemNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemsInfo",
                table: "Orders");

            migrationBuilder.AddColumn<byte[]>(
                name: "OrderItems",
                table: "Orders",
                type: "longblob",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderItems",
                table: "Orders");

            migrationBuilder.AddColumn<byte[]>(
                name: "ItemsInfo",
                table: "Orders",
                type: "longblob",
                nullable: false);
        }
    }
}
