// -----------------------------------------------------------------------
// <copyright file="IMove.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyGames.Core;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Define a move")]
public interface IMove;

public interface IMove<in TBoard, out TPlayedMove> : IMove
    where TBoard : IBoard
    where TPlayedMove : IPlayedMove
{
    TPlayedMove Apply(TBoard board, IPlayer player);
}
