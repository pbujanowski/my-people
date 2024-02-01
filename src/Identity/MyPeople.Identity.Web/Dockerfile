#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 4000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Identity/MyPeople.Identity.Web/MyPeople.Identity.Web.csproj", "src/Identity/MyPeople.Identity.Web/"]
COPY ["src/Common/MyPeople.Common.Configuration/MyPeople.Common.Configuration.csproj", "src/Common/MyPeople.Common.Configuration/"]
COPY ["src/Identity/MyPeople.Identity.Application/MyPeople.Identity.Application.csproj", "src/Identity/MyPeople.Identity.Application/"]
COPY ["src/Identity/MyPeople.Identity.Infrastructure/MyPeople.Identity.Infrastructure.csproj", "src/Identity/MyPeople.Identity.Infrastructure/"]
COPY ["src/Identity/MyPeople.Identity.Infrastructure.Migrations.Sqlite/MyPeople.Identity.Infrastructure.Migrations.Sqlite.csproj", "src/Identity/MyPeople.Identity.Infrastructure.Migrations.Sqlite/"]
COPY ["src/Identity/MyPeople.Identity.Infrastructure.Migrations.SqlServer/MyPeople.Identity.Infrastructure.Migrations.SqlServer.csproj", "src/Identity/MyPeople.Identity.Infrastructure.Migrations.SqlServer/"]
COPY ["src/Identity/MyPeople.Identity.Domain/MyPeople.Identity.Domain.csproj", "src/Identity/MyPeople.Identity.Domain/"]
RUN dotnet restore "./src/Identity/MyPeople.Identity.Web/./MyPeople.Identity.Web.csproj"
COPY . .
WORKDIR "/src/src/Identity/MyPeople.Identity.Web"
RUN dotnet build "./MyPeople.Identity.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MyPeople.Identity.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyPeople.Identity.Web.dll"]