using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NMM.Const;
namespace NMM
{
    class Move
    {
        public MorrisNode src {get; set;}//src is the place where the piece is moved FROM.
        public MorrisNode dest{ get; set; }//dest is the place where the piece is moved TO.
        public MorrisNode ate { get; set; } //ate is the place where the eaten piece IS.
        public bool ateWasMill { get; set; } //True if the "ate" piece was in a mill
        public int phase { get; set; } //The phase number of the move.
        public Move(MorrisNode src, MorrisNode dest, MorrisNode ate,int phase)
        {
            this.src = src;
            this.dest = dest;
            this.ate = ate;
            this.phase = phase;
            this.ateWasMill = false;
        }
        public void SetAte(MorrisNode ate, bool wasMill)
        {
            this.ate = ate;
            this.ateWasMill = wasMill;
        }
    }
}
