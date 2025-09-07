To run the app:
1. Run the DataCreation.sql script to create tables for AbySalto database
2. Run the DataInsertion.sql to insert data to the database
3. Run the solution of a project
4. Start the app and navigate to terminal
5. Click link with localhost address to open Swagger
Example of order to add:

{
  "customerName": "Marko Horvat",
  "comments": "Ostaviti na recepciji",
  "paymentType": "Mobile Payment",
  "customerStreet": "Slavonska Avenija",
  "buildingNo": "20",
  "apartmentNo": null,
  "postCode": "10000",
  "customerCity": "Zagreb",
  "phoneNumber": "+385934567890",
  "currency": "EUR",
  "items": [
    {
      "itemName": "Špageti Carbonara",
      "price": 9.50,
      "quantity": 2
    },
    {
      "itemName": "Tiramisu",
      "price": 5.00,
      "quantity": 1
    }
  ]
}

