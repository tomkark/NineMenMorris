using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NMM
{
    class IndexHooks
    {
        public MorrisNode hooks { get; set; }
        // near1 is clockwise
        // near2 is counter clockwise
        private MorrisNode CreateLayer(int layer) // This function will build the indexes from the most inner layer starting at 0.
        {
            MorrisNode helper;
            MorrisNode tree = new MorrisNode(Const.location.MidTop, layer);
            tree.near1 = new MorrisNode(Const.location.TopRight, layer);
            tree.near1.near2 = tree;
            helper = tree.near1;
            int size = 2;
            for(int i = 2; i < 8; i++)
            {
                helper.near1 = (new MorrisNode(helper.place + 1, layer));
                size++;
                helper.near1.near2 = helper;
                helper = helper.near1; 
            }
            helper.near1 = tree;
            helper.near1.near2 = helper;
            return tree;
            }
        private MorrisNode LinkLayer(MorrisNode tree, MorrisNode newLayer)
        {
            //Cycle through all the mids and link between layers
            tree.outer = newLayer;;
            newLayer.inner = tree;
            tree = tree.near1.near1;
            newLayer = newLayer.near1.near1;
            tree.outer = newLayer;;
            newLayer.inner = tree;
            tree = tree.near1.near1;
            newLayer = newLayer.near1.near1;
            tree.outer = newLayer;;
            newLayer.inner = tree;
            tree = tree.near1.near1;
            newLayer = newLayer.near1.near1;
            tree.outer = newLayer;;
            newLayer.inner = tree;
            newLayer = newLayer.near1.near1;
            return newLayer;
        } //This function links 2 given layers
        private void CreateTree(int layers)
        {
            this.hooks = CreateLayer(0);
            MorrisNode helper = this.hooks;
            for(int i = 1; i < layers; i++)
            {
                helper = LinkLayer(helper, CreateLayer(i));
            }
        } //Creates the entire graph(board) of the game.
        public IndexHooks(int layer = Const.layers)
        {
            /* what we know about each endpoint:
             * Left Top has 2 under and 2 in the right
             * Left Bottom has 2 above and 2 in th right
             * Right Bottom has 2 above and 2 left
             * Right Top has 2 under and 2 left
            */
            CreateTree(layer);
        } //constructor
    }
}
