using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVVM2004PurchasingManaging.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalIndeksPrice",
                table: "OrderPriceRecords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalIndeksPrice",
                table: "OrderPriceRecords",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
