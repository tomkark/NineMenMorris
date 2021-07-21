using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMM
{
    public class Const
    {
        public enum location
        {
            TopLeft = 8,
            TopRight = 2,
            BottomLeft = 6,
            BottomRight = 4,
            MidLeft = 7,
            MidRight = 3,
            MidTop = 1,
            MidBottom = 5
        }
        public static Random rnd = new Random();
        public const int SIZEY = 850; // board x limit
        public const int SIZEX = 850; // board y limit 
        public const double GAMMA = 0.95;
        public const int WinVal = 10000; // Winning heuristic
        public const int MaxDepth = 8; // max depth to search in minimax/alphabeta
        public const int layers = 3;
        public const int tileSize = 40;
        public const int piecesToPut = layers == 2 ? 3 : layers * 3;
        //public const int piecesToPut = 9;
        public const int TIMELIMIT_PLAY = 2048;
        public const int layerWidthX = SIZEX / ((layers + 1) * 2);
        public const int layerWidthY = SIZEY / ((layers + 1) * 2);
    }
}
