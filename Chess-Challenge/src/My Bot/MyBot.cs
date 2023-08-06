using ChessChallenge.API;
using System.Collections.Generic;
using System;
using System.Linq;
public class MyBot : IChessBot
{
    int[] pieceValues = { 0, 100, 300, 300, 500, 900, 10000 };
    Square[] centralSquares = new Square[] { new("d4"), new("d5"), new("e4"), new("e5") };
    Dictionary<ulong, (int, int, ulong)> cachedStates = new();
    Random rnd = new();
    public Move Think(Board board, Timer timer)
    {
        Move[] moves = board.GetLegalMoves();

        int indexOfMove = 0;
        TakeWeightCycle(board, moves, 0, true, ref indexOfMove);
        cachedStates.Clear();
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
        return moves[indexOfMove];

    }
    int maxDepth = 6;
    int[] checkAmount = new int[] { 999, 999, 15, 30, 6, 12, 3 };
    int maxCountDepthInc = 12;
    int pieceSum = 0;
    float pawnSum = 0;
    int TakeWeightCycle(Board board, Move[] moves, int currentDepth, bool isAITurn, ref int moveIndex, int checkTopAmount = 999, int alpha = -99999, int beta = 99999, int currentScore = 0)
    {
        if (cachedStates.Count > 1000000) cachedStates.Clear();
        int depthInc = 0;
        if (currentDepth == 0)
        {
            pieceSum = board.GetAllPieceLists().Sum(x => x.Count);
            pawnSum = board.GetPieceList(PieceType.Pawn, true).Count + board.GetPieceList(PieceType.Pawn, false).Count;
            pawnSum /= 4;
            if (pieceSum < 7) depthInc = 3;
            else if (pieceSum <= 9) depthInc = 2;
            else if (pieceSum <= maxCountDepthInc) depthInc = 1;
        }
        maxDepth += depthInc;
        List<(int weight, int index)> decreases = new();
        int ind = 0;
        foreach (Move move in moves)
        {
            if (move.IsPromotion && move.PromotionPieceType != PieceType.Queen) { ind++; continue; }
            int totalScore = 0;
            board.MakeMove(move);

            if (board.IsInCheckmate())
            {
                if (currentDepth == 0 && board.GetAllPieceLists().Sum(x => x.Count) <= maxCountDepthInc) maxDepth -= depthInc;
                board.UndoMove(move);
                if (currentDepth == 0) moveIndex = ind;
                return isAITurn ? 10000 : -10000;
            }

            if (pieceSum - pawnSum < maxCountDepthInc && move.MovePieceType == PieceType.Pawn) totalScore += isAITurn ? 35 : -35;

            if (board.GameRepetitionHistory.Length >= 4)
            {
                int repeatCount = 0;
                for (int i = 0; i < board.GameRepetitionHistory.Length; i++)
                {
                    if (board.ZobristKey == board.GameRepetitionHistory[i]) repeatCount++;
                }
                if (repeatCount > 3) totalScore -= 60;
                else if (repeatCount > 1) totalScore -= 40;
                else if (repeatCount == 1) totalScore -= 15;
            }
            if (board.IsInCheck())
            {
                totalScore += isAITurn ? 20 : -20;
            }
            else if (move.MovePieceType == PieceType.King && isAITurn) totalScore -= 15;
            if (move.IsCastles)
            {
                totalScore += isAITurn ? 150 : -150;
            }
            if (move.IsPromotion)
            {
                totalScore += isAITurn ? 210 : -210;
            }
            if (move.IsCapture)
            {
                int val = pieceValues[(int)move.CapturePieceType];
                totalScore += isAITurn ? val : -val;
            }

            if (centralSquares.Contains(move.TargetSquare) && pieceSum > maxCountDepthInc) totalScore += isAITurn ? 5 : -5;

            decreases.Add((totalScore, ind));
            board.UndoMove(move);
            ind++;
        }
        if (decreases.Count == 0) return 0;
        decreases = isAITurn ? decreases.OrderByDescending(x => x.weight).ToList() : decreases.OrderBy(x => x.weight).ToList();
        if (checkTopAmount < decreases.Count && decreases[checkTopAmount] == decreases[0]) checkTopAmount += 5;
        int index = 0;
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
                int weight2;
                if (currentDepth > 2 && cachedStates.ContainsKey(board.ZobristKey) && cachedStates[board.ZobristKey].Item2 == currentDepth && cachedStates[board.ZobristKey].Item3 == board.AllPiecesBitboard)
                    weight2 = cachedStates[board.ZobristKey].Item1;
                else
                {
                    weight2 = TakeWeightCycle(board, nextMoves, currentDepth + 1, !isAITurn, ref moveIndex, nextSearchAmount, alpha, beta, currentScore + decreases[i].weight);
                    if (!cachedStates.ContainsKey(board.ZobristKey) && currentDepth > 2)
                    {
                        cachedStates.Add(board.ZobristKey, (weight2, currentDepth, board.AllPiecesBitboard));
                    }
                }
                if (isAITurn) weight = Math.Max(weight2, weight);
                else weight = Math.Min(weight2, weight);

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
                board.UndoMove(moves[decreases[i].index]);
                if (isAITurn)
                {
                    if (weight >= beta && currentDepth != 0) { return weight; }
                    else if (weight >= beta) break;
                }
                else
                {
                    if (weight <= alpha && currentDepth != 0) { return weight; }
                    else if (weight <= alpha) break;
                }
                index++;


            }
            if (currentDepth == 0)
            {
                moveIndex = tiedBestEntires.Count > 0 ? tiedBestEntires[rnd.Next(tiedBestEntires.Count)] : bestIndex;
                if (board.GetAllPieceLists().Sum(x => x.Count) <= maxCountDepthInc) maxDepth -= depthInc;
            }
            return weight;
        }
        else
        {
            return currentScore + decreases[0].weight;
        }

    }
}