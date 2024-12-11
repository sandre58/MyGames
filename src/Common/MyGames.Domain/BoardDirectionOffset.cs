// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;

namespace MyGames.Domain
{
    [ExcludeFromCodeCoverage]
    public readonly record struct BoardDirectionOffset(BoardDirection Direction, int Multiplier = int.MaxValue);
}
