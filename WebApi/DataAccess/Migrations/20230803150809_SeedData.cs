using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[User]
           ([Name]
           ,[Surname]
           ,[TCNo]
           ,[Email]
           ,[Password]
           ,[Telephone]
           ,[PlateNo]
           ,[Role])
     VALUES
           ('admin'
           ,'admin'
           ,'15487855889'
           ,'admin@gmail.com'
           ,'21232f297a57a5a743894a0e4a801fc3'
           ,'05894782636'
           ,'05AD547'
           ,'admin')");

            migrationBuilder.Sql(@"INSERT INTO [dbo].[BankCardInfo]
           ([UserId]
           ,[Balance]
           ,[CardNo])
     VALUES
           (1
           ,0
           ,1001)");


            migrationBuilder.Sql(@"INSERT INTO [dbo].[Genre]([Name])VALUES('Elektrik')");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Genre]([Name])VALUES('Su')");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Genre]([Name])VALUES('Doğalgaz')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
