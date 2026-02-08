
# Must be the very first line
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the project file first
COPY AnimalMatchingGame.csproj .
RUN dotnet workload restore
RUN dotnet restore AnimalMatchingGame.csproj

# Copy the rest
COPY . .
# We must target a specific RID since it's a MAUI app
RUN dotnet publish AnimalMatchingGame.csproj -c Release -f net8.0-android -o /app

# This stage will likely fail on Render because it's not a web app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "AnimalMatchingGame.dll"]
