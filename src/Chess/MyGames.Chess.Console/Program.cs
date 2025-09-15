// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MyGames.Chess;
using MyGames.Chess.Console;
using MyGames.Chess.Console.Players;
using MyGames.Chess.Strategies;
using MyNet.Utilities;
using MyNet.Utilities.Generator;
using Spectre.Console;

Dictionary<string, Func<string, string, ConsolePlayer>> playerBuilders = new()
{
    ["Human"] = (name, color) => new HumanPlayer(name, color),
    ["Random"] = (name, color) => new RandomPlayer(name, color),
    ["AI"] = (name, color) => new AIPlayer(name, color, RandomGenerator.Enum<Level>())
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
    IChessMove move = null!;

    var isCheck = game.IsCheck(player);
    if (isCheck)
        AnsiConsole.MarkupLine($"[orange1]Warning : {player.DisplayName} is in check ![/]");

    switch (player)
    {
        case RandomPlayer:
            AnsiConsole.Status().Start($"[bold {player.Color}] {player.DisplayName} thinking...[/]", _ => Thread.Sleep(1000));
            move = player.NextMove(game);
            break;
        case AIPlayer:
            AnsiConsole.Status().Start($"[bold {player.Color}] {player.DisplayName} thinking...[/]", _ => move = player.NextMove(game));
            break;
        default:
            move = player.NextMove(game);
            break;
    }

    switch (move)
    {
        case EnPassantCaptureMove enPassantCaptureMove:
            AnsiConsole.MarkupLine($"[bold {player.Color}] {player.DisplayName} plays {ConsoleHelper.WritePiece(enPassantCaptureMove.Piece)} in {ConsoleHelper.WriteCoordinates(enPassantCaptureMove.Destination)} and takes 'en passant'[/]");
            break;

        case ChessMove chessMove:
            AnsiConsole.MarkupLine($"[bold {player.Color}] {player.DisplayName} plays {ConsoleHelper.WritePiece(chessMove.Piece)} in {ConsoleHelper.WriteCoordinates(chessMove.Destination)}{(move is PromotePawnMove pawnReachEndOfBoardMove ? $" and replaces it by {pawnReachEndOfBoardMove.ExchangePiece}" : string.Empty)}[/]");
            break;

        case CastlingMove:
            AnsiConsole.MarkupLine($"[bold {player.Color}] {player.DisplayName} plays castle[/]");
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
        if (isCheck)
            AnsiConsole.MarkupLine("[bold red]Invalid move. Protect your king with a valid move.[/]");
        else
            AnsiConsole.MarkupLine("[bold red]Invalid move. Try again.[/]");
    }
}

// End summary
if (game.HasCheckmate())
    AnsiConsole.MarkupLine($"[bold {game.Winner?.CastIn<ConsolePlayer>().Color}] Checkmate ! {game.Winner?.CastIn<ConsolePlayer>().DisplayName} wins ![/]");
else if (game.HasStalemate())
    AnsiConsole.MarkupLine("Stalemate. It's a draw!");
else
    AnsiConsole.MarkupLine("Game is over. It's a draw!");

ChessGame createGame()
{
    var gameType = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Choose game").AddChoices("Human vs Human", "Human vs Random", "Human vs AI", "Random vs Human", "Random vs Random", "Random vs AI", "AI vs Human", "AI vs Random", "AI vs AI", "Custom"));

    ConsolePlayer whitePlayer, blackPlayer;

    if (gameType != "Custom")
    {
        whitePlayer = playerBuilders[gameType.Split(" vs ")[0]]("Player 1", "white");
        blackPlayer = playerBuilders[gameType.Split(" vs ")[1]]("Player 2", "gray");

        if (whitePlayer is AIPlayer aiPlayer1)
            aiPlayer1.Level = askLevel(aiPlayer1);
        if (blackPlayer is AIPlayer aiPlayer2)
            aiPlayer2.Level = askLevel(aiPlayer2);
    }
    else
    {
        whitePlayer = createPlayer(1, "white");
        blackPlayer = createPlayer(2, "gray");
    }

    return new ChessGame(whitePlayer, blackPlayer);
}

ConsolePlayer createPlayer(int playerNumber, string color)
{
    var type = AnsiConsole.Prompt(new SelectionPrompt<string>().Title($"Choose type of player {playerNumber} ").AddChoices(playerBuilders.Keys));
    var name = AnsiConsole.Prompt(new TextPrompt<string>($"Enter name of player {playerNumber} ").DefaultValue($"Player {playerNumber}"));

    var player = playerBuilders[type](name, color);

    if (player is AIPlayer aiPlayer)
        aiPlayer.Level = askLevel(aiPlayer);
    return player;
}

void writeGameSummary(ChessGame game)
{
    AnsiConsole.Clear();
    var rule = new Rule("CHESS");
    AnsiConsole.Write(rule);
    AnsiConsole.MarkupLine(string.Join(" vs ", game.Players.OfType<ConsolePlayer>().Select(ConsoleHelper.WritePlayer)));
    AnsiConsole.Write(new Rule());
}

void writeGameTable(ChessGame game)
{
    var table = new Table()
    {
        ShowRowSeparators = true
    };
    table.AddColumn(new TableColumn(string.Empty));
    game.Board.Columns.ForEach(column => table.AddColumn(new TableColumn(ConsoleHelper.WriteColumn(column.Index)).Centered()));
    game.Board.Rows.ForEach(row => table.AddRow(new List<string>() { ConsoleHelper.WriteRow(row.Index) }
                                                .Concat(row.Select(x => !x.IsEmpty ? $"[{((ConsolePlayer)game.GetPlayer(x.Piece.Color)).Color}]{ConsoleHelper.WritePiece(x.Piece)}[/]" : string.Empty)).ToArray()));

    AnsiConsole.Write(table);
    AnsiConsole.WriteLine();
}

static Level askLevel(AIPlayer aiPlayer) => AnsiConsole.Prompt(new SelectionPrompt<Level>().Title($"Choose level of {aiPlayer.DisplayName} ").AddChoices(Enum.GetValues<Level>()));
