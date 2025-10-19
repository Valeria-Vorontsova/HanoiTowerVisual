using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HanoiTower
{
    public class TowerRenderer
    {
        private const int DiskHeight = 20;
        private const int TowerWidth = 10;
        private const int TowerSpacing = 200;
        private const int BaseY = 300;
        private const int TowerHeight = 200;
        private const int PoleHeight = 180;
        private const int PoleWidth = 8;

        // Анимация
        private DiskAnimation currentAnimation;
        private Timer animationTimer;

        public TowerRenderer()
        {
            animationTimer = new Timer();
            animationTimer.Interval = 16; // ~60 FPS
            animationTimer.Tick += AnimationTimer_Tick;
        }

        public void Draw(Graphics g, HanoiGame game, int selectedTower = -1)
        {
            g.Clear(Color.White);
            DrawBases(g, game);
            DrawPoles(g, game);
            DrawTowers(g, game, selectedTower);

            // Рисуем анимируемый диск поверх всего
            if (currentAnimation != null)
            {
                DrawAnimatedDisk(g);
            }
        }

        private void DrawBases(Graphics g, HanoiGame game)
        {
            for (int i = 0; i < game.Towers.Count; i++)
            {
                int x = GetTowerX(i);

                // Основание башни
                g.FillRectangle(Brushes.Brown, x - 40, BaseY, 80, 20);

                // Подпись
                g.DrawString($"Башня {i + 1}", SystemFonts.DefaultFont, Brushes.Black,
                    x - 30, BaseY + 30);
            }
        }

        private void DrawPoles(Graphics g, HanoiGame game)
        {
            for (int i = 0; i < game.Towers.Count; i++)
            {
                int x = GetTowerX(i);
                // Стержень (палка)
                g.FillRectangle(Brushes.Gray, x - PoleWidth / 2, BaseY - PoleHeight, PoleWidth, PoleHeight);
            }
        }

        private void DrawTowers(Graphics g, HanoiGame game, int selectedTower)
        {
            for (int i = 0; i < game.Towers.Count; i++)
            {
                int x = GetTowerX(i);
                DrawTower(g, game.Towers[i], x);

                // Выделение выбранной башни
                if (i == selectedTower)
                {
                    g.DrawRectangle(Pens.Red, x - 60, BaseY - TowerHeight, 120, TowerHeight + 20);
                }
            }
        }

        private void DrawTower(Graphics g, Tower tower, int towerX)
        {
            var disks = tower.GetDisks().ToArray();

            for (int i = 0; i < disks.Length; i++)
            {
                var disk = disks[i];
                // Диски насаживаются на стержень - центр диска совпадает с центром стержня
                int y = BaseY - PoleHeight + (disks.Length - i) * DiskHeight;
                int x = towerX - disk.Width / 2;

                // Рисуем диск с отверстием (прозрачная середина)
                DrawDiskWithHole(g, disk, x, y, towerX);
            }
        }

        private void DrawDiskWithHole(Graphics g, Disk disk, int x, int y, int towerX)
        {
            using (var brush = new SolidBrush(disk.Color))
            {
                // Рисуем основной диск
                g.FillRectangle(brush, x, y, disk.Width, DiskHeight);
                g.DrawRectangle(Pens.Black, x, y, disk.Width, DiskHeight);

                // "Отверстие" - рисуем белый прямоугольник в центре
                int holeWidth = PoleWidth + 4;
                int holeX = towerX - holeWidth / 2;
                g.FillRectangle(Brushes.White, holeX, y, holeWidth, DiskHeight);
                g.DrawRectangle(Pens.Black, holeX, y, holeWidth, DiskHeight);

                // Номер диска
                g.DrawString(disk.Size.ToString(), SystemFonts.DefaultFont,
                    Brushes.Black, x + disk.Width / 2 - 5, y + 3);
            }
        }

        private void DrawAnimatedDisk(Graphics g)
        {
            var disk = currentAnimation.Disk;
            int x = currentAnimation.CurrentX - disk.Width / 2;
            int y = currentAnimation.CurrentY;

            DrawDiskWithHole(g, disk, x, y, currentAnimation.CurrentX);
        }

        public void AnimateMove(Disk disk, int fromTower, int toTower, int moveDuration = 500)
        {
            int startX = GetTowerX(fromTower);
            int endX = GetTowerX(toTower);

            // Высота подъема (над самой высокой башней)
            int liftHeight = BaseY - PoleHeight - 50;

            currentAnimation = new DiskAnimation
            {
                Disk = disk,
                StartX = startX,
                EndX = endX,
                StartY = BaseY - PoleHeight,
                LiftHeight = liftHeight,
                Duration = moveDuration,
                StartTime = DateTime.Now
            };

            animationTimer.Start();
        }

        private double EaseInOutCubic(double t)
        {
            return t < 0.5 ? 4 * t * t * t : 1 - Math.Pow(-2 * t + 2, 3) / 2;
        }

        public int GetTowerX(int towerIndex) => 100 + towerIndex * TowerSpacing;

        public int GetTowerIndexFromPoint(Point point)
        {
            for (int i = 0; i < 3; i++)
            {
                int towerX = GetTowerX(i);
                if (point.X >= towerX - 50 && point.X <= towerX + 50)
                    return i;
            }
            return -1;
        }

        public event Action OnFrameUpdated;

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (currentAnimation == null) return;

            double elapsed = (DateTime.Now - currentAnimation.StartTime).TotalMilliseconds;
            double progress = elapsed / currentAnimation.Duration;

            if (progress >= 1.0)
            {
                // Анимация завершена
                currentAnimation = null;
                animationTimer.Stop();
                OnAnimationCompleted?.Invoke();
                return;
            }

            // Плавная анимация с easing
            progress = EaseInOutCubic(progress);

            // Анимация в три этапа:
            if (progress < 0.33)
            {
                double stageProgress = progress / 0.33;
                currentAnimation.CurrentX = currentAnimation.StartX;
                currentAnimation.CurrentY = (int)(currentAnimation.StartY -
                    currentAnimation.LiftHeight * EaseInOutCubic(stageProgress));
            }
            else if (progress < 0.66)
            {
                double stageProgress = (progress - 0.33) / 0.33;
                currentAnimation.CurrentX = (int)(currentAnimation.StartX +
                    (currentAnimation.EndX - currentAnimation.StartX) * EaseInOutCubic(stageProgress));
                currentAnimation.CurrentY = currentAnimation.StartY - currentAnimation.LiftHeight;
            }
            else
            {
                double stageProgress = (progress - 0.66) / 0.34;
                currentAnimation.CurrentX = currentAnimation.EndX;
                currentAnimation.CurrentY = (int)((currentAnimation.StartY - currentAnimation.LiftHeight) +
                    currentAnimation.LiftHeight * EaseInOutCubic(stageProgress));
            }

            // ДОБАВЬТЕ ЭТУ СТРОЧКУ - уведомляем форму о необходимости перерисовки
            OnFrameUpdated?.Invoke();
        }

        // Событие завершения анимации
        public event Action OnAnimationCompleted;
    }


    public class DiskAnimation
    {
        public Disk Disk { get; set; }
        public int StartX { get; set; }
        public int EndX { get; set; }
        public int StartY { get; set; }
        public int LiftHeight { get; set; }
        public int Duration { get; set; }
        public DateTime StartTime { get; set; }
        public int CurrentX { get; set; }
        public int CurrentY { get; set; }
    }
}