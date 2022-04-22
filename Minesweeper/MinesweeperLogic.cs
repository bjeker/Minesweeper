using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    internal class MinesweeperLogic
    {
        Random rand = new Random();
        public void PlaceBombsHandler(object sender, PlaceBombsEventArgs e)
        {
            //gets information of clicked locations x
            int clickedRow = e.ClickLocation.Item1;
            //gets information of clicked locations y
            int clickedCol = e.ClickLocation.Item2;

            List<Tuple<int, int>> response = new List<Tuple<int, int>>();

            for (int i = 0; i < 20; i++)
            {
                bool validChoice = false;
                do
                {
                    //sets up new bomb location
                    Tuple<int, int> bombLocation = Tuple.Create(rand.Next(10), rand.Next(10));
                    
                    //NEW CODE 
                    e.Bombs[bombLocation.Item1, bombLocation.Item2].BackColor = Color.Red;
                    e.Bombs[bombLocation.Item1, bombLocation.Item2].IsBomb = true;

                    //checks if valid locaiton
                    validChoice = CheckIfValidLocation(clickedRow, clickedCol, response, validChoice, bombLocation);
                } while (!validChoice);
            }
        }

        private bool CheckIfValidLocation(int clickedRow, int clickedCol, List<Tuple<int, int>> response, bool validChoice, Tuple<int, int> bombLocation)
        {
            //checks if bombLocations row and col are not the same
            if (bombLocation.Item1 != clickedRow && bombLocation.Item2 != clickedCol)
            {
                validChoice = true;
                foreach (var location in response)
                {
                    if (bombLocation.Item1 != location.Item1 && bombLocation.Item2 != location.Item2)
                    {
                        validChoice = false;
                    }
                }
            }
            return validChoice;
        }
    }
}
