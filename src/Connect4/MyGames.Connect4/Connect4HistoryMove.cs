// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyGames.Common;

namespace MyGames.Connect4.Core
{
    public class Connect4HistoryMove : HistoryMove<Connect4Player, Connect4Move>
    {
        public Connect4HistoryMove(Connect4Player player, Connect4Move move) : base(player, move)
        {
        }
    }
}
