# Contributing to FantasyLandWalk

Thanks for your interest in contributing! This project is built publicly with GitHub Copilot CLI & Coding Agent, and we welcome community contributions.

## Getting Started

1. Fork the repository
2. Clone your fork: `git clone https://github.com/YOUR_USERNAME/FantasyLandWalk.git`
3. Create a feature branch: `git checkout -b feature/your-feature-name`
4. Make your changes
5. Run tests: `dotnet test`
6. Commit with a clear message
7. Push and open a Pull Request

## Development Setup

- .NET 10 SDK with MAUI workload (`dotnet workload install maui`)
- macOS (for iOS builds) or Windows/Linux (Android only)
- Recommended: Visual Studio 2022+ or VS Code with C# Dev Kit

## Guidelines

### Code Style
- Follow standard C# conventions
- Use MVVM pattern (ViewModels in `ViewModels/`, Views in `Views/`)
- Use `CommunityToolkit.Mvvm` for MVVM helpers
- Keep services behind interfaces for testability

### Naming Convention (Important!)
- **Do NOT use real-world trademarked names** (Tolkien, Star Wars, etc.)
- Use our evocative original names (see README waypoint table)
- When adding new content, create original names that reference but don't copy source material

### Testing
- Add unit tests for new ViewModels and Services
- Ensure `dotnet test` passes before submitting a PR
- Tests live in `tests/FantasyLandWalk.Tests/`

### Commits
- Write clear, descriptive commit messages
- Reference issue numbers when applicable: `Fix #42: Add waypoint description`

## Types of Contributions

- 🐛 **Bug fixes** — Find something broken? Fix it!
- ✨ **Features** — Check the issues for feature requests
- 🗺️ **New maps** — Want to add a new fantasy world? Open a map request issue first
- 📝 **Documentation** — README, code comments, guides
- 🎨 **Art & assets** — Map illustrations, icons, UI improvements
- 🧪 **Tests** — More test coverage is always welcome

## Questions?

Open an issue or start a discussion. We're friendly! 🧙‍♂️
