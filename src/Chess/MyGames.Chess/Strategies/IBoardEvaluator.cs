// -----------------------------------------------------------------------
// <copyright file="IBoardEvaluator.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyGames.Chess.Strategies;

public interface IBoardEvaluator
{
    int Evaluate(ChessBoard board, ChessColor color);
}
