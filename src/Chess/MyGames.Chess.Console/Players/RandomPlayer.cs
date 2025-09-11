// -----------------------------------------------------------------------
// <copyright file="RandomPlayer.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using MyGames.Core;
using MyNet.Utilities.Generator;

namespace MyGames.Chess.Console.Players;

internal sealed class RandomPlayer(string name, string color) : ConsolePlayer(name, color)
{
    public override IChessMove NextMove(ChessGame game)
    {
        var pieces = game.GetPieces(this).ToDictionary(x => x, x =>
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
        });

        var availablePiece = RandomGenerator.ListItem(pieces.Where(x => x.Value.Count != 0).ToList());
        var move = RandomGenerator.ListItem(availablePiece.Value.ToList());

        return move is ChessMove chessMove && chessMove.Piece is Pawn pawn && ChessBoard.IsEnd(chessMove.Destination, game.GetColor(this))
            ? new PromotePawnMove(pawn, chessMove.Destination, RandomGenerator.Enum<ExchangePiece>())
            : move;
    }
}
