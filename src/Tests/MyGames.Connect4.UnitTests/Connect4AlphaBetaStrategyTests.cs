// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using MyGames.Connect4.Strategies;
using MyGames.Connect4.UnitTests.Mocks;
using Xunit;

namespace MyGames.Connect4.UnitTests
{
    public class Connect4AlphaBetaStrategyTests
    {
        private readonly Connect4AlphaBetaStrategy _strategy;
        private readonly Connect4Game _game;
        private readonly IConnect4Player _player;

        public Connect4AlphaBetaStrategyTests()
        {
            _strategy = new Connect4AlphaBetaStrategy(Level.Medium);
            _player = new MockPlayer();
            _game = new Connect4Game(new List<IConnect4Player> { _player, new MockPlayer() });
        }

        [Fact]
        public void ProvideMove_ShouldReturnValidMove()
        {
            // Act
            var move = _strategy.ProvideMove(_game, _player);

            // Assert
            Assert.NotNull(move);
            Assert.InRange(move.Column, 0, _game.Board.Columns.Count - 1);
        }

        [Fact]
        public void ProvideMove_ShouldThrowException_WhenNoMoveAllowed()
        {
            // Arrange
            for (int i = 0; i < _game.Board.Columns.Count; i++)
            {
                for (int j = 0; j < _game.Board.Rows.Count; j++)
                {
                    _game.Board.Insert(new Connect4Piece(_player), i);
                }
            }

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _strategy.ProvideMove(_game, _player));
        }

        [Fact]
        public void EvaluatePosition_ShouldReturnCorrectScore()
        {
            // Arrange
            var piece = new Connect4Piece(_player);
            _game.Board.Insert(piece, 0);
            _game.Board.Insert(piece, 1);
            _game.Board.Insert(piece, 2);

            // Act
            var score = Connect4AlphaBetaStrategy.EvaluatePosition(_game, _player);

            // Assert
            Assert.True(score > 0);
        }

        [Fact]
        public void GetValidMoves_ShouldReturnNonFullColumns()
        {
            // Act
            var validMoves = Connect4AlphaBetaStrategy.GetValidMoves(_game.Board.Columns);

            // Assert
            Assert.NotEmpty(validMoves);
            Assert.All(validMoves, move => Assert.False(_game.Board.GetColumn(move.Column).IsFull()));
        }

        [Fact]
        public void GetValidMoves_ShouldReturnNonEmptyList()
        {
            // Arrange
            var player = new MockPlayer();
            var players = new List<IConnect4Player> { player };
            var game = new Connect4Game(players);

            // Act
            var validMoves = Connect4AlphaBetaStrategy.GetValidMoves(game.Board.Columns);

            // Assert
            Assert.NotEmpty(validMoves);
        }

        [Fact]
        public void GetValidMoves_ShouldReturnEmptyList()
        {
            // Act
            var validMoves = Connect4AlphaBetaStrategy.GetValidMoves([]);

            // Assert
            Assert.Empty(validMoves);
        }
    }
}
