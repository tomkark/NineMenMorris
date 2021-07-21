using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static NMM.Const;
using static NMM.Const.location;
using System.Windows.Forms;

namespace NMM
{
    class Board
    {
        public int winner, phase, player, lastRandomized;
        public List<MorrisNode> moves, removeMoves;
        public bool removeOn, onGoing, gameOver; //isMoveBack;
        public IndexHooks hooks;
        public int[] playerPieces, leftPieces, morrises;
        public Stack<Move> moveHistory;
        public Board(int layer = layers) // Constructor
        {
            lastRandomized = 0;
            hooks = new IndexHooks(layer);
            moveHistory = new Stack<Move>();
            moves = new List<MorrisNode>();
            removeMoves = new List<MorrisNode>();
			playerPieces = new int[2];
            morrises = new int[2] { 0, 0 };
			leftPieces = new int[2]{ piecesToPut, piecesToPut };
			player = 1;
			removeOn = false;
			onGoing = false;
        } 
        public int PlacePiece(MorrisNode src, MorrisNode dest)//Places piece in the selected node
        {
            /* Return Value: 
            0 - Piece Was succesfully placed (no mill).
            1 - Piece was succesfully placed (mill).
            2 - A piece already exists there.
            3 - Cannot put piece in there
            4 - Both arguments were invalid.
			*/
            bool phaseTransition = false;
            if (src == null && dest == null) return 4; //Cannot place or move from a null to a null
            if (dest.occupied != 0) return 2;//cannot place in an occupied piece
            if (phase != 0 && !moves.Contains(dest)) //If it's not phase 0 and moves is null, you can't move since no available pieces can be moved to, unless it's a moveBack call we end this function.
            {
                return 3;
            }
            dest.occupied = player;
            if (phase == 0)
            {
                leftPieces[player - 1]--;
                if (leftPieces[0] == 0 && leftPieces[1] == 0)
                {
                    playerPieces[0] = CountPieces(1);
                    playerPieces[1] = CountPieces(2);
                    phase = 1;
                    phaseTransition = true;
                }
            }
            if (src != null)
            {
                if (src.isInMill)
                {
                    dest.occupied = 0;
                    CreateOrDeleteOrVerifyMill(src, 0); // src.occupied is set to 0 in CreateOrFixMill already. so no need to do this.
                    dest.occupied = player;
                }
                src.occupied = 0;
            }
            bool temp = CreateOrDeleteOrVerifyMill(dest, 1);
            removeOn = temp; // True if temp is true because then we created a mill and we shall have removeOn=true.
            moveHistory.Push(new Move(src, dest, null, (phaseTransition) ? 0 : phase));
            int checkWinRes = CheckWin();
            if (!removeOn)
            {
                player = 3 - player;
                if(!(phaseTransition && checkWinRes != 0))
                    onGoing = false;
                lastRandomized++;
                return 0;
            }
            removeMoves = PossibleRemoves(player);
            return 1;
        }
        public int CountPieces(int p) // 1 - player 1, 2 - player 2
        {
            //Counts pieces for player p
            int count = 0;
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
                        count++;
                    }
                    helper = helper.near1;
                }
                first = false;
            }
            return count;
        }
        public bool ChoosePiece(MorrisNode node)//Chooses the selected piece if possible
        {
            if (node.occupied != player) return false;
            moves = PossibleMoves(node);
            if (moves.Count == 0)
                return false;
            onGoing = true;
            return true;
        }  
        public int RemovePiece(MorrisNode node)//Removes a piece for the opposite player
        {
            /*
			Return Value: 
			0 - Piece Was succesfully removed.
			1 - Piece is not occupied and therefore cannot be removed.
			2 - Piece is occupied by the same player and therefore cannot be removed.
			3 - Piece is not removable, probably because it's in a mill and there are other non mill options
            4 - Piece Was succesfully removed and current player has won!
			*/
            bool tempIsInMill = false;
            if (node.occupied == 0)
            {
                return 1;
            }
            if (node.occupied == player)
            {
                return 2;
            }
            if (removeMoves.Contains(node))
            {
                if (node.isInMill)
                {
                    tempIsInMill = true;
                    CreateOrDeleteOrVerifyMill(node, 0);
                }
                node.occupied = 0;
                playerPieces[3 - player - 1]--;
                removeOn = false;
                onGoing = false;
                if (moveHistory.Peek().ate == null)
                {
                    Move updateLast = moveHistory.Pop();
                    updateLast.SetAte(node, tempIsInMill);
                    moveHistory.Push(updateLast);
                }
                if (phase != 0)
                {
                    CheckWin();
                }
                player = 3 - player;
                lastRandomized = 0;
                return 0;
            }
            return 3;
        }//Removes a piece for the opposite player
        public int CheckWin()// Checks whether a player has won
        {
            /* Return Value: 
            0 - No one has won.
            1 - Player 1 won.
            2 - Player 2 won.
			*/


            //If one of the player cannot move at all, meaning all possible moves returns an empty list, he automatically loses, i don't think it's a formal rule but i found it on an article researching the game.
            // I did not check if that works since i can't really think of a way to create a situation like this game-wise.
            if (phase == 0) return 0;
            if(playerPieces[0] > 2 && AllPossibleMoves(1).Count != 0) 
            { // Player 1 has not lost
                if (playerPieces[1] > 2 && AllPossibleMoves(2).Count != 0)
                { //Player 2 has not lost
                    return 0;
                }
                else
                {
                    gameOver = true;
                    winner = 1;
                    return 1;
                }
            }
            gameOver = true;
            winner = 2;
            return 2;
        }
        public List<Move> AllPossibleMoves(int p)//All current possible moves for player p.
        {
            List<Move> APM = new List<Move>();
            bool first = true;
            MorrisNode helper = hooks.hooks;
            if(phase != 0)
            {
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
                            List<MorrisNode> possible = PossibleMoves(helper);
                            foreach (MorrisNode node in possible)
                            {
                                helper.occupied = 0;
                                node.occupied = p;
                                bool ate = CreateOrDeleteOrVerifyMill(node, 2);
                                helper.occupied = p;
                                node.occupied = 0;
                                if (ate)
                                {
                                    foreach (MorrisNode remove in PossibleRemoves(p))
                                    {
                                        APM.Add(new Move(helper, node, remove, phase));
                                    }
                                }
                                else
                                {
                                    APM.Add(new Move(helper, node, null, phase));
                                }
                            }
                        }
                        helper = helper.near1;
                    }
                    first = false;
                }
            }
            else
            {
                while (helper.outer != null || first)
                {
                    if (!first)
                    {
                        helper = helper.outer;
                    }
                    for (int i = 1; i < 9; i++)
                    {
                        if (helper.occupied == 0)
                        {
                            helper.occupied = p;
                            bool ate = CreateOrDeleteOrVerifyMill(helper, 2);
                            helper.occupied = 0;
                            if (ate)
                            {
                                foreach (MorrisNode remove in PossibleRemoves(p))
                                {
                                    APM.Add(new Move(null, helper, remove, phase));
                                }
                            }
                            else
                            {
                                APM.Add(new Move(null, helper, null, phase));
                            }
                        }
                        helper = helper.near1;
                    }
                    first = false;
                }
            }
            return APM;
        }
        public List<MorrisNode> PossibleMoves(MorrisNode node)
        {
            if (node == null || node.occupied == 0) return null;
            bool fly = playerPieces[player - 1] <= 3;
            List<MorrisNode> neighbours = new List<MorrisNode>();
            if (!fly)
            {
                if (node.near1.occupied == 0)
                {
                    neighbours.Add(node.near1);
                }
                if (node.near2.occupied == 0)
                {
                    neighbours.Add(node.near2);
                }
                if (node.HasInner() && node.inner.occupied == 0)
                {
                    neighbours.Add(node.inner);
                }
                if (node.HasOuter() && node.outer.occupied == 0)
                {
                    neighbours.Add(node.outer);

                }
            }
            else if (fly)
            {
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
                        if (helper.occupied == 0)
                        {
                            neighbours.Add(helper);
                        }
                        helper = helper.near1;
                    }
                    first = false;
                }
            }
            return neighbours;
        }
        public List<MorrisNode> PossibleRemoves(int p)
        {
            List<MorrisNode> possibilities = new List<MorrisNode>();
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
                    if (helper.occupied == 3 - p && !helper.isInMill)
                    {
                        possibilities.Add(helper);
                    }
                    helper = helper.near1;
                }
                first = false;
            }
            if(possibilities.Count == 0)
            {
                first = true;
                helper = hooks.hooks;
                while (helper.outer != null || first)
                {
                    if (!first)
                    {
                        helper = helper.outer;
                    }
                    for (int i = 1; i < 9; i++)
                    {
                        if (helper.occupied == 3 - p)
                        {
                            possibilities.Add(helper);
                        }
                        helper = helper.near1;
                    }
                    first = false;
                }
            }
            return possibilities;
        }
        public bool CreateOrDeleteOrVerifyMill(MorrisNode node, int create = 0)//create=true is for mill creation, create=false is for removal(mill verification).
        {
            // create = 0: unmill the selected node and recursively check the other ones in the mill.
            // create = 1: creates a mill if possible.
            // create = 2: verify the node is in a mill
            List<MorrisNode[]> list = new List<MorrisNode[]>();
            if (node.place == TopLeft || node.place == TopRight || node.place == BottomLeft || node.place == BottomRight)
            {
                if ((node.occupied == node.near1.occupied && node.occupied == node.near1.near1.occupied))
                    list.Add(new MorrisNode[2]{node.near1, node.near1.near1});               
                if ((node.occupied == node.near2.occupied && node.occupied == node.near2.near2.occupied))
                    list.Add(new MorrisNode[2] {node.near2, node.near2.near2});
            }
            else if (node.place == MidTop || node.place == MidBottom || node.place == MidRight || node.place == MidLeft)
            {
                if (node.occupied == node.near1.occupied && node.occupied == node.near2.occupied)
                    list.Add(new MorrisNode[2] {node.near1, node.near2});
                if (node.HasInner() && node.HasOuter() && (node.occupied == node.inner.occupied) && (node.occupied == node.outer.occupied))
                    list.Add(new MorrisNode[2] {node.outer, node.inner});
                if (node.HasOuter() && node.outer.HasOuter() && (node.occupied == node.outer.occupied) && (node.occupied == node.outer.outer.occupied))
                    list.Add(new MorrisNode[2] { node.outer, node.outer.outer});
                if (node.HasInner() && node.inner.HasInner() && (node.occupied == node.inner.occupied) && (node.occupied == node.inner.inner.occupied))
                    list.Add(new MorrisNode[2] {node.inner, node.inner.inner});
            }
            if(list.Count > 0)
            {
                int temp = node.occupied;
                if(create == 2)
                {
                    return true;
                }
                foreach(MorrisNode[] arr in list)
                {
                    if (create==1) // create is true if we are establishing a new mill
                    {
                        if (node.occupied > 0)
                            morrises[node.occupied-1]++;
                        node.isInMill = true;
                        arr[0].isInMill = true;
                        arr[1].isInMill = true;
                        //MillFormer = node;
                        //morrises[player - 1]++;
                    }
                    else //we will fix recursively accidental mills or fake mills.
                    {
                        if(node.occupied > 0)morrises[node.occupied - 1]--;
                        node.isInMill = false;
                        node.occupied = 0;
                        arr[0].isInMill = CreateOrDeleteOrVerifyMill(arr[0], 2);
                        arr[1].isInMill = CreateOrDeleteOrVerifyMill(arr[1], 2);
                        node.occupied = temp;
                    }
                }
                if(create == 0)
                {
                    node.occupied = 0;
                }
                return true;
            }
            return false;
        } 
        public List<MorrisNode> MoveBack() //Does a move back and returns to the previous game state
        {
            /*
			Return Value: 
            List<MorrisNode> - The nodes that were changed in order for Form1.cs to update them.
			null - could not revert move.
			*/
            if (onGoing || removeOn)
            {
                return null;
            }
            if (moveHistory.Count > 0)
            {
                if (gameOver)
                {
                    winner = 0;
                    gameOver = false;
                }
                Move temp = moveHistory.Pop();
                MorrisNode source = temp.src, dest = temp.dest, eaten = temp.ate;
                if (temp.phase == 0)// If was placed while in phase 0
                {
                    leftPieces[3 - player - 1]++; //Dest was the piece that was placed in the last move, hence the one we want to remove.
                }
                // phase 1/2 move back
                else
                {
                    source.occupied = 3 - player;
                    dest.occupied = 0;
                    CreateOrDeleteOrVerifyMill(source, 1);
                    dest.occupied = 3 - player;
                }
                if (temp.phase == 0 && phase != 0)
                {
                    phase = 0;
                }
                if(eaten != null)
                {
                    if(source != null)source.occupied = 0;
                    CreateOrDeleteOrVerifyMill(dest, 0);
                    if (source != null) source.occupied = 3-player;

                    eaten.occupied = player;
                    CreateOrDeleteOrVerifyMill(eaten, 1);
                    playerPieces[player - 1]++;
                }
                dest.occupied = 0;
                player = 3 - player;
                return (new List<MorrisNode> { source, dest, eaten });

            }
            return null;

        }
    }
}
