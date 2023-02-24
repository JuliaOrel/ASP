using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP_DZ_6_Books.Migrations
{
    public partial class AddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Books",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        NameBook = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        FIOAuthor = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        YearIssue = table.Column<int>(type: "int", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false),
            //        Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Books", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
