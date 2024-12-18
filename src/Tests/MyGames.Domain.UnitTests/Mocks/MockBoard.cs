﻿// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace MyGames.Domain.UnitTests.Mocks
{
    public class MockBoard : Board<MockPiece>
    {
        public MockBoard(int rows, int columns) : base(rows, columns) { }

        public MockBoard(int rows, int columns, IDictionary<MockPiece, BoardCoordinates> pieces) : base(rows, columns, pieces) { }

        public MockBoard(Board<MockPiece> board) : base(board) { }

        protected override Board<MockPiece> NewInstance(IDictionary<MockPiece, BoardCoordinates> pieces) => new MockBoard(Rows.Count, Columns.Count, pieces);

        public new bool InsertPiece(MockPiece piece, BoardCoordinates coordinates, bool replaceIfTaken = true) => base.InsertPiece(piece, coordinates, replaceIfTaken);

        public new bool MovePiece(MockPiece piece, BoardCoordinates coordinates) => base.MovePiece(piece, coordinates);

        public new bool RemovePiece(BoardCoordinates coordinates) => base.RemovePiece(coordinates);

        public new bool RemovePiece(MockPiece piece) => base.RemovePiece(piece);
    }
}
