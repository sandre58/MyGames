// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyGames.Domain;

namespace MyGames.Connect4
{
    public interface IConnect4Player : IPlayer
    {
        public Connect4Move NextMove(Connect4Game game);
    }
}
