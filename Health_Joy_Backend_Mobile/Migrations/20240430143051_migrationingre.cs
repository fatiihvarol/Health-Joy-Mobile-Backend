using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Health_Joy_Backend_Mobile.Migrations
{
    /// <inheritdoc />
    public partial class migrationingre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientProduct_Ingredients_IngredientsIngredientId",
                table: "IngredientProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientProduct_Products_ProductsProductId",
                table: "IngredientProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientProduct",
                table: "IngredientProduct");

            migrationBuilder.RenameTable(
                name: "IngredientProduct",
                newName: "ProductIngredients");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientProduct_ProductsProductId",
                table: "ProductIngredients",
                newName: "IX_ProductIngredients_ProductsProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductIngredients",
                table: "ProductIngredients",
                columns: new[] { "IngredientsIngredientId", "ProductsProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIngredients_Ingredients_IngredientsIngredientId",
                table: "ProductIngredients",
                column: "IngredientsIngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIngredients_Products_ProductsProductId",
                table: "ProductIngredients",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductIngredients_Ingredients_IngredientsIngredientId",
                table: "ProductIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductIngredients_Products_ProductsProductId",
                table: "ProductIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductIngredients",
                table: "ProductIngredients");

            migrationBuilder.RenameTable(
                name: "ProductIngredients",
                newName: "IngredientProduct");

            migrationBuilder.RenameIndex(
                name: "IX_ProductIngredients_ProductsProductId",
                table: "IngredientProduct",
                newName: "IX_IngredientProduct_ProductsProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientProduct",
                table: "IngredientProduct",
                columns: new[] { "IngredientsIngredientId", "ProductsProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientProduct_Ingredients_IngredientsIngredientId",
                table: "IngredientProduct",
                column: "IngredientsIngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientProduct_Products_ProductsProductId",
                table: "IngredientProduct",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
