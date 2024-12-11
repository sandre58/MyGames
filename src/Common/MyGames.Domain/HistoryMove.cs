// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace MyGames.Domain
{
    [DebuggerDisplay("{DebuggerDisplayValue}")]
    public class HistoryMove<TPlayer, TBoard, TMove, TPlayedMove>
        where TPlayer : IPlayer
        where TBoard : IBoard
        where TMove : IMove<TBoard, TPlayedMove>
        where TPlayedMove : IPlayedMove
    {
        public HistoryMove(TBoard board, TPlayer player, TPlayedMove playedMove) => (Board, Player, Move) = (board, player, playedMove);

        public HistoryMove(TBoard board, HistoryMove<TPlayer, TBoard, TMove, TPlayedMove> historyMove) => (Board, Player, Move) = (board, historyMove.Player, historyMove.Move);

        [ExcludeFromCodeCoverage]
        private string? DebuggerDisplayValue => ToString();

        public TPlayer Player { get; }

        public TPlayedMove Move { get; }

        public TBoard Board { get; }

        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Player}: {Move}";
    }
}
