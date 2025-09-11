// -----------------------------------------------------------------------
// <copyright file="IGame.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyNet.Utilities;

namespace MyGames.Core;

public interface IGame<TBoard, TMove, TPlayedMove> : ICloneable<IGame<TBoard, TMove, TPlayedMove>>
    where TBoard : IBoard
    where TMove : IMove<TBoard, TPlayedMove>
    where TPlayedMove : IPlayedMove
{
    bool IsOver { get; }

    bool MakeMove(TMove move);

    void Undo();
}
