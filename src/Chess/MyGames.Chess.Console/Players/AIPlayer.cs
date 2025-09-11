// -----------------------------------------------------------------------
// <copyright file="AIPlayer.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using MyGames.Chess.Strategies;

namespace MyGames.Chess.Console.Players;

internal sealed class AIPlayer(string name, string color, Level level = Level.Medium) : ConsolePlayer(name, color)
{
    public Level Level { get; set; } = level;

    public override IChessMove NextMove(ChessGame game)
    {
        var move = new ChessAlphaBetaStrategy(new NaiveBoardEvaluator(), Level).ProvideMove(game, this);

        return move ?? throw new InvalidOperationException("No move allowed");
    }
}
