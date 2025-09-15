// -----------------------------------------------------------------------
// <copyright file="Connect4Move.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyGames.Core;
using MyGames.Core.Exceptions;
using MyNet.Utilities;

namespace MyGames.Connect4;

public class Connect4Move(int column) : IMove<Connect4Board, Connect4Move>, IPlayedMove
{
    public int Column { get; } = column;

    public Connect4Move Apply(Connect4Board board, IPlayer player) => !board.Insert(new Connect4Piece(player.CastIn<IConnect4Player>()), Column) ? throw new InvalidMoveException(player, this) : this;

    public override string ToString() => $"Column {Column}";
}
