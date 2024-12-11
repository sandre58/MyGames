// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;

namespace MyGames.Domain.Exceptions
{
    public class InvalidMoveException : Exception
    {
        public IMove Move { get; }

        public IPlayer Player { get; }

        public InvalidMoveException(IPlayer player, IMove move) : base("Invalid move.")
        {
            Move = move;
            Player = player;
        }
    }
}
