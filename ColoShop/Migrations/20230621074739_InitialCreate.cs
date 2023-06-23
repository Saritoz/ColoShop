using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColoShop.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_Post_tb_Product",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_Post_User",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_IdProduct",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_IdUser",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "IdProduct",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "Post",
                newName: "CategoryID");

            migrationBuilder.AddColumn<int>(
                name: "IdRole",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductCategory",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductCategory",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "amount",
                table: "Product_Order",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Avatar",
                table: "Product",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeature",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHome",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHot",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSale",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProductionCode",
                table: "Product",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Product",
                type: "int",
                nullable: true,
                defaultValueSql: "((0))");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Post",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Post",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Detail",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Post",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Post",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Order",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Order",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "(N'pending')");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "New",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statistic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    ViewCount = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistic", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Statistic");

            migrationBuilder.DropColumn(
                name: "IdRole",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductCategory");

            migrationBuilder.DropColumn(
                name: "amount",
                table: "Product_Order");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IsFeature",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IsHome",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IsHot",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IsSale",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductionCode",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Detail",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Post",
                newName: "IdUser");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductCategory",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Post",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<int>(
                name: "IdProduct",
                table: "Post",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "New",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.CreateIndex(
                name: "IX_Post_IdProduct",
                table: "Post",
                column: "IdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_Post_IdUser",
                table: "Post",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_Post_tb_Product",
                table: "Post",
                column: "IdProduct",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_Post_User",
                table: "Post",
                column: "IdUser",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
