// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyGames.Chess.Strategies
{
    public interface IBoardEvaluator
    {
        int Evaluate(ChessBoard board, ChessColor color);
    }
}
