// -----------------------------------------------------------------------
// <copyright file="ConsoleHelper.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Globalization;
using MyGames.Chess.Console.Players;
using MyGames.Core;
using MyNet.Utilities.Helpers;

namespace MyGames.Chess.Console;

internal static class ConsoleHelper
{
    public static string WritePlayer(ConsolePlayer player)
        => $"[bold {player.Color}]{player.DisplayName} ({player.GetType().Name.Replace("Player", string.Empty, StringComparison.OrdinalIgnoreCase)}{(player is AIPlayer ai ? $" - {ai.Level}" : string.Empty)})[/]";

    public static string WritePiece(ChessPiece piece) => piece switch
    {
        King => "♚",
        Queen => "♛",
        Rook => "♜",
        Bishop => "♝",
        Knight => "♞",
        Pawn => "●",
        _ => throw new NotImplementedException()
    };

    public static string WriteColumn(int column) => CharHelper.GetAlphabet()[column % 25].ToString().ToUpper(CultureInfo.CurrentCulture);

    public static string WriteRow(int row) => $"{ChessBoard.NumberOfRows - row}";

    public static string WriteCoordinates(BoardCoordinates coordinates) => $"{WriteColumn(coordinates.Column)}{WriteRow(coordinates.Row)}";
}
