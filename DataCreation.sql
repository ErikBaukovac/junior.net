--Creation script

CREATE TABLE dbo.PaymentType
(
	PaymentTypeId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	PaymentName NVARCHAR(100) NOT NULL	
);

CREATE TABLE dbo.OrderStatus
(
	OrderStatusId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	StatusName NVARCHAR(50) NOT NULL
);

CREATE TABLE dbo.Currency
(
	CurrencyId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	CurrencyName NVARCHAR(50) NOT NULL
);

CREATE TABLE dbo.Item
(
	ItemId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	ItemName NVARCHAR(200) NOT NULL
);

CREATE TABLE dbo.Customer
(
	CustomerId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	CustomerName NVARCHAR(200) NOT NULL,
	PhoneNumber NVARCHAR(13)
);

CREATE TABLE dbo.CustomerAddress
(
	CustomerAddressId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	CustomerId INT NOT NULL CONSTRAINT FK_CustomerAddress_Customer_CustomerId FOREIGN KEY REFERENCES Customer(CustomerId),
	StreetName NVARCHAR(200) NOT NULL,
	BuildingNo NVARCHAR(100),
	ApartmentNo NVARCHAR(100),
	PostCode NVARCHAR(15),
	City NVARCHAR(100)
);

CREATE TABLE dbo.ItemPrice
(
	ItemPriceId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	ItemId INT NOT NULL CONSTRAINT FK_ItemPrice_Item_ItemId FOREIGN KEY REFERENCES Item(ItemId),
	Price DECIMAL(10, 2) NOT NULL,
	CurrencyId INT NOT NULL CONSTRAINT FK_ItemPrice_Currency_CurrencyId FOREIGN KEY REFERENCES Currency(CurrencyId)
);

CREATE TABLE dbo.[Order]
(
	OrderId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	OrderStatusId INT NOT NULL CONSTRAINT FK_Order_OrderStatus_OrderStatusId FOREIGN KEY REFERENCES OrderStatus(OrderStatusId),
	PaymentTypeId INT NOT NULL CONSTRAINT FK_Order_PaymentType_PaymentTypeId FOREIGN KEY REFERENCES PaymentType(PaymentTypeId),
	CustomerAddressId INT NOT NULL CONSTRAINT FK_Order_CustomerAddress_CustomerAddressId FOREIGN KEY REFERENCES CustomerAddress(CustomerAddressId),
	TotalValue DECIMAL(10,2),
	CurrencyId INT NOT NULL CONSTRAINT FK_Order_Currency_CurrencyId FOREIGN KEY REFERENCES Currency(CurrencyId),
	Comments NVARCHAR(300) NULL,
	CreatedAt DATETIME NOT NULL
);

CREATE TABLE dbo.OrderItem
(
	OrderItemId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	OrderId INT NOT NULL CONSTRAINT FK_OrderItem_Order_OrderId FOREIGN KEY REFERENCES [Order](OrderId),
	ItemPriceId INT NOT NULL CONSTRAINT FK_OrderItem_ItemPrice_ItemPriceId FOREIGN KEY REFERENCES ItemPrice(ItemPriceId),
	Quantity INT NOT NULL
);