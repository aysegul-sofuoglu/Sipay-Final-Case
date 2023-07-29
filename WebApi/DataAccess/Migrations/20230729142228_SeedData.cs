using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Genre]([Name])VALUES('Elektrik')");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Genre]([Name])VALUES('Su')");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Genre]([Name])VALUES('Doğalgaz')");

            migrationBuilder.Sql(@"INSERT INTO [dbo].[User]([Name],[Surname],[TCNo],[Email],[Telephone],[PlateNo])
                                VALUES('Aysegul','Sofuoğlu','12345678912','ayse@gmail.com',12345647865,456)");

            migrationBuilder.Sql(@"INSERT INTO [dbo].[Apartment]([UserId],[Block],[Situation],[Type],[Floor],[ApartmentNo])
                                VALUES(1,'A',1,'2+1',10,20)");

            migrationBuilder.Sql(@"INSERT INTO [dbo].[BankCardInfo]([UserId],[Balance],[CardNo])VALUES(1,14000,10001)");

            migrationBuilder.Sql(@"INSERT INTO [dbo].[Dues]([ApartmentId],[Mounth],[Year],[Amounth])VALUES(1,2,2022,500)");

            migrationBuilder.Sql(@"INSERT INTO [dbo].[Invoice]([GenreId],[ApartmentId],[Mounth],[Year],[Amounth])VALUES(1,1,2,2022,500)");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
