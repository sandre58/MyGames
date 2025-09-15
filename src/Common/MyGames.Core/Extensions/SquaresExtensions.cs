// -----------------------------------------------------------------------
// <copyright file="SquaresExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyGames.Core.Extensions;

public static class SquaresExtensions
{
    public static IEnumerable<Square<TPiece>> GetNotEmptySquares<TPiece>(this SquaresCollection<TPiece> squares)
        where TPiece : IPiece
        => squares.Where(x => !x.IsEmpty);

    public static IEnumerable<Square<TPiece>> GetEmptySquares<TPiece>(this SquaresCollection<TPiece> squares)
        where TPiece : IPiece
        => squares.Where(x => x.IsEmpty);

    public static IReadOnlyCollection<IReadOnlyCollection<Square<TPiece>>> GetNotEmptyConsecutives<TPiece>(this SquaresCollection<TPiece> squares, Func<TPiece, TPiece, bool> isSimilarPiece)
        where TPiece : IPiece
    {
        var result = new List<IReadOnlyCollection<Square<TPiece>>>();
        if (squares.Count == 0) return result;

        var currentConsecutiveElements = new List<Square<TPiece>>();

        if (!squares[0].IsEmpty)
            currentConsecutiveElements.Add(squares[0]);

        for (var i = 1; i < squares.Count; i++)
        {
            if (!squares[i].IsEmpty && !squares[i - 1].IsEmpty && isSimilarPiece(squares[i - 1].Piece, squares[i].Piece))
            {
                currentConsecutiveElements.Add(squares[i]);
            }
            else
            {
                if (currentConsecutiveElements.Count > 0)
                {
                    result.Add([.. currentConsecutiveElements]);
                    currentConsecutiveElements.Clear();
                }

                if (!squares[i].IsEmpty)
                    currentConsecutiveElements.Add(squares[i]);
            }
        }

        result.Add([.. currentConsecutiveElements]);
        return result;
    }

    public static IReadOnlyCollection<IReadOnlyCollection<Square<TPiece>>> GetConsecutives<TPiece>(this SquaresCollection<TPiece> squares, Func<Square<TPiece>, Square<TPiece>, bool> isSimilarSquare)
        where TPiece : IPiece
    {
        var result = new List<List<Square<TPiece>>>();
        if (squares.Count == 0) return result;

        var currentConsecutiveElements = new List<Square<TPiece>> { squares[0] };

        for (var i = 1; i < squares.Count; i++)
        {
            if (isSimilarSquare(squares[i - 1], squares[i]))
            {
                currentConsecutiveElements.Add(squares[i]);
            }
            else
            {
                if (currentConsecutiveElements.Count > 0)
                {
                    result.Add([.. currentConsecutiveElements]);
                    currentConsecutiveElements.Clear();
                }

                currentConsecutiveElements.Add(squares[i]);
            }
        }

        result.Add([.. currentConsecutiveElements]);
        return result;
    }
}
