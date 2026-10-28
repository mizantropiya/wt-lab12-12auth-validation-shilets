# WT-12 - Authorization and validation in Razor Pages

Учебное приложение ASP.NET Core Razor Pages на основе WT-8.

## Что реализовано
- PostgreSQL + Entity Framework Core;
- модель `User` и миграция `AddUsers`;
- учебная запись `student / 1234`;
- вход через `/Login`;
- Session (`UserLogin`);
- защищённая страница `/Profile`;
- выход пользователя;
- серверная валидация `[Required]`, `[StringLength]` и `ModelState.IsValid`;
- CRUD банковских счетов из WT-8 сохранён.

> Пароль хранится открытым текстом только потому, что это прямо предусмотрено учебным заданием WT-12. Для реальных приложений нужен ASP.NET Core Identity и безопасное хеширование.

## Локальный запуск
WT-12 использует ту же локальную PostgreSQL, что и WT-8:
`Host=localhost;Port=5433;Database=wt_lab8;Username=wt8_user;Password=wt8_pass`.

Если контейнер WT-8 остановлен:
```powershell
docker start wt-lab8-postgres
```

Затем:
```powershell
dotnet tool restore
dotnet tool run dotnet-ef database update
dotnet run
```

## Учётная запись
- Login: `student`
- Password: `1234`
