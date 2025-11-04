using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalR.DataAccessLayer.Migrations
{
    public partial class order_restaurantTable_relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantTableID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RestaurantTableID",
                table: "Orders",
                column: "RestaurantTableID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_RestaurantTables_RestaurantTableID",
                table: "Orders",
                column: "RestaurantTableID",
                principalTable: "RestaurantTables",
                principalColumn: "RestaurantTableID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_RestaurantTables_RestaurantTableID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_RestaurantTableID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RestaurantTableID",
                table: "Orders");
        }
    }
}
