using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminAssistant.Infra.DAL.EntityFramework.Migrations
{
    public partial class V0o0o1o0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bank",
                schema: "Accounts",
                columns: table => new
                {
                    BankID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.BankID);
                });

            migrationBuilder.UpdateData(
                schema: "Accounts",
                table: "BankAccountType",
                keyColumn: "BankAccountTypeID",
                keyValue: 1,
                columns: new[] { "AllowCompany", "AllowPersonal" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                schema: "Accounts",
                table: "BankAccountType",
                keyColumn: "BankAccountTypeID",
                keyValue: 2,
                columns: new[] { "AllowCompany", "AllowPersonal" },
                values: new object[] { true, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bank",
                schema: "Accounts");

            migrationBuilder.UpdateData(
                schema: "Accounts",
                table: "BankAccountType",
                keyColumn: "BankAccountTypeID",
                keyValue: 1,
                columns: new[] { "AllowCompany", "AllowPersonal" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                schema: "Accounts",
                table: "BankAccountType",
                keyColumn: "BankAccountTypeID",
                keyValue: 2,
                columns: new[] { "AllowCompany", "AllowPersonal" },
                values: new object[] { true, true });
        }
    }
}
