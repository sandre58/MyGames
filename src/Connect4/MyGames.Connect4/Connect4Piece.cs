// -----------------------------------------------------------------------
// <copyright file="Connect4Piece.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;
using MyGames.Core;

namespace MyGames.Connect4;

public class Connect4Piece(IConnect4Player player) : IPiece
{
    public IConnect4Player Player { get; } = player;

    public bool IsSimilar(IPiece? obj) => obj is Connect4Piece piece && Player == piece.Player;

    public override string ToString() => string.Format(CultureInfo.InvariantCulture, "{0,1}", Player.ToString());
}
