#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
# COPY ["Aware.Api.csproj", "./"]
# RUN dotnet restore "./Aware.Api.csproj"
COPY . .
RUN dotnet restore "Aware.Api/Aware.Api.csproj"

WORKDIR /src/.

COPY . .
RUN dotnet build "Aware.Api/Aware.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aware.Api/Aware.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aware.Api.dll"]