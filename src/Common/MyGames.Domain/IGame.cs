// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Utilities;

namespace MyGames.Domain
{
    public interface IGame<TBoard, TMove, TPlayedMove> : ICloneable<IGame<TBoard, TMove, TPlayedMove>>
        where TBoard : IBoard
        where TMove : IMove<TBoard, TPlayedMove>
        where TPlayedMove : IPlayedMove
    {
        bool IsOver { get; }

        bool MakeMove(TMove move);

        void Undo();
    }
}
