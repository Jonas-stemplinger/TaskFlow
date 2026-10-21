using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskFlow.Migrations
{
    /// <inheritdoc />
    public partial class AddDashboard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dashboards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dashboards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskModelDashboard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DashboardId = table.Column<int>(type: "int", nullable: true),
                    DashboardId1 = table.Column<int>(type: "int", nullable: true),
                    DashboardId2 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskModelDashboard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskModelDashboard_Dashboards_DashboardId",
                        column: x => x.DashboardId,
                        principalTable: "Dashboards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskModelDashboard_Dashboards_DashboardId1",
                        column: x => x.DashboardId1,
                        principalTable: "Dashboards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskModelDashboard_Dashboards_DashboardId2",
                        column: x => x.DashboardId2,
                        principalTable: "Dashboards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskModelDashboard_DashboardId",
                table: "TaskModelDashboard",
                column: "DashboardId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskModelDashboard_DashboardId1",
                table: "TaskModelDashboard",
                column: "DashboardId1");

            migrationBuilder.CreateIndex(
                name: "IX_TaskModelDashboard_DashboardId2",
                table: "TaskModelDashboard",
                column: "DashboardId2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskModelDashboard");

            migrationBuilder.DropTable(
                name: "Dashboards");
        }
    }
}
