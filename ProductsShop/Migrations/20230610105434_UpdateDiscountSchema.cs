using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsShop.Migrations
{
    public partial class UpdateDiscountSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountStartDate",
                table: "Discount",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "DiscountPercent",
                table: "Discount",
                newName: "Discrount");

            migrationBuilder.RenameColumn(
                name: "DiscountEndDate",
                table: "Discount",
                newName: "EndDate");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Discount",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Discount");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Discount",
                newName: "DiscountStartDate");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Discount",
                newName: "DiscountEndDate");

            migrationBuilder.RenameColumn(
                name: "Discrount",
                table: "Discount",
                newName: "DiscountPercent");
        }
    }
}
