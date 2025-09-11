// -----------------------------------------------------------------------
// <copyright file="EnPassantCaptureMove.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using MyGames.Chess.Exceptions;
using MyGames.Chess.Extensions;
using MyGames.Core;
using MyGames.Core.Extensions;

namespace MyGames.Chess;

public sealed class EnPassantCaptureMove(Pawn pawn, BoardCoordinates destination) : ChessMove(pawn, destination)
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
        if (game.History.Count == 0 || game.History[^1] is not HistoryMove<IChessPlayer, ChessBoard, IChessMove, ChessPlayedMove> lastMove || lastMove.Move.Piece is not Pawn || Math.Abs(lastMove.Move.Start.GetDirection(lastMove.Move.Destination).Row) != 2)
            return false;

        // Ensure the move does not put the player in check
        return !game.Board.IsCheckAfterMove(this);
    }

    public override string ToString() => $"En passant {Piece} to ({Destination.Row}, {Destination.Column})";
}
