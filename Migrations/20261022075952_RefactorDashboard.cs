using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskFlow.Migrations
{
    /// <inheritdoc />
    public partial class RefactorDashboard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskModelDashboard_Dashboards_DashboardId",
                table: "TaskModelDashboard");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskModelDashboard_Dashboards_DashboardId1",
                table: "TaskModelDashboard");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskModelDashboard_Dashboards_DashboardId2",
                table: "TaskModelDashboard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskModelDashboard",
                table: "TaskModelDashboard");

            migrationBuilder.DropIndex(
                name: "IX_TaskModelDashboard_DashboardId",
                table: "TaskModelDashboard");

            migrationBuilder.DropIndex(
                name: "IX_TaskModelDashboard_DashboardId1",
                table: "TaskModelDashboard");

            migrationBuilder.DropIndex(
                name: "IX_TaskModelDashboard_DashboardId2",
                table: "TaskModelDashboard");

            migrationBuilder.DropColumn(
                name: "DashboardId1",
                table: "TaskModelDashboard");

            migrationBuilder.DropColumn(
                name: "DashboardId2",
                table: "TaskModelDashboard");

            migrationBuilder.RenameTable(
                name: "TaskModelDashboard",
                newName: "DashboardTodos");

            migrationBuilder.Sql("DELETE FROM DashboardTodos;");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "DashboardTodos",
                newName: "Priority");

            migrationBuilder.AlterColumn<int>(
                name: "DashboardId",
                table: "DashboardTodos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "DashboardTodos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TodoId",
                table: "DashboardTodos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DashboardTodos",
                table: "DashboardTodos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_UserId",
                table: "Dashboards",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DashboardTodos_DashboardId_TodoId",
                table: "DashboardTodos",
                columns: new[] { "DashboardId", "TodoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DashboardTodos_TodoId",
                table: "DashboardTodos",
                column: "TodoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboards_Users_UserId",
                table: "Dashboards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DashboardTodos_Dashboards_DashboardId",
                table: "DashboardTodos",
                column: "DashboardId",
                principalTable: "Dashboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DashboardTodos_Todos_TodoId",
                table: "DashboardTodos",
                column: "TodoId",
                principalTable: "Todos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dashboards_Users_UserId",
                table: "Dashboards");

            migrationBuilder.DropForeignKey(
                name: "FK_DashboardTodos_Dashboards_DashboardId",
                table: "DashboardTodos");

            migrationBuilder.DropForeignKey(
                name: "FK_DashboardTodos_Todos_TodoId",
                table: "DashboardTodos");

            migrationBuilder.DropIndex(
                name: "IX_Dashboards_UserId",
                table: "Dashboards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DashboardTodos",
                table: "DashboardTodos");

            migrationBuilder.DropIndex(
                name: "IX_DashboardTodos_DashboardId_TodoId",
                table: "DashboardTodos");

            migrationBuilder.DropIndex(
                name: "IX_DashboardTodos_TodoId",
                table: "DashboardTodos");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "DashboardTodos");

            migrationBuilder.DropColumn(
                name: "TodoId",
                table: "DashboardTodos");

            migrationBuilder.RenameTable(
                name: "DashboardTodos",
                newName: "TaskModelDashboard");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "TaskModelDashboard",
                newName: "Text");

            migrationBuilder.AlterColumn<int>(
                name: "DashboardId",
                table: "TaskModelDashboard",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DashboardId1",
                table: "TaskModelDashboard",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DashboardId2",
                table: "TaskModelDashboard",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskModelDashboard",
                table: "TaskModelDashboard",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_TaskModelDashboard_Dashboards_DashboardId",
                table: "TaskModelDashboard",
                column: "DashboardId",
                principalTable: "Dashboards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskModelDashboard_Dashboards_DashboardId1",
                table: "TaskModelDashboard",
                column: "DashboardId1",
                principalTable: "Dashboards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskModelDashboard_Dashboards_DashboardId2",
                table: "TaskModelDashboard",
                column: "DashboardId2",
                principalTable: "Dashboards",
                principalColumn: "Id");
        }
    }
}
