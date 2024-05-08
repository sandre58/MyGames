// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using MyGames.Connect4.Core;
using MyGames.Common;
using MyNet.Utilities;
using MyNet.Utilities.Generator;
using MyNet.Utilities.Helpers;
using Xunit;

namespace MyGames.Connect4.UnitTests
{
    public class RandomPlayer(string name, byte[]? image = null) : Connect4Player(name, image)
    {
        public override Connect4Move NextMove(Connect4Game game) => new(RandomGenerator.ListItem(game.Board.GetValidColumns().ToList()));
    }

    public class AIPlayer(string name, byte[]? image = null) : Connect4Player(name, image)
    {
        public override Connect4Move NextMove(Connect4Game game)
        {
            var depth = 8; // Profondeur de recherche de l'algorithme
            var alpha = int.MinValue;
            var beta = int.MaxValue;

            var bestColumn = -1;
            var bestScore = int.MinValue;

            for (var column = 0; column < game.Board.NumberOfColumns; column++)
            {
                var clone = game.Clone().CastIn<Connect4Game>();
                if (!clone.Board.IsFull(column))
                {
                    clone.MakeMove(column);
                    var score = AlphaBeta(clone, depth - 1, alpha, beta, false, this);

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestColumn = column;
                    }
                }
            }

            return new Connect4Move(bestColumn);
        }

        private static int AlphaBeta(Connect4Game game, int depth, int alpha, int beta, bool myMove, Connect4Player myPlayer)
        {
            if (depth == 0 || game.IsOver) return EvaluatePosition(game, myPlayer);

            if (myMove)
            {
                var maxScore = int.MinValue;

                for (var column = 0; column < game.Board.NumberOfColumns; column++)
                {
                    var clone = game.Clone().CastIn<Connect4Game>();
                    if (!clone.Board.IsFull(column))
                    {
                        clone.MakeMove(column);
                        var score = AlphaBeta(clone, depth - 1, alpha, beta, false, myPlayer);
                        maxScore = Math.Max(maxScore, score);
                        alpha = Math.Max(alpha, score);

                        if (alpha >= beta)
                            break;
                    }
                }

                return maxScore;
            }
            else
            {
                var minScore = int.MaxValue;

                for (var column = 0; column < game.Board.NumberOfColumns; column++)
                {
                    var clone = game.Clone().CastIn<Connect4Game>();
                    if (!clone.Board.IsFull(column))
                    {
                        clone.MakeMove(column);
                        var score = AlphaBeta(clone, depth - 1, alpha, beta, true, myPlayer);
                        minScore = Math.Min(minScore, score);
                        beta = Math.Min(beta, score);

                        if (beta <= alpha)
                            break;
                    }
                }

                return minScore;
            }
        }

