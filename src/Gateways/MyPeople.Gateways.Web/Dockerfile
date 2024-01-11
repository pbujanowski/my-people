#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Gateways/MyPeople.Gateways.Web/MyPeople.Gateways.Web.csproj", "src/Gateways/MyPeople.Gateways.Web/"]
COPY ["src/Common/MyPeople.Common.Configuration/MyPeople.Common.Configuration.csproj", "src/Common/MyPeople.Common.Configuration/"]
RUN dotnet restore "./src/Gateways/MyPeople.Gateways.Web/./MyPeople.Gateways.Web.csproj"
COPY . .
WORKDIR "/src/src/Gateways/MyPeople.Gateways.Web"
RUN dotnet build "./MyPeople.Gateways.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MyPeople.Gateways.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyPeople.Gateways.Web.dll"]