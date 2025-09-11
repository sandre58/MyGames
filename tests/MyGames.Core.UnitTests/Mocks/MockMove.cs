// -----------------------------------------------------------------------
// <copyright file="MockMove.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyNet.Utilities.Generator;

namespace MyGames.Core.UnitTests.Mocks;

public class MockMove : IMove<MockBoard, MockMove>, IPlayedMove
{
    public MockMove(MockPlayer player)
        : this(player, RandomGenerator.Int(1, 10)) { }

    public MockMove(MockPlayer player, int value) => (Player, Value) = (player, value);

    public int Value { get; }

    public MockPlayer Player { get; }

    public MockMove Apply(MockBoard board, IPlayer player) => this;
}
