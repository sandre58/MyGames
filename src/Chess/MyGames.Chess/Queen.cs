// -----------------------------------------------------------------------
// <copyright file="Queen.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using MyGames.Chess.Extensions;
using MyGames.Core;

namespace MyGames.Chess;

public sealed class Queen(ChessColor color) : ChessPiece(color)
{
    protected override string GetNotation() => "♛";

    protected override IEnumerable<BoardCoordinates> GetPossibleMoves(ChessBoard board, BoardCoordinates from)
        => board.GetAllPossibleMoves(
            from,
            Color,
            [
                new BoardDirectionOffset(BoardDirection.Up, board.Columns.Count), // Up
                new BoardDirectionOffset(BoardDirection.Down, board.Columns.Count), // Down
                new BoardDirectionOffset(BoardDirection.Left, board.Columns.Count), // Left
                new BoardDirectionOffset(BoardDirection.Right, board.Columns.Count), // Right
                new BoardDirectionOffset(BoardDirection.UpLeft, board.Columns.Count), // Up left
                new BoardDirectionOffset(BoardDirection.UpRight, board.Columns.Count), // Up right
                new BoardDirectionOffset(BoardDirection.DownLeft, board.Columns.Count), // Down left
                new BoardDirectionOffset(BoardDirection.DownRight, board.Columns.Count) // Down right
            ]);
}
