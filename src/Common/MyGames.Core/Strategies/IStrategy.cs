// -----------------------------------------------------------------------
// <copyright file="IStrategy.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyGames.Core.Strategies;

public interface IStrategy<TGame, TBoard, TMove, TPlayedMove>
    where TGame : IGame<TBoard, TMove, TPlayedMove>
    where TBoard : IBoard
    where TMove : IMove<TBoard, TPlayedMove>
    where TPlayedMove : IPlayedMove
{
    TMove ProvideMove(TGame game, IPlayer player);
}
