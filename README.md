# Sipay .Net Bootcamp Final Case
 ## Description
 The project is prepared for Patikadev Sipay .Net Bootcamp. It is a backend project prepared for site-apartment management application.It is designed for the management of electricity, water, natural gas bills and dues of the apartments in the site. 
 
 - The project was developed in C# and layered architecture was used.
 - Used using SOLID principles.

 ## Used Technologies
 - Asp.Net Core 6.0
 - Microsoft SQL Server
 - Entity Framework Core
 - JSON Web Token
 - Migrations
 - Swagger

## Installation and Usage
- You must make your database connections before starting the project. Enter your MSSQL information in the appsettings.json file in the WebApi layer.

![Ekran görüntüsü 2023-08-06 003042](https://github.com/aysegul-sofuoglu/Sipay-Final-Case/assets/73445450/fb994e32-1230-4ab6-b54b-e07cc2938f7c)

- Migrations should be added to the DataAccess layer of the project.
   - `cd.DataAccess`
   - `dotnet ef migrations add InitalMigration -s ../WebApi/`
- Then you need to add seed data.
   -  `dotnet ef migrations add SeedData -s ../WebApi/`

- In the resulting SeedData file, you should add a user in the admin role, a bank card for admin, invoice types, blocks and apartment types. In the resulting SeedData file, you should add a user in the admin role, a bank card for admin, invoice types, blocks and apartment types. When the application first starts, admin should be able to get a token as login. The password given to the user is hashed with md5.

          `migrationBuilder.Sql(@"INSERT INTO [dbo].[User]([Name],[Surname],[TCNo],[Email],[Password],[Telephone],[PlateNo],[Role])
                           VALUES('admin','admin','15487855889','admin@gmail.com','21232f297a57a5a743894a0e4a801fc3','05894782636','05AD547','admin')");

            migrationBuilder.Sql(@"INSERT INTO [dbo].[BankCardInfo]([UserId],[Balance],[CardNo])VALUES(1,0,1001)");


            migrationBuilder.Sql(@"INSERT INTO [dbo].[Genre]([Name])VALUES('Elektrik')");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Genre]([Name])VALUES('Su')");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Genre]([Name])VALUES('Doğalgaz')");

            migrationBuilder.Sql(@"INSERT INTO [dbo].[Block]([Name])VALUES('A')");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Block]([Name])VALUES('B')");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Block]([Name])VALUES('C')");


            migrationBuilder.Sql(@"INSERT INTO [dbo].[ApartmentType]([type])VALUES('1+0')");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[ApartmentType]([type])VALUES('2+1')");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[ApartmentType]([type])VALUES('3+1')");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[ApartmentType]([type])VALUES('4+1')");`

- Update your database. You can see that tables are created.
   -  ` cd..`
   -  `dotnet ef database update --project "./DataAccess" --startup-project "./WebApi"`

## Database Design
![Ekran görüntüsü 2023-08-06 011142](https://github.com/aysegul-sofuoglu/Sipay-Final-Case/assets/73445450/0fa5ef3a-3b34-4db5-a89a-dc9330c49332)
 
 ## Folder Structure
- **WebApi :** This layer contains the APIs used in Controllers.
- **Business :** In this layer, the methods used in APIs are found in service classes.
- **DataAccess :** This layer contains the models used in the project, the designs of these models, and the relations. Database operations are located in this layer. 
- **Base :** Structures that can be used throughout the project are included in this layer.
- **Schema :** The data that the user will see when API requests are called is organized in this layer.

## Role Descriptions and Authorizations
Roles : admin , user
- **Admin** 
  - When the application starts, it gets a token with the existing mail and password information and logs in.
  - Adds new users, updates, deletes, can see users.
  - Assigns users to apartments. Can see apartments. View invoices and dues for apartment buildings. See payment information if paid.
  - Enter the dues and invoice information to be paid per apartment.
  - See incoming payment information.
  - See incoming messages. If it displays messages, the seen information is updated.
  - View unpaid invoices and dues as a list.
- **User** 
  - It sees the invoice and dues information assigned to itself.
  - Can pay by credit card.
  - Can send a message to the administrator.
  - It displays its own apartment and bank card by searching with id.
  - It can add and update a new bank card for itself.
 
  ![sipay](https://github.com/aysegul-sofuoglu/Sipay-Final-Case/assets/73445450/ce0ec22e-8ce5-400d-8350-c296db84973d)

