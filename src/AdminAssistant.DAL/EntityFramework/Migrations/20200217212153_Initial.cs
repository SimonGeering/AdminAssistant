using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminAssistant.DAL.EntityFramework.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audit",
                columns: table => new
                {
                    AuditID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsArchived = table.Column<bool>(nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: false),
                    ArchivedOn = table.Column<DateTime>(nullable: false),
                    ArchivedBy = table.Column<string>(maxLength: 50, nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => x.AuditID);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountType",
                columns: table => new
                {
                    BankAccountTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    AllowPersonal = table.Column<bool>(nullable: false, defaultValue: false),
                    AllowCompany = table.Column<bool>(nullable: false, defaultValue: false),
                    IsDeprecated = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountType", x => x.BankAccountTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ContactAddress",
                columns: table => new
                {
                    ContactAddressID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressID = table.Column<int>(nullable: false),
                    ContactID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactAddress", x => x.ContactAddressID);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    CurrencyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "CHAR(3)", maxLength: 3, nullable: false),
                    DecimalFormat = table.Column<string>(type: "CHAR(5)", maxLength: 5, nullable: false),
                    IsDeprecated = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.CurrencyID);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    PermissionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionKey = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.PermissionID);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    SettingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingKey = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.SettingID);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressID);
                    table.ForeignKey(
                        name: "FK_Address_Audit_AuditID",
                        column: x => x.AuditID,
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    AssetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditID = table.Column<int>(nullable: false),
                    OwnerID = table.Column<int>(nullable: false),
                    PurchasePrice = table.Column<int>(nullable: false),
                    DepreciatedValue = table.Column<int>(nullable: false),
                    ReplacementCost = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.AssetID);
                    table.ForeignKey(
                        name: "FK_Asset_Audit_AuditID",
                        column: x => x.AuditID,
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountTransaction",
                columns: table => new
                {
                    BankAccountTransactionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankAccountID = table.Column<int>(nullable: false),
                    AuditID = table.Column<int>(nullable: false),
                    PayeeID = table.Column<int>(nullable: false),
                    CurrencyID = table.Column<int>(nullable: false),
                    Credit = table.Column<int>(nullable: false),
                    Debit = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Notes = table.Column<string>(maxLength: 4000, nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountTransaction", x => x.BankAccountTransactionID);
                    table.ForeignKey(
                        name: "FK_BankAccountTransaction_Audit_AuditID",
                        column: x => x.AuditID,
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Budget",
                columns: table => new
                {
                    BudgetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditID = table.Column<int>(nullable: false),
                    OwnerID = table.Column<int>(nullable: false),
                    BudgetName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budget", x => x.BudgetID);
                    table.ForeignKey(
                        name: "FK_Budget_Audit_AuditID",
                        column: x => x.AuditID,
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BudgetEntry",
                columns: table => new
                {
                    BudgetEntryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BudgetID = table.Column<int>(nullable: false),
                    AuditID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetEntry", x => x.BudgetEntryID);
                    table.ForeignKey(
                        name: "FK_BudgetEntry_Audit_AuditID",
                        column: x => x.AuditID,
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    ContactID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerID = table.Column<int>(nullable: false),
                    TitleID = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    AuditID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ContactID);
                    table.ForeignKey(
                        name: "FK_Contact_Audit_AuditID",
                        column: x => x.AuditID,
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    DocumentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditID = table.Column<int>(nullable: false),
                    OwnerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.DocumentID);
                    table.ForeignKey(
                        name: "FK_Document_Audit_AuditID",
                        column: x => x.AuditID,
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payee",
                columns: table => new
                {
                    PayeeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payee", x => x.PayeeID);
                    table.ForeignKey(
                        name: "FK_Payee_Audit_AuditID",
                        column: x => x.AuditID,
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    UserProfileID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditID = table.Column<int>(nullable: false),
                    SignOn = table.Column<string>(maxLength: 50, nullable: false),
                    MSGraphID = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.UserProfileID);
                    table.ForeignKey(
                        name: "FK_UserProfile_Audit_AuditID",
                        column: x => x.AuditID,
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditID = table.Column<int>(nullable: false),
                    UserProfileID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CompanyNumber = table.Column<string>(maxLength: 50, nullable: false),
                    VATNumber = table.Column<string>(maxLength: 50, nullable: false),
                    DateOfIncorporation = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyID);
                    table.ForeignKey(
                        name: "FK_Company_Audit_AuditID",
                        column: x => x.AuditID,
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Company_UserProfile_UserProfileID",
                        column: x => x.UserProfileID,
                        principalTable: "UserProfile",
                        principalColumn: "UserProfileID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonalDetails",
                columns: table => new
                {
                    PersonalDetailsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditID = table.Column<int>(nullable: false),
                    UserProfileID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDetails", x => x.PersonalDetailsID);
                    table.ForeignKey(
                        name: "FK_PersonalDetails_Audit_AuditID",
                        column: x => x.AuditID,
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalDetails_UserProfile_UserProfileID",
                        column: x => x.UserProfileID,
                        principalTable: "UserProfile",
                        principalColumn: "UserProfileID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfilePermission",
                columns: table => new
                {
                    UserProfilePermissionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserProfileID = table.Column<int>(nullable: false),
                    PermissionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfilePermission", x => x.UserProfilePermissionID);
                    table.ForeignKey(
                        name: "FK_UserProfilePermission_Permission_PermissionID",
                        column: x => x.PermissionID,
                        principalTable: "Permission",
                        principalColumn: "PermissionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfilePermission_UserProfile_UserProfileID",
                        column: x => x.UserProfileID,
                        principalTable: "UserProfile",
                        principalColumn: "UserProfileID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileSetting",
                columns: table => new
                {
                    UserProfileSettingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserProfileID = table.Column<int>(nullable: false),
                    SettingID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileSetting", x => x.UserProfileSettingID);
                    table.ForeignKey(
                        name: "FK_UserProfileSetting_Setting_SettingID",
                        column: x => x.SettingID,
                        principalTable: "Setting",
                        principalColumn: "SettingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfileSetting_UserProfile_UserProfileID",
                        column: x => x.UserProfileID,
                        principalTable: "UserProfile",
                        principalColumn: "UserProfileID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    OwnerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(nullable: true),
                    PersonalDetailsID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.OwnerID);
                    table.ForeignKey(
                        name: "FK_Owner_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Owner_PersonalDetails_PersonalDetailsID",
                        column: x => x.PersonalDetailsID,
                        principalTable: "PersonalDetails",
                        principalColumn: "PersonalDetailsID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankAccount",
                columns: table => new
                {
                    BankAccountID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditID = table.Column<int>(nullable: false),
                    OwnerID = table.Column<int>(nullable: false),
                    BankAccountTypeID = table.Column<int>(nullable: false),
                    CurrencyID = table.Column<int>(nullable: false),
                    AccountName = table.Column<string>(maxLength: 50, nullable: false),
                    OpeningBalance = table.Column<int>(nullable: false),
                    CurrentBalance = table.Column<int>(nullable: false),
                    OpenedOn = table.Column<DateTime>(nullable: false),
                    IsBudgeted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.BankAccountID);
                    table.ForeignKey(
                        name: "FK_BankAccount_Audit_AuditID",
                        column: x => x.AuditID,
                        principalTable: "Audit",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccount_Currency_CurrencyID",
                        column: x => x.CurrencyID,
                        principalTable: "Currency",
                        principalColumn: "CurrencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccount_Owner_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Owner",
                        principalColumn: "OwnerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BankAccountType",
                columns: new[] { "BankAccountTypeID", "AllowCompany", "AllowPersonal", "Description" },
                values: new object[,]
                {
                    { 1, true, true, "Current Account" },
                    { 2, true, true, "Savings Account" }
                });

            migrationBuilder.InsertData(
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
                table: "Address",
                column: "AuditID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Asset_AuditID",
                table: "Asset",
                column: "AuditID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_AuditID",
                table: "BankAccount",
                column: "AuditID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_CurrencyID",
                table: "BankAccount",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_OwnerID",
                table: "BankAccount",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountTransaction_AuditID",
                table: "BankAccountTransaction",
                column: "AuditID");

            migrationBuilder.CreateIndex(
                name: "IX_Budget_AuditID",
                table: "Budget",
                column: "AuditID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetEntry_AuditID",
                table: "BudgetEntry",
                column: "AuditID");

            migrationBuilder.CreateIndex(
                name: "IX_Company_AuditID",
                table: "Company",
                column: "AuditID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_UserProfileID",
                table: "Company",
                column: "UserProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_AuditID",
                table: "Contact",
                column: "AuditID");

            migrationBuilder.CreateIndex(
                name: "IX_Document_AuditID",
                table: "Document",
                column: "AuditID");

            migrationBuilder.CreateIndex(
                name: "IX_Owner_PersonalDetailsID",
                table: "Owner",
                column: "PersonalDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_Owner_CompanyID_PersonalDetailsID",
                table: "Owner",
                columns: new[] { "CompanyID", "PersonalDetailsID" },
                unique: true,
                filter: "[CompanyID] IS NOT NULL AND [PersonalDetailsID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payee_AuditID",
                table: "Payee",
                column: "AuditID");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_PermissionKey",
                table: "Permission",
                column: "PermissionKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetails_AuditID",
                table: "PersonalDetails",
                column: "AuditID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetails_UserProfileID",
                table: "PersonalDetails",
                column: "UserProfileID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Setting_SettingKey",
                table: "Setting",
                column: "SettingKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_AuditID",
                table: "UserProfile",
                column: "AuditID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_SignOn",
                table: "UserProfile",
                column: "SignOn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilePermission_PermissionID",
                table: "UserProfilePermission",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilePermission_UserProfileID_PermissionID",
                table: "UserProfilePermission",
                columns: new[] { "UserProfileID", "PermissionID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileSetting_SettingID",
                table: "UserProfileSetting",
                column: "SettingID");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileSetting_UserProfileID_SettingID",
                table: "UserProfileSetting",
                columns: new[] { "UserProfileID", "SettingID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "BankAccount");

            migrationBuilder.DropTable(
                name: "BankAccountTransaction");

            migrationBuilder.DropTable(
                name: "BankAccountType");

            migrationBuilder.DropTable(
                name: "Budget");

            migrationBuilder.DropTable(
                name: "BudgetEntry");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "ContactAddress");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "Payee");

            migrationBuilder.DropTable(
                name: "UserProfilePermission");

            migrationBuilder.DropTable(
                name: "UserProfileSetting");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "Owner");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "PersonalDetails");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "Audit");
        }
    }
}
