# WT-12 — готовый проект с запуском с нуля

**Предметная область:** банковские счета из WT-8. **Технологии:** ASP.NET Core Razor Pages / EF Core 8 / PostgreSQL. Приложение, миграции, SQL-данные, Git-история и вспомогательные файлы уже включены. Проект не надо создавать вручную.

## Что необходимо на Windows

- **.NET 8 SDK** (подходит и установленный у вас SDK 9 для сборки проекта; для запуска проекта `net8.0` нужна среда выполнения ASP.NET Core 8).
- **Docker Desktop**: запустить и дождаться состояния *Engine running*.
- Интернет при первой загрузке пакетов NuGet и Docker-образа PostgreSQL.

Из ранее показанного PowerShell видно, что `dotnet` у вас установлен. Если Docker тоже установлен, повторно устанавливать ничего не требуется.

## Первый запуск — без ручных команд

1. Распаковать архив полностью в удобную папку. Нужна папка `wt-lab12-12auth-validation-shilets` с файлом `.sln`.
2. Запустить Docker Desktop.
3. Дважды щёлкнуть **`01_START_WT12.cmd`** в корне проекта.
4. Скрипт проверит .NET и Docker; запустит PostgreSQL `wt-lab8-postgres`, либо создаст этот контейнер, если его нет; затем выполнит `dotnet build` и запустит приложение.
5. После старта открыть **http://localhost:5290/Login**. Учебный логин **student**, пароль **1234**.
6. **Не закрывать консоль**, пока работает приложение. Для остановки нажать `Ctrl+C` в консоли.

На чистой базе автоматически выполняются три миграции: `InitialCreate`, `AddAccountDescription`, `AddUsers`. Появляются демонстрационные банковские счета и пользователь.

Если контейнер WT-8 уже есть, проект использует прежнюю локальную БД, не удаляя данные.

## Проверка для отчёта

При работающем Docker откройте **`02_CHECK_BUILD_AND_MIGRATIONS.cmd`**. Он последовательно выполняет `dotnet tool restore`, `dotnet build`, `dotnet tool run dotnet-ef database update`, `dotnet tool run dotnet-ef migrations list` — **при успехе в последнем списке должна быть `20261028000100_AddUsers` без `(Pending)`**.

SQL-проверка для рисунка 5 (PowerShell):

```powershell
docker exec -it wt-lab8-postgres psql -U wt8_user -d wt_lab8
```

Затем:

```sql
SELECT "Id", "Login", "Password" FROM "Users" ORDER BY "Id";
```

Выход из `psql`: `\q`.

Остальные скриншоты описаны в `WT12_ПОШАГОВАЯ_ИНСТРУКЦИЯ.txt`.

## Ручной запуск (если нужен PowerShell)

```powershell
$env:ASPNETCORE_ENVIRONMENT = "Development"
Remove-Item Env:DATABASE_URL -ErrorAction SilentlyContinue
docker start wt-lab8-postgres
dotnet tool restore
dotnet build
dotnet tool run dotnet-ef database update
dotnet tool run dotnet-ef migrations list
dotnet run --launch-profile http
```

Если PostgreSQL ещё не существует, сначала запустите `01_START_WT12.cmd`, он создаст контейнер. Тестовая БД использует только локальные учебные реквизиты. **Не размещайте настоящие пароли от облачных БД в GitHub.**

## GitHub

В папке проекта сохранён валидный `.git` с веткой `main` и историей коммитов. При создании нового пустого репозитория на GitHub:

```powershell
git log --oneline
git remote add origin https://github.com/mizantropiya/wt-lab12-12auth-validation-shilets.git
git push -u origin main
```

Не создавайте новый `git init` и не удаляйте `.git`: это уничтожит подготовленную историю. Если `origin` уже существует, используйте `git remote set-url origin <адрес>`.

## Ограничения

Пароль `student / 1234` хранится открыто исключительно для выполнения учебной методички. Для настоящего приложения нужны хеширование/ASP.NET Core Identity. Данная поставка проверена на структуру и целостность, но **фактический запуск в вашей Windows зависит от установленного SDK, Docker и NuGet**, поэтому при непредвиденной ошибке сохраняйте консольный вывод.
