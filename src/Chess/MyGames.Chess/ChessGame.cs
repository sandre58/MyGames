// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using MyGames.Chess.Exceptions;
using MyGames.Domain;
using MyNet.Utilities;

namespace MyGames.Chess
{
    public class ChessGame : BoardGame<ChessBoard, IChessPlayer, ChessPiece, IChessMove, ChessPlayedMove>
    {
        public const int MaxMovesForStalemate = 50;

        private ChessResult _result;
        private readonly Dictionary<ChessColor, bool> _isStalemate = new()
        {
            { ChessColor.White, false },
            { ChessColor.Black, false }
        };
        private readonly Dictionary<ChessColor, bool> _isCheckmate = new()
        {
            { ChessColor.White, false },
            { ChessColor.Black, false }
        };
        private readonly Dictionary<ChessColor, bool> _isCheck = new()
        {
            { ChessColor.White, false },
            { ChessColor.Black, false }
        };

        public ChessGame(IChessPlayer whitePlayer, IChessPlayer blackPlayer) : this(new ChessBoard(), whitePlayer, blackPlayer) { }

        public ChessGame(ChessBoard board, IChessPlayer whitePlayer, IChessPlayer blackPlayer, int currentPlayerIndex = 0) : base(board, [whitePlayer, blackPlayer], currentPlayerIndex) { }

        public IChessPlayer WhitePlayer => Players[0];

        public IChessPlayer BlackPlayer => Players[1];

        public ChessBoardPiecesCollection Whites => Board.Whites;

        public ChessBoardPiecesCollection Blacks => Board.Blacks;

        protected override IChessPlayer? GetWinner()
            => _result == ChessResult.WhiteWin || IsCheckmate(BlackPlayer) ? WhitePlayer
            : _result == ChessResult.BlackWin || IsCheckmate(WhitePlayer) ? BlackPlayer
            : null;

        protected override bool IsDraw() => _result == ChessResult.Draw || HasStalemate();

        public void SetResult(ChessResult result)
        {
            _result = result;
            Winner = GetWinner();
        }

        public bool HasCheckmate() => _isCheckmate.Any(x => x.Value);

        public bool HasStalemate() => _isStalemate.Any(x => x.Value);

        public bool HasCheck() => _isCheck.Any(x => x.Value);

        public bool IsCheckmate(IChessPlayer player) => _isCheckmate[GetColor(player)];

        public bool IsStalemate(IChessPlayer player) => _isStalemate[GetColor(player)];

        public bool IsCheck(IChessPlayer player) => _isCheck[GetColor(player)];

        #region Players

        public ChessBoardPiecesCollection GetPieces(IChessPlayer player) => Board.GetPieces(GetColor(player));

        public ChessBoardPiecesCollection GetOpponentPieces(IChessPlayer player) => Board.GetPieces(GetOpponentColor(player));

        public IChessPlayer GetPlayer(ChessColor color)
            => color switch
            {
                ChessColor.White => WhitePlayer,
                ChessColor.Black => BlackPlayer,
                _ => throw new NotSupportedException()
            };

        public IChessPlayer GetOpponent(IChessPlayer player)
            => player == WhitePlayer ? BlackPlayer : player == BlackPlayer ? WhitePlayer : throw new NotSupportedException();

        public ChessColor GetColor(IChessPlayer player)
            => player == WhitePlayer ? ChessColor.White : player == BlackPlayer ? ChessColor.Black : throw new NotSupportedException();

        public ChessColor GetOpponentColor(IChessPlayer player)
            => player == WhitePlayer ? ChessColor.Black : player == BlackPlayer ? ChessColor.White : throw new NotSupportedException();

        public static ChessColor GetOpponentColor(ChessColor color)
            => color == ChessColor.White ? ChessColor.Black : color == ChessColor.Black ? ChessColor.White : throw new NotSupportedException();

        #endregion

        #region Make Move

        public bool HasMoved(ChessPiece piece) => History.Any(x => x.Move.Piece == piece);

        protected override IChessMove NextMoveOfCurrentPlayer() => GetCurrentPlayer().NextMove(this);

        protected override ChessPlayedMove ApplyMove(IChessMove move, IChessPlayer player)
            => GetColor(player) == move.Piece.Color && move.IsValid(this)
                ? move.Apply(Board, player)
                : throw new ChessInvalidMoveException(player, move, _isCheck[GetColor(player)]);

        protected override void AnalyseBoard()
        {
            _isCheck[ChessColor.White] = Board.IsCheck(ChessColor.White);
            _isCheck[ChessColor.Black] = Board.IsCheck(ChessColor.Black);

            var isStalemate = IsStalemate();
            _isStalemate[ChessColor.White] = isStalemate || Board.IsStalemate(ChessColor.White);
            _isStalemate[ChessColor.Black] = isStalemate || Board.IsStalemate(ChessColor.Black);

            _isCheckmate[ChessColor.White] = Board.IsCheckmate(ChessColor.White);
            _isCheckmate[ChessColor.Black] = Board.IsCheckmate(ChessColor.Black);

            base.AnalyseBoard();
        }

        private bool IsStalemate()
        {
            if (Whites.Count == 1 && Blacks.Count == 1)
                return true;

            // Checks if the number of moves without capture or pawn movement has reached the limit of 50 moves
            var movesWithoutCaptureOrPawnMove = History.Reverse<HistoryMove<IChessPlayer, ChessBoard, IChessMove, ChessPlayedMove>>()
                                                       .TakeWhile(x => x.Move.Piece is Pawn || x.Move.TakenPiece is not null)
                                                       .Count();

            if (movesWithoutCaptureOrPawnMove >= MaxMovesForStalemate)
                return true;

            // Check for threefold repetition
            var positionCounts = new Dictionary<string, int>();

            foreach (var move in History)
            {
                var boardState = move.Board.ToString(); // Assuming Board.ToString() gives a unique representation of the board state
                if (positionCounts.TryGetValue(boardState, out var value))
                {
                    positionCounts[boardState] = ++value;
                    if (value == 3)
                        return true;
                }
                else
                {
                    positionCounts[boardState] = 1;
                }
            }

            return false;
        }

        #endregion

        protected override BoardGame<ChessBoard, IChessPlayer, ChessPiece, IChessMove, ChessPlayedMove> NewInstance(ChessBoard board)
        {
            var instance = new ChessGame(board.Clone().CastIn<ChessBoard>(), WhitePlayer, BlackPlayer);

            instance._isCheck[ChessColor.White] = _isCheck[ChessColor.White];
            instance._isCheck[ChessColor.Black] = _isCheck[ChessColor.Black];

            instance._isStalemate[ChessColor.White] = _isStalemate[ChessColor.White];
            instance._isStalemate[ChessColor.Black] = _isStalemate[ChessColor.Black];

            instance._isCheckmate[ChessColor.White] = _isCheckmate[ChessColor.White];
            instance._isCheckmate[ChessColor.Black] = _isCheckmate[ChessColor.Black];

            return instance;
        }
    }
}
