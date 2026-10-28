using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using WtLab12Banking.AuthValidation.Data;

#nullable disable

namespace WtLab12Banking.AuthValidation.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20261019000200_AddAccountDescription")]
public partial class AddAccountDescription : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Description",
            table: "Accounts",
            type: "character varying(200)",
            maxLength: 200,
            nullable: false,
            defaultValue: "");

        migrationBuilder.UpdateData("Accounts", "Id", 1, "Description", "Счёт для повседневных расходов");
        migrationBuilder.UpdateData("Accounts", "Id", 2, "Description", "Сбережения и накопления");
        migrationBuilder.UpdateData("Accounts", "Id", 3, "Description", "Кредитный счёт");
        migrationBuilder.UpdateData("Accounts", "Id", 4, "Description", "Расчёты организации");
        migrationBuilder.UpdateData("Accounts", "Id", 5, "Description", "Накопления в долларах");
        migrationBuilder.UpdateData("Accounts", "Id", 6, "Description", "Планируемые расходы");
        migrationBuilder.UpdateData("Accounts", "Id", 7, "Description", "Счёт в евро");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "Description", table: "Accounts");
    }
}
