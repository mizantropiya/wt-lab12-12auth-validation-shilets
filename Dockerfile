FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY wt-lab12-12auth-validation-shilets.csproj ./
RUN dotnet restore "wt-lab12-12auth-validation-shilets.csproj"

COPY . ./
RUN dotnet publish "wt-lab12-12auth-validation-shilets.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish ./
ENV ASPNETCORE_URLS=http://0.0.0.0:10000
EXPOSE 10000
ENTRYPOINT ["dotnet", "wt-lab12-12auth-validation-shilets.dll"]
