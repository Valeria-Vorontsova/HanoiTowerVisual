using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HanoiTower
{
    public partial class MainForm : Form
    {
        private HanoiGame game;
        private HanoiTower solver;
        private TowerRenderer renderer;
        private int selectedTower = -1;
        private Queue<Move> solutionMoves;
        private bool isAutoSolving = false;
        private int animationSpeed = 500; // мс между ходами
        private int totalSolutionMoves = 0;
        private int currentSolutionStep = 0;

        public MainForm()
        {
            InitializeComponent();
            InitializeGame((int)numDisks.Value);
            UpdateSpeedLabel();
        }

        private void InitializeGame(int disks)
        {
            game = new HanoiGame(disks);
            solver = new HanoiTower();
            renderer = new TowerRenderer();
            selectedTower = -1;
            isAutoSolving = false;
            solutionMoves = null;
            animationTimer.Stop();

            // общее количество ходов для решения (2^n - 1)
            totalSolutionMoves = (int)Math.Pow(2, disks) - 1;
            currentSolutionStep = 0;

            UpdateControls();
            Invalidate();
        }

        #region Event Handlers

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            renderer.Draw(e.Graphics, game, selectedTower);
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (isAutoSolving) return;

            int clickedTower = renderer.GetTowerIndexFromPoint(e.Location);
            if (clickedTower == -1) return;

            if (selectedTower == -1)
            {
                // Выбор башни
                if (!game.Towers[clickedTower].IsEmpty)
                {
                    selectedTower = clickedTower;
                    Invalidate();
                }
            }
            else
            {
                // Попытка перемещения
                if (game.TryMoveDisk(selectedTower, clickedTower))
                {
                    if (game.IsSolved)
                    {
                        ShowVictoryMessage();
                    }
                }

                selectedTower = -1;
                UpdateControls();
                Invalidate();
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

            if (solutionMoves.Count > 0)
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
            animationTimer.Stop();
            currentSolutionStep = 0;
            UpdateControls();
            Invalidate();
        }

        private void numDisks_ValueChanged(object sender, EventArgs e)
        {
            if (!isAutoSolving)
            {
                InitializeGame((int)numDisks.Value);
            }
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            if (solutionMoves.Count > 0)
            {
                ExecuteNextMove();
            }
            else
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

        #endregion

        #region Private Methods

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
            if (solutionMoves.Count > 0)
            {
                var move = solutionMoves.Dequeue();
                game.TryMoveDisk(move.From, move.To);
                currentSolutionStep++;
                UpdateControls();
                Invalidate();

                if (game.IsSolved)
                {
                    StopAutoSolve();
                    ShowVictoryMessage();
                }
            }
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
            lblStatus.Text = game.IsSolved ? "Решено!" : "В процессе...";
            lblStatus.ForeColor = game.IsSolved ? Color.Green : Color.Black;

            btnNextMove.Enabled = !isAutoSolving && !game.IsSolved;
            btnAutoSolve.Text = isAutoSolving ? "Стоп" : "Авторешение";
            btnAutoSolve.BackColor = isAutoSolving ? Color.LightCoral : SystemColors.Control;
            btnReset.Enabled = !isAutoSolving;
            numDisks.Enabled = !isAutoSolving;

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

        #endregion
    }
}