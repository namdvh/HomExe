using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomExe.Data.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PtCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PtCategory", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "RecipeeCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeeCategory", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Height = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Recipee",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pictures = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Recipe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipee", x => x.RecipeId);
                    table.ForeignKey(
                        name: "FK_Recipee_RecipeeCategory",
                        column: x => x.CategoryId,
                        principalTable: "RecipeeCategory",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateTable(
                name: "PT",
                columns: table => new
                {
                    PtId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    LinkMeet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Certificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Schedules = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PT", x => x.PtId);
                    table.ForeignKey(
                        name: "FK_PT_PtCategory",
                        column: x => x.CategoryId,
                        principalTable: "PtCategory",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK_PT_Role",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "HealthReport",
                columns: table => new
                {
                    HealthId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Problems = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthReport", x => x.HealthId);
                    table.ForeignKey(
                        name: "FK_HealthReport_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PtId = table.Column<int>(type: "int", nullable: false),
                    Created_date = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    End_date = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Schedule = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Contract_PT",
                        column: x => x.PtId,
                        principalTable: "PT",
                        principalColumn: "PtId");
                    table.ForeignKey(
                        name: "FK_Contract_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    VideoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PtId = table.Column<int>(type: "int", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.VideoId);
                    table.ForeignKey(
                        name: "FK_Video_PT",
                        column: x => x.PtId,
                        principalTable: "PT",
                        principalColumn: "PtId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_PtId",
                table: "Contract",
                column: "PtId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthReport_UserId",
                table: "HealthReport",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PT_CategoryId",
                table: "PT",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PT_RoleId",
                table: "PT",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipee_CategoryId",
                table: "Recipee",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_PtId",
                table: "Video",
                column: "PtId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "HealthReport");

            migrationBuilder.DropTable(
                name: "Recipee");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "RecipeeCategory");

            migrationBuilder.DropTable(
                name: "PT");

            migrationBuilder.DropTable(
                name: "PtCategory");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
