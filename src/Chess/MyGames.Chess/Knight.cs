// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyGames.Chess.Extensions;
using MyGames.Domain;
using System.Collections.Generic;

namespace MyGames.Chess
{
    public class Knight : ChessPiece
    {
        public Knight(ChessColor color) : base(color) { }

        protected override string GetNotation() => "♞";

        protected override IEnumerable<BoardCoordinates> GetPossibleMoves(ChessBoard board, BoardCoordinates from)
            => board.GetAllPossibleMoves(from, Color,
            [
                new BoardDirectionOffset(new BoardDirection(-2, -1), 1), // Up Left
                new BoardDirectionOffset(new BoardDirection(-2, 1), 1), // Up Right
                new BoardDirectionOffset(new BoardDirection(2, -1), 1), // Down Left
                new BoardDirectionOffset(new BoardDirection(2, 1), 1), // Down Right
                new BoardDirectionOffset(new BoardDirection(-1, -2), 1), // Left Up
                new BoardDirectionOffset(new BoardDirection(1, -2), 1), // Left Down
                new BoardDirectionOffset(new BoardDirection(-1, 2), 1), // Right Up
                new BoardDirectionOffset(new BoardDirection(1, 2), 1), // Right Down
            ]);
    }
}
