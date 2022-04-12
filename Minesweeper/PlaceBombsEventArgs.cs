using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class PlaceBombsEventArgs : EventArgs
    {
        Tuple<int, int> clickLocation;

        public PlaceBombsEventArgs(int row, int col)
        {
            ClickLocation = Tuple.Create(row, col);
        }

        public Tuple<int, int> ClickLocation { get => clickLocation; set => clickLocation = value; }
    }
}
