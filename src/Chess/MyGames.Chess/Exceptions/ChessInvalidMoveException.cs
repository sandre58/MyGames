// -----------------------------------------------------------------------
// <copyright file="ChessInvalidMoveException.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using MyGames.Core;
using MyGames.Core.Exceptions;

namespace MyGames.Chess.Exceptions;

public class ChessInvalidMoveException : InvalidMoveException
{
    public bool IsCheck { get; }

    public ChessInvalidMoveException(IPlayer player, IMove move)
        : base(player, move) { }

    public ChessInvalidMoveException(IPlayer player, IMove move, bool isCheck)
        : base(player, move) => IsCheck = isCheck;

    public ChessInvalidMoveException() { }

    public ChessInvalidMoveException(string message)
        : base(message) { }

    public ChessInvalidMoveException(string message, Exception innerException)
        : base(message, innerException) { }
}
