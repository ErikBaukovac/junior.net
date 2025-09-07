--Insertion script

-- Payment types
INSERT INTO dbo.PaymentType (PaymentName)
VALUES ('Cash'), ('Credit Card'), ('Mobile Payment');

-- Order statuses
INSERT INTO dbo.OrderStatus (StatusName)
VALUES ('Na čekanju'), ('U pripremi'), ('završena');

-- Currencies
INSERT INTO dbo.Currency (CurrencyName)
VALUES ('EUR'), ('USD');

-- Items (menu)
INSERT INTO dbo.Item (ItemName)
VALUES ('Margarita'), 
       ('Slavonska'), 
       ('Špageti Carbonara'), 
       ('Caezar Salata'), 
       ('Tiramisu'), 
       ('Coca-Cola 0.5L'), 
       ('Mineral Water 0.5L');

-- Customers
INSERT INTO dbo.Customer (CustomerName, PhoneNumber)
VALUES ('Josip Doe', '+385912345678'),
       ('Anna Prva', '+385923456789'),
       ('Marko Horvat', '+385934567890');

-- Addresses
INSERT INTO dbo.CustomerAddress (CustomerId, StreetName, BuildingNo, ApartmentNo, PostCode, City)
VALUES (1, 'Ilica', '23', '2A', '10000', 'Zagreb'),
       (2, 'Savska Cesta', '44', NULL, '10000', 'Zagreb'),
       (3, 'Zvonimirova', '12B', '4', '21000', 'Split'),
       (3, 'Slavonska Avenija', '20', NULL , '10000', 'Zagreb');

-- Item prices (valid from 2025-01-01)
INSERT INTO dbo.ItemPrice (ItemId, Price, CurrencyId)
VALUES (1, 6.50, 1), 
       (2, 8.00, 1), 
       (3, 9.50, 1), 
       (4, 7.00, 1), 
       (5, 5.00, 1), 
       (6, 2.50, 1), 
       (7, 2.00, 1); 

-- Orders
INSERT INTO dbo.[Order] (OrderStatusId, PaymentTypeId, CustomerAddressId, TotalValue, CurrencyId, Comments, CreatedAt)
VALUES (2, 2, 1, 17.00, 1, 'Tanka kora molio bih vas', '2025-09-01 18:30:00'),
       (3, 1, 2, 11.50, 1, NULL, '2025-09-02 12:15:00'),
       (1, 3, 3, 20.00, 1, 'Ostaviti na recepciji', '2025-09-03 19:45:00');

-- Order items
INSERT INTO dbo.OrderItem (OrderId, ItemPriceId, Quantity)
VALUES (1, 2, 1),  
       (1, 6, 2),  
       (2, 1, 1),  
       (2, 7, 1),  
       (3, 3, 2),  
       (3, 5, 1);  
