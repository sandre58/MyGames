// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MyGames.Connect4.Console.Players
{
    internal abstract class ConsolePlayer : IConnect4Player
    {
        private static int _creationCounter = 0;
        private static readonly List<(char symbol, string consoleSymbol, string color)> DefaultValues =
        [
            ('O', ":yellow_circle:", "yellow"),
            ('X', ":red_circle:", "red")
        ];

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

        [ExcludeFromCodeCoverage]
        public override string ToString() => Symbol.ToString();
    }
}
