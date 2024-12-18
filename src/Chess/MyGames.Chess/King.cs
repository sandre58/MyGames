﻿// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using MyGames.Chess.Extensions;
using MyGames.Domain;

namespace MyGames.Chess
{
    public class King : ChessPiece
    {
        public King(ChessColor color) : base(color) { }

        protected override string GetNotation() => "♚";

        protected override IEnumerable<BoardCoordinates> GetPossibleMoves(ChessBoard board, BoardCoordinates from) => board.GetAllPossibleMoves(from, Color,
            [
                new BoardDirectionOffset(BoardDirection.Up, 1),
                new BoardDirectionOffset(BoardDirection.Down, 1),
                new BoardDirectionOffset(BoardDirection.Left, 1),
                new BoardDirectionOffset(BoardDirection.Right, 1),
                new BoardDirectionOffset(BoardDirection.UpLeft, 1),
                new BoardDirectionOffset(BoardDirection.UpRight, 1),
                new BoardDirectionOffset(BoardDirection.DownLeft, 1),
                new BoardDirectionOffset(BoardDirection.DownRight, 1)
            ]);
    }
}
