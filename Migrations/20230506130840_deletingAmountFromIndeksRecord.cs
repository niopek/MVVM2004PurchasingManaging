using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVVM2004PurchasingManaging.Migrations
{
    /// <inheritdoc />
    public partial class deletingAmountFromIndeksRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "IndeksPriceRecords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "IndeksPriceRecords",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
