#  

## Other Notes

### Dependency Injection

- Transient — Objects are different. One new instance for every controller and every service
- Scoped — Objects are same through the request
- Singleton — Objects are the same for all requests for the entire application lifetime

### Dotnet Core Basic Commands

Create Solution

```powershell
dotnet new solution
```

Create Projects

```powershell
dotnet new webapi -o PrjName.Api
dotnet new classlib -o PrjName.Core
dotnet new classlib -o PrjName.Services
dotnet new classlib -o PrjName.Data
```

Add Projects to Solution

```powershell
dotnet sln add PrjName.Api/PrjName.Api.csproj PrjName.Core/PrjName.Core.csproj PrjName.Data/PrjName.Data.csproj PrjName.Services/PrjName.Services.csproj
```

Add Dependencies

```powershell
dotnet add PrjName.Api/PrjName.Api.csproj reference PrjName.Core/PrjName.Core.csproj PrjName.Services/PrjName.Services.csproj

dotnet add PrjName.Data/PrjName.Data.csproj reference PrjName.Core/PrjName.Core.csproj

dotnet add PrjName.Services/PrjName.Services.csproj reference PrjName.Core/PrjName.Core.csproj

dotnet add PrjName.Api/PrjName.Api.csproj reference PrjName.Services/PrjName.Services.csproj PrjName.Core/PrjName.Core.csproj PrjName.Data/PrjName.Data.csproj

dotnet add PrjName.Api/PrjName.Api.csproj package AutoMapper
dotnet add PrjName.Api/PrjName.Api.csproj package AutoMapper.Extensions.Microsoft.DependencyInjection
```

### EF Core Commands

Install EF globally

```powershell
dotnet tool install --global dotnet-ef
```

Add EF Packages to Projects

```powershell
dotnet add PrjName.Data/PrjName.Data.csproj package Microsoft.EntityFrameworkCore

dotnet add PrjName.Data/PrjName.Data.csproj package Microsoft.EntityFrameworkCore.Design

dotnet add PrjName.Data/PrjName.Data.csproj package  Microsoft.EntityFrameworkCore.SqlServer
```

Add Migrations

```powershell
dotnet ef --startup-project Prj.Api/Prj.Api.csproj migrations add InitialModel -p Prj.Data/Prj.Data.csproj
```

```powershell
dotnet ef --startup-project Prj.Api/Prj.Api.csproj database update
```

```powershell
dotnet ef --startup-project Prj.Api/Prj.Api.csproj migrations add SeedMusicsAndArtistsTable -p Prj.Data/Prj.Data.csproj
```

Seed Data

```csharp
  protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("INSERT INTO TableName (Name) Values ('Value Col1')");
```

Run App

```powershell
dotnet run -p PrjName.Api/PrjName.Api.csproj
```