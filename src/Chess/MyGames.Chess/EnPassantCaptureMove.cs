// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MyGames.Chess.Exceptions;
using MyGames.Chess.Extensions;
using MyGames.Domain;
using MyGames.Domain.Extensions;

namespace MyGames.Chess
{
    public class EnPassantCaptureMove(Pawn pawn, BoardCoordinates destination) : ChessMove(pawn, destination)
    {
        public override ChessPlayedMove Apply(ChessBoard board, IPlayer player)
        {
            var start = board.GetCoordinates(Piece);
            var captureCoordinates = new BoardCoordinates(Piece.Color == ChessColor.White ? Destination.Row + 1 : Destination.Row - 1, Destination.Column);

            // Remove the captured pawn
            return board.TryGetPiece(captureCoordinates) is Pawn p && board.Move(Piece, Destination) && board.Remove(p)
                ? new ChessPlayedMove(Piece, start, Destination, false, false, true, p, null)
                : throw new ChessInvalidMoveException(player, this, board.IsCheck(Piece.Color));
        }

        public override bool IsValid(ChessGame game)
        {
            // Validate that the move is an en passant capture
            var captureCoordinates = new BoardCoordinates(Piece.Color == ChessColor.White ? Destination.Row + 1 : Destination.Row - 1, Destination.Column);
            var capturedPiece = game.Board.TryGetPiece(captureCoordinates);
            if (!game.Board.Exists(captureCoordinates) || capturedPiece is not Pawn || capturedPiece.Color == Piece.Color)
                return false;

            // Validate that the last move was a double pawn move
            var lastMove = game.History.LastOrDefault();
            if (lastMove is null || lastMove.Move.Piece is not Pawn || Math.Abs(lastMove.Move.Start.GetDirection(lastMove.Move.Destination).Row) != 2)
                return false;

            // Ensure the move does not put the player in check
            return !game.Board.IsCheckAfterMove(this);
        }

        [ExcludeFromCodeCoverage]
        public override string ToString() => $"En passant {Piece} to ({Destination.Row}, {Destination.Column})";
    }
}
