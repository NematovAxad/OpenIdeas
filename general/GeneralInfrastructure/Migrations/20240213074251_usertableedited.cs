using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeneralInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class usertableedited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "first_name",
                schema: "user",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                schema: "user",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "first_name",
                schema: "user",
                table: "user");

            migrationBuilder.DropColumn(
                name: "last_name",
                schema: "user",
                table: "user");
        }
    }
}
