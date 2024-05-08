// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MyGames.Common.Exceptions;

namespace MyGames.Common
{
    public abstract class BoardGame<TBoard, TPlayer, TPiece, TMove, THistoryMove>
        where TBoard : Board<TPiece>
        where TPiece : IPiece
        where TPlayer : IPlayer
        where TMove : IMove
        where THistoryMove : HistoryMove<TPlayer, TMove>
    {
        private int _currentPlayerIndex;
        private readonly Dictionary<THistoryMove, BoardGame<TBoard, TPlayer, TPiece, TMove, THistoryMove>> _history = [];
        private readonly Stack<THistoryMove> _cancelledMovesHistory = new();

        protected BoardGame(TBoard board, IEnumerable<TPlayer> players)
        {
            Board = board;

            if (!players.Any()) throw new ArgumentException("No players provided", nameof(players));

            Players = players.ToList().AsReadOnly();
        }

        public TBoard Board { get; }

        public ReadOnlyCollection<TPlayer> Players { get; }

        public bool IsOver { get; private set; }

        public TPlayer? Winner { get; private set; }

        protected abstract TMove GetMoveOfPlayer(TPlayer player);

        protected abstract THistoryMove MakeMove(TPlayer player, TMove move);

        protected abstract THistoryMove CancelMove(THistoryMove historyMove);

        protected abstract bool GetIsOver();

        protected abstract TPlayer? GetWinner();

        protected abstract BoardGame<TBoard, TPlayer, TPiece, TMove, THistoryMove> NewInstance();

        public TPlayer GetCurrentPlayer()
            => _currentPlayerIndex >= 0 && _currentPlayerIndex < Players.Count ? Players[_currentPlayerIndex] : throw new InvalidOperationException("Current player has not been defined.");

        public bool NextMove() => MakeMove(GetMoveOfPlayer(GetCurrentPlayer()));

        public bool MakeMove(TMove move)
        {
            if (IsOver) return false;

            try
            {
                var historyMove = MakeMove(GetCurrentPlayer(), move);
                OnMoveCompleted(historyMove);
                _history.Add(historyMove, Clone());

                return true;
            }
            catch (InvalidMoveException)
            {
                return false;
            }
        }

        public THistoryMove? CancelLastMove()
        {
            if (_history.Count == 0) return default;

            var lastHistory = _history.Last().Key;

            try
            {
                SetCurrentPlayer(lastHistory.Player);

                var historyMove = CancelMove(lastHistory);
                OnMoveCompleted(historyMove);

                _history.Remove(lastHistory);
                _cancelledMovesHistory.Push(historyMove);

                return lastHistory;
            }
            catch (InvalidMoveException)
            {
                return default;
            }
        }

        public THistoryMove? RemakeLastCancelledMove()
        {
            if (_cancelledMovesHistory.Count == 0) return default;

            var historyMove = _cancelledMovesHistory.Pop();

            SetCurrentPlayer(historyMove.Player);

            return MakeMove(historyMove.Move) ? historyMove : default;
        }

        public IDictionary<THistoryMove, BoardGame<TBoard, TPlayer, TPiece, TMove, THistoryMove>> GetHistory() => _history.ToDictionary(x => x.Key, x => x.Value.Clone());

        public IEnumerable<THistoryMove> GetHistoryMoves() => _history.Keys;

        public IEnumerable<BoardGame<TBoard, TPlayer, TPiece, TMove, THistoryMove>> GetHistoryGames() => _history.Values.Select(x => x.Clone());

        public BoardGame<TBoard, TPlayer, TPiece, TMove, THistoryMove> GetGameFromHistory(THistoryMove historyMove) => _history[historyMove].Clone();

        public BoardGame<TBoard, TPlayer, TPiece, TMove, THistoryMove> Clone()
        {
            var clone = NewInstance();

            clone.Board.SetFrom(Board);
            clone.Winner = Winner;
            clone.IsOver = IsOver;

            clone.SetCurrentPlayer(GetCurrentPlayer());

            return clone;
        }


        protected virtual void SwitchCurrentPlayer() => SetCurrentPlayer((_currentPlayerIndex + 1) % Players.Count);

        protected void SetCurrentPlayer(int indexPlayer) => _currentPlayerIndex = Math.Max(0, Math.Min(indexPlayer, Players.Count - 1));

        protected void SetCurrentPlayer(TPlayer player) => SetCurrentPlayer(Players.IndexOf(player));

        protected virtual void OnMoveCompleted(THistoryMove historyMove)
        {
            Winner = GetWinner();
            IsOver = Winner is not null || GetIsOver();

            SwitchCurrentPlayer();
        }
    }
}
