// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyGames.Domain;

namespace MyGames.Chess
{
    public interface IChessMove : IMove<ChessBoard, ChessPlayedMove>
    {
        ChessPiece Piece { get; }

        bool IsValid(ChessGame game);
    }
}