        private static int EvaluatePosition(Connect4Game game, Connect4Player player)
        {
            // Évaluation de la position actuelle du plateau
            // Retourne un score représentant l'avantage du joueur
            // Par exemple, une position avantageuse peut avoir un score positif, tandis qu'une position désavantageuse peut avoir un score négatif.

            // À titre d'exemple, cette implémentation simple attribue un score de 1 pour chaque pièce du joueur 'O' et un score de -1 pour chaque pièce du joueur 'X'.

            var score = 0;

            for (var row = 0; row < game.Board.NumberOfRows; row++)
            {
                for (var column = 0; column < game.Board.NumberOfColumns; column++)
                {
                    if (game.Board.GetPiece(row, column) is not Connect4Piece piece) continue;

                    if (piece.Player == player)
                        score++;
                    else
                        score--;
                }
            }

            return score;
        }
    }

    public class Connect4GameTests
    {
        private static Connect4Game CreateNewGame(int numberOfPlayers = 2)
        {
            var players = EnumerableHelper.Range(1, numberOfPlayers, 1).Select(x => new RandomPlayer($"P{x}"));
            var game = new Connect4Game(players);

            return game;
        }

        [Fact]
        public void NewGame_CurrentPlayerIsPlayer1()
        {
            // Arrange
            var game = CreateNewGame();

            // Act
            var currentPlayer = game.GetCurrentPlayer();

            // Assert
            Assert.Equal(game.Players[0], currentPlayer);
        }

        [Fact]
        public void NewGame_BoardIsEmpty()
        {
            // Arrange
            var game = CreateNewGame();

            // Act & Assert
            Assert.True(game.Board.IsEmpty());
        }

        [Fact]
        public void MakeMove_ValidMove_SetsCorrectPlayerAtPosition()
        {
            // Arrange
            var game = CreateNewGame();
            var column = 3;

            // Act
            var moveIsValid = game.MakeMove(column);

            var piece = game.Board.GetPiece(game.Board.NumberOfRows - 1, column);

            // Assert
            Assert.True(moveIsValid);
            Assert.NotNull(piece);
            Assert.Equal(game.Players[0], piece!.Player);
        }

        [Fact]
        public void MakeMove_InvalidMove_ThrowsIndexOutOfRangeException()
        {
            // Arrange
            var game = CreateNewGame();
            var column = game.Board.NumberOfColumns;

            // Act & Assert
            Assert.Throws<IndexOutOfRangeException>(() => game.MakeMove(column));
        }

        [Fact]
        public void MakeMove_InvalidMoveInFullColumn_ReturnFalse()
        {
            // Arrange
            var game = CreateNewGame();
            var column = 3;

            // Act & Assert
            // Fill the column
            for (var row = 0; row < game.Board.NumberOfRows; row++)
                game.MakeMove(column);

            var moveIsValid = game.MakeMove(column);

            // Assert
            Assert.False(moveIsValid);
        }

        [Fact]
        public void IsColumnFull_EmptyColumn_ReturnsFalse()
        {
            // Arrange
            var game = CreateNewGame();
            var column = 0;

            // Act
            var isColumnFull = game.Board.IsFull(column);

            // Assert
            Assert.False(isColumnFull);
        }

        [Fact]
        public void IsColumnFull_FullColumn_ReturnsTrue()
        {
            // Arrange
            var game = CreateNewGame();
            var column = 3;

            // Fill the column
            for (var row = 0; row < game.Board.NumberOfRows; row++)
                game.MakeMove(column);

            // Act
            var isColumnFull = game.Board.IsFull(column);

            // Assert
            Assert.True(isColumnFull);
        }

        [Fact]
        public void IsGameOver_EmptyBoard_ReturnsFalse()
        {
            // Arrange
            var game = CreateNewGame();

            // Act
            var isGameOver = game.IsOver;

            // Assert
            Assert.False(isGameOver);
        }

        [Fact]
        public void IsGameOver_WinningMove_ReturnsTrue()
        {
            // Arrange
            var game = CreateNewGame();

            // Perform winning moves
            game.MakeMove(0); // Player1
            game.MakeMove(1); // Player2
            game.MakeMove(0); // Player1
            game.MakeMove(1); // Player2
            game.MakeMove(0); // Player1
            game.MakeMove(1); // Player2
            game.MakeMove(0); // Player1

            // Act
            var isGameOver = game.IsOver;
            var pieceWinners = game.WinnerPieces.ToList();

            // Assert
            Assert.True(isGameOver);
            Assert.Equal(4, pieceWinners.Count);
            Assert.All(pieceWinners, x => Assert.Equal(game.Players[0], x.Player));
        }

        [Fact]
        public void IsGameOver_FullBoardNoWin_ReturnsTrue()
        {
            // Arrange
            var game = CreateNewGame();

            // Fill the board without a win
            for (var column = 0; column < game.Board.NumberOfColumns; column++)
            {
                for (var row = 0; row < game.Board.NumberOfRows; row++)
                    game.MakeMove(column);
            }

            // Act
            var isGameOver = game.IsOver;

            // Assert
            Assert.True(isGameOver);
        }

        [Fact]
        public void IsGameOver_WinningHorizontalMove_ReturnsSameRow()
        {
            // Arrange
            var game = CreateNewGame();

            // Perform winning moves
            game.MakeMove(0); // Player1
            game.MakeMove(0); // Player2
            game.MakeMove(1); // Player1
            game.MakeMove(1); // Player2
            game.MakeMove(2); // Player1
            game.MakeMove(2); // Player2
            game.MakeMove(3); // Player1

            // Act
            var pieceWinners = game.WinnerPieces.ToList();

            // Assert
            Assert.Equal(game.Players[0], game.Winner);
            Assert.Equal(4, pieceWinners.Count);
            Assert.All(pieceWinners, x => Assert.Equal(game.Board.NumberOfRows - 1, game.Board.FindPiece(x)!.Value.Row));
        }

        [Fact]
        public void IsGameOver_WinningVerticalMove_ReturnsSameColumn()
        {
            // Arrange
            var game = CreateNewGame();

            // Perform winning moves
            game.MakeMove(3); // Player1
            game.MakeMove(1); // Player2
            game.MakeMove(3); // Player1
            game.MakeMove(1); // Player2
            game.MakeMove(3); // Player1
            game.MakeMove(2); // Player2
            game.MakeMove(3); // Player1

            // Act
            var pieceWinners = game.WinnerPieces.ToList();

            // Assert
            Assert.Equal(game.Players[0], game.Winner);
            Assert.Equal(4, pieceWinners.Count);
            Assert.All(pieceWinners, x => Assert.Equal(3, game.Board.FindPiece(x)!.Value.Column));
        }

        [Fact]
        public void IsGameOver_WinningDiagonalBottomToTopMove_ReturnsSameColumn()
        {
            // Arrange
            var game = CreateNewGame();

            // Perform winning moves
            game.MakeMove(1); // Player1
            game.MakeMove(2); // Player2
            game.MakeMove(2); // Player1
            game.MakeMove(3); // Player2
            game.MakeMove(4); // Player1
            game.MakeMove(3); // Player2
            game.MakeMove(3); // Player1
            game.MakeMove(4); // Player2
            game.MakeMove(4); // Player1
            game.MakeMove(5); // Player2
            game.MakeMove(4); // Player1

            // Act
            var pieceWinners = game.WinnerPieces.ToList();

            // Assert
            Assert.Equal(game.Players[0], game.Winner);
            Assert.Equal(4, pieceWinners.Count);

            Assert.Equal(new BoardCoordinates(game.Board.NumberOfRows - 1, 1), game.Board.FindPiece(pieceWinners[0])!.Value);
            Assert.Equal(new BoardCoordinates(game.Board.NumberOfRows - 2, 2), game.Board.FindPiece(pieceWinners[1])!.Value);
            Assert.Equal(new BoardCoordinates(game.Board.NumberOfRows - 3, 3), game.Board.FindPiece(pieceWinners[2])!.Value);
            Assert.Equal(new BoardCoordinates(game.Board.NumberOfRows - 4, 4), game.Board.FindPiece(pieceWinners[3])!.Value);
        }

        [Fact]
        public void IsGameOver_WinningDiagonalTopToBottomMove_ReturnsSameColumn()
        {
            // Arrange
            var game = CreateNewGame();

            // Perform winning moves
            game.MakeMove(4); // Player1
            game.MakeMove(3); // Player2
            game.MakeMove(3); // Player1
            game.MakeMove(2); // Player2
            game.MakeMove(1); // Player1
            game.MakeMove(2); // Player2
            game.MakeMove(2); // Player1
            game.MakeMove(1); // Player2
            game.MakeMove(1); // Player1
            game.MakeMove(0); // Player2
            game.MakeMove(1); // Player1

            // Act
            var pieceWinners = game.WinnerPieces.ToList();

            // Assert
            Assert.Equal(game.Players[0], game.Winner);
            Assert.Equal(4, pieceWinners.Count);

            Assert.Equal(new BoardCoordinates(game.Board.NumberOfRows - 4, 1), game.Board.FindPiece(pieceWinners[0])!.Value);
            Assert.Equal(new BoardCoordinates(game.Board.NumberOfRows - 3, 2), game.Board.FindPiece(pieceWinners[1])!.Value);
            Assert.Equal(new BoardCoordinates(game.Board.NumberOfRows - 2, 3), game.Board.FindPiece(pieceWinners[2])!.Value);
            Assert.Equal(new BoardCoordinates(game.Board.NumberOfRows - 1, 4), game.Board.FindPiece(pieceWinners[3])!.Value);
        }

        [Fact]
        public void HistoryMoves_NextMove_AddHistoryMove()
        {
            var player1 = new RandomPlayer("X");
            var player2 = new AIPlayer("O");
            var game = new Connect4Game([player1, player2]);

            var moveCount = 0;
            while (!game.IsOver)
            {
                if (game.NextMove())
                {
                    moveCount++;
                    Assert.Equal(moveCount, game.GetHistoryMoves().Count());
                }
            }
        }
    }
}
