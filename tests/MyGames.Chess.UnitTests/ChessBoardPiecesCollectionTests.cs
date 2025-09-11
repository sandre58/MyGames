// -----------------------------------------------------------------------
// <copyright file="ChessBoardPiecesCollectionTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Xunit;

namespace MyGames.Chess.UnitTests;

public class ChessBoardPiecesCollectionTests
{
    [Fact]
    public void Whites_ShouldReturnCollectionWithWhitePieces()
    {
        // Act
        var collection = ChessBoardPiecesCollection.Whites();

        // Assert
        Assert.NotNull(collection);
        Assert.Equal(16, collection.Count);
        Assert.IsType<King>(collection.King);
        Assert.Equal(ChessColor.White, collection.King.Color);
    }

    [Fact]
    public void Blacks_ShouldReturnCollectionWithBlackPieces()
    {
        // Act
        var collection = ChessBoardPiecesCollection.Blacks();

        // Assert
        Assert.NotNull(collection);
        Assert.Equal(16, collection.Count);
        Assert.IsType<King>(collection.King);
        Assert.Equal(ChessColor.Black, collection.King.Color);
    }

    [Fact]
    public void Insert_ShouldAddPieceToCollection()
    {
        // Arrange
        var collection = ChessBoardPiecesCollection.Whites(false);
        var piece = new Rook(ChessColor.White);

        // Act
        var result = collection.Insert(piece);

        // Assert
        Assert.True(result);
        Assert.Contains(piece, collection);
    }

    [Fact]
    public void Insert_ShouldNotAddDuplicatePiece()
    {
        // Arrange
        var collection = ChessBoardPiecesCollection.Whites(false);
        var piece = new Rook(ChessColor.White);
        collection.Insert(piece);

        // Act
        var result = collection.Insert(piece);

        // Assert
        Assert.False(result);
        Assert.Single(collection);
    }

    [Fact]
    public void Remove_ShouldRemovePieceFromCollection()
    {
        // Arrange
        var collection = ChessBoardPiecesCollection.Whites();
        var piece = collection.King;

        // Act
        var result = collection.Remove(piece);

        // Assert
        Assert.True(result);
        Assert.DoesNotContain(piece, collection);
    }

    [Fact]
    public void Remove_ShouldReturnFalseIfPieceNotInCollection()
    {
        // Arrange
        var collection = ChessBoardPiecesCollection.Whites(false);
        var piece = new Rook(ChessColor.White);

        // Act
        var result = collection.Remove(piece);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Clone_ShouldReturnExactCopyOfCollection()
    {
        // Arrange
        var collection = ChessBoardPiecesCollection.Whites();

        // Act
        var clone = collection.Clone();

        // Assert
        Assert.NotNull(clone);
        Assert.Equal(collection.Count, clone.Count);
        for (var i = 0; i < collection.Count; i++)
        {
            Assert.Equal(collection[i], clone[i]);
        }
    }

    [Fact]
    public void SetFrom_ShouldCopyPiecesFromAnotherCollection()
    {
        // Arrange
        var collection1 = ChessBoardPiecesCollection.Whites();
        var collection2 = ChessBoardPiecesCollection.Blacks(false);

        // Act
        collection2.SetFrom(collection1);

        // Assert
        Assert.Equal(collection1.Count, collection2.Count);
        for (var i = 0; i < collection1.Count; i++)
        {
            Assert.Equal(collection1[i], collection2[i]);
        }
    }

    [Fact]
    public void SetFrom_ShouldNotCopyPiecesFromNull()
    {
        // Arrange
        var collection = ChessBoardPiecesCollection.Blacks();

        // Act
        collection.SetFrom(null);

        // Assert
        Assert.NotEmpty(collection);
    }

    [Fact]
    public void IsAlive_ShouldReturnTrueIfPieceIsInCollection()
    {
        // Arrange
        var collection = ChessBoardPiecesCollection.Whites();
        var piece = collection.King;

        // Act
        var result = collection.IsAlive(piece);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsAlive_ShouldReturnFalseIfPieceIsNotInCollection()
    {
        // Arrange
        var collection = ChessBoardPiecesCollection.Whites(false);
        var piece = new Rook(ChessColor.White);

        // Act
        var result = collection.IsAlive(piece);

        // Assert
        Assert.False(result);
    }
}
