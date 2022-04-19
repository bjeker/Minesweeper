using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class RandomCellEventArgs : EventArgs
    {
        Cell[,] randomCells;

        //Passes cell array
        public RandomCellEventArgs(Cell[,] cell)
        {
            RandomCells = cell;
        }

        public Cell[,] RandomCells { get => randomCells; set => randomCells = value; }
    }
}
