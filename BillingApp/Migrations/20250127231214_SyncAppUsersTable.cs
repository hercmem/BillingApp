using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillingApp.Migrations
{
    /// <inheritdoc />
    public partial class SyncAppUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "app_users",
                columns: table => new
                {
                    User_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Last_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Property = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, defaultValue: "example@example.com"),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false, defaultValue: "defaultpassword")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_users", x => x.User_Id);
                });

            migrationBuilder.CreateTable(
                name: "Calls",
                columns: table => new
                {
                    Call_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calls", x => x.Call_ID);
                });

            migrationBuilder.CreateTable(
                name: "phonePrograms",
                columns: table => new
                {
                    programName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    benfits = table.Column<string>(type: "text", nullable: false),
                    Charge = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_programs", x => x.programName);
                });

            migrationBuilder.CreateTable(
                name: "admin",
                columns: table => new
                {
                    Admin_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin", x => x.Admin_id);
                    table.ForeignKey(
                        name: "FK_admin_app_users",
                        column: x => x.User_id,
                        principalTable: "app_users",
                        principalColumn: "User_Id");
                });

            migrationBuilder.CreateTable(
                name: "sellers",
                columns: table => new
                {
                    Seller_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sellers", x => x.Seller_id);
                    table.ForeignKey(
                        name: "FK_sellers_app_users",
                        column: x => x.User_id,
                        principalTable: "app_users",
                        principalColumn: "User_Id");
                });

            migrationBuilder.CreateTable(
                name: "phones",
                columns: table => new
                {
                    phoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    programName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phones", x => x.phoneNumber);
                    table.ForeignKey(
                        name: "FK_phones_programs",
                        column: x => x.programName,
                        principalTable: "phonePrograms",
                        principalColumn: "programName");
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Bill_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    phoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    Costs = table.Column<decimal>(type: "decimal(7,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Bill_ID);
                    table.ForeignKey(
                        name: "FK_Bills_phones1",
                        column: x => x.phoneNumber,
                        principalTable: "phones",
                        principalColumn: "phoneNumber");
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    Client_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AFM = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    phoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    User_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.Client_id);
                    table.ForeignKey(
                        name: "FK_clients_app_users",
                        column: x => x.User_id,
                        principalTable: "app_users",
                        principalColumn: "User_Id");
                    table.ForeignKey(
                        name: "FK_clients_phones1",
                        column: x => x.phoneNumber,
                        principalTable: "phones",
                        principalColumn: "phoneNumber");
                });

            migrationBuilder.CreateTable(
                name: "BillsCalls",
                columns: table => new
                {
                    Bill_ID = table.Column<int>(type: "int", nullable: false),
                    Call_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_BillsCalls_Bills",
                        column: x => x.Bill_ID,
                        principalTable: "Bills",
                        principalColumn: "Bill_ID");
                    table.ForeignKey(
                        name: "FK_BillsCalls_Calls",
                        column: x => x.Call_ID,
                        principalTable: "Calls",
                        principalColumn: "Call_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_admin_User_id",
                table: "admin",
                column: "User_id");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_phoneNumber",
                table: "Bills",
                column: "phoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_BillsCalls_Bill_ID",
                table: "BillsCalls",
                column: "Bill_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BillsCalls_Call_ID",
                table: "BillsCalls",
                column: "Call_ID");

            migrationBuilder.CreateIndex(
                name: "IX_clients_phoneNumber",
                table: "clients",
                column: "phoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_clients_User_id",
                table: "clients",
                column: "User_id");

            migrationBuilder.CreateIndex(
                name: "IX_phones_programName",
                table: "phones",
                column: "programName");

            migrationBuilder.CreateIndex(
                name: "IX_sellers_User_id",
                table: "sellers",
                column: "User_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin");

            migrationBuilder.DropTable(
                name: "BillsCalls");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "sellers");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Calls");

            migrationBuilder.DropTable(
                name: "app_users");

            migrationBuilder.DropTable(
                name: "phones");

            migrationBuilder.DropTable(
                name: "phonePrograms");
        }
    }
}
