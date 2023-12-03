
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

Table "Accounts.BankAccountTransaction"
{
  "BankAccountTransactionID" INT [pk]
  "AuditID" INT
  "BankAccountID" INT
  "PayeeID" INT
  "CurrencyID" INT
  "BankAccountTransactionTypeID" INT
  "BankAccountStatementID" INT
  "BankAccountStatementNumber" INT
  "IsReconciled" BIT
  "TransactionDate" DATETIME2
  "Credit" INT
  "Debit" INT
  "Description" NVARCHAR(255)
  "Notes" NVARCHAR(4000)
}

Table "Accounts.Payee"
{
  "PayeeID" INT [pk]
  "AuditID" INT
  "Name" NVARCHAR(255)
}
Ref: "Accounts.Payee"."PayeeID" < "Accounts.BankAccountTransaction"."PayeeID"
Ref: "Accounts.Payee"."PayeeID" < "Accounts.PayeeContact"."PayeeID"

Table "Accounts.PayeeContact"
{
  "PayeeContactID" INT [pk]
  "AuditID" INT
  "PayeeID" INT
  "ContactID" INT
  "IsPrimaryContact" BIT
}

Table "Accounts.BankAccountTransactionType"
{
  "BankAccountTransactionTypeID" INT [pk]
  "IsDeprecated" BIT
  "Description" NVARCHAR(255)
}
Ref: "Accounts.BankAccountTransactionType"."BankAccountTransactionTypeID" < "Accounts.BankAccountTransaction"."BankAccountTransactionTypeID"

*/
