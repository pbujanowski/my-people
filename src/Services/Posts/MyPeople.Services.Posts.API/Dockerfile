#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5010

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Posts/MyPeople.Services.Posts.API/MyPeople.Services.Posts.API.csproj", "src/Services/Posts/MyPeople.Services.Posts.API/"]
COPY ["src/Services/Common/MyPeople.Services.Common/MyPeople.Services.Common.csproj", "src/Services/Common/MyPeople.Services.Common/"]
COPY ["src/Common/MyPeople.Common.Configuration/MyPeople.Common.Configuration.csproj", "src/Common/MyPeople.Common.Configuration/"]
COPY ["src/Services/Posts/MyPeople.Services.Posts.Application/MyPeople.Services.Posts.Application.csproj", "src/Services/Posts/MyPeople.Services.Posts.Application/"]
COPY ["src/Common/MyPeople.Common.Models/MyPeople.Common.Models.csproj", "src/Common/MyPeople.Common.Models/"]
COPY ["src/Services/Posts/MyPeople.Services.Posts.Domain/MyPeople.Services.Posts.Domain.csproj", "src/Services/Posts/MyPeople.Services.Posts.Domain/"]
COPY ["src/Services/Posts/MyPeople.Services.Posts.Infrastructure/MyPeople.Services.Posts.Infrastructure.csproj", "src/Services/Posts/MyPeople.Services.Posts.Infrastructure/"]
COPY ["src/Services/Posts/MyPeople.Services.Posts.Infrastructure.Migrations.Sqlite/MyPeople.Services.Posts.Infrastructure.Migrations.Sqlite.csproj", "src/Services/Posts/MyPeople.Services.Posts.Infrastructure.Migrations.Sqlite/"]
COPY ["src/Services/Posts/MyPeople.Services.Posts.Infrastructure.Migrations.SqlServer/MyPeople.Services.Posts.Infrastructure.Migrations.SqlServer.csproj", "src/Services/Posts/MyPeople.Services.Posts.Infrastructure.Migrations.SqlServer/"]
COPY ["src/Common/MyPeople.Common.Abstractions/MyPeople.Common.Abstractions.csproj", "src/Common/MyPeople.Common.Abstractions/"]
RUN dotnet restore "./src/Services/Posts/MyPeople.Services.Posts.API/./MyPeople.Services.Posts.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Posts/MyPeople.Services.Posts.API"
RUN dotnet build "./MyPeople.Services.Posts.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MyPeople.Services.Posts.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyPeople.Services.Posts.API.dll"]