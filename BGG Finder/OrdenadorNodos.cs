using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BGG_Finder
{
    class OrdenadorNodos : IComparer
    {
        public int Compare(object x, object y)
        {
            TreeNode nx = (TreeNode)x;
            if (nx.Tag.Equals("numeroJugadores"))
            {
                return 0;
            }
            TreeNode ny = (TreeNode)y;
            return string.Compare(nx.Text, ny.Text);
        }

    }
}
