using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AdditionalPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.RequestId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Trains",
                columns: table => new
                {
                    TrainId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TrainNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartStation = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EndStation = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartureTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    BaseTicketPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalSeatCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trains", x => x.TrainId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TravelDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsRecurring = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BookingStatus = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    UserNic = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TrainId = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Trains_TrainId",
                        column: x => x.TrainId,
                        principalTable: "Trains",
                        principalColumn: "TrainId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    SeatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SeatNumber = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TrainId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.SeatId);
                    table.ForeignKey(
                        name: "FK_Seats_Trains_TrainId",
                        column: x => x.TrainId,
                        principalTable: "Trains",
                        principalColumn: "TrainId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BookingRequests",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingRequests", x => new { x.BookingId, x.RequestId });
                    table.ForeignKey(
                        name: "FK_BookingRequests_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingRequests_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "RequestId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BookingSeats",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    SeatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingSeats", x => new { x.BookingId, x.SeatId });
                    table.ForeignKey(
                        name: "FK_BookingSeats_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingSeats_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "SeatId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "RequestId", "AdditionalPrice", "Description" },
                values: new object[,]
                {
                    { 1, 10m, "Window Seat" },
                    { 2, 0m, "Wheelchair Assistance" },
                    { 3, 25m, "Meal" }
                });

            migrationBuilder.InsertData(
                table: "Trains",
                columns: new[] { "TrainId", "ArrivalTime", "BaseTicketPrice", "DepartureTime", "EndStation", "StartStation", "TotalSeatCount", "TrainNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 9, 30, 0, 0, DateTimeKind.Unspecified), 120m, new DateTime(2026, 1, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), "Kandy", "Colombo", 20, "IC101" },
                    { 2, new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 90m, new DateTime(2026, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), "Galle", "Colombo", 20, "IC205" }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "SeatId", "SeatNumber", "TrainId" },
                values: new object[,]
                {
                    { 1, "S1", 1 },
                    { 2, "S2", 1 },
                    { 3, "S3", 1 },
                    { 4, "S4", 1 },
                    { 5, "S5", 1 },
                    { 6, "S6", 1 },
                    { 7, "S7", 1 },
                    { 8, "S8", 1 },
                    { 9, "S9", 1 },
                    { 10, "S10", 1 },
                    { 11, "S11", 1 },
                    { 12, "S12", 1 },
                    { 13, "S13", 1 },
                    { 14, "S14", 1 },
                    { 15, "S15", 1 },
                    { 16, "S16", 1 },
                    { 17, "S17", 1 },
                    { 18, "S18", 1 },
                    { 19, "S19", 1 },
                    { 20, "S20", 1 },
                    { 21, "S1", 2 },
                    { 22, "S2", 2 },
                    { 23, "S3", 2 },
                    { 24, "S4", 2 },
                    { 25, "S5", 2 },
                    { 26, "S6", 2 },
                    { 27, "S7", 2 },
                    { 28, "S8", 2 },
                    { 29, "S9", 2 },
                    { 30, "S10", 2 },
                    { 31, "S11", 2 },
                    { 32, "S12", 2 },
                    { 33, "S13", 2 },
                    { 34, "S14", 2 },
                    { 35, "S15", 2 },
                    { 36, "S16", 2 },
                    { 37, "S17", 2 },
                    { 38, "S18", 2 },
                    { 39, "S19", 2 },
                    { 40, "S20", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingRequests_RequestId",
                table: "BookingRequests",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TrainId",
                table: "Bookings",
                column: "TrainId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingSeats_SeatId",
                table: "BookingSeats",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_TrainId",
                table: "Seats",
                column: "TrainId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingRequests");

            migrationBuilder.DropTable(
                name: "BookingSeats");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Trains");
        }
    }
}
