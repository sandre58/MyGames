// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MyGames.Domain;
using MyGames.Domain.Extensions;
using MyGames.Domain.Strategies;
using MyGames.Connect4.Extensions;

namespace MyGames.Connect4.Strategies
{
    public class Connect4AlphaBetaStrategy(Level level = Level.Medium) : IStrategy<Connect4Game, Connect4Board, Connect4Move, Connect4Move>
    {
        private readonly Level _level = level;

        public Connect4Move ProvideMove(Connect4Game game, IPlayer player)
        {
            var move = AlphaBetaHelper.ComputeBestMove<Connect4Game, Connect4Board, Connect4Move, Connect4Move>(game, (int)_level, x => GetValidMoves(x.Board.Columns), x => EvaluatePosition(x, player));

            return move ?? throw new InvalidOperationException("No move allowed");
        }

        /// <summary>
        /// Evaluation of the current position of the board.
        /// For example, an advantageous position may have a positive score, while a disadvantageous position may have a negative score.
        /// </summary>
        /// <param name="game">Game to evaluate</param>
        /// <param name="player">Player position to evaluate</param>
        /// <returns>Returns a score representing the player's advantage</returns>
        internal static int EvaluatePosition(Connect4Game game, IPlayer player)
        {
            var score = 0;

            foreach (var square in game.Board.GetNotEmptySquares().Reverse().ToList())
            {
                if (square.Piece.Player == player)
                    score += EvaluatePiece(game, square.Coordinates, player);
                else
                    score -= EvaluatePiece(game, square.Coordinates, square.Piece.Player);
            }

            return score;
        }

        private static int EvaluatePiece(Connect4Game game, BoardCoordinates coordinates, IPlayer player)
        {
            var score = 0;

            // Central column preference
            if (coordinates.Column == game.Board.Columns.Count / 2)
                score += 3 * game.NumberOfPiecesForWin;

            // Horizontal
            score += EvaluateDirection(game, coordinates, 0, 1, player);

            // Vertical
            score += EvaluateDirection(game, coordinates, 1, 0, player);

            // Diagonal /
            score += EvaluateDirection(game, coordinates, 1, 1, player);

            // Diagonal \
            score += EvaluateDirection(game, coordinates, 1, -1, player);

            return score;
        }

        private static int EvaluateDirection(Connect4Game game, BoardCoordinates coordinates, int deltaRow, int deltaColumn, IPlayer player)
        {
            var score = 0;
            var count = 0;
            var futureEmptyCount = 0;
            var nextEmptyCount = 0;

            for (var offset = 0; offset < game.NumberOfPiecesForWin; offset++)
            {
                var offsetCoordinates = new BoardCoordinates(coordinates.Row + offset * deltaRow, coordinates.Column + offset * deltaColumn);

                if (game.Board.Exists(offsetCoordinates))
                {
                    var currentColumn = game.Board.GetColumn(offsetCoordinates.Column);
                    var nextRow = currentColumn.GetNextRow();
                    var piece = game.Board.TryGetPiece(offsetCoordinates);
                    if (piece is null)
                    {
                        if (offsetCoordinates.Row == nextRow)
                            nextEmptyCount++;
                        else
                            futureEmptyCount++;
                    }
                    else if (piece.Player == player)
                        count++;
                    else
                    {
                        count = 0;
                        break;
                    }
                }
            }

            var allEmptyCount = futureEmptyCount + nextEmptyCount;

            // Winning Move
            if (count >= game.NumberOfPiecesForWin) score += 1000;

            // Potential Winning Move (close)
            else if (count == game.NumberOfPiecesForWin - 1 && nextEmptyCount >= 1) score += 200;

            // Potential Winning Move (future)
            else if (count == game.NumberOfPiecesForWin - 1 && allEmptyCount >= 1) score += 100;

            // Potential setup (close)
            else if (count + nextEmptyCount >= game.NumberOfPiecesForWin) score += count * 2;

            // Potential setup (future)
            else if (count + allEmptyCount >= game.NumberOfPiecesForWin) score += count;

            return score;
        }

        internal static List<Connect4Move> GetValidMoves(IReadOnlyCollection<SquaresColumn<Connect4Piece>> columns)
        {
            if (columns.Count == 0) return [];

            var middleIndex = (int)Math.Ceiling(columns.Count / 2.0);
            var leftPart = columns.Take(middleIndex).OrderByDescending(x => x.Index).ToList();
            var rightPart = columns.Skip(middleIndex).OrderBy(x => x.Index).ToList();

            var result = new List<SquaresColumn<Connect4Piece>>();
            var maxLength = Math.Max(leftPart.Count, rightPart.Count);

            for (var i = 0; i < maxLength; i++)
            {
                if (i < leftPart.Count) result.Add(leftPart[i]);
                if (i < rightPart.Count) result.Add(rightPart[i]);
            }

            return result.Where(x => !x.IsFull()).Select(x => new Connect4Move(x.Index)).ToList();
        }
    }
}
