using System.Drawing;

namespace HanoiTower
{
    public class Disk
    {
        public int Size { get; }
        public int Width { get; }
        public Color Color { get; }

        public Disk(int size, int totalDisks)
        {
            Size = size;
            Width = CalculateWidth(size, totalDisks);
            Color = CalculateColor(size);
        }

        private int CalculateWidth(int size, int totalDisks)
        {
            const int minWidth = 40;
            const int baseWidth = 120;
            return minWidth + (size - 1) * (baseWidth - minWidth) / (totalDisks - 1);
        }

        private Color CalculateColor(int size)
        {
            return Color.FromArgb(
                255,
                (size * 50) % 255,
                (size * 80) % 255,
                (size * 120) % 255
            );
        }
    }
}
