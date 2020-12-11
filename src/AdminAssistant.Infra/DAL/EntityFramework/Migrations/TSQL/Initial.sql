IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF SCHEMA_ID(N'Contacts') IS NULL EXEC(N'CREATE SCHEMA [Contacts];');
GO

IF SCHEMA_ID(N'AssetRegister') IS NULL EXEC(N'CREATE SCHEMA [AssetRegister];');
GO

IF SCHEMA_ID(N'Core') IS NULL EXEC(N'CREATE SCHEMA [Core];');
GO

IF SCHEMA_ID(N'Accounts') IS NULL EXEC(N'CREATE SCHEMA [Accounts];');
GO

IF SCHEMA_ID(N'Budget') IS NULL EXEC(N'CREATE SCHEMA [Budget];');
GO

IF SCHEMA_ID(N'Documents') IS NULL EXEC(N'CREATE SCHEMA [Documents];');
GO

CREATE TABLE [Core].[Audit] (
    [AuditID] int NOT NULL IDENTITY,
    [IsArchived] bit NOT NULL DEFAULT CAST(0 AS bit),
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    [CreatedOn] datetime2 NOT NULL DEFAULT (GETDATE()),
    [CreatedBy] nvarchar(50) NOT NULL,
    [UpdatedOn] datetime2 NOT NULL,
    [UpdatedBy] nvarchar(50) NOT NULL,
    [ArchivedOn] datetime2 NOT NULL,
    [ArchivedBy] nvarchar(50) NOT NULL,
    [DeletedOn] datetime2 NOT NULL,
    [DeletedBy] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Audit] PRIMARY KEY ([AuditID])
);
GO

CREATE TABLE [Accounts].[Bank] (
    [BankID] int NOT NULL IDENTITY,
    [BankName] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Bank] PRIMARY KEY ([BankID])
);
GO

CREATE TABLE [Accounts].[BankAccountType] (
    [BankAccountTypeID] int NOT NULL IDENTITY,
    [Description] nvarchar(255) NOT NULL,
    [AllowPersonal] bit NOT NULL DEFAULT CAST(0 AS bit),
    [AllowCompany] bit NOT NULL DEFAULT CAST(0 AS bit),
    [IsDeprecated] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_BankAccountType] PRIMARY KEY ([BankAccountTypeID])
);
GO

CREATE TABLE [Contacts].[ContactAddress] (
    [ContactAddressID] int NOT NULL IDENTITY,
    [AddressID] int NOT NULL,
    [ContactID] int NOT NULL,
    CONSTRAINT [PK_ContactAddress] PRIMARY KEY ([ContactAddressID])
);
GO

CREATE TABLE [Core].[Currency] (
    [CurrencyID] int NOT NULL IDENTITY,
    [Symbol] CHAR(3) NOT NULL,
    [DecimalFormat] CHAR(5) NOT NULL,
    [IsDeprecated] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_Currency] PRIMARY KEY ([CurrencyID])
);
GO

CREATE TABLE [Core].[Permission] (
    [PermissionID] int NOT NULL IDENTITY,
    [PermissionKey] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_Permission] PRIMARY KEY ([PermissionID])
);
GO

CREATE TABLE [Core].[Setting] (
    [SettingID] int NOT NULL IDENTITY,
    [SettingKey] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_Setting] PRIMARY KEY ([SettingID])
);
GO

