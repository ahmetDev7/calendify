using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace calendify_app.Migrations
{
    /// <inheritdoc />
    public partial class update_event_attendance_columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Event_Attendance",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Feedback",
                table: "Event_Attendance",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "Event",
                columns: new[] { "Id", "AdminApproval", "Date", "Description", "EndTime", "Location", "StartTime", "Title", "UserId" },
                values: new object[,]
                {
                    { new Guid("200b7a82-7b43-4fc2-b26a-5b503b1ee9d9"), true, new DateTime(2024, 10, 15, 12, 0, 0, 0, DateTimeKind.Utc), "Een informele lunch waarbij een spreker zijn expertise deelt over een vakgebied.", new DateTime(2024, 10, 15, 13, 0, 0, 0, DateTimeKind.Utc), "Utrecht", new DateTime(2024, 10, 15, 12, 0, 0, 0, DateTimeKind.Utc), "Lunch & Learn", null },
                    { new Guid("25c662f0-4a27-4511-9604-fff06fb7101a"), true, new DateTime(2024, 10, 1, 21, 0, 0, 0, DateTimeKind.Utc), "baba", new DateTime(2024, 9, 1, 10, 0, 0, 0, DateTimeKind.Utc), "Rotterdam", new DateTime(2024, 9, 1, 10, 0, 0, 0, DateTimeKind.Utc), "Dinner op kantoor", null },
                    { new Guid("34b8ada2-b7f4-4ab0-b294-ca8bc36a23d6"), true, new DateTime(2024, 10, 10, 9, 0, 0, 0, DateTimeKind.Utc), "Een dag vol activiteiten om de samenwerking tussen teams te verbeteren.", new DateTime(2024, 10, 10, 17, 0, 0, 0, DateTimeKind.Utc), "Den Haag", new DateTime(2024, 10, 10, 9, 0, 0, 0, DateTimeKind.Utc), "Teambuilding Dag", null },
                    { new Guid("3dfa10d9-b6d0-4f54-a304-064764684f29"), false, new DateTime(2024, 10, 12, 11, 0, 0, 0, DateTimeKind.Utc), "Een creatieve sessie om nieuwe ideeën en innovaties te bedenken voor het bedrijf.", new DateTime(2024, 10, 12, 13, 0, 0, 0, DateTimeKind.Utc), "Rotterdam", new DateTime(2024, 10, 12, 11, 0, 0, 0, DateTimeKind.Utc), "Brainstorm Sessie", null },
                    { new Guid("4f36dd4f-8109-4d27-915e-191c1deba5a5"), true, new DateTime(2024, 10, 25, 16, 30, 0, 0, DateTimeKind.Utc), "Vrijdagmiddagborrel om de werkweek af te sluiten met collega's.", new DateTime(2024, 10, 25, 18, 30, 0, 0, DateTimeKind.Utc), "Rotterdam", new DateTime(2024, 10, 25, 16, 30, 0, 0, DateTimeKind.Utc), "Vrijmibo op kantoor", null },
                    { new Guid("ada7b4f5-d0a2-490f-9d0f-acafd4cb9f32"), true, new DateTime(2024, 10, 20, 14, 0, 0, 0, DateTimeKind.Utc), "Presentatie van het nieuwe project voor een belangrijke klant.", new DateTime(2024, 10, 20, 15, 30, 0, 0, DateTimeKind.Utc), "Eindhoven", new DateTime(2024, 10, 20, 14, 0, 0, 0, DateTimeKind.Utc), "Presentatie Klantproject", null },
                    { new Guid("b0b0af25-4fcf-466f-97da-c27261afbb29"), true, new DateTime(2024, 10, 5, 13, 0, 0, 0, DateTimeKind.Utc), "Een interactieve sessie over het verbeteren van productiviteit op de werkvloer.", new DateTime(2024, 10, 5, 15, 0, 0, 0, DateTimeKind.Utc), "Amsterdam", new DateTime(2024, 10, 5, 13, 0, 0, 0, DateTimeKind.Utc), "Workshop Productiviteit", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("200b7a82-7b43-4fc2-b26a-5b503b1ee9d9"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("25c662f0-4a27-4511-9604-fff06fb7101a"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("34b8ada2-b7f4-4ab0-b294-ca8bc36a23d6"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("3dfa10d9-b6d0-4f54-a304-064764684f29"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("4f36dd4f-8109-4d27-915e-191c1deba5a5"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("ada7b4f5-d0a2-490f-9d0f-acafd4cb9f32"));

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: new Guid("b0b0af25-4fcf-466f-97da-c27261afbb29"));

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Event_Attendance",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Feedback",
                table: "Event_Attendance",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

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
    }
}
