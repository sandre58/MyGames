// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;

namespace MyGames.Domain
{
    public readonly struct BoardDirection(int row, int column)
    {
        public static BoardDirection Up => new(-1, 0);

        public static BoardDirection Down => new(1, 0);

        public static BoardDirection Left => new(0, -1);

        public static BoardDirection Right => new(0, 1);

        public static BoardDirection UpLeft => new(-1, -1);

        public static BoardDirection UpRight => new(-1, 1);

        public static BoardDirection DownLeft => new(1, -1);

        public static BoardDirection DownRight => new(1, 1);

        public int Row { get; } = row;

        public int Column { get; } = column;

        public static BoardDirection operator +(BoardDirection a, BoardDirection b) => new(a.Row + b.Row, a.Column + b.Column);

        public static BoardDirection operator -(BoardDirection a, BoardDirection b) => new(a.Row - b.Row, a.Column - b.Column);

        public static BoardDirection operator *(BoardDirection a, int b) => new(a.Row * b, a.Column * b);

        public static BoardDirection operator /(BoardDirection a, int b) => new(a.Row / b, a.Column / b);

        public static bool operator ==(BoardDirection a, BoardDirection b) => a.Row == b.Row && a.Column == b.Column;

        public static bool operator !=(BoardDirection a, BoardDirection b) => a.Row != b.Row || a.Column != b.Column;

        public override bool Equals(object? obj) => obj is BoardDirection direction && this == direction;

        [ExcludeFromCodeCoverage]
        public override int GetHashCode() => Row.GetHashCode() ^ Column.GetHashCode();

        [ExcludeFromCodeCoverage]
        public override string ToString() => $"({Row}, {Column})";
    }
}
