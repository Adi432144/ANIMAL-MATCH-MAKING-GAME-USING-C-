# This tells dotnet to only focus on the Android build and ignore the others
RUN dotnet publish "AnimalMatchingGame.csproj" -f net8.0-android -c Release -o /app
