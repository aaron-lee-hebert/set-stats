# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

SetStats is a .NET 9.0 web application for tracking lifting sessions and workout progress. The application uses ASP.NET Core Identity for authentication, Entity Framework Core with PostgreSQL for data persistence, and follows a clean architecture pattern with three main projects.

## Architecture

### Project Structure

The solution follows a layered architecture:

- **SetStats.Core**: Domain entities and repository interfaces. Contains no dependencies on infrastructure concerns.
- **SetStats.Data**: Data access layer implementing repositories and EF Core configurations. Depends on SetStats.Core.
- **SetStats.Web**: ASP.NET Core MVC web application. Depends on both SetStats.Data and SetStats.Core.

### Domain Model Hierarchy

The domain centers around tracking training cycles and workout sessions:

1. **User** (ASP.NET Core Identity) - Extended with FirstName, LastName, and timestamp properties
2. **TrainingProgram** - A named training plan owned by a user (e.g., "5/3/1", "Linear Progression")
3. **TrainingCycle** - A specific instance/run of a training program with start/end dates and rounding factor
4. **WorkoutWeek** - A week within a training cycle (1-4), can be Standard or Deload
5. **WorkoutDay** - A specific workout session with a date and status (Planned/Completed/Skipped)
6. **ProgrammedSet** - A planned set within a workout day (percentage of training max, target reps, set order)
7. **CompletedSet** - The actual execution of a programmed set (actual weight, reps, RPE, notes)

Additional entities:
- **ExerciseType** - Categorizes exercises (Main/Assistance)
- **UserMax** - Tracks user's one-rep max for exercises
- **ProgressRecord** - Records progress over time
- **TrainingProgramCycle** - Links training programs to cycles

### Repository Pattern

The codebase uses the repository pattern with a generic `IRepository<T>` base interface providing standard CRUD operations. Specialized repositories extend this for entity-specific queries. All repositories are registered with dependency injection in Program.cs.

### Database Conventions

The ApplicationDbContext applies PostgreSQL naming conventions:
- All table names are lowercase
- All column names use snake_case
- Custom `ToSnakeCase()` method converts C# PascalCase to database snake_case

## Development Commands

### Build and Run

```bash
# Build the solution
dotnet build

# Run the web application
dotnet run --project SetStats.Web

# Build in Release mode
dotnet build -c Release
```

### Database Migrations

```bash
# Add a new migration (run from solution root)
dotnet ef migrations add <MigrationName> --project SetStats.Data --startup-project SetStats.Web

# Update database to latest migration
dotnet ef database update --project SetStats.Data --startup-project SetStats.Web

# Remove last migration (if not applied)
dotnet ef migrations remove --project SetStats.Data --startup-project SetStats.Web

# View migration SQL without applying
dotnet ef migrations script --project SetStats.Data --startup-project SetStats.Web
```

### Code Quality

The project uses strict code analysis settings:
- TreatWarningsAsErrors: true
- AnalysisMode: All
- SonarAnalyzer.CSharp for additional static analysis

```bash
# Build with full analysis (warnings treated as errors)
dotnet build

# Check for code style violations
dotnet format --verify-no-changes
```

## Configuration

### Connection Strings

The application expects a PostgreSQL connection string named "DefaultConnection" in configuration. For local development, use User Secrets:

```bash
# Set connection string via user secrets
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=setstats;Username=your_user;Password=your_password" --project SetStats.Web
```

The UserSecretsId is configured in SetStats.Web.csproj.

## Code Standards

### C# Style Guidelines

- **Namespace declarations**: File-scoped namespaces (enforced as error)
- **Expression-bodied members**: Required for methods, properties, constructors, etc. (enforced as error)
- **Braces**: Required even for single-line blocks (enforced as error)
- **Nullable reference types**: Enabled project-wide
- **ImplicitUsings**: Enabled

### Entity Framework Patterns

When creating new entities:
1. Add entity class to SetStats.Core/Entities
2. Create entity configuration in SetStats.Data/Configurations implementing `IEntityTypeConfiguration<T>`
3. Add repository interface to SetStats.Core/Interfaces/Repositories if needed
4. Add repository implementation to SetStats.Data/Repositories if needed
5. Register repository in Program.cs dependency injection
6. Create migration: `dotnet ef migrations add CreateXEntity --project SetStats.Data --startup-project SetStats.Web`

Entity configurations will automatically be discovered via `builder.ApplyConfigurationsFromAssembly()` in ApplicationDbContext.

### Dependency Management

The solution uses Central Package Management via Directory.Packages.props. All package versions are defined centrally, and individual projects reference packages without version numbers.

## Important Notes

- The solution uses .NET 9.0
- Database provider is PostgreSQL (Npgsql.EntityFrameworkCore.PostgreSQL)
- ASP.NET Core Identity uses Guid as the primary key type
- Migration files are marked as generated code in .editorconfig and excluded from analysis
