// -----------------------------------------------------------------------
// <copyright file="IChessPlayer.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyGames.Core;

namespace MyGames.Chess;

public interface IChessPlayer : IPlayer
{
    IChessMove NextMove(ChessGame game);
}
