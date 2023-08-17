using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogYes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Soft_Delete_Time : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "User",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "Role",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "Comment",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "Category",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "Blog",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "Blog");
        }
    }
}