using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using static NMM.Const;
using System.Security.Policy;

namespace NMM
{
    public partial class GameVisuals : Form
    {
        BoardAB board;
        MorrisNode selected;
        private int layersForm, layerX, layerY, marginX = 50, marginY = 40, lineThickness = 3, tempPieces, marginFix;
        private bool isPC;
        public List<Button> top, bottom;
        List<string> hvcList, cvcList1,cvcList2;
        private Color[] clrPlayer = { Color.Red, Color.Blue};
        public GameVisuals()// Form constructor
        {
            marginFix = 0;
            layersForm = layers;
            layerX = layerWidthX;
            layerY = layerWidthY;
            InitializeComponent();
            board = new BoardAB();
            DrawBoard();
            tempPieces = piecesToPut;
            top = new List<Button>();
            bottom = new List<Button>();
            CreateSideTables();
            isPC = false;
            PhaseZero1.Text = "Player 1 has " + (piecesToPut) + " pieces left to place!";
            PhaseZero2.Text = "Player 2 has " + (piecesToPut) + " pieces left to place!";
            hvcList = new List<string>() { "Computer 1 (Simple Heuristic)", "Computer 2 (Complex Heuristic)" };
            hvcListCB.DataSource = hvcList;
            cvcList1 = new List<string>() { "Computer 1 (Simple Heuristic)", "Computer 2 (Complex Heuristic)" };
            cvcListCB1.DataSource = cvcList1;
            cvcList2 = new List<string>() { "Computer 1 (Simple Heuristic)", "Computer 2 (Complex Heuristic)" };
            cvcListCB2.DataSource = cvcList2;
        } 
        private void CreateSideTables()
        {
            Color linesColor = Color.Black;
            int yTop = 50,
                yBottom = marginY + ((layersForm - layersForm + 1) * layerY) + tileSize / 2 + ((layersForm) * layerY * 2) + 60,
                x = marginX + (layersForm - layersForm + 1) * layerX + tileSize / 2,
                tableLength = ((layersForm) * layerX * 2) + 40;

            Paint += (o, e) =>
            {
                // Top Table (Red Pieces)
                Graphics g = e.Graphics;
                using (Pen selPen = new Pen(linesColor, lineThickness)) //Top Line
                    g.DrawLine(selPen, x-20 - (lineThickness-2), yTop, x + ((layersForm) * layerX * 2) + 20+ (lineThickness - 1), yTop);
                using (Pen selPen = new Pen(linesColor, lineThickness)) //Bottom Line
                    g.DrawLine(selPen, x - 20 - (lineThickness - 2), yTop + 60, x + ((layersForm) * layerX * 2) + 20 + (lineThickness - 1), yTop + 60);
                using (Pen selPen = new Pen(linesColor, lineThickness)) // Left Line
                    g.DrawLine(selPen, x - 20, yTop, x - 20, yTop + 60);
                using (Pen selPen = new Pen(linesColor, lineThickness)) // Right Line
                    g.DrawLine(selPen, x + ((layersForm) * layerX * 2) + 20, yTop, x + ((layersForm) * layerX * 2) + 20, yTop + 60);

                //Bottom Table (Blue Pieces)
                using(Pen selPen = new Pen(linesColor, lineThickness)) //Top Line
                    g.DrawLine(selPen, x - 20 - (lineThickness - 2), yBottom+marginFix, x + ((layersForm) * layerX * 2) + 20 + (lineThickness - 1), yBottom + marginFix);
                using (Pen selPen = new Pen(linesColor, lineThickness)) //Bottom Line
                    g.DrawLine(selPen, x - 20 - (lineThickness - 2), yBottom + 60 + marginFix, x + ((layersForm) * layerX * 2) + 20 + (lineThickness - 1), yBottom + 60 + marginFix);
                using (Pen selPen = new Pen(linesColor, lineThickness)) // Left Line
                    g.DrawLine(selPen, x - 20, yBottom + marginFix, x - 20, yBottom + 60 + marginFix);
                using (Pen selPen = new Pen(linesColor, lineThickness)) // Right Line
                    g.DrawLine(selPen, x + ((layersForm) * layerX * 2) + 20, yBottom + marginFix, x + ((layersForm) * layerX * 2) + 20, yBottom + 60 + marginFix);
            };
            int tempSize = (layersForm <= 5) ? tileSize : (int)(tileSize / (1.5));
            for (int i = 0; i < tempPieces; i++)
            {
                var newPiece = new Button()
                {
                    Size = new Size(tempSize, tempSize),
                    Location = new Point((x-20) + tempSize / 2 + i * tempSize + ((3 + 2 * i) / 2) * ((tableLength / (tempPieces+1)) - tempSize), yTop + tileSize / 4 + ((tempSize != tileSize) ? tileSize / 6 : 0)),
                    BackColor = Color.Red,
                    TabStop = false,
                    Enabled = false,
                    FlatStyle = FlatStyle.Flat,
                };
                newPiece.FlatAppearance.BorderSize = 3;
                newPiece.FlatAppearance.BorderColor = Color.Black;
                top.Add(newPiece);
                Controls.Add(newPiece);
            }
            for (int i = 0; i < tempPieces; i++)
            {
                var newPiece = new Button()
                {
                    Size = new Size(tempSize, tempSize),
                    Location = new Point((x - 20) + tempSize / 2 + i * tempSize + ((3 + 2 * i) / 2) * ((tableLength / (tempPieces + 1)) - tempSize), yBottom + tileSize/ 4 + ((tempSize != tileSize) ? tileSize / 6 : 0) + marginFix),
                    BackColor = Color.Blue,
                    TabStop = false,
                    Enabled = false,
                    FlatStyle = FlatStyle.Flat,
                };
                newPiece.FlatAppearance.BorderSize = 3;
                newPiece.FlatAppearance.BorderColor = Color.Black;
                bottom.Add(newPiece);
                Controls.Add(newPiece);
            }
        }
        private void DrawBoard()
        {
            if(layersForm > 4)
            {
                marginFix = 40;
            }
            roundInfo.Text = "Player " + board.player + " place your next piece!";
            boardInfo.Text = "Game Has Begun!";
            bool first = true;
            IndexHooks helper1 = board.hooks;
            MorrisNode helper = helper1.hooks;
            while (helper.outer != null || first)
            {
                if (!first)
                {
                    helper = helper.outer;
                }
                for (int i = 1; i < 9; i++)
                {
                    MorrisButton b = DrawNode(helper);
                    b.ancestor = helper;
                    helper.btn = b;;
                    helper = helper.near1;
                }
                DrawSquare(helper.layerNum, helper.near2.btn.Location.X, helper.near2.btn.Location.Y, (helper.near1.btn.Location.X - helper.near2.btn.Location.X), (helper.near2.near2.near2.btn.Location.Y - helper.near2.btn.Location.Y), helper);
                first = false;
            }
            board.hooks = helper1;
        } // Draws the board with a simple loop on IndexHooks and with the use of DrawNode() and DrawSquare()
        public MorrisButton DrawNode(MorrisNode node)
        {
            int x = 0, y = 0;
            switch ((int)node.place) // X Switch
            {
                case 1:
                case 5:
                    x = Const.SIZEX / 2;
                    break;
                case 2:
                case 3:
                case 4:
                    x = Const.SIZEX - ((layersForm - node.layerNum) * layerX);
                    break;
                case 6:
                case 7:
                case 8:
                    x = ((layersForm - node.layerNum) * layerX);
                    break;
            }
            switch ((int)node.place) // Y Switch
            {
                case 3:
                case 7:
                    y = Const.SIZEY / 2;
                    break;
                case 4:
                case 5:
                case 6:
                    y = Const.SIZEY - ((layersForm - node.layerNum) * layerY);
                    break;
                case 1:
                case 2:
                case 8:
                    y = (layersForm - node.layerNum) * layerY;
                    break;
            }
            var newPiece = new MorrisButton()
            {
                Size = new Size(tileSize, tileSize),
                Location = new Point(x+marginX, y+marginY+marginFix),
                BackColor = GameVisuals.DefaultBackColor,
                TabStop = false,
                Tag = "tile",
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter
            };
            newPiece.FlatAppearance.BorderSize = 4;
            newPiece.FlatAppearance.BorderColor = Color.Black;
            newPiece.Click += new EventHandler(ClickHandler);
            newPiece.MouseDown += new MouseEventHandler(PieceInfo);
            Controls.Add(newPiece);
            return newPiece;
        } // Creates a tile with a button and returns it with information about the location.
        public void DrawSquare(int layer, int x, int y, int width, int height, MorrisNode startMid)
        {
            Color linesColor = Color.Black;
            Paint += (o, e) =>
            {
                Graphics g = e.Graphics;
                using (Pen selPen = new Pen(linesColor, lineThickness))
                    g.DrawRectangle(selPen, x+tileSize/2, y+ tileSize / 2, width, height);
                MorrisNode temp = startMid;
                if (layer == layersForm - 1)
                {
                    for(int i = 0; i < 4; i++)
                    {
                        while (temp.HasInner())
                        {
                            temp = temp.inner;
                        }
                        using (Pen selPen = new Pen(linesColor, lineThickness))
                            g.DrawLine(selPen, new Point(startMid.btn.Location.X+tileSize/2, startMid.btn.Location.Y + tileSize / 2), new Point(temp.btn.Location.X + tileSize / 2, temp.btn.Location.Y + tileSize / 2));
                        startMid = startMid.near1.near1;
                        temp = startMid;
                    }
                }
            };
        } // Draws a square based on the constant information and layer number.
        private void PieceInfo(object sender, MouseEventArgs e)
        {
            MorrisButton b = (MorrisButton)sender;
            if (e.Button == MouseButtons.Right)
            {
                string info = string.Format("-Red Pieces: {7}\n-Blue Pieces: {8}\n-Red Morrises: {11}\n-Blue Morrises: {12}\nOccupied by: Player {10}\n\nLayer: {0}.\nLocation: {1}.\nIsInMill: {2}.\nWidth: {3}\nHeight: {4}\nBorderColor: {5}\nBorderSize: {6}\nSelected: {9}", b.ancestor.layerNum, b.ancestor.place, b.ancestor.isInMill, b.Width, b.Height, b.FlatAppearance.BorderColor, b.FlatAppearance.BorderSize,board.playerPieces[0],board.playerPieces[1], b.ancestor.selected,b.ancestor.occupied,board.morrises[0],board.morrises[1]);
                MessageBox.Show(info);
            }
        }//Some helpful function in debugging that shows gamem stats when right clicking a game piece.
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nine Men Morris", "By Tom Kark");
        }
        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want a full restart?", "Restart", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Restart();
            }
        } // Restart button on the tool strip.
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }// exit button on the tool strip.
        private void changeSize(object sender, EventArgs ea)
        {
            boardInfo.Text = "Changing board.....";
            sizeModify.Enabled = false;
            for (int ix = this.Controls.Count - 1; ix >= 0; ix--)
            {
                if (this.Controls[ix] is MorrisButton && (string)this.Controls[ix].Tag == "tile")
                {
                    this.Controls[ix].Dispose();
                }
                if (ix % 8 == 0) this.Refresh();
            }
            Paint += (o, e) =>
            {
                for (int layer = 0; layer < layersForm; layer++)
                {

                    Graphics g = e.Graphics;
                    using (Pen selPen = new Pen(GameVisuals.DefaultBackColor))
                        g.DrawRectangle(selPen, ((layersForm - layer) * layerX) + tileSize / 2,
                            ((layersForm - layer) * layerY) + tileSize / 2,
                            ((layer + 1) * layerX * 2),
                            ((layer + 1) * layerY * 2));
                    g.Clear(GameVisuals.DefaultBackColor);
                }
            };
            layersForm = (int)layerNumeric.Value;
            layerX = SIZEX / ((layersForm + 1) * 2);
            layerY = SIZEY / ((layersForm + 1) * 2);
            foreach (Button r in top)
            {
                Controls.Remove(r);
            }
            foreach (Button r in bottom)
            {
                Controls.Remove(r);
            }
            top = new List<Button>();
            bottom = new List<Button>();
            tempPieces = (layersForm == 2 ? 3 : layersForm * 3);

            board = new BoardAB(layersForm);
            DrawBoard();
            CreateSideTables();

            board.leftPieces = new int[2] {tempPieces , tempPieces };
            sizeModify.Enabled = true;
            PhaseZero1.Text = "Player 1 has " + (board.leftPieces[0]) + " pieces left to place!";
            PhaseZero2.Text = "Player 2 has " + (board.leftPieces[1]) + " pieces left to place!";
            IsFlyOne.Text = "Player 1 can fly : false";
            IsFlyTwo.Text = "Player 2 can fly : false";
            PhaseZeroInfo.Enabled = true;
            hvh.Checked = true;
            radioPanel.Enabled = true;
            cpuPlay.Enabled = true;
            hintBtn.Enabled = true;
            PhaseZero1.Enabled = true;
            PhaseZero2.Enabled = true;
            PhaseZeroInfo.Visible = true;
            PhaseZero1.Visible = true;
            PhaseZero2.Visible = true;
            PhaseOneInfo.Visible = false;
            IsFlyOne.Visible = false;
            IsFlyTwo.Visible = false;
            moveBackBtn.Enabled = false;
            this.Refresh();
        } // // Change the board size dynamically and reset the game status.
        private void PlaceGraphical(MorrisNode src, MorrisNode dest)
        {
            int placed = board.PlacePiece(src,dest);
            if (placed == 0 || placed == 1)
            {
                if (board.moveHistory.Peek().phase == 0)
                {
                    if ((placed == 0 && 3-board.player == 1) || (placed == 1 && board.player == 1))
                    {
                        Controls.Remove(top[top.Count - 1]);
                        top.RemoveAt(top.Count - 1);
                    }
                    else
                    {
                        Controls.Remove(bottom[bottom.Count - 1]);
                        bottom.RemoveAt(bottom.Count - 1);
                    }
                }
                dest.btn.graphicChange = true;
                if(src != null)src.btn.graphicChange = true;
                if (board.phase != 0 && src != null)
                {
                    GraphicalUnfocus(src);
                }
                if(placed == 1)
                {
                    foreach (MorrisNode m in board.removeMoves)
                    {
                        m.selected = true;
                    }
                }
                UpdateAndShowBoard();
                if (board.gameOver)
                {
                    GraphicalWinner();
                }
                else if(!isPC)
                {
                    CompPlayGraphic();
                }
            }
        } //Places the piece graphically and also in the board.
        private void GraphicalUnfocus(MorrisNode node)
        {
            node.selected = false;
            foreach (MorrisNode m in board.moves)
            {
                m.selected = false;
            }
            UpdateAndShowBoard();
        } //Unfocuses focused nodes (usually green)
        private void GraphicalMoveBack(object sender, EventArgs e)
        {
            bool over = false;
            if (board.gameOver)
            {
                over = true;
            }
            hvh.Checked = true;
            int tempPhase = board.phase;
            List<MorrisNode> temp = board.MoveBack();
            if(over && !board.gameOver)
            {
                PhaseOneInfo.Visible = true;
                IsFlyOne.Visible = true;
                IsFlyTwo.Visible = true;
                PhaseZeroInfo.Visible = true;
                PhaseZero1.Visible = true;
                PhaseZero2.Visible = true;
                radioPanel.Enabled = true;
                Phase1();
            }
            if (board.phase == 0)
            {
                int tableLength = ((layersForm) * layerX * 2) + 40,
                    yTop = 50,
                    yBottom = marginY + ((layersForm - layersForm + 1) * layerY) + tileSize / 2 + ((layersForm) * layerY * 2) + 60,
                    x = marginX + (layersForm - layersForm + 1) * layerX + tileSize / 2;
                if (board.player == 1)
                {
                    var newPiece = new Button()
                    {
                        Size = new Size(tileSize, tileSize),
                        Location = new Point((x - 20) + tileSize / 2 + top.Count * tileSize + ((3 + 2 * top.Count) / 2) * ((tableLength / (tempPieces + 1)) - tileSize), yTop + 10),
                        BackColor = Color.Red,
                        TabStop = false,
                        Enabled = false,
                        FlatStyle = FlatStyle.Flat,
                    };
                    newPiece.FlatAppearance.BorderSize = 3;
                    newPiece.FlatAppearance.BorderColor = Color.Black;
                    Controls.Add(newPiece);
                    top.Add(newPiece);
                }
                else
                {
                    var newPiece = new Button()
                    {
                        Size = new Size(tileSize, tileSize),
                        Location = new Point((x-20) + tileSize / 2 + bottom.Count * tileSize + ((3 + 2 * bottom.Count) / 2) * ((tableLength / (tempPieces + 1)) - tileSize), yBottom + 10),
                        BackColor = Color.Blue,
                        TabStop = false,
                        Enabled = false,
                        FlatStyle = FlatStyle.Flat,
                    };
                    newPiece.FlatAppearance.BorderSize = 3;
                    newPiece.FlatAppearance.BorderColor = Color.Black;
                    Controls.Add(newPiece);
                    bottom.Add(newPiece);
                }
            }
            if (temp != null)
            {
                foreach(MorrisNode node in temp)
                {
                    if (node != null) node.btn.graphicChange = true;
                }
                if (board.phase == 0 && tempPhase != 0)
                    Phase0();
            }
            UpdateAndShowBoard();
        } //Moves back graphically and called the moveback in board.
        private void GraphicalRemove(MorrisNode node)
        {
            int temp = board.RemovePiece(node);
            if (temp == 0)
            {
                node.btn.graphicChange = true;
                foreach (MorrisNode m in board.removeMoves)
                {
                    m.selected = false;
                }
                UpdateAndShowBoard();
                if (board.gameOver)
                {
                    GraphicalWinner();
                }
                else
                {
                    roundInfo.Text = "Player " + board.player + " place your next piece!";
                    IsFlyOne.Text = "Player 1 can fly : " + (board.playerPieces[0] <= 3);
                    IsFlyTwo.Text = "Player 2 can fly : " + (board.playerPieces[1] <= 3);
                    if (!isPC)
                    {
                        CompPlayGraphic();
                    }
                }
            }
        } //Removes the piece graphically and also in the board.
        private void GraphicalWinner()
        {
            boardInfo.Text = "Player " + board.winner + " has won!!!!!!";
            PhaseOneInfo.Visible = false;
            IsFlyOne.Visible = false;
            moveBackBtn.Enabled = true;
            IsFlyTwo.Visible = false;
            PhaseZeroInfo.Visible = false;
            PhaseZero1.Visible = false;
            PhaseZero2.Visible = false;
            roundInfo.Text = "GG";
        } //Displays the winner and disables a few elements since the game is over.
        private void GraphicalChoose(MorrisNode node)
        {
            bool chosen = board.ChoosePiece(node);
            if (chosen)
            {
                selected = node;
                node.selected = true;
                foreach (MorrisNode m in board.moves)
                {
                    m.selected = true;
                }
            }
            UpdateAndShowBoard();
        } //Chooses the piece graphically and also in the board.
        private void ClickHandler(object sender, System.EventArgs e)
        {
            /* 
            Priorities In Click Handler, Game-wise:
            - Check if game is over, if it is then the board should be unavailable.
            - If not over:
                - Check if a an move or a remove move are ongoing.
                - If not:
                    - Regular play.
            */
            MorrisButton b = (MorrisButton)sender;
            MorrisNode clickedPiece = b.ancestor;
            if (!isPC && !((board.player == 2 && hvc.Checked) || cvc.Checked))
            {
                if (!board.gameOver) //If game has ended then the board should be unavailable
                {
                    if (board.onGoing || board.removeOn)
                    {
                        if (board.removeOn)
                        {
                            GraphicalRemove(clickedPiece);
                        }
                        else if (board.phase != 0)//Ongoing
                        {
                            if (clickedPiece.occupied == 0)
                            {
                                PlaceGraphical(selected, clickedPiece);
                            }
                            else if (clickedPiece.occupied == board.player)
                            {
                                GraphicalUnfocus(selected);
                                if (selected.btn == clickedPiece.btn)
                                {
                                    board.onGoing = false;
                                    roundInfo.Text = "Player " + board.player + " please choose a piece!";
                                }
                                else
                                {
                                    selected.selected = false;
                                    if (board.PossibleMoves(clickedPiece).Count == 0)
                                    {
                                        roundInfo.Text = "Player " + board.player + " please choose a piece!";
                                    }
                                    board.onGoing = false;
                                    ClickHandler(sender, e);
                                    return;
                                }
                            }

                        }
                    }
                    else if (board.phase == 0)
                    {
                        PlaceGraphical(null, clickedPiece);
                        if (board.phase != 0)
                            Phase1();

                    }
                    else
                        GraphicalChoose(clickedPiece);
                }
                hintBtn.Enabled = (!board.removeOn && !board.onGoing);
                moveBackBtn.Enabled = (!board.removeOn && !board.onGoing && (board.moveHistory.Count > 0));
                radioPanel.Enabled = (!board.removeOn && !board.onGoing);
            }          
        } //Handles the clicks in the board for the human player.
        private void Phase1()
        {
            if (board.gameOver)
            {
                GraphicalWinner();
            }
            else
            {
                PhaseZero1.Text = "Player 1 has " + (board.leftPieces[0]) + " pieces left to place!";
                PhaseZero2.Text = "Player 2 has " + (board.leftPieces[1]) + " pieces left to place!";
                PhaseZeroInfo.Enabled = false;
                PhaseZero1.Enabled = false;
                PhaseZero2.Enabled = false;
                PhaseOneInfo.Visible = true;
                IsFlyOne.Visible = true;
                IsFlyTwo.Visible = true;
                boardInfo.Text = "Game Has Begun!";
                roundInfo.Text = "Player " + board.player + " please choose a piece!";
                IsFlyOne.Text = "Player 1 can fly : " + (board.playerPieces[0] <= 3);
                IsFlyTwo.Text = "Player 2 can fly : " + (board.playerPieces[1] <= 3);
            }

        } //Transition into phase 1 graphically.
        private void Phase0()
        {
            PhaseZeroInfo.Enabled = true;
            PhaseZero1.Enabled = true;
            PhaseZero2.Enabled = true;
            PhaseOneInfo.Visible = false;
            IsFlyOne.Visible = false;
            IsFlyTwo.Visible = false;
            PhaseZero1.Text = "Player 1 has " + (board.leftPieces[0]) + " pieces left to place!";
            PhaseZero2.Text = "Player 2 has " + (board.leftPieces[1]) + " pieces left to place!";
        } //Transition into phase 0 graphically
        private void CompPlayGraphic(bool isCPUPlay = false)
        {
            if (!board.removeOn && (((hvc.Checked && board.player == 2) || cvc.Checked ) || isCPUPlay))
            {
                hintBtn.Enabled = false;
                int time = 500;
                this.Refresh();
                bool transition = false;
                radioPanel.Enabled = false;
                int eval1 = 1, eval2 = 1;
                if (hvc.Checked)
                {
                    eval2 = ((string)hvcListCB.SelectedValue == hvcList[0]) ? 1 : 2;
                }
                if (cvc.Checked)
                {
                    eval1 = ((string)cvcListCB1.SelectedValue == cvcList1[0]) ? 1 : 2;
                    eval2 = ((string)cvcListCB2.SelectedValue == cvcList2[0]) ? 1 : 2;
                }
                MorrisNode[] best = board.CompPlay(eval1,eval2);
                isPC = true;
                if (board.phase == 0)
                {
                    transition = true;
                    PlaceGraphical(null, best[1]);
                    if (board.removeOn && best[2] != null)
                    {
                        wait(time);
                        GraphicalRemove(best[2]);
                    }
                }
                else
                {
                    GraphicalChoose(best[0]);
                    wait(time);
                    if (board.onGoing)
                    {
                        PlaceGraphical(best[0], best[1]);
                        if (board.removeOn && best[2] != null)
                        {
                            wait(time);
                            GraphicalRemove(best[2]);
                        }
                    }
                }
                isPC = false;
                foreach(MorrisNode node in best)
                {
                    if(node != null) node.btn.graphicChange = true;
                }
                UpdateAndShowBoard();
                if (board.phase != 0 && transition)
                    Phase1();
                if (board.gameOver)
                {
                    GraphicalWinner();
                }
                else if (cvc.Checked)
                {
                    cpuPlay.Enabled = false;
                    radioPanel.Enabled = true;
                    wait(time*2);
                    CompPlayGraphic();
                }
                else if (hvc.Checked || isCPUPlay)
                {
                    radioPanel.Enabled = true;
                    cpuPlay.Enabled = true;
                    CompPlayGraphic();


                }
                if (hvh.Checked || (hvc.Checked && board.player == 1))
                {
                    hintBtn.Enabled = true;
                    moveBackBtn.Enabled = true;
                }

            }
        } //Called when pc is playing and manages the moves graphically and in the board.
        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        } //Wait function 
        private void UpdateAndShowBoard()
        {
            if (!board.onGoing && board.phase == 0)
            {
                roundInfo.Text = "Player " + board.player + " place your next piece!";
            }
            else if (board.moves.Count > 0 && board.onGoing)
            {
                roundInfo.Text = "Player " + board.player + " please choose an availabe tile to move your selected piece! (possiblites glow in green)";
            }
            else
            {
                roundInfo.Text = "Player " + board.player + " please choose a piece!";
            }
            if (board.removeOn)
            {
                roundInfo.Text = "Player " + board.player + " may remove one of Player's " + (3 - board.player) + " pieces!";
            }
            if (board.phase != 0)
            {
                IsFlyOne.Text = "Player 1 can fly : " + (board.playerPieces[0] <= 3);
                IsFlyTwo.Text = "Player 2 can fly : " + (board.playerPieces[1] <= 3);
            }
            else if (board.phase == 0)
            {
                PhaseZero1.Text = "Player 1 has " + (board.leftPieces[0]) + " pieces left to place!";
                PhaseZero2.Text = "Player 2 has " + (board.leftPieces[1]) + " pieces left to place!";
            }
            if (board.moveHistory.Count == 0)
            {
                moveBackBtn.Enabled = false;
            }
            bool first = true;
            MorrisNode helper = board.hooks.hooks;
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
                        if (helper.btn.graphicChange)
                        {
                            helper.btn.graphicChange = false;
                            helper.btn.BackColor = GameVisuals.DefaultBackColor;
                            helper.btn.FlatAppearance.BorderColor = Color.Black;
                        }
                        if (helper.selected)
                        {
                            helper.btn.FlatAppearance.BorderColor = Color.Green;
                        }
                        else
                        {
                            helper.btn.FlatAppearance.BorderColor = Color.Black;
                        }
                    }
                    else
                    {
                        if (helper.btn.graphicChange)
                        {
                            helper.btn.graphicChange = false;
                        }
                        helper.btn.BackColor = clrPlayer[helper.occupied - 1];
                        if (helper.selected)
                        {
                            helper.btn.FlatAppearance.BorderColor = Color.Green;
                        }
                        else
                        {
                            helper.btn.FlatAppearance.BorderColor = (helper.isInMill) ? Color.Gray : Color.Black;
                        }
                    }
                    helper = helper.near1;
                }
                first = false;
            }
        } //Updates the board state graphically and the information surrounding it.

        private void hvcListCB_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        } //Prevent the user from typing in the DropDown text.

        private void cvcListCB1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }//Prevent the user from typing in the DropDown text.

        private void cvcListCB2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }//Prevent the user from typing in the DropDown text.
        private void singleCPUPlay(object sender, EventArgs e)
        {
            CompPlayGraphic(true);
        } //Does a single computer play for the current player.

        private void radioChange(object sender, EventArgs e)
        {
            if (hvh.Checked)
            {
                if (!board.gameOver)
                {
                    cpuPlay.Enabled = true;

                }
                if (board.moveHistory.Count > 0)
                    moveBackBtn.Enabled = true;
            }
            if(!board.onGoing && !board.removeOn && !board.gameOver)
            {
                if((hvc.Checked && board.player == 2) || cvc.Checked)
                {
                    cpuPlay.Enabled = false;
                }
                CompPlayGraphic();
            }
        } //Manages the radio button changes.
        private void hintClick(object sender, EventArgs e)
        {
            string temp = roundInfo.Text;
            hintBtn.Enabled = false;
            int time = 250;
            int blinkCount = 5;
            int eval1 = 1, eval2 = 1;
            if (hvc.Checked)
            {
                eval2 = ((string)hvcListCB.SelectedValue == hvcList[0]) ? 1 : 2;
            }
            if (cvc.Checked)
            {
                eval1 = ((string)cvcListCB1.SelectedValue == cvcList1[0]) ? 1 : 2;
                eval2 = ((string)cvcListCB2.SelectedValue == cvcList2[0]) ? 1 : 2;
            }
            MorrisNode[] best = board.CompPlay(eval1,eval2);
            isPC = true;
            roundInfo.ForeColor = Color.Orange;
            if (best[0] != null)
            {
                roundInfo.Text = "Choose the blinking piece";
                for (int i = 0; i < blinkCount; i++)
                {
                    wait(time);
                    best[0].btn.FlatAppearance.BorderSize += 4;
                    best[0].btn.FlatAppearance.BorderColor = Color.Orange;
                    wait(time);
                    best[0].btn.FlatAppearance.BorderSize -= 4;
                    best[0].btn.FlatAppearance.BorderColor = Color.Black;
                }
            }
            roundInfo.Text = "Move the chosen piece to the blinking spot";
            if (board.phase == 0)
            {
                roundInfo.Text = "Place your next piece in the blinking spot";
            }

            for (int i = 0; i < blinkCount; i++)
            {
                wait(time);
                best[1].btn.FlatAppearance.BorderSize += 2;
                best[1].btn.FlatAppearance.BorderColor = Color.Orange;
                wait(time);
                best[1].btn.FlatAppearance.BorderSize -= 2;
                best[1].btn.FlatAppearance.BorderColor = Color.Black;
            }
            if (best[2] != null)
            {
                roundInfo.Text = "After the move eat the blinking piece";
                for (int i = 0; i < blinkCount; i++)
                {
                    wait(time);
                    best[2].btn.FlatAppearance.BorderSize += 2;
                    best[2].btn.FlatAppearance.BorderColor = Color.Orange;
                    wait(time);
                    best[2].btn.FlatAppearance.BorderSize -= 2;
                    best[2].btn.FlatAppearance.BorderColor = Color.Black;
                }
            }  
            roundInfo.Text = "After the move eat the blinking piece";
            roundInfo.Text = temp;
            roundInfo.ForeColor = Color.Black;
            isPC = false;
            hintBtn.Enabled = true;
        } //Does a computer play but does not play it, instead it gives the user a "hint" with this move.
    }
}
