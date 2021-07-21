using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace NMM
{
    public class MorrisButton : Button
    {
        public MorrisNode ancestor;
        public bool graphicChange = false;
    }
}
