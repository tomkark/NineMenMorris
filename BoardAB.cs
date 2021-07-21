using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NMM.Const;
using static NMM.Const.location;

namespace NMM
{
    class BoardAB : Board
    {
        public BoardAB()
        {
        }
        public BoardAB(int layer = layers)
        {
            hooks = new IndexHooks(layer);
        }
        // evaluate board. positive score for player 1. negative score for player 2
        // lowest possible score for player 1 and highest possible score player 2
        //1 for maximizer(P2), -1 for minimizer(P1), 0 otherwise
        public double Evaluate1()
        {
            int numOfPieces = CountPieces(2) - CountPieces(1);
            return numOfPieces;
        }
        public double Evaluate2() 
        {
            double score;
            int numOfMorrises,
                numOfBlockedOpponentsPieces,
                numOfPieces,
                numOfTwoPieceConfig,
                numOfThreePieceConfig,
                doubleMorris;
            if(phase == 0)
            {
                numOfMorrises = morrises[1] - morrises[0];
                numOfBlockedOpponentsPieces = numOfBlockedPieces(1) - numOfBlockedPieces(2);//*
                numOfPieces = CountPieces(2) - CountPieces(1);
                numOfTwoPieceConfig = numOf2PieceConfiguration(2) - numOf2PieceConfiguration(1);
                numOfThreePieceConfig = numOf3PieceConfiguration(2) - numOf3PieceConfiguration(1);
                score = 9 * numOfMorrises + 3 * numOfBlockedOpponentsPieces + 8 * numOfPieces + 5 * numOfTwoPieceConfig + 3 * numOfThreePieceConfig;
            }
            else if (playerPieces[player-1] > 3)
            {
                numOfMorrises = morrises[1] - morrises[0];
                numOfBlockedOpponentsPieces = numOfBlockedPieces(1) - numOfBlockedPieces(2);//*
                numOfPieces = CountPieces(2) - CountPieces(1);
                doubleMorris = doubleMorrisCount(2) - doubleMorrisCount(1);
                score = 8 * numOfMorrises + 5 * numOfBlockedOpponentsPieces + 4 * numOfPieces + 4 * doubleMorris;

            }
            else 
            {
                numOfPieces = CountPieces(2) - CountPieces(1);
                numOfTwoPieceConfig = numOf2PieceConfiguration(2) - numOf2PieceConfiguration(1);
                numOfThreePieceConfig = numOf3PieceConfiguration(2) - numOf3PieceConfiguration(1);
                score = 15 * numOfPieces + 4 * numOfTwoPieceConfig + 2 * numOfThreePieceConfig;
            }
            return score;

        }
        public MorrisNode[] CompPlay(int eval1, int eval2) 
        {
            Move best = BestMove(eval1,eval2);
            return new MorrisNode[]{best.src, best.dest,best.ate};
        }
        public int Elapsed(DateTime start) // how much time elapsed on stopper
        {
            DateTime end = DateTime.UtcNow;
            TimeSpan timeDiff = end - start;
            int elapsed = Convert.ToInt32(timeDiff.TotalMilliseconds);
            return elapsed;
        }
        public Move BestMove(int eval1, int eval2) // chose best move using alpha-beta for phase 1/2.
        {
            //MAXIMIZIER - PC
            //MINIMIZER - PLAYER
            double value, bestVal;
            int index, depth;
            int timelimit;
            double alpha = double.MinValue;
            double beta = double.MaxValue;
            List<MoveScore> listScores = new List<MoveScore>();
            List<Move> list = AllPossibleMoves(player);
            DateTime start = DateTime.UtcNow;
            depth = 1;
            timelimit = TIMELIMIT_PLAY;
            do
            {
                listScores.Clear();
                foreach (Move m in list)
                {
                    if (AdvancedPlayHandler(m))
                        listScores.Add(new MoveScore(m, (player == 2 ) ? WinVal : -WinVal));
                    else
                    {
                        value = GAMMA * AlphaBeta(depth - 1, player, alpha, beta, eval1, eval2);
                        MoveBack();
                        listScores.Add(new MoveScore(m, value));
                    }
                }
                depth++;
            } while (Elapsed(start) < timelimit / 8);
            listScores.Sort();
            if (player == 2)
                listScores.Reverse();
            bestVal = listScores[0].score;
            index = 1;
            while (index < listScores.Count && listScores[index].score == bestVal)
                index++;
            int r = Const.rnd.Next(index);
            if (lastRandomized > 7 && phase != 0 && (playerPieces[0] == 3 && playerPieces[1] == 3) && listScores[r].move.ate == null) //If after 10 rounds no has eaten, then randomize between the best 3 *scores*
            {
                index = 1;
                lastRandomized = 0;
                int count = 0;
                double tempVal = bestVal;
                while (index < listScores.Count-1 && count < 3)
                {
                    if (listScores[index + 1].score != tempVal)
                    {
                        tempVal = listScores[index + 1].score;
                        count++;
                    }
                    index++;
                }
                r = Const.rnd.Next(index);
            }
            return listScores[r].move;
        }
        public double AlphaBeta(int depth, int playerAB, double alpha, double beta, int eval1, int eval2) //Alphabeta
        {
            double value, bestVal;
            List<Move> list = AllPossibleMoves(playerAB);
            if (depth == 0)
            {
                if(playerAB == 1)
                {
                    if(eval1 == 1)
                        return Evaluate1();
                    return Evaluate2();
                }
                if (eval2 == 1)
                    return Evaluate1();
                return Evaluate2();
            }
            if (playerAB == 2) // maximizer player #2
            {
                bestVal = double.MinValue;
                foreach (Move m in list)
                {
                    if (AdvancedPlayHandler(m))
                        return WinVal;
                    value = GAMMA * AlphaBeta(depth - 1, 3 - playerAB, alpha, beta, eval1, eval2);
                    MoveBack();
                    bestVal = Math.Max(bestVal, value);
                    alpha = Math.Max(alpha, bestVal);
                    if (beta <= alpha)
                        break;
                }
                return bestVal;
            }
            else // minimizer player #1
            {
                bestVal = double.MaxValue;
                foreach (Move m in list)
                {
                    if (AdvancedPlayHandler(m))
                        return -WinVal;
                    value = GAMMA * AlphaBeta(depth - 1, 3 - playerAB, alpha, beta, eval1, eval2);
                    MoveBack();
                    bestVal = Math.Min(bestVal, value);
                    beta = Math.Min(beta, bestVal);
                    if (beta <= alpha)
                        break;
                }
                return bestVal;
            }

        }
        bool AdvancedPlayHandler(Move m) // True if WinVal should be returned in AB.
        {
            if (phase != 0)
            {
                if (m.ate != null && playerPieces[3 - player - 1] - 1 <= 2)
                    return true;
                if (ChoosePiece(m.src))
                    if (PlacePiece(m.src, m.dest) == 1)
                            RemovePiece(m.ate);
            }
            else
            {
                if (PlacePiece(null, m.dest) == 1) {
                        RemovePiece(m.ate);
                }
            }
            return false;
        }
        public int numOfBlockedPieces(int p) //Counts the amount of blocked pieces for player p
        {
            int blocked = 0;
            bool first = true;
            MorrisNode helper = hooks.hooks;
            while (helper.outer != null || first)
            {
                if (!first)
                {
                    helper = helper.outer;
                }
                for (int i = 1; i < 9; i++)
                {
                    if (helper.occupied == p)
                    {
                        if (PossibleMoves(helper).Count == 0)
                        {
                            blocked++;
                        }
                    }
                    helper = helper.near1;
                }
                first = false;
            }
            return blocked;
        }
        public int numOf2PieceConfiguration(int p)// Counts the number of 2 piece configurations for player p
        {
            int config = 0;
            bool first = true;
            MorrisNode helper = hooks.hooks;
            while (helper.outer != null || first)
            {
                if (!first)
                {
                    helper = helper.outer;
                }
                for (int i = 1; i < 9; i++)
                {
                    if (helper.occupied == p)
                    {
                        config += numOf2ConfigHelp(helper);
                    }
                    helper = helper.near1;
                }
                first = false;
            }
            return config;
        }
        private int numOf2ConfigHelp(MorrisNode node)
        {
            int config = 0;
            if (node.place == TopLeft || node.place == TopRight || node.place == BottomLeft || node.place == BottomRight)
            {
                if ((node.occupied == node.near1.occupied && node.occupied != 0 && node.near1.near1.occupied == 0))
                    config++;
                if ((node.occupied == node.near2.occupied && node.occupied != 0 && node.near2.near2.occupied == 0))
                    config++;
            }
            else if (node.place == MidTop || node.place == MidBottom || node.place == MidRight || node.place == MidLeft)
            {
                if (node.occupied == 0 && node.near1.occupied == node.near2.occupied && node.near1.occupied != 0)
                    config++;
                if (node.HasInner() && node.HasOuter() && (node.occupied == 0 && node.inner.occupied == node.outer.occupied && node.inner.occupied != 0))
                    config++;
                if (node.HasOuter() && node.outer.HasOuter() && (node.occupied == 0 && node.outer.occupied == node.outer.outer.occupied && node.outer.occupied != 0))
                    config++;
                if (node.HasInner() && node.inner.HasInner() && (node.occupied == 0 && node.inner.occupied == node.inner.inner.occupied && node.inner.occupied != 0))
                    config++;
            }
            return config;
        }
        public int numOf3PieceConfiguration(int p)// Counts the number of 3 piece configurations for player p
        {
            int config = 0;
            bool first = true;
            MorrisNode helper = hooks.hooks;
            while (helper.outer != null || first)
            {
                if (!first)
                {
                    helper = helper.outer;
                }
                for (int i = 1; i < 9; i++)
                {
                    if (helper.occupied == p)
                    {
                        config += numOf3ConfigHelp(helper) ? 1 : 0;
                    }
                    helper = helper.near1;
                }
                first = false;
            }
            return config;
        }
        private bool numOf3ConfigHelp(MorrisNode node)
        {
            if (node.place == TopLeft || node.place == TopRight || node.place == BottomLeft || node.place == BottomRight)
            {
                if ((node.occupied == node.near1.occupied && node.occupied == node.near2.occupied && node.near1.near1.occupied == node.near2.near2.occupied && node.near1.near1.occupied == 0))
                    return true;
            }
            else if (node.place == MidTop || node.place == MidBottom || node.place == MidRight || node.place == MidLeft)
            {
                if (node.HasInner() && node.inner.HasInner() && (node.occupied == node.inner.occupied && node.occupied == node.near1.occupied && node.inner.inner.occupied == 0 && node.near2.occupied == 0))
                    return true;
                if (node.HasInner() && node.inner.HasInner() && (node.occupied == node.inner.occupied && node.occupied == node.near2.occupied && node.inner.inner.occupied == 0 && node.near1.occupied == 0))
                    return true;
                if (node.HasOuter() && node.outer.HasOuter() && (node.occupied == node.outer.occupied && node.occupied == node.near1.occupied && node.outer.outer.occupied == 0 && node.near2.occupied == 0))
                    return true;
                if (node.HasOuter() && node.outer.HasOuter() && (node.occupied == node.outer.occupied && node.occupied == node.near2.occupied && node.outer.outer.occupied == 0 && node.near1.occupied == 0))
                    return true;

                if (node.HasInner() && node.HasOuter() && (node.occupied == node.inner.occupied && node.occupied == node.near1.occupied && node.outer.occupied == 0 && node.near2.occupied == 0))
                    return true;
                if (node.HasInner() && node.HasOuter() && (node.occupied == node.inner.occupied && node.occupied == node.near2.occupied && node.outer.occupied == 0 && node.near1.occupied == 0))
                    return true;
                if (node.HasInner() && node.HasOuter() && (node.occupied == node.outer.occupied && node.occupied == node.near1.occupied && node.inner.occupied == 0 && node.near2.occupied == 0))
                    return true;
                if (node.HasInner() && node.HasOuter() && (node.occupied == node.outer.occupied && node.occupied == node.near2.occupied && node.inner.occupied == 0 && node.near1.occupied == 0))
                    return true;
            }
            return false;
        }
        public int doubleMorrisCount(int p)//Checks how many double morrises are there for player p
        {
            int doubleMorris = 0;
            bool first = true;
            MorrisNode helper = hooks.hooks;
            while (helper.outer != null || first)
            {
                if (!first)
                {
                    helper = helper.outer;
                }
                for (int i = 1; i < 9; i++)
                {
                    if (helper.occupied == p)
                    {
                        doubleMorris += doubleMorrisHelp(helper) ? 1 : 0;
                    }
                    helper = helper.near1;
                }
                first = false;
            }
            return doubleMorris;
        }
        private bool doubleMorrisHelp(MorrisNode node)
        {
            int morrisCount = 0;
            if (node.place == TopLeft || node.place == TopRight || node.place == BottomLeft || node.place == BottomRight)
            {
                if ((node.occupied == node.near1.occupied && node.occupied == node.near1.near1.occupied))
                    morrisCount++;
                if ((node.occupied == node.near2.occupied && node.occupied == node.near2.near2.occupied))
                    morrisCount++;
            }
            else if (node.place == MidTop || node.place == MidBottom || node.place == MidRight || node.place == MidLeft)
            {
                if (node.occupied == node.near1.occupied && node.occupied == node.near2.occupied)
                    morrisCount++;
                if (node.HasInner() && node.HasOuter() && (node.occupied == node.inner.occupied) && (node.occupied == node.outer.occupied))
                    morrisCount++;
                if (node.HasOuter() && node.outer.HasOuter() && (node.occupied == node.outer.occupied) && (node.occupied == node.outer.outer.occupied))
                    morrisCount++;
                if (node.HasInner() && node.inner.HasInner() && (node.occupied == node.inner.occupied) && (node.occupied == node.inner.inner.occupied))
                    morrisCount++;
            }
            return (morrisCount == 2);
        }
    }
}
