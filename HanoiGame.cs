using System;
using System.Collections.Generic;
using System.Linq;

namespace HanoiTower
{
    public class HanoiGame
    {
        public List<Tower> Towers { get; }
        public int TotalDisks { get; }
        public int MoveCount { get; private set; }
        public bool IsSolved => Towers[2].DiskCount == TotalDisks;

        public HanoiGame(int diskCount)
        {
            TotalDisks = diskCount;
            Towers = new List<Tower>();

            for (int i = 0; i < 3; i++)
            {
                Towers.Add(new Tower(i));
            }

            InitializeGame();
        }

        private void InitializeGame()
        {
            for (int i = TotalDisks; i >= 1; i--)
            {
                var disk = new Disk(i, TotalDisks);
                Towers[0].PlaceDisk(disk);
            }

            MoveCount = 0;
        }

        // Этот метод теперь только проверяет возможность хода
        public bool CanMove(int from, int to)
        {
            if (from < 0 || from >= 3 || to < 0 || to >= 3)
                return false;
            if (from == to || Towers[from].IsEmpty)
                return false;

            var disk = Towers[from].Peek();
            return Towers[to].CanPlaceDisk(disk);
        }

        // Этот метод выполняет перемещение (вызывается после анимации)
        public void Move(int from, int to)
        {
            if (CanMove(from, to))
            {
                var disk = Towers[from].RemoveDisk();
                Towers[to].PlaceDisk(disk);
                MoveCount++;
            }
        }

        // Старый метод TryMoveDisk можно оставить для обратной совместимости
        public bool TryMoveDisk(int from, int to)
        {
            if (!CanMove(from, to))
                return false;

            Move(from, to);
            return true;
        }

        public void Reset()
        {
            foreach (var tower in Towers)
            {
                tower.Clear();
            }
            InitializeGame();
        }
    }
}