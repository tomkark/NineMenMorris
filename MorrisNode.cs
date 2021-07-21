using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NMM.Const;
namespace NMM
{
    public class MorrisNode
    {
        public bool selected { get; set; }
        public bool isInMill { get; set; }
        public location place { get; set; }
        public MorrisNode near1 { get; set; } // The next piece clockwise.
        public MorrisNode near2 { get; set; } // The next piece counter-clockwise.
        public MorrisNode inner { get; set; } //Inner piece (if it's a mid, and it's not the most inner layer, then there is an inner node).
        public MorrisNode outer { get; set; } //Outer piece (if it's a mid, and it's not the most outer layer, then there is an outer node).
        public int layerNum { get; set; } //The layer number, the most inner is 0, and the most outer is N-1 if there are N layers.
        public MorrisButton btn { get; set; } //Reference to the btn related to this node.
        public int occupied{ get; set; } // 0 - unoccupied, 1 - red, 2 - blue
        public MorrisNode(location place, int layerNum)
        {
            this.place = place;
            this.near2 = null;
            this.near1 = null;
            this.inner = null;
            this.outer = null;
            this.layerNum = layerNum;
            this.occupied = 0;
            this.btn = null;
            this.isInMill = false;
            this.selected = false;
        }
        public bool HasInner()
        {
            return (inner != null);
        }
        public bool HasOuter()
        {
            return (outer != null);
        }
    }




}
