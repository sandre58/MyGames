// -----------------------------------------------------------------------
// <copyright file="Rook.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using MyGames.Chess.Extensions;
using MyGames.Core;

namespace MyGames.Chess;

public sealed class Rook(ChessColor color) : ChessPiece(color)
{
    protected override string GetNotation() => "♜";

    protected override IEnumerable<BoardCoordinates> GetPossibleMoves(ChessBoard board, BoardCoordinates from)
        => board.GetAllPossibleMoves(
            from,
            Color,
            [
                new BoardDirectionOffset(BoardDirection.Up, board.Columns.Count),
                new BoardDirectionOffset(BoardDirection.Down, board.Columns.Count),
                new BoardDirectionOffset(BoardDirection.Left, board.Columns.Count),
                new BoardDirectionOffset(BoardDirection.Right, board.Columns.Count),
            ]);
}
