// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyGames.Domain.Strategies;

namespace MyGames.Chess.Strategies
{
    public interface IChessStrategy : IStrategy<ChessGame, ChessBoard, IChessMove, ChessPlayedMove>
    {
    }
}
