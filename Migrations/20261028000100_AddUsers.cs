using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WtLab12Banking.AuthValidation.Data;

#nullable disable

namespace WtLab12Banking.AuthValidation.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20261028000100_AddUsers")]
public partial class AddUsers : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Login = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                Password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Users_Login",
            table: "Users",
            column: "Login",
            unique: true);

        migrationBuilder.Sql("INSERT INTO \"Users\" (\"Login\", \"Password\") VALUES ('student', '1234');");

        // WT-8 uses explicit Id values for seven seeded accounts.
        // Move the PostgreSQL identity sequence past those values so Create can insert a new account.
        migrationBuilder.Sql("SELECT setval(pg_get_serial_sequence('\"Accounts\"', 'Id'), COALESCE((SELECT MAX(\"Id\") FROM \"Accounts\"), 1), true);");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Users");
    }
}
