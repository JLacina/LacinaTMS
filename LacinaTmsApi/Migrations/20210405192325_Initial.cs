using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LacinaTmsApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainTask",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    FinishDate = table.Column<DateTime>(nullable: false),
                    State = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainTask", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    FinishDate = table.Column<DateTime>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    MainTaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubTasks_MainTask_MainTaskId",
                        column: x => x.MainTaskId,
                        principalTable: "MainTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MainTask",
                columns: new[] { "Id", "Description", "FinishDate", "Name", "StartDate", "State" },
                values: new object[] { 1, "Code the EF part of the project", new DateTime(2021, 4, 5, 0, 0, 0, 0, DateTimeKind.Local), "Code Entity Framework task", new DateTime(2021, 4, 5, 20, 23, 25, 68, DateTimeKind.Local).AddTicks(7897), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_MainTask_Id",
                table: "MainTask",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SubTasks_Id",
                table: "SubTasks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SubTasks_MainTaskId",
                table: "SubTasks",
                column: "MainTaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubTasks");

            migrationBuilder.DropTable(
                name: "MainTask");
        }
    }
}
