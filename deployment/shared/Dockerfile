# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/ src/

RUN dotnet restore src/RequestFlow/RequestFlow.csproj
RUN dotnet build src/RequestFlow/RequestFlow.csproj -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish src/RequestFlow/RequestFlow.csproj -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RequestFlow.dll"]
