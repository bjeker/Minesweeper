namespace Minesweeper
{
    public partial class MinesweeperGUI : Form
    {
        Cell[,] cells = new Cell[10, 10];
        public MinesweeperGUI()
        {
            InitializeComponent();
            CreateGrid();
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
                    cells[row, col].Location = new Point(col * cells[row, col].SizeOfCell, row * cells[row, col].SizeOfCell);
                    this.Controls.Add(cells[row, col]);
                }
            }
        }
    }
}