// -----------------------------------------------------------------------
// <copyright file="ChessMove.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyGames.Chess.Exceptions;
using MyGames.Chess.Extensions;
using MyGames.Core;
using MyGames.Core.Extensions;

namespace MyGames.Chess;

public class ChessMove(ChessPiece piece, BoardCoordinates destination) : IChessMove
{
    public ChessPiece Piece { get; } = piece;

    public BoardCoordinates Destination { get; } = destination;

    public virtual ChessPlayedMove Apply(ChessBoard board, IPlayer player)
    {
        var start = board.GetCoordinates(Piece);
        var takenPiece = board.TryGetPiece(Destination);

        return board.Move(Piece, Destination)
            ? new ChessPlayedMove(Piece, start, Destination, false, false, false, takenPiece, null)
            : throw new ChessInvalidMoveException(player, this, board.IsCheck(Piece.Color));
    }

    public virtual bool IsValid(ChessGame game) => game.Board.CanMove(Piece, Destination) && !game.Board.IsCheckAfterMove(this);

    public override string ToString() => $"Move {Piece} to ({Destination.Row}, {Destination.Column})";
}
