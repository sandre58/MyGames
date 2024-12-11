// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace MyGames.Domain
{
    [DebuggerDisplay("{DebuggerDisplayValue}")]
    public class SquaresColumn<T> : SquaresCollection<T> where T : IPiece
    {
        internal SquaresColumn(int index, IEnumerable<Square<T>> squares) : base(squares) => Index = index;

        [ExcludeFromCodeCoverage]
        private string? DebuggerDisplayValue => ToString();

        public int Index { get; }

        [ExcludeFromCodeCoverage]
        public override string ToString() => Index.ToString();
    }
}
