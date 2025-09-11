// -----------------------------------------------------------------------
// <copyright file="ConsolePlayer.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyGames.Chess.Console.Players;

internal abstract class ConsolePlayer(string displayName, string color) : IChessPlayer
{
    public string DisplayName { get; } = displayName;

    public string Color { get; } = color;

    public abstract IChessMove NextMove(ChessGame game);

    public override string ToString() => DisplayName;
}
