using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APShopBE.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ProductShippingRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Condition_conditionId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Condition",
                table: "Condition");

            migrationBuilder.RenameTable(
                name: "Condition",
                newName: "Conditions");

            migrationBuilder.AddColumn<int>(
                name: "shippingId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conditions",
                table: "Conditions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Shippings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    shippingType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_shippingId",
                table: "Products",
                column: "shippingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Conditions_conditionId",
                table: "Products",
                column: "conditionId",
                principalTable: "Conditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Shippings_shippingId",
                table: "Products",
                column: "shippingId",
                principalTable: "Shippings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Conditions_conditionId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Shippings_shippingId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Shippings");

            migrationBuilder.DropIndex(
                name: "IX_Products_shippingId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conditions",
                table: "Conditions");

            migrationBuilder.DropColumn(
                name: "shippingId",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Conditions",
                newName: "Condition");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Condition",
                table: "Condition",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Condition_conditionId",
                table: "Products",
                column: "conditionId",
                principalTable: "Condition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
