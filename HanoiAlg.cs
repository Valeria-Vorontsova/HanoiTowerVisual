using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanoiTower
{
    public class HanoiTower
    {
        public List<Move> GenerateSolution(int disks, int from, int to, int aux)
        {
            var moves = new List<Move>();
            GenerateRecursive(disks, from, to, aux, moves);
            return moves;
        }

        private void GenerateRecursive(int n, int from, int to, int aux, List<Move> moves)
        {
            if (n == 1)
            {
                moves.Add(new Move(from, to));
                return;
            }

            GenerateRecursive(n - 1, from, aux, to, moves);
            moves.Add(new Move(from, to));
            GenerateRecursive(n - 1, aux, to, from, moves);
        }
    }
}
