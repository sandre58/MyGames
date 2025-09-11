// -----------------------------------------------------------------------
// <copyright file="BoardDirection.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace MyGames.Core;

public readonly struct BoardDirection(int row, int column) : IEquatable<BoardDirection>
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

    public static BoardDirection Add(BoardDirection left, BoardDirection right) => new(left.Row + right.Row, left.Column + right.Column);

    public static BoardDirection Subtract(BoardDirection left, BoardDirection right) => new(left.Row - right.Row, left.Column - right.Column);

    public static BoardDirection Multiply(BoardDirection left, int right) => new(left.Row * right, left.Column * right);

    public static BoardDirection Divide(BoardDirection left, int right) => new(left.Row / right, left.Column / right);

    public static BoardDirection operator +(BoardDirection a, BoardDirection b) => Add(a, b);

    public static BoardDirection operator -(BoardDirection a, BoardDirection b) => Subtract(a, b);

    public static BoardDirection operator *(BoardDirection a, int b) => Multiply(a, b);

    public static BoardDirection operator /(BoardDirection a, int b) => Divide(a, b);

    public static bool operator ==(BoardDirection a, BoardDirection b) => a.Row == b.Row && a.Column == b.Column;

    public static bool operator !=(BoardDirection a, BoardDirection b) => a.Row != b.Row || a.Column != b.Column;

    public bool Equals(BoardDirection other) => this == other;

    public override bool Equals(object? obj) => obj is BoardDirection direction && Equals(direction);

    public override int GetHashCode() => Row.GetHashCode() ^ Column.GetHashCode();

    public override string ToString() => $"({Row}, {Column})";
}
