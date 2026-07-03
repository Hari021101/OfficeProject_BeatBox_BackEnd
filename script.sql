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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [FullName] nvarchar(max) NULL,
        [IsEmailVerified] bit NOT NULL,
        [IsPhoneVerified] bit NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [Categories] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [ParentId] uniqueidentifier NULL,
        [ParentCategoryId] uniqueidentifier NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Categories_Categories_ParentCategoryId] FOREIGN KEY ([ParentCategoryId]) REFERENCES [Categories] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [Orders] (
        [OrderId] int NOT NULL IDENTITY,
        [UserId] nvarchar(max) NOT NULL,
        [TotalAmount] decimal(18,2) NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [ShippingAddress] nvarchar(max) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Orders] PRIMARY KEY ([OrderId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [OtpRecords] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(max) NOT NULL,
        [Code] nvarchar(max) NOT NULL,
        [Type] int NOT NULL,
        [ExpiresAt] datetime2 NOT NULL,
        [IsUsed] bit NOT NULL,
        CONSTRAINT [PK_OtpRecords] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [Carts] (
        [CartId] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Carts] PRIMARY KEY ([CartId]),
        CONSTRAINT [FK_Carts_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [UserAddresses] (
        [UserAddressId] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [FullName] nvarchar(max) NOT NULL,
        [AddressLine1] nvarchar(max) NOT NULL,
        [AddressLine2] nvarchar(max) NOT NULL,
        [City] nvarchar(max) NOT NULL,
        [State] nvarchar(max) NOT NULL,
        [PostalCode] nvarchar(max) NOT NULL,
        [Country] nvarchar(max) NOT NULL,
        [Phone] nvarchar(max) NOT NULL,
        [IsDefault] bit NOT NULL,
        CONSTRAINT [PK_UserAddresses] PRIMARY KEY ([UserAddressId]),
        CONSTRAINT [FK_UserAddresses_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [Products] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        [DiscountPrice] decimal(18,2) NULL,
        [StockQuantity] int NOT NULL,
        [ImageUrl] nvarchar(max) NOT NULL,
        [CategoryId] uniqueidentifier NOT NULL,
        [Brand] nvarchar(max) NOT NULL,
        [Rating] float NULL,
        [BatteryLife] nvarchar(max) NOT NULL,
        [Color] nvarchar(max) NOT NULL,
        [Connectivity] nvarchar(max) NOT NULL,
        [IsFeatured] bit NOT NULL,
        [SoldCount] int NOT NULL,
        [DeliveryDays] int NOT NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [Payments] (
        [PaymentId] int NOT NULL IDENTITY,
        [OrderId] int NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [Method] nvarchar(max) NOT NULL,
        [TransactionId] nvarchar(max) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Payments] PRIMARY KEY ([PaymentId]),
        CONSTRAINT [FK_Payments_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [CartItems] (
        [CartItemId] int NOT NULL IDENTITY,
        [CartId] int NOT NULL,
        [ProductId] uniqueidentifier NOT NULL,
        [Quantity] int NOT NULL,
        [UnitPrice] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_CartItems] PRIMARY KEY ([CartItemId]),
        CONSTRAINT [FK_CartItems_Carts_CartId] FOREIGN KEY ([CartId]) REFERENCES [Carts] ([CartId]) ON DELETE CASCADE,
        CONSTRAINT [FK_CartItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [Inventories] (
        [Id] uniqueidentifier NOT NULL,
        [ProductId] uniqueidentifier NOT NULL,
        [AvailableStock] int NOT NULL,
        [ReservedStock] int NOT NULL,
        [WarehouseLocation] nvarchar(max) NOT NULL,
        [LowStockThreshold] int NOT NULL,
        [LastUpdated] datetime2 NOT NULL,
        CONSTRAINT [PK_Inventories] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Inventories_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [OrderItems] (
        [OrderItemId] int NOT NULL IDENTITY,
        [OrderId] int NOT NULL,
        [ProductId] uniqueidentifier NOT NULL,
        [Quantity] int NOT NULL,
        [UnitPrice] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_OrderItems] PRIMARY KEY ([OrderItemId]),
        CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE CASCADE,
        CONSTRAINT [FK_OrderItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [ProductFaqs] (
        [Id] int NOT NULL IDENTITY,
        [ProductId] uniqueidentifier NOT NULL,
        [Question] nvarchar(max) NOT NULL,
        [Answer] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ProductFaqs] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductFaqs_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [ProductImages] (
        [Id] int NOT NULL IDENTITY,
        [ProductId] uniqueidentifier NOT NULL,
        [ImageUrl] nvarchar(max) NOT NULL,
        [IsPrimary] bit NOT NULL,
        CONSTRAINT [PK_ProductImages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductImages_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [ProductReviews] (
        [Id] int NOT NULL IDENTITY,
        [ProductId] uniqueidentifier NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        [Rating] int NOT NULL,
        [Comment] nvarchar(max) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [IsVerifiedPurchase] bit NOT NULL,
        CONSTRAINT [PK_ProductReviews] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductReviews_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProductReviews_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [WishlistItems] (
        [WishlistItemId] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ProductId] uniqueidentifier NOT NULL,
        [AddedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_WishlistItems] PRIMARY KEY ([WishlistItemId]),
        CONSTRAINT [FK_WishlistItems_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_WishlistItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE TABLE [InventoryHistories] (
        [Id] uniqueidentifier NOT NULL,
        [InventoryId] uniqueidentifier NOT NULL,
        [Change] int NOT NULL,
        [Reason] nvarchar(max) NOT NULL,
        [Timestamp] datetime2 NOT NULL,
        [PerformedBy] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_InventoryHistories] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_InventoryHistories_Inventories_InventoryId] FOREIGN KEY ([InventoryId]) REFERENCES [Inventories] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_CartItems_CartId] ON [CartItems] ([CartId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_CartItems_ProductId] ON [CartItems] ([ProductId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Carts_UserId] ON [Carts] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Categories_ParentCategoryId] ON [Categories] ([ParentCategoryId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Inventories_ProductId] ON [Inventories] ([ProductId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_InventoryHistories_InventoryId] ON [InventoryHistories] ([InventoryId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_OrderItems_OrderId] ON [OrderItems] ([OrderId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_OrderItems_ProductId] ON [OrderItems] ([ProductId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Payments_OrderId] ON [Payments] ([OrderId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ProductFaqs_ProductId] ON [ProductFaqs] ([ProductId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ProductImages_ProductId] ON [ProductImages] ([ProductId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ProductReviews_ProductId] ON [ProductReviews] ([ProductId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ProductReviews_UserId] ON [ProductReviews] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_UserAddresses_UserId] ON [UserAddresses] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_WishlistItems_ProductId] ON [WishlistItems] ([ProductId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_WishlistItems_UserId] ON [WishlistItems] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602104246_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260602104246_InitialCreate', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602115835_AddInventoryRowVersion'
)
BEGIN
    ALTER TABLE [Inventories] ADD [RowVersion] rowversion NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260602115835_AddInventoryRowVersion'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260602115835_AddInventoryRowVersion', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260608145614_AddNotifications'
)
BEGIN
    CREATE TABLE [Notifications] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Message] nvarchar(max) NOT NULL,
        [IsRead] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Notifications] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Notifications_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260608145614_AddNotifications'
)
BEGIN
    CREATE INDEX [IX_Notifications_UserId] ON [Notifications] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260608145614_AddNotifications'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260608145614_AddNotifications', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260609065953_AddProductImageColors'
)
BEGIN
    ALTER TABLE [ProductImages] ADD [ColorCode] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260609065953_AddProductImageColors'
)
BEGIN
    ALTER TABLE [ProductImages] ADD [ColorName] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260609065953_AddProductImageColors'
)
BEGIN
    CREATE TABLE [ProductVariant] (
        [Id] int NOT NULL IDENTITY,
        [ProductId] uniqueidentifier NOT NULL,
        [ColorName] nvarchar(max) NOT NULL,
        [ColorCode] nvarchar(max) NOT NULL,
        [ImageUrl] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ProductVariant] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductVariant_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260609065953_AddProductImageColors'
)
BEGIN
    CREATE INDEX [IX_ProductVariant_ProductId] ON [ProductVariant] ([ProductId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260609065953_AddProductImageColors'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260609065953_AddProductImageColors', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260609110316_AddCoupons'
)
BEGIN
    CREATE TABLE [Coupons] (
        [Id] int NOT NULL IDENTITY,
        [Code] nvarchar(max) NOT NULL,
        [DiscountAmount] decimal(18,2) NOT NULL,
        [DiscountPercentage] decimal(18,2) NULL,
        [MinimumOrderAmount] decimal(18,2) NOT NULL,
        [ExpiryDate] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        [UsageLimit] int NOT NULL,
        [UsedCount] int NOT NULL,
        CONSTRAINT [PK_Coupons] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260609110316_AddCoupons'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260609110316_AddCoupons', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260609143130_AddCreatedDateToAppUser'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD [CreatedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260609143130_AddCreatedDateToAppUser'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260609143130_AddCreatedDateToAppUser', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260610133056_AddOrderPaymentRelation'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260610133056_AddOrderPaymentRelation', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260610151215_AddPromoFieldsToOrder'
)
BEGIN
    ALTER TABLE [Orders] ADD [DiscountAmount] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260610151215_AddPromoFieldsToOrder'
)
BEGIN
    ALTER TABLE [Orders] ADD [PromoCode] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260610151215_AddPromoFieldsToOrder'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260610151215_AddPromoFieldsToOrder', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611053222_AddProductVariants'
)
BEGIN
    DROP TABLE [ProductVariant];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611053222_AddProductVariants'
)
BEGIN
    CREATE TABLE [ProductVariants] (
        [Id] uniqueidentifier NOT NULL,
        [ProductId] uniqueidentifier NOT NULL,
        [Color] nvarchar(max) NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        [DiscountPrice] decimal(18,2) NOT NULL,
        [StockQuantity] int NOT NULL,
        [ImageUrl] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ProductVariants] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductVariants_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611053222_AddProductVariants'
)
BEGIN
    CREATE INDEX [IX_ProductVariants_ProductId] ON [ProductVariants] ([ProductId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611053222_AddProductVariants'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260611053222_AddProductVariants', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611062301_AddColorCodeToProductVariant'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductVariants]') AND [c].[name] = N'DiscountPrice');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ProductVariants] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [ProductVariants] ALTER COLUMN [DiscountPrice] decimal(18,2) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611062301_AddColorCodeToProductVariant'
)
BEGIN
    ALTER TABLE [ProductVariants] ADD [ColorCode] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611062301_AddColorCodeToProductVariant'
)
BEGIN
    ALTER TABLE [CartItems] ADD [VariantId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611062301_AddColorCodeToProductVariant'
)
BEGIN
    CREATE INDEX [IX_CartItems_VariantId] ON [CartItems] ([VariantId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611062301_AddColorCodeToProductVariant'
)
BEGIN
    ALTER TABLE [CartItems] ADD CONSTRAINT [FK_CartItems_ProductVariants_VariantId] FOREIGN KEY ([VariantId]) REFERENCES [ProductVariants] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611062301_AddColorCodeToProductVariant'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260611062301_AddColorCodeToProductVariant', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611073533_FixVariantModel'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Color');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Products] DROP COLUMN [Color];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611073533_FixVariantModel'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'DiscountPrice');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Products] DROP COLUMN [DiscountPrice];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611073533_FixVariantModel'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'ImageUrl');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Products] DROP COLUMN [ImageUrl];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611073533_FixVariantModel'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Price');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Products] DROP COLUMN [Price];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611073533_FixVariantModel'
)
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'StockQuantity');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Products] DROP COLUMN [StockQuantity];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611073533_FixVariantModel'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260611073533_FixVariantModel', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611133806_ProductIndexes'
)
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Name');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Products] ALTER COLUMN [Name] nvarchar(450) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611133806_ProductIndexes'
)
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Brand');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Products] ALTER COLUMN [Brand] nvarchar(450) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611133806_ProductIndexes'
)
BEGIN
    CREATE INDEX [IX_Products_Brand] ON [Products] ([Brand]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611133806_ProductIndexes'
)
BEGIN
    CREATE INDEX [IX_Products_Name] ON [Products] ([Name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611133806_ProductIndexes'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260611133806_ProductIndexes', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611143249_RemoveProductIndexes'
)
BEGIN
    DROP INDEX [IX_Products_Brand] ON [Products];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611143249_RemoveProductIndexes'
)
BEGIN
    DROP INDEX [IX_Products_Name] ON [Products];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611143249_RemoveProductIndexes'
)
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Name');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Products] ALTER COLUMN [Name] nvarchar(max) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611143249_RemoveProductIndexes'
)
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Brand');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Products] ALTER COLUMN [Brand] nvarchar(max) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260611143249_RemoveProductIndexes'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260611143249_RemoveProductIndexes', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260612061430_AddProductVariantsImages'
)
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductVariants]') AND [c].[name] = N'ImageUrl');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [ProductVariants] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [ProductVariants] DROP COLUMN [ImageUrl];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260612061430_AddProductVariantsImages'
)
BEGIN
    CREATE TABLE [ProductVariantImages] (
        [Id] uniqueidentifier NOT NULL,
        [VariantId] uniqueidentifier NOT NULL,
        [ImageUrl] nvarchar(max) NOT NULL,
        [IsPrimary] bit NOT NULL,
        [DisplayOrder] int NOT NULL,
        CONSTRAINT [PK_ProductVariantImages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductVariantImages_ProductVariants_VariantId] FOREIGN KEY ([VariantId]) REFERENCES [ProductVariants] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260612061430_AddProductVariantsImages'
)
BEGIN
    CREATE INDEX [IX_ProductVariantImages_VariantId] ON [ProductVariantImages] ([VariantId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260612061430_AddProductVariantsImages'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260612061430_AddProductVariantsImages', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260617133305_AutoMigration'
)
BEGIN
    CREATE TABLE [AuditLogs] (
        [Id] uniqueidentifier NOT NULL,
        [AdminId] nvarchar(max) NOT NULL,
        [AdminName] nvarchar(max) NOT NULL,
        [Role] nvarchar(max) NOT NULL,
        [Action] nvarchar(max) NOT NULL,
        [Target] nvarchar(max) NOT NULL,
        [Details] nvarchar(max) NOT NULL,
        [Timestamp] datetime2 NOT NULL,
        [Icon] nvarchar(max) NOT NULL,
        [ColorClass] nvarchar(max) NOT NULL,
        [BgClass] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_AuditLogs] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260617133305_AutoMigration'
)
BEGIN
    CREATE TABLE [ReturnRequests] (
        [Id] uniqueidentifier NOT NULL,
        [OrderId] uniqueidentifier NOT NULL,
        [OrderId1] int NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        [ProductId] uniqueidentifier NOT NULL,
        [Reason] nvarchar(max) NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [AdminNotes] nvarchar(max) NULL,
        [RequestDate] datetime2 NOT NULL,
        [ProcessedDate] datetime2 NULL,
        CONSTRAINT [PK_ReturnRequests] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ReturnRequests_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ReturnRequests_Orders_OrderId1] FOREIGN KEY ([OrderId1]) REFERENCES [Orders] ([OrderId]) ON DELETE CASCADE,
        CONSTRAINT [FK_ReturnRequests_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260617133305_AutoMigration'
)
BEGIN
    CREATE INDEX [IX_ReturnRequests_OrderId1] ON [ReturnRequests] ([OrderId1]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260617133305_AutoMigration'
)
BEGIN
    CREATE INDEX [IX_ReturnRequests_ProductId] ON [ReturnRequests] ([ProductId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260617133305_AutoMigration'
)
BEGIN
    CREATE INDEX [IX_ReturnRequests_UserId] ON [ReturnRequests] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260617133305_AutoMigration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260617133305_AutoMigration', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260617135508_AddVariantDataToCartItem'
)
BEGIN
    ALTER TABLE [OrderItems] ADD [Color] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260617135508_AddVariantDataToCartItem'
)
BEGIN
    ALTER TABLE [OrderItems] ADD [ColorCode] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260617135508_AddVariantDataToCartItem'
)
BEGIN
    ALTER TABLE [OrderItems] ADD [ProductImageUrl] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260617135508_AddVariantDataToCartItem'
)
BEGIN
    ALTER TABLE [OrderItems] ADD [ProductVariantId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260617135508_AddVariantDataToCartItem'
)
BEGIN
    ALTER TABLE [CartItems] ADD [Color] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260617135508_AddVariantDataToCartItem'
)
BEGIN
    ALTER TABLE [CartItems] ADD [ColorCode] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260617135508_AddVariantDataToCartItem'
)
BEGIN
    ALTER TABLE [CartItems] ADD [ProductImageUrl] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260617135508_AddVariantDataToCartItem'
)
BEGIN
    ALTER TABLE [CartItems] ADD [ProductVariantId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260617135508_AddVariantDataToCartItem'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260617135508_AddVariantDataToCartItem', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260619112919_AddCategoryImageUrl'
)
BEGIN
    ALTER TABLE [Categories] ADD [ImageUrl] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260619112919_AddCategoryImageUrl'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260619112919_AddCategoryImageUrl', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260625132704_AddIsActiveToAppUser'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260625132704_AddIsActiveToAppUser'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260625132704_AddIsActiveToAppUser', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260629132617_AddVariantFieldsToDb'
)
BEGIN
    ALTER TABLE [ProductVariants] ADD [Capacity] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260629132617_AddVariantFieldsToDb'
)
BEGIN
    ALTER TABLE [ProductVariants] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260629132617_AddVariantFieldsToDb'
)
BEGIN
    ALTER TABLE [ProductVariants] ADD [Sku] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260629132617_AddVariantFieldsToDb'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260629132617_AddVariantFieldsToDb', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260629142018_AddNotificationFieldsAndAuditIp'
)
BEGIN
    ALTER TABLE [Notifications] ADD [Icon] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260629142018_AddNotificationFieldsAndAuditIp'
)
BEGIN
    ALTER TABLE [Notifications] ADD [NavigationUrl] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260629142018_AddNotificationFieldsAndAuditIp'
)
BEGIN
    ALTER TABLE [Notifications] ADD [OrderId] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260629142018_AddNotificationFieldsAndAuditIp'
)
BEGIN
    ALTER TABLE [Notifications] ADD [ProductId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260629142018_AddNotificationFieldsAndAuditIp'
)
BEGIN
    ALTER TABLE [Notifications] ADD [Type] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260629142018_AddNotificationFieldsAndAuditIp'
)
BEGIN
    ALTER TABLE [AuditLogs] ADD [IPAddress] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260629142018_AddNotificationFieldsAndAuditIp'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260629142018_AddNotificationFieldsAndAuditIp', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260703131454_UpdateBeforeDeploy'
)
BEGIN
    ALTER TABLE [ReturnRequests] DROP CONSTRAINT [FK_ReturnRequests_Orders_OrderId1];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260703131454_UpdateBeforeDeploy'
)
BEGIN
    DROP INDEX [IX_ReturnRequests_OrderId1] ON [ReturnRequests];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260703131454_UpdateBeforeDeploy'
)
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ReturnRequests]') AND [c].[name] = N'OrderId1');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [ReturnRequests] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [ReturnRequests] DROP COLUMN [OrderId1];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260703131454_UpdateBeforeDeploy'
)
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ReturnRequests]') AND [c].[name] = N'OrderId');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [ReturnRequests] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [ReturnRequests] DROP COLUMN [OrderId];
    ALTER TABLE [ReturnRequests] ADD [OrderId] int NOT NULL DEFAULT 0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260703131454_UpdateBeforeDeploy'
)
BEGIN
    CREATE INDEX [IX_ReturnRequests_OrderId] ON [ReturnRequests] ([OrderId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260703131454_UpdateBeforeDeploy'
)
BEGIN
    ALTER TABLE [ReturnRequests] ADD CONSTRAINT [FK_ReturnRequests_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260703131454_UpdateBeforeDeploy'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260703131454_UpdateBeforeDeploy', N'9.0.0');
END;

COMMIT;
GO

