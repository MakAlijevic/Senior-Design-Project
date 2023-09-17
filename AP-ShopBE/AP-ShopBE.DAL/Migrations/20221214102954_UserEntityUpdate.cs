using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APShopBE.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserEntityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isCartNotified",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isCartNotified",
                table: "Users");
        }
    }
}
