// -----------------------------------------------------------------------
// <copyright file="Level.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyGames.Connect4.Strategies;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1008:Enums should have zero value", Justification = "We wont't zero level")]
public enum Level
{
    VeryEasy = 1,

    Medium = 3,

    Hard = 6
}
