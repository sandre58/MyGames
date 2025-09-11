// -----------------------------------------------------------------------
// <copyright file="BoardCoordinatesExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyGames.Core.Extensions;

public static class BoardCoordinatesExtensions
{
    public static BoardDirection GetDirection(this BoardCoordinates from, BoardCoordinates to) => to - from;
}
