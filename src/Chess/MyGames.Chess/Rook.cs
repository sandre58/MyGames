// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyGames.Chess.Extensions;
using MyGames.Domain;
using System.Collections.Generic;

namespace MyGames.Chess
{
    public class Rook : ChessPiece
    {
        public Rook(ChessColor color) : base(color) { }

        protected override string GetNotation() => "♜";

        protected override IEnumerable<BoardCoordinates> GetPossibleMoves(ChessBoard board, BoardCoordinates from)
            => board.GetAllPossibleMoves(from, Color,
            [
                new BoardDirectionOffset(BoardDirection.Up, board.Columns.Count),
                new BoardDirectionOffset(BoardDirection.Down, board.Columns.Count),
                new BoardDirectionOffset(BoardDirection.Left, board.Columns.Count),
                new BoardDirectionOffset(BoardDirection.Right, board.Columns.Count),
            ]);
    }
}
