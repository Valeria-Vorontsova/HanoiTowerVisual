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

        public void Draw(Graphics g, HanoiGame game, int selectedTower = -1)
        {
            g.Clear(Color.White);
            DrawBases(g, game);
            DrawTowers(g, game, selectedTower);
        }

        private void DrawBases(Graphics g, HanoiGame game)
        {
            for (int i = 0; i < game.Towers.Count; i++)
            {
                int x = GetTowerX(i);

                // Основание башни
                g.FillRectangle(Brushes.Brown, x - TowerWidth / 2, BaseY, TowerWidth, TowerHeight);

                // Подпись
                g.DrawString($"Башня {i + 1}", SystemFonts.DefaultFont, Brushes.Black,
                    x - 30, BaseY + TowerHeight + 10);
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
            var disks = tower.GetDisks().ToArray(); // Теперь получаем диски в правильном порядке

            for (int i = 0; i < disks.Length; i++)
            {
                var disk = disks[i];
                int y = BaseY - (i + 1) * DiskHeight; // Теперь i=0 - нижний диск, i=max - верхний
                int x = towerX - disk.Width / 2;

                // Рисуем диск
                using (var brush = new SolidBrush(disk.Color))
                {
                    g.FillRectangle(brush, x, y, disk.Width, DiskHeight);
                }
                g.DrawRectangle(Pens.Black, x, y, disk.Width, DiskHeight);

                // Номер диска
                g.DrawString(disk.Size.ToString(), SystemFonts.DefaultFont,
                    Brushes.White, x + disk.Width / 2 - 5, y + 3);
            }
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
    }
}