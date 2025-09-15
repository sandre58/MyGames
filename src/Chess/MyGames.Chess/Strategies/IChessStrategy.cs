// -----------------------------------------------------------------------
// <copyright file="IChessStrategy.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyGames.Core.Strategies;

namespace MyGames.Chess.Strategies;

public interface IChessStrategy : IStrategy<ChessGame, ChessBoard, IChessMove, ChessPlayedMove>;
