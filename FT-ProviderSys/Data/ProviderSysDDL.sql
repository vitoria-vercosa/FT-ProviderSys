CREATE TABLE Provider
(
	ProviderId INT NOT NULL IDENTITY PRIMARY KEY,
	CorporateName VARCHAR(250) NOT NULL,
	LegalEntityIdentifier VARCHAR(20) NOT NULL,
	State VARCHAR(2),
	ContactName VARCHAR(250),
	ContactEmail VARCHAR(250)
)

CREATE TABLE Product
(
	ProductId INT NOT NULL IDENTITY PRIMARY KEY,
	Code VARCHAR(50) NOT NULL,
	ProductName VARCHAR(100) NOT NULL,
	Description VARCHAR(250),
	RegistrationDate DATETIME NOT NULL,
	Price FLOAT NOT NULL
)

CREATE TABLE [Order]
(
	OrderId INT NOT NULL IDENTITY PRIMARY KEY,
	Code VARCHAR(20) NOT NULL,
	OrderDate DATETIME NOT NULL,
	ProviderId INT NOT NULL,
	Amount FLOAT,
	CONSTRAINT [FK_Order_Provider] FOREIGN KEY (ProviderId) REFERENCES Provider(ProviderId)
)

CREATE TABLE Order_Product
(
	OrderId INT NOT NULL,
	ProductId INT NOT NULL,
	CONSTRAINT [FK_OP_Order] FOREIGN KEY (OrderId) REFERENCES [Order](OrderId),
	CONSTRAINT [FK_OP_Product] FOREIGN KEY (ProductId) REFERENCES [Product](ProductId),
	CONSTRAINT [PK_Table] PRIMARY KEY ([OrderId],[ProductId])
)