CREATE TABLE [Contacts].[Address] (
    [AddressID] int NOT NULL IDENTITY,
    [AuditID] int NOT NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY ([AddressID]),
    CONSTRAINT [FK_Address_Audit_AuditID] FOREIGN KEY ([AuditID]) REFERENCES [Core].[Audit] ([AuditID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AssetRegister].[Asset] (
    [AssetID] int NOT NULL IDENTITY,
    [AuditID] int NOT NULL,
    [OwnerID] int NOT NULL,
    [PurchasePrice] int NOT NULL,
    [DepreciatedValue] int NOT NULL,
    [ReplacementCost] int NOT NULL,
    CONSTRAINT [PK_Asset] PRIMARY KEY ([AssetID]),
    CONSTRAINT [FK_Asset_Audit_AuditID] FOREIGN KEY ([AuditID]) REFERENCES [Core].[Audit] ([AuditID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Accounts].[BankAccountTransaction] (
    [BankAccountTransactionID] int NOT NULL IDENTITY,
    [BankAccountID] int NOT NULL,
    [AuditID] int NOT NULL,
    [PayeeID] int NOT NULL,
    [CurrencyID] int NOT NULL,
    [Credit] int NOT NULL,
    [Debit] int NOT NULL,
    [Description] nvarchar(255) NOT NULL,
    [Notes] nvarchar(4000) NOT NULL,
    [TransactionDate] datetime2 NOT NULL,
    CONSTRAINT [PK_BankAccountTransaction] PRIMARY KEY ([BankAccountTransactionID]),
    CONSTRAINT [FK_BankAccountTransaction_Audit_AuditID] FOREIGN KEY ([AuditID]) REFERENCES [Core].[Audit] ([AuditID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Budget].[Budget] (
    [BudgetID] int NOT NULL IDENTITY,
    [AuditID] int NOT NULL,
    [OwnerID] int NOT NULL,
    [BudgetName] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Budget] PRIMARY KEY ([BudgetID]),
    CONSTRAINT [FK_Budget_Audit_AuditID] FOREIGN KEY ([AuditID]) REFERENCES [Core].[Audit] ([AuditID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Budget].[BudgetEntry] (
    [BudgetEntryID] int NOT NULL IDENTITY,
    [BudgetID] int NOT NULL,
    [AuditID] int NOT NULL,
    CONSTRAINT [PK_BudgetEntry] PRIMARY KEY ([BudgetEntryID]),
    CONSTRAINT [FK_BudgetEntry_Audit_AuditID] FOREIGN KEY ([AuditID]) REFERENCES [Core].[Audit] ([AuditID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Contacts].[Contact] (
    [ContactID] int NOT NULL IDENTITY,
    [OwnerID] int NOT NULL,
    [TitleID] int NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [AuditID] int NOT NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY ([ContactID]),
    CONSTRAINT [FK_Contact_Audit_AuditID] FOREIGN KEY ([AuditID]) REFERENCES [Core].[Audit] ([AuditID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Documents].[Document] (
    [DocumentID] int NOT NULL IDENTITY,
    [AuditID] int NOT NULL,
    [OwnerID] int NOT NULL,
    [FileName] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_Document] PRIMARY KEY ([DocumentID]),
    CONSTRAINT [FK_Document_Audit_AuditID] FOREIGN KEY ([AuditID]) REFERENCES [Core].[Audit] ([AuditID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Accounts].[Payee] (
    [PayeeID] int NOT NULL IDENTITY,
    [AuditID] int NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Payee] PRIMARY KEY ([PayeeID]),
    CONSTRAINT [FK_Payee_Audit_AuditID] FOREIGN KEY ([AuditID]) REFERENCES [Core].[Audit] ([AuditID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Core].[UserProfile] (
    [UserProfileID] int NOT NULL IDENTITY,
    [AuditID] int NOT NULL,
    [SignOn] nvarchar(50) NOT NULL,
    [MSGraphID] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_UserProfile] PRIMARY KEY ([UserProfileID]),
    CONSTRAINT [FK_UserProfile_Audit_AuditID] FOREIGN KEY ([AuditID]) REFERENCES [Core].[Audit] ([AuditID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Core].[Company] (
    [CompanyID] int NOT NULL IDENTITY,
    [AuditID] int NOT NULL,
    [UserProfileID] int NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [CompanyNumber] nvarchar(50) NOT NULL,
    [VATNumber] nvarchar(50) NOT NULL,
    [DateOfIncorporation] datetime2 NOT NULL,
    CONSTRAINT [PK_Company] PRIMARY KEY ([CompanyID]),
    CONSTRAINT [FK_Company_Audit_AuditID] FOREIGN KEY ([AuditID]) REFERENCES [Core].[Audit] ([AuditID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Company_UserProfile_UserProfileID] FOREIGN KEY ([UserProfileID]) REFERENCES [Core].[UserProfile] ([UserProfileID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Core].[PersonalDetails] (
    [PersonalDetailsID] int NOT NULL IDENTITY,
    [AuditID] int NOT NULL,
    [UserProfileID] int NOT NULL,
    CONSTRAINT [PK_PersonalDetails] PRIMARY KEY ([PersonalDetailsID]),
    CONSTRAINT [FK_PersonalDetails_Audit_AuditID] FOREIGN KEY ([AuditID]) REFERENCES [Core].[Audit] ([AuditID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PersonalDetails_UserProfile_UserProfileID] FOREIGN KEY ([UserProfileID]) REFERENCES [Core].[UserProfile] ([UserProfileID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Core].[UserProfilePermission] (
    [UserProfilePermissionID] int NOT NULL IDENTITY,
    [UserProfileID] int NOT NULL,
    [PermissionID] int NOT NULL,
    CONSTRAINT [PK_UserProfilePermission] PRIMARY KEY ([UserProfilePermissionID]),
    CONSTRAINT [FK_UserProfilePermission_Permission_PermissionID] FOREIGN KEY ([PermissionID]) REFERENCES [Core].[Permission] ([PermissionID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_UserProfilePermission_UserProfile_UserProfileID] FOREIGN KEY ([UserProfileID]) REFERENCES [Core].[UserProfile] ([UserProfileID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Core].[UserProfileSetting] (
    [UserProfileSettingID] int NOT NULL IDENTITY,
    [UserProfileID] int NOT NULL,
    [SettingID] int NOT NULL,
    CONSTRAINT [PK_UserProfileSetting] PRIMARY KEY ([UserProfileSettingID]),
    CONSTRAINT [FK_UserProfileSetting_Setting_SettingID] FOREIGN KEY ([SettingID]) REFERENCES [Core].[Setting] ([SettingID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_UserProfileSetting_UserProfile_UserProfileID] FOREIGN KEY ([UserProfileID]) REFERENCES [Core].[UserProfile] ([UserProfileID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Core].[Owner] (
    [OwnerID] int NOT NULL IDENTITY,
    [CompanyID] int NULL,
    [PersonalDetailsID] int NULL,
    CONSTRAINT [PK_Owner] PRIMARY KEY ([OwnerID]),
    CONSTRAINT [FK_Owner_Company_CompanyID] FOREIGN KEY ([CompanyID]) REFERENCES [Core].[Company] ([CompanyID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Owner_PersonalDetails_PersonalDetailsID] FOREIGN KEY ([PersonalDetailsID]) REFERENCES [Core].[PersonalDetails] ([PersonalDetailsID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Accounts].[BankAccount] (
    [BankAccountID] int NOT NULL IDENTITY,
    [AuditID] int NOT NULL,
    [OwnerID] int NOT NULL,
    [BankAccountTypeID] int NOT NULL,
    [CurrencyID] int NOT NULL,
    [AccountName] nvarchar(50) NOT NULL,
    [OpeningBalance] int NOT NULL,
    [CurrentBalance] int NOT NULL,
    [OpenedOn] datetime2 NOT NULL,
    [IsBudgeted] bit NOT NULL,
    CONSTRAINT [PK_BankAccount] PRIMARY KEY ([BankAccountID]),
    CONSTRAINT [FK_BankAccount_Audit_AuditID] FOREIGN KEY ([AuditID]) REFERENCES [Core].[Audit] ([AuditID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_BankAccount_Currency_CurrencyID] FOREIGN KEY ([CurrencyID]) REFERENCES [Core].[Currency] ([CurrencyID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_BankAccount_Owner_OwnerID] FOREIGN KEY ([OwnerID]) REFERENCES [Core].[Owner] ([OwnerID]) ON DELETE NO ACTION
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BankAccountTypeID', N'AllowCompany', N'AllowPersonal', N'Description') AND [object_id] = OBJECT_ID(N'[Accounts].[BankAccountType]'))
    SET IDENTITY_INSERT [Accounts].[BankAccountType] ON;
INSERT INTO [Accounts].[BankAccountType] ([BankAccountTypeID], [AllowCompany], [AllowPersonal], [Description])
VALUES (1, CAST(1 AS bit), CAST(1 AS bit), N'Current Account'),
(2, CAST(1 AS bit), CAST(1 AS bit), N'Savings Account');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BankAccountTypeID', N'AllowCompany', N'AllowPersonal', N'Description') AND [object_id] = OBJECT_ID(N'[Accounts].[BankAccountType]'))
    SET IDENTITY_INSERT [Accounts].[BankAccountType] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CurrencyID', N'DecimalFormat', N'Symbol') AND [object_id] = OBJECT_ID(N'[Core].[Currency]'))
    SET IDENTITY_INSERT [Core].[Currency] ON;
INSERT INTO [Core].[Currency] ([CurrencyID], [DecimalFormat], [Symbol])
VALUES (1, '2.2-2', N'GBP'),
(2, '2.2-2', N'EUR'),
(3, '2.2-2', N'USD');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CurrencyID', N'DecimalFormat', N'Symbol') AND [object_id] = OBJECT_ID(N'[Core].[Currency]'))
    SET IDENTITY_INSERT [Core].[Currency] OFF;
GO

CREATE UNIQUE INDEX [IX_Address_AuditID] ON [Contacts].[Address] ([AuditID]);
GO

CREATE UNIQUE INDEX [IX_Asset_AuditID] ON [AssetRegister].[Asset] ([AuditID]);
GO

CREATE UNIQUE INDEX [IX_BankAccount_AuditID] ON [Accounts].[BankAccount] ([AuditID]);
GO

CREATE INDEX [IX_BankAccount_CurrencyID] ON [Accounts].[BankAccount] ([CurrencyID]);
GO

CREATE INDEX [IX_BankAccount_OwnerID] ON [Accounts].[BankAccount] ([OwnerID]);
GO

CREATE INDEX [IX_BankAccountTransaction_AuditID] ON [Accounts].[BankAccountTransaction] ([AuditID]);
GO

CREATE INDEX [IX_Budget_AuditID] ON [Budget].[Budget] ([AuditID]);
GO

CREATE INDEX [IX_BudgetEntry_AuditID] ON [Budget].[BudgetEntry] ([AuditID]);
GO

CREATE UNIQUE INDEX [IX_Company_AuditID] ON [Core].[Company] ([AuditID]);
GO

CREATE INDEX [IX_Company_UserProfileID] ON [Core].[Company] ([UserProfileID]);
GO

CREATE INDEX [IX_Contact_AuditID] ON [Contacts].[Contact] ([AuditID]);
GO

CREATE INDEX [IX_Document_AuditID] ON [Documents].[Document] ([AuditID]);
GO

CREATE UNIQUE INDEX [IX_Owner_CompanyID_PersonalDetailsID] ON [Core].[Owner] ([CompanyID], [PersonalDetailsID]) WHERE [CompanyID] IS NOT NULL AND [PersonalDetailsID] IS NOT NULL;
GO

CREATE INDEX [IX_Owner_PersonalDetailsID] ON [Core].[Owner] ([PersonalDetailsID]);
GO

CREATE INDEX [IX_Payee_AuditID] ON [Accounts].[Payee] ([AuditID]);
GO

CREATE UNIQUE INDEX [IX_Permission_PermissionKey] ON [Core].[Permission] ([PermissionKey]);
GO

CREATE UNIQUE INDEX [IX_PersonalDetails_AuditID] ON [Core].[PersonalDetails] ([AuditID]);
GO

CREATE UNIQUE INDEX [IX_PersonalDetails_UserProfileID] ON [Core].[PersonalDetails] ([UserProfileID]);
GO

CREATE UNIQUE INDEX [IX_Setting_SettingKey] ON [Core].[Setting] ([SettingKey]);
GO

CREATE UNIQUE INDEX [IX_UserProfile_AuditID] ON [Core].[UserProfile] ([AuditID]);
GO

CREATE UNIQUE INDEX [IX_UserProfile_SignOn] ON [Core].[UserProfile] ([SignOn]);
GO

CREATE INDEX [IX_UserProfilePermission_PermissionID] ON [Core].[UserProfilePermission] ([PermissionID]);
GO

CREATE UNIQUE INDEX [IX_UserProfilePermission_UserProfileID_PermissionID] ON [Core].[UserProfilePermission] ([UserProfileID], [PermissionID]);
GO

CREATE INDEX [IX_UserProfileSetting_SettingID] ON [Core].[UserProfileSetting] ([SettingID]);
GO

CREATE UNIQUE INDEX [IX_UserProfileSetting_UserProfileID_SettingID] ON [Core].[UserProfileSetting] ([UserProfileID], [SettingID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201019144909_Initial', N'5.0.0-rc.1.20451.13');
GO

COMMIT;
GO

