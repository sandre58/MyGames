// -----------------------------------------------------------------------
// <copyright file="BoardExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace MyGames.Core.Extensions;

public static class BoardExtensions
{
    public static TPiece GetPiece<TPiece>(this Board<TPiece> board, BoardCoordinates coordinates)
        where TPiece : IPiece
        => board.GetSquare(coordinates).Piece;

    public static TPiece GetPiece<TPiece>(this Board<TPiece> board, int row, int column)
        where TPiece : IPiece
        => board.GetPiece(new(row, column));

    public static TPiece? TryGetPiece<TPiece>(this Board<TPiece> board, BoardCoordinates coordinates)
        where TPiece : IPiece
        => board.TryGetSquare(coordinates) is Square<TPiece> square && !square.IsEmpty ? square.Piece : default;

    public static TPiece? TryGetPiece<TPiece>(this Board<TPiece> board, int row, int column)
        where TPiece : IPiece
        => board.TryGetPiece(new(row, column));

    public static IEnumerable<Square<TPiece>> GetNotEmptySquares<TPiece>(this Board<TPiece> board)
        where TPiece : IPiece
        => board.Squares.Where(x => !x.IsEmpty);

    public static IEnumerable<Square<TPiece>> GetEmptySquares<TPiece>(this Board<TPiece> board)
        where TPiece : IPiece
        => board.Squares.Where(x => x.IsEmpty);

    public static SquaresCollection<TPiece> GetSquaresBetween<TPiece>(this Board<TPiece> board, BoardCoordinates start, BoardCoordinates end, bool includeStart = true, bool includeEnd = true)
        where TPiece : IPiece
    {
        var path = new List<Square<TPiece>>();

        var rowDirection = end.Row > start.Row ? 1 : (end.Row < start.Row ? -1 : 0);
        var columnDirection = end.Column > start.Column ? 1 : (end.Column < start.Column ? -1 : 0);
        var direction = new BoardDirection(rowDirection, columnDirection);

        if (includeStart)
            path.Add(board.GetSquare(start));

        var current = start + direction;

        while (current.Row != end.Row || current.Column != end.Column)
        {
            path.Add(board.GetSquare(current));

            current += direction;
        }

        if (includeEnd)
            path.Add(board.GetSquare(end));

        return new SquaresCollection<TPiece>(path);
    }
}
