using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogYes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UniqueComposite_Index_Of_SoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Roles_Name",
                table: "Roles");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("49450bf9-e270-4293-8bca-0fb0c11db70e"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DeleteTime", "DisplayName", "Email", "Passphrase", "RegisterTime", "RoleId", "Salt", "SoftDeleted", "TelephoneNumber", "Username" },
                values: new object[] { new Guid("b3fd20dc-f1fe-4bc4-99ea-506e05efd19b"), null, "developer", "unknow", "Uh+8E9ft9jptdMzAVRKo0UYQtqn5epsbJUZQGbL/Xhk=", new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("e8df3280-8ab1-4b45-8d6a-6c3e669317ac"), "5+fPPv0FShtKo3ed746TiuNojEZsxuPkhbU+YvF5DuQ=", false, "unknow", "dev" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username_SoftDeleted",
                table: "Users",
                columns: new[] { "Username", "SoftDeleted" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name_SoftDeleted",
                table: "Roles",
                columns: new[] { "Name", "SoftDeleted" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Username_SoftDeleted",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Roles_Name_SoftDeleted",
                table: "Roles");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b3fd20dc-f1fe-4bc4-99ea-506e05efd19b"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DeleteTime", "DisplayName", "Email", "Passphrase", "RegisterTime", "RoleId", "Salt", "SoftDeleted", "TelephoneNumber", "Username" },
                values: new object[] { new Guid("49450bf9-e270-4293-8bca-0fb0c11db70e"), null, "developer", "unknow", "Uh+8E9ft9jptdMzAVRKo0UYQtqn5epsbJUZQGbL/Xhk=", new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("e8df3280-8ab1-4b45-8d6a-6c3e669317ac"), "5+fPPv0FShtKo3ed746TiuNojEZsxuPkhbU+YvF5DuQ=", false, "unknow", "dev" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);
        }
    }
}
