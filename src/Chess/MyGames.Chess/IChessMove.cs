// -----------------------------------------------------------------------
// <copyright file="IChessMove.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyGames.Core;

namespace MyGames.Chess;

public interface IChessMove : IMove<ChessBoard, ChessPlayedMove>
{
    ChessPiece Piece { get; }

    bool IsValid(ChessGame game);
}
