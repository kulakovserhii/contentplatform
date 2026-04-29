using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContentPlatform.Migrations
{
    /// <inheritdoc />
    public partial class Created15Achievements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "BadgeTitle", "Category", "Description", "ImageUrl", "Name", "RequirementValue" },
                values: new object[,]
                {
                    { 1, null, 5, "Залишено перший відгук", null, "Дебют", 1 },
                    { 2, null, 5, "Ваш відгук отриав перший лайк", null, "Перша вподобайка", 1 },
                    { 3, null, 5, "Залишено 15 відгуків сумарно", null, "Критик", 15 },
                    { 4, null, 5, "Ваші відгуки зібрали 100 лайків", null, "Зірка", 100 },
                    { 5, null, 5, "За реєстрацію в системі", null, "Ласкаво просимо", 0 },
                    { 6, null, 0, "5 відгуків на фільми", null, "Кіноман", 5 },
                    { 7, null, 4, "5 відгуків на серіали", null, "Серіаломан", 5 },
                    { 8, null, 1, "5 відгуків на музику", null, "Меломан", 5 },
                    { 9, null, 2, "5 відгуків на ігри", null, "Геймер", 5 },
                    { 10, null, 3, "5 відгуків на книги", null, "Книголюб", 5 },
                    { 11, null, 5, "Ви поставили 50 лайків іншим", null, "Активний виборець", 50 },
                    { 12, null, 5, "Один відгук набрав 30 лайків", null, "Думка громади", 30 },
                    { 13, null, 5, "Сумарно отримано 500 лайків", null, "Популярний автор", 500 },
                    { 14, null, 5, "Поставлено 5 оцінок 10/10", null, "Перфекціоніст", 5 },
                    { 15, null, 5, "Поставлено 5 оцінок 3/10 або нижче", null, "Скептик", 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
