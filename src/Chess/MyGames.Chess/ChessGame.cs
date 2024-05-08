// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Linq;
using MyGames.Common;
using MyGames.Common.Exceptions;

namespace MyGames.Chess
{
    public class ChessGame : BoardGame<ChessBoard, ChessPlayer, ChessPiece, ChessMove, ChessHistoryMove>
    {
        public ChessGame(ChessPlayer whitePlayer, ChessPlayer blackPlayer) : base(new ChessBoard(), [whitePlayer, blackPlayer]) { }

        public ChessPlayer WhitePlayer => Players[0];

        public ChessPlayer BlackPlayer => Players[0];

        protected override ChessPlayer? GetWinner() => IsInCheckmate(WhitePlayer) ? WhitePlayer : IsInCheckmate(BlackPlayer) ? BlackPlayer : null;

        protected override bool GetIsOver() => IsInStalemate(WhitePlayer) || IsInStalemate(BlackPlayer);

        protected override ChessHistoryMove MakeMove(ChessPlayer player, ChessMove move)
            => (player == WhitePlayer && move.Piece.Color == ChessColor.White || player == BlackPlayer && move.Piece.Color == ChessColor.Black) && Board.Move(move.Piece, move.Row, move.Column) ? new(player, move) : throw new InvalidMoveException(player, move);

        protected override ChessHistoryMove CancelMove(ChessHistoryMove historyMove)
            => throw new System.NotImplementedException();

        protected override ChessMove GetMoveOfPlayer(ChessPlayer player) => player.NextMove(this);

        public static bool IsInCheck(ChessPlayer player) => false;

        public static bool IsInCheckmate(ChessPlayer player) => false;

        public static bool IsInStalemate(ChessPlayer player) => false;

        protected override BoardGame<ChessBoard, ChessPlayer, ChessPiece, ChessMove, ChessHistoryMove> NewInstance() => new ChessGame(WhitePlayer, BlackPlayer);
    }
}
