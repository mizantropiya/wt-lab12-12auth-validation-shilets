using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WtLab12Banking.AuthValidation.Data;

#nullable disable

namespace WtLab12Banking.AuthValidation.Migrations;

public partial class AddUsers
{
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.4")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity("WtLab12Banking.AuthValidation.Models.Account", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("integer");
            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

            b.Property<string>("AccountNumber")
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnType("character varying(30)");

            b.Property<string>("AccountType")
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnType("character varying(30)");

            b.Property<decimal>("Balance")
                .HasPrecision(18, 2)
                .HasColumnType("numeric(18,2)");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp with time zone");

            b.Property<string>("Currency")
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnType("character varying(3)");

            b.Property<string>("Description")
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("character varying(200)")
                .HasDefaultValue("");

            b.Property<string>("Nickname")
                .HasMaxLength(50)
                .HasColumnType("character varying(50)");

            b.Property<string>("OwnerName")
                .IsRequired()
                .HasMaxLength(80)
                .HasColumnType("character varying(80)");

            b.HasKey("Id");
            b.HasIndex("AccountNumber").IsUnique();
            b.ToTable("Accounts");

            b.HasData(
                new { Id = 1, AccountNumber = "BY01BANK0001", OwnerName = "Николь кот", Currency = "BYN", Balance = 2500m, Nickname = "Основной счёт", AccountType = "Обычный счёт", CreatedAt = new DateTime(2026,1,1,9,0,0,DateTimeKind.Utc), Description = "Счёт для повседневных расходов" },
                new { Id = 2, AccountNumber = "BY01BANK0002", OwnerName = "Анна Бронская", Currency = "BYN", Balance = 8200m, Nickname = "Накопления", AccountType = "Сберегательный", CreatedAt = new DateTime(2026,1,2,9,0,0,DateTimeKind.Utc), Description = "Сбережения и накопления" },
                new { Id = 3, AccountNumber = "BY01BANK0003", OwnerName = "Иван Шкред", Currency = "BYN", Balance = 1100m, Nickname = "Кредитная карта", AccountType = "Кредитный", CreatedAt = new DateTime(2026,1,3,9,0,0,DateTimeKind.Utc), Description = "Кредитный счёт" },
                new { Id = 4, AccountNumber = "BY01BANK0004", OwnerName = "ОАО мм ОО", Currency = "BYN", Balance = 21000m, Nickname = "Расчётный счёт", AccountType = "Бизнес-счёт", CreatedAt = new DateTime(2026,1,4,9,0,0,DateTimeKind.Utc), Description = "Расчёты организации" },
                new { Id = 5, AccountNumber = "BY01BANK0005", OwnerName = "Дана Сан", Currency = "USD", Balance = 3600m, Nickname = "На карзину в ЗЯ", AccountType = "Сберегательный", CreatedAt = new DateTime(2026,1,5,9,0,0,DateTimeKind.Utc), Description = "Накопления в долларах" },
                new { Id = 6, AccountNumber = "BY01BANK0006", OwnerName = "Николь Кот", Currency = "USD", Balance = 750m, Nickname = "Планируемые расходы", AccountType = "Кредитный", CreatedAt = new DateTime(2026,1,6,9,0,0,DateTimeKind.Utc), Description = "Планируемые расходы" },
                new { Id = 7, AccountNumber = "BY01BANK0007", OwnerName = "Нюша", Currency = "EUR", Balance = 900m, Nickname = "Евросчёт", AccountType = "Обычный счёт", CreatedAt = new DateTime(2026,1,7,9,0,0,DateTimeKind.Utc), Description = "Счёт в евро" }
            );
        });

        modelBuilder.Entity("WtLab12Banking.AuthValidation.Models.User", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("integer");
            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

            b.Property<string>("Login")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("character varying(50)");

            b.Property<string>("Password")
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("character varying(100)");

            b.HasKey("Id");
            b.HasIndex("Login").IsUnique();
            b.ToTable("Users");
        });
#pragma warning restore 612, 618
    }
}
