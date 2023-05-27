using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DisasterAlleviationPOE.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "MonetaryDonation",
                columns: table => new
                {
                    MonetaryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DonationAmount = table.Column<decimal>(type: "Money", nullable: false),
                    DonorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonetaryDonation", x => x.MonetaryID);
                });

            migrationBuilder.CreateTable(
                name: "RequiredAidTypes",
                columns: table => new
                {
                    RequiredAidTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequiredAidName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredAidTypes", x => x.RequiredAidTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Disasters",
                columns: table => new
                {
                    DisasterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredAidTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disasters", x => x.DisasterID);
                    table.ForeignKey(
                        name: "FK_Disasters_RequiredAidTypes_RequiredAidTypeID",
                        column: x => x.RequiredAidTypeID,
                        principalTable: "RequiredAidTypes",
                        principalColumn: "RequiredAidTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsDonations",
                columns: table => new
                {
                    GoodsDonationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfItems = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    DisasterID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsDonations", x => x.GoodsDonationID);
                    table.ForeignKey(
                        name: "FK_GoodsDonations_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsDonations_Disasters_DisasterID",
                        column: x => x.DisasterID,
                        principalTable: "Disasters",
                        principalColumn: "DisasterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseGoods",
                columns: table => new
                {
                    GoodsPurchaseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisasterID = table.Column<int>(type: "int", nullable: false),
                    MonetaryID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseAmount = table.Column<decimal>(type: "Money", nullable: false),
                    DisastersDisasterID = table.Column<int>(type: "int", nullable: true),
                    MonetaryDonationsMonetaryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseGoods", x => x.GoodsPurchaseID);
                    table.ForeignKey(
                        name: "FK_PurchaseGoods_Disasters_DisastersDisasterID",
                        column: x => x.DisastersDisasterID,
                        principalTable: "Disasters",
                        principalColumn: "DisasterID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseGoods_MonetaryDonation_MonetaryDonationsMonetaryID",
                        column: x => x.MonetaryDonationsMonetaryID,
                        principalTable: "MonetaryDonation",
                        principalColumn: "MonetaryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Disasters_RequiredAidTypeID",
                table: "Disasters",
                column: "RequiredAidTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsDonations_CategoryID",
                table: "GoodsDonations",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsDonations_DisasterID",
                table: "GoodsDonations",
                column: "DisasterID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseGoods_DisastersDisasterID",
                table: "PurchaseGoods",
                column: "DisastersDisasterID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseGoods_MonetaryDonationsMonetaryID",
                table: "PurchaseGoods",
                column: "MonetaryDonationsMonetaryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodsDonations");

            migrationBuilder.DropTable(
                name: "PurchaseGoods");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Disasters");

            migrationBuilder.DropTable(
                name: "MonetaryDonation");

            migrationBuilder.DropTable(
                name: "RequiredAidTypes");
        }
    }
}
