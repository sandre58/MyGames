// -----------------------------------------------------------------------
// <copyright file="AlphaBetaHelper.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using MyNet.Utilities;

namespace MyGames.Core.Strategies;

public static class AlphaBetaHelper
{
    public static TMove? ComputeBestMove<TGame, TBoard, TMove, TPlayedMove>(TGame game, int depth, Func<TGame, IList<TMove>> validMoves, Func<TGame, int> evaluatePosition)
        where TGame : IGame<TBoard, TMove, TPlayedMove>
        where TBoard : IBoard
        where TMove : IMove<TBoard, TPlayedMove>
        where TPlayedMove : IPlayedMove
    {
        const int alpha = int.MinValue;
        const int beta = int.MaxValue;

        TMove? bestMove = default;
        var bestScore = int.MinValue;

        var clone = game.Clone().CastIn<TGame>();
        foreach (var move in validMoves(clone))
        {
            clone.MakeMove(move);
            var score = AlphaBeta<TGame, TBoard, TMove, TPlayedMove>(clone, depth - 1, alpha, beta, validMoves, evaluatePosition, false);

            // cancel move
            clone.Undo();

            if (score > bestScore)
            {
                bestScore = score;
                bestMove = move;
            }
        }

        return bestMove;
    }

    public static int AlphaBeta<TGame, TBoard, TMove, TPlayedMove>(TGame game,
                                                                   int depth,
                                                                   int alpha,
                                                                   int beta,
                                                                   Func<TGame, IList<TMove>> validMoves,
                                                                   Func<TGame, int> evaluatePosition,
                                                                   bool maximize)
        where TGame : IGame<TBoard, TMove, TPlayedMove>
        where TBoard : IBoard
        where TMove : IMove<TBoard, TPlayedMove>
        where TPlayedMove : IPlayedMove
    {
        if (depth == 0 || game.IsOver) return evaluatePosition(game);

        var moves = validMoves(game);

        if (maximize)
        {
            var eval = int.MinValue;

            foreach (var move in moves)
            {
                // Play move in clone
                game.MakeMove(move);

                // Recursive call to AlphaBeta for player
                eval = Math.Max(eval, AlphaBeta<TGame, TBoard, TMove, TPlayedMove>(game, depth - 1, alpha, beta, validMoves, evaluatePosition, false));

                // cancel move
                game.Undo();

                if (eval >= beta)
                    break;
                alpha = Math.Max(alpha, eval);
            }

            return eval;
        }
        else
        {
            var eval = int.MaxValue;
            foreach (var move in moves)
            {
                // Play move in clone
                _ = game.MakeMove(move);

                // Recursive call to AlphaBeta for opponent
                eval = Math.Min(eval, AlphaBeta<TGame, TBoard, TMove, TPlayedMove>(game, depth - 1, alpha, beta, validMoves, evaluatePosition, true));

                // cancel move
                game.Undo();

                if (alpha >= eval)
                    return eval;
                beta = Math.Min(beta, eval);
            }

            return eval;
        }
    }
}
