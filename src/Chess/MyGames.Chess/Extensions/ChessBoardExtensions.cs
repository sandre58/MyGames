// -----------------------------------------------------------------------
// <copyright file="ChessBoardExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using MyGames.Core;
using MyNet.Utilities;

namespace MyGames.Chess.Extensions;

public static class ChessBoardExtensions
{
    public static IEnumerable<BoardCoordinates> GetAllPossibleMoves(this ChessBoard board, BoardCoordinates from, ChessColor pieceColor, BoardDirectionOffset[] directions)
    {
        var possibleMoves = new List<BoardCoordinates>();

        foreach (var (direction, multiplier) in directions)
        {
            var count = 1;
            var coordinates = from + direction;
            while (count <= multiplier && board.Exists(coordinates))
            {
                var square = board.GetSquare(coordinates);
                if (square.IsEmpty)
                {
                    possibleMoves.Add(coordinates);
                }
                else
                {
                    if (square.Piece.Color != pieceColor)
                        possibleMoves.Add(coordinates);
                    break;
                }

                coordinates += direction;
                count++;
            }
        }

        return possibleMoves;
    }

    public static bool CanMove(this ChessBoard board, ChessPiece piece, BoardCoordinates coordinates)
        => piece.GetPossibleMoves(board).Contains(coordinates);

    public static bool CanMove(this ChessBoard board, ChessPiece piece, BoardDirection direction)
        => board.TryGetCoordinates(piece) is BoardCoordinates coordinates && CanMove(board, piece, coordinates + direction);

    public static bool IsCheckAfterMove(this ChessBoard board, IChessMove move)
    {
        // Clone the board to simulate the move
        var simulatedBoard = board.Clone().CastIn<ChessBoard>();
        move.Apply(simulatedBoard, null!);

        return simulatedBoard.IsCheck(move.Piece.Color);
    }

    public static bool MakeCheckAfterMove(this ChessBoard board, IChessMove move)
    {
        // Clone the board to simulate the move
        var simulatedBoard = board.Clone().CastIn<ChessBoard>();
        move.Apply(simulatedBoard, null!);

        return simulatedBoard.IsCheck(ChessBoard.GetOpponentColor(move.Piece.Color));
    }

    public static bool IsAttackedBy(this ChessBoard board, Square<ChessPiece> square, ChessColor color) =>
        board.GetPieces(color).Any(x => x.GetPossibleMoves(board).Contains(square.Coordinates));

    public static bool IsAttacked(this ChessBoard board, ChessPiece piece) =>
        board.Exists(piece) && board.GetPieces(ChessBoard.GetOpponentColor(piece.Color)).Any(x => x.GetPossibleMoves(board).Contains(board.GetSquare(piece).Coordinates));
}
