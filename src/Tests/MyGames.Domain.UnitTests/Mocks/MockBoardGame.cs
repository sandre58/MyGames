// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using MyNet.Utilities;

namespace MyGames.Domain.UnitTests.Mocks
{
    public class MockBoardGame : BoardGame<MockBoard, MockPlayer, MockPiece, MockMove, MockMove>
    {
        public MockBoardGame(MockBoard board, IEnumerable<MockPlayer> players, int currentPlayerIndex = 0) : base(board, players, currentPlayerIndex)
        {
        }

        protected override MockPlayer? GetWinner() => null;

        protected override bool IsDraw() => false;

        protected override BoardGame<MockBoard, MockPlayer, MockPiece, MockMove, MockMove> NewInstance(MockBoard board) => new MockBoardGame(board.Clone().CastIn<MockBoard>(), Players);

        protected override MockMove NextMoveOfCurrentPlayer() => new(GetCurrentPlayer());
    }
}
