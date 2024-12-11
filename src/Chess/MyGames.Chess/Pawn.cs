// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using MyGames.Domain;
using MyGames.Domain.Extensions;

namespace MyGames.Chess
{
    public class Pawn : ChessPiece
    {
        public Pawn(ChessColor color) : base(color) { }

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
                var forwardTwo = from + direction * 2;
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
}
