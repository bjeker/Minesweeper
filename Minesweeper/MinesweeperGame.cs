using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    internal class MinesweeperGame
    {
        MinesweeperGUI gui;
        MinesweeperLogic logic;

        public MinesweeperGame()
        {
            //instances of gui and logic classes
            gui = new MinesweeperGUI();
            logic = new MinesweeperLogic();
            gui.PlaceBombs += logic.PlaceBombsHandler;
            Application.Run(gui);
        }
    }
}
