// -----------------------------------------------------------------------
// <copyright file="Connect4Game.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MyGames.Core;
using MyGames.Core.Extensions;
using MyNet.Utilities;

namespace MyGames.Connect4;

public class Connect4Game(Connect4Board board, IEnumerable<IConnect4Player> players, int numberOfPiecesForWin = Connect4Game.DefaultNumberOfPiecesForWin) : BoardGame<Connect4Board, IConnect4Player, Connect4Piece, Connect4Move, Connect4Move>(board, players)
{
    public const int DefaultNumberOfPiecesForWin = 4;

    public Connect4Game(IEnumerable<IConnect4Player> players, int numberOfRows = Connect4Board.DefaultRows, int numberOfColumns = Connect4Board.DefaultColumns, int numberOfPiecesForWin = DefaultNumberOfPiecesForWin)
        : this(new Connect4Board(numberOfRows, numberOfColumns), players, numberOfPiecesForWin) { }

    public int NumberOfPiecesForWin { get; } = numberOfPiecesForWin;

    public ReadOnlyCollection<Square<Connect4Piece>> WinnerSquares { get; private set; } = new ReadOnlyCollection<Square<Connect4Piece>>([]);

    protected override IConnect4Player? ComputeWinner() => WinnerSquares.FirstOrDefault()?.Piece.Player;

    protected override bool IsDraw() => Board.IsFull();

    protected override Connect4Move NextMoveOfCurrentPlayer() => CurrentPlayer.CastIn<IConnect4Player>().NextMove(this);

    public bool MakeMove(int column) => MakeMove(new Connect4Move(column));

    protected override void AnalyseBoard()
    {
        WinnerSquares = new ReadOnlyCollection<Square<Connect4Piece>>([.. ComputeWinnerSquares()]);
        base.AnalyseBoard();
    }

    private IReadOnlyCollection<Square<Connect4Piece>> ComputeWinnerSquares()
    {
        var winners = ComputeHorizontalWinnerSquares();
        if (winners.Count != 0) return winners;

        winners = ComputeVerticalWinnerSquares();
        if (winners.Count != 0) return winners;

        winners = ComputeDiagonalWinnerSquares();
        return winners.Count != 0 ? winners : [];
    }

    private IReadOnlyCollection<Square<Connect4Piece>> ComputeHorizontalWinnerSquares()
    {
        foreach (var row in Board.Rows.Reverse())
        {
            if (row.IsEmpty()) break;

            var winnerPieces = row.GetNotEmptyConsecutives((x, y) => x.IsSimilar(y)).FirstOrDefault(x => x.Count >= NumberOfPiecesForWin);
            if (winnerPieces is not null) return winnerPieces;
        }

        return [];
    }

    private IReadOnlyCollection<Square<Connect4Piece>> ComputeVerticalWinnerSquares()
    {
        foreach (var row in Board.Columns)
        {
            var winnerPieces = row.GetNotEmptyConsecutives((x, y) => x.IsSimilar(y)).FirstOrDefault(x => x.Count >= NumberOfPiecesForWin);
            if (winnerPieces is not null) return winnerPieces;
        }

        return [];
    }

    private List<Square<Connect4Piece>> ComputeDiagonalWinnerSquares()
    {
        var downRight = computeDiagonalWinnersSquares(BoardDirection.DownRight);

        if (downRight.Count != 0) return downRight;

        var upRight = computeDiagonalWinnersSquares(BoardDirection.UpRight);

        return upRight.Count != 0 ? upRight : [];

        List<Square<Connect4Piece>> computeDiagonalWinnersSquares(BoardDirection direction)
        {
            foreach (var square in Board.GetNotEmptySquares().ToList())
            {
                var winnerSquares = new List<Square<Connect4Piece>> { square };

                var nextCoordinates = square.Coordinates + direction;
                for (var i = 1; i < NumberOfPiecesForWin; i++)
                {
                    var nextSquare = Board.TryGetSquare(nextCoordinates);

                    if (nextSquare?.IsEmpty == false && nextSquare.Piece.IsSimilar(square.Piece))
                        winnerSquares.Add(nextSquare);
                    else
                        break;

                    if (winnerSquares.Count >= NumberOfPiecesForWin)
                        return winnerSquares;

                    nextCoordinates += direction;
                }
            }

            return [];
        }
    }

    protected override BoardGame<Connect4Board, IConnect4Player, Connect4Piece, Connect4Move, Connect4Move> NewInstance(Connect4Board board)
        => new Connect4Game(board.Clone().CastIn<Connect4Board>(), Players.OfType<IConnect4Player>(), NumberOfPiecesForWin);
}
