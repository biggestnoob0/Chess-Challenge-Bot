using ChessChallenge.API;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessChallenge.Example
{
    // A simple bot that can spot mate in one, and always captures the most valuable piece it can.
    // Plays randomly otherwise.
    public class EvilBot : IChessBot
    {
        //// Piece values: null, pawn, knight, bishop, rook, queen, king
        //int[] pieceValues = { 0, 100, 300, 300, 500, 900, 10000 };

        //public Move Think(Board board, Timer timer)
        //{
        //    Move[] allMoves = board.GetLegalMoves();

        //    // Pick a random move to play if nothing better is found
        //    Random rng = new();
        //    Move moveToPlay = allMoves[rng.Next(allMoves.Length)];
        //    int highestValueCapture = 0;

        //    foreach (Move move in allMoves)
        //    {
        //        // Always play checkmate in one
        //        if (MoveIsCheckmate(board, move))
        //        {
        //            moveToPlay = move;
        //            break;
        //        }

        //        // Find highest value capture
        //        Piece capturedPiece = board.GetPiece(move.TargetSquare);
        //        int capturedPieceValue = pieceValues[(int)capturedPiece.PieceType];

        //        if (capturedPieceValue > highestValueCapture)
        //        {
        //            moveToPlay = move;
        //            highestValueCapture = capturedPieceValue;
        //        }
        //    }

        //    return moveToPlay;
        //}

        //// Test if this move gives checkmate
        //bool MoveIsCheckmate(Board board, Move move)
        //{
        //    board.MakeMove(move);
        //    bool isMate = board.IsInCheckmate();
        //    board.UndoMove(move);
        //    return isMate;
        //}
        int[] pieceValues = { 0, 100, 300, 300, 500, 900, 10000 };
        Random rnd = new();
        public Move Think(Board board, Timer timer)
        {
            Move[] moves = board.GetLegalMoves();

            Move nextMove = Move.NullMove;

            List<Move> sameWeightMoves = new();

            (Move move, int weight) bestScore = (Move.NullMove, -99999);
            for (int i = 0; i < moves.Length; i++)
            {
                board.MakeMove(moves[i]);

                if (board.IsInCheckmate())
                {
                    board.UndoMove(moves[i]);
                    return moves[i];
                }

                int weight = TakeWeightCycle(board, board.GetLegalMoves(), 0, false).weight;
                if (moves[i].IsCapture)
                {
                    weight += pieceValues[(int)moves[i].CapturePieceType];
                }
                if (weight > bestScore.weight)
                {
                    bestScore = (moves[i], weight);
                    sameWeightMoves.Clear();
                }
                else if (weight == bestScore.weight) sameWeightMoves.Add(moves[i]);

                board.UndoMove(moves[i]);
            }
            if (sameWeightMoves.Count > 0)
            {
                nextMove = sameWeightMoves[rnd.Next(sameWeightMoves.Count)];
            }
            else
            {
                nextMove = bestScore.move;
            }
            if (nextMove.IsPromotion)
            {
                nextMove = new Move(nextMove.StartSquare.Name + nextMove.TargetSquare.Name + 'q', board);
            }
            return nextMove;

        }
        int maxDepth = 2;
        (int weight, int bestMoveIndex) TakeWeightCycle(Board board, Move[] moves, int currentDepth, bool isAITurn)
        {

            List<int> decreases = new();
            foreach (Move move in moves)
            {
                if (isAITurn && move.IsCapture)
                {
                    decreases.Add(pieceValues[(int)move.CapturePieceType]);
                    continue;
                }
                else if (move.IsCapture)
                {
                    decreases.Add(-pieceValues[(int)move.CapturePieceType]);
                }
                else
                {
                    decreases.Add(0);
                }
            }
            int index = 0;
            if (currentDepth < maxDepth)
            {
                foreach (Move move in moves)
                {

                    board.MakeMove(move);
                    Move[] nextMoves = board.GetLegalMoves();
                    if (nextMoves.Length > 0)
                    {
                        (int bestWeight, int bestMoveIndex) = TakeWeightCycle(board, nextMoves, currentDepth + 1, !isAITurn);

                    }
                    board.UndoMove(move);
                    index++;
                }


            }
            if (decreases.Count > 0 && isAITurn) return (decreases.Max(), decreases.IndexOf(decreases.Max()));
            else if (decreases.Count > 0) return (decreases.Min(), decreases.IndexOf(decreases.Max()));
            else return (0, 0);
        }
    }
}