// -----------------------------------------------------------------------
// <copyright file="Knight.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using MyGames.Chess.Extensions;
using MyGames.Core;

namespace MyGames.Chess;

public sealed class Knight(ChessColor color) : ChessPiece(color)
{
    protected override string GetNotation() => "♞";

    protected override IEnumerable<BoardCoordinates> GetPossibleMoves(ChessBoard board, BoardCoordinates from)
        => board.GetAllPossibleMoves(
            from,
            Color,
            [
                new BoardDirectionOffset(new BoardDirection(-2, -1), 1), // Up Left
                new BoardDirectionOffset(new BoardDirection(-2, 1), 1), // Up Right
                new BoardDirectionOffset(new BoardDirection(2, -1), 1), // Down Left
                new BoardDirectionOffset(new BoardDirection(2, 1), 1), // Down Right
                new BoardDirectionOffset(new BoardDirection(-1, -2), 1), // Left Up
                new BoardDirectionOffset(new BoardDirection(1, -2), 1), // Left Down
                new BoardDirectionOffset(new BoardDirection(-1, 2), 1), // Right Up
                new BoardDirectionOffset(new BoardDirection(1, 2), 1), // Right Down
            ]);
}
