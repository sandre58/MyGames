// -----------------------------------------------------------------------
// <copyright file="AlphaBetaTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using MyGames.Core.Strategies;
using MyGames.Core.UnitTests.Mocks;
using MyNet.Utilities;
using Xunit;

namespace MyGames.Core.UnitTests;

public class AlphaBetaTests
{
    private const int Depth = 6;

    private static MockBoardGame CreateGame()
    {
        var board = new MockBoard(6, 7);
        var players = new List<MockPlayer> { new(), new() };

        return new MockBoardGame(board, players);
    }

    private static IList<MockMove> MockValidMoves(MockBoardGame game)
    {
        var result = new List<MockMove>();
        for (var i = 0; i < Depth / 2; i++)
        {
            result.Add(new MockMove(game.Players[0], i + 3));
            result.Add(new MockMove(game.Players[1], i - 3));
        }

        return result;
    }

    private static int EvaluatePosition(MockBoardGame game, MockPlayer player) => game.History.Sum(x => x.Player == player ? x.Move.Value : -x.Move.Value);

    [Fact]
    public void ComputeBestMove_ValidMoves_ReturnsBestMove()
    {
        // Arrange
        var game = CreateGame();

        // Act
        var bestMove = AlphaBetaHelper.ComputeBestMove<MockBoardGame, MockBoard, MockMove, MockMove>(game, Depth, MockValidMoves, x => EvaluatePosition(x, game.Players[0]));

        // Assert
        Assert.NotNull(bestMove);
        Assert.Equal(5, bestMove.Value);
    }

    [Fact]
    public void AlphaBeta_Maximize_ReturnsMaxScore()
    {
        // Arrange
        var game = CreateGame();
        const int alpha = int.MinValue;
        const int beta = int.MaxValue;

        // Act
        var score = AlphaBetaHelper.AlphaBeta<MockBoardGame, MockBoard, MockMove, MockMove>(game, Depth, alpha, beta, MockValidMoves, x => EvaluatePosition(x, game.Players[0]), true);

        // Assert
        Assert.Equal(0, score);
    }

    [Fact]
    public void AlphaBeta_Minimize_ReturnsMinScore()
    {
        // Arrange
        var game = CreateGame();
        const int depth = 3;
        const int alpha = int.MinValue;
        const int beta = int.MaxValue;

        // Act
        var score = AlphaBetaHelper.AlphaBeta<MockBoardGame, MockBoard, MockMove, MockMove>(game, depth, alpha, beta, MockValidMoves, x => EvaluatePosition(x, game.Players[0]), false);

        // Assert
        Assert.Equal(-3, score);
    }
}
