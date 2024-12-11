// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using MyGames.Chess.Strategies;

namespace MyGames.Chess.Console.Players
{
    internal class AIPlayer(string name, string color, Level level = Level.Medium) : ConsolePlayer(name, color)
    {
        public Level Level { get; set; } = level;

        public override IChessMove NextMove(ChessGame game)
        {
            var move = new ChessAlphaBetaStrategy(Level).ProvideMove(game, this);

            return move ?? throw new InvalidOperationException("No move allowed");
        }
    }
}
