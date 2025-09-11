// -----------------------------------------------------------------------
// <copyright file="SquaresExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using MyGames.Core;

namespace MyGames.Connect4.Extensions;

public static class SquaresExtensions
{
    public static int GetNextRow(this SquaresColumn<Connect4Piece> column) => column.IsFull() ? -1 : column.Last(x => x.IsEmpty).Row;
}
