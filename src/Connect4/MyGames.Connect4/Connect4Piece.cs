// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using MyGames.Domain;

namespace MyGames.Connect4
{
    public class Connect4Piece : IPiece
    {
        public Connect4Piece(IConnect4Player player) => Player = player;

        public IConnect4Player Player { get; }

        public bool IsSimilar(IPiece? obj) => obj is Connect4Piece piece && Player == piece.Player;

        [ExcludeFromCodeCoverage]
        public override string ToString() => string.Format("{0,1}", Player.ToString());
    }
}
