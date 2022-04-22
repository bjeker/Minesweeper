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
        Cell[,] bombs;

        public PlaceBombsEventArgs(int row, int col, Cell[,] cells)
        {
            ClickLocation = Tuple.Create(row, col);
            Bombs = cells;
        }

        public Tuple<int, int> ClickLocation { get => clickLocation; set => clickLocation = value; }
        public Cell[,] Bombs { get => bombs; set => bombs = value; }
    }
}
