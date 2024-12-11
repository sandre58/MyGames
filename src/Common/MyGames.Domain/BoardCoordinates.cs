// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;

namespace MyGames.Domain
{
    public readonly struct BoardCoordinates(int row, int column)
    {
        public int Row { get; } = row;

        public int Column { get; } = column;

        public static bool operator ==(BoardCoordinates a, BoardCoordinates b) => a.Row == b.Row && a.Column == b.Column;

        public static bool operator !=(BoardCoordinates a, BoardCoordinates b) => a.Row != b.Row || a.Column != b.Column;

        public static BoardDirection operator -(BoardCoordinates a, BoardCoordinates b) => new(a.Row - b.Row, a.Column - b.Column);

        public static BoardCoordinates operator +(BoardCoordinates a, BoardDirection b) => new(a.Row + b.Row, a.Column + b.Column);

        public static BoardCoordinates operator -(BoardCoordinates a, BoardDirection b) => new(a.Row - b.Row, a.Column - b.Column);

        public override bool Equals(object? obj) => obj is BoardCoordinates coordinates && this == coordinates;

        [ExcludeFromCodeCoverage]
        public override int GetHashCode() => Row.GetHashCode() ^ Column.GetHashCode();

        [ExcludeFromCodeCoverage]
        public override string ToString() => $"({Row}, {Column})";
    }
}
