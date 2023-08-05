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


        //    int[] pieceValues = { 0, 100, 300, 300, 500, 900, 10000 };
        //    Random rnd = new();
        //    public Move Think(Board board, Timer timer)
        //    {
        //        Move[] moves = board.GetLegalMoves();

        //        Move nextMove = Move.NullMove;

        //        List<Move> sameWeightMoves = new();

        //        (Move move, int weight) bestScore = (Move.NullMove, -99999);
        //        for (int i = 0; i < moves.Length; i++)
        //        {
        //            board.MakeMove(moves[i]);

        //            if (board.IsInCheckmate())
        //            {
        //                board.UndoMove(moves[i]);
        //                return moves[i];
        //            }

        //            int weight = TakeWeightCycle(board, board.GetLegalMoves(), 0, false).weight;
        //            if (moves[i].IsCapture)
        //            {
        //                weight += pieceValues[(int)moves[i].CapturePieceType];
        //            }
        //            if (weight > bestScore.weight)
        //            {
        //                bestScore = (moves[i], weight);
        //                sameWeightMoves.Clear();
        //            }
        //            else if (weight == bestScore.weight) sameWeightMoves.Add(moves[i]);

        //            board.UndoMove(moves[i]);
        //        }
        //        if (sameWeightMoves.Count > 0)
        //        {
        //            nextMove = sameWeightMoves[rnd.Next(sameWeightMoves.Count)];
        //        }
        //        else
        //        {
        //            nextMove = bestScore.move;
        //        }
        //        if (nextMove.IsPromotion)
        //        {
        //            nextMove = new Move(nextMove.StartSquare.Name + nextMove.TargetSquare.Name + 'q', board);
        //        }
        //        return nextMove;

        //    }
        //    int maxDepth = 2;
        //    (int weight, int bestMoveIndex) TakeWeightCycle(Board board, Move[] moves, int currentDepth, bool isAITurn)
        //    {

        //        List<int> decreases = new();
        //        foreach (Move move in moves)
        //        {
        //            if (isAITurn && move.IsCapture)
        //            {
        //                decreases.Add(pieceValues[(int)move.CapturePieceType]);
        //                continue;
        //            }
        //            else if (move.IsCapture)
        //            {
        //                decreases.Add(-pieceValues[(int)move.CapturePieceType]);
        //            }
        //            else
        //            {
        //                decreases.Add(0);
        //            }
        //        }
        //        int index = 0;
        //        if (currentDepth < maxDepth)
        //        {
        //            foreach (Move move in moves)
        //            {

        //                board.MakeMove(move);
        //                Move[] nextMoves = board.GetLegalMoves();
        //                if (nextMoves.Length > 0)
        //                {
        //                    (int bestWeight, int bestMoveIndex) = TakeWeightCycle(board, nextMoves, currentDepth + 1, !isAITurn);

        //                }
        //                board.UndoMove(move);
        //                index++;
        //            }


        //        }
        //        if (decreases.Count > 0 && isAITurn) return (decreases.Max(), decreases.IndexOf(decreases.Max()));
        //        else if (decreases.Count > 0) return (decreases.Min(), decreases.IndexOf(decreases.Max()));
        //        else return (0, 0);
        //    }


        int[] pieceValues = { 0, 100, 300, 300, 500, 900, 10000 };
        Square[] centralSquares = new Square[] { new("d4"), new("d5"), new("e4"), new("e5") };
        Random rnd = new();
        public Move Think(Board board, Timer timer)
        {
            Move[] moves = board.GetLegalMoves();

            Move nextMove = Move.NullMove;

            int indexOfMove = 0;
            TakeWeightCycle(board, moves, 0, true, ref indexOfMove);
            //for (int i = 0; i < moves.Length; i++)
            //{
            //    board.MakeMove(moves[i]);

            //    if (board.IsInCheckmate())
            //    {
            //        board.UndoMove(moves[i]);
            //        return moves[i];
            //    }

            //    int weight = TakeWeightCycle(board, board.GetLegalMoves(), 0, false, ref depthWeights, alpha: bestScore.weight);
            //    if (moves[i].IsCapture)
            //    {
            //        weight += pieceValues[(int)moves[i].CapturePieceType];
            //    }
            //    if (weight > bestScore.weight)
            //    {
            //        bestScore = (moves[i], weight);
            //        sameWeightMoves.Clear();
            //    }
            //    else if (weight == bestScore.weight) sameWeightMoves.Add(moves[i]);

            //    board.UndoMove(moves[i]);
            //}
            nextMove = moves[indexOfMove];
            if (nextMove.IsPromotion)
            {
                nextMove = new Move(nextMove.StartSquare.Name + nextMove.TargetSquare.Name + 'q', board);
            }
            return nextMove;

        }
        int maxDepth = 4;
        int[] checkAmount = new int[] { 999, 999, 16, 12, 8 };
        int TakeWeightCycle(Board board, Move[] moves, int currentDepth, bool isAITurn, ref int moveIndex, int checkTopAmount = 999, int alpha = -99999, int beta = 99999, int currentScore = 0)
        {
            List<(int weight, int index)> decreases = new();
            int ind = 0;
            foreach (Move move in moves)
            {
                board.MakeMove(move);
                if (board.IsInCheckmate())
                {
                    board.UndoMove(move);
                    if (currentDepth == 0) moveIndex = ind;
                    return isAITurn ? 10000 : -10000;
                }

                if (isAITurn && move.IsCapture)
                {
                    decreases.Add((pieceValues[(int)move.CapturePieceType], ind));
                }
                else if (move.IsCapture)
                {
                    decreases.Add((-pieceValues[(int)move.CapturePieceType], ind));
                }
                else
                {
                    if (centralSquares.Contains(move.TargetSquare)) decreases.Add((isAITurn ? 5 : -5, ind));
                    else decreases.Add((0, ind));
                }

                board.UndoMove(move);
                ind++;
            }
            if (decreases.Count == 0) return 0;
            decreases = isAITurn ? decreases.OrderByDescending(x => x.weight).ToList() : decreases.OrderByDescending(x => x.weight).ToList();
            if (checkTopAmount < decreases.Count && decreases[checkTopAmount] == decreases[0]) checkTopAmount += 5;
            int index = 0;
            if (currentDepth < maxDepth)
            {
                int currentBest = isAITurn ? -99999 : 99999;
                int weight = isAITurn ? -99999 : 99999;
                int bestIndex = 0;
                List<int> tiedBestEntires = new();
                bool cutoff = false;
                for (int i = 0; i < checkTopAmount; i++)
                {
                    if (decreases.Count <= i) break;
                    board.MakeMove(moves[decreases[i].index]);
                    Move[] nextMoves = board.GetLegalMoves();
                    int nextSearchAmount = checkAmount[Math.Clamp(currentDepth + 1, 0, checkAmount.Length - 1)];
                    int weight2 = TakeWeightCycle(board, nextMoves, currentDepth + 1, !isAITurn, ref moveIndex, nextSearchAmount, alpha, beta, currentScore + decreases[i].weight);
                    if (isAITurn) weight = Math.Max(weight2, weight);
                    else weight = Math.Min(weight2, weight);

                    if (isAITurn && weight > alpha)
                    {
                        currentBest = weight;
                        bestIndex = decreases[i].index;
                        tiedBestEntires.Clear();
                        tiedBestEntires.Add(bestIndex);
                        alpha = weight;
                    }
                    else if (!isAITurn && weight < beta)
                    {
                        currentBest = weight;
                        bestIndex = decreases[i].index;
                        tiedBestEntires.Clear();
                        tiedBestEntires.Add(bestIndex);
                        beta = weight;
                    }

                    if (isAITurn)
                    {
                        if (weight >= beta && currentDepth != 0) { board.UndoMove(moves[decreases[i].index]); return weight; }
                        else if (weight >= beta) { cutoff = true; break; }
                    }
                    else
                    {
                        if (weight <= alpha && currentDepth != 0) { board.UndoMove(moves[decreases[i].index]); return weight; }
                        else if (weight <= alpha) { cutoff = true; break; }
                    }
                    board.UndoMove(moves[decreases[i].index]);
                    index++;


                }
                if (currentDepth == 0)
                {
                    moveIndex = tiedBestEntires[rnd.Next(tiedBestEntires.Count)];
                }
                return weight;
            }
            else
            {
                return currentScore + decreases[0].weight;
            }

        }

    }


}