# 🗺️ FantasyLandWalk

**Track your real-world steps through fantasy worlds.**

FantasyLandWalk is a cross-platform .NET MAUI app that transforms your daily walks into epic fantasy adventures. Every step you take moves your avatar along a journey through beautifully crafted fantasy maps — from cozy burrows to fiery mountain peaks.

> 🤖 **Built publicly with GitHub Copilot CLI & Coding Agent** — this project showcases AI-assisted development from scaffolding to features.

## ✨ Features

### Available Now
- 🗺️ **The Realm Walk** — A 2,860 km journey through 9 waypoints, from The Burrows to The Flame Peak
- 📍 Interactive fantasy map with route overlay and progress tracking
- 📊 Progress card showing current location, distance walked, and next waypoint
- ✅ Waypoint checklist tracking your milestones

### Coming Soon
- 🤖 **AI Journey Narrator** — Personalized story narration powered by AI, using your real walking stats
- 🎧 **Ambient Soundscape** — Immersive audio that changes with each map region
- 🌑 **The Battle Station** — A second map inspired by a galaxy far, far away
- 📸 Photo Journal — Tag photos to your fantasy location
- 🌦️ Live weather effects on the map
- 🏅 Milestone celebrations with story unlocks
- 👣 Step tracking with Apple Health / Health Connect integration

## 🛠️ Tech Stack

| Component | Technology |
|-----------|-----------|
| Framework | .NET 10 / MAUI |
| Architecture | MVVM (CommunityToolkit.Mvvm) |
| Map Rendering | SkiaSharp |
| Local Storage | SQLite (planned) |
| AI Integration | Microsoft.Extensions.AI (planned) |
| Testing | xUnit |
| CI/CD | GitHub Actions |

## 🚀 Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) with MAUI workload
- For iOS: macOS with Xcode
- For Android: Android SDK (API 26+)

### Install MAUI workload
```bash
dotnet workload install maui
```

### Build & Run
```bash
# Clone the repo
git clone https://github.com/Sweekriti91/FantasyLandWalk.git
cd FantasyLandWalk

# Restore and build
dotnet restore
dotnet build

# Run on Android emulator
dotnet build -t:Run -f net10.0-android

# Run on iOS simulator
dotnet build -t:Run -f net10.0-ios

# Run tests
dotnet test
```

## 🗂️ Project Structure

```
FantasyLandWalk/
├── src/
│   └── FantasyLandWalk/           # Main MAUI app
│       ├── Models/                # Data models (Journey, Waypoint, etc.)
│       ├── ViewModels/            # MVVM ViewModels
│       ├── Views/                 # XAML pages
│       ├── Services/              # Business logic & platform services
│       └── Resources/Maps/        # Map image assets
├── tests/
│   └── FantasyLandWalk.Tests/     # xUnit tests
├── .github/
│   ├── workflows/                 # CI/CD pipelines
│   ├── ISSUE_TEMPLATE/            # Issue templates
│   └── copilot-instructions.md    # Copilot agent context
└── FantasyLandWalk.sln
```

## 🌍 The Realm Walk — Waypoints

| # | Waypoint | Distance | Terrain |
|---|----------|----------|---------|
| 1 | The Burrows | 0 km (start) | Rolling green hills |
| 2 | Crossroads Inn | 193 km | Farmland & roads |
| 3 | Storm Ridge | 322 km | Windswept highlands |
| 4 | The Hidden Valley | 483 km | Mountain refuge |
| 5 | The Deep Mines | 744 km | Underground caverns |
| 6 | The Golden Wood | 933 km | Ancient enchanted forest |
| 7 | The Great Statues | 1,094 km | River canyon |
| 8 | The Shadow Lands | 2,173 km | Desolate volcanic plains |
| 9 | The Flame Peak | 2,860 km | The final summit |

## 🤝 Contributing

We welcome contributions! See [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

This project is built publicly — check out the [issues](https://github.com/Sweekriti91/FantasyLandWalk/issues) for things to work on.

## 📄 License

MIT — see [LICENSE](LICENSE) for details.

## 🎤 About

This is a personal demo/conference talk app by [@Sweekriti91](https://github.com/Sweekriti91), built to showcase .NET MAUI capabilities and the GitHub Copilot CLI + Coding Agent workflow.

Inspired by [The Conqueror Events](https://www.theconqueror.events/lotr8/) and [Fantasy Hike](https://forge7.net/fantasyhike/).
