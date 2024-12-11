// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;

namespace MyGames.Chess.Console.Players
{
    internal abstract class ConsolePlayer : IChessPlayer
    {
        protected ConsolePlayer(string displayName, string color)
        {
            DisplayName = displayName;
            Color = color;
        }

        public string DisplayName { get; }

        public string Color { get; }

        public abstract IChessMove NextMove(ChessGame game);

        [ExcludeFromCodeCoverage]
        public override string ToString() => DisplayName.ToString();
    }
}
