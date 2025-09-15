// -----------------------------------------------------------------------
// <copyright file="MockPiece.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyNet.Utilities.Generator;

namespace MyGames.Core.UnitTests.Mocks;

public class MockPiece : IPiece
{
    public MockPiece()
        : this(RandomGenerator.Int(1, 2000)) { }

    private MockPiece(int id) => Id = id;

    public int Id { get; set; }

    public bool IsSimilar(IPiece? obj) => obj is MockPiece piece && Id == piece.Id;
}
