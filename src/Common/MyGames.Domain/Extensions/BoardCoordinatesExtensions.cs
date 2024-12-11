// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyGames.Domain.Extensions
{
    public static class BoardCoordinatesExtensions
    {
        public static BoardDirection GetDirection(this BoardCoordinates from, BoardCoordinates to) => to - from;
    }
}
