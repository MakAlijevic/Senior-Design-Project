using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APShopBE.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ProductConditionRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "conditionId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Condition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    conditionType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condition", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_conditionId",
                table: "Products",
                column: "conditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Condition_conditionId",
                table: "Products",
                column: "conditionId",
                principalTable: "Condition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Condition_conditionId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Condition");

            migrationBuilder.DropIndex(
                name: "IX_Products_conditionId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "conditionId",
                table: "Products");
        }
    }
}
