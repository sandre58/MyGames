// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using MyGames.Chess.Extensions;
using MyGames.Domain;

namespace MyGames.Chess
{
    public class Queen : ChessPiece
    {
        public Queen(ChessColor color) : base(color) { }

        protected override string GetNotation() => "♛";

        protected override IEnumerable<BoardCoordinates> GetPossibleMoves(ChessBoard board, BoardCoordinates from)
            => board.GetAllPossibleMoves(from, Color,
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
}
