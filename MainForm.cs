using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HanoiTower
{
    public partial class MainForm : Form
    {
        private HanoiGame game;
        private HanoiAlg solver;
        private TowerRenderer renderer;
        private int selectedTower = -1;
        private Queue<Move> solutionMoves;
        private bool isAutoSolving = false;
        private int animationSpeed = 500;
        private int totalSolutionMoves = 0;
        private int currentSolutionStep = 0;

        private bool isAnimating = false;
        private Move pendingMove = null;

        public MainForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            InitializeGame((int)numDisks.Value);
            UpdateSpeedLabel();
            renderer.OnFrameUpdated += () => Invalidate();
        }

        private void InitializeGame(int disks)
        {
            game = new HanoiGame(disks);
            solver = new HanoiAlg();
            renderer = new TowerRenderer();

            renderer.OnAnimationCompleted += OnAnimationCompleted;

            selectedTower = -1;
            isAutoSolving = false;
            isAnimating = false;
            pendingMove = null;
            solutionMoves = null;
            animationTimer.Stop();

            totalSolutionMoves = (int)Math.Pow(2, disks) - 1;
            currentSolutionStep = 0;

            UpdateControls();
            Invalidate();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            renderer.Draw(e.Graphics, game, selectedTower);
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (isAutoSolving || isAnimating) return;

            int clickedTower = renderer.GetTowerIndexFromPoint(e.Location);
            if (clickedTower == -1) return;

            if (selectedTower == -1)
            {
                if (!game.Towers[clickedTower].IsEmpty)
                {
                    selectedTower = clickedTower;
                    Invalidate();
                }
            }
            else
            {
                if (game.CanMove(selectedTower, clickedTower))
                {
                    var disk = game.Towers[selectedTower].Peek();
                    isAnimating = true;

                    pendingMove = new Move(selectedTower, clickedTower);
                    renderer.AnimateMove(disk, selectedTower, clickedTower, animationSpeed / 2);

                    selectedTower = -1;
                    UpdateControls();
                    Invalidate();
                }
                else
                {
                    selectedTower = -1;
                    Invalidate();
                }
            }
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            InitializeGame((int)numDisks.Value);
        }

        private void btnNextMove_Click(object sender, EventArgs e)
        {
            if (solutionMoves == null)
            {
                GenerateSolution();
            }

            if (solutionMoves.Count > 0 && !isAnimating)
            {
                ExecuteNextMove();
            }
        }

        private void btnAutoSolve_Click(object sender, EventArgs e)
        {
            if (isAutoSolving)
            {
                StopAutoSolve();
            }
            else
            {
                StartAutoSolve();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            game.Reset();
            solutionMoves = null;
            isAutoSolving = false;
            isAnimating = false;
            pendingMove = null;
            animationTimer.Stop();
            currentSolutionStep = 0;
            UpdateControls();
            Invalidate();
        }

        private void numDisks_ValueChanged(object sender, EventArgs e)
        {
            if (!isAutoSolving && !isAnimating)
            {
                InitializeGame((int)numDisks.Value);
            }
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            if (solutionMoves != null && solutionMoves.Count > 0 && !isAnimating)
            {
                ExecuteNextMove();
            }
            else if (solutionMoves != null && solutionMoves.Count == 0 && !isAnimating)
            {
                StopAutoSolve();
                if (game.IsSolved)
                {
                    ShowVictoryMessage();
                }
            }
        }

        private void btnSpeedUp_Click(object sender, EventArgs e)
        {
            if (animationSpeed > 100)
            {
                animationSpeed -= 100;
                UpdateSpeedLabel();
                if (isAutoSolving)
                {
                    animationTimer.Interval = animationSpeed;
                }
            }
        }

        private void btnSpeedDown_Click(object sender, EventArgs e)
        {
            if (animationSpeed < 2000)
            {
                animationSpeed += 100;
                UpdateSpeedLabel();
                if (isAutoSolving)
                {
                    animationTimer.Interval = animationSpeed;
                }
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void GenerateSolution()
        {
            var moves = solver.GenerateSolution(game.TotalDisks, 0, 2, 1);
            solutionMoves = new Queue<Move>(moves);
            currentSolutionStep = 0;
            progressSolution.Maximum = moves.Count;
            progressSolution.Value = 0;
        }

        private void ExecuteNextMove()
        {
            if (solutionMoves != null && solutionMoves.Count > 0 && !isAnimating)
            {
                var move = solutionMoves.Dequeue();

                if (game.CanMove(move.From, move.To))
                {
                    var disk = game.Towers[move.From].Peek();
                    isAnimating = true;
                    pendingMove = move;

                    renderer.AnimateMove(disk, move.From, move.To, animationSpeed / 2);
                    currentSolutionStep++;
                    UpdateControls();
                    Invalidate();
                }
            }
        }

        private void OnAnimationCompleted()
        {
            if (pendingMove != null)
            {
                game.Move(pendingMove.From, pendingMove.To);
                pendingMove = null;
            }

            isAnimating = false;

            if (game.IsSolved)
            {
                if (isAutoSolving)
                {
                    StopAutoSolve();
                }
                ShowVictoryMessage();
            }

            UpdateControls();
            Invalidate();
        }

        private void StartAutoSolve()
        {
            if (solutionMoves == null)
            {
                GenerateSolution();
            }

            isAutoSolving = true;
            animationTimer.Interval = animationSpeed;
            animationTimer.Start();
            UpdateControls();
        }

        private void StopAutoSolve()
        {
            isAutoSolving = false;
            animationTimer.Stop();
            UpdateControls();
        }

        private void UpdateControls()
        {
            lblMoves.Text = $"Ходов: {game.MoveCount}/{totalSolutionMoves}";

            string status = "В процессе...";
            if (game.IsSolved) status = "Решено!";
            else if (isAnimating) status = "Анимация...";
            lblStatus.Text = status;

            lblStatus.ForeColor = game.IsSolved ? Color.Green :
                                isAnimating ? Color.Blue : Color.Black;

            btnNextMove.Enabled = !isAutoSolving && !game.IsSolved && !isAnimating;
            btnAutoSolve.Text = isAutoSolving ? "Стоп" : "Авторешение";
            btnAutoSolve.BackColor = isAutoSolving ? Color.LightCoral : SystemColors.Control;
            btnAutoSolve.Enabled = !isAnimating;
            btnReset.Enabled = !isAutoSolving && !isAnimating;
            numDisks.Enabled = !isAutoSolving && !isAnimating;

            if (solutionMoves != null)
            {
                progressSolution.Value = currentSolutionStep;
            }
        }

        private void UpdateSpeedLabel()
        {
            lblSpeed.Text = $"Скорость: {2100 - animationSpeed}%";
        }

        private void ShowVictoryMessage()
        {
            MessageBox.Show(
                $"Головоломка решена!\n\nЗатрачено ходов: {game.MoveCount}\nМинимальное количество ходов: {totalSolutionMoves}",
                "Победа!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}