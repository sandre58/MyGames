// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyGames.Domain;

namespace MyGames.Chess
{
    public interface IChessPlayer : IPlayer
    {
        IChessMove NextMove(ChessGame game);
    }
}
