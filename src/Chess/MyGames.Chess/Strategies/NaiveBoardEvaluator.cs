// -----------------------------------------------------------------------
// <copyright file="NaiveBoardEvaluator.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using MyGames.Chess.Extensions;
using MyGames.Core;

namespace MyGames.Chess.Strategies;

public class NaiveBoardEvaluator : IBoardEvaluator
{
    /// <summary>
    /// Evaluation of the current position of the board.
    /// For example, an advantageous position may have a positive score, while a disadvantageous position may have a negative score.
    /// </summary>
    /// <param name="board">Game to evaluate.</param>
    /// <param name="color">Player position to evaluate.</param>
    /// <returns>Returns a score representing the player's advantage.</returns>
    public int Evaluate(ChessBoard board, ChessColor color)
    {
        var score = 0;

        // Piece values
        var pieceValues = new Dictionary<Type, int>
        {
            { typeof(Pawn), 100 },
            { typeof(Knight), 320 },
            { typeof(Bishop), 330 },
            { typeof(Rook), 500 },
            { typeof(Queen), 900 },
            { typeof(King), 20000 }
        };

        // Evaluate pieces on the board
        foreach (var piece in board.GetPieces(color))
            score += pieceValues[piece.GetType()];

        foreach (var piece in board.GetPieces(color))
            score -= pieceValues[piece.GetType()];

        // Additional factors
        score += EvaluatePieceActivity(board, color);
        score -= EvaluatePieceActivity(board, ChessBoard.GetOpponentColor(color));
        score += EvaluateKingSafety(board, color);
        score -= EvaluateKingSafety(board, ChessBoard.GetOpponentColor(color));
        score += EvaluateControlOfCenter(board, color);
        score -= EvaluateControlOfCenter(board, ChessBoard.GetOpponentColor(color));

        return score;
    }

    private static int EvaluateControlOfCenter(ChessBoard board, ChessColor color)
    {
        var centerSquares = new List<BoardCoordinates>
        {
            new(3, 3),
            new(3, 4),
            new(4, 3),
            new(4, 4)
        };

        return centerSquares.Count(x => board.IsAttackedBy(board.GetSquare(x), color)) * 5;
    }

    private static int EvaluateKingSafety(ChessBoard board, ChessColor color)
    {
        var safetyScore = 0;
        var king = board.GetPieces(color).King;
        var kingPosition = board.GetCoordinates(king);
        var surroundingSquares = new List<BoardCoordinates>
            {
                kingPosition + BoardDirection.Up,
                kingPosition + BoardDirection.Down,
                kingPosition + BoardDirection.Left,
                kingPosition + BoardDirection.Right,
                kingPosition + BoardDirection.Up + BoardDirection.Left,
                kingPosition + BoardDirection.Up + BoardDirection.Right,
                kingPosition + BoardDirection.Down + BoardDirection.Left,
                kingPosition + BoardDirection.Down + BoardDirection.Right
            };

        foreach (var coordinates in surroundingSquares)
        {
            if (board.TryGetSquare(coordinates) is Square<ChessPiece> square && board.IsAttackedBy(square, ChessBoard.GetOpponentColor(color)))
                safetyScore -= 10;
        }

        return safetyScore;
    }

    private static int EvaluatePieceActivity(ChessBoard board, ChessColor color)
    {
        var activityScore = 0;
        foreach (var piece in board.GetPieces(color))
            activityScore += piece.GetPossibleMoves(board).Count();
        return activityScore;
    }
}
