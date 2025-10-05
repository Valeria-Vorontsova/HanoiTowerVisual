namespace HanoiTower
{
    public class Move
    {
        public int From { get; }
        public int To { get; }

        public Move(int from, int to)
        {
            From = from;
            To = to;
        }

        public override string ToString()
        {
            return $"с {From + 1} на {To + 1}";
        }
    }
}