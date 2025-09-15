<div id="top"></div>

<!-- PROJECT INFO -->
<br />
<div align="center">
  <a href="https://github.com/sandre58/MyGames">
    <img src="../../assets/connect4.png" width="256" height="256">
  </a>

<h1 align="center">Connect 4 Game</h1>

[![Framework][framework-shield]][framework-url]
[![Version][version-shield]][version-url]
[![License][license-shield]][license-url]

  <p align="center">
    <br />
    A complete Connect 4 implementation in C# with .NET 10, featuring a core library, console application, and WPF desktop application.
    Play against AI opponents or other players with customizable board sizes and winning conditions.
  </p>

</div>

## 🎯 Features

- **Multiple Interfaces**: Console and WPF desktop applications
- **Customizable Board**: Adjustable rows, columns, and pieces needed to win
- **AI Opponents**: Alpha-beta pruning algorithm with multiple difficulty levels
- **Interactive Gameplay**: Smooth drag-and-drop in WPF, intuitive console interface
- **Real-time Validation**: Instant move validation and win detection
- **Extensible Architecture**: Clean, well-structured codebase for easy modification

## 🏗️ Architecture

The Connect4 project consists of three main components:

### MyGames.Connect4 (Core Library)
The core game engine containing all Connect 4 logic:

- **Game Logic**: Complete Connect 4 implementation with customizable rules
- **Board Management**: Efficient grid-based board representation
- **Win Detection**: Horizontal, vertical, and diagonal win pattern recognition
- **AI Strategy**: Alpha-beta pruning with configurable difficulty levels
- **Move Validation**: Comprehensive move validation and game state management

### MyGames.Connect4.Console (Console Application)
A console-based interface for playing Connect 4:

- **Text-based Interface**: Clean ASCII art board visualization
- **Player Management**: Human players, AI opponents, and random players
- **Interactive Input**: Column-based move input system
- **Game Statistics**: Move history and game progression tracking

### MyGames.Connect4.Wpf (WPF Desktop Application)
A rich desktop application with modern UI:

- **Interactive GUI**: Drag-and-drop piece placement
- **Visual Feedback**: Animated piece drops and win highlighting
- **Customizable Settings**: Board size, player types, and difficulty configuration
- **Theme Support**: Multiple visual themes and customization options
- **Configuration Management**: Persistent settings and preferences

## 🚀 Getting Started

### Prerequisites

- .NET 10.0 or later
- Visual Studio 2022 or compatible IDE
- Windows OS (for WPF application)

### Installation

1. Clone the repository:
```bash
git clone https://github.com/sandre58/MyGames.git
```

2. Navigate to the Connect4 directory:
```bash
cd MyGames/src/Connect4
```

3. Build the solution:
```bash
dotnet build
```

4. Run your preferred application:

**Console Application:**
```bash
dotnet run --project MyGames.Connect4.Console
```

**WPF Application:**
```bash
dotnet run --project MyGames.Connect4.Wpf
```

## 🎮 How to Play

### Game Rules
Connect 4 is a strategy game where players take turns dropping colored pieces into a vertical grid. The objective is to be the first to form a horizontal, vertical, or diagonal line of four pieces.

### Console Application
- Enter the column number (1-7 for standard board) to drop your piece
- The piece will fall to the lowest available row in that column
- First player to connect four pieces wins!

### WPF Application
- Click on a column or drag pieces to drop them
- Visual indicators show valid moves and winning combinations
- Customize board size and game settings through the configuration menu

## 🎛️ Customization Options

### Board Configuration
- **Rows**: Default 6, customizable from 4 to 10
- **Columns**: Default 7, customizable from 4 to 12  
- **Win Condition**: Default 4 in a row, customizable from 3 to 6

### Player Types
- **Human Player**: Interactive input for real players
- **AI Player**: Computer opponent with multiple difficulty levels
- **Random Player**: Makes random valid moves (useful for testing)

### AI Difficulty Levels
- **Level 1**: Depth 1-2 (Beginner)
- **Level 2**: Depth 3-4 (Intermediate)
- **Level 3**: Depth 5-6 (Advanced)
- **Level 4**: Depth 7+ (Expert)

## 🧩 Core Components

### Connect4Board
- Grid-based board representation
- Efficient piece placement and validation
- Full/empty column detection
- Board state serialization

### Connect4Game
- Turn management and game flow
- Win condition checking (horizontal, vertical, diagonal)
- Move validation and execution
- Game state tracking (in progress, won, draw)

### Connect4Piece
- Player piece representation
- Color and ownership tracking
- Board position management

### AI Strategy
- **Alpha-Beta Pruning**: Efficient minimax algorithm implementation
- **Position Evaluation**: Strategic position assessment
- **Move Ordering**: Optimization for better pruning
- **Configurable Depth**: Adjustable difficulty through search depth

## 🧪 Testing

Comprehensive unit tests cover:

- Board state management and validation
- Move execution and validation
- Win condition detection
- AI strategy behavior and performance
- Game flow and state transitions

Run tests using:
```bash
dotnet test ../../tests/MyGames.Connect4.UnitTests/
```

## 📁 Project Structure

```
Connect4/
├── MyGames.Connect4/              # Core Connect4 library
│   ├── Strategies/                # AI implementation
│   ├── Extensions/                # Extension methods
│   ├── Connect4Board.cs           # Board logic
│   ├── Connect4Game.cs            # Game engine
│   ├── Connect4Move.cs            # Move representation
│   └── Connect4Piece.cs           # Piece logic
├── MyGames.Connect4.Console/      # Console application
│   ├── Players/                   # Player implementations
│   └── Program.cs                 # Console entry point
├── MyGames.Connect4.Wpf/          # WPF desktop application
│   ├── Configuration/             # Settings management
│   ├── Resources/                 # Images and assets
│   ├── Settings/                  # User preferences
│   ├── MainWindow.xaml            # Main UI
│   └── App.xaml                   # Application setup
└── README.md                      # This file
```

## 🔧 Advanced Configuration

### WPF Application Settings
- **Theme Configuration**: Light/Dark themes, custom colors
- **Language Settings**: Multi-language support
- **Game Preferences**: Default board size, AI difficulty
- **Visual Effects**: Animation speed, sound effects

### Performance Optimization
- Configurable AI search depth for performance tuning
- Memory-efficient board representation
- Optimized win detection algorithms
- Cached position evaluations

## 🤝 Contributing

Contributions are welcome! Areas for contribution include:

- New AI strategies and algorithms
- Additional UI themes and customizations
- Performance optimizations
- Extended board configurations
- Multi-language support improvements

Please feel free to submit a Pull Request. For major changes, open an issue first to discuss your ideas.

## 📝 Changelog

See [CHANGELOG.md](CHANGELOG.md) for version history and changes.

## 📄 License

Copyright © Stéphane ANDRE.

This project is licensed under the MIT License - see the [LICENSE](../../LICENSE) file for details.

## 🙏 Acknowledgments

- Classic Connect 4 game mechanics and rules
- Alpha-beta pruning algorithm implementation
- WPF and modern UI design patterns
- Console application design best practices

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- MARKDOWN LINKS & IMAGES -->
[framework-shield]: https://img.shields.io/badge/.NET-10.0-purple
[framework-url]: https://dotnet.microsoft.com/download/dotnet/10.0
[version-shield]: https://img.shields.io/badge/version-1.0.0-blue
[version-url]: https://github.com/sandre58/MyGames/releases
[license-shield]: https://img.shields.io/github/license/sandre58/MyGames?style=flat-square
[license-url]: https://github.com/sandre58/MyGames/blob/main/LICENSE