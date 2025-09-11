// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using MyGames.Connect4;
using MyGames.Connect4.Console.Players;
using MyGames.Connect4.Strategies;
using MyNet.Utilities;
using MyNet.Utilities.Generator;
using Spectre.Console;

Dictionary<string, Func<string, ConsolePlayer>> playerBuilders = new()
{
    ["Human"] = name => new HumanPlayer(name),
    ["Random"] = name => new RandomPlayer(name),
    ["AI"] = name => new AIPlayer(name, RandomGenerator.Enum<Level>())
};

// Options
var game = createGame();
var showHistory = AnsiConsole.Prompt(new ConfirmationPrompt("Show moves history ?"));

// Start summary
writeGameSummary(game);

// Game
writeGameTable(game);
while (!game.IsOver)
{
    var player = (ConsolePlayer)game.CurrentPlayer;
    Connect4Move move = null!;

    switch (player)
    {
        case RandomPlayer:
            AnsiConsole.Status().Start($"[bold {player.Color}] {player.DisplayName} thinking...[/]", _ => Thread.Sleep(1000));
            move = player.NextMove(game);
            AnsiConsole.MarkupLine($"[bold {player.Color}] {player.DisplayName} plays in column {move.Column + 1}[/]");
            break;
        case AIPlayer:
            AnsiConsole.Status().Start($"[bold {player.Color}] {player.DisplayName} thinking...[/]", _ => move = player.NextMove(game));
            AnsiConsole.MarkupLine($"[bold {player.Color}] {player.DisplayName} plays in column {move.Column + 1}[/]");
            break;
        default:
            move = player.NextMove(game);
            break;
    }

    if (game.MakeMove(move))
    {
        if (!showHistory)
        {
            AnsiConsole.Clear();
            writeGameSummary(game);
        }

        writeGameTable(game);
    }
    else
    {
        AnsiConsole.MarkupLine("[bold red]Invalid move. Try again.[/]");
    }
}

// End summary
AnsiConsole.MarkupLine(game.Winner != null ? $"[bold {game.Winner.CastIn<ConsolePlayer>().Color}] {game.Winner.CastIn<ConsolePlayer>().DisplayName} wins ![/]" : "It's a draw!");

int promptOption(string prompt, int minValue, int defaultValue)
    => AnsiConsole.Prompt(new TextPrompt<int>(prompt).DefaultValue(defaultValue)
                                                     .Validate(x => x < minValue
                                                                    ? ValidationResult.Error($"Must be greater than {minValue}")
                                                                    : ValidationResult.Success()));

Connect4Game createGame()
{
    var gameType = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Choose game").AddChoices("Human vs Human", "Human vs Random", "Human vs AI", "Random vs Human", "Random vs Random", "Random vs AI", "AI vs Human", "AI vs Random", "AI vs AI", "Custom"));

    int rows, columns, numberOfPiecesForWin;
    ConsolePlayer playerOne, playerTwo;

    if (gameType != "Custom")
    {
        rows = Connect4Board.DefaultRows;
        columns = Connect4Board.DefaultColumns;
        numberOfPiecesForWin = Connect4Game.DefaultNumberOfPiecesForWin;
        playerOne = playerBuilders[gameType.Split(" vs ")[0]]("Player 1");
        playerTwo = playerBuilders[gameType.Split(" vs ")[1]]("Player 2");

        if (playerOne is AIPlayer aiPlayer1)
            aiPlayer1.Level = askLevel(aiPlayer1);
        if (playerTwo is AIPlayer aiPlayer2)
            aiPlayer2.Level = askLevel(aiPlayer2);
    }
    else
    {
        rows = promptOption("How many rows ?", 1, Connect4Board.DefaultRows);
        columns = promptOption("How many columns ?", 1, Connect4Board.DefaultColumns);
        numberOfPiecesForWin = promptOption("How many pieces for win ?", 3, Connect4Game.DefaultNumberOfPiecesForWin);
        playerOne = createPlayer(1);
        playerTwo = createPlayer(2);
    }

    return new Connect4Game([playerOne, playerTwo], rows, columns, numberOfPiecesForWin);
}

ConsolePlayer createPlayer(int playerNumber)
{
    var type = AnsiConsole.Prompt(new SelectionPrompt<string>().Title($"Choose type of player {playerNumber} ").AddChoices(playerBuilders.Keys));
    var name = AnsiConsole.Prompt(new TextPrompt<string>($"Enter name of player {playerNumber} ").DefaultValue($"Player {playerNumber}"));

    var player = playerBuilders[type](name);

    if (player is AIPlayer aiPlayer)
        aiPlayer.Level = askLevel(aiPlayer);
    return player;
}

void writeGameSummary(Connect4Game game)
{
    AnsiConsole.Clear();
    var rule = new Rule("CONNECT 4");
    AnsiConsole.Write(rule);
    AnsiConsole.MarkupLine(string.Join(" vs ", game.Players.OfType<ConsolePlayer>().Select(writePlayer)));
    AnsiConsole.MarkupLine($"[bold blue]{game.NumberOfPiecesForWin} pieces lined up to win[/]");
    AnsiConsole.Write(new Rule());
}

void writeGameTable(Connect4Game game)
{
    var table = new Table();
    game.Board.Columns.ForEach(column => table.AddColumn(new TableColumn((column.Index + 1).ToString(CultureInfo.InvariantCulture)).Centered()));
    game.Board.Rows.ForEach(row => table.AddRow(row.Select(square => !square.IsEmpty ? writePiece(square.Piece) : string.Empty).ToArray()));

    AnsiConsole.Write(table);
    AnsiConsole.WriteLine();
}

string writePlayer(ConsolePlayer player)
    => $"[bold {player.Color}]{player.DisplayName} ({player.GetType().Name.Replace("Player", string.Empty, StringComparison.OrdinalIgnoreCase)}{(player is AIPlayer ai ? $" - {ai.Level}" : string.Empty)})[/]";

string writePiece(Connect4Piece piece) => $"{((ConsolePlayer)piece.Player).ConsoleSymbol}";

static Level askLevel(AIPlayer aiPlayer) => AnsiConsole.Prompt(new SelectionPrompt<Level>().Title($"Choose level of {aiPlayer.DisplayName} ").AddChoices(Enum.GetValues<Level>()));
