// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using MyNet.Utilities;

namespace MyGames.Domain
{
    [DebuggerDisplay("{DebuggerDisplayValue}")]
    public class Square<T>(BoardCoordinates coordinates, T? piece = default) : ISimilar<Square<T>>
        where T : IPiece
    {
        private readonly BoardCoordinates _coordinates = coordinates;
        private T? _piece = piece;

        [ExcludeFromCodeCoverage]
        private string? DebuggerDisplayValue => ToString();

        public T Piece => _piece ?? throw new System.InvalidOperationException("The square is empty.");

        public BoardCoordinates Coordinates => _coordinates;

        public int Row => _coordinates.Row;

        public int Column => _coordinates.Column;

        public bool IsEmpty => _piece is null;

        internal void SetPiece(T piece) => _piece = piece;

        internal void Clear() => _piece = default;

        public bool IsSimilar(Square<T>? obj) => obj is not null && Coordinates == obj.Coordinates && (IsEmpty && obj.IsEmpty || !IsEmpty && !obj.IsEmpty && Piece.IsSimilar(obj.Piece));

        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{_coordinates}, {_piece}";
    }
}
