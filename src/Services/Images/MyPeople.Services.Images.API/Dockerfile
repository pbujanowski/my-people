#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5020

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Images/MyPeople.Services.Images.API/MyPeople.Services.Images.API.csproj", "src/Services/Images/MyPeople.Services.Images.API/"]
COPY ["src/Services/Common/MyPeople.Services.Common/MyPeople.Services.Common.csproj", "src/Services/Common/MyPeople.Services.Common/"]
COPY ["src/Common/MyPeople.Common.Configuration/MyPeople.Common.Configuration.csproj", "src/Common/MyPeople.Common.Configuration/"]
COPY ["src/Services/Images/MyPeople.Services.Images.Application/MyPeople.Services.Images.Application.csproj", "src/Services/Images/MyPeople.Services.Images.Application/"]
COPY ["src/Services/Images/MyPeople.Services.Images.Domain/MyPeople.Services.Images.Domain.csproj", "src/Services/Images/MyPeople.Services.Images.Domain/"]
COPY ["src/Services/Images/MyPeople.Services.Images.Infrastructure/MyPeople.Services.Images.Infrastructure.csproj", "src/Services/Images/MyPeople.Services.Images.Infrastructure/"]
COPY ["src/Services/Images/MyPeople.Services.Images.Infrastructure.Migrations.Sqlite/MyPeople.Services.Images.Infrastructure.Migrations.Sqlite.csproj", "src/Services/Images/MyPeople.Services.Images.Infrastructure.Migrations.Sqlite/"]
COPY ["src/Services/Images/MyPeople.Services.Images.Infrastructure.Migrations.SqlServer/MyPeople.Services.Images.Infrastructure.Migrations.SqlServer.csproj", "src/Services/Images/MyPeople.Services.Images.Infrastructure.Migrations.SqlServer/"]
RUN dotnet restore "./src/Services/Images/MyPeople.Services.Images.API/./MyPeople.Services.Images.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Images/MyPeople.Services.Images.API"
RUN dotnet build "./MyPeople.Services.Images.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MyPeople.Services.Images.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyPeople.Services.Images.API.dll"]