// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using MyGames.Domain;

namespace MyGames.Chess
{
    public class ChessPlayedMove : IPlayedMove
    {
        public ChessPlayedMove(ChessPiece piece,
                               BoardCoordinates start,
                               BoardCoordinates destination,
                               bool isPromotion,
                               bool isCastling,
                               bool enPassant,
                               ChessPiece? takenPiece,
                               ExchangePiece? exchangePiece)
        {
            Piece = piece;
            Start = start;
            Destination = destination;
            IsPromotion = isPromotion;
            IsCastling = isCastling;
            EnPassant = enPassant;
            TakenPiece = takenPiece;
            ExchangePiece = exchangePiece;
        }

        public ChessPiece Piece { get; }

        public BoardCoordinates Start { get; }

        public BoardCoordinates Destination { get; }

        public bool IsPromotion { get; }

        public bool IsCastling { get; }

        public bool EnPassant { get; }

        public ChessPiece? TakenPiece { get; }

        public ExchangePiece? ExchangePiece { get; }

        [ExcludeFromCodeCoverage]
        public override string ToString()
            => IsPromotion ? $"Promote {Piece} to {ExchangePiece}"
                : IsCastling ? $"Castling {Piece} to ({Destination.Row}, {Destination.Column})"
                : EnPassant ? $"En passant {Piece} to ({Destination.Row}, {Destination.Column})"
                : $"Move {Piece} to ({Destination.Row}, {Destination.Column})";
    }
}
