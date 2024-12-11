// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using MyGames.Domain;
using MyGames.Domain.Exceptions;
using MyNet.Utilities;

namespace MyGames.Connect4
{
    public class Connect4Move : IMove<Connect4Board, Connect4Move>, IPlayedMove
    {
        public Connect4Move(int column) => Column = column;

        public int Column { get; }

        public Connect4Move Apply(Connect4Board board, IPlayer player) => !board.Insert(new Connect4Piece(player.CastIn<IConnect4Player>()), Column) ? throw new InvalidMoveException(player, this) : this;

        [ExcludeFromCodeCoverage]
        public override string ToString() => $"Column {Column}";
    }
}
