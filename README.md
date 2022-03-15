# WebProject

# DATABASE NAME: WebProjectDb

# Table 1:

CREATE TABLE [dbo].[Product] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (50)  NULL,
    [ImageName] NVARCHAR (100) NULL,
    [Price]     INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

# Table 2:

CREATE TABLE [dbo].[Customer] (
    [EmailAddress]    NVARCHAR (50) NOT NULL,
    [Name]            NVARCHAR (50) NULL,
    [PhoneNumber]     NVARCHAR (50) NULL,
    [isPhoneVerified] CHAR (1)      NULL,
    PRIMARY KEY CLUSTERED ([EmailAddress] ASC)
);

# Table 3:

CREATE TABLE [dbo].[Cart] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [CustomerEmail] NVARCHAR (50) NOT NULL,
    [Status]        NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Cart_Customer] FOREIGN KEY ([CustomerEmail]) REFERENCES [dbo].[Customer] ([EmailAddress])
);

# Table 4:

CREATE TABLE [dbo].[CartItem] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [ProductId] INT NOT NULL,
    [CartId]    INT NOT NULL,
    [Quantity]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Table_Cart] FOREIGN KEY ([CartId]) REFERENCES [dbo].[Cart] ([Id]),
    CONSTRAINT [FK_Table_Prodcut] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
);

# Table 5: 

CREATE TABLE [dbo].[CardCode] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [ProductId] INT            NOT NULL,
    [Code]      NVARCHAR (100) NOT NULL,
    [Status]    NVARCHAR (20)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Table_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_CardCode_ProductId]
    ON [dbo].[CardCode]([ProductId] ASC);

