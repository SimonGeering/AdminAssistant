using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminAssistant.Infra.DAL.EntityFramework.Migrations
{
    public partial class V0o0o2o0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                schema: "Documents",
                table: "Document",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

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
            migrationBuilder.DropColumn(
                name: "FileName",
                schema: "Documents",
                table: "Document");

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
