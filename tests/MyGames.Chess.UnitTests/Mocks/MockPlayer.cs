// -----------------------------------------------------------------------
// <copyright file="MockPlayer.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using MyNet.Utilities.Generator;

namespace MyGames.Chess.UnitTests.Mocks;

public class MockPlayer : IChessPlayer
{
    public IChessMove NextMove(ChessGame game)
    {
        var piece = RandomGenerator.ListItem(game.GetPieces(this).ToList());
        return new ChessMove(piece, RandomGenerator.ListItem(piece.GetPossibleMoves(game.Board).ToList()));
    }
}
