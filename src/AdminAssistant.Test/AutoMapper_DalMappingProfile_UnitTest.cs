#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Accounts;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Core;
using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.DomainModel.Modules.DocumentsModule;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Documents;
using AdminAssistant.Infra.DAL;

namespace AdminAssistant.Test.Infra.DAL;

public class DalMappingProfile_Should
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public DalMappingProfile_Should()
    {
        _configuration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = _configuration.CreateMapper();
    }

    [Fact]
    [Trait("Category", "Unit")]
    [SuppressMessage("Style", "IDE0022:Use expression body for methods", Justification = "One line test")]
    public void HaveValidConfiguration()
    {
        // Arrange

        // Act
        _configuration.AssertConfigurationIsValid();

        // Assert
    }



/*
project Admin_Assistant
{
  database_type: "SQL Server"
}

//
// ==============================
// Core Module
// ==============================
//
Table "Core.Audit"
{
  "AuditID" INT [pk, increment]
  "IsArchived" BIT [not null]
  "IsDeleted" BIT [not null]
  "CreatedOn" DATETIME2 [not null]
  "CreatedBy" NVARCHAR(50) [not null]
  "ReplacementCost" INT
  "UpdatedOn" DATETIME2
  "UpdatedBy" NVARCHAR(50) [not null]
  "ArchivedOn" DATETIME2
  "ArchivedBy" NVARCHAR(50) [not null]
  "DeletedOn" DATETIME2
  "DeletedBy" NVARCHAR(50) [not null]
}

Table "Core.Owner"
{
  "OwnerID" INT [pk, increment]
  "CompanyID" INT
  "PersonalDetailsID" INT
}

Table "Core.Company"
{
  "CompanyID" INT [pk, increment]
  "AuditID" INT
  "UserProfileID" INT
  "Name" NVARCHAR(50)
  "CompanyNumber" NVARCHAR(50)
  "VATNumber" NVARCHAR(50)
  "DateOfIncorporation" DATETIME2
}
Ref: "Core.Company"."CompanyID" < "Core.Owner"."CompanyID"

Table "Core.UserProfile"
{
  "UserProfileID" INT [pk, increment]
  "AuditID" INT
  "SignOn" NVARCHAR(50)
  "MSGraphID" NVARCHAR(50)
}
Ref: "Core.UserProfile"."UserProfileID" < "Core.PersonalDetails"."UserProfileID"
Ref: "Core.UserProfile"."UserProfileID" < "Core.UserProfilePermission"."UserProfileID"
Ref: "Core.UserProfile"."UserProfileID" < "Core.Company"."UserProfileID"
Ref: "Core.UserProfile"."UserProfileID" < "Core.UserProfileSetting"."UserProfileID"

Table "Core.Permission"
{
  "PermissionID" INT [pk]
  "PermissionKey" NVARCHAR(20)
}
Ref: "Core.Permission"."PermissionID" < "Core.UserProfilePermission"."PermissionID"

Table "Core.PersonalDetails"
{
  "PersonalDetailsID" INT [pk]
  "AuditID" INT
  "UserProfileID" NVARCHAR(255)
}
Ref: "Core.PersonalDetails"."PersonalDetailsID" < "Core.Owner"."PersonalDetailsID"

Table "Core.UserProfileSetting"
{
  "UserProfileSettingID" INT [pk]
  "UserProfileID" INT
  "SettingID" INT
}
Table "Core.UserProfilePermission"
{
  "UserProfilePermissionID" INT [pk]
  "UserProfileID" INT
  "PermissionID" INT
}

Table "Core.Setting"
{
  "SettingID" INT [pk]
  "SettingKey" NVARCHAR(20)
}
Ref: "Core.Setting"."SettingID" < "Core.UserProfileSetting"."SettingID"

Table "Core.Currency"
{
  "CurrencyID" INT [pk]
  "IsDeprecated" BIT
  "Symbol" CHAR(3)
  "DecimalFormat" CHAR(5)
}
Ref: "Core.Currency"."CurrencyID" < "Accounts.BankAccount"."CurrencyID"
Ref: "Core.Currency"."CurrencyID" < "Accounts.BankAccountTransaction"."CurrencyID"

//
// ==============================
// Accounts Module
// ==============================
//
Table "Accounts.Bank"
{
  "BankID" INT [pk]
  "AuditID" INT
  "Name" NVRCHAR(50)
}
Ref: "Accounts.Bank"."BankID" < "Accounts.BankAccount"."BankID"

Table "Accounts.BankAccountType"
{
  "BankAccountTypeID" INT [pk]
  "IsDeprecated" BIT
  "Description" NVARCHAR(255)
  "AllowPersonal" BIT
  "AllowCompany" BIT
}
Ref: "Accounts.BankAccountType"."BankAccountTypeID" < "Accounts.BankAccount"."BankAccountTypeID"

Table "Accounts.BankAccount"
{
  "BankAccountID" INT [pk]
  "AuditID" INT
  "OwnerID" INT
  "BankID" INT
  "BankAccountTypeID" INT
  "CurrencyID" INT
  "AccountName" NVARCHAR(50)
  "OpeningBalance" INT
  "CurrentBalance" INT
  "OpenedOn" DATETIME2
  "IsBudgeted" BIT
}
Ref: "Accounts.BankAccount"."BankAccountID" < "Accounts.BankAccountTransaction"."BankAccountID"
*/
    [Theory]
    [Trait("Category", "Unit")]
    // Accounts Schema
    [InlineData(typeof(BankEntity), typeof(Bank))]
    [InlineData(typeof(BankAccountEntity), typeof(BankAccount))]
    [InlineData(typeof(BankAccountStatementEntity), typeof(BankAccountStatement))]
    [InlineData(typeof(BankAccountTypeEntity), typeof(BankAccountType))]
    [InlineData(typeof(BankAccountTransactionEntity), typeof(BankAccountTransaction))]
    [InlineData(typeof(BankAccountTransactionTypeEntity), typeof(BankAccountTransactionType))]
    [InlineData(typeof(PayeeEntity), typeof(Payee))]
    [InlineData(typeof(PayeeContactEntity), typeof(PayeeContact))]
    [InlineData(typeof(Bank), typeof(BankEntity))]
    [InlineData(typeof(BankAccount), typeof(BankAccountEntity))]
    [InlineData(typeof(BankAccountStatement), typeof(BankAccountStatementEntity))]
    [InlineData(typeof(BankAccountType), typeof(BankAccountTypeEntity))]
    [InlineData(typeof(BankAccountTransaction), typeof(BankAccountTransactionEntity))]
    [InlineData(typeof(BankAccountTransactionType), typeof(BankAccountTransactionTypeEntity))]
    [InlineData(typeof(Payee), typeof(PayeeEntity))]
    [InlineData(typeof(PayeeContact), typeof(PayeeContactEntity))]
    // Documents Schema
    [InlineData(typeof(DocumentEntity), typeof(Document))]
    [InlineData(typeof(Document), typeof(DocumentEntity))]
    // Core Schema
    [InlineData(typeof(CurrencyEntity), typeof(Currency))]
    [InlineData(typeof(Currency), typeof(CurrencyEntity))]
    public void ShouldSupportSchemaMappingFromSourceToDestination(Type source, Type destination)
    {
        // Arrange
        var instance = Activator.CreateInstance(source);

        // Act
        var result = _mapper.Map(instance, source, destination);

        // Assert
        result.Should().NotBeNull();
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
