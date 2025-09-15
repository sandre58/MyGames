// -----------------------------------------------------------------------
// <copyright file="MockBoardGame.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using MyNet.Utilities;

namespace MyGames.Core.UnitTests.Mocks;

public class MockBoardGame(MockBoard board, IEnumerable<MockPlayer> players, int currentPlayerIndex = 0) : BoardGame<MockBoard, MockPlayer, MockPiece, MockMove, MockMove>(board, players, currentPlayerIndex)
{
    protected override MockPlayer? ComputeWinner() => null;

    protected override bool IsDraw() => false;

    protected override BoardGame<MockBoard, MockPlayer, MockPiece, MockMove, MockMove> NewInstance(MockBoard board) => new MockBoardGame(board.Clone().CastIn<MockBoard>(), Players);

    protected override MockMove NextMoveOfCurrentPlayer() => new(CurrentPlayer);
}
