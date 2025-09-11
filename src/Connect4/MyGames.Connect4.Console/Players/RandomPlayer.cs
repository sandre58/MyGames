// -----------------------------------------------------------------------
// <copyright file="RandomPlayer.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using MyNet.Utilities.Generator;

namespace MyGames.Connect4.Console.Players;

internal sealed class RandomPlayer(string name) : ConsolePlayer(name)
{
    public override Connect4Move NextMove(Connect4Game game) => new(RandomGenerator.ListItem(game.Board.GetValidColumns().ToList()).Index);
}
