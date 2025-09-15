// -----------------------------------------------------------------------
// <copyright file="Pawn.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using MyGames.Core;
using MyGames.Core.Extensions;

namespace MyGames.Chess;

public sealed class Pawn(ChessColor color) : ChessPiece(color)
{
    protected override string GetNotation() => "●";

    protected override IEnumerable<BoardCoordinates> GetPossibleMoves(ChessBoard board, BoardCoordinates from)
    {
        var possibleMoves = new List<BoardCoordinates>();
        var direction = Color == ChessColor.White ? BoardDirection.Up : BoardDirection.Down;
        var startRow = Color == ChessColor.White ? board.Rows.Count - 2 : 1;

        // Move forward by one
        var forwardOne = from + direction;
        if (board.Exists(forwardOne) && board.IsEmpty(forwardOne))
        {
            possibleMoves.Add(forwardOne);

            // Move forward by two if at starting position
            var forwardTwo = from + (direction * 2);
            if (from.Row == startRow && board.Exists(forwardTwo) && board.IsEmpty(forwardTwo))
                possibleMoves.Add(forwardTwo);
        }

        // Capture diagonally
        var captureLeft = from + direction + BoardDirection.Left;
        var captureRight = from + direction + BoardDirection.Right;

        if (board.Exists(captureLeft) && !board.IsEmpty(captureLeft) && board.GetPiece(captureLeft).Color != Color)
            possibleMoves.Add(captureLeft);

        if (board.Exists(captureRight) && !board.IsEmpty(captureRight) && board.GetPiece(captureRight).Color != Color)
            possibleMoves.Add(captureRight);

        return possibleMoves;
    }
}
