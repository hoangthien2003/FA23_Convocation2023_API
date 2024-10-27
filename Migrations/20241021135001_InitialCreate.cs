using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FA23_Convocation2023_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bachelor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    Mail = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    Faculty = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    Major = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    Image = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    Status = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    StatusBaChelor = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    HallName = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    SessionNum = table.Column<int>(type: "int", nullable: true),
                    Chair = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ChairParent = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CheckIn = table.Column<bool>(type: "bit", nullable: true),
                    TimeCheckIn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Bachelor__3214EC07BBA70F7D", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CheckIn",
                columns: table => new
                {
                    CheckinID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HallName = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    SessionNum = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckIn", x => x.CheckinID);
                });

            migrationBuilder.CreateTable(
                name: "Hall",
                columns: table => new
                {
                    HallId = table.Column<int>(type: "int", nullable: false),
                    HallName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Hall__7E60E2149F83D79B", x => x.HallId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    RoleName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    Session = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Session__C9F492901585092D", x => x.SessionId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    Password = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    RoleID = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Roles",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bachelor");

            migrationBuilder.DropTable(
                name: "CheckIn");

            migrationBuilder.DropTable(
                name: "Hall");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
