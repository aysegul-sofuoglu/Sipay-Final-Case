using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[User]([Name],[Surname],[TCNo],[Email],[Password],[Telephone],[PlateNo],[Role])
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
            migrationBuilder.Sql(@"INSERT INTO [dbo].[ApartmentType]([type])VALUES('4+1')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
