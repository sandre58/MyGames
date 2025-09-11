// -----------------------------------------------------------------------
// <copyright file="MovesExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyGames.Core;

namespace MyGames.Chess.Extensions;

public static class MovesExtensions
{
    public static bool Advance(this ChessGame game, Pawn pawn)
    {
        var direction = pawn.Color == ChessColor.White ? BoardDirection.Up : BoardDirection.Down;
        var destination = game.Board.GetCoordinates(pawn) + direction;

        return game.MakeMove(new ChessMove(pawn, destination));
    }

    public static bool Promote(this ChessGame game, Pawn pawn, BoardDirection direction, ExchangePiece exchangePiece)
    {
        var destination = game.Board.GetCoordinates(pawn) + direction;

        return game.MakeMove(new PromotePawnMove(pawn, destination, exchangePiece));
    }

    public static bool Move(this ChessGame game, ChessPiece piece, BoardDirection direction)
    {
        var destination = game.Board.GetCoordinates(piece) + direction;

        return game.MakeMove(new ChessMove(piece, destination));
    }
}
