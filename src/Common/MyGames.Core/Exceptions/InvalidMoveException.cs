// -----------------------------------------------------------------------
// <copyright file="InvalidMoveException.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace MyGames.Core.Exceptions;

public class InvalidMoveException : Exception
{
    public IMove Move { get; }

    public IPlayer Player { get; }

    public InvalidMoveException(IPlayer player, IMove move)
        : base("Invalid move.")
    {
        Move = move;
        Player = player;
    }

    public InvalidMoveException() => (Move, Player) = (null!, null!);

    public InvalidMoveException(string message)
        : base(message) => (Move, Player) = (null!, null!);

    public InvalidMoveException(string message, Exception innerException)
        : base(message, innerException) => (Move, Player) = (null!, null!);
}
