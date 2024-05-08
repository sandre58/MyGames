// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyGames.Common;

namespace MyGames.Connect4.Core
{
    public class Connect4Piece : IPiece
    {
        public Connect4Piece(Connect4Player player) => Player = player;

        public Connect4Player Player { get; }

        public override string ToString() => string.Format("{0,1}", Player.ToString());
    }
}
