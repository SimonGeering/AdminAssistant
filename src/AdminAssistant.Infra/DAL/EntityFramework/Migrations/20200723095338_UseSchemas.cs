using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminAssistant.Infra.DAL.EntityFramework.Migrations
{
    public partial class UseSchemas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Accounts");

            migrationBuilder.EnsureSchema(
                name: "AssetRegister");

            migrationBuilder.EnsureSchema(
                name: "Core");

            migrationBuilder.EnsureSchema(
                name: "Budget");

            migrationBuilder.EnsureSchema(
                name: "Contacts");

            migrationBuilder.EnsureSchema(
                name: "Documents");

            migrationBuilder.RenameTable(
                name: "UserProfileSetting",
                newName: "UserProfileSetting",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "UserProfilePermission",
                newName: "UserProfilePermission",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "UserProfile",
                newName: "UserProfile",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "Setting",
                newName: "Setting",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "PersonalDetails",
                newName: "PersonalDetails",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "Permission",
                newName: "Permission",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "Payee",
                newName: "Payee",
                newSchema: "Accounts");

            migrationBuilder.RenameTable(
                name: "Owner",
                newName: "Owner",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "Document",
                newName: "Document",
                newSchema: "Documents");

            migrationBuilder.RenameTable(
                name: "Currency",
                newName: "Currency",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "ContactAddress",
                newName: "ContactAddress",
                newSchema: "Contacts");

            migrationBuilder.RenameTable(
                name: "Contact",
                newName: "Contact",
                newSchema: "Contacts");

            migrationBuilder.RenameTable(
                name: "Company",
                newName: "Company",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "BudgetEntry",
                newName: "BudgetEntry",
                newSchema: "Budget");

            migrationBuilder.RenameTable(
                name: "Budget",
                newName: "Budget",
                newSchema: "Budget");

            migrationBuilder.RenameTable(
                name: "BankAccountType",
                newName: "BankAccountType",
                newSchema: "Accounts");

            migrationBuilder.RenameTable(
                name: "BankAccountTransaction",
                newName: "BankAccountTransaction",
                newSchema: "Accounts");

            migrationBuilder.RenameTable(
                name: "BankAccount",
                newName: "BankAccount",
                newSchema: "Accounts");

            migrationBuilder.RenameTable(
                name: "Audit",
                newName: "Audit",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "Asset",
                newName: "Asset",
                newSchema: "AssetRegister");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Address",
                newSchema: "Contacts");

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
            migrationBuilder.RenameTable(
                name: "Document",
                schema: "Documents",
                newName: "Document");

            migrationBuilder.RenameTable(
                name: "UserProfileSetting",
                schema: "Core",
                newName: "UserProfileSetting");

            migrationBuilder.RenameTable(
                name: "UserProfilePermission",
                schema: "Core",
                newName: "UserProfilePermission");

            migrationBuilder.RenameTable(
                name: "UserProfile",
                schema: "Core",
                newName: "UserProfile");

            migrationBuilder.RenameTable(
                name: "Setting",
                schema: "Core",
                newName: "Setting");

            migrationBuilder.RenameTable(
                name: "PersonalDetails",
                schema: "Core",
                newName: "PersonalDetails");

            migrationBuilder.RenameTable(
                name: "Permission",
                schema: "Core",
                newName: "Permission");

            migrationBuilder.RenameTable(
                name: "Owner",
                schema: "Core",
                newName: "Owner");

            migrationBuilder.RenameTable(
                name: "Currency",
                schema: "Core",
                newName: "Currency");

            migrationBuilder.RenameTable(
                name: "Company",
                schema: "Core",
                newName: "Company");

            migrationBuilder.RenameTable(
                name: "Audit",
                schema: "Core",
                newName: "Audit");

            migrationBuilder.RenameTable(
                name: "ContactAddress",
                schema: "Contacts",
                newName: "ContactAddress");

            migrationBuilder.RenameTable(
                name: "Contact",
                schema: "Contacts",
                newName: "Contact");

            migrationBuilder.RenameTable(
                name: "Address",
                schema: "Contacts",
                newName: "Address");

            migrationBuilder.RenameTable(
                name: "BudgetEntry",
                schema: "Budget",
                newName: "BudgetEntry");

            migrationBuilder.RenameTable(
                name: "Budget",
                schema: "Budget",
                newName: "Budget");

            migrationBuilder.RenameTable(
                name: "Asset",
                schema: "AssetRegister",
                newName: "Asset");

            migrationBuilder.RenameTable(
                name: "Payee",
                schema: "Accounts",
                newName: "Payee");

            migrationBuilder.RenameTable(
                name: "BankAccountType",
                schema: "Accounts",
                newName: "BankAccountType");

            migrationBuilder.RenameTable(
                name: "BankAccountTransaction",
                schema: "Accounts",
                newName: "BankAccountTransaction");

            migrationBuilder.RenameTable(
                name: "BankAccount",
                schema: "Accounts",
                newName: "BankAccount");

            migrationBuilder.UpdateData(
                table: "BankAccountType",
                keyColumn: "BankAccountTypeID",
                keyValue: 1,
                columns: new[] { "AllowCompany", "AllowPersonal" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "BankAccountType",
                keyColumn: "BankAccountTypeID",
                keyValue: 2,
                columns: new[] { "AllowCompany", "AllowPersonal" },
                values: new object[] { true, true });
        }
    }
}
