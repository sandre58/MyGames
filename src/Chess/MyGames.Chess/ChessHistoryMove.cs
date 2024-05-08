// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyGames.Common;

namespace MyGames.Chess
{
    public class ChessHistoryMove : HistoryMove<ChessPlayer, ChessMove>
    {
        public ChessHistoryMove(ChessPlayer player, ChessMove move) : base(player, move)
        {
        }
    }
}
