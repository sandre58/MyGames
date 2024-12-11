// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using MyGames.Chess.Extensions;
using MyGames.Chess.UnitTests.Mocks;
using MyGames.Domain;
using MyNet.Utilities;
using MyNet.Utilities.Generator;
using Xunit;

namespace MyGames.Chess.UnitTests
{
    public class ChessGameTests
    {
        private static ChessGame CreateGame(Func<ChessBoardPiecesCollection, ChessBoardPiecesCollection, IDictionary<ChessPiece, (int row, int column)>>? newPiecesCreation = null)
        {
            var whitePlayer = new MockPlayer();
            var blackPlayer = new MockPlayer();
            var board = newPiecesCreation is not null ? new ChessBoard(newPiecesCreation) : new ChessBoard();
            return new ChessGame(board, whitePlayer, blackPlayer);
        }

        [Fact]
        public void WhitePlayer_ShouldBeAssignedCorrectly()
        {
            var game = CreateGame();

            Assert.Equal(game.Players[0], game.WhitePlayer);
            Assert.Equal(game.Players[1], game.BlackPlayer);
        }

        [Fact]
        public void SetResult_ShouldUpdateResult()
        {
            var game = CreateGame();

            game.SetResult(ChessResult.WhiteWin);
            Assert.Equal(game.WhitePlayer, game.Winner);
        }

        [Fact]
        public void IsCheckmate_ShouldReturnFalseInitially()
        {
            var game = CreateGame();

            Assert.False(game.HasCheckmate());
        }

        [Fact]
        public void IsStalemate_ShouldReturnFalseInitially()
        {
            var game = CreateGame();

            Assert.False(game.HasStalemate());
        }

