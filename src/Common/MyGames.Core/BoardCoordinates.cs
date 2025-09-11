// -----------------------------------------------------------------------
// <copyright file="BoardCoordinates.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyGames.Core;

public readonly struct BoardCoordinates(int row, int column) : System.IEquatable<BoardCoordinates>
{
    public int Row { get; } = row;

    public int Column { get; } = column;

    public static BoardCoordinates Add(BoardCoordinates left, BoardDirection right) => new(left.Row + right.Row, left.Column + right.Column);

    public static BoardDirection Subtract(BoardCoordinates left, BoardCoordinates right) => new(left.Row - right.Row, left.Column - right.Column);

    public static BoardCoordinates Subtract(BoardCoordinates left, BoardDirection right) => new(left.Row - right.Row, left.Column - right.Column);

    public static bool operator ==(BoardCoordinates a, BoardCoordinates b) => a.Row == b.Row && a.Column == b.Column;

    public static bool operator !=(BoardCoordinates a, BoardCoordinates b) => a.Row != b.Row || a.Column != b.Column;

    public static BoardDirection operator -(BoardCoordinates a, BoardCoordinates b) => Subtract(a, b);

    public static BoardCoordinates operator +(BoardCoordinates a, BoardDirection b) => Add(a, b);

    public static BoardCoordinates operator -(BoardCoordinates a, BoardDirection b) => Subtract(a, b);

    public bool Equals(BoardCoordinates other) => this == other;

    public override bool Equals(object? obj) => obj is BoardCoordinates coordinates && Equals(coordinates);

    public override int GetHashCode() => Row.GetHashCode() ^ Column.GetHashCode();

    public override string ToString() => $"({Row}, {Column})";
}
