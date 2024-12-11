// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using MyGames.Chess.Extensions;
using MyGames.Domain;

namespace MyGames.Chess
{
    public class Bishop : ChessPiece
    {
        public Bishop(ChessColor color) : base(color) { }

        protected override string GetNotation() => "♝";

        protected override IEnumerable<BoardCoordinates> GetPossibleMoves(ChessBoard board, BoardCoordinates from)
            => board.GetAllPossibleMoves(from, Color,
            [
                new BoardDirectionOffset(BoardDirection.UpLeft, board.Columns.Count),
                new BoardDirectionOffset(BoardDirection.UpRight, board.Columns.Count),
                new BoardDirectionOffset(BoardDirection.DownLeft, board.Columns.Count),
                new BoardDirectionOffset(BoardDirection.DownRight, board.Columns.Count)
            ]);
    }
}
