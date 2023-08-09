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


        //int[] pieceValues = { 0, 100, 300, 300, 500, 900, 10000 };
        //Random rnd = new();
        //public Move Think(Board board, Timer timer)
        //{
        //    Move[] moves = board.GetLegalMoves();

        //    Move nextMove = Move.NullMove;

        //    List<Move> sameWeightMoves = new();

        //    (Move move, int weight) bestScore = (Move.NullMove, -99999);
        //    for (int i = 0; i < moves.Length; i++)
        //    {
        //        board.MakeMove(moves[i]);

        //        if (board.IsInCheckmate())
        //        {
        //            board.UndoMove(moves[i]);
        //            return moves[i];
        //        }

        //        int weight = TakeWeightCycle(board, board.GetLegalMoves(), 0, false).weight;
        //        if (moves[i].IsCapture)
        //        {
        //            weight += pieceValues[(int)moves[i].CapturePieceType];
        //        }
        //        if (weight > bestScore.weight)
        //        {
        //            bestScore = (moves[i], weight);
        //            sameWeightMoves.Clear();
        //        }
        //        else if (weight == bestScore.weight) sameWeightMoves.Add(moves[i]);

        //        board.UndoMove(moves[i]);
        //    }
        //    if (sameWeightMoves.Count > 0)
        //    {
        //        nextMove = sameWeightMoves[rnd.Next(sameWeightMoves.Count)];
        //    }
        //    else
        //    {
        //        nextMove = bestScore.move;
        //    }
        //    if (nextMove.IsPromotion)
        //    {
        //        nextMove = new Move(nextMove.StartSquare.Name + nextMove.TargetSquare.Name + 'q', board);
        //    }
        //    return nextMove;

        //}
        //int maxDepth = 2;
        //(int weight, int bestMoveIndex) TakeWeightCycle(Board board, Move[] moves, int currentDepth, bool isAITurn)
        //{

        //    List<int> decreases = new();
        //    foreach (Move move in moves)
        //    {
        //        if (isAITurn && move.IsCapture)
        //        {
        //            decreases.Add(pieceValues[(int)move.CapturePieceType]);
        //            continue;
        //        }
        //        else if (move.IsCapture)
        //        {
        //            decreases.Add(-pieceValues[(int)move.CapturePieceType]);
        //        }
        //        else
        //        {
        //            decreases.Add(0);
        //        }
        //    }
        //    int index = 0;
        //    if (currentDepth < maxDepth)
        //    {
        //        foreach (Move move in moves)
        //        {

        //            board.MakeMove(move);
        //            Move[] nextMoves = board.GetLegalMoves();
        //            if (nextMoves.Length > 0)
        //            {
        //                (int bestWeight, int bestMoveIndex) = TakeWeightCycle(board, nextMoves, currentDepth + 1, !isAITurn);

        //            }
        //            board.UndoMove(move);
        //            index++;
        //        }


        //    }
        //    if (decreases.Count > 0 && isAITurn) return (decreases.Max(), decreases.IndexOf(decreases.Max()));
        //    else if (decreases.Count > 0) return (decreases.Min(), decreases.IndexOf(decreases.Max()));
        //    else return (0, 0);
        //}


        //int[] pieceValues = { 0, 100, 300, 300, 500, 900, 10000 };
        //Square[] centralSquares = new Square[] { new("d4"), new("d5"), new("e4"), new("e5") };
        //Random rnd = new();
        //public Move Think(Board board, Timer timer)
        //{
        //    Move[] moves = board.GetLegalMoves();

        //    Move nextMove = Move.NullMove;

        //    int indexOfMove = 0;
        //    TakeWeightCycle(board, moves, 0, true, ref indexOfMove);
        //    //for (int i = 0; i < moves.Length; i++)
        //    //{
        //    //    board.MakeMove(moves[i]);

        //    //    if (board.IsInCheckmate())
        //    //    {
        //    //        board.UndoMove(moves[i]);
        //    //        return moves[i];
        //    //    }

        //    //    int weight = TakeWeightCycle(board, board.GetLegalMoves(), 0, false, ref depthWeights, alpha: bestScore.weight);
        //    //    if (moves[i].IsCapture)
        //    //    {
        //    //        weight += pieceValues[(int)moves[i].CapturePieceType];
        //    //    }
        //    //    if (weight > bestScore.weight)
        //    //    {
        //    //        bestScore = (moves[i], weight);
        //    //        sameWeightMoves.Clear();
        //    //    }
        //    //    else if (weight == bestScore.weight) sameWeightMoves.Add(moves[i]);

        //    //    board.UndoMove(moves[i]);
        //    //}
        //    nextMove = moves[indexOfMove];
        //    if (nextMove.IsPromotion)
        //    {
        //        nextMove = new Move(nextMove.StartSquare.Name + nextMove.TargetSquare.Name + 'q', board);
        //    }
        //    return nextMove;

        //}
        //int maxDepth = 4;
        //int[] checkAmount = new int[] { 999, 999, 16, 12, 8 };
        //int TakeWeightCycle(Board board, Move[] moves, int currentDepth, bool isAITurn, ref int moveIndex, int checkTopAmount = 999, int alpha = -99999, int beta = 99999, int currentScore = 0)
        //{
        //    List<(int weight, int index)> decreases = new();
        //    int ind = 0;
        //    foreach (Move move in moves)
        //    {
        //        board.MakeMove(move);
        //        if (board.IsInCheckmate())
        //        {
        //            board.UndoMove(move);
        //            if (currentDepth == 0) moveIndex = ind;
        //            return isAITurn ? 10000 : -10000;
        //        }

        //        if (isAITurn && move.IsCapture)
        //        {
        //            decreases.Add((pieceValues[(int)move.CapturePieceType], ind));
        //        }
        //        else if (move.IsCapture)
        //        {
        //            decreases.Add((-pieceValues[(int)move.CapturePieceType], ind));
        //        }
        //        else
        //        {
        //            if (centralSquares.Contains(move.TargetSquare)) decreases.Add((isAITurn ? 5 : -5, ind));
        //            else decreases.Add((0, ind));
        //        }

        //        board.UndoMove(move);
        //        ind++;
        //    }
        //    if (decreases.Count == 0) return 0;
        //    decreases = isAITurn ? decreases.OrderByDescending(x => x.weight).ToList() : decreases.OrderByDescending(x => x.weight).ToList();
        //    if (checkTopAmount < decreases.Count && decreases[checkTopAmount] == decreases[0]) checkTopAmount += 5;
        //    int index = 0;
        //    if (currentDepth < maxDepth)
        //    {
        //        int currentBest = isAITurn ? -99999 : 99999;
        //        int weight = isAITurn ? -99999 : 99999;
        //        int bestIndex = 0;
        //        List<int> tiedBestEntires = new();
        //        bool cutoff = false;
        //        for (int i = 0; i < checkTopAmount; i++)
        //        {
        //            if (decreases.Count <= i) break;
        //            board.MakeMove(moves[decreases[i].index]);
        //            Move[] nextMoves = board.GetLegalMoves();
        //            int nextSearchAmount = checkAmount[Math.Clamp(currentDepth + 1, 0, checkAmount.Length - 1)];
        //            int weight2 = TakeWeightCycle(board, nextMoves, currentDepth + 1, !isAITurn, ref moveIndex, nextSearchAmount, alpha, beta, currentScore + decreases[i].weight);
        //            if (isAITurn) weight = Math.Max(weight2, weight);
        //            else weight = Math.Min(weight2, weight);

        //            if (isAITurn && weight > alpha)
        //            {
        //                currentBest = weight;
        //                bestIndex = decreases[i].index;
        //                tiedBestEntires.Clear();
        //                tiedBestEntires.Add(bestIndex);
        //                alpha = weight;
        //            }
        //            else if (!isAITurn && weight < beta)
        //            {
        //                currentBest = weight;
        //                bestIndex = decreases[i].index;
        //                tiedBestEntires.Clear();
        //                tiedBestEntires.Add(bestIndex);
        //                beta = weight;
        //            }

        //            if (isAITurn)
        //            {
        //                if (weight >= beta && currentDepth != 0) { board.UndoMove(moves[decreases[i].index]); return weight; }
        //                else if (weight >= beta) { cutoff = true; break; }
        //            }
        //            else
        //            {
        //                if (weight <= alpha && currentDepth != 0) { board.UndoMove(moves[decreases[i].index]); return weight; }
        //                else if (weight <= alpha) { cutoff = true; break; }
        //            }
        //            board.UndoMove(moves[decreases[i].index]);
        //            index++;


        //        }
        //        if (currentDepth == 0)
        //        {
        //            moveIndex = tiedBestEntires[rnd.Next(tiedBestEntires.Count)];
        //        }
        //        return weight;
        //    }
        //    else
        //    {
        //        return currentScore + decreases[0].weight;
        //    }

        //}

        //    int[] pieceValues = { 0, 100, 300, 300, 500, 900, 10000 };
        //    Square[] centralSquares = new Square[] { new("d4"), new("d5"), new("e4"), new("e5") };
        //    Random rnd = new();
        //    public Move Think(Board board, Timer timer)
        //    {
        //        Move[] moves = board.GetLegalMoves();

        //        Move nextMove = Move.NullMove;

        //        int indexOfMove = 0;
        //        TakeWeightCycle(board, moves, 0, true, ref indexOfMove);
        //        //for (int i = 0; i < moves.Length; i++)
        //        //{
        //        //    board.MakeMove(moves[i]);

        //        //    if (board.IsInCheckmate())
        //        //    {
        //        //        board.UndoMove(moves[i]);
        //        //        return moves[i];
        //        //    }

        //        //    int weight = TakeWeightCycle(board, board.GetLegalMoves(), 0, false, ref depthWeights, alpha: bestScore.weight);
        //        //    if (moves[i].IsCapture)
        //        //    {
        //        //        weight += pieceValues[(int)moves[i].CapturePieceType];
        //        //    }
        //        //    if (weight > bestScore.weight)
        //        //    {
        //        //        bestScore = (moves[i], weight);
        //        //        sameWeightMoves.Clear();
        //        //    }
        //        //    else if (weight == bestScore.weight) sameWeightMoves.Add(moves[i]);

        //        //    board.UndoMove(moves[i]);
        //        //}
        //        nextMove = moves[indexOfMove];
        //        if (nextMove.IsPromotion)
        //        {
        //            nextMove = new Move(nextMove.StartSquare.Name + nextMove.TargetSquare.Name + 'q', board);
        //        }
        //        return nextMove;

        //    }
        //    int maxDepth = 5;
        //    //int[] checkAmount = new int[] { 999, 999, 25, 18, 12 };
        //    int[] checkAmount = new int[] { 999, 999, 20, 35, 9, 15, 3 };
        //    int TakeWeightCycle(Board board, Move[] moves, int currentDepth, bool isAITurn, ref int moveIndex, int checkTopAmount = 999, int alpha = -99999, int beta = 99999, int currentScore = 0)
        //    {
        //        List<(int weight, int index)> decreases = new();
        //        int ind = 0;
        //        foreach (Move move in moves)
        //        {
        //            int totalScore = 0;
        //            board.MakeMove(move);
        //            if (board.GameRepetitionHistory.Length >= 4)
        //            {
        //                int repeatCount = 0;
        //                for (int i = 0; i < board.GameRepetitionHistory.Length; i++)
        //                {
        //                    if (board.ZobristKey == board.GameRepetitionHistory[i]) repeatCount++;
        //                }
        //                if (repeatCount > 1) totalScore -= 25;
        //            }
        //            if (board.IsInCheckmate())
        //            {
        //                board.UndoMove(move);
        //                if (currentDepth == 0) moveIndex = ind;
        //                return isAITurn ? 10000 : -10000;
        //            }

        //            if (isAITurn && move.IsCapture)
        //            {
        //                totalScore += pieceValues[(int)move.CapturePieceType];
        //            }
        //            else if (move.IsCapture)
        //            {
        //                totalScore += -pieceValues[(int)move.CapturePieceType];
        //            }
        //            else
        //            {
        //                if (centralSquares.Contains(move.TargetSquare)) totalScore += isAITurn ? 5 : -5;
        //            }
        //            decreases.Add((totalScore, ind));

        //            board.UndoMove(move);
        //            ind++;
        //        }
        //        if (decreases.Count == 0) return 0;
        //        decreases = isAITurn ? decreases.OrderByDescending(x => x.weight).ToList() : decreases.OrderBy(x => x.weight).ToList();
        //        if (checkTopAmount < decreases.Count && decreases[checkTopAmount] == decreases[0]) checkTopAmount += 5;
        //        int index = 0;
        //        if (currentDepth < maxDepth)
        //        {
        //            int currentBest = isAITurn ? -99999 : 99999;
        //            int weight = isAITurn ? -99999 : 99999;
        //            int bestIndex = 0;
        //            List<int> tiedBestEntires = new();
        //            bool cutoff = false;
        //            for (int i = 0; i < checkTopAmount; i++)
        //            {
        //                if (decreases.Count <= i) break;
        //                board.MakeMove(moves[decreases[i].index]);
        //                Move[] nextMoves = board.GetLegalMoves();
        //                int nextSearchAmount = checkAmount[Math.Clamp(currentDepth + 1, 0, checkAmount.Length - 1)];
        //                int weight2 = TakeWeightCycle(board, nextMoves, currentDepth + 1, !isAITurn, ref moveIndex, nextSearchAmount, alpha, beta, currentScore + decreases[i].weight);
        //                if (isAITurn) weight = Math.Max(weight2, weight);
        //                else weight = Math.Min(weight2, weight);

        //                if (isAITurn && weight > alpha)
        //                {
        //                    currentBest = weight;
        //                    bestIndex = decreases[i].index;
        //                    tiedBestEntires.Clear();
        //                    tiedBestEntires.Add(bestIndex);
        //                    alpha = weight;
        //                }
        //                else if (!isAITurn && weight < beta)
        //                {
        //                    currentBest = weight;
        //                    bestIndex = decreases[i].index;
        //                    tiedBestEntires.Clear();
        //                    tiedBestEntires.Add(bestIndex);
        //                    beta = weight;
        //                }

        //                if (isAITurn)
        //                {
        //                    if (weight >= beta && currentDepth != 0) { board.UndoMove(moves[decreases[i].index]); return weight; }
        //                    else if (weight >= beta) { cutoff = true; break; }
        //                }
        //                else
        //                {
        //                    if (weight <= alpha && currentDepth != 0) { board.UndoMove(moves[decreases[i].index]); return weight; }
        //                    else if (weight <= alpha) { cutoff = true; break; }
        //                }
        //                board.UndoMove(moves[decreases[i].index]);
        //                index++;


        //            }
        //            if (currentDepth == 0)
        //            {
        //                moveIndex = tiedBestEntires[rnd.Next(tiedBestEntires.Count)];
        //            }
        //            return weight;
        //        }
        //        else
        //        {
        //            return currentScore + decreases[0].weight;
        //        }

        //    }

        //int[] pieceValues = { 0, 100, 300, 300, 500, 900, 10000 };
        //Square[] centralSquares = new Square[] { new("d4"), new("d5"), new("e4"), new("e5") };
        //Dictionary<ulong, (int, int, ulong)> cachedStates = new();
        //Random rnd = new();
        //public Move Think(Board board, Timer timer)
        //{
        //    Move[] moves = board.GetLegalMoves();

        //    int indexOfMove = 0;
        //    TakeWeightCycle(board, moves, 0, true, ref indexOfMove);
        //    cachedStates.Clear();

        //    return moves[indexOfMove];

        //}
        //int maxDepth = 6;
        //int[] checkAmount = new int[] { 999, 999, 20, 30, 6, 12, 3 };
        //int maxCountDepthInc = 12;
        //int pieceSum = 0;
        //float pawnSum = 0;
        //int TakeWeightCycle(Board board, Move[] moves, int currentDepth, bool isAITurn, ref int moveIndex, int checkTopAmount = 999, int alpha = -99999, int beta = 99999, int currentScore = 0)
        //{
        //    if (cachedStates.Count > 1000000) cachedStates.Clear();
        //    int depthInc = 0;
        //    if (currentDepth == 0)
        //    {
        //        pieceSum = board.GetAllPieceLists().Sum(x => x.Count);
        //        pawnSum = board.GetPieceList(PieceType.Pawn, true).Count + board.GetPieceList(PieceType.Pawn, false).Count;
        //        pawnSum /= 4;
        //        if (pieceSum < 7) depthInc = 3;
        //        else if (pieceSum <= 9) depthInc = 2;
        //        else if (pieceSum <= maxCountDepthInc) depthInc = 1;
        //    }
        //    maxDepth += depthInc;
        //    List<(int weight, int index)> decreases = new();
        //    int ind = 0;
        //    foreach (Move move in moves)
        //    {
        //        if (move.IsPromotion && move.PromotionPieceType != PieceType.Queen) { ind++; continue; }
        //        int totalScore = 0;
        //        board.MakeMove(move);

        //        if (board.IsInCheckmate())
        //        {
        //            if (currentDepth == 0 && board.GetAllPieceLists().Sum(x => x.Count) <= maxCountDepthInc) maxDepth -= depthInc;
        //            board.UndoMove(move);
        //            if (currentDepth == 0) moveIndex = ind;
        //            return isAITurn ? 10000 : -10000;
        //        }

        //        if (pieceSum - pawnSum < maxCountDepthInc && move.MovePieceType == PieceType.Pawn) totalScore += isAITurn ? 35 : -35;

        //        if (board.GameRepetitionHistory.Length >= 4)
        //        {
        //            int repeatCount = 0;
        //            for (int i = 0; i < board.GameRepetitionHistory.Length; i++)
        //            {
        //                if (board.ZobristKey == board.GameRepetitionHistory[i]) repeatCount++;
        //            }
        //            if (repeatCount > 3) totalScore -= 60;
        //            else if (repeatCount > 1) totalScore -= 40;
        //            else if (repeatCount == 1) totalScore -= 15;
        //        }
        //        if (board.IsInCheck())
        //        {
        //            totalScore += isAITurn ? 20 : -20;
        //        }
        //        else if (move.MovePieceType == PieceType.King && isAITurn) totalScore -= 15;
        //        if (move.IsCastles)
        //        {
        //            totalScore += isAITurn ? 150 : -150;
        //        }
        //        if (move.IsPromotion)
        //        {
        //            totalScore += isAITurn ? 210 : -210;
        //        }
        //        if (move.IsCapture)
        //        {
        //            int val = pieceValues[(int)move.CapturePieceType];
        //            totalScore += isAITurn ? val : -val;
        //        }

        //        if (centralSquares.Contains(move.TargetSquare) && pieceSum > maxCountDepthInc) totalScore += isAITurn ? 5 : -5;

        //        decreases.Add((totalScore, ind));
        //        board.UndoMove(move);
        //        ind++;
        //    }
        //    if (decreases.Count == 0) return 0;
        //    decreases = isAITurn ? decreases.OrderByDescending(x => x.weight).ToList() : decreases.OrderBy(x => x.weight).ToList();
        //    if (checkTopAmount < decreases.Count && decreases[checkTopAmount] == decreases[0]) checkTopAmount += 5;
        //    int index = 0;
        //    if (currentDepth < maxDepth)
        //    {
        //        int weight = isAITurn ? -99999 : 99999;
        //        int bestIndex = 0;
        //        List<int> tiedBestEntires = new();
        //        for (int i = 0; i < checkTopAmount; i++)
        //        {
        //            if (decreases.Count <= i) break;
        //            board.MakeMove(moves[decreases[i].index]);
        //            Move[] nextMoves = board.GetLegalMoves();
        //            int nextSearchAmount = checkAmount[Math.Clamp(currentDepth + 1, 0, checkAmount.Length - 1)];
        //            int weight2;
        //            if (currentDepth > 2 && cachedStates.ContainsKey(board.ZobristKey) && cachedStates[board.ZobristKey].Item2 == currentDepth && cachedStates[board.ZobristKey].Item3 == board.AllPiecesBitboard)
        //                weight2 = cachedStates[board.ZobristKey].Item1;
        //            else
        //            {
        //                weight2 = TakeWeightCycle(board, nextMoves, currentDepth + 1, !isAITurn, ref moveIndex, nextSearchAmount, alpha, beta, currentScore + decreases[i].weight);
        //                if (!cachedStates.ContainsKey(board.ZobristKey) && currentDepth > 2)
        //                {
        //                    cachedStates.Add(board.ZobristKey, (weight2, currentDepth, board.AllPiecesBitboard));
        //                }
        //            }
        //            if (isAITurn) weight = Math.Max(weight2, weight);
        //            else weight = Math.Min(weight2, weight);

        //            if (isAITurn && weight > alpha)
        //            {
        //                bestIndex = i;
        //                tiedBestEntires.Clear();
        //                tiedBestEntires.Add(bestIndex);
        //                alpha = weight;
        //            }
        //            else if (!isAITurn && weight < beta)
        //            {
        //                bestIndex = i;
        //                tiedBestEntires.Clear();
        //                tiedBestEntires.Add(bestIndex);
        //                beta = weight;
        //            }
        //            board.UndoMove(moves[decreases[i].index]);
        //            if (isAITurn)
        //            {
        //                if (weight >= beta && currentDepth != 0) { return weight; }
        //                else if (weight >= beta) break;
        //            }
        //            else
        //            {
        //                if (weight <= alpha && currentDepth != 0) { return weight; }
        //                else if (weight <= alpha) break;
        //            }
        //            index++;

        //        }
        //        if (currentDepth == 0)
        //        {
        //            moveIndex = tiedBestEntires.Count > 0 ? tiedBestEntires[rnd.Next(tiedBestEntires.Count)] : decreases[bestIndex].index;
        //            if (board.GetAllPieceLists().Sum(x => x.Count) <= maxCountDepthInc) maxDepth -= depthInc;
        //        }
        //        return weight;
        //    }
        //    else
        //    {
        //        return currentScore + decreases[0].weight;
        //    }

        //}

        //int[] pieceValues = { 0, 100, 300, 300, 500, 900, 10000 };
        //Square[] centralSquares = new Square[] { new("d4"), new("d5"), new("e4"), new("e5") };
        ////Dictionary<ulong, (int, int, ulong)> cachedStates = new();
        //Random rnd = new();
        //public Move Think(Board board, Timer timer)
        //{
        //    Move[] moves = board.GetLegalMoves();
        //    int indexOfMove = 0;
        //    TakeWeightCycle(board, moves, 0, true, ref indexOfMove);
        //    //cachedStates.Clear();
        //    return moves[indexOfMove];

        //}
        //int maxDepth = 4;
        //int[] checkAmount = new int[] { 999, 999, 20, 30, 6, 50, 3 };
        //int maxCountDepthInc = 12;
        //int pieceSum;
        //float pawnSum;
        //int TakeWeightCycle(Board board, Move[] moves, int currentDepth, bool isAITurn, ref int moveIndex, int checkTopAmount = 999, int alpha = -99999, int beta = 99999, int currentScore = 0)
        //{
        //    //if (cachedStates.Count > 1000000) cachedStates.Clear();
        //    int depthInc = 0;
        //    if (currentDepth == 0)
        //    {
        //        pieceSum = board.GetAllPieceLists().Sum(x => x.Count);
        //        pawnSum = board.GetPieceList(PieceType.Pawn, true).Count + board.GetPieceList(PieceType.Pawn, false).Count;
        //        pawnSum /= 4;
        //        if (pieceSum < 5) depthInc = 4;
        //        else if (pieceSum < 7) depthInc = 3;
        //        else if (pieceSum <= 9) depthInc = 2;
        //        else if (pieceSum <= maxCountDepthInc) depthInc = 1;
        //    }
        //    maxDepth += depthInc;
        //    List<(int weight, int index)> decreases = new();
        //    int ind = 0;
        //    foreach (Move move in moves)
        //    {
        //        if (move.IsPromotion && move.PromotionPieceType != PieceType.Queen) { ind++; continue; }
        //        int totalScore = 0;
        //        board.MakeMove(move);

        //        if (board.IsInCheckmate())
        //        {
        //            board.UndoMove(move);
        //            if (currentDepth == 0) moveIndex = ind;
        //            return isAITurn ? 15000 : -15000;
        //        }

        //        if (pieceSum - pawnSum < maxCountDepthInc && move.MovePieceType == PieceType.Pawn) totalScore += 35;

        //        if (board.GameRepetitionHistory.Length >= 4)
        //        {
        //            int repeatSub = 0;
        //            int repeatCount = 0;
        //            for (int i = 0; i < board.GameRepetitionHistory.Length; i++)
        //            {
        //                if (board.ZobristKey == board.GameRepetitionHistory[i]) repeatCount++;
        //            }
        //            if (repeatCount > 3) repeatSub = 60;
        //            else if (repeatCount > 1) repeatSub = 40;
        //            if (repeatCount == 1) repeatSub = 15;

        //            if (isAITurn) repeatSub = -repeatSub;
        //            totalScore += repeatSub;
        //        }
        //        if (board.IsInCheck()) totalScore += 20;
        //        else if (move.MovePieceType == PieceType.King && isAITurn) totalScore -= 15;
        //        if (move.IsCastles) totalScore += 150;
        //        if (move.IsPromotion) totalScore += 210;
        //        if (move.IsCapture) totalScore += pieceValues[(int)move.CapturePieceType];

        //        if (centralSquares.Contains(move.TargetSquare) && pieceSum > maxCountDepthInc) totalScore += 5;

        //        decreases.Add((totalScore, ind));
        //        board.UndoMove(move);
        //        ind++;
        //    }
        //    if (decreases.Count == 0) return 0;
        //    decreases = decreases.OrderByDescending(x => x.weight).ToList();
        //    if (!isAITurn) decreases = decreases.Select(x => x = (-x.weight, x.index)).ToList();
        //    if (checkTopAmount < decreases.Count && decreases[checkTopAmount] == decreases[0]) checkTopAmount += 5;
        //    int index = 0;
        //    if (currentDepth < maxDepth)
        //    {
        //        int weight = isAITurn ? -99999 : 99999;
        //        int bestIndex = 0;
        //        List<int> tiedBestEntires = new();
        //        for (int i = 0; i < checkTopAmount; i++)
        //        {
        //            if (decreases.Count <= i) break;
        //            if (decreases[i].weight < -7000 && isAITurn) continue;
        //            board.MakeMove(moves[decreases[i].index]);
        //            Move[] nextMoves = board.GetLegalMoves();
        //            int nextSearchAmount = checkAmount[Math.Clamp(currentDepth + 1, 0, checkAmount.Length - 1)];
        //            if (checkTopAmount == 1) nextSearchAmount = 1;
        //            int weight2;
        //            //if (currentDepth > 2 && cachedStates.ContainsKey(board.ZobristKey) && cachedStates[board.ZobristKey].Item2 == currentDepth && cachedStates[board.ZobristKey].Item3 == board.AllPiecesBitboard)
        //            //    weight2 = cachedStates[board.ZobristKey].Item1;
        //            //else
        //            //{
        //            weight2 = TakeWeightCycle(board, nextMoves, currentDepth + 1, !isAITurn, ref moveIndex, nextSearchAmount, alpha, beta, currentScore + decreases[i].weight);
        //            //if (!cachedStates.ContainsKey(board.ZobristKey) && currentDepth > 2)
        //            //{
        //            //    cachedStates.Add(board.ZobristKey, (weight2, currentDepth, board.AllPiecesBitboard));
        //            //}
        //            //}
        //            if (isAITurn) weight = Math.Max(weight2, weight);
        //            else weight = Math.Min(weight2, weight);

        //            if (isAITurn && weight > alpha)
        //            {
        //                bestIndex = decreases[i].index;
        //                tiedBestEntires.Clear();
        //                tiedBestEntires.Add(bestIndex);
        //                alpha = weight;
        //            }
        //            else if (!isAITurn && weight < beta)
        //            {
        //                bestIndex = decreases[i].index;
        //                tiedBestEntires.Clear();
        //                tiedBestEntires.Add(bestIndex);
        //                beta = weight;
        //            }
        //            board.UndoMove(moves[decreases[i].index]);
        //            if (isAITurn)
        //            {
        //                if (weight >= beta && currentDepth != 0) { return weight; }
        //                else if (weight >= beta) break;
        //            }
        //            else
        //            {
        //                if (weight <= alpha && currentDepth != 0) { return weight; }
        //                else if (weight <= alpha) break;
        //            }
        //            index++;


        //        }
        //        if (currentDepth == 0)
        //        {
        //            moveIndex = tiedBestEntires.Count > 0 ? tiedBestEntires[rnd.Next(tiedBestEntires.Count)] : bestIndex;
        //            if (pieceSum <= maxCountDepthInc) maxDepth -= depthInc;
        //        }
        //        return weight;
        //    }
        //    else
        //    {
        //        return currentScore + decreases[0].weight;
        //    }

        //}

        //int[] pieceValues = { 0, 100, 300, 300, 500, 900, 10000 };
        //Square[] centralSquares = new Square[] { new("d4"), new("d5"), new("e4"), new("e5") };
        ////Dictionary<ulong, (int, int, ulong)> cachedStates = new();
        //Random rnd = new();
        //bool isBlack;
        //bool firstRound = true;
        //int ourSidePieceCount;
        //public Move Think(Board board, Timer timer)
        //{
        //    Move[] moves = board.GetLegalMoves();
        //    if (firstRound)
        //    {
        //        isBlack = board.GetPiece(moves[0].StartSquare).IsWhite ? false : true;
        //        ourSidePieceCount = 16;
        //        firstRound = false;
        //    }
        //    else
        //    {
        //        if (board.GameMoveHistory[board.GameMoveHistory.Length - 1].IsCapture) ourSidePieceCount--;
        //    }
        //    int indexOfMove = 0;
        //    TakeWeightCycle(board, moves, 0, true, ref indexOfMove);
        //    //cachedStates.Clear();
        //    return moves[indexOfMove];

        //}
        //int originalMaxDepth;
        //int maxDepth = 2;
        //int depthSub = 0;
        //int[] checkAmount = new int[] { 999, 999, 20, 30, 6, 15, 3 };
        //int maxCountDepthInc = 12;
        //int pieceSum;
        //float pawnSum;
        //bool checkingMore;
        //int checkingMoreDepthStart;
        //int bestScore;
        //bool searchedFurther;
        //int prevBestScore;
        //int TakeWeightCycle(Board board, Move[] moves, int currentDepth, bool isAITurn, ref int moveIndex, int checkTopAmount = 999, int alpha = -99999, int beta = 99999, int currentScore = 0)
        //{
        //    //if (cachedStates.Count > 1000000) cachedStates.Clear();
        //    int depthInc = 0;
        //    if (currentDepth == 0)
        //    {
        //        bestScore = -99999;
        //        prevBestScore = -99999;
        //        checkingMoreDepthStart = 0;
        //        searchedFurther = false;
        //        checkingMore = false;
        //        pieceSum = board.GetAllPieceLists().Sum(x => x.Count);
        //        pawnSum = board.GetPieceList(PieceType.Pawn, true).Count + board.GetPieceList(PieceType.Pawn, false).Count;
        //        pawnSum /= 4;
        //        if (pieceSum <= 9) depthInc = 2;
        //        else if (pieceSum <= maxCountDepthInc) depthInc = 1;
        //        originalMaxDepth = maxDepth + depthInc;
        //    }
        //    maxDepth += depthInc;
        //    float percentOfScores = 1 - (0.05f * currentDepth);
        //    List<(int weight, int index)> decreases = new();
        //    int ind = 0;
        //    foreach (Move move in moves)
        //    {
        //        if (move.IsPromotion && move.PromotionPieceType != PieceType.Queen) { ind++; continue; }
        //        int totalScore = 0;
        //        board.MakeMove(move);

        //        if (board.IsInCheckmate())
        //        {
        //            board.UndoMove(move);
        //            if (currentDepth == 0) moveIndex = ind;
        //            return (int)(isAITurn ? 15000 * percentOfScores : -15000 * percentOfScores);
        //        }
        //        board.ForceSkipTurn();
        //        totalScore += (isAITurn ? 1 : 1) * board.GetLegalMoves().Length;
        //        board.UndoSkipTurn();
        //        if (pieceSum - pawnSum < maxCountDepthInc && move.MovePieceType == PieceType.Pawn) totalScore += 35;

        //        if (board.GameRepetitionHistory.Length >= 4)
        //        {
        //            int repeatSub = 0;
        //            int repeatCount = 0;
        //            for (int i = 0; i < board.GameRepetitionHistory.Length; i++)
        //            {
        //                if (board.ZobristKey == board.GameRepetitionHistory[i]) repeatCount++;
        //            }
        //            if (repeatCount > 3) repeatSub = 60;
        //            else if (repeatCount > 1) repeatSub = 40;
        //            if (repeatCount == 1) repeatSub = 15;

        //            if (isAITurn) repeatSub = -repeatSub;
        //            totalScore += repeatSub;
        //        }
        //        if (board.IsInCheck()) totalScore += 20;
        //        else if (move.MovePieceType == PieceType.King && isAITurn) totalScore -= 15;
        //        if (move.IsCastles) totalScore += 150;
        //        if (move.IsPromotion) totalScore += 210;
        //        if (move.IsCapture) totalScore += pieceValues[(int)move.CapturePieceType];

        //        if (centralSquares.Contains(move.TargetSquare) && pieceSum > maxCountDepthInc) totalScore += 5;

        //        totalScore = (int)(totalScore * percentOfScores);
        //        decreases.Add((totalScore, ind));
        //        board.UndoMove(move);
        //        ind++;
        //    }
        //    if (decreases.Count == 0) return 0;
        //    decreases = decreases.OrderByDescending(x => x.weight).ToList();
        //    if (!isAITurn) decreases = decreases.Select(x => x = (-x.weight, x.index)).ToList();
        //    int index = 0;
        //    if (currentDepth == maxDepth && depthSub == 0 && currentScore + decreases[0].weight > bestScore)
        //    {
        //        //prevBestScore = bestScore;
        //        //depthSub = 4;
        //        //maxDepth += depthSub;
        //        //searchedFurther = true;
        //        //bestScore = currentScore + decreases[0].weight;
        //    }
        //    if (currentDepth < maxDepth)
        //    {
        //        int weight = isAITurn ? -99999 : 99999;
        //        int bestIndex = 0;
        //        List<int> tiedBestEntires = new();
        //        for (int i = 0; i < checkTopAmount; i++)
        //        {
        //            if (decreases.Count <= i) break;
        //            if (decreases[i].weight < -7000 && isAITurn) continue;
        //            board.MakeMove(moves[decreases[i].index]);
        //            Move[] nextMoves = board.GetLegalMoves();
        //            int nextSearchAmount = checkAmount[Math.Clamp(currentDepth + 1, 0, checkAmount.Length - 1)];
        //            if (depthSub != 0)
        //            {
        //                nextSearchAmount = isAITurn ? 1 : 1;
        //                if (checkingMore) nextSearchAmount = isAITurn ? 2 : 2;
        //            }
        //            int weight2;
        //            //if (currentDepth > 2 && cachedStates.ContainsKey(board.ZobristKey) && cachedStates[board.ZobristKey].Item2 == currentDepth && cachedStates[board.ZobristKey].Item3 == board.AllPiecesBitboard)
        //            //    weight2 = cachedStates[board.ZobristKey].Item1;
        //            //else
        //            //{
        //            weight2 = TakeWeightCycle(board, nextMoves, currentDepth + 1, !isAITurn, ref moveIndex, nextSearchAmount, alpha, beta, currentScore + decreases[i].weight);
        //            //if (!cachedStates.ContainsKey(board.ZobristKey) && currentDepth > 2)
        //            //{
        //            //    cachedStates.Add(board.ZobristKey, (weight2, currentDepth, board.AllPiecesBitboard));
        //            //}
        //            //}
        //            board.UndoMove(moves[decreases[i].index]);

        //            if (checkingMoreDepthStart < currentDepth && checkingMore) checkingMore = false;

        //            if (depthSub != 0 && weight2 < -1000 && currentDepth >= originalMaxDepth)
        //            {
        //                if (isAITurn)
        //                {
        //                    if (weight <= weight2 && checkTopAmount > 1 || currentDepth < maxDepth - 4)
        //                    {
        //                        if (i == checkTopAmount - 1 || i == decreases.Count - 1)
        //                        {
        //                            return weight2;
        //                        }
        //                    }
        //                    if (checkingMore) continue;
        //                    checkTopAmount = 2;
        //                    if (currentDepth == originalMaxDepth) checkTopAmount = 10;
        //                    checkingMore = true;
        //                    checkingMoreDepthStart = currentDepth;
        //                    continue;
        //                }
        //                return weight2;
        //            }
        //            if (currentDepth < originalMaxDepth && depthSub != 0)
        //            {
        //                bestScore = prevBestScore;
        //                maxDepth -= depthSub;
        //                searchedFurther = false;
        //                depthSub = 0;
        //            }
        //            //if(weight2 < -5000) return weight2;

        //            if (isAITurn) weight = Math.Max(weight2, weight);
        //            else weight = Math.Min(weight2, weight);

        //            if (depthSub != 0 && currentDepth == originalMaxDepth) continue;

        //            if (isAITurn && weight > alpha)
        //            {
        //                bestIndex = decreases[i].index;
        //                tiedBestEntires.Clear();
        //                tiedBestEntires.Add(bestIndex);
        //                alpha = weight;
        //            }
        //            else if (!isAITurn && weight < beta)
        //            {
        //                bestIndex = decreases[i].index;
        //                tiedBestEntires.Clear();
        //                tiedBestEntires.Add(bestIndex);
        //                beta = weight;
        //            }
        //            if (isAITurn)
        //            {
        //                if (weight >= beta && currentDepth != 0 && currentDepth < originalMaxDepth) { return beta; }
        //                else if (weight >= beta) break;
        //            }
        //            else
        //            {
        //                if (weight <= alpha && currentDepth != 0 && currentDepth < originalMaxDepth) { return alpha; }
        //                else if (weight <= alpha) break;
        //            }
        //            index++;


        //        }
        //        if (searchedFurther && currentDepth == originalMaxDepth)
        //        {
        //            maxDepth -= depthSub;
        //            searchedFurther = false;
        //            depthSub = 0;
        //        }
        //        if (currentDepth == 0)
        //        {
        //            moveIndex = tiedBestEntires.Count > 0 ? tiedBestEntires[rnd.Next(tiedBestEntires.Count)] : bestIndex;
        //            if (pieceSum <= maxCountDepthInc) maxDepth -= depthInc;

        //        }
        //        if (depthSub != 0 && currentDepth != originalMaxDepth) return currentScore + decreases.Find(x => x.index == bestIndex).weight + weight;
        //        else if (currentDepth == originalMaxDepth) return currentScore + decreases.Find(x => x.index == bestIndex).weight + (int)Math.Pow(weight, 0.3f);
        //        return isAITurn ? alpha : beta;
        //    }
        //    else
        //    {
        //        //if (depthSub == 0 && currentScore + decreases[0].weight > bestScore - 20)
        //        //{
        //        //    bestScore = currentScore + decreases[0].weight;
        //        //    int i = 0;
        //        //    int limit = 5 < decreases.Count ? 5 : decreases.Count - 1;
        //        //    int res;
        //        //    while (i < limit)
        //        //    {
        //        //        depthSub = 6;
        //        //        maxDepth += depthSub;
        //        //        board.MakeMove(moves[decreases[i].index]);
        //        //        res = TakeWeightCycle(board, board.GetLegalMoves(), currentDepth + 1, !isAITurn, ref moveIndex, 10, alpha, beta, currentScore + decreases[i].weight);
        //        //        board.UndoMove(moves[decreases[i].index]);
        //        //        maxDepth -= depthSub;
        //        //        depthSub = 0;
        //        //        int finalRes = (res - currentScore + decreases[i].weight) / 4 + (currentScore + decreases[i].weight);
        //        //        bestFinalDepthWeight = Math.Max(bestFinalDepthWeight, res);
        //        //        alpha = Math.Max(alpha, bestFinalDepthWeight);
        //        //        if (bestFinalDepthWeight >= beta) { searchedFurther = true; return beta; }
        //        //        i++;
        //        //    }
        //        //    searchedFurther = true;
        //        //    return alpha;
        //        //}
        //        return currentScore + decreases[0].weight;
        //    }

        //}


        int[] pieceValues = { 0, 100, 300, 300, 500, 900, 10000 };
        Square[] centralSquares = new Square[] { new("d4"), new("d5"), new("e4"), new("e5") };
        //Dictionary<ulong, (int, int, ulong)> cachedStates = new();
        Random rnd = new();
        bool isBlack;
        bool firstRound = true;
        int ourSidePieceCount;
        int otherSidePieceCount;
        public Move Think(Board board, Timer timer)
        {
            Move[] moves = board.GetLegalMoves();
            if (firstRound)
            {
                isBlack = board.GetPiece(moves[0].StartSquare).IsWhite ? false : true;
                ourSidePieceCount = 16;
                otherSidePieceCount = 16;
                firstRound = false;
            }
            else
            {
                if (board.GameMoveHistory[board.GameMoveHistory.Length - 1].IsCapture) ourSidePieceCount--;
            }
            int indexOfMove = 0;
            TakeWeightCycle(board, moves, 0, true, ref indexOfMove);
            if (moves[indexOfMove].IsCapture) otherSidePieceCount--;
            //cachedStates.Clear();
            return moves[indexOfMove];

        }
        int originalMaxDepth;
        int maxDepth = 3;
        int depthSub = 0;
        int[] checkAmount = new int[] { 999, 999, 20, 30, 6, 15, 3 };
        int maxCountDepthInc = 12;
        int pieceSum;
        float pawnSum;
        //bool checkingMore;
        //int checkingMoreDepthStart;
        //int bestScore;
        //bool searchedFurther;
        //int prevBestScore;
        //int highestScore;
        int TakeWeightCycle(Board board, Move[] moves, int currentDepth, bool isAITurn, ref int moveIndex, int checkTopAmount = 999, int alpha = -99999, int beta = 99999, int currentScore = 0)
        {
            //if (cachedStates.Count > 1000000) cachedStates.Clear();
            int depthInc = 0;
            if (currentDepth == 0)
            {
                //bestScore = -99999;
                //prevBestScore = -99999;
                //checkingMoreDepthStart = 0;
                //searchedFurther = false;
                //checkingMore = false;
                //highestScore = -99999;
                pieceSum = board.GetAllPieceLists().Sum(x => x.Count);
                pawnSum = board.GetPieceList(PieceType.Pawn, true).Count + board.GetPieceList(PieceType.Pawn, false).Count;
                pawnSum /= 4;
                if (pieceSum <= 9) depthInc = 2;
                else if (pieceSum <= maxCountDepthInc) depthInc = 1;
                originalMaxDepth = maxDepth + depthInc;
            }
            maxDepth += depthInc;
            float percentOfScores = 1 - (0.02f * currentDepth);
            List<(int weight, int index)> decreases = new();
            int ind = 0;
            foreach (Move move in moves)
            {
                if (move.IsPromotion && move.PromotionPieceType != PieceType.Queen) { ind++; continue; }
                int totalScore = 0;
                board.MakeMove(move);

                if (board.IsInCheckmate())
                {
                    board.UndoMove(move);
                    if (currentDepth == 0) moveIndex = ind;
                    return (int)(isAITurn ? 15000 * percentOfScores : -15000 * percentOfScores);
                }
                board.ForceSkipTurn();
                totalScore += (isAITurn ? 1 : 1) * board.GetLegalMoves().Length;
                board.UndoSkipTurn();
                if (pieceSum - pawnSum < maxCountDepthInc && move.MovePieceType == PieceType.Pawn) totalScore += 35;

                if (board.GameRepetitionHistory.Length >= 4)
                {
                    int repeatSub = 0;
                    int repeatCount = 0;
                    for (int i = 0; i < board.GameRepetitionHistory.Length; i++)
                    {
                        if (board.ZobristKey == board.GameRepetitionHistory[i]) repeatCount++;
                    }
                    if (repeatCount > 3) repeatSub = 60;
                    else if (repeatCount > 1) repeatSub = 40;
                    if (repeatCount == 1) repeatSub = 15;

                    if (isAITurn) repeatSub = -repeatSub;
                    totalScore += repeatSub;
                }
                if (board.IsInCheck()) totalScore += 20;
                else if (move.MovePieceType == PieceType.King && isAITurn) totalScore -= 15;
                if (move.IsCastles) totalScore += 150;
                if (move.IsPromotion) totalScore += 140;
                if (move.IsPromotion && pieceSum < 10) totalScore += 610;
                else if (move.IsPromotion && pieceSum < 14) totalScore += 310;
                if (move.IsCapture)
                {
                    totalScore += pieceValues[(int)move.CapturePieceType];
                }
                if (move.IsCapture) totalScore += Math.Clamp((otherSidePieceCount - ourSidePieceCount) * 20, 0, 99999);

                if (centralSquares.Contains(move.TargetSquare) && pieceSum > maxCountDepthInc) totalScore += 5;

                totalScore = (int)(totalScore * percentOfScores);
                decreases.Add((totalScore, ind));
                board.UndoMove(move);
                ind++;
            }
            if (decreases.Count == 0) return 0;
            decreases = decreases.OrderByDescending(x => x.weight).ToList();
            if (!isAITurn) decreases = decreases.Select(x => x = (-x.weight, x.index)).ToList();
            int index = 0;
            //if (currentDepth == maxDepth && depthSub == 0 && currentScore + decreases[0].weight > bestScore)
            //{
            //    //prevBestScore = bestScore;
            //    //depthSub = 4;
            //    //maxDepth += depthSub;
            //    //searchedFurther = true;
            //    //bestScore = currentScore + decreases[0].weight;
            //}
            if (currentDepth < maxDepth)
            {
                int weight = isAITurn ? -99999 : 99999;
                int bestIndex = 0;
                List<int> tiedBestEntires = new();
                for (int i = 0; i < checkTopAmount; i++)
                {
                    if (decreases.Count <= i) break;
                    board.MakeMove(moves[decreases[i].index]);
                    Move[] nextMoves = board.GetLegalMoves();
                    int nextSearchAmount = checkAmount[Math.Clamp(currentDepth + 1, 0, checkAmount.Length - 1)];
                    //if (depthSub != 0)
                    //{
                    //    nextSearchAmount = isAITurn ? 1 : 1;
                    //    if (checkingMore) nextSearchAmount = isAITurn ? 2 : 2;
                    //}
                    int weight2;
                    //if (currentDepth > 2 && cachedStates.ContainsKey(board.ZobristKey) && cachedStates[board.ZobristKey].Item2 == currentDepth && cachedStates[board.ZobristKey].Item3 == board.AllPiecesBitboard)
                    //    weight2 = cachedStates[board.ZobristKey].Item1;
                    //else
                    //{
                    weight2 = TakeWeightCycle(board, nextMoves, currentDepth + 1, !isAITurn, ref moveIndex, nextSearchAmount, alpha, beta, currentScore + decreases[i].weight);
                    //if (!cachedStates.ContainsKey(board.ZobristKey) && currentDepth > 2)
                    //{
                    //    cachedStates.Add(board.ZobristKey, (weight2, currentDepth, board.AllPiecesBitboard));
                    //}
                    //}
                    board.UndoMove(moves[decreases[i].index]);

                    //if(!isAITurn) 

                    //if (checkingMoreDepthStart < currentDepth && checkingMore) checkingMore = false;

                    //if (depthSub != 0 && weight2 < -1000 && currentDepth >= originalMaxDepth)
                    //{
                    //    if (isAITurn)
                    //    {
                    //        if (weight <= weight2 && checkTopAmount > 1 || currentDepth < maxDepth - 4)
                    //        {
                    //            if (i == checkTopAmount - 1 || i == decreases.Count - 1)
                    //            {
                    //                return weight2;
                    //            }
                    //        }
                    //        if (checkingMore) continue;
                    //        checkTopAmount = 2;
                    //        if (currentDepth == originalMaxDepth) checkTopAmount = 10;
                    //        checkingMore = true;
                    //        checkingMoreDepthStart = currentDepth;
                    //        continue;
                    //    }
                    //    return weight2;
                    //}
                    //if (currentDepth < originalMaxDepth && depthSub != 0)
                    //{
                    //    bestScore = prevBestScore;
                    //    maxDepth -= depthSub;
                    //    searchedFurther = false;
                    //    depthSub = 0;
                    //}
                    //if(weight2 < -5000) return weight2;

                    if (isAITurn) weight = Math.Max(weight2, weight);
                    else weight = Math.Min(weight2, weight);

                    //if (depthSub != 0 && currentDepth == originalMaxDepth) continue;

                    if (isAITurn && weight > alpha)
                    {
                        bestIndex = decreases[i].index;
                        tiedBestEntires.Clear();
                        tiedBestEntires.Add(bestIndex);
                        alpha = weight;
                    }
                    else if (!isAITurn && weight < beta)
                    {
                        bestIndex = decreases[i].index;
                        tiedBestEntires.Clear();
                        tiedBestEntires.Add(bestIndex);
                        beta = weight;
                    }
                    if (isAITurn)
                    {
                        if (weight >= beta && currentDepth != 0 && currentDepth < originalMaxDepth) { return beta; }
                        else if (weight >= beta) break;
                    }
                    else
                    {
                        if (weight <= alpha && currentDepth != 0 && currentDepth < originalMaxDepth) { return alpha; }
                        else if (weight <= alpha) break;
                    }
                    index++;


                }
                //if (searchedFurther && currentDepth == originalMaxDepth)
                //{
                //    maxDepth -= depthSub;
                //    searchedFurther = false;
                //    depthSub = 0;
                //}
                if (currentDepth == 0)
                {
                    moveIndex = tiedBestEntires.Count > 0 ? tiedBestEntires[rnd.Next(tiedBestEntires.Count)] : bestIndex;
                    if (pieceSum <= maxCountDepthInc) maxDepth -= depthInc;

                }
                //if (depthSub != 0 && currentDepth != originalMaxDepth) return currentScore + decreases.Find(x => x.index == bestIndex).weight + weight;
                //else if (currentDepth == originalMaxDepth) return currentScore + decreases.Find(x => x.index == bestIndex).weight + (int)Math.Pow(weight, 0.3f);
                return isAITurn ? alpha : beta;
            }
            else
            {
                //if (depthSub == 0 && currentScore + decreases[0].weight > bestScore - 20)
                //{
                //    bestScore = currentScore + decreases[0].weight;
                //    int i = 0;
                //    int limit = 5 < decreases.Count ? 5 : decreases.Count - 1;
                //    int res;
                //    while (i < limit)
                //    {
                //        depthSub = 6;
                //        maxDepth += depthSub;
                //        board.MakeMove(moves[decreases[i].index]);
                //        res = TakeWeightCycle(board, board.GetLegalMoves(), currentDepth + 1, !isAITurn, ref moveIndex, 10, alpha, beta, currentScore + decreases[i].weight);
                //        board.UndoMove(moves[decreases[i].index]);
                //        maxDepth -= depthSub;
                //        depthSub = 0;
                //        int finalRes = (res - currentScore + decreases[i].weight) / 4 + (currentScore + decreases[i].weight);
                //        bestFinalDepthWeight = Math.Max(bestFinalDepthWeight, res);
                //        alpha = Math.Max(alpha, bestFinalDepthWeight);
                //        if (bestFinalDepthWeight >= beta) { searchedFurther = true; return beta; }
                //        i++;
                //    }
                //    searchedFurther = true;
                //    return alpha;
                //}
                //if(currentScore + decreases[0].weight > highestScore) highestScore = currentScore + decreases[0].weight;
                return currentScore + decreases[0].weight;
            }

        }





    }
}