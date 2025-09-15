// -----------------------------------------------------------------------
// <copyright file="IConnect4Strategy.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyGames.Core.Strategies;

namespace MyGames.Connect4.Strategies;

public interface IConnect4Strategy : IStrategy<Connect4Game, Connect4Board, Connect4Move, Connect4Move>;
