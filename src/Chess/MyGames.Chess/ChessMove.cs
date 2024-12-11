// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using MyGames.Chess.Exceptions;
using MyGames.Chess.Extensions;
using MyGames.Domain;
using MyGames.Domain.Extensions;

namespace MyGames.Chess
{
    public class ChessMove : IChessMove
    {
        public ChessMove(ChessPiece piece, BoardCoordinates destination)
        {
            Piece = piece;
            Destination = destination;
        }

        public ChessPiece Piece { get; }

        public BoardCoordinates Destination { get; }

        public virtual ChessPlayedMove Apply(ChessBoard board, IPlayer player)
        {
            var start = board.GetCoordinates(Piece);
            var takenPiece = board.TryGetPiece(Destination);

            return board.Move(Piece, Destination)
                ? new ChessPlayedMove(Piece, start, Destination, false, false, false, takenPiece, null)
                : throw new ChessInvalidMoveException(player, this, board.IsCheck(Piece.Color));
        }

        public virtual bool IsValid(ChessGame game) => game.Board.CanMove(Piece, Destination) && !game.Board.IsCheckAfterMove(this);

        [ExcludeFromCodeCoverage]
        public override string ToString() => $"Move {Piece} to ({Destination.Row}, {Destination.Column})";
    }
}
