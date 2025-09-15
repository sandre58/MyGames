// -----------------------------------------------------------------------
// <copyright file="BoardGame.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using MyGames.Core.Exceptions;
using MyNet.Utilities;

namespace MyGames.Core;

[DebuggerDisplay("{DebuggerDisplayValue}")]
public abstract class BoardGame<TBoard, TPlayer, TPiece, TMove, TPlayedMove> : IGame<TBoard, TMove, TPlayedMove>
    where TBoard : Board<TPiece>
    where TPlayer : IPlayer
    where TPiece : IPiece
    where TMove : IMove<TBoard, TPlayedMove>
    where TPlayedMove : IPlayedMove
{
    private readonly Stack<HistoryMove<TPlayer, TBoard, TMove, TPlayedMove>> _history = [];
    private readonly Stack<HistoryMove<TPlayer, TBoard, TMove, TPlayedMove>> _undoHistory = new();
    private int _currentPlayerIndex;

    protected BoardGame(TBoard board, IEnumerable<TPlayer> players, int currentPlayerIndex = 0)
    {
        Board = board;

        var playersList = players.ToList();

        if (playersList.Count == 0) throw new ArgumentException("No players provided", nameof(players));

        Players = playersList.AsReadOnly();
        _currentPlayerIndex = currentPlayerIndex;

        AnalyseBoardInternal();
    }

    private string? DebuggerDisplayValue => ToString();

    public TBoard Board { get; }

    public ReadOnlyCollection<TPlayer> Players { get; }

    public bool IsOver { get; private set; }

    public TPlayer? Winner { get; protected set; }

    public TPlayer CurrentPlayer => _currentPlayerIndex >= 0 && _currentPlayerIndex < Players.Count ? Players[_currentPlayerIndex] : throw new InvalidOperationException("Current player has not been defined.");

    public IReadOnlyList<HistoryMove<TPlayer, TBoard, TMove, TPlayedMove>> History => _history.Reverse().ToList().AsReadOnly();

    public IReadOnlyList<HistoryMove<TPlayer, TBoard, TMove, TPlayedMove>> UndoHistory => _undoHistory.Reverse().ToList().AsReadOnly();

    protected abstract bool IsDraw();

    protected abstract TPlayer? ComputeWinner();

    #region Switch Player

    protected void SetCurrentPlayer(int indexPlayer) => _currentPlayerIndex = Math.Max(0, Math.Min(indexPlayer, Players.Count - 1));

    protected void SetCurrentPlayer(TPlayer player) => SetCurrentPlayer(Players.IndexOf(player));

    protected virtual void SwitchCurrentPlayer() => SetCurrentPlayer((_currentPlayerIndex + 1) % Players.Count);

    #endregion

    #region Make Move

    protected abstract TMove NextMoveOfCurrentPlayer();

    public bool NextMove() => MakeMove(NextMoveOfCurrentPlayer());

    protected virtual TPlayedMove ApplyMove(TMove move, TPlayer player) => move.Apply(Board, player);

    public bool MakeMove(TMove move)
    {
        if (IsOver) return false;

        try
        {
            var boardBeforeMove = Board.Clone().CastIn<TBoard>();
            var currentPlayer = CurrentPlayer;
            var playedMove = ApplyMove(move, currentPlayer);
            var historyMove = new HistoryMove<TPlayer, TBoard, TMove, TPlayedMove>(boardBeforeMove, currentPlayer, playedMove);
            OnMoveCompleted(playedMove);
            _history.Push(historyMove);

            AnalyseBoard();

            SwitchCurrentPlayer();

            return true;
        }
        catch (InvalidMoveException)
        {
            return false;
        }
    }

    protected virtual void OnMoveCompleted(TPlayedMove playedMove) { }

    private void AnalyseBoardInternal() => AnalyseBoard();

    protected virtual void AnalyseBoard()
    {
        Winner = ComputeWinner();
        IsOver = Winner is not null || IsDraw();
    }

    #endregion

    #region Undo/Redo

    public bool CanUndo() => _history.Count > 0;

    public bool CanRedo() => _undoHistory.Count > 0;

    void IGame<TBoard, TMove, TPlayedMove>.Undo() => Undo();

    public HistoryMove<TPlayer, TBoard, TMove, TPlayedMove> Undo()
    {
        if (!CanUndo()) throw new InvalidOperationException("No moves to undo");

        var lastHistoryMove = _history.Pop();

        var boardBeforeUndo = Board.Clone().CastIn<TBoard>();

        Board.SetFrom(lastHistoryMove.Board);
        AnalyseBoard();

        var undoHistoryMove = new HistoryMove<TPlayer, TBoard, TMove, TPlayedMove>(boardBeforeUndo, lastHistoryMove);
        _undoHistory.Push(undoHistoryMove);

        SetCurrentPlayer(lastHistoryMove.Player);

        return undoHistoryMove;
    }

    public HistoryMove<TPlayer, TBoard, TMove, TPlayedMove> Redo()
    {
        if (!CanRedo()) throw new InvalidOperationException("No moves to redo");

        var lastHistoryMove = _undoHistory.Pop();

        var boardBeforeRedo = Board.Clone().CastIn<TBoard>();

        Board.SetFrom(lastHistoryMove.Board);
        AnalyseBoard();

        var redoHistoryMove = new HistoryMove<TPlayer, TBoard, TMove, TPlayedMove>(boardBeforeRedo, lastHistoryMove);
        _history.Push(redoHistoryMove);

        SetCurrentPlayer(lastHistoryMove.Player);

        return redoHistoryMove;
    }

    #endregion

    #region Clone

    protected abstract BoardGame<TBoard, TPlayer, TPiece, TMove, TPlayedMove> NewInstance(TBoard board);

    IGame<TBoard, TMove, TPlayedMove> ICloneable<IGame<TBoard, TMove, TPlayedMove>>.Clone() => Clone();

    public BoardGame<TBoard, TPlayer, TPiece, TMove, TPlayedMove> Clone()
    {
        var clone = NewInstance(Board);

        clone.Winner = Winner;
        clone.IsOver = IsOver;

        clone.SetCurrentPlayer(CurrentPlayer);

        return clone;
    }

    #endregion

    public override string? ToString() => Board.ToString();
}
