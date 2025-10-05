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

        public bool TryMoveDisk(int from, int to)
        {
            if (from == to || Towers[from].IsEmpty)
                return false;

            var disk = Towers[from].Peek(); 
            if (!Towers[to].CanPlaceDisk(disk))
                return false;

            Towers[from].RemoveDisk();
            Towers[to].PlaceDisk(disk);
            MoveCount++;

            return true;
        }

        public void Reset()
        {
            foreach (var tower in Towers)
            {
                while (!tower.IsEmpty)
                    tower.RemoveDisk();
            }
            InitializeGame();
        }
    }
}