using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APShopBE.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ProductEntityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isModified",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isModified",
                table: "Products");
        }
    }
}
