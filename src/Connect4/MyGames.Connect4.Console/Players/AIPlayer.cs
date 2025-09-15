// -----------------------------------------------------------------------
// <copyright file="AIPlayer.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using MyGames.Connect4.Strategies;

namespace MyGames.Connect4.Console.Players;

internal sealed class AIPlayer(string name, Level level = Level.Medium) : ConsolePlayer(name)
{
    public Level Level { get; set; } = level;

    public override Connect4Move NextMove(Connect4Game game)
    {
        var move = new Connect4AlphaBetaStrategy(Level).ProvideMove(game, this);

        return move ?? throw new InvalidOperationException("No move allowed");
    }
}
