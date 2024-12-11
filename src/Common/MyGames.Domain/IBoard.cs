// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Utilities;

namespace MyGames.Domain
{
    public interface IBoard : ICloneable<IBoard>, ISimilar<IBoard>
    {
        bool Exists(BoardCoordinates coordinates);

        bool IsEmpty();

        bool IsEmpty(BoardCoordinates coordinates);

        bool IsFull();
    }
}
