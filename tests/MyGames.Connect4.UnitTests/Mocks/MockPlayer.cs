// -----------------------------------------------------------------------
// <copyright file="MockPlayer.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using MyNet.Utilities.Generator;

namespace MyGames.Connect4.UnitTests.Mocks;

public class MockPlayer : IConnect4Player
{
    public Connect4Move NextMove(Connect4Game game) => new(RandomGenerator.ListItem(game.Board.GetValidColumns().ToList()).Index);
}
