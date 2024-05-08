// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyGames.Common;

namespace MyGames.Chess
{
    public class ChessPiece : IPiece
    {
        public ChessPiece(ChessColor color) => Color = color;

        public ChessColor Color { get; }
    }
}
