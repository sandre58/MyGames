// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using MyNet.Utilities;

namespace MyGames.Domain
{
    [DebuggerDisplay("{DebuggerDisplayValue}")]
    public abstract class Board<TPiece> : IBoard where TPiece : IPiece
    {
        private readonly Dictionary<BoardCoordinates, Square<TPiece>> _squares = [];
        private readonly Dictionary<TPiece, BoardCoordinates> _pieces = [];
        private readonly Dictionary<int, SquaresColumn<TPiece>> _columns = [];
        private readonly Dictionary<int, SquaresRow<TPiece>> _rows = [];

        protected Board(int rows, int columns) => BuildBoard(rows, columns);

        protected Board(int rows, int columns, IDictionary<TPiece, BoardCoordinates> pieces) : this(rows, columns) => pieces.ForEach(x => InsertPiece(x.Key, x.Value));

        protected Board(Board<TPiece> board) : this(board.Rows.Count, board.Columns.Count) => board.GetPieces().ForEach(x => InsertPiece(x, board.GetCoordinates(x)));

        [ExcludeFromCodeCoverage]
        private string? DebuggerDisplayValue => ToString();

        public IReadOnlyCollection<Square<TPiece>> Squares => _squares.Values;

        public IReadOnlyCollection<SquaresRow<TPiece>> Rows => _rows.Values;

        public IReadOnlyCollection<SquaresColumn<TPiece>> Columns => _columns.Values;

        private void BuildBoard(int rows, int columns)
        {
            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    var coordinates = new BoardCoordinates(row, column);
                    _squares.Add(coordinates, new Square<TPiece>(coordinates));
                }
            }

            _columns.AddRange(_squares.GroupBy(x => x.Value.Column).ToDictionary(x => x.Key, x => new SquaresColumn<TPiece>(x.Key, x.Select(y => y.Value).OrderBy(y => y.Row).ToList())));
            _rows.AddRange(_squares.GroupBy(x => x.Value.Row).ToDictionary(x => x.Key, x => new SquaresRow<TPiece>(x.Key, x.Select(y => y.Value).OrderBy(y => y.Column).ToList())));
        }

        public IEnumerable<TPiece> GetPieces() => _pieces.Keys;

        public SquaresColumn<TPiece> GetColumn(int column) => _columns.GetOrDefault(column, new SquaresColumn<TPiece>(column, []))!;

        public SquaresRow<TPiece> GetRow(int row) => _rows.GetOrDefault(row, new SquaresRow<TPiece>(row, []))!;

        public Square<TPiece> GetSquare(BoardCoordinates coordinates) => _squares[coordinates];

        public Square<TPiece> GetSquare(TPiece piece) => _squares[GetCoordinates(piece)];

        public Square<TPiece>? TryGetSquare(BoardCoordinates coordinates) => _squares.GetOrDefault(coordinates);

        public Square<TPiece>? TryGetSquare(TPiece piece) => TryGetCoordinates(piece) is BoardCoordinates coordinates ? _squares[coordinates] : null;

        public BoardCoordinates GetCoordinates(TPiece piece) => _pieces[piece];

        public BoardCoordinates? TryGetCoordinates(TPiece piece) => Exists(piece) ? _pieces[piece] : null;

        public bool Exists(TPiece piece) => _pieces.ContainsKey(piece);

        public bool Exists(BoardCoordinates coordinates) => _squares.ContainsKey(coordinates);

        public bool IsEmpty(BoardCoordinates coordinates) => _squares.ContainsKey(coordinates) && _squares[coordinates].IsEmpty;

        public bool IsEmpty() => _squares.All(x => x.Value.IsEmpty);

        public bool IsFull() => _squares.All(x => !x.Value.IsEmpty);

        protected virtual bool InsertPiece(TPiece piece, BoardCoordinates coordinates, bool replaceIfTaken = true)
        {
            if (!replaceIfTaken && !IsEmpty(coordinates)) return false;

            _squares[coordinates].SetPiece(piece);
            _pieces.AddOrUpdate(piece, coordinates);

            return true;
        }

        protected bool RemovePiece(TPiece piece) => RemovePiece(_pieces[piece]);

        protected virtual bool RemovePiece(BoardCoordinates coordinates)
        {
            if (IsEmpty(coordinates)) return false;

            var piece = _squares[coordinates].Piece;
            _squares[coordinates].Clear();

            return _pieces.Remove(piece);
        }

        protected virtual bool MovePiece(TPiece piece, BoardCoordinates coordinates) => RemovePiece(piece) && InsertPiece(piece, coordinates);

        public bool IsSimilar(IBoard? obj) => obj is Board<TPiece> board && Rows.Count == board.Rows.Count && Columns.Count == board.Columns.Count && Squares.All(x => board.GetSquare(x.Coordinates).IsSimilar(x));

        internal virtual void SetFrom(Board<TPiece> board)
        {
            _pieces.Clear();
            _squares.Values.ForEach(x => x.Clear());
            board.GetPieces().ForEach(x => InsertPiece(x, board.GetCoordinates(x)));
        }

        [ExcludeFromCodeCoverage]
        IBoard ICloneable<IBoard>.Clone() => Clone();

        public Board<TPiece> Clone() => NewInstance(_pieces);

        protected abstract Board<TPiece> NewInstance(IDictionary<TPiece, BoardCoordinates> pieces);

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            var strBuilder = new StringBuilder();

            foreach (var row in Rows)
            {
                foreach (var square in row)
                {
                    // Row header
                    if (square.Column == 0)
                        strBuilder.Append(row.ToString());

                    // Left separator
                    strBuilder.Append('|');

                    // Piece
                    strBuilder.Append(string.Format("{0,1}", !square.IsEmpty ? square.Piece?.ToString() : string.Empty));

                    // Right separator
                    if (square.Column == row.Count - 1)
                        strBuilder.Append('|');
                }

                strBuilder.AppendLine();
            }

            // Column headers
            strBuilder.Append($"  ");
            foreach (var column in Columns)
            {
                strBuilder.Append($"{column} ");
            }

            return strBuilder.ToString();
        }
    }
}
