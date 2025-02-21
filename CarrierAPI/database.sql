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

CREATE TABLE [Carriers] (
    [CarrierId] int NOT NULL IDENTITY,
    [CarrierName] nvarchar(max) NOT NULL,
    [CarrierIsActive] bit NOT NULL,
    [CarrierPlusDesiCost] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Carriers] PRIMARY KEY ([CarrierId])
);
GO

CREATE TABLE [CarrierConfigurations] (
    [CarrierConfigurationId] int NOT NULL IDENTITY,
    [CarrierMaxDesi] int NOT NULL,
    [CarrierMinDesi] int NOT NULL,
    [CarrierCost] decimal(18,2) NOT NULL,
    [CarrierId] int NOT NULL,
    CONSTRAINT [PK_CarrierConfigurations] PRIMARY KEY ([CarrierConfigurationId]),
    CONSTRAINT [FK_CarrierConfigurations_Carriers_CarrierId] FOREIGN KEY ([CarrierId]) REFERENCES [Carriers] ([CarrierId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Orders] (
    [OrderId] int NOT NULL IDENTITY,
    [OrderDesi] int NOT NULL,
    [OrderDate] datetime2 NOT NULL,
    [OrderCarrierCost] decimal(18,2) NOT NULL,
    [CarrierId] int NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([OrderId]),
    CONSTRAINT [FK_Orders_Carriers_CarrierId] FOREIGN KEY ([CarrierId]) REFERENCES [Carriers] ([CarrierId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_CarrierConfigurations_CarrierId] ON [CarrierConfigurations] ([CarrierId]);
GO

CREATE INDEX [IX_Orders_CarrierId] ON [Orders] ([CarrierId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250220201956_InitialCreate', N'6.0.36');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250221134044_ImplementModelBuilder', N'6.0.36');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [CarrierReport] (
    [CarrierReportId] int NOT NULL IDENTITY,
    [CarrierReportDate] datetime2 NOT NULL,
    [CarrierCost] decimal(18,2) NOT NULL,
    [CarrierId] int NOT NULL,
    CONSTRAINT [PK_CarrierReport] PRIMARY KEY ([CarrierReportId]),
    CONSTRAINT [FK_CarrierReport_Carriers_CarrierId] FOREIGN KEY ([CarrierId]) REFERENCES [Carriers] ([CarrierId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_CarrierReport_CarrierId] ON [CarrierReport] ([CarrierId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250221192157_CarrierReports', N'6.0.36');
GO

COMMIT;
GO

