// -----------------------------------------------------------------------
// <copyright file="ChessPiece.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using MyGames.Core;
using MyNet.Utilities;

namespace MyGames.Chess;

[DebuggerDisplay("{DebuggerDisplayValue}")]
public abstract class ChessPiece(ChessColor color) : IPiece
{
    private string? DebuggerDisplayValue => ToString();

    public ChessColor Color { get; } = color;

    protected abstract string GetNotation();

    public IEnumerable<BoardCoordinates> GetPossibleMoves(ChessBoard board) => GetPossibleMoves(board, board.GetSquare(this).Coordinates);

    protected abstract IEnumerable<BoardCoordinates> GetPossibleMoves(ChessBoard board, BoardCoordinates from);

    public bool IsSimilar(IPiece? obj) => obj is ChessPiece piece && Color == piece.Color && GetType() == piece.GetType();

    public override bool Equals(object? obj) => ReferenceEquals(this, obj);

    public override int GetHashCode() => HashCode.Combine(DebuggerDisplayValue, Color);

    public override string ToString() => GetNotation() + Color.ToString().GetInitials().ToLower(CultureInfo.CurrentCulture);
}
