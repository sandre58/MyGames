// -----------------------------------------------------------------------
// <copyright file="ChessBoardFactory.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace MyGames.Chess.Factories;

public static class ChessBoardFactory
{
    public static ChessBoard Empty() => new((_, __) => new Dictionary<ChessPiece, (int, int)>());

    public static ChessBoard Create() => new();

    public static ChessBoard Create(Func<ChessBoardPiecesCollection, ChessBoardPiecesCollection, IDictionary<ChessPiece, (int Row, int Column)>> newPiecesCreation)
    {
        var validNewPiecesCreation = new Func<ChessBoardPiecesCollection, ChessBoardPiecesCollection, IDictionary<ChessPiece, (int Row, int Column)>>((whites, blacks) =>
        {
            var newPieces = newPiecesCreation.Invoke(whites, blacks);

            if (!newPieces.Keys.Where(x => x.Color == ChessColor.White).OfType<King>().Any())
                newPieces.Add(whites.King, (7, ChessBoardPiecesCollection.KingColumn));

            if (!newPieces.Keys.Where(x => x.Color == ChessColor.Black).OfType<King>().Any())
                newPieces.Add(blacks.King, (0, ChessBoardPiecesCollection.KingColumn));

            return newPieces;
        });
        return new(validNewPiecesCreation);
    }
}
