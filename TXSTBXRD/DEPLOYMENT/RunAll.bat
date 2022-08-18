cd ..

dotnet build

start /d "."  dotnet run --project .\TXTBXRD-SERVICES\IDENTITY-SERVICE\IDENTITY-SERVICE.csproj
start /d "."  dotnet run --project .\TXSTBXRD-UI\TXSTBXRD-UI.csproj