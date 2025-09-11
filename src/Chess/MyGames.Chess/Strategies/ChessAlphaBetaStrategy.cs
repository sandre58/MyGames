// -----------------------------------------------------------------------
// <copyright file="ChessAlphaBetaStrategy.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using MyGames.Core;
using MyGames.Core.Strategies;
using MyNet.Utilities;

namespace MyGames.Chess.Strategies;

public class ChessAlphaBetaStrategy(IBoardEvaluator boardEvaluator, Level level = Level.Medium) : IStrategy<ChessGame, ChessBoard, IChessMove, ChessPlayedMove>
{
    private readonly Level _level = level;

    private readonly IBoardEvaluator _boardEvaluator = boardEvaluator;

    public IChessMove ProvideMove(ChessGame game, IPlayer player)
    {
        var move = AlphaBetaHelper.ComputeBestMove<ChessGame, ChessBoard, IChessMove, ChessPlayedMove>(game, (int)_level, x => GetValidMoves(x, x.CurrentPlayer), x => _boardEvaluator.Evaluate(x.Board, x.GetColor(player.CastIn<IChessPlayer>())));

        return move ?? throw new InvalidOperationException("No move allowed");
    }

    private static List<IChessMove> GetValidMoves(ChessGame game, IChessPlayer player)
        => [.. game.GetPieces(player).SelectMany(x =>
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
        })];
}
