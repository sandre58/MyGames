// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyGames.Common;

namespace MyGames.Connect4.Core
{
    public class Connect4Move : IMove
    {
        public Connect4Move(int column) => Column = column;

        public int Column { get; }

        public override string ToString() => $"Column {Column}";
    }
}
