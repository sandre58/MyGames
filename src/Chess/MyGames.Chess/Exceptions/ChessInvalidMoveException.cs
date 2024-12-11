// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyGames.Domain;
using MyGames.Domain.Exceptions;

namespace MyGames.Chess.Exceptions
{
    public class ChessInvalidMoveException : InvalidMoveException
    {
        public bool IsCheck { get; }

        public ChessInvalidMoveException(IPlayer player, IMove move, bool isCheck) : base(player, move) => IsCheck = isCheck;
    }
}
