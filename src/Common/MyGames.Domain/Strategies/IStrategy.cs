// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyGames.Domain.Strategies
{
    public interface IStrategy<TGame, TBoard, TMove, TPlayedMove>
        where TGame : IGame<TBoard, TMove, TPlayedMove>
        where TBoard : IBoard
        where TMove : IMove<TBoard, TPlayedMove>
        where TPlayedMove : IPlayedMove
    {
        TMove ProvideMove(TGame game, IPlayer player);
    }
}
