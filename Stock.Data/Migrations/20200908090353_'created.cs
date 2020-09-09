using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stock.Data.Migrations
{
    public partial class created : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderPruducts",
                table: "OrderPruducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderPruducts_OrderId",
                table: "OrderPruducts");

            migrationBuilder.DropColumn(
                name: "Favicon",
                table: "Regulations");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Regulations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderPruducts");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "OrderPruducts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "OrderPruducts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderPruducts",
                table: "OrderPruducts",
                columns: new[] { "OrderId", "PruductId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderPruducts",
                table: "OrderPruducts");

            migrationBuilder.AddColumn<string>(
                name: "Favicon",
                table: "Regulations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Regulations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "OrderPruducts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "OrderPruducts",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "OrderPruducts",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderPruducts",
                table: "OrderPruducts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPruducts_OrderId",
                table: "OrderPruducts",
                column: "OrderId");
        }
    }
}
