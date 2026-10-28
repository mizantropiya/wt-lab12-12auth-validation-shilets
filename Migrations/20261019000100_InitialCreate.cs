using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WtLab12Banking.AuthValidation.Data;

#nullable disable

namespace WtLab12Banking.AuthValidation.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20261019000100_InitialCreate")]
public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Accounts",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                AccountNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                OwnerName = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                Balance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                Nickname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                AccountType = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Accounts", x => x.Id);
            });

        migrationBuilder.InsertData(
            table: "Accounts",
            columns: new[] { "Id", "AccountNumber", "OwnerName", "Currency", "Balance", "Nickname", "AccountType", "CreatedAt" },
            values: new object[,]
            {
                { 1, "BY01BANK0001", "Николь кот", "BYN", 2500m, "Основной счёт", "Обычный счёт", new DateTime(2026,1,1,9,0,0,DateTimeKind.Utc) },
                { 2, "BY01BANK0002", "Анна Бронская", "BYN", 8200m, "Накопления", "Сберегательный", new DateTime(2026,1,2,9,0,0,DateTimeKind.Utc) },
                { 3, "BY01BANK0003", "Иван Шкред", "BYN", 1100m, "Кредитная карта", "Кредитный", new DateTime(2026,1,3,9,0,0,DateTimeKind.Utc) },
                { 4, "BY01BANK0004", "ОАО мм ОО", "BYN", 21000m, "Расчётный счёт", "Бизнес-счёт", new DateTime(2026,1,4,9,0,0,DateTimeKind.Utc) },
                { 5, "BY01BANK0005", "Дана Сан", "USD", 3600m, "На карзину в ЗЯ", "Сберегательный", new DateTime(2026,1,5,9,0,0,DateTimeKind.Utc) },
                { 6, "BY01BANK0006", "Николь Кот", "USD", 750m, "Планируемые расходы", "Кредитный", new DateTime(2026,1,6,9,0,0,DateTimeKind.Utc) },
                { 7, "BY01BANK0007", "Нюша", "EUR", 900m, "Евросчёт", "Обычный счёт", new DateTime(2026,1,7,9,0,0,DateTimeKind.Utc) }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Accounts_AccountNumber",
            table: "Accounts",
            column: "AccountNumber",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Accounts");
    }
}
