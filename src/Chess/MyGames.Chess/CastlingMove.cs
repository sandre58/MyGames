// -----------------------------------------------------------------------
// <copyright file="CastlingMove.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using MyGames.Chess.Exceptions;
using MyGames.Chess.Extensions;
using MyGames.Core;
using MyGames.Core.Extensions;

namespace MyGames.Chess;

public sealed class CastlingMove : IChessMove
{
    public static CastlingMove Short(King king) => new(king, 4, 6, 7, 5);

    public static CastlingMove Long(King king) => new(king, 4, 2, 0, 3);

    private CastlingMove(King king, int kingStartColumn, int kingEndColumn, int rookStartColumn, int rookEndColumn)
    {
        King = king;

        var row = ChessBoard.GetStartRowOf(king.Color);
        KingStartCoordinates = new(row, kingStartColumn);
        KingEndCoordinates = new(row, kingEndColumn);
        RookStartCoordinates = new(row, rookStartColumn);
        RookEndCoordinates = new(row, rookEndColumn);
    }

    public ChessPiece King { get; }

    public BoardCoordinates KingStartCoordinates { get; }

    public BoardCoordinates KingEndCoordinates { get; }

    public BoardCoordinates RookStartCoordinates { get; }

    public BoardCoordinates RookEndCoordinates { get; }

    ChessPiece IChessMove.Piece => King;

    public ChessPlayedMove Apply(ChessBoard board, IPlayer player)
    {
        var rookSquare = board.GetSquare(RookStartCoordinates);
        var rook = !rookSquare.IsEmpty && rookSquare.Piece is Rook rookPiece && rookPiece.Color == King.Color ? rookPiece : null;
        return rook is not null && board.Move(King, KingEndCoordinates) && board.Move(rook, RookEndCoordinates)
            ? new ChessPlayedMove(King, KingStartCoordinates, KingEndCoordinates, false, true, false, null, null)
            : throw new ChessInvalidMoveException(player, this, board.IsCheck(King.Color));
    }

    public bool IsValid(ChessGame game)
    {
        var rookSquare = game.Board.GetSquare(RookStartCoordinates);
        var rook = !rookSquare.IsEmpty && rookSquare.Piece is Rook rookPiece && rookPiece.Color == King.Color ? rookPiece : null;

        // Check if the king or rook have moved
        if (rook is null || game.HasMoved(King) || game.HasMoved(rook))
            return false;

        // Check if there are pieces between the king and the rook
        var path = game.Board.GetSquaresBetween(KingStartCoordinates, RookStartCoordinates, false, false);
        if (!path.IsEmpty())
            return false;

        // Check if the king is in check, or will pass through or land on a square that is attacked
        var kingPath = game.Board.GetSquaresBetween(KingStartCoordinates, KingEndCoordinates);
        return !kingPath.Any(x => game.Board.IsAttackedBy(x, ChessBoard.GetOpponentColor(King.Color)));
    }

    public override string ToString() => $"{King} castle to {KingEndCoordinates}";
}
