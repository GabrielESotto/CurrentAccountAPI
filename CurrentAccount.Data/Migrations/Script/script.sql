BEGIN TRANSACTION;
GO

CREATE TABLE [AccountsStatements] (
    [Id] uniqueidentifier NOT NULL,
    [Description] varchar(100) NOT NULL,
    [Date] datetime2 NOT NULL,
    [Value] decimal(18,2) NOT NULL,
    [Detached] int NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_AccountsStatements] PRIMARY KEY ([Id])
);
GO

COMMIT;
GO

