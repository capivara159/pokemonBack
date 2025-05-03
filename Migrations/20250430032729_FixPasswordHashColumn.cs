using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonApi.Migrations
{
    /// <inheritdoc />
    public partial class FixPasswordHashColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "senha_hash",
                table: "usuarios",
                newName: "senhahash");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "senhahash",
                table: "usuarios",
                newName: "senha_hash");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");
        }
    }
}
