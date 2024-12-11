// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Utilities.Generator;
using System.Linq;

namespace MyGames.Chess.UnitTests.Mocks
{
    public class MockPlayer : IChessPlayer
    {
        public IChessMove NextMove(ChessGame game)
        {
            var piece = RandomGenerator.ListItem(game.GetPieces(this).ToList());
            return new ChessMove(piece, RandomGenerator.ListItem(piece.GetPossibleMoves(game.Board).ToList()));
        }
    }
}
