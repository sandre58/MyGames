// -----------------------------------------------------------------------
// <copyright file="Square.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;
using MyNet.Utilities;

namespace MyGames.Core;

[DebuggerDisplay("{DebuggerDisplayValue}")]
public class Square<T>(BoardCoordinates coordinates, T? piece = default) : ISimilar<Square<T>>
    where T : IPiece
{
    private T? _piece = piece;

    private string? DebuggerDisplayValue => ToString();

    public T Piece => _piece ?? throw new System.InvalidOperationException("The square is empty.");

    public BoardCoordinates Coordinates { get; } = coordinates;

    public int Row => Coordinates.Row;

    public int Column => Coordinates.Column;

    public bool IsEmpty => _piece is null;

    internal void SetPiece(T piece) => _piece = piece;

    internal void Clear() => _piece = default;

    public bool IsSimilar(Square<T>? obj) => obj is not null && Coordinates == obj.Coordinates && ((IsEmpty && obj.IsEmpty) || (!IsEmpty && !obj.IsEmpty && Piece.IsSimilar(obj.Piece)));

    public override string ToString() => $"{Coordinates}, {_piece}";
}
