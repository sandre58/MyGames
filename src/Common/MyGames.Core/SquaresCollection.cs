// -----------------------------------------------------------------------
// <copyright file="SquaresCollection.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyGames.Core;

public class SquaresCollection<T>(IEnumerable<Square<T>> squares) : IReadOnlyList<Square<T>>
    where T : IPiece
{
    private readonly ReadOnlyCollection<Square<T>> _squares = squares.ToList().AsReadOnly();

    public Square<T> this[int index] => _squares[index];

    public int Count => _squares.Count;

    public bool IsValid(int index) => index >= 0 && index < _squares.Count;

    public bool IsEmpty() => _squares.All(x => x.IsEmpty);

    public bool IsFull() => _squares.All(x => !x.IsEmpty);

    public Square<T>? GetSquare(int index) => IsValid(index) ? _squares[index] : null;

    public IEnumerator<Square<T>> GetEnumerator() => _squares.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
