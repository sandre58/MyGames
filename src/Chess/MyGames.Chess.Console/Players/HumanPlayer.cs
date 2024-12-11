// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MyGames.Domain;
using Spectre.Console;

namespace MyGames.Chess.Console.Players
{
    internal class HumanPlayer(string name, string color) : ConsolePlayer(name, color)
    {
        private sealed record PieceChoice(ChessPiece Piece, BoardCoordinates Coordinates)
        {
            [ExcludeFromCodeCoverage]
            public override string ToString() => $"{ConsoleHelper.WritePiece(Piece)} in {ConsoleHelper.WriteCoordinates(Coordinates)}";
        }

        private sealed record MoveChoice(IChessMove Move)
        {
            [ExcludeFromCodeCoverage]
            public override string ToString() => Move switch
            {
                CastlingMove castlingMove => ConsoleHelper.WriteCoordinates(castlingMove.KingEndCoordinates),
                ChessMove chessMove => ConsoleHelper.WriteCoordinates(chessMove.Destination),
                _ => string.Empty
            };
        }

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
            var availablePieces = pieces.Where(x => x.Value.Count != 0).ToList();
            var pieceChoice = AnsiConsole.Prompt(new SelectionPrompt<PieceChoice>().Title($"[bold {Color}]{DisplayName}[/] : Choose a piece to move ").AddChoices(availablePieces.Select(x => new PieceChoice(x.Key, game.Board.GetSquare(x.Key).Coordinates))));
            var moveChoice = AnsiConsole.Prompt(new SelectionPrompt<MoveChoice>().Title($"[bold {Color}]{DisplayName}[/] : Choose a cell to play ").AddChoices(pieces[pieceChoice.Piece].Select(x => new MoveChoice(x))));

            if (pieceChoice.Piece is Pawn pawn && moveChoice.Move is ChessMove chessMove && ChessBoard.IsEnd(chessMove.Destination, game.GetColor(this)))
            {
                var pieceReplacement = AnsiConsole.Prompt(new SelectionPrompt<ExchangePiece>().Title($"[bold {Color}]{DisplayName}[/] : Choose a new piece ").AddChoices(Enum.GetValues<ExchangePiece>()));
                return new PromotePawnMove(pawn, chessMove.Destination, pieceReplacement);
            }

            return moveChoice.Move;
        }
    }
}
