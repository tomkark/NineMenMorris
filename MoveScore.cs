using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMM
{
    struct MoveScore : IComparable<MoveScore>
    {
        public Move move { get; set; }
        public double score { get; set; }

        public MoveScore(Move move, double score)
        {
            this.move = move;
            this.score = score;
        }

        public int CompareTo(MoveScore other)
        {
            return this.score.CompareTo(other.score);
        }
    }
}
