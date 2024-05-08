// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyGames.Common;

namespace MyGames.Connect4.Core
{
    public abstract class Connect4Player : Player
    {
        protected Connect4Player(string name, byte[]? image = null) : base(name, image)
        {
        }

        public abstract Connect4Move NextMove(Connect4Game game);
    }
}
