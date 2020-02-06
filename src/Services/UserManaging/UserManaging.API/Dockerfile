#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/UserManaging/UserManaging.API/UserManaging.API.csproj", "Services/UserManaging/UserManaging.API/"]
COPY ["Services/UserManaging/UserManaging.CQRS/UserManaging.CQRS.csproj", "Services/UserManaging/UserManaging.CQRS/"]
COPY ["Services/UserManaging/UserManaging.Domain/UserManaging.Domain.csproj", "Services/UserManaging/UserManaging.Domain/"]
COPY ["Services/UserManaging/UserManaging.Infrastructure/UserManaging.Infrastructure.csproj", "Services/UserManaging/UserManaging.Infrastructure/"]
RUN dotnet restore "Services/UserManaging/UserManaging.API/UserManaging.API.csproj"
COPY . .
WORKDIR "/src/Services/UserManaging/UserManaging.API"
RUN dotnet build "UserManaging.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserManaging.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManaging.API.dll"]
