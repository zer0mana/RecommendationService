# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# Копируем .sln и .csproj файлы для всех проектов
COPY *.sln .
COPY RecommendationService/*.csproj ./RecommendationService/
COPY RecommendationService-BLL/*.csproj ./RecommendationService-BLL/
COPY RecommendationService-DAL/*.csproj ./RecommendationService-DAL/
# Добавьте сюда копирование .csproj для других проектов, если они есть

# Восстанавливаем зависимости для всех проектов
RUN dotnet restore

# Копируем весь остальной исходный код
COPY . .

# Публикуем основной проект RecommendationService
WORKDIR /source/RecommendationService
RUN dotnet publish -c Release -o /app

# Этап выполнения
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=build /app .

# Точка входа - запуск приложения
ENTRYPOINT ["dotnet", "RecommendationService.dll"] 