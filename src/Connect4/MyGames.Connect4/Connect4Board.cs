// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using MyGames.Common;

namespace MyGames.Connect4.Core
{
    public class Connect4Board : Board<Connect4Piece>
    {
        public const int Rows = 6;
        public const int Columns = 7;

        public Connect4Board(int rows = Rows, int columns = Columns) : base(rows, columns) { }

        public bool IsEmpty(int column) => GetPiece(NumberOfRows - 1, column) is null;

        public bool IsEmpty() => Enumerable.Range(0, NumberOfColumns).All(IsEmpty);

        public bool IsFull(int column) => GetPiece(0, column) is not null;

        public bool IsFull() => Enumerable.Range(0, NumberOfColumns).All(IsFull);

        public IEnumerable<int> GetValidColumns() => Enumerable.Range(0, NumberOfColumns).Where(x => !IsFull(x));

        private int GetRowOfLastPiece(int column) => IsEmpty(column) ? NumberOfRows : Enumerable.Range(0, NumberOfRows).Reverse().LastOrDefault(x => GetPiece(x, column) is not null);

        internal bool Add(Connect4Piece piece, int column)
        {
            if (IsFull(column)) return false;

            var row = GetRowOfLastPiece(column) - 1;

            return InsertPiece(piece, row, column);
        }

        internal bool Remove(int column) => !IsEmpty(column) && RemovePiece(GetRowOfLastPiece(column), column);

        protected override Board<Connect4Piece> NewInstance() => new Connect4Board(NumberOfRows, NumberOfColumns);
    }
}
