// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Utilities.Generator;

namespace MyGames.Domain.UnitTests.Mocks
{
    public class MockPiece : IPiece
    {
        public MockPiece() : this(RandomGenerator.Int(1, 2000)) { }

        private MockPiece(int id) => Id = id;

        public int Id { get; set; }

        public bool IsSimilar(IPiece? obj) => obj is MockPiece piece && Id == piece.Id;
    }
}
