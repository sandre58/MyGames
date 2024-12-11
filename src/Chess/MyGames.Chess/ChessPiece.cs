// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using MyGames.Domain;
using MyNet.Utilities;

namespace MyGames.Chess
{
    [DebuggerDisplay("{DebuggerDisplayValue}")]
    public abstract class ChessPiece : IPiece
    {
        protected ChessPiece(ChessColor color) => Color = color;

        [ExcludeFromCodeCoverage]
        private string? DebuggerDisplayValue => ToString();

        public ChessColor Color { get; }

        protected abstract string GetNotation();

        public IEnumerable<BoardCoordinates> GetPossibleMoves(ChessBoard board) => GetPossibleMoves(board, board.GetSquare(this).Coordinates);

        protected abstract IEnumerable<BoardCoordinates> GetPossibleMoves(ChessBoard board, BoardCoordinates from);

        public bool IsSimilar(IPiece? obj) => obj is ChessPiece piece && Color == piece.Color && GetType() == piece.GetType();

        public override bool Equals(object? obj) => ReferenceEquals(this, obj);

        [ExcludeFromCodeCoverage]
        public override int GetHashCode() => HashCode.Combine(DebuggerDisplayValue, Color);

        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{GetNotation()}{Color.ToString().GetInitials().ToLower()}";
    }
}
