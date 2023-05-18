using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVVM2004PurchasingManaging.Migrations
{
    /// <inheritdoc />
    public partial class OrderRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "IndeksPriceRecords");

            migrationBuilder.CreateTable(
                name: "OrderPriceRecords",
                columns: table => new
                {
                    OrderPriceRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IndeksId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    PlantId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalIndeksPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPriceRecords", x => x.OrderPriceRecordId);
                    table.ForeignKey(
                        name: "FK_OrderPriceRecords_IndeksPriceRecords_IndeksId_SupplierId_PlantId",
                        columns: x => new { x.IndeksId, x.SupplierId, x.PlantId },
                        principalTable: "IndeksPriceRecords",
                        principalColumns: new[] { "IndeksId", "SupplierId", "PlantId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderRecords",
                columns: table => new
                {
                    OrderRecordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRecords", x => x.OrderRecordId);
                });

            migrationBuilder.CreateTable(
                name: "OrderPriceRecordOrderRecord",
                columns: table => new
                {
                    OrderPriceRecordsOrderPriceRecordId = table.Column<int>(type: "int", nullable: false),
                    OrderRecordsOrderRecordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPriceRecordOrderRecord", x => new { x.OrderPriceRecordsOrderPriceRecordId, x.OrderRecordsOrderRecordId });
                    table.ForeignKey(
                        name: "FK_OrderPriceRecordOrderRecord_OrderPriceRecords_OrderPriceRecordsOrderPriceRecordId",
                        column: x => x.OrderPriceRecordsOrderPriceRecordId,
                        principalTable: "OrderPriceRecords",
                        principalColumn: "OrderPriceRecordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPriceRecordOrderRecord_OrderRecords_OrderRecordsOrderRecordId",
                        column: x => x.OrderRecordsOrderRecordId,
                        principalTable: "OrderRecords",
                        principalColumn: "OrderRecordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderPriceRecordOrderRecord_OrderRecordsOrderRecordId",
                table: "OrderPriceRecordOrderRecord",
                column: "OrderRecordsOrderRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPriceRecords_IndeksId_SupplierId_PlantId",
                table: "OrderPriceRecords",
                columns: new[] { "IndeksId", "SupplierId", "PlantId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderPriceRecordOrderRecord");

            migrationBuilder.DropTable(
                name: "OrderPriceRecords");

            migrationBuilder.DropTable(
                name: "OrderRecords");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "IndeksPriceRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
