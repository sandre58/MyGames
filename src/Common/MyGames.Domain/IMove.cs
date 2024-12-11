// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyGames.Domain
{
    public interface IMove { }

    public interface IMove<in TBoard, out TPlayedMove> : IMove
        where TBoard : IBoard
        where TPlayedMove : IPlayedMove
    {
        TPlayedMove Apply(TBoard board, IPlayer player);
    }
}
