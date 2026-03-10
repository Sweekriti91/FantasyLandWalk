# FantasyLandWalk — Copilot Instructions

## Project Context

- **FantasyLandWalk** is a .NET 10 MAUI cross-platform app (Android + iOS)
- It tracks real-world walking steps and maps them onto fantasy world journeys
- Personal demo/conference talk app, not a production store app
- Built publicly to showcase Copilot CLI + Coding Agent workflow

## Architecture

- **MVVM pattern** using CommunityToolkit.Mvvm (`ObservableObject`, `RelayCommand`, source generators)
- .NET MAUI Shell for navigation
- Services behind interfaces, registered in DI container (`MauiProgram.cs`)
- **SkiaSharp** for custom map rendering (not MAUI Maps — these are fantasy maps, not real-world)
- Local storage will use **SQLite-net-pcl**
- AI features will use **Microsoft.Extensions.AI**

## Project Structure

```
src/FantasyLandWalk/          # Main MAUI app
  Models/                      # Data models
  ViewModels/                  # MVVM ViewModels
  Views/                       # XAML pages
  Services/                    # Business logic & platform services
  Resources/Maps/              # Map image assets
tests/FantasyLandWalk.Tests/   # xUnit tests
```

## Naming Conventions

- **CRITICAL:** Do NOT use real-world trademarked names (Tolkien, Star Wars, Lord of the Rings, etc.)
- Use evocative original names that reference but don't copy. Examples:
  - "The Burrows" (not "The Shire"), "The Hidden Valley" (not "Rivendell")
  - "The Flame Peak" (not "Mount Doom"), "The Battle Station" (not "Death Star")
- C# naming: `PascalCase` for public members, `_camelCase` for private fields, `I`-prefix for interfaces

## Code Conventions

- Every ViewModel should inherit from `ObservableObject` (CommunityToolkit.Mvvm)
- Commands use `[RelayCommand]` attribute (source generators)
- Observable properties use `[ObservableProperty]` attribute
- Services: create interface first (`IXxxService`), then implementation (`XxxService`)
- Register services in `MauiProgram.cs` `CreateMauiApp()`
- Default units: **Kilometers**
- Target platforms: Android 8.0+ (API 26) / iOS 16+

## Testing

- Use **xUnit** for unit tests
- Test ViewModels and Services (not Views)
- Mock services using interfaces
- Run tests with: `dotnet test`
- All PRs must pass tests before merge

## CI/CD

- GitHub Actions: `build.yml` (build Android + iOS), `test.yml` (run xUnit tests)
- Build targets: `net10.0-android`, `net10.0-ios`
- Tests run on: `ubuntu-latest` (no platform-specific tests in CI)

## When Making Changes

1. Follow MVVM — UI logic in ViewModels, not code-behind
2. Add/update tests for new ViewModels and Services
3. Ensure `dotnet build` and `dotnet test` pass
4. No trademarked names in any file
5. Keep the demo/conference-friendly vibe — clean, well-commented showcase code
