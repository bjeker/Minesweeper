namespace Minesweeper
{
    public partial class MinesweeperGUI : Form
    {
        public delegate void PlaceBombsEventHandler(object sender, PlaceBombsEventArgs e);
        public event PlaceBombsEventHandler PlaceBombs;

        bool firstClick = true;
        Cell[,] cells = new Cell[10, 10];
        //count for timer tick
        int count = 0;
        //determines win condition
        int clicks = 0;


        public MinesweeperGUI()
        {
            InitializeComponent();
            CreateGrid();
            timer1.Enabled = true;
            timer1.Start();
        }

        //creates grid of cells
        private void CreateGrid()
        {
            //goes through cell array
            for (int row = 0; row < cells.GetLength(0); row++)
            {
                for (int col = 0; col < cells.GetLength(1); col++)
                {
                    //new cell creation
                    cells[row, col] = new Cell();
                    //32x32 grid of cells
                    cells[row, col].Location = new Point(col * cells[row, col].SizeOfCell, (row + 1) * cells[row, col].SizeOfCell);
                    cells[row, col].Row = row;
                    cells[row, col].Col = col;
                    cells[row, col].CellClick += CellClickHandler;
                    this.Controls.Add(cells[row, col]);
                }
            }
        }
        //when cells are clicked
        public void CellClickHandler(object sender, EventArgs e)
        {
            //clicked cell is the current cell
            Cell currentCell = (Cell)sender;
            //clicked cells background color is the target color
            Color targetColor = ((Cell)sender).BackColor;
            if (firstClick)
            {
                //NEW CODE ADDED CURRENT CELL TO END
                PlaceBombsNow(currentCell.Row, currentCell.Col);
                firstClick = false;
            }

            //cell checks
            CheckAboveCorners(currentCell, targetColor);
            CheckBelowCorners(currentCell, targetColor);
            CheckAround(currentCell, targetColor);

            //used to determine win
            clicks++;

            //win condition
            if (clicks == 81)
            {
                if (currentCell.BackColor != Color.Red)
                {
                    Properties.Settings.Default.wins++;
                    MessageBox.Show("YOU WON!");
                }
            }

            //lost condition
            if (currentCell.BackColor == Color.Red)
            {
                MessageBox.Show("BOOM! YOU LOST!");
                Properties.Settings.Default.time = count;
                Properties.Settings.Default.losses++;
                Properties.Settings.Default.Save();
                timer1.Enabled = false;
            }
        }

        //checks around cell
        private void CheckAround(Cell currentCell, Color targetColor)
        {
            //check above cell
            if (currentCell.Row > 0)
            {
                if (cells[currentCell.Row - 1, currentCell.Col].IsBomb)
                {
                    cells[currentCell.Row, currentCell.Col].NearbyBombs++;
                }

            }
            //check left of cell
            if (currentCell.Col > 0)
            {
                if (cells[currentCell.Row, currentCell.Col - 1].IsBomb)
                {
                    cells[currentCell.Row, currentCell.Col].NearbyBombs++;
                }
            }
            //check right of cell
            if (currentCell.Col < cells.GetLength(1) - 1)
            {
                if (cells[currentCell.Row, currentCell.Col + 1].IsBomb)
                {
                    cells[currentCell.Row, currentCell.Col].NearbyBombs++;
                }
            }

            //check below cell
            if (currentCell.Row < cells.GetLength(0) - 1)
            {
                if (cells[currentCell.Row + 1, currentCell.Col].IsBomb)
                {
                    cells[currentCell.Row, currentCell.Col].NearbyBombs++;
                }
            }

            //no nearby bombs
            if (currentCell.NearbyBombs == 0)
            {
                if (currentCell.Row > 0)
                {
                    if (targetColor == cells[currentCell.Row - 1, currentCell.Col].BackColor)
                    {
                        cells[currentCell.Row - 1, currentCell.Col].PerformClick();
                    }
                }

                if (currentCell.Col > 0)
                {
                    if (targetColor == cells[currentCell.Row, currentCell.Col - 1].BackColor)
                    {
                        cells[currentCell.Row, currentCell.Col - 1].PerformClick();
                    }
                }

                if (currentCell.Col < cells.GetLength(1) - 1)
                {
                    if (targetColor == cells[currentCell.Row, currentCell.Col + 1].BackColor)
                    {
                        cells[currentCell.Row, currentCell.Col + 1].PerformClick();
                    }
                }

                if (currentCell.Row < cells.GetLength(0) - 1)
                {
                    if (targetColor == cells[currentCell.Row + 1, currentCell.Col].BackColor)
                    {
                        cells[currentCell.Row + 1, currentCell.Col].PerformClick();
                    }
                }
            }

            if (!currentCell.IsBomb)
            {
                ShowNearbyBombs(currentCell);
            }
        }

        //check top right and left corners
        private void CheckAboveCorners(Cell currentCell, Color targetColor)
        {
            if (currentCell.Row > 0)
            {
                if (currentCell.Col > 0)
                {
                    if (cells[currentCell.Row - 1, currentCell.Col - 1].IsBomb)
                    {

                        cells[currentCell.Row, currentCell.Col].NearbyBombs++;
                    }
                }
                if (currentCell.Col < cells.GetLength(1) - 1)
                {
                    if (cells[currentCell.Row - 1, currentCell.Col + 1].IsBomb)
                    {

                        cells[currentCell.Row, currentCell.Col].NearbyBombs++;
                    }
                }
            }
        }

        //check bottom right and left corners
        private void CheckBelowCorners(Cell currentCell, Color targetColor)
        {
            if (currentCell.Row < cells.GetLength(0) - 1)
            {
                if (currentCell.Col > 0)
                {
                    if (cells[currentCell.Row + 1, currentCell.Col - 1].IsBomb)
                    {
                        cells[currentCell.Row, currentCell.Col].NearbyBombs++;
                    }
                }
                if (currentCell.Col < cells.GetLength(1) - 1)
                {
                    if (cells[currentCell.Row + 1, currentCell.Col + 1].IsBomb)
                    {
                        cells[currentCell.Row, currentCell.Col].NearbyBombs++;
                    }
                }
            }
        }

        //shows nearby bombs
        public void ShowNearbyBombs(Cell currentCell)
        {
            int bombs = cells[currentCell.Row, currentCell.Col].NearbyBombs;
            if (bombs > 0)
            {

                cells[currentCell.Row, currentCell.Col].Label.Text = $"{bombs}";

                switch (bombs)
                {
                    case 0:
                        break;
                    case 1:
                        cells[currentCell.Row, currentCell.Col].Label.ForeColor = Color.Blue;
                        break;
                    case 2:
                        cells[currentCell.Row, currentCell.Col].Label.ForeColor = Color.Green;
                        break;
                    case 3:
                        cells[currentCell.Row, currentCell.Col].Label.ForeColor = Color.Red;
                        break;
                    case 4:
                        cells[currentCell.Row, currentCell.Col].Label.ForeColor = Color.Purple;
                        break;
                    case 5:
                        cells[currentCell.Row, currentCell.Col].Label.ForeColor = Color.Orange;
                        break;
                }

                cells[currentCell.Row, currentCell.Col].Refresh();
            }
        }

        //prepares to place bombs
        public void PlaceBombsNow(int row, int col)
        {
            PlaceBombsEventArgs e = new PlaceBombsEventArgs(row, col, cells);
            PlaceTheBombs(this, e);
        }

        //places bombs
        protected virtual void PlaceTheBombs(object sender, PlaceBombsEventArgs e)
        {
            //raises flag for PlaceBombs event
            if (PlaceBombs != null)
            {
                PlaceBombs(this, e);
            }
        }

        //exit game when clicked
        private void exitGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //restart game when clicked
        private void restartGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        //controls timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!firstClick)
            {
                count++;
                label1.Text = count.ToString();
            }
        }

        //average time to complete
        private void averageTimeToCompleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Average Time: " + (Properties.Settings.Default.time / (Properties.Settings.Default.wins + Properties.Settings.Default.losses)) + " seconds");
        }

        //help about the program
        private void helpInstructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Welcome to minesweeper!\nClick a square to begin, the numbers show how close to a bomb the click is.\nDon't blow up!");
        }

        //info about program
        private void aboutInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coded by Ryan Bieker\nCoded by 4/22/22\nCoded for CS 3020 Class");
        }

        //win los ratio
        private void gameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Wins: " + Properties.Settings.Default.wins + "\n Losses: " + Properties.Settings.Default.losses);

        }

        //reset all stats
        private void resetStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.time = 0;
            Properties.Settings.Default.losses = 0;
            Properties.Settings.Default.wins = 0;
            Properties.Settings.Default.Save();
        }
    }
}