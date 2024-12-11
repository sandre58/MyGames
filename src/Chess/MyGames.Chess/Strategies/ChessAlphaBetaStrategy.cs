// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using MyGames.Chess.Extensions;
using MyGames.Domain;
using MyGames.Domain.Strategies;
using MyNet.Utilities;

namespace MyGames.Chess.Strategies
{
    public class ChessAlphaBetaStrategy(Level level = Level.Medium) : IStrategy<ChessGame, ChessBoard, IChessMove, ChessPlayedMove>
    {
        private readonly Level _level = level;

        public IChessMove ProvideMove(ChessGame game, IPlayer player)
        {
            var move = AlphaBetaHelper.ComputeBestMove<ChessGame, ChessBoard, IChessMove, ChessPlayedMove>(game, (int)_level, x => GetValidMoves(x, x.GetCurrentPlayer()), x => EvaluatePosition(x, player.CastIn<IChessPlayer>()));

            return move ?? throw new InvalidOperationException("No move allowed");
        }

        /// <summary>
        /// Evaluation of the current position of the board.
        /// For example, an advantageous position may have a positive score, while a disadvantageous position may have a negative score.
        /// </summary>
        /// <param name="game">Game to evaluate</param>
        /// <param name="player">Player position to evaluate</param>
        /// <returns>Returns a score representing the player's advantage</returns>
        private static int EvaluatePosition(ChessGame game, IChessPlayer player)
        {
            var score = 0;

            // Piece values
            var pieceValues = new Dictionary<Type, int>
            {
                { typeof(Pawn), 100 },
                { typeof(Knight), 320 },
                { typeof(Bishop), 330 },
                { typeof(Rook), 500 },
                { typeof(Queen), 900 },
                { typeof(King), 20000 }
            };

            // Evaluate pieces on the board
            foreach (var piece in game.GetPieces(player))
                score += pieceValues[piece.GetType()];

            foreach (var piece in game.GetPieces(player))
                score -= pieceValues[piece.GetType()];

            // Additional factors
            score += EvaluatePieceActivity(game, player);
            score -= EvaluatePieceActivity(game, game.GetOpponent(player));
            score += EvaluateKingSafety(game, player);
            score -= EvaluateKingSafety(game, game.GetOpponent(player));
            score += EvaluateControlOfCenter(game, player);
            score -= EvaluateControlOfCenter(game, game.GetOpponent(player));

            return score;
        }

        private static int EvaluateKingSafety(ChessGame game, IChessPlayer player)
        {
            var safetyScore = 0;
            var king = game.GetPieces(player).King;
            var kingPosition = game.Board.GetCoordinates(king);
            var surroundingSquares = new List<BoardCoordinates>
                {
                    kingPosition + BoardDirection.Up,
                    kingPosition + BoardDirection.Down,
                    kingPosition + BoardDirection.Left,
                    kingPosition + BoardDirection.Right,
                    kingPosition + BoardDirection.Up + BoardDirection.Left,
                    kingPosition + BoardDirection.Up + BoardDirection.Right,
                    kingPosition + BoardDirection.Down + BoardDirection.Left,
                    kingPosition + BoardDirection.Down + BoardDirection.Right
                };

            foreach (var coordinates in surroundingSquares)
            {
                if (game.Board.TryGetSquare(coordinates) is Square<ChessPiece> square && game.Board.IsAttackedBy(square, game.GetOpponentColor(player)))
                    safetyScore -= 10;
            }
            return safetyScore;
        }

        private static int EvaluateControlOfCenter(ChessGame game, IChessPlayer player)
        {
            var centerSquares = new List<BoardCoordinates>
            {
                new(3, 3),
                new(3, 4),
                new(4, 3),
                new(4, 4)
            };

            return centerSquares.Count(x => game.Board.IsAttackedBy(game.Board.GetSquare(x), game.GetColor(player))) * 5;
        }

        private static int EvaluatePieceActivity(ChessGame game, IChessPlayer player)
        {
            var activityScore = 0;
            foreach (var piece in game.GetPieces(player))
                activityScore += piece.GetPossibleMoves(game.Board).Count();
            return activityScore;
        }

        private static List<IChessMove> GetValidMoves(ChessGame game, IChessPlayer player)
            => game.GetPieces(player).SelectMany(x =>
            {
                var chessMoves = x.GetPossibleMoves(game.Board).Select(y => (IChessMove)new ChessMove(x, y)).Where(y => y.IsValid(game)).ToList();

                if (x is King king)
                {
                    var shortCastling = CastlingMove.Short(king);
                    var longCastling = CastlingMove.Long(king);

                    if (shortCastling.IsValid(game))
                        chessMoves.Add(shortCastling);
                    if (longCastling.IsValid(game))
                        chessMoves.Add(longCastling);
                }

                if (x is Pawn pawn1)
                {
                    var coordinates = game.Board.GetCoordinates(pawn1);
                    var direction = pawn1.Color == ChessColor.White ? BoardDirection.Up : BoardDirection.Down;
                    var captureLeft = coordinates + direction + BoardDirection.Left;
                    var captureRight = coordinates + direction + BoardDirection.Right;
                    var enPassantLeft = new EnPassantCaptureMove(pawn1, captureLeft);
                    var enPassantRight = new EnPassantCaptureMove(pawn1, captureRight);
                    if (enPassantLeft.IsValid(game))
                        chessMoves.Add(enPassantLeft);
                    if (enPassantRight.IsValid(game))
                        chessMoves.Add(enPassantRight);
                }

                return chessMoves;
            }).ToList();
    }
}
