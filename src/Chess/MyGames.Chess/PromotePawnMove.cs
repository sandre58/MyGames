// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using MyGames.Chess.Exceptions;
using MyGames.Chess.Extensions;
using MyGames.Domain;

namespace MyGames.Chess
{
    public class PromotePawnMove(Pawn pawn, BoardCoordinates destination, ExchangePiece exchangePiece) : ChessMove(pawn, destination)
    {
        public ExchangePiece ExchangePiece { get; } = exchangePiece;

        public override ChessPlayedMove Apply(ChessBoard board, IPlayer player)
        {
            var start = board.GetCoordinates(Piece);
            return board.Move(Piece, Destination) && board.Replace(Piece, CreateReplacementPiece(ExchangePiece, Piece.Color))
                ? new ChessPlayedMove(Piece, start, Destination, true, false, false, null, ExchangePiece)
                : throw new ChessInvalidMoveException(player, this, board.IsCheck(Piece.Color));
        }

        public override bool IsValid(ChessGame game) => base.IsValid(game) && ChessBoard.IsEnd(Destination, Piece.Color) && !game.Board.IsCheckAfterMove(this);

        internal static ChessPiece CreateReplacementPiece(ExchangePiece piece, ChessColor color) => piece switch
        {
            ExchangePiece.Queen => new Queen(color),
            ExchangePiece.Rook => new Rook(color),
            ExchangePiece.Bishop => new Bishop(color),
            ExchangePiece.Knight => new Knight(color),
            _ => throw new NotSupportedException()
        };

        [ExcludeFromCodeCoverage]
        public override string ToString() => $"Promote {Piece} to {ExchangePiece}";
    }
}
