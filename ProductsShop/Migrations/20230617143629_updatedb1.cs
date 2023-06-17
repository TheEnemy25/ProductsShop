using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsShop.Migrations
{
    public partial class updatedb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Order_OrderId",
                table: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCart_OrderId",
                table: "ShoppingCart");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ShoppingCartId",
                table: "Order",
                column: "ShoppingCartId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_ShoppingCart_ShoppingCartId",
                table: "Order",
                column: "ShoppingCartId",
                principalTable: "ShoppingCart",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_ShoppingCart_ShoppingCartId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ShoppingCartId",
                table: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_OrderId",
                table: "ShoppingCart",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Order_OrderId",
                table: "ShoppingCart",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");
        }
    }
}
