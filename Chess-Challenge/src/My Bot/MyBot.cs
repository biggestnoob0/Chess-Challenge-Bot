using ChessChallenge.API;
using System.Collections.Generic;
using System;
using System.Linq;
public class MyBot : IChessBot
{
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
            totalScore += 1 * board.GetLegalMoves().Length;
            board.UndoSkipTurn();
            if (pieceSum - pawnSum < maxCountDepthInc && move.MovePieceType == PieceType.Pawn) totalScore += 35;

            if (board.GameRepetitionHistory.Length >= 2)
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
            if (board.IsInCheck()) totalScore += 110;
            else if (move.MovePieceType == PieceType.King) totalScore -= 15;
            if (move.IsCastles) totalScore += 150;
            if (move.IsPromotion) totalScore += 90;
            if (move.IsPromotion && pieceSum - (pawnSum * 4) < 7) totalScore += pieceSum - (pawnSum * 4) > 4 ? 310 : 610;
            if (move.IsCapture) 
            { 
                totalScore += pieceValues[(int)move.CapturePieceType];
                
            }
            //if (move.IsCapture) totalScore += Math.Clamp((otherSidePieceCount - ourSidePieceCount) * 20, 0, 99999);

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