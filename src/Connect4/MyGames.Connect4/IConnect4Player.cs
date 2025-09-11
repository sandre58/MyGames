// -----------------------------------------------------------------------
// <copyright file="IConnect4Player.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyGames.Core;

namespace MyGames.Connect4;

public interface IConnect4Player : IPlayer
{
    Connect4Move NextMove(Connect4Game game);
}
