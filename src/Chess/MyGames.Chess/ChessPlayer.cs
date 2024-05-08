// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using MyGames.Common;

namespace MyGames.Chess
{
    public abstract class ChessPlayer : Player
    {
        protected ChessPlayer(string name, byte[]? image = null) : base(name, image)
        {
        }

        public ChessMove NextMove(ChessGame chessGame) => throw new NotImplementedException();
    }
}
