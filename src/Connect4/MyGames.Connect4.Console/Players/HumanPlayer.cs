// -----------------------------------------------------------------------
// <copyright file="HumanPlayer.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using Spectre.Console;

namespace MyGames.Connect4.Console.Players;

internal sealed class HumanPlayer(string name) : ConsolePlayer(name)
{
    public override Connect4Move NextMove(Connect4Game game)
    {
        var column = AnsiConsole.Prompt(new TextPrompt<int>($"[bold {Color}]{DisplayName}[/] : Choose a column to play ").AddChoices(game.Board.GetValidColumns().Select(x => x.Index + 1)));
        return new(column - 1);
    }
}
