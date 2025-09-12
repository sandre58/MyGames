<div id="top"></div>

<!-- PROJECT INFO -->
<br />
<div align="center">
  <a href="https://github.com/sandre58/MyGames">
    <img src="../../assets/chess.png" width="256" height="256">
  </a>

<h1 align="center">Chess Game</h1>

[![Framework][framework-shield]][framework-url]
[![Version][version-shield]][version-url]
[![License][license-shield]][license-url]

  <p align="center">
    <br />
    A comprehensive chess implementation in C# with .NET 10, featuring both a core library and console application.
    Play against AI opponents or other humans with full chess rule support including castling, en passant, and pawn promotion.
  </p>

</div>

## 🎯 Features

- **Complete Chess Implementation**: Full implementation of chess rules including special moves
- **AI Opponents**: Multiple AI strategies with different difficulty levels
- **Console Interface**: Clean and intuitive console-based gameplay
- **Extensible Architecture**: Well-structured codebase for easy extension and modification
- **Unit Tested**: Comprehensive test coverage for reliable gameplay

## 🏗️ Architecture

The Chess project is organized into two main components:

### MyGames.Chess (Core Library)
The core chess engine containing all game logic and rules:

- **Game Logic**: Complete chess game implementation with turn management
- **Board Representation**: Efficient chess board with piece tracking
- **Move Validation**: Full move validation including check/checkmate detection
- **Special Moves**: Support for castling, en passant, and pawn promotion
- **AI Strategies**: Alpha-beta pruning algorithm with configurable difficulty

### MyGames.Chess.Console (Console Application)
A console-based interface for playing chess:

- **Interactive Gameplay**: Clean console interface with move input
- **Player Types**: Human players, AI opponents, and random players
- **Game Visualization**: ASCII board representation
- **Move History**: Track and display game progression

## 🚀 Getting Started

### Prerequisites

- .NET 10.0 or later
- Visual Studio 2022 or compatible IDE

### Installation

1. Clone the repository:
```bash
git clone https://github.com/sandre58/MyGames.git
```

2. Navigate to the Chess directory:
```bash
cd MyGames/src/Chess
```

3. Build the solution:
```bash
dotnet build
```

4. Run the console application:
```bash
dotnet run --project MyGames.Chess.Console
```

## 🎮 How to Play

### Console Application

The console application provides several player types:

1. **Human Player**: Interactive console input for moves
2. **AI Player**: Computer opponent with configurable difficulty
3. **Random Player**: Makes random legal moves (for testing)

### Move Input Format

Moves are entered using standard algebraic notation:
- `e2e4` - Move pawn from e2 to e4
- `Ng1f3` - Move knight from g1 to f3
- `O-O` - Kingside castling
- `O-O-O` - Queenside castling

## 🧩 Core Components

### Chess Pieces
- **Pawn**: Basic piece with en passant and promotion support
- **Rook**: Straight-line movement with castling support
- **Knight**: L-shaped movement pattern
- **Bishop**: Diagonal movement
- **Queen**: Combined rook and bishop movement
- **King**: Single square movement with castling support

### Special Moves
- **Castling**: Both kingside and queenside castling
- **En Passant**: Capture of pawns that moved two squares
- **Pawn Promotion**: Promotion to queen, rook, bishop, or knight

### AI Strategy
- **Alpha-Beta Pruning**: Efficient minimax algorithm
- **Board Evaluation**: Sophisticated position evaluation
- **Difficulty Levels**: Configurable search depth

## 🧪 Testing

The project includes comprehensive unit tests covering:

- Board state management
- Move validation and generation
- Special move implementations
- Game state detection (check, checkmate, stalemate)
- AI strategy behavior

Run tests using:
```bash
dotnet test ../../tests/MyGames.Chess.UnitTests/
```

## 📁 Project Structure

```
Chess/
├── MyGames.Chess/                 # Core chess library
│   ├── Pieces/                    # Chess piece implementations
│   ├── Moves/                     # Move types and validation
│   ├── Strategies/                # AI strategies
│   ├── Factories/                 # Object creation factories
│   ├── Extensions/                # Extension methods
│   └── Exceptions/                # Custom exceptions
├── MyGames.Chess.Console/         # Console application
│   ├── Players/                   # Player implementations
│   └── ConsoleHelper.cs           # Console utilities
└── README.md                      # This file
```

## 🔧 Configuration

### AI Difficulty Levels

The AI can be configured with different difficulty levels:

- **Level 1**: Depth 1-2 (Beginner)
- **Level 2**: Depth 3-4 (Intermediate)  
- **Level 3**: Depth 5-6 (Advanced)
- **Level 4**: Depth 7+ (Expert)

### Customization

The chess engine supports various customization options:
- Board evaluation strategies
- AI search algorithms
- Player implementations
- Move notation formats

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.

## 📝 Changelog

See [CHANGELOG.md](CHANGELOG.md) for a list of changes and version history.

## 📄 License

Copyright © Stéphane ANDRE.

This project is licensed under the MIT License - see the [LICENSE](../../LICENSE) file for details.

## 🙏 Acknowledgments

- Classic chess rules and gameplay mechanics
- Alpha-beta pruning algorithm implementation
- Console interface design patterns

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- MARKDOWN LINKS & IMAGES -->
[framework-shield]: https://img.shields.io/badge/.NET-10.0-purple
[framework-url]: https://dotnet.microsoft.com/download/dotnet/10.0
[version-shield]: https://img.shields.io/badge/version-1.0.0-blue
[version-url]: https://github.com/sandre58/MyGames/releases
[license-shield]: https://img.shields.io/github/license/sandre58/MyGames?style=flat-square
[license-url]: https://github.com/sandre58/MyGames/blob/main/LICENSE