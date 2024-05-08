// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyGames.Common
{
    public abstract class HistoryMove<TPlayer, TMove>
        where TPlayer : IPlayer
        where TMove : IMove
    {
        protected HistoryMove(TPlayer player, TMove move) => (Player, Move) = (player, move);

        public TPlayer Player { get; }

        public TMove Move { get; }

        public override string ToString() => $"{Player}: {Move}";
    }
}
