using ChessChallenge.API;
using System.Collections.Generic;
using System;
using System.Linq;

public class MyBot : IChessBot
{
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

            int weight = TakeWeightCycle(board, board.GetLegalMoves(), 0, false);
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
    int maxDepth = 4;
    int[] checkAmount = new int[] { 30, 12, 5, 3};
    int TakeWeightCycle(Board board, Move[] moves, int currentDepth, bool isAITurn, int checkTopAmount = 30)
    {
        List<(int weight, int index)> decreases = new();
        int ind = 0;
        foreach (Move move in moves)
        {
            if (isAITurn && move.IsCapture)
            {
                decreases.Add((pieceValues[(int)move.CapturePieceType], ind));
                continue;
            }
            else if (move.IsCapture)
            {
                decreases.Add((-pieceValues[(int)move.CapturePieceType], ind));
            }
            else
            {
                decreases.Add((0, ind));
            }
            ind++;
        }
        decreases = decreases.OrderByDescending(x => x.weight).ToList();
        if (checkTopAmount < decreases.Count && decreases[checkTopAmount] == decreases[0]) checkTopAmount += 5;
        int index = 0;
        if (currentDepth < maxDepth)
        {
            for (int i = 0; i < checkTopAmount; i++)
            {
                if (decreases.Count <= i) break;
                board.MakeMove(moves[decreases[i].index]);
                Move[] nextMoves = board.GetLegalMoves();
                if (nextMoves.Length > 0)
                {
                     TakeWeightCycle(board, nextMoves, currentDepth + 1, !isAITurn, checkAmount[Math.Clamp(currentDepth + 1, 0, checkAmount.Length - 1)]);

                }
                board.UndoMove(moves[decreases[i].index]);
                index++;


            }
        }
        if (decreases.Count > 0 && isAITurn) return (decreases.Max(x => x.weight));
        else if (decreases.Count > 0) return (decreases.Min(x => x.weight));
        else return 0;
    }
}