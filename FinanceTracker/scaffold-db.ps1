$conn = "Server=DESKTOP-ALIM42N;Database=Finances;Trusted_Connection=True;TrustServerCertificate=True;"
$provider = "Microsoft.EntityFrameworkCore.SqlServer"

# 1. Генерируем модуль MasterData (схема md)
Write-Host "Scaffolding MasterData..." -ForegroundColor Cyan
dotnet ef dbcontext scaffold $conn $provider `
    --project ..\MasterData.Data\MasterData.Data.csproj `
    --startup-project ..\MasterData.Data\ `
    --schema md `
    --context MasterDataContext `
    --output-dir DBModels `
    --context-dir DBContext `
    --no-onconfiguring `
    --force # Перезаписывает файлы, если они уже есть

# 2. Генерируем модуль Security (схема sc)
Write-Host "Scaffolding Security..." -ForegroundColor Cyan
dotnet ef dbcontext scaffold $conn $provider `
    --project ..\Security.Data\Security.Data.csproj `
    --startup-project ..\Security.Data\ `
    --schema sc `
    --context SecurityContext `
    --output-dir DBModels `
    --context-dir DBContext `
    --no-onconfiguring `
    --force

# 3. Генерируем модуль FinanceTracker (схема dbo)
Write-Host "Scaffolding FinanceTracker..." -ForegroundColor Cyan
dotnet ef dbcontext scaffold $conn $provider `
    --project ..\FinanceTracker.Data\FinanceTracker.Data.csproj `
    --startup-project ..\FinanceTracker.Data\ `
    --schema dbo `
    --context FinanceTrackerContext `
    --output-dir DBModels `
    --context-dir DBContext `
    --no-onconfiguring `
    --force

Write-Host "Done!" -ForegroundColor Green
