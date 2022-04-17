namespace Minesweeper
{
    public partial class MinesweeperGUI : Form
    {
        public delegate void PlaceBombsEventHandler(object sender, PlaceBombsEventArgs e);
        public event PlaceBombsEventHandler PlaceBombs;
        bool firstClick = true;
        Cell[,] cells = new Cell[10, 10];

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
            Cell currentCell = (Cell)sender;
            Color targetColor = ((Cell)sender).BackColor;
            if (firstClick)
            {
                PlaceBombsNow(currentCell.Row, currentCell.Col);
                firstClick = false;
            }    
            CheckAbove(currentCell, targetColor);
            CheckBelow(currentCell, targetColor);

            //column is positive
            if (currentCell.Col > 0)
            {
                if (targetColor == cells[currentCell.Row, currentCell.Col - 1].BackColor)
                {
                    //slow down program by 1 second each click
                    Thread.Sleep(100);
                    cells[currentCell.Row, currentCell.Col - 1].PerformClick();
                }
            }
            if (currentCell.Col < cells.GetLength(1) - 1)
            {
                if (targetColor == cells[currentCell.Row, currentCell.Col + 1].BackColor)
                {
                    //slow down program by 1 second each click
                    Thread.Sleep(100);
                    cells[currentCell.Row, currentCell.Col + 1].PerformClick();
                }
            }
        }

        //check above
        private void CheckAbove(Cell currentCell, Color targetColor)
        {
            if (currentCell.Row > 0)
            {
                if (targetColor == cells[currentCell.Row - 1, currentCell.Col].BackColor)
                {
                    //slow down program by 1 second each click
                    Thread.Sleep(100);
                    cells[currentCell.Row - 1, currentCell.Col].PerformClick();
                }
                if (currentCell.Col > 0)
                {
                    if (targetColor == cells[currentCell.Row - 1, currentCell.Col - 1].BackColor)
                    {
                        //slow down program by 1 second each click
                        Thread.Sleep(100);
                        cells[currentCell.Row - 1, currentCell.Col - 1].PerformClick();
                    }
                }
                if (currentCell.Col < cells.GetLength(1) - 1)
                {
                    if (targetColor == cells[currentCell.Row - 1, currentCell.Col + 1].BackColor)
                    {
                        //slow down program by 1 second each click
                        Thread.Sleep(100);
                        cells[currentCell.Row - 1, currentCell.Col + 1].PerformClick();
                    }
                }
            }
        }

        //check below
        private void CheckBelow(Cell currentCell, Color targetColor)
        {
            if (currentCell.Row < cells.GetLength(0) - 1)
            {
                if (targetColor == cells[currentCell.Row + 1, currentCell.Col].BackColor)
                {
                    //slow down program by 1 second each click
                    Thread.Sleep(100);
                    cells[currentCell.Row + 1, currentCell.Col].PerformClick();
                }
                if (currentCell.Col > 0)
                {
                    if (targetColor == cells[currentCell.Row + 1, currentCell.Col - 1].BackColor)
                    {
                        //slow down program by 1 second each click
                        Thread.Sleep(100);
                        cells[currentCell.Row + 1, currentCell.Col - 1].PerformClick();
                    }
                }
                if (currentCell.Col < cells.GetLength(1) - 1)
                {
                    if (targetColor == cells[currentCell.Row + 1, currentCell.Col + 1].BackColor)
                    {
                        //slow down program by 1 second each click
                        Thread.Sleep(100);
                        cells[currentCell.Row + 1, currentCell.Col + 1].PerformClick();
                    }
                }
            }
        }

        public void PlaceBombsNow(int row, int col)
        {
            PlaceBombsEventArgs e = new PlaceBombsEventArgs(row, col);
            PlaceTheBombs(this, e);
        }

        //places bombs
        protected virtual void PlaceTheBombs(object sender, PlaceBombsEventArgs e)
        {
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

        //count for timer tick
        private int count = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            count++;
            label1.Text = count.ToString();
        }
    }
}