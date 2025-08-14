using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RPGAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Factions_FactionId",
                table: "Goals");

            migrationBuilder.AlterColumn<int>(
                name: "FactionId",
                table: "Goals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Factions",
                columns: new[] { "Id", "Name", "Resources" },
                values: new object[,]
                {
                    { 1, "Knights of Valor", "[\"Big army\",\"Love of the people\"]" },
                    { 2, "Wizards of the Arcane", "[\"Big library\",\"A lot of money\"]" },
                    { 3, "Rangers of the Wild", "[\"Syvian magic\"]" }
                });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "Id", "Description", "FactionId", "Name", "ProgressMade", "ProgressNecessary" },
                values: new object[,]
                {
                    { 1, "Proteger o reino contra invasores.", 1, "Defender o Reino", 0, 100 },
                    { 2, "Encontrar o artefato lendário perdido.", 2, "Descobrir Artefato Antigo", 0, 50 },
                    { 3, "Conquistar novas terras para a facção.", 1, "Expandir Território", 0, 75 },
                    { 4, "Desenvolver um novo feitiço poderoso.", 2, "Aprimorar Magia", 0, 40 },
                    { 5, "Firmar uma aliança estratégica com os elfos.", 3, "Aliança com Elfos", 0, 30 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Factions_FactionId",
                table: "Goals",
                column: "FactionId",
                principalTable: "Factions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Factions_FactionId",
                table: "Goals");

            migrationBuilder.DeleteData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Factions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Factions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Factions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "FactionId",
                table: "Goals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Factions_FactionId",
                table: "Goals",
                column: "FactionId",
                principalTable: "Factions",
                principalColumn: "Id");
        }
    }
}
