using Microsoft.EntityFrameworkCore;
using WtLab12Banking.AuthValidation.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

if (!string.IsNullOrWhiteSpace(databaseUrl))
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':', 2);

    if (userInfo.Length != 2)
        throw new InvalidOperationException("Некорректная переменная DATABASE_URL.");

    connectionString =
        $"Host={uri.Host};" +
        $"Port={(uri.Port > 0 ? uri.Port : 5432)};" +
        $"Database={Uri.UnescapeDataString(uri.AbsolutePath.TrimStart('/'))};" +
        $"Username={Uri.UnescapeDataString(userInfo[0])};" +
        $"Password={Uri.UnescapeDataString(userInfo[1])};" +
        "SSL Mode=Disable";
}

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Не задана строка подключения. Для локального запуска используйте appsettings.Development.json, " +
        "а для размещения задайте DATABASE_URL.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString, npgsql =>
        npgsql.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(3),
            errorCodesToAdd: null)));

var app = builder.Build();

// Миграции применяются автоматически при запуске.
// Небольшой retry делает локальный запуск устойчивым, если PostgreSQL только что стартовала.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DatabaseStartup");

    const int maxAttempts = 10;
    for (var attempt = 1; attempt <= maxAttempts; attempt++)
    {
        try
        {
            db.Database.Migrate();
            logger.LogInformation("PostgreSQL подключена, миграции применены.");
            break;
        }
        catch (Exception ex) when (attempt < maxAttempts)
        {
            logger.LogWarning(ex,
                "PostgreSQL пока недоступна. Повтор {Attempt}/{MaxAttempts} через 2 секунды...",
                attempt, maxAttempts);
            await Task.Delay(TimeSpan.FromSeconds(2));
        }
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.MapRazorPages();

var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrWhiteSpace(port))
    app.Run($"http://0.0.0.0:{port}");
else
    app.Run();
