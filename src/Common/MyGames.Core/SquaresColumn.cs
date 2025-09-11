// -----------------------------------------------------------------------
// <copyright file="SquaresColumn.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace MyGames.Core;

[DebuggerDisplay("{DebuggerDisplayValue}")]
public class SquaresColumn<T> : SquaresCollection<T>
    where T : IPiece
{
    internal SquaresColumn(int index, IEnumerable<Square<T>> squares)
        : base(squares) => Index = index;

    private string? DebuggerDisplayValue => ToString();

    public int Index { get; }

    public override string ToString() => Index.ToString(CultureInfo.InvariantCulture);
}
