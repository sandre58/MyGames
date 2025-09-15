// -----------------------------------------------------------------------
// <copyright file="IBoard.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyNet.Utilities;

namespace MyGames.Core;

public interface IBoard : ICloneable<IBoard>, ISimilar<IBoard>
{
    bool Exists(BoardCoordinates coordinates);

    bool IsEmpty();

    bool IsEmpty(BoardCoordinates coordinates);

    bool IsFull();
}
