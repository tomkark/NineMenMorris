namespace NMM
{
    partial class GameVisuals
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameVisuals));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boardInfo = new System.Windows.Forms.Label();
            this.boardSizeInfo = new System.Windows.Forms.Label();
            this.layerNumeric = new System.Windows.Forms.NumericUpDown();
            this.PhaseZeroInfo = new System.Windows.Forms.Label();
            this.PhaseZero1 = new System.Windows.Forms.Label();
            this.PhaseZero2 = new System.Windows.Forms.Label();
            this.PhaseOneInfo = new System.Windows.Forms.Label();
            this.roundInfo = new System.Windows.Forms.Label();
            this.IsFlyOne = new System.Windows.Forms.Label();
            this.IsFlyTwo = new System.Windows.Forms.Label();
            this.moveBackBtn = new System.Windows.Forms.Button();
            this.radioPanel = new System.Windows.Forms.Panel();
            this.cvcListCB2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cvcListCB1 = new System.Windows.Forms.ComboBox();
            this.hvcListCB = new System.Windows.Forms.ComboBox();
            this.cvc = new System.Windows.Forms.RadioButton();
            this.hvc = new System.Windows.Forms.RadioButton();
            this.hvh = new System.Windows.Forms.RadioButton();
            this.hintBtn = new System.Windows.Forms.Button();
            this.cpuPlay = new System.Windows.Forms.Button();
            this.sizeModify = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layerNumeric)).BeginInit();
            this.radioPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1384, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restartToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(38, 22);
            this.toolStripDropDownButton1.Text = "File";
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(45, 22);
            this.toolStripDropDownButton2.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // boardInfo
            // 
            this.boardInfo.Font = new System.Drawing.Font("Trebuchet MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boardInfo.ForeColor = System.Drawing.Color.Red;
            this.boardInfo.Location = new System.Drawing.Point(967, 109);
            this.boardInfo.Name = "boardInfo";
            this.boardInfo.Size = new System.Drawing.Size(334, 78);
            this.boardInfo.TabIndex = 2;
            this.boardInfo.Text = "Game Title";
            // 
            // boardSizeInfo
            // 
            this.boardSizeInfo.AutoSize = true;
            this.boardSizeInfo.Location = new System.Drawing.Point(972, 533);
            this.boardSizeInfo.Name = "boardSizeInfo";
            this.boardSizeInfo.Size = new System.Drawing.Size(117, 13);
            this.boardSizeInfo.TabIndex = 3;
            this.boardSizeInfo.Text = "Change Board Size To:";
            // 
            // layerNumeric
            // 
            this.layerNumeric.Location = new System.Drawing.Point(1096, 531);
            this.layerNumeric.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.layerNumeric.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.layerNumeric.Name = "layerNumeric";
            this.layerNumeric.Size = new System.Drawing.Size(120, 20);
            this.layerNumeric.TabIndex = 4;
            this.layerNumeric.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // PhaseZeroInfo
            // 
            this.PhaseZeroInfo.Font = new System.Drawing.Font("Trebuchet MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PhaseZeroInfo.Location = new System.Drawing.Point(967, 159);
            this.PhaseZeroInfo.Name = "PhaseZeroInfo";
            this.PhaseZeroInfo.Size = new System.Drawing.Size(334, 28);
            this.PhaseZeroInfo.TabIndex = 6;
            this.PhaseZeroInfo.Text = "Phase zero: Place Pieces:";
            // 
            // PhaseZero1
            // 
            this.PhaseZero1.Font = new System.Drawing.Font("Trebuchet MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PhaseZero1.Location = new System.Drawing.Point(967, 187);
            this.PhaseZero1.Name = "PhaseZero1";
            this.PhaseZero1.Size = new System.Drawing.Size(334, 28);
            this.PhaseZero1.TabIndex = 7;
            // 
            // PhaseZero2
            // 
            this.PhaseZero2.Font = new System.Drawing.Font("Trebuchet MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PhaseZero2.Location = new System.Drawing.Point(967, 215);
            this.PhaseZero2.Name = "PhaseZero2";
            this.PhaseZero2.Size = new System.Drawing.Size(334, 28);
            this.PhaseZero2.TabIndex = 8;
            // 
            // PhaseOneInfo
            // 
            this.PhaseOneInfo.Font = new System.Drawing.Font("Trebuchet MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PhaseOneInfo.Location = new System.Drawing.Point(967, 275);
            this.PhaseOneInfo.Name = "PhaseOneInfo";
            this.PhaseOneInfo.Size = new System.Drawing.Size(334, 28);
            this.PhaseOneInfo.TabIndex = 9;
            this.PhaseOneInfo.Text = "Phase One: Move Pieces";
            this.PhaseOneInfo.Visible = false;
            // 
            // roundInfo
            // 
            this.roundInfo.Font = new System.Drawing.Font("Trebuchet MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roundInfo.Location = new System.Drawing.Point(273, 9);
            this.roundInfo.Name = "roundInfo";
            this.roundInfo.Size = new System.Drawing.Size(782, 28);
            this.roundInfo.TabIndex = 10;
            this.roundInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IsFlyOne
            // 
            this.IsFlyOne.Font = new System.Drawing.Font("Trebuchet MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsFlyOne.Location = new System.Drawing.Point(967, 303);
            this.IsFlyOne.Name = "IsFlyOne";
            this.IsFlyOne.Size = new System.Drawing.Size(334, 28);
            this.IsFlyOne.TabIndex = 11;
            this.IsFlyOne.Visible = false;
            // 
            // IsFlyTwo
            // 
            this.IsFlyTwo.Font = new System.Drawing.Font("Trebuchet MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsFlyTwo.Location = new System.Drawing.Point(967, 331);
            this.IsFlyTwo.Name = "IsFlyTwo";
            this.IsFlyTwo.Size = new System.Drawing.Size(334, 28);
            this.IsFlyTwo.TabIndex = 12;
            this.IsFlyTwo.Visible = false;
            // 
            // moveBackBtn
            // 
            this.moveBackBtn.Enabled = false;
            this.moveBackBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moveBackBtn.Location = new System.Drawing.Point(971, 507);
            this.moveBackBtn.Name = "moveBackBtn";
            this.moveBackBtn.Size = new System.Drawing.Size(127, 23);
            this.moveBackBtn.TabIndex = 13;
            this.moveBackBtn.Text = "Move Back A Step";
            this.moveBackBtn.UseVisualStyleBackColor = true;
            this.moveBackBtn.Click += new System.EventHandler(this.GraphicalMoveBack);
            // 
            // radioPanel
            // 
            this.radioPanel.Controls.Add(this.cvcListCB2);
            this.radioPanel.Controls.Add(this.label1);
            this.radioPanel.Controls.Add(this.cvcListCB1);
            this.radioPanel.Controls.Add(this.hvcListCB);
            this.radioPanel.Controls.Add(this.cvc);
            this.radioPanel.Controls.Add(this.hvc);
            this.radioPanel.Controls.Add(this.hvh);
            this.radioPanel.Location = new System.Drawing.Point(971, 429);
            this.radioPanel.Name = "radioPanel";
            this.radioPanel.Size = new System.Drawing.Size(409, 73);
            this.radioPanel.TabIndex = 14;
            // 
            // cvcListCB2
            // 
            this.cvcListCB2.DisplayMember = "Computer 1 (Simple Heuristic)";
            this.cvcListCB2.FormattingEnabled = true;
            this.cvcListCB2.Location = new System.Drawing.Point(226, 47);
            this.cvcListCB2.Name = "cvcListCB2";
            this.cvcListCB2.Size = new System.Drawing.Size(175, 21);
            this.cvcListCB2.TabIndex = 6;
            this.cvcListCB2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cvcListCB2_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(205, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Vs";
            // 
            // cvcListCB1
            // 
            this.cvcListCB1.DisplayMember = "Computer 1 (Simple Heuristic)";
            this.cvcListCB1.FormattingEnabled = true;
            this.cvcListCB1.Location = new System.Drawing.Point(24, 47);
            this.cvcListCB1.Name = "cvcListCB1";
            this.cvcListCB1.Size = new System.Drawing.Size(175, 21);
            this.cvcListCB1.TabIndex = 4;
            this.cvcListCB1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cvcListCB1_KeyPress);
            // 
            // hvcListCB
            // 
            this.hvcListCB.DisplayMember = "Computer 1 (Simple Heuristic)";
            this.hvcListCB.FormattingEnabled = true;
            this.hvcListCB.Location = new System.Drawing.Point(78, 23);
            this.hvcListCB.Name = "hvcListCB";
            this.hvcListCB.Size = new System.Drawing.Size(175, 21);
            this.hvcListCB.TabIndex = 3;
            this.hvcListCB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.hvcListCB_KeyPress);
            // 
            // cvc
            // 
            this.cvc.AutoSize = true;
            this.cvc.Location = new System.Drawing.Point(4, 50);
            this.cvc.Name = "cvc";
            this.cvc.Size = new System.Drawing.Size(14, 13);
            this.cvc.TabIndex = 2;
            this.cvc.UseVisualStyleBackColor = true;
            this.cvc.CheckedChanged += new System.EventHandler(this.radioChange);
            // 
            // hvc
            // 
            this.hvc.AutoSize = true;
            this.hvc.Location = new System.Drawing.Point(4, 27);
            this.hvc.Name = "hvc";
            this.hvc.Size = new System.Drawing.Size(77, 17);
            this.hvc.TabIndex = 1;
            this.hvc.Text = "Human Vs ";
            this.hvc.UseVisualStyleBackColor = true;
            this.hvc.CheckedChanged += new System.EventHandler(this.radioChange);
            // 
            // hvh
            // 
            this.hvh.AutoSize = true;
            this.hvh.Checked = true;
            this.hvh.Location = new System.Drawing.Point(4, 4);
            this.hvh.Name = "hvh";
            this.hvh.Size = new System.Drawing.Size(111, 17);
            this.hvh.TabIndex = 0;
            this.hvh.TabStop = true;
            this.hvh.Text = "Human Vs Human";
            this.hvh.UseVisualStyleBackColor = true;
            this.hvh.CheckedChanged += new System.EventHandler(this.radioChange);
            // 
            // hintBtn
            // 
            this.hintBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hintBtn.Location = new System.Drawing.Point(971, 617);
            this.hintBtn.Name = "hintBtn";
            this.hintBtn.Size = new System.Drawing.Size(239, 23);
            this.hintBtn.TabIndex = 15;
            this.hintBtn.Text = "Hint";
            this.hintBtn.UseVisualStyleBackColor = true;
            this.hintBtn.Click += new System.EventHandler(this.hintClick);
            // 
            // cpuPlay
            // 
            this.cpuPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cpuPlay.Location = new System.Drawing.Point(971, 588);
            this.cpuPlay.Name = "cpuPlay";
            this.cpuPlay.Size = new System.Drawing.Size(239, 23);
            this.cpuPlay.TabIndex = 17;
            this.cpuPlay.Text = "CPU Play";
            this.cpuPlay.UseVisualStyleBackColor = true;
            this.cpuPlay.Click += new System.EventHandler(this.singleCPUPlay);
            // 
            // sizeModify
            // 
            this.sizeModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sizeModify.Location = new System.Drawing.Point(971, 559);
            this.sizeModify.Name = "sizeModify";
            this.sizeModify.Size = new System.Drawing.Size(240, 23);
            this.sizeModify.TabIndex = 18;
            this.sizeModify.Text = "Restart with new size";
            this.sizeModify.UseVisualStyleBackColor = true;
            this.sizeModify.Click += new System.EventHandler(this.changeSize);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1384, 1041);
            this.Controls.Add(this.sizeModify);
            this.Controls.Add(this.cpuPlay);
            this.Controls.Add(this.hintBtn);
            this.Controls.Add(this.radioPanel);
            this.Controls.Add(this.moveBackBtn);
            this.Controls.Add(this.IsFlyTwo);
            this.Controls.Add(this.IsFlyOne);
            this.Controls.Add(this.roundInfo);
            this.Controls.Add(this.PhaseOneInfo);
            this.Controls.Add(this.PhaseZero2);
            this.Controls.Add(this.PhaseZero1);
            this.Controls.Add(this.PhaseZeroInfo);
            this.Controls.Add(this.layerNumeric);
            this.Controls.Add(this.boardSizeInfo);
            this.Controls.Add(this.boardInfo);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MaximumSize = new System.Drawing.Size(1400, 1080);
            this.MinimumSize = new System.Drawing.Size(1400, 1038);
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TomMorris";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layerNumeric)).EndInit();
            this.radioPanel.ResumeLayout(false);
            this.radioPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label boardInfo;
        private System.Windows.Forms.Label boardSizeInfo;
        private System.Windows.Forms.NumericUpDown layerNumeric;
        private System.Windows.Forms.Label PhaseZeroInfo;
        private System.Windows.Forms.Label PhaseZero1;
        private System.Windows.Forms.Label PhaseZero2;
        private System.Windows.Forms.Label PhaseOneInfo;
        private System.Windows.Forms.Label roundInfo;
        private System.Windows.Forms.Label IsFlyOne;
        private System.Windows.Forms.Label IsFlyTwo;
        private System.Windows.Forms.Button moveBackBtn;
        private System.Windows.Forms.Panel radioPanel;
        private System.Windows.Forms.RadioButton hvc;
        private System.Windows.Forms.RadioButton hvh;
        private System.Windows.Forms.RadioButton cvc;
        private System.Windows.Forms.Button hintBtn;
        private System.Windows.Forms.Button cpuPlay;
        private System.Windows.Forms.ComboBox hvcListCB;
        private System.Windows.Forms.ComboBox cvcListCB2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cvcListCB1;
        private System.Windows.Forms.Button sizeModify;
    }
}

