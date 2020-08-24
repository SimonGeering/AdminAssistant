// <auto-generated />
using System;
using AdminAssistant.Infra.DAL.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdminAssistant.Infra.DAL.EntityFramework.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Accounts.BankAccountEntity", b =>
                {
                    b.Property<int>("BankAccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<int>("AuditID")
                        .HasColumnType("int");

                    b.Property<int>("BankAccountTypeID")
                        .HasColumnType("int");

                    b.Property<int>("CurrencyID")
                        .HasColumnType("int");

                    b.Property<int>("CurrentBalance")
                        .HasColumnType("int");

                    b.Property<bool>("IsBudgeted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OpenedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("OpeningBalance")
                        .HasColumnType("int");

                    b.Property<int>("OwnerID")
                        .HasColumnType("int");

                    b.HasKey("BankAccountID");

                    b.HasIndex("AuditID")
                        .IsUnique();

                    b.HasIndex("CurrencyID");

                    b.HasIndex("OwnerID");

                    b.ToTable("BankAccount","Accounts");
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Accounts.BankAccountTransactionEntity", b =>
                {
                    b.Property<int>("BankAccountTransactionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditID")
                        .HasColumnType("int");

                    b.Property<int>("BankAccountID")
                        .HasColumnType("int");

                    b.Property<int>("Credit")
                        .HasColumnType("int");

                    b.Property<int>("CurrencyID")
                        .HasColumnType("int");

                    b.Property<int>("Debit")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255)
                        .IsUnicode(true);

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(4000)")
                        .HasMaxLength(4000)
                        .IsUnicode(true);

                    b.Property<int>("PayeeID")
                        .HasColumnType("int");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("BankAccountTransactionID");

                    b.HasIndex("AuditID");

                    b.ToTable("BankAccountTransaction","Accounts");
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Accounts.BankAccountTypeEntity", b =>
                {
                    b.Property<int>("BankAccountTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AllowCompany")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("AllowPersonal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255)
                        .IsUnicode(true);

                    b.Property<bool>("IsDeprecated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.HasKey("BankAccountTypeID");

                    b.ToTable("BankAccountType","Accounts");

                    b.HasData(
                        new
                        {
                            BankAccountTypeID = 1,
                            AllowCompany = true,
                            AllowPersonal = true,
                            Description = "Current Account",
                            IsDeprecated = false
                        },
                        new
                        {
                            BankAccountTypeID = 2,
                            AllowCompany = true,
                            AllowPersonal = true,
                            Description = "Savings Account",
                            IsDeprecated = false
                        });
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Accounts.BankEntity", b =>
                {
                    b.Property<int>("BankID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.HasKey("BankID");

                    b.ToTable("Bank","Accounts");
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Accounts.PayeeEntity", b =>
                {
                    b.Property<int>("PayeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.HasKey("PayeeID");

                    b.HasIndex("AuditID");

                    b.ToTable("Payee","Accounts");
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.AssetRegister.AssetEntity", b =>
                {
                    b.Property<int>("AssetID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditID")
                        .HasColumnType("int");

                    b.Property<int>("DepreciatedValue")
                        .HasColumnType("int");

                    b.Property<int>("OwnerID")
                        .HasColumnType("int");

                    b.Property<int>("PurchasePrice")
                        .HasColumnType("int");

                    b.Property<int>("ReplacementCost")
                        .HasColumnType("int");

                    b.HasKey("AssetID");

                    b.HasIndex("AuditID")
                        .IsUnique();

                    b.ToTable("Asset","AssetRegister");
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Budget.BudgetEntity", b =>
                {
                    b.Property<int>("BudgetID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditID")
                        .HasColumnType("int");

                    b.Property<string>("BudgetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<int>("OwnerID")
                        .HasColumnType("int");

                    b.HasKey("BudgetID");

                    b.HasIndex("AuditID");

                    b.ToTable("Budget","Budget");
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Budget.BudgetEntryEntity", b =>
                {
                    b.Property<int>("BudgetEntryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditID")
                        .HasColumnType("int");

                    b.Property<int>("BudgetID")
                        .HasColumnType("int");

                    b.HasKey("BudgetEntryID");

                    b.HasIndex("AuditID");

                    b.ToTable("BudgetEntry","Budget");
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Contacts.AddressEntity", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditID")
                        .HasColumnType("int");

                    b.HasKey("AddressID");

                    b.HasIndex("AuditID")
                        .IsUnique();

                    b.ToTable("Address","Contacts");
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Contacts.ContactAddressEntity", b =>
                {
                    b.Property<int>("ContactAddressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressID")
                        .HasColumnType("int");

                    b.Property<int>("ContactID")
                        .HasColumnType("int");

                    b.HasKey("ContactAddressID");

                    b.ToTable("ContactAddress","Contacts");
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Contacts.ContactEntity", b =>
                {
                    b.Property<int>("ContactID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int>("OwnerID")
                        .HasColumnType("int");

                    b.Property<int>("TitleID")
                        .HasColumnType("int");

                    b.HasKey("ContactID");

                    b.HasIndex("AuditID");

                    b.ToTable("Contact","Contacts");
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Core.AuditEntity", b =>
                {
                    b.Property<int>("AuditID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArchivedBy")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasDefaultValue(null);

                    b.Property<DateTime>("ArchivedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("DeletedBy")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasDefaultValue(null);

                    b.Property<DateTime>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsArchived")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasDefaultValue(null);

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("AuditID");

                    b.ToTable("Audit","Core");
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Core.CompanyEntity", b =>
                {
                    b.Property<int>("CompanyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditID")
                        .HasColumnType("int");

                    b.Property<string>("CompanyNumber")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasDefaultValue(null);

                    b.Property<DateTime>("DateOfIncorporation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<int>("UserProfileID")
                        .HasColumnType("int");

                    b.Property<string>("VATNumber")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasDefaultValue(null);

                    b.HasKey("CompanyID");

                    b.HasIndex("AuditID")
                        .IsUnique();

                    b.HasIndex("UserProfileID");

                    b.ToTable("Company","Core");
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Core.CurrencyEntity", b =>
                {
                    b.Property<int>("CurrencyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DecimalFormat")
                        .IsRequired()
                        .HasColumnType("CHAR(5)")
                        .HasMaxLength(5);

                    b.Property<bool>("IsDeprecated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("CHAR(3)")
                        .HasMaxLength(3)
                        .IsUnicode(true);

                    b.HasKey("CurrencyID");

                    b.ToTable("Currency","Core");

                    b.HasData(
                        new
                        {
                            CurrencyID = 1,
                            DecimalFormat = "2.2-2",
                            IsDeprecated = false,
                            Symbol = "GBP"
                        },
                        new
                        {
                            CurrencyID = 2,
                            DecimalFormat = "2.2-2",
                            IsDeprecated = false,
                            Symbol = "EUR"
                        },
                        new
                        {
                            CurrencyID = 3,
                            DecimalFormat = "2.2-2",
                            IsDeprecated = false,
                            Symbol = "USD"
                        });
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Core.OwnerEntity", b =>
                {
                    b.Property<int>("OwnerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompanyID")
                        .HasColumnType("int");

                    b.Property<int?>("PersonalDetailsID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("OwnerID");

                    b.HasIndex("PersonalDetailsID");

                    b.HasIndex("CompanyID", "PersonalDetailsID")
                        .IsUnique()
                        .HasFilter("[CompanyID] IS NOT NULL AND [PersonalDetailsID] IS NOT NULL");

                    b.ToTable("Owner","Core");
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Core.PermissionEntity", b =>
                {
                    b.Property<int>("PermissionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PermissionKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(true);

                    b.HasKey("PermissionID");

                    b.HasIndex("PermissionKey")
                        .IsUnique();

                    b.ToTable("Permission","Core");
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Core.PersonalDetailsEntity", b =>
                {
                    b.Property<int>("PersonalDetailsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditID")
                        .HasColumnType("int");

                    b.Property<int>("UserProfileID")
                        .HasColumnType("int");

                    b.HasKey("PersonalDetailsID");

                    b.HasIndex("AuditID")
                        .IsUnique();

                    b.HasIndex("UserProfileID")
                        .IsUnique();

                    b.ToTable("PersonalDetails","Core");
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Core.SettingEntity", b =>
                {
                    b.Property<int>("SettingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SettingKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(true);

                    b.HasKey("SettingID");

                    b.HasIndex("SettingKey")
                        .IsUnique();

                    b.ToTable("Setting","Core");
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Core.UserProfileEntity", b =>
                {
                    b.Property<int>("UserProfileID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditID")
                        .HasColumnType("int");

                    b.Property<string>("MSGraphID")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasDefaultValue(null);

                    b.Property<string>("SignOn")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.HasKey("UserProfileID");

                    b.HasIndex("AuditID")
                        .IsUnique();

                    b.HasIndex("SignOn")
                        .IsUnique();

                    b.ToTable("UserProfile","Core");
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Core.UserProfilePermissionEntity", b =>
                {
                    b.Property<int>("UserProfilePermissionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PermissionID")
                        .HasColumnType("int");

                    b.Property<int>("UserProfileID")
                        .HasColumnType("int");

                    b.HasKey("UserProfilePermissionID");

                    b.HasIndex("PermissionID");

                    b.HasIndex("UserProfileID", "PermissionID")
                        .IsUnique();

                    b.ToTable("UserProfilePermission","Core");
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Core.UserProfileSettingEntity", b =>
                {
                    b.Property<int>("UserProfileSettingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SettingID")
                        .HasColumnType("int");

                    b.Property<int>("UserProfileID")
                        .HasColumnType("int");

                    b.HasKey("UserProfileSettingID");

                    b.HasIndex("SettingID");

                    b.HasIndex("UserProfileID", "SettingID")
                        .IsUnique();

                    b.ToTable("UserProfileSetting","Core");
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Documents.DocumentEntity", b =>
                {
                    b.Property<int>("DocumentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditID")
                        .HasColumnType("int");

                    b.Property<int>("OwnerID")
                        .HasColumnType("int");

                    b.HasKey("DocumentID");

                    b.HasIndex("AuditID");

                    b.ToTable("Document","Documents");
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Accounts.BankAccountEntity", b =>
                {
                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.AuditEntity", "Audit")
                        .WithOne("BankAccount")
                        .HasForeignKey("AdminAssistant.DAL.EntityFramework.Model.Accounts.BankAccountEntity", "AuditID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.CurrencyEntity", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.OwnerEntity", "Owner")
                        .WithMany("BankAccounts")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Accounts.BankAccountTransactionEntity", b =>
                {
                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.AuditEntity", "Audit")
                        .WithMany()
                        .HasForeignKey("AuditID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Accounts.PayeeEntity", b =>
                {
                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.AuditEntity", "Audit")
                        .WithMany()
                        .HasForeignKey("AuditID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.AssetRegister.AssetEntity", b =>
                {
                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.AuditEntity", "Audit")
                        .WithOne("Asset")
                        .HasForeignKey("AdminAssistant.DAL.EntityFramework.Model.AssetRegister.AssetEntity", "AuditID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Budget.BudgetEntity", b =>
                {
                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.AuditEntity", "Audit")
                        .WithMany()
                        .HasForeignKey("AuditID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Budget.BudgetEntryEntity", b =>
                {
                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.AuditEntity", "Audit")
                        .WithMany()
                        .HasForeignKey("AuditID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Contacts.AddressEntity", b =>
                {
                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.AuditEntity", "Audit")
                        .WithOne("Address")
                        .HasForeignKey("AdminAssistant.DAL.EntityFramework.Model.Contacts.AddressEntity", "AuditID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Contacts.ContactEntity", b =>
                {
                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.AuditEntity", "Audit")
                        .WithMany()
                        .HasForeignKey("AuditID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Core.CompanyEntity", b =>
                {
                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.AuditEntity", "Audit")
                        .WithOne("Company")
                        .HasForeignKey("AdminAssistant.DAL.EntityFramework.Model.Core.CompanyEntity", "AuditID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.UserProfileEntity", "UserProfile")
                        .WithMany("Companies")
                        .HasForeignKey("UserProfileID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Core.OwnerEntity", b =>
                {
                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.CompanyEntity", "Company")
                        .WithMany("Owns")
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.PersonalDetailsEntity", "PersonalDetails")
                        .WithMany("Owns")
                        .HasForeignKey("PersonalDetailsID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Core.PersonalDetailsEntity", b =>
                {
                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.AuditEntity", "Audit")
                        .WithOne("PersonalDetails")
                        .HasForeignKey("AdminAssistant.DAL.EntityFramework.Model.Core.PersonalDetailsEntity", "AuditID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.UserProfileEntity", "UserProfile")
                        .WithOne("PersonalDetails")
                        .HasForeignKey("AdminAssistant.DAL.EntityFramework.Model.Core.PersonalDetailsEntity", "UserProfileID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Core.UserProfileEntity", b =>
                {
                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.AuditEntity", "Audit")
                        .WithOne("UserProfile")
                        .HasForeignKey("AdminAssistant.DAL.EntityFramework.Model.Core.UserProfileEntity", "AuditID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Core.UserProfilePermissionEntity", b =>
                {
                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.PermissionEntity", "Permission")
                        .WithMany("UserProfilePermissions")
                        .HasForeignKey("PermissionID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.UserProfileEntity", "UserProfile")
                        .WithMany("Permissions")
                        .HasForeignKey("UserProfileID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Core.UserProfileSettingEntity", b =>
                {
                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.SettingEntity", "Setting")
                        .WithMany("UserProfileSettings")
                        .HasForeignKey("SettingID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.UserProfileEntity", "UserProfile")
                        .WithMany("Settings")
                        .HasForeignKey("UserProfileID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AdminAssistant.DAL.EntityFramework.Model.Documents.DocumentEntity", b =>
                {
                    b.HasOne("AdminAssistant.DAL.EntityFramework.Model.Core.AuditEntity", "Audit")
                        .WithMany()
                        .HasForeignKey("AuditID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
