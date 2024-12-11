// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using MyGames.Connect4.Extensions;
using MyGames.Domain;

namespace MyGames.Connect4
{
    public class Connect4Board : Board<Connect4Piece>
    {
        public const int DefaultRows = 6;
        public const int DefaultColumns = 7;

        public Connect4Board(int rows = DefaultRows, int columns = DefaultColumns) : base(rows, columns) { }

        public Connect4Board(int rows, int columns, IDictionary<Connect4Piece, BoardCoordinates> pieces) : base(rows, columns, pieces) { }

        public IEnumerable<SquaresColumn<Connect4Piece>> GetValidColumns() => Columns.Where(x => !x.IsFull());

        public bool Insert(Connect4Piece piece, int columnIndex)
        {
            var column = GetColumn(columnIndex);
            if (column.IsFull()) return false;

            var row = column.GetNextRow();

            return InsertPiece(piece, new(row, column.Index));
        }

        public bool Remove(int columnIndex)
        {
            var column = GetColumn(columnIndex);
            return !column.IsEmpty() && RemovePiece(new BoardCoordinates(column.GetNextRow() + 1, column.Index));
        }

        protected override Board<Connect4Piece> NewInstance(IDictionary<Connect4Piece, BoardCoordinates> pieces) => new Connect4Board(Rows.Count, Columns.Count, pieces);
    }
}
