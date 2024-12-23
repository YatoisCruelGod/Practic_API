FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

EXPOSE 5000
ENV ASPNETCORE_URLS http://+:5000
ENV ASPNETCORE_ENVIRONMENT Development

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Practic_API/Practic_API.csproj", "Practic_API/"]
RUN dotnet restore "Practic_API/Practic_API.csproj"

COPY . .
FROM build AS publish
RUN dotnet publish "Practic_API/Practic_API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Practic_API.dll"]