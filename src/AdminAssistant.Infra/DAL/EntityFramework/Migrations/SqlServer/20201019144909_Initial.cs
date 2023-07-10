#pragma warning disable IDE0053 // Use expression body for lambda expressions
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminAssistant.Infra.DAL.EntityFramework.Migrations.SqlServer
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Contacts");

            migrationBuilder.EnsureSchema(
                name: "AssetRegister");

            migrationBuilder.EnsureSchema(
                name: "Core");

            migrationBuilder.EnsureSchema(
                name: "Accounts");

            migrationBuilder.EnsureSchema(
                name: "Budget");

            migrationBuilder.EnsureSchema(
                name: "Documents");

            migrationBuilder.CreateTable(
                name: "Audit",
                schema: "Core",
                columns: table => new
                {
                    AuditID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ArchivedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArchivedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => x.AuditID);
                });

            migrationBuilder.CreateTable(
                name: "Bank",
                schema: "Accounts",
                columns: table => new
                {
                    BankID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.BankID);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountType",
                schema: "Accounts",
                columns: table => new
                {
                    BankAccountTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AllowPersonal = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AllowCompany = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsDeprecated = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountType", x => x.BankAccountTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ContactAddress",
                schema: "Contacts",
                columns: table => new
                {
                    ContactAddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressID = table.Column<int>(type: "int", nullable: false),
                    ContactID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactAddress", x => x.ContactAddressID);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                schema: "Core",
                columns: table => new
                {
                    CurrencyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "CHAR(3)", maxLength: 3, nullable: false),
                    DecimalFormat = table.Column<string>(type: "CHAR(5)", maxLength: 5, nullable: false),
                    IsDeprecated = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.CurrencyID);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "Core",
                columns: table => new
                {
                    PermissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionKey = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.PermissionID);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                schema: "Core",
                columns: table => new
                {
                    SettingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingKey = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.SettingID);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "Contacts",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressID);
                    table.ForeignKey(
                        name: "FK_Address_Audit_AuditID",
                        column: x => x.AuditID,
                        principalSchema: "Core",
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Asset",
                schema: "AssetRegister",
                columns: table => new
                {
                    AssetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditID = table.Column<int>(type: "int", nullable: false),
                    OwnerID = table.Column<int>(type: "int", nullable: false),
                    PurchasePrice = table.Column<int>(type: "int", nullable: false),
                    DepreciatedValue = table.Column<int>(type: "int", nullable: false),
                    ReplacementCost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.AssetID);
                    table.ForeignKey(
                        name: "FK_Asset_Audit_AuditID",
                        column: x => x.AuditID,
                        principalSchema: "Core",
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountTransaction",
                schema: "Accounts",
                columns: table => new
                {
                    BankAccountTransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankAccountID = table.Column<int>(type: "int", nullable: false),
                    AuditID = table.Column<int>(type: "int", nullable: false),
                    PayeeID = table.Column<int>(type: "int", nullable: false),
                    CurrencyID = table.Column<int>(type: "int", nullable: false),
                    Credit = table.Column<int>(type: "int", nullable: false),
                    Debit = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountTransaction", x => x.BankAccountTransactionID);
                    table.ForeignKey(
                        name: "FK_BankAccountTransaction_Audit_AuditID",
                        column: x => x.AuditID,
                        principalSchema: "Core",
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Budget",
                schema: "Budget",
                columns: table => new
                {
                    BudgetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditID = table.Column<int>(type: "int", nullable: false),
                    OwnerID = table.Column<int>(type: "int", nullable: false),
                    BudgetName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budget", x => x.BudgetID);
                    table.ForeignKey(
                        name: "FK_Budget_Audit_AuditID",
                        column: x => x.AuditID,
                        principalSchema: "Core",
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BudgetEntry",
                schema: "Budget",
                columns: table => new
                {
                    BudgetEntryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BudgetID = table.Column<int>(type: "int", nullable: false),
                    AuditID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetEntry", x => x.BudgetEntryID);
                    table.ForeignKey(
                        name: "FK_BudgetEntry_Audit_AuditID",
                        column: x => x.AuditID,
                        principalSchema: "Core",
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "Contacts",
                columns: table => new
                {
                    ContactID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerID = table.Column<int>(type: "int", nullable: false),
                    TitleID = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ContactID);
                    table.ForeignKey(
                        name: "FK_Contact_Audit_AuditID",
                        column: x => x.AuditID,
                        principalSchema: "Core",
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                schema: "Documents",
                columns: table => new
                {
                    DocumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditID = table.Column<int>(type: "int", nullable: false),
                    OwnerID = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.DocumentID);
                    table.ForeignKey(
                        name: "FK_Document_Audit_AuditID",
                        column: x => x.AuditID,
                        principalSchema: "Core",
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payee",
                schema: "Accounts",
                columns: table => new
                {
                    PayeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payee", x => x.PayeeID);
                    table.ForeignKey(
                        name: "FK_Payee_Audit_AuditID",
                        column: x => x.AuditID,
                        principalSchema: "Core",
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                schema: "Core",
                columns: table => new
                {
                    UserProfileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditID = table.Column<int>(type: "int", nullable: false),
                    SignOn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MSGraphID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.UserProfileID);
                    table.ForeignKey(
                        name: "FK_UserProfile_Audit_AuditID",
                        column: x => x.AuditID,
                        principalSchema: "Core",
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                schema: "Core",
                columns: table => new
                {
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditID = table.Column<int>(type: "int", nullable: false),
                    UserProfileID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VATNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfIncorporation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyID);
                    table.ForeignKey(
                        name: "FK_Company_Audit_AuditID",
                        column: x => x.AuditID,
                        principalSchema: "Core",
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Company_UserProfile_UserProfileID",
                        column: x => x.UserProfileID,
                        principalSchema: "Core",
                        principalTable: "UserProfile",
                        principalColumn: "UserProfileID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonalDetails",
                schema: "Core",
                columns: table => new
                {
                    PersonalDetailsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditID = table.Column<int>(type: "int", nullable: false),
                    UserProfileID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDetails", x => x.PersonalDetailsID);
                    table.ForeignKey(
                        name: "FK_PersonalDetails_Audit_AuditID",
                        column: x => x.AuditID,
                        principalSchema: "Core",
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalDetails_UserProfile_UserProfileID",
                        column: x => x.UserProfileID,
                        principalSchema: "Core",
                        principalTable: "UserProfile",
                        principalColumn: "UserProfileID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfilePermission",
                schema: "Core",
                columns: table => new
                {
                    UserProfilePermissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserProfileID = table.Column<int>(type: "int", nullable: false),
                    PermissionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfilePermission", x => x.UserProfilePermissionID);
                    table.ForeignKey(
                        name: "FK_UserProfilePermission_Permission_PermissionID",
                        column: x => x.PermissionID,
                        principalSchema: "Core",
                        principalTable: "Permission",
                        principalColumn: "PermissionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfilePermission_UserProfile_UserProfileID",
                        column: x => x.UserProfileID,
                        principalSchema: "Core",
                        principalTable: "UserProfile",
                        principalColumn: "UserProfileID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileSetting",
                schema: "Core",
                columns: table => new
                {
                    UserProfileSettingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserProfileID = table.Column<int>(type: "int", nullable: false),
                    SettingID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileSetting", x => x.UserProfileSettingID);
                    table.ForeignKey(
                        name: "FK_UserProfileSetting_Setting_SettingID",
                        column: x => x.SettingID,
                        principalSchema: "Core",
                        principalTable: "Setting",
                        principalColumn: "SettingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfileSetting_UserProfile_UserProfileID",
                        column: x => x.UserProfileID,
                        principalSchema: "Core",
                        principalTable: "UserProfile",
                        principalColumn: "UserProfileID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Owner",
                schema: "Core",
                columns: table => new
                {
                    OwnerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: true),
                    PersonalDetailsID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.OwnerID);
                    table.ForeignKey(
                        name: "FK_Owner_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalSchema: "Core",
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Owner_PersonalDetails_PersonalDetailsID",
                        column: x => x.PersonalDetailsID,
                        principalSchema: "Core",
                        principalTable: "PersonalDetails",
                        principalColumn: "PersonalDetailsID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankAccount",
                schema: "Accounts",
                columns: table => new
                {
                    BankAccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditID = table.Column<int>(type: "int", nullable: false),
                    OwnerID = table.Column<int>(type: "int", nullable: false),
                    BankAccountTypeID = table.Column<int>(type: "int", nullable: false),
                    CurrencyID = table.Column<int>(type: "int", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OpeningBalance = table.Column<int>(type: "int", nullable: false),
                    CurrentBalance = table.Column<int>(type: "int", nullable: false),
                    OpenedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsBudgeted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.BankAccountID);
                    table.ForeignKey(
                        name: "FK_BankAccount_Audit_AuditID",
                        column: x => x.AuditID,
                        principalSchema: "Core",
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccount_Currency_CurrencyID",
                        column: x => x.CurrencyID,
                        principalSchema: "Core",
                        principalTable: "Currency",
                        principalColumn: "CurrencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccount_Owner_OwnerID",
                        column: x => x.OwnerID,
                        principalSchema: "Core",
                        principalTable: "Owner",
                        principalColumn: "OwnerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Accounts",
                table: "BankAccountType",
                columns: new[] { "BankAccountTypeID", "AllowCompany", "AllowPersonal", "Description" },
                values: new object[,]
                {
                    { 1, true, true, "Current Account" },
                    { 2, true, true, "Savings Account" }
                });

            migrationBuilder.InsertData(
                schema: "Core",
                table: "Currency",
                columns: new[] { "CurrencyID", "DecimalFormat", "Symbol" },
                values: new object[,]
                {
                    { 1, "2.2-2", "GBP" },
                    { 2, "2.2-2", "EUR" },
                    { 3, "2.2-2", "USD" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_AuditID",
                schema: "Contacts",
                table: "Address",
                column: "AuditID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Asset_AuditID",
                schema: "AssetRegister",
                table: "Asset",
                column: "AuditID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_AuditID",
                schema: "Accounts",
                table: "BankAccount",
                column: "AuditID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_CurrencyID",
                schema: "Accounts",
                table: "BankAccount",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_OwnerID",
                schema: "Accounts",
                table: "BankAccount",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountTransaction_AuditID",
                schema: "Accounts",
                table: "BankAccountTransaction",
                column: "AuditID");

            migrationBuilder.CreateIndex(
                name: "IX_Budget_AuditID",
                schema: "Budget",
                table: "Budget",
                column: "AuditID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetEntry_AuditID",
                schema: "Budget",
                table: "BudgetEntry",
                column: "AuditID");

            migrationBuilder.CreateIndex(
                name: "IX_Company_AuditID",
                schema: "Core",
                table: "Company",
                column: "AuditID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_UserProfileID",
                schema: "Core",
                table: "Company",
                column: "UserProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_AuditID",
                schema: "Contacts",
                table: "Contact",
                column: "AuditID");

            migrationBuilder.CreateIndex(
                name: "IX_Document_AuditID",
                schema: "Documents",
                table: "Document",
                column: "AuditID");

            migrationBuilder.CreateIndex(
                name: "IX_Owner_CompanyID_PersonalDetailsID",
                schema: "Core",
                table: "Owner",
                columns: new[] { "CompanyID", "PersonalDetailsID" },
                unique: true,
                filter: "[CompanyID] IS NOT NULL AND [PersonalDetailsID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Owner_PersonalDetailsID",
                schema: "Core",
                table: "Owner",
                column: "PersonalDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_Payee_AuditID",
                schema: "Accounts",
                table: "Payee",
                column: "AuditID");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_PermissionKey",
                schema: "Core",
                table: "Permission",
                column: "PermissionKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetails_AuditID",
                schema: "Core",
                table: "PersonalDetails",
                column: "AuditID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetails_UserProfileID",
                schema: "Core",
                table: "PersonalDetails",
                column: "UserProfileID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Setting_SettingKey",
                schema: "Core",
                table: "Setting",
                column: "SettingKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_AuditID",
                schema: "Core",
                table: "UserProfile",
                column: "AuditID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_SignOn",
                schema: "Core",
                table: "UserProfile",
                column: "SignOn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilePermission_PermissionID",
                schema: "Core",
                table: "UserProfilePermission",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilePermission_UserProfileID_PermissionID",
                schema: "Core",
                table: "UserProfilePermission",
                columns: new[] { "UserProfileID", "PermissionID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileSetting_SettingID",
                schema: "Core",
                table: "UserProfileSetting",
                column: "SettingID");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileSetting_UserProfileID_SettingID",
                schema: "Core",
                table: "UserProfileSetting",
                columns: new[] { "UserProfileID", "SettingID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address",
                schema: "Contacts");

            migrationBuilder.DropTable(
                name: "Asset",
                schema: "AssetRegister");

            migrationBuilder.DropTable(
                name: "Bank",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "BankAccount",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "BankAccountTransaction",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "BankAccountType",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "Budget",
                schema: "Budget");

            migrationBuilder.DropTable(
                name: "BudgetEntry",
                schema: "Budget");

            migrationBuilder.DropTable(
                name: "Contact",
                schema: "Contacts");

            migrationBuilder.DropTable(
                name: "ContactAddress",
                schema: "Contacts");

            migrationBuilder.DropTable(
                name: "Document",
                schema: "Documents");

            migrationBuilder.DropTable(
                name: "Payee",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "UserProfilePermission",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "UserProfileSetting",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Currency",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Owner",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Setting",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Company",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "PersonalDetails",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "UserProfile",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Audit",
                schema: "Core");
        }
    }
}
#pragma warning restore IDE0053 // Use expression body for lambda expressions
