using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GeneralInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class basetablescreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "idea");

            migrationBuilder.CreateTable(
                name: "ideas",
                schema: "idea",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    body = table.Column<string>(type: "text", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ideas", x => x.id);
                    table.ForeignKey(
                        name: "FK_ideas_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "user",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "idea_comments",
                schema: "idea",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idea_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: false),
                    comment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idea_comments", x => x.id);
                    table.ForeignKey(
                        name: "FK_idea_comments_ideas_idea_id",
                        column: x => x.idea_id,
                        principalSchema: "idea",
                        principalTable: "ideas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_idea_comments_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "user",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "idea_files",
                schema: "idea",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idea_id = table.Column<int>(type: "integer", nullable: false),
                    file_path = table.Column<string>(type: "text", nullable: false),
                    file_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idea_files", x => x.id);
                    table.ForeignKey(
                        name: "FK_idea_files_ideas_idea_id",
                        column: x => x.idea_id,
                        principalSchema: "idea",
                        principalTable: "ideas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "idea_rates",
                schema: "idea",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idea_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    idea_mark = table.Column<int>(type: "integer", nullable: false),
                    rate_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idea_rates", x => x.id);
                    table.ForeignKey(
                        name: "FK_idea_rates_ideas_idea_id",
                        column: x => x.idea_id,
                        principalSchema: "idea",
                        principalTable: "ideas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_idea_rates_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "user",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_idea_comments_idea_id",
                schema: "idea",
                table: "idea_comments",
                column: "idea_id");

            migrationBuilder.CreateIndex(
                name: "IX_idea_comments_user_id",
                schema: "idea",
                table: "idea_comments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_idea_files_idea_id",
                schema: "idea",
                table: "idea_files",
                column: "idea_id");

            migrationBuilder.CreateIndex(
                name: "IX_idea_rates_idea_id",
                schema: "idea",
                table: "idea_rates",
                column: "idea_id");

            migrationBuilder.CreateIndex(
                name: "IX_idea_rates_user_id",
                schema: "idea",
                table: "idea_rates",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ideas_user_id",
                schema: "idea",
                table: "ideas",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "idea_comments",
                schema: "idea");

            migrationBuilder.DropTable(
                name: "idea_files",
                schema: "idea");

            migrationBuilder.DropTable(
                name: "idea_rates",
                schema: "idea");

            migrationBuilder.DropTable(
                name: "ideas",
                schema: "idea");
        }
    }
}
