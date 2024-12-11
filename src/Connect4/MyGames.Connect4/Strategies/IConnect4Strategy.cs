// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyGames.Domain.Strategies;

namespace MyGames.Connect4.Strategies
{
    public interface IConnect4Strategy : IStrategy<Connect4Game, Connect4Board, Connect4Move, Connect4Move>
    {
    }
}
