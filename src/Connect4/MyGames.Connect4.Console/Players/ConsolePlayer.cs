// -----------------------------------------------------------------------
// <copyright file="ConsolePlayer.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace MyGames.Connect4.Console.Players;

internal abstract class ConsolePlayer : IConnect4Player
{
    private static readonly List<(char Symbol, string ConsoleSymbol, string Color)> DefaultValues =
    [
        ('O', ":yellow_circle:", "yellow"),
        ('X', ":red_circle:", "red")
    ];

    private static int _creationCounter;

    protected ConsolePlayer(string displayName)
    {
        DisplayName = displayName;

        var (symbol, consoleSymbol, color) = DefaultValues[_creationCounter % DefaultValues.Count];
        Symbol = symbol;
        ConsoleSymbol = consoleSymbol;
        Color = color;

        IncrementCounter();
    }

    private static void IncrementCounter() => _creationCounter++;

    public string DisplayName { get; }

    public char Symbol { get; }

    public string ConsoleSymbol { get; }

    public string Color { get; }

    public abstract Connect4Move NextMove(Connect4Game game);

    public override string ToString() => Symbol.ToString();
}
