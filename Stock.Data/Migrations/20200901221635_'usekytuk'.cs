using Microsoft.EntityFrameworkCore.Migrations;

namespace Stock.Data.Migrations
{
    public partial class usekytuk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BelongedUserId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BelongedUserId",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }
    }
}
