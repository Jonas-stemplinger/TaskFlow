using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskFlow.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamProjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamprojectId",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isTeamProject",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Teamprojects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTeamProject = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teamprojects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTeamproject",
                columns: table => new
                {
                    TeamprojectsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTeamproject", x => new { x.TeamprojectsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AppUserTeamproject_Teamprojects_TeamprojectsId",
                        column: x => x.TeamprojectsId,
                        principalTable: "Teamprojects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserTeamproject_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TeamprojectId",
                table: "Tasks",
                column: "TeamprojectId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserTeamproject_UsersId",
                table: "AppUserTeamproject",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Teamprojects_TeamprojectId",
                table: "Tasks",
                column: "TeamprojectId",
                principalTable: "Teamprojects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Teamprojects_TeamprojectId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "AppUserTeamproject");

            migrationBuilder.DropTable(
                name: "Teamprojects");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TeamprojectId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TeamprojectId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "isTeamProject",
                table: "Projects");
        }
    }
}
