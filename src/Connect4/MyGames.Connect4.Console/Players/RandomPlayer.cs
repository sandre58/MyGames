// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Linq;
using MyNet.Utilities.Generator;

namespace MyGames.Connect4.Console.Players
{
    internal class RandomPlayer(string name) : ConsolePlayer(name)
    {
        public override Connect4Move NextMove(Connect4Game game) => new(RandomGenerator.ListItem(game.Board.GetValidColumns().ToList()).Index);
    }
}
