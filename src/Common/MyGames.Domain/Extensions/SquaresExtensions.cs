// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace MyGames.Domain.Extensions
{
    public static class SquaresExtensions
    {
        public static IEnumerable<Square<TPiece>> GetNotEmptySquares<TPiece>(this SquaresCollection<TPiece> squares) where TPiece : IPiece => squares.Where(x => !x.IsEmpty);

        public static IEnumerable<Square<TPiece>> GetEmptySquares<TPiece>(this SquaresCollection<TPiece> squares) where TPiece : IPiece => squares.Where(x => x.IsEmpty);

        public static List<List<Square<TPiece>>> GetNotEmptyConsecutives<TPiece>(this SquaresCollection<TPiece> squares, Func<TPiece, TPiece, bool> isSimilarPiece) where TPiece : IPiece
        {
            var result = new List<List<Square<TPiece>>>();
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
                        result.Add(new List<Square<TPiece>>(currentConsecutiveElements));
                        currentConsecutiveElements.Clear();
                    }

                    if (!squares[i].IsEmpty)
                        currentConsecutiveElements.Add(squares[i]);
                }
            }

            result.Add(new List<Square<TPiece>>(currentConsecutiveElements));
            return result;
        }

        public static List<List<Square<TPiece>>> GetConsecutives<TPiece>(this SquaresCollection<TPiece> squares, Func<Square<TPiece>, Square<TPiece>, bool> isSimilarSquare) where TPiece : IPiece
        {
            var result = new List<List<Square<TPiece>>>();
            if (squares.Count == 0) return result;

            var currentConsecutiveElements = new List<Square<TPiece>> { squares[0] };

            for (var i = 1; i < squares.Count; i++)
            {
                if (isSimilarSquare(squares[i - 1], squares[i]))
                    currentConsecutiveElements.Add(squares[i]);
                else
                {
                    if (currentConsecutiveElements.Count > 0)
                    {
                        result.Add(new List<Square<TPiece>>(currentConsecutiveElements));
                        currentConsecutiveElements.Clear();
                    }

                    currentConsecutiveElements.Add(squares[i]);
                }
            }

            result.Add(new List<Square<TPiece>>(currentConsecutiveElements));
            return result;
        }
    }
}
