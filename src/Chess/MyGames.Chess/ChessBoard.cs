// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Utilities.Helpers;
using MyGames.Common;

namespace MyGames.Chess
{
    public class ChessBoard : Board<ChessPiece>
    {
        public ChessBoard() : base(8, 8)
        {
            // White
            InsertPiece(new Rook(ChessColor.White), 0, 0);
            InsertPiece(new Knight(ChessColor.White), 1, 0);
            InsertPiece(new Bishop(ChessColor.White), 2, 0);
            InsertPiece(new Queen(ChessColor.White), 3, 0);
            InsertPiece(new King(ChessColor.White), 4, 0);
            InsertPiece(new Bishop(ChessColor.White), 5, 0);
            InsertPiece(new Knight(ChessColor.White), 6, 0);
            InsertPiece(new Rook(ChessColor.White), 7, 0);
            EnumerableHelper.Iteration(8, x => InsertPiece(new Pawn(ChessColor.White), x, 1));

            // Black
            InsertPiece(new Rook(ChessColor.Black), 0, 7);
            InsertPiece(new Knight(ChessColor.Black), 1, 7);
            InsertPiece(new Bishop(ChessColor.Black), 2, 7);
            InsertPiece(new Queen(ChessColor.Black), 3, 7);
            InsertPiece(new King(ChessColor.Black), 4, 7);
            InsertPiece(new Bishop(ChessColor.Black), 5, 7);
            InsertPiece(new Knight(ChessColor.Black), 6, 7);
            InsertPiece(new Rook(ChessColor.Black), 7, 7);
            EnumerableHelper.Iteration(8, x => InsertPiece(new Pawn(ChessColor.Black), x, 6));
        }

        internal bool Replace(ChessPiece oldPiece, ChessPiece newPiece)
        {
            var coordinates = FindPiece(oldPiece);

            return coordinates.HasValue && InsertPiece(newPiece, coordinates.Value, true);
        }

        internal bool Move(ChessPiece piece, int row, int column) => base.MovePiece(piece, row, column);

        protected override Board<ChessPiece> NewInstance() => new ChessBoard();
    }
}
