// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using MyNet.Utilities.Extensions;

namespace MyGames.Common
{
    [DebuggerDisplay("{DebuggerDisplayValue}")]
    public abstract class Board<TPiece>
        where TPiece : IPiece
    {
        private readonly TPiece?[,] _board;

        protected Board(int rows, int columns) => _board = new TPiece[rows.IsUpperThanOrThrow(0), columns.IsUpperThanOrThrow(0)];

        private string? DebuggerDisplayValue => ToString();

        public int NumberOfRows => _board.GetLength(0);

        public int NumberOfColumns => _board.GetLength(1);

        public bool IsEmpty(int row, int column) => _board.GetValue(row, column) is null;

        public IDictionary<BoardCoordinates, TPiece> GetPieces()
        {
            var result = new Dictionary<BoardCoordinates, TPiece>();

            for (var row = 0; row < NumberOfRows; row++)
            {
                for (var column = 0; column < NumberOfColumns; column++)
                {
                    if (_board.GetValue(row, column) is TPiece piece)
                        result.Add(new BoardCoordinates(row, column), piece);
                }
            }

            return result;
        }

        public TPiece? GetPiece(int row, int column) => row >= 0 && row < NumberOfRows && column >= 0 && column < NumberOfColumns ? (TPiece?)_board.GetValue(row, column) : default;

        public BoardCoordinates? FindPiece(TPiece piece)
        {
            for (var row = 0; row < NumberOfRows; row++)
            {
                for (var column = 0; column < NumberOfColumns; column++)
                {
                    if (Equals(_board.GetValue(row, column), piece))
                        return new BoardCoordinates(row, column);
                }
            }

            return null;
        }

        protected bool InsertPiece(TPiece piece, BoardCoordinates coordinates, bool replaceIfTaken = true) => InsertPiece(piece, coordinates.Row, coordinates.Column, replaceIfTaken);

        protected virtual bool InsertPiece(TPiece piece, int row, int column, bool replaceIfTaken = true)
        {
            if (!replaceIfTaken && !IsEmpty(row, column)) return false;

            _board.SetValue(piece, row, column);
            return true;
        }

        protected bool RemovePiece(TPiece piece)
        {
            var coordinates = FindPiece(piece);
            return coordinates.HasValue && RemovePiece(coordinates.Value);
        }

        protected bool RemovePiece(BoardCoordinates coordinates) => RemovePiece(coordinates.Row, coordinates.Column);

        protected virtual bool RemovePiece(int row, int column)
        {
            if (IsEmpty(row, column)) return false;

            _board.SetValue(default, row, column);
            return true;
        }

        protected virtual bool MovePiece(TPiece piece, int row, int column)
        {
            var coordinates = FindPiece(piece);

            return coordinates.HasValue && RemovePiece(coordinates.Value) && InsertPiece(piece, coordinates.Value);
        }

        internal void SetFrom(Board<TPiece> board)
        {
            foreach (var piece in GetPieces())
                RemovePiece(piece.Key);

            foreach (var piece in board.GetPieces())
                InsertPiece(piece.Value, piece.Key);
        }

        public Board<TPiece> Clone()
        {
            var clone = NewInstance();

            foreach (var piece in GetPieces())
                clone.InsertPiece(piece.Value, piece.Key);

            return clone;
        }

        protected abstract Board<TPiece> NewInstance();

        public override string ToString()
        {
            var strBuilder = new StringBuilder();

            for (var row = 0; row < NumberOfRows; row++)
            {
                for (var column = 0; column < NumberOfColumns; column++)
                {
                    var piece = GetPiece(row, column);

                    // Left separator
                    strBuilder.Append('|');

                    // Piece
                    strBuilder.Append(string.Format("{0,2}", piece?.ToString()));

                    // Right separator
                    if (column == NumberOfColumns - 1)
                        strBuilder.Append('|');

                }

                strBuilder.AppendLine();
            }

            return strBuilder.ToString();
        }
    }
}
