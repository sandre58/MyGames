<div id="top"></div>

<!-- PROJECT INFO -->
<br />
<div align="center">
  <a href="https://github.com/sandre58/MyGames">
    <img src="assets/logo.png" width="256" height="256">
  </a>

<h1 align="center">My Games</h1>

[![Downloads][downloads-shield]][downloads-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]

  <p align="center">
    <br />
    A comprehensive collection of classic board games implemented in C# with .NET 10. 
    Features both console applications and rich WPF desktop interfaces with AI opponents, 
    customizable gameplay, and modern user experiences.
  </p>

[![Language][language-shield]][language-url]
[![Build][build-shield]][build-url]

</div>

## 🎯 Overview

My Games is a modern implementation of classic board games built with .NET 10, offering multiple ways to play your favorite games. Each game includes:

- **Multiple Interfaces**: Console applications for quick play and WPF desktop apps for rich visual experiences
- **AI Opponents**: Sophisticated artificial intelligence with configurable difficulty levels
- **Customizable Gameplay**: Adjustable rules, board sizes, and game settings
- **Clean Architecture**: Well-structured, testable code with comprehensive unit test coverage
- **Cross-Platform Core**: Game logic built for portability and extensibility

## ✨ Key Features

- **🎮 Multiple Game Modes**: Play against AI opponents or challenge friends in local multiplayer
- **🖥️ Dual Interfaces**: Choose between lightweight console apps or feature-rich WPF desktop applications
- **🤖 Advanced AI**: Alpha-beta pruning algorithms with multiple difficulty levels for challenging gameplay
- **⚙️ Highly Customizable**: Adjust board sizes, rules, difficulty settings, and visual themes
- **🏗️ Modern Architecture**: Clean, testable code structure with comprehensive unit test coverage
- **🎨 Rich UI**: Beautiful WPF interfaces with animations, themes, and intuitive drag-and-drop controls

## 🎲 Available Games

### ♟️ Chess

<div align="center">
<img src="assets/chess.png" width="100" height="100">
<br />

[![Framework][framework-shield]][framework-url]
[![Version][chess-version-shield]][chess-version-url]

**[📖 View Chess Documentation](src/Chess/README.md)**

</div>

A complete chess implementation featuring all standard rules including castling, en passant, and pawn promotion. Play against sophisticated AI opponents or challenge friends with both console and future WPF interfaces.

**Features:**
- Complete chess rule implementation
- AI with Alpha-beta pruning algorithm  
- Console interface with algebraic notation
- Special moves: castling, en passant, promotion
- Comprehensive unit test coverage

### 🔴 Connect 4

<div align="center">
<img src="assets/connect4.png" width="100" height="100">
<br />

[![Framework][framework-shield]][framework-url]
[![Version][connect4-version-shield]][connect4-version-url]

**[📖 View Connect4 Documentation](src/Connect4/README.md)**

</div>

The classic Connect 4 game with both console and beautiful WPF interfaces. Customize board dimensions, winning conditions, and enjoy smooth gameplay with multiple AI difficulty levels.

**Features:**
- Console and WPF desktop applications
- Customizable board sizes (4x4 to 12x10)
- Configurable win conditions (3-6 in a row)
- Drag-and-drop WPF interface with animations
- Theme support and visual customization

## 🚀 Quick Start

### Prerequisites

- **.NET 10.0** or later
- **Visual Studio 2022** or compatible IDE
- **Windows OS** (for WPF applications)

### Installation

1. **Clone the repository:**
```bash
git clone https://github.com/sandre58/MyGames.git
cd MyGames
```

2. **Build the solution:**
```bash
dotnet build
```

3. **Run a game:**

**Chess Console:** 
```bash
dotnet run --project src/Chess/MyGames.Chess.Console
```

**Connect4 Console:**
```bash
dotnet run --project src/Connect4/MyGames.Connect4.Console
```

**Connect4 WPF:**
```bash
dotnet run --project src/Connect4/MyGames.Connect4.Wpf
```

## 🏗️ Project Structure

```
MyGames/
├── src/
│   ├── Chess/                     # Chess game implementation
│   │   ├── MyGames.Chess/         # Core chess library
│   │   ├── MyGames.Chess.Console/ # Console application
│   │   └── README.md              # Chess documentation
│   ├── Connect4/                  # Connect4 game implementation
│   │   ├── MyGames.Connect4/      # Core Connect4 library  
│   │   ├── MyGames.Connect4.Console/ # Console application
│   │   ├── MyGames.Connect4.Wpf/  # WPF desktop application
│   │   └── README.md              # Connect4 documentation
│   └── Common/
│       └── MyGames.Core/          # Shared game framework
├── tests/                         # Unit tests for all projects
├── assets/                        # Game icons and images
└── build/                         # Build configuration files
```

## 🧪 Testing

