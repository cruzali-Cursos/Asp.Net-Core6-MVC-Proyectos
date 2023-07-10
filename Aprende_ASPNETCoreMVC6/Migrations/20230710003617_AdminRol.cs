using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aprende_ASPNETCoreMVC6.Migrations
{
    public partial class AdminRol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF NOT EXISTS(SELECT Id FROM AspNetRoles 
                                WHERE Id = '7337f3aa-9917-45f4-ad95-0d05daf80e74')
                                BEGIN
                                    INSERT AspNetRoles (Id, [Name], [NormalizedName]) 
                                    VALUES ('7337f3aa-9917-45f4-ad95-0d05daf80e74', 'admin','ADMIN')
                                END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE AspNetRoles Where Id = '7337f3aa-9917-45f4-ad95-0d05daf80e74'");

        }
    }
}
