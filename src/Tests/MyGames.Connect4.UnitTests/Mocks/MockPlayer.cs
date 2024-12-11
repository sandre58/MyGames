// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Utilities.Generator;
using System.Linq;

namespace MyGames.Connect4.UnitTests.Mocks
{
    public class MockPlayer : IConnect4Player
    {
        public Connect4Move NextMove(Connect4Game game) => new(RandomGenerator.ListItem(game.Board.GetValidColumns().ToList()).Index);
    }
}
