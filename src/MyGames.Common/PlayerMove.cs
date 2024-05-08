// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyGames.Common
{
    public class PlayerMove<TMove> where TMove : IMove
    {
        public PlayerMove(IPlayer player, TMove move)
        {
            Player = player;
            Move = move;
        }

        public IPlayer Player { get; }

        public TMove Move { get; }

        public override string ToString() => $"{Player} : {Move}";
    }
}
