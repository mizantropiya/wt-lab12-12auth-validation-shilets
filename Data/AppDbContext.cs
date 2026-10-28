using Microsoft.EntityFrameworkCore;
using WtLab12Banking.AuthValidation.Models;

namespace WtLab12Banking.AuthValidation.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var account = modelBuilder.Entity<Account>();

        account.ToTable("Accounts");
        account.HasKey(x => x.Id);
        account.HasIndex(x => x.AccountNumber).IsUnique();
        account.Property(x => x.AccountNumber).HasMaxLength(30).IsRequired();
        account.Property(x => x.OwnerName).HasMaxLength(80).IsRequired();
        account.Property(x => x.Currency).HasMaxLength(3).IsRequired();
        account.Property(x => x.Balance).HasPrecision(18, 2);
        account.Property(x => x.Nickname).HasMaxLength(50);
        account.Property(x => x.AccountType).HasMaxLength(30).IsRequired();
        account.Property(x => x.Description).HasMaxLength(200).HasDefaultValue(string.Empty);

        var user = modelBuilder.Entity<User>();
        user.ToTable("Users");
        user.HasKey(x => x.Id);
        user.HasIndex(x => x.Login).IsUnique();
        user.Property(x => x.Login).HasMaxLength(50).IsRequired();
        user.Property(x => x.Password).HasMaxLength(100).IsRequired();

        account.HasData(
            new Account
            {
                Id = 1, AccountNumber = "BY01BANK0001", OwnerName = "Николь кот", Currency = "BYN",
                Balance = 2500m, Nickname = "Основной счёт", AccountType = "Обычный счёт",
                CreatedAt = new DateTime(2026, 1, 1, 9, 0, 0, DateTimeKind.Utc),
                Description = "Счёт для повседневных расходов"
            },
            new Account
            {
                Id = 2, AccountNumber = "BY01BANK0002", OwnerName = "Анна Бронская", Currency = "BYN",
                Balance = 8200m, Nickname = "Накопления", AccountType = "Сберегательный",
                CreatedAt = new DateTime(2026, 1, 2, 9, 0, 0, DateTimeKind.Utc),
                Description = "Сбережения и накопления"
            },
            new Account
            {
                Id = 3, AccountNumber = "BY01BANK0003", OwnerName = "Иван Шкред", Currency = "BYN",
                Balance = 1100m, Nickname = "Кредитная карта", AccountType = "Кредитный",
                CreatedAt = new DateTime(2026, 1, 3, 9, 0, 0, DateTimeKind.Utc),
                Description = "Кредитный счёт"
            },
            new Account
            {
                Id = 4, AccountNumber = "BY01BANK0004", OwnerName = "ОАО мм ОО", Currency = "BYN",
                Balance = 21000m, Nickname = "Расчётный счёт", AccountType = "Бизнес-счёт",
                CreatedAt = new DateTime(2026, 1, 4, 9, 0, 0, DateTimeKind.Utc),
                Description = "Расчёты организации"
            },
            new Account
            {
                Id = 5, AccountNumber = "BY01BANK0005", OwnerName = "Дана Сан", Currency = "USD",
                Balance = 3600m, Nickname = "На карзину в ЗЯ", AccountType = "Сберегательный",
                CreatedAt = new DateTime(2026, 1, 5, 9, 0, 0, DateTimeKind.Utc),
                Description = "Накопления в долларах"
            },
            new Account
            {
                Id = 6, AccountNumber = "BY01BANK0006", OwnerName = "Николь Кот", Currency = "USD",
                Balance = 750m, Nickname = "Планируемые расходы", AccountType = "Кредитный",
                CreatedAt = new DateTime(2026, 1, 6, 9, 0, 0, DateTimeKind.Utc),
                Description = "Планируемые расходы"
            },
            new Account
            {
                Id = 7, AccountNumber = "BY01BANK0007", OwnerName = "Нюша", Currency = "EUR",
                Balance = 900m, Nickname = "Евросчёт", AccountType = "Обычный счёт",
                CreatedAt = new DateTime(2026, 1, 7, 9, 0, 0, DateTimeKind.Utc),
                Description = "Счёт в евро"
            });
    }
}
