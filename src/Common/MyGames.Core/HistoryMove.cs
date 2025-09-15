// -----------------------------------------------------------------------
// <copyright file="HistoryMove.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;

namespace MyGames.Core;

[DebuggerDisplay("{DebuggerDisplayValue}")]
public class HistoryMove<TPlayer, TBoard, TMove, TPlayedMove>
    where TPlayer : IPlayer
    where TBoard : IBoard
    where TMove : IMove<TBoard, TPlayedMove>
    where TPlayedMove : IPlayedMove
{
    public HistoryMove(TBoard board, TPlayer player, TPlayedMove playedMove) => (Board, Player, Move) = (board, player, playedMove);

    public HistoryMove(TBoard board, HistoryMove<TPlayer, TBoard, TMove, TPlayedMove> historyMove) => (Board, Player, Move) = (board, historyMove.Player, historyMove.Move);

    private string? DebuggerDisplayValue => ToString();

    public TPlayer Player { get; }

    public TPlayedMove Move { get; }

    public TBoard Board { get; }

    public override string ToString() => $"{Player}: {Move}";
}
