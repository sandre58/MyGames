// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyGames.Common
{
    public class HistoryState<TGame, TBoard, TPlayer, TPiece, TMove, THistoryMove>
        where TGame : BoardGame<TBoard, TPlayer, TPiece, TMove, THistoryMove>
        where TBoard : Board<TPiece>
        where TPiece : IPiece
        where TPlayer : IPlayer
        where TMove : IMove
        where THistoryMove : HistoryMove<TPlayer, TMove>
    {
        public HistoryState(HistoryMove<TPlayer, TMove> historyMove, TGame game)
        {
            HistoryMove = historyMove;
            Game = game;
        }

        public HistoryMove<TPlayer, TMove> HistoryMove { get; }

        public TGame Game { get; }
    }
}