Run all tests across the solution:
```bash
dotnet test
```

Run tests for a specific game:
```bash
dotnet test tests/MyGames.Chess.UnitTests/
dotnet test tests/MyGames.Connect4.UnitTests/
```

## 🎮 Game Features Comparison

| Feature | Chess | Connect4 |
|---------|-------|----------|
| Console Interface | ✅ | ✅ |
| WPF Desktop App | 🔄 (Planned) | ✅ |
| AI Opponents | ✅ | ✅ |
| Customizable Rules | ✅ | ✅ |
| Board Customization | ❌ | ✅ |
| Themes/Visual Options | ❌ | ✅ |
| Drag & Drop UI | ❌ | ✅ |

## 🤖 AI Implementation

Both games feature sophisticated AI opponents powered by:

- **Alpha-Beta Pruning**: Efficient minimax algorithm implementation
- **Position Evaluation**: Strategic position assessment functions  
- **Configurable Difficulty**: Adjustable search depth from beginner to expert
- **Move Ordering**: Optimization techniques for better performance

## 🛠️ Development

### Building from Source

1. **Prerequisites**: Ensure you have .NET 10.0 SDK installed
2. **Clone**: `git clone https://github.com/sandre58/MyGames.git`
3. **Build**: `dotnet build` in the root directory
4. **Test**: `dotnet test` to run all unit tests

### Contributing

Contributions are welcome! Please see our [Contributing Guidelines](CONTRIBUTING.md) for details on:

- Code style and standards
- Adding new games or features
- Improving AI algorithms
- UI/UX enhancements
- Testing requirements

### Architecture

The project follows a clean architecture pattern:

- **Core Libraries**: Game logic and rules (platform-independent)
- **Console Applications**: Lightweight interfaces for quick gameplay
- **WPF Applications**: Rich desktop experiences with themes and animations
- **Shared Framework**: Common game abstractions and utilities

## 🗺️ Roadmap

### Near Term
- [ ] Chess WPF application with rich UI
- [ ] Online multiplayer support
- [ ] Game replay and analysis features
- [ ] Additional AI difficulty levels

### Future
- [ ] Mobile applications (MAUI)
- [ ] Additional classic games (Checkers, Reversi)
- [ ] Tournament and rating systems
- [ ] Advanced AI with machine learning

## 📊 Statistics

- **Languages**: C#, XAML
- **Framework**: .NET 10
- **Architecture**: Clean Architecture with MVVM (WPF)
- **Testing**: Unit tests with high coverage
- **Games**: 2 implemented, more planned

## 🤝 Contributing

We welcome contributions of all kinds:

- 🐛 **Bug Reports**: Found an issue? Let us know!
- 💡 **Feature Ideas**: Have suggestions for improvements?
- 🔧 **Code Contributions**: Submit pull requests for fixes or features
- 📝 **Documentation**: Help improve our documentation
- 🎨 **UI/UX**: Design improvements and theme contributions

## 📄 License

Copyright © Stéphane ANDRE.

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- Classic game rules and mechanics
- Algorithm implementations for AI opponents
- .NET community for frameworks and tools
- Contributors and testers

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[language-shield]: https://img.shields.io/github/languages/top/sandre58/MyGames
[language-url]: https://github.com/sandre58/MyGames
[forks-shield]: https://img.shields.io/github/forks/sandre58/MyGames?style=for-the-badge
[forks-url]: https://github.com/sandre58/MyGames/network/members
[stars-shield]: https://img.shields.io/github/stars/sandre58/MyGames?style=for-the-badge
[stars-url]: https://github.com/sandre58/MyGames/stargazers
[issues-shield]: https://img.shields.io/github/issues/sandre58/MyGames?style=for-the-badge
[issues-url]: https://github.com/sandre58/MyGames/issues
[license-shield]: https://img.shields.io/github/license/sandre58/MyGames?style=for-the-badge
[license-url]: https://github.com/sandre58/MyGames/blob/main/LICENSE
[build-shield]: https://img.shields.io/github/actions/workflow/status/sandre58/MyGames/ci.yml?logo=github&label=CI
[build-url]: https://github.com/sandre58/MyGames/actions
[downloads-shield]: https://img.shields.io/github/downloads/sandre58/MyGames/total?style=for-the-badge
[downloads-url]: https://github.com/sandre58/MyGames/releases
[framework-shield]: https://img.shields.io/badge/.NET-10.0-purple
[framework-url]: https://dotnet.microsoft.com/download/dotnet/10.0
[connect4-version-shield]: https://img.shields.io/badge/version-1.0.0-blue
[connect4-version-url]: https://github.com/sandre58/MyGames/releases
[chess-version-shield]: https://img.shields.io/badge/version-1.0.0-blue
[chess-version-url]: https://github.com/sandre58/MyGames/releases