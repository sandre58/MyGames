// -----------------------------------------------------------------------
// <copyright file="ChessBoardPiecesCollection.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyNet.Utilities;

namespace MyGames.Chess;

public sealed class ChessBoardPiecesCollection : IReadOnlyList<ChessPiece>, ISettable<ChessBoardPiecesCollection>, ICloneable<ChessBoardPiecesCollection>
{
    public const int LeftRookColumn = 0;
    public const int LeftKnightColumn = 1;
    public const int LeftBishopColumn = 2;
    public const int QueenColumn = 3;
    public const int KingColumn = 4;
    public const int RightBishopColumn = 5;
    public const int RightKnightColumn = 6;
    public const int RightRookColumn = 7;

    public static ChessBoardPiecesCollection Whites(bool fillWithOriginalPieces = true) => new(ChessColor.White, fillWithOriginalPieces);

    public static ChessBoardPiecesCollection Blacks(bool fillWithOriginalPieces = true) => new(ChessColor.Black, fillWithOriginalPieces);

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "No space used in more")]
    private readonly ChessPiece[,] _originalPieces = new ChessPiece[2, 8];
    private readonly List<ChessPiece> _currentPieces = [];

    private ChessBoardPiecesCollection(ChessColor color, bool fillWithOriginalPieces = true)
    {
        _originalPieces[0, LeftRookColumn] = new Rook(color);
        _originalPieces[0, LeftKnightColumn] = new Knight(color);
        _originalPieces[0, LeftBishopColumn] = new Bishop(color);
        _originalPieces[0, QueenColumn] = new Queen(color);
        _originalPieces[0, KingColumn] = new King(color);
        _originalPieces[0, RightBishopColumn] = new Bishop(color);
        _originalPieces[0, RightKnightColumn] = new Knight(color);
        _originalPieces[0, RightRookColumn] = new Rook(color);
        Enumerable.Range(0, 8).ForEach(x => _originalPieces[1, x] = new Pawn(color));

        if (fillWithOriginalPieces)
            _originalPieces.Cast<ChessPiece>().ForEach(x => Insert(x));
    }

    private ChessBoardPiecesCollection()
    {
    }

    public ChessPiece this[int index] => _currentPieces[index];

    public int Count => _currentPieces.Count;

    public Rook LeftRook => (Rook)_originalPieces[0, LeftRookColumn];

    public Knight LeftKnight => (Knight)_originalPieces[0, LeftKnightColumn];

    public Bishop LeftBishop => (Bishop)_originalPieces[0, LeftBishopColumn];

    public Queen Queen => (Queen)_originalPieces[0, QueenColumn];

    public King King => (King)_originalPieces[0, KingColumn];

    public Bishop RightBishop => (Bishop)_originalPieces[0, RightBishopColumn];

    public Knight RightKnight => (Knight)_originalPieces[0, RightKnightColumn];

    public Rook RightRook => (Rook)_originalPieces[0, RightRookColumn];

    public Pawn GetPawn(int column) => (Pawn)_originalPieces[1, column];

    public bool IsAlive(ChessPiece piece) => _currentPieces.Contains(piece);

    internal bool Remove(ChessPiece piece) => _currentPieces.Remove(piece);

    internal bool Insert(ChessPiece piece)
    {
        if (_currentPieces.Contains(piece)) return false;

        _currentPieces.Add(piece);

        return true;
    }

    public IEnumerable<ChessPiece> GetOriginalPieces() => [.. _originalPieces.Cast<ChessPiece>()];

    public IEnumerator<ChessPiece> GetEnumerator() => _currentPieces.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void SetFrom(ChessBoardPiecesCollection? from)
    {
        if (from is null) return;

        _currentPieces.Clear();
        _currentPieces.AddRange(from);
    }

    public ChessBoardPiecesCollection Clone()
    {
        var instance = new ChessBoardPiecesCollection();

        for (var i = 0; i < _originalPieces.GetLength(0); i++)
        {
            for (var j = 0; j < _originalPieces.GetLength(1); j++)
                instance._originalPieces[i, j] = _originalPieces[i, j];
        }

        instance._currentPieces.AddRange(_currentPieces);

        return instance;
    }
}
