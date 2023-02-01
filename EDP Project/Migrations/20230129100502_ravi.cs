using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDPProject.Migrations
{
    /// <inheritdoc />
    public partial class ravi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    CouponId = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CouponName = table.Column<string>(name: "Coupon_Name", type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PercentageDiscount = table.Column<int>(name: "Percentage_Discount", type: "int", nullable: false),
                    ExpiryDate = table.Column<DateTime>(name: "Expiry_Date", type: "date", nullable: false),
                    TotalCouponRedeemed = table.Column<int>(name: "Total_Coupon_Redeemed", type: "int", nullable: false),
                    TotalCouponQuantity = table.Column<int>(name: "Total_Coupon_Quantity", type: "int", nullable: false),
                    CurrentCouponQuantity = table.Column<int>(name: "Current_Coupon_Quantity", type: "int", nullable: false),
                    Pointstoredeem = table.Column<int>(name: "Points_to_redeem", type: "int", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.CouponId);
                });

            migrationBuilder.CreateTable(
                name: "CouponRedemptions",
                columns: table => new
                {
                    CouponRedemptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateofRedemption = table.Column<DateTime>(name: "Date_of_Redemption", type: "date", nullable: false),
                    CouponId = table.Column<string>(type: "nvarchar(5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponRedemptions", x => x.CouponRedemptionId);
                    table.ForeignKey(
                        name: "FK_CouponRedemptions_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "CouponId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CouponRedemptions_CouponId",
                table: "CouponRedemptions",
                column: "CouponId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CouponRedemptions");

            migrationBuilder.DropTable(
                name: "Coupons");
        }
    }
}
