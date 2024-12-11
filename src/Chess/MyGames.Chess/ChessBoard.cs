// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using MyGames.Chess.Extensions;
using MyGames.Domain;
using MyGames.Domain.Extensions;
using MyNet.Utilities;

namespace MyGames.Chess
{
    public class ChessBoard : Board<ChessPiece>
    {
        public const int NumberOfRows = 8;
        public const int NumberOfColumns = 8;
        public const int WhiteKingRow = 7;
        public const int WhitePawnsRow = 6;
        public const int BlackKingRow = 0;
        public const int BlackPawnsRow = 1;
        private readonly Dictionary<ChessColor, ChessBoardPiecesCollection> _pieces;

        public ChessBoard() : this(
            (whites, blacks) =>
            {
                var whitesOriginalPieces = whites.GetOriginalPieces().ToList().To(x => x.ToDictionary(y => y, y => new BoardCoordinates(y is Pawn ? WhitePawnsRow : WhiteKingRow, x.IndexOf(y) % NumberOfColumns)))!;
                var blacksOriginalPieces = blacks.GetOriginalPieces().ToList().To(x => x.ToDictionary(y => y, y => new BoardCoordinates(y is Pawn ? BlackPawnsRow : BlackKingRow, x.IndexOf(y) % NumberOfColumns)))!;
                return whitesOriginalPieces.Union(blacksOriginalPieces).ToDictionary(x => x.Key, x => (x.Value.Row, x.Value.Column));
            })
        {
        }

        public ChessBoard(Func<ChessBoardPiecesCollection, ChessBoardPiecesCollection, IDictionary<ChessPiece, (int row, int column)>> newPiecesCreation)
             : this(ChessBoardPiecesCollection.Whites(false), ChessBoardPiecesCollection.Blacks(false), newPiecesCreation) { }

        private ChessBoard(ChessBoardPiecesCollection whites, ChessBoardPiecesCollection blacks, Func<ChessBoardPiecesCollection, ChessBoardPiecesCollection, IDictionary<ChessPiece, (int row, int column)>> newPiecesCreation)
            : base(NumberOfRows, NumberOfColumns)
        {
            _pieces = new()
            {
                { ChessColor.White, whites },
                { ChessColor.Black, blacks }
            };

            var newPieces = newPiecesCreation.Invoke(Whites, Blacks);
            newPieces.ForEach(x => InsertPiece(x.Key, new BoardCoordinates(x.Value.row, x.Value.column)));
        }

        public ChessBoardPiecesCollection Whites => GetPieces(ChessColor.White);

        public ChessBoardPiecesCollection Blacks => GetPieces(ChessColor.Black);

        public ChessBoardPiecesCollection GetPieces(ChessColor color) => _pieces[color];

        public static int GetStartRowOf(ChessColor color) => color == ChessColor.White ? WhiteKingRow : BlackKingRow;

        public static bool IsEnd(BoardCoordinates coordinates, ChessColor color) => coordinates.Row == GetStartRowOf(GetOpponentColor(color));

        public static ChessColor GetOpponentColor(ChessColor color) => ChessColor.White == color ? ChessColor.Black : ChessColor.White;

        internal bool Replace(ChessPiece oldPiece, ChessPiece newPiece) => TryGetCoordinates(oldPiece) is BoardCoordinates coordinates && RemovePiece(oldPiece) && InsertPiece(newPiece, coordinates);

        internal bool Move(ChessPiece piece, BoardCoordinates coordinates)
        {
            RemovePiece(coordinates);
            return MovePiece(piece, coordinates);
        }

        internal bool Remove(ChessPiece piece) => RemovePiece(piece);

        protected override bool InsertPiece(ChessPiece piece, BoardCoordinates coordinates, bool replaceIfTaken = true) => base.InsertPiece(piece, coordinates, replaceIfTaken) && GetPieces(piece.Color).Insert(piece);

        protected override bool RemovePiece(BoardCoordinates coordinates) => this.TryGetPiece(coordinates) is ChessPiece piece && base.RemovePiece(coordinates) && GetPieces(piece.Color).Remove(piece);

        public bool IsCheck(ChessColor color) => this.IsAttacked(_pieces[color].King);

        public bool IsCheckmate(ChessColor color) => IsCheck(color) && IsCheckAfterAnyMove(color);

        public bool IsStalemate(ChessColor color) => !IsCheck(color) && IsCheckAfterAnyMove(color);

        private bool IsCheckAfterAnyMove(ChessColor color)
        {
            // Get all pieces of the player
            var playerPieces = GetPieces(color);

            // Check if any piece has a legal move
            foreach (var piece in playerPieces)
            {
                var possibleMoves = piece.GetPossibleMoves(this);
                foreach (var move in possibleMoves)
                {
                    if (!this.IsCheckAfterMove(new ChessMove(piece, move)))
                        return false;
                }
            }

            return true;
        }

        protected override Board<ChessPiece> NewInstance(IDictionary<ChessPiece, BoardCoordinates> pieces)
            => new ChessBoard(Whites.Clone(), Blacks.Clone(), (x, y) => pieces.ToDictionary(z => z.Key, z => (z.Value.Row, z.Value.Column)));
    }
}
