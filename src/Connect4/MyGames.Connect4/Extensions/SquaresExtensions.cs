// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Linq;
using MyGames.Domain;

namespace MyGames.Connect4.Extensions
{
    public static class SquaresExtensions
    {
        public static int GetNextRow(this SquaresColumn<Connect4Piece> column) => column.IsFull() ? -1 : column.Last(x => x.IsEmpty).Row;

    }
}
