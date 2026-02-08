# 1. Use the SDK for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 2. Correct the path to your .csproj file
# If your file is inside a folder, include the folder name:
COPY ["MyProjectFolder/MyProject.csproj", "MyProjectFolder/"]

RUN dotnet restore "MyProjectFolder/MyProject.csproj"

# 3. Copy everything else and publish
COPY . .
WORKDIR "/src/MyProjectFolder"
RUN dotnet publish -c Release -o /app

# 4. Final runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "MyProject.dll"]
