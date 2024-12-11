// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Linq;
using Spectre.Console;

namespace MyGames.Connect4.Console.Players
{
    internal class HumanPlayer(string name) : ConsolePlayer(name)
    {
        public override Connect4Move NextMove(Connect4Game game)
        {
            var column = AnsiConsole.Prompt(new TextPrompt<int>($"[bold {Color}]{DisplayName}[/] : Choose a column to play ").AddChoices(game.Board.GetValidColumns().Select(x => x.Index + 1)));
            return new(column - 1);
        }
    }
}
