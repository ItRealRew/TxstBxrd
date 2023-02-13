cd ..

dotnet build

start /d "."  dotnet run --project .\TXTBXRD-SERVICES\IDENTITY-SERVICE\IDENTITY-SERVICE.csproj
start /d "."  dotnet run --project .\TXTBXRD-SERVICES\IDENTITY-SERVICE\COMMUNICATIONS-SERVICE.csproj
start /d "."  dotnet run --project .\TXTBXRD-SERVICES\EVENT-SERVICE\EVENT-SERVICE.csproj
start /d "."  dotnet run --project .\TXTBXRD-SERVICES\PERSONALITY-SERVICE\PERSONALITY-SERVICE.csproj
start /d "."  dotnet run --project .\TXSTBXRD-UI\TXSTBXRD-UI.csproj