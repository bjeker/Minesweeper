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
            gui = new MinesweeperGUI();
            logic = new MinesweeperLogic();
            Application.Run(gui);
            gui.PlaceBombs += logic.PlaceBombsHandler;
            Application.Run(gui);
        }
    }
}