        [Fact]
        public void IsCheck_ShouldReturnFalseInitially()
        {
            var game = CreateGame();

            Assert.False(game.HasCheck());
        }
        [Fact]
        public void IsStalemate_ShouldReturnTrue_WhenKingIsInStalemate()
        {
            // Arrange
            var game = CreateGame((whites, blacks) => new Dictionary<ChessPiece, (int, int)>
            {
                { whites.King, (0, 0) },
                { blacks.King, (2, 1) },
                { blacks.Queen, (1, 2) }
            });

            // Act
            var result = game.IsStalemate(game.WhitePlayer);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsStalemate_ShouldReturnFalse_WhenKingIsNotInStalemate()
        {
            // Arrange
            var game = CreateGame((whites, blacks) => new Dictionary<ChessPiece, (int, int)>
            {
                { whites.King, (0, 0) },
                { blacks.King, (7, 7) },
                { blacks.LeftRook, (1, 1) }
            });

            // Act
            var result = game.IsStalemate(game.WhitePlayer);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsStalemate_ShouldReturnFalse_WhenKingIsInCheck()
        {
            // Arrange
            var game = CreateGame((whites, blacks) => new Dictionary<ChessPiece, (int, int)>
            {
                { whites.King, (0, 0) },
                { blacks.King, (7, 7) },
                { blacks.Queen, (0, 1) }
            });

            // Act
            var result = game.IsStalemate(game.WhitePlayer);

            // Assert
            Assert.False(result);
        }


        [Fact]
        public void HasStalemate_ShouldReturnTrue_WhenOnlyKingsRemain()
        {
            // Arrange
            var game = CreateGame((whites, blacks) => new Dictionary<ChessPiece, (int, int)>
            {
                { whites.King, (0, 0) },
                { blacks.King, (7, 7) },
            });

            // Act
            var result = game.HasStalemate();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void HasStalemate_ShouldReturnTrue_WhenFiftyMoveRuleIsMet()
        {
            // Arrange
            var game = CreateGame((whites, blacks) => new Dictionary<ChessPiece, (int, int)>
            {
                { whites.Queen, (3, 4) },
                { blacks.Queen, (4, 4) },
            });

            for (int i = 0; i < ChessGame.MaxMovesForStalemate / 2; i++)
            {
                game.Move(game.Whites.Queen, RandomGenerator.ListItem([BoardDirection.Right, BoardDirection.Left]));
                game.Move(game.Blacks.Queen, RandomGenerator.ListItem([BoardDirection.Right, BoardDirection.Left]));
            }

            // Act
            var result = game.HasStalemate();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void HasStalemate_ShouldReturnTrue_WhenThreefoldRepetitionOccurs()
        {
            // Arrange
            var game = CreateGame((whites, blacks) => new Dictionary<ChessPiece, (int, int)>
            {
                { whites.Queen, (3, 4) },
                { blacks.Queen, (4, 4) },
            });

            game.Move(game.Whites.Queen, BoardDirection.Right);
            game.Move(game.Whites.Queen, BoardDirection.Left);

            game.Move(game.Whites.Queen, BoardDirection.Left);
            game.Move(game.Whites.Queen, BoardDirection.Right);

            game.Move(game.Whites.Queen, BoardDirection.Right);
            game.Move(game.Whites.Queen, BoardDirection.Left);

            // Act
            var result = game.HasStalemate();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void HasStalemate_ShouldReturnFalse_WhenStalemateConditionsAreNotMet()
        {
            // Arrange
            var game = CreateGame();

            // Act
            var result = game.HasStalemate();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsCheck_ShouldReturnTrue_WhenKingIsInCheck()
        {
            // Arrange
            var game = CreateGame((whites, blacks) => new Dictionary<ChessPiece, (int, int)>
            {
                { whites.King, (0, 0) },
                { blacks.LeftRook, (0, 1) }
            });

            // Act
            var result = game.IsCheck(game.WhitePlayer);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsCheck_ShouldReturnFalse_WhenKingIsNotInCheck()
        {
            // Arrange
            var game = CreateGame((whites, blacks) => new Dictionary<ChessPiece, (int, int)>
            {
                { whites.King, (0, 0) },
                { blacks.LeftRook, (1, 1) },
            });

            // Act
            var result = game.IsCheck(game.WhitePlayer);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetPieces_ShouldReturnWhitePieces_WhenPlayerIsWhite()
        {
            // Arrange
            var game = CreateGame();

            // Act
            var pieces = game.GetPieces(game.WhitePlayer);

            // Assert
            Assert.Equal(game.Whites, pieces);
        }

        [Fact]
        public void GetOpponentPieces_ShouldReturnBlackPieces_WhenPlayerIsWhite()
        {
            // Arrange
            var game = CreateGame();

            // Act
            var pieces = game.GetOpponentPieces(game.WhitePlayer);

            // Assert
            Assert.Equal(game.Blacks, pieces);
        }

        [Fact]
        public void GetPlayer_ShouldReturnWhitePlayer_WhenColorIsWhite()
        {
            // Arrange
            var game = CreateGame();

            // Act
            var player = game.GetPlayer(ChessColor.White);

            // Assert
            Assert.Equal(game.WhitePlayer, player);
        }

        [Fact]
        public void GetPlayer_ShouldReturnBlackPlayer_WhenColorIsBlack()
        {
            // Arrange
            var game = CreateGame();

            // Act
            var player = game.GetPlayer(ChessColor.Black);

            // Assert
            Assert.Equal(game.BlackPlayer, player);
        }

        [Fact]
        public void GetPlayer_ShouldThrowNotSupportedException_WhenColorIsInvalid()
        {
            // Arrange
            var game = CreateGame();

            // Act & Assert
            Assert.Throws<NotSupportedException>(() => game.GetPlayer((ChessColor)999));
        }

        [Fact]
        public void GetOpponent_ShouldReturnBlackPlayer_WhenPlayerIsWhite()
        {
            // Arrange
            var game = CreateGame();

            // Act
            var opponent = game.GetOpponent(game.WhitePlayer);

            // Assert
            Assert.Equal(game.BlackPlayer, opponent);
        }

        [Fact]
        public void GetOpponent_ShouldReturnWhitePlayer_WhenPlayerIsBlackPlayer()
        {
            // Arrange
            var game = CreateGame();

            // Act
            var opponent = game.GetOpponent(game.BlackPlayer);

            // Assert
            Assert.Equal(game.WhitePlayer, opponent);
        }

        [Fact]
        public void GetOpponent_ShouldThrowNotSupportedException_WhenPlayerIsInvalid()
        {
            // Arrange
            var game = CreateGame();

            // Act & Assert
            Assert.Throws<NotSupportedException>(() => game.GetOpponent(new MockPlayer()));
        }

        [Fact]
        public void GetColor_ShouldReturnWhite_WhenPlayerIsWhite()
        {
            // Arrange
            var game = CreateGame();

            // Act
            var color = game.GetColor(game.WhitePlayer);

            // Assert
            Assert.Equal(ChessColor.White, color);
        }

        [Fact]
        public void GetColor_ShouldReturnBlack_WhenPlayerIsBlackPlayer()
        {
            // Arrange
            var game = CreateGame();

            // Act
            var color = game.GetColor(game.BlackPlayer);

            // Assert
            Assert.Equal(ChessColor.Black, color);
        }

        [Fact]
        public void GetColor_ShouldThrowNotSupportedException_WhenPlayerIsInvalid()
        {
            // Arrange
            var game = CreateGame();

            // Act & Assert
            Assert.Throws<NotSupportedException>(() => game.GetColor(new MockPlayer()));
        }

        [Fact]
        public void GetOpponentColor_ShouldReturnBlack_WhenPlayerIsWhite()
        {
            // Arrange
            var game = CreateGame();

            // Act
            var color = game.GetOpponentColor(game.WhitePlayer);

            // Assert
            Assert.Equal(ChessColor.Black, color);
        }

        [Fact]
        public void GetOpponentColor_ShouldReturnWhite_WhenPlayerIsBlackPlayer()
        {
            // Arrange
            var game = CreateGame();

            // Act
            var color = game.GetOpponentColor(game.BlackPlayer);

            // Assert
            Assert.Equal(ChessColor.White, color);
        }

        [Fact]
        public void GetOpponentColor_ShouldThrowNotSupportedException_WhenPlayerIsInvalid()
        {
            // Arrange
            var game = CreateGame();

            // Act & Assert
            Assert.Throws<NotSupportedException>(() => game.GetOpponentColor(new MockPlayer()));
        }

        [Fact]
        public void GetOpponentColor_Static_ShouldReturnBlack_WhenColorIsWhite()
        {
            // Act
            var color = ChessGame.GetOpponentColor(ChessColor.White);

            // Assert
            Assert.Equal(ChessColor.Black, color);
        }

        [Fact]
        public void GetOpponentColor_ShouldReturnWhite_WhenColorIsBlack()
        {
            // Act
            var color = ChessGame.GetOpponentColor(ChessColor.Black);

            // Assert
            Assert.Equal(ChessColor.White, color);
        }

        [Fact]
        public void GetOpponentColor_ShouldThrowNotSupportedException_WhenColorIsInvalid()
        {
            // Arrange
            var invalidColor = (ChessColor)999;

            // Act & Assert
            Assert.Throws<NotSupportedException>(() => ChessGame.GetOpponentColor(invalidColor));
        }

        [Fact]
        public void NewInstance_ShouldCreateNewGameWithSamePlayers()
        {
            // Arrange
            var game = CreateGame();

            // Act
            var newInstance = game.Clone().CastIn<ChessGame>();

            // Assert
            Assert.NotNull(newInstance);
            Assert.Equal(game.WhitePlayer, newInstance.WhitePlayer);
            Assert.Equal(game.BlackPlayer, newInstance.BlackPlayer);
        }

        [Fact]
        public void NewInstance_ShouldCopyCheckStates()
        {
            // Arrange
            var game = CreateGame();
            game.SetResult(ChessResult.WhiteWin);

            // Act
            var newInstance = game.Clone().CastIn<ChessGame>();

            // Assert
            Assert.Equal(game.IsCheck(game.WhitePlayer), newInstance.IsCheck(game.WhitePlayer));
            Assert.Equal(game.IsCheck(game.BlackPlayer), newInstance.IsCheck(game.BlackPlayer));
        }

        [Fact]
        public void NewInstance_ShouldCopyStalemateStates()
        {
            // Arrange
            var game = CreateGame();
            game.SetResult(ChessResult.Draw);

            // Act
            var newInstance = game.Clone().CastIn<ChessGame>();

            // Assert
            Assert.Equal(game.IsStalemate(game.WhitePlayer), newInstance.IsStalemate(game.WhitePlayer));
            Assert.Equal(game.IsStalemate(game.BlackPlayer), newInstance.IsStalemate(game.BlackPlayer));
        }

        [Fact]
        public void NewInstance_ShouldCopyCheckmateStates()
        {
            // Arrange
            var game = CreateGame();
            game.SetResult(ChessResult.BlackWin);

            // Act
            var newInstance = game.Clone().CastIn<ChessGame>();

            // Assert
            Assert.Equal(game.IsCheckmate(game.WhitePlayer), newInstance.IsCheckmate(game.WhitePlayer));
            Assert.Equal(game.IsCheckmate(game.BlackPlayer), newInstance.IsCheckmate(game.BlackPlayer));
        }
    }
}
