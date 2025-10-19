namespace HanoiTower
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.panelControls = new System.Windows.Forms.Panel();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.btnSpeedDown = new System.Windows.Forms.Button();
            this.btnSpeedUp = new System.Windows.Forms.Button();
            this.progressSolution = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblMoves = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnAutoSolve = new System.Windows.Forms.Button();
            this.btnNextMove = new System.Windows.Forms.Button();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.numDisks = new System.Windows.Forms.NumericUpDown();
            this.lblDiskCount = new System.Windows.Forms.Label();
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.panelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDisks)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControls
            // 
            this.panelControls.BackColor = System.Drawing.Color.LightGray;
            this.panelControls.Controls.Add(this.lblSpeed);
            this.panelControls.Controls.Add(this.btnSpeedDown);
            this.panelControls.Controls.Add(this.btnSpeedUp);
            this.panelControls.Controls.Add(this.progressSolution);
            this.panelControls.Controls.Add(this.lblStatus);
            this.panelControls.Controls.Add(this.lblMoves);
            this.panelControls.Controls.Add(this.btnReset);
            this.panelControls.Controls.Add(this.btnAutoSolve);
            this.panelControls.Controls.Add(this.btnNextMove);
            this.panelControls.Controls.Add(this.btnNewGame);
            this.panelControls.Controls.Add(this.numDisks);
            this.panelControls.Controls.Add(this.lblDiskCount);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControls.Location = new System.Drawing.Point(0, 0);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(784, 70);
            this.panelControls.TabIndex = 0;
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(680, 40);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(56, 13);
            this.lblSpeed.TabIndex = 11;
            this.lblSpeed.Text = "Скорость:";
            // 
            // btnSpeedDown
            // 
            this.btnSpeedDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSpeedDown.Location = new System.Drawing.Point(711, 10);
            this.btnSpeedDown.Name = "btnSpeedDown";
            this.btnSpeedDown.Size = new System.Drawing.Size(25, 25);
            this.btnSpeedDown.TabIndex = 10;
            this.btnSpeedDown.Text = "-";
            this.btnSpeedDown.UseVisualStyleBackColor = true;
            this.btnSpeedDown.Click += new System.EventHandler(this.btnSpeedDown_Click);
            // 
            // btnSpeedUp
            // 
            this.btnSpeedUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSpeedUp.Location = new System.Drawing.Point(680, 10);
            this.btnSpeedUp.Name = "btnSpeedUp";
            this.btnSpeedUp.Size = new System.Drawing.Size(25, 25);
            this.btnSpeedUp.TabIndex = 9;
            this.btnSpeedUp.Text = "+";
            this.btnSpeedUp.UseVisualStyleBackColor = true;
            this.btnSpeedUp.Click += new System.EventHandler(this.btnSpeedUp_Click);
            // 
            // progressSolution
            // 
            this.progressSolution.Location = new System.Drawing.Point(524, 12);
            this.progressSolution.Name = "progressSolution";
            this.progressSolution.Size = new System.Drawing.Size(150, 23);
            this.progressSolution.TabIndex = 8;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStatus.Location = new System.Drawing.Point(120, 45);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(104, 15);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "В процессе...";
            // 
            // lblMoves
            // 
            this.lblMoves.AutoSize = true;
            this.lblMoves.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblMoves.Location = new System.Drawing.Point(12, 45);
            this.lblMoves.Name = "lblMoves";
            this.lblMoves.Size = new System.Drawing.Size(85, 15);
            this.lblMoves.TabIndex = 6;
            this.lblMoves.Text = "Ходов: 0/15";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(438, 10);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(80, 25);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Сброс";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnAutoSolve
            // 
            this.btnAutoSolve.Location = new System.Drawing.Point(352, 10);
            this.btnAutoSolve.Name = "btnAutoSolve";
            this.btnAutoSolve.Size = new System.Drawing.Size(80, 25);
            this.btnAutoSolve.TabIndex = 4;
            this.btnAutoSolve.Text = "Авторешение";
            this.btnAutoSolve.UseVisualStyleBackColor = true;
            this.btnAutoSolve.Click += new System.EventHandler(this.btnAutoSolve_Click);
            // 
            // btnNextMove
            // 
            this.btnNextMove.Location = new System.Drawing.Point(256, 10);
            this.btnNextMove.Name = "btnNextMove";
            this.btnNextMove.Size = new System.Drawing.Size(90, 25);
            this.btnNextMove.TabIndex = 3;
            this.btnNextMove.Text = "Следующий ход";
            this.btnNextMove.UseVisualStyleBackColor = true;
            this.btnNextMove.Click += new System.EventHandler(this.btnNextMove_Click);
            // 
            // btnNewGame
            // 
            this.btnNewGame.Location = new System.Drawing.Point(170, 10);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(80, 25);
            this.btnNewGame.TabIndex = 2;
            this.btnNewGame.Text = "Новая игра";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // numDisks
            // 
            this.numDisks.Location = new System.Drawing.Point(114, 13);
            this.numDisks.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numDisks.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numDisks.Name = "numDisks";
            this.numDisks.Size = new System.Drawing.Size(50, 20);
            this.numDisks.TabIndex = 1;
            this.numDisks.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numDisks.ValueChanged += new System.EventHandler(this.numDisks_ValueChanged);
            // 
            // lblDiskCount
            // 
            this.lblDiskCount.AutoSize = true;
            this.lblDiskCount.Location = new System.Drawing.Point(12, 15);
            this.lblDiskCount.Name = "lblDiskCount";
            this.lblDiskCount.Size = new System.Drawing.Size(96, 13);
            this.lblDiskCount.TabIndex = 0;
            this.lblDiskCount.Text = "Количество дисков:";
            // 
            // animationTimer
            // 
            this.animationTimer.Tick += new System.EventHandler(this.animationTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.panelControls);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ханойская башня";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseClick);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDisks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.Label lblDiskCount;
        private System.Windows.Forms.NumericUpDown numDisks;
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Button btnNextMove;
        private System.Windows.Forms.Button btnAutoSolve;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblMoves;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar progressSolution;
        private System.Windows.Forms.Timer animationTimer;
        private System.Windows.Forms.Button btnSpeedUp;
        private System.Windows.Forms.Button btnSpeedDown;
        private System.Windows.Forms.Label lblSpeed;
    }
}