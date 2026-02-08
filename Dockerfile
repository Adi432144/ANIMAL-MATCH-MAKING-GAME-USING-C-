# 1. Use the SDK image to build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 2. Copy the csproj from the root (where it actually is!)
COPY ["AnimalMatchingGame.csproj", "./"]
RUN dotnet restore "AnimalMatchingGame.csproj"

# 3. Copy everything else and publish
COPY . .
RUN dotnet publish "AnimalMatchingGame.csproj" -c Release -o /app

# 4. Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

# Ensure the DLL name matches your project output
ENTRYPOINT ["dotnet", "AnimalMatchingGame.dll"]
