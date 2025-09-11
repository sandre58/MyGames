// -----------------------------------------------------------------------
// <copyright file="Bishop.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using MyGames.Chess.Extensions;
using MyGames.Core;

namespace MyGames.Chess;

public sealed class Bishop(ChessColor color) : ChessPiece(color)
{
    protected override string GetNotation() => "♝";

    protected override IEnumerable<BoardCoordinates> GetPossibleMoves(ChessBoard board, BoardCoordinates from)
        => board.GetAllPossibleMoves(
            from,
            Color,
            [
                new BoardDirectionOffset(BoardDirection.UpLeft, board.Columns.Count),
                new BoardDirectionOffset(BoardDirection.UpRight, board.Columns.Count),
                new BoardDirectionOffset(BoardDirection.DownLeft, board.Columns.Count),
                new BoardDirectionOffset(BoardDirection.DownRight, board.Columns.Count)
            ]);
}
