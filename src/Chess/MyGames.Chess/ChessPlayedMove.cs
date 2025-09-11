// -----------------------------------------------------------------------
// <copyright file="ChessPlayedMove.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyGames.Core;

namespace MyGames.Chess;

public sealed class ChessPlayedMove(ChessPiece piece,
                       BoardCoordinates start,
                       BoardCoordinates destination,
                       bool isPromotion,
                       bool isCastling,
                       bool enPassant,
                       ChessPiece? takenPiece,
                       ExchangePiece? exchangePiece) : IPlayedMove
{
    public ChessPiece Piece { get; } = piece;

    public BoardCoordinates Start { get; } = start;

    public BoardCoordinates Destination { get; } = destination;

    public bool IsPromotion { get; } = isPromotion;

    public bool IsCastling { get; } = isCastling;

    public bool EnPassant { get; } = enPassant;

    public ChessPiece? TakenPiece { get; } = takenPiece;

    public ExchangePiece? ExchangePiece { get; } = exchangePiece;

    public override string ToString()
        => IsPromotion ? $"Promote {Piece} to {ExchangePiece}"
            : IsCastling ? $"Castling {Piece} to ({Destination.Row}, {Destination.Column})"
            : EnPassant ? $"En passant {Piece} to ({Destination.Row}, {Destination.Column})"
            : $"Move {Piece} to ({Destination.Row}, {Destination.Column})";
}
