using System.Collections.Generic;
using System.Linq;

namespace HanoiTower
{
    public class Tower
    {
        private Stack<Disk> disks;
        public int Index { get; }

        public Tower(int index)
        {
            Index = index;
            disks = new Stack<Disk>();
        }

        public bool CanPlaceDisk(Disk disk)
        {
            return disks.Count == 0 || disk.Size < disks.Peek().Size;
        }

        public void PlaceDisk(Disk disk)
        {
            disks.Push(disk);
        }

        public Disk RemoveDisk()
        {
            return disks.Pop();
        }

        public bool IsEmpty => disks.Count == 0;

        public IEnumerable<Disk> GetDisks()
        {
            // Преобразуем Stack в List и переворачиваем порядок
            return disks.Reverse().ToList();
        }

        public Disk Peek()
        {
            return disks.Peek();
        }

        public int DiskCount => disks.Count;

        public void Clear()
        {
            disks.Clear();
        }
    }
}
