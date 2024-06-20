using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_Project.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Percentage",
                table: "Discounts",
                newName: "Value");

            migrationBuilder.AddColumn<int>(
                name: "ContractId1",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfferType",
                table: "Discounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SoftwareId",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ContractId1",
                table: "Payments",
                column: "ContractId1");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_SoftwareId",
                table: "Discounts",
                column: "SoftwareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Software_SoftwareId",
                table: "Discounts",
                column: "SoftwareId",
                principalTable: "Software",
                principalColumn: "SoftwareId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Contracts_ContractId1",
                table: "Payments",
                column: "ContractId1",
                principalTable: "Contracts",
                principalColumn: "ContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Software_SoftwareId",
                table: "Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Contracts_ContractId1",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ContractId1",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_SoftwareId",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "ContractId1",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "OfferType",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "SoftwareId",
                table: "Discounts");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Discounts",
                newName: "Percentage");
        }
    }
}
