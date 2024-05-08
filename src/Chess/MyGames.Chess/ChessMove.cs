// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyGames.Common;

namespace MyGames.Chess
{
    public class ChessMove : IMove
    {
        public ChessMove(ChessPiece piece, int row, int column)
        {
            Piece = piece;
            Row = row;
            Column = column;
        }

        public ChessPiece Piece { get; }

        public int Row { get; }

        public int Column { get; }

        public override string ToString() => $"{Piece} to ({Row}, {Column})";
    }
}
