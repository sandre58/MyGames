// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MyGames.Common;
using MyGames.Common.Exceptions;

namespace MyGames.Connect4.Core
{
    public class Connect4Game : BoardGame<Connect4Board, Connect4Player, Connect4Piece, Connect4Move, Connect4HistoryMove>
    {
        public const int DefaultNumberOfPiecesForWin = 4;

        public Connect4Game(IEnumerable<Connect4Player> players, int numberOfRows = Connect4Board.Rows, int numberOfColumns = Connect4Board.Columns, int numberOfPiecesForWin = DefaultNumberOfPiecesForWin)
            : base(new Connect4Board(numberOfRows, numberOfColumns), players) => NumberOfPiecesForWin = numberOfPiecesForWin;

        public int NumberOfPiecesForWin { get; }

        public ReadOnlyCollection<Connect4Piece> WinnerPieces { get; private set; } = new ReadOnlyCollection<Connect4Piece>([]);

        protected override Connect4Player? GetWinner() => WinnerPieces.FirstOrDefault()?.Player;

        protected override bool GetIsOver() => Board.IsFull();

        public bool MakeMove(int column) => MakeMove(new Connect4Move(column));

        protected override Connect4HistoryMove MakeMove(Connect4Player player, Connect4Move move)
            => !Board.Add(new Connect4Piece(player), move.Column) ? throw new InvalidMoveException(player, move) : new(player, move);

        protected override Connect4HistoryMove CancelMove(Connect4HistoryMove historyMove)
            => !Board.Remove(historyMove.Move.Column) ? throw new InvalidMoveException(historyMove.Player, historyMove.Move) : new(historyMove.Player, historyMove.Move);

        protected override Connect4Move GetMoveOfPlayer(Connect4Player player) => player.NextMove(this);

        protected override void OnMoveCompleted(Connect4HistoryMove historyMove)
        {
            WinnerPieces = new ReadOnlyCollection<Connect4Piece>([.. GetWinnerPieces()]);

            base.OnMoveCompleted(historyMove);
        }

        private List<Connect4Piece> GetWinnerPieces()
        {
            var winners = GetHorizontalWinnerPieces();
            if (winners.Count != 0) return winners;

            winners = GetVerticalWinnerPieces();
            if (winners.Count != 0) return winners;

            winners = GetDiagonalWinnerPieces();
            return winners.Count != 0 ? winners : [];
        }

        private List<Connect4Piece> GetHorizontalWinnerPieces()
        {
            for (var row = 0; row < Board.NumberOfRows; row++)
            {
                var pieceWinners = new List<Connect4Piece>();
                Connect4Piece? previousPiece = null;

                for (var column = 0; column < Board.NumberOfColumns; column++)
                {
                    var currentPiece = Board.GetPiece(row, column);

                    if (currentPiece is null) break;

                    if (previousPiece is null || currentPiece.Player != previousPiece.Player)
                        pieceWinners.Clear();

                    pieceWinners.Add(currentPiece);

                    if (pieceWinners.Count >= NumberOfPiecesForWin)
                        return pieceWinners;
                    else if (NumberOfPiecesForWin - pieceWinners.Count > Board.NumberOfColumns - 1 - column)
                        break;

                    previousPiece = currentPiece;
                }
            }

            return [];
        }

        private List<Connect4Piece> GetVerticalWinnerPieces()
        {
            for (var column = 0; column < Board.NumberOfColumns; column++)
            {
                var pieceWinners = new List<Connect4Piece>();
                Connect4Piece? previousPiece = null;

                for (var row = Board.NumberOfRows - 1; row >= 0; row--)
                {
                    var currentPiece = Board.GetPiece(row, column);

                    if (currentPiece is null) break;

                    if (previousPiece is null || currentPiece.Player != previousPiece.Player)
                        pieceWinners.Clear();

                    pieceWinners.Add(currentPiece);

                    if (pieceWinners.Count >= NumberOfPiecesForWin)
                        return pieceWinners;
                    else if (NumberOfPiecesForWin - pieceWinners.Count > row)
                        break;

                    previousPiece = currentPiece;
                }
            }

            return [];
        }

        private List<Connect4Piece> GetDiagonalWinnerPieces()
        {
            var bottomToTop = getDiagonalWinners(true);

            if (bottomToTop.Count != 0) return bottomToTop;

            var topToBottom = getDiagonalWinners(false);

            return topToBottom.Count != 0 ? topToBottom : [];

            List<Connect4Piece> getDiagonalWinners(bool bottomToTop)
            {
                foreach (var piece in Board.GetPieces())
                {
                    var pieceWinners = new List<Connect4Piece> { piece.Value };

                    for (var i = 1; i < NumberOfPiecesForWin; i++)
                    {
                        var nextColumn = piece.Key.Column + i;
                        var nextRow = bottomToTop ? piece.Key.Row - i : piece.Key.Row + i;

                        var nextPiece = Board.GetPiece(nextRow, nextColumn);

                        if (nextPiece is not null && nextPiece.Player == piece.Value.Player)
                            pieceWinners.Add(nextPiece!);
                        else
                            break;

                        if (pieceWinners.Count >= NumberOfPiecesForWin)
                            return pieceWinners;
                    }
                }

                return [];
            }
        }

        protected override BoardGame<Connect4Board, Connect4Player, Connect4Piece, Connect4Move, Connect4HistoryMove> NewInstance()
            => new Connect4Game(Players, Board.NumberOfRows, Board.NumberOfColumns, NumberOfPiecesForWin);
    }
}
