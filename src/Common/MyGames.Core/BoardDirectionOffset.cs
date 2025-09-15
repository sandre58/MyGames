// -----------------------------------------------------------------------
// <copyright file="BoardDirectionOffset.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyGames.Core;

public readonly record struct BoardDirectionOffset(BoardDirection Direction, int Multiplier = int.MaxValue);
