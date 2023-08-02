using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[User]([Name],[Surname],[TCNo],[Email],[Telephone],[PlateNo])
                                VALUES('admin','admin','12345678912','admin@gmail.com',12345647865,456)");

            migrationBuilder.Sql(@"INSERT INTO [dbo].[BankCardInfo]([UserId],[Balance],[CardNo])VALUES(1,0,'1')");

            migrationBuilder.Sql(@"INSERT INTO [dbo].[Genre]([Name])VALUES('Elektrik')");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Genre]([Name])VALUES('Su')");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Genre]([Name])VALUES('Doğalgaz')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
