using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace calendify_app.Migrations
{
    /// <inheritdoc />
    public partial class seeding_db_events_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Event",
                columns: new[] { "Id", "AdminApproval", "Date", "Description", "EndTime", "Location", "StartTime", "Title", "UserId" },
                values: new object[,]
                {
                    { new Guid("06a757aa-2e73-4a31-9ec4-b3e27e7412c7"), true, new DateTime(2024, 10, 10, 9, 0, 0, 0, DateTimeKind.Utc), "Een dag vol activiteiten om de samenwerking tussen teams te verbeteren.", new DateTime(2024, 10, 10, 17, 0, 0, 0, DateTimeKind.Utc), "Den Haag", new DateTime(2024, 10, 10, 9, 0, 0, 0, DateTimeKind.Utc), "Teambuilding Dag", null },
                    { new Guid("58be84cc-651e-4ca3-b43d-319e295f3055"), true, new DateTime(2024, 10, 20, 14, 0, 0, 0, DateTimeKind.Utc), "Presentatie van het nieuwe project voor een belangrijke klant.", new DateTime(2024, 10, 20, 15, 30, 0, 0, DateTimeKind.Utc), "Eindhoven", new DateTime(2024, 10, 20, 14, 0, 0, 0, DateTimeKind.Utc), "Presentatie Klantproject", null },
                    { new Guid("591d5969-6a62-425a-add8-b95b6c13beec"), false, new DateTime(2024, 10, 12, 11, 0, 0, 0, DateTimeKind.Utc), "Een creatieve sessie om nieuwe ideeën en innovaties te bedenken voor het bedrijf.", new DateTime(2024, 10, 12, 13, 0, 0, 0, DateTimeKind.Utc), "Rotterdam", new DateTime(2024, 10, 12, 11, 0, 0, 0, DateTimeKind.Utc), "Brainstorm Sessie", null },
                    { new Guid("8218f536-8436-4d3a-a4b9-da19cd0f6398"), true, new DateTime(2024, 10, 25, 16, 30, 0, 0, DateTimeKind.Utc), "Vrijdagmiddagborrel om de werkweek af te sluiten met collega's.", new DateTime(2024, 10, 25, 18, 30, 0, 0, DateTimeKind.Utc), "Rotterdam", new DateTime(2024, 10, 25, 16, 30, 0, 0, DateTimeKind.Utc), "Vrijmibo op kantoor", null },
                    { new Guid("8929c16d-cb08-4a4c-a06e-7379291ad951"), true, new DateTime(2024, 10, 5, 13, 0, 0, 0, DateTimeKind.Utc), "Een interactieve sessie over het verbeteren van productiviteit op de werkvloer.", new DateTime(2024, 10, 5, 15, 0, 0, 0, DateTimeKind.Utc), "Amsterdam", new DateTime(2024, 10, 5, 13, 0, 0, 0, DateTimeKind.Utc), "Workshop Productiviteit", null },
                    { new Guid("99f2cebf-e1d1-4a84-9331-78e6812dd580"), true, new DateTime(2024, 10, 1, 21, 0, 0, 0, DateTimeKind.Utc), "baba", new DateTime(2024, 9, 1, 10, 0, 0, 0, DateTimeKind.Utc), "Rotterdam", new DateTime(2024, 9, 1, 10, 0, 0, 0, DateTimeKind.Utc), "Dinner op kantoor", null },
                    { new Guid("b6cf6bba-c7b8-46f7-a911-389d4ac97cb5"), true, new DateTime(2024, 10, 15, 12, 0, 0, 0, DateTimeKind.Utc), "Een informele lunch waarbij een spreker zijn expertise deelt over een vakgebied.", new DateTime(2024, 10, 15, 13, 0, 0, 0, DateTimeKind.Utc), "Utrecht", new DateTime(2024, 10, 15, 12, 0, 0, 0, DateTimeKind.Utc), "Lunch & Learn", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("06a757aa-2e73-4a31-9ec4-b3e27e7412c7"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("58be84cc-651e-4ca3-b43d-319e295f3055"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("591d5969-6a62-425a-add8-b95b6c13beec"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("8218f536-8436-4d3a-a4b9-da19cd0f6398"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("8929c16d-cb08-4a4c-a06e-7379291ad951"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("99f2cebf-e1d1-4a84-9331-78e6812dd580"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("b6cf6bba-c7b8-46f7-a911-389d4ac97cb5"));
        }
    }
}
