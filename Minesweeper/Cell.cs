using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Cell : UserControl
    {
        //button
        Button cellButton = new Button();
        Random rand = new Random();
        //cell content
        char cellContent = ' ';
        //size of cell
        int sizeOfCell = 32;
        //location of the cells
        int row = -1;
        int col = -1;

        public EventHandler CellClick;

        //each cell
        public Cell()
        {
            InitializeComponent();
            //set size of cell for x and y
            this.Size = new Size(sizeOfCell, sizeOfCell);
            //removing bebbles, drop shadow???? no natural padding???
            cellButton.FlatStyle = FlatStyle.Flat;
            cellButton.Size = new Size(sizeOfCell, sizeOfCell);
            cellButton.Location = new Point(0, 0);
            cellButton.BackColor = Color.LightGray;
            cellButton.Click += ButtonCLickHandler;
            this.Controls.Add(cellButton);
            this.BackColor = Color.WhiteSmoke;
            //this.BackColor = (rand.Next(2) % 2 == 0) ? Color.Red : Color.White;
        }

        //size, button, row, and col getters and setters
        public int SizeOfCell { get => sizeOfCell; }
        public Button button { get => button; }
        public int Row { get => row; set => row = value; }
        public int Col { get => col; set => col = value; }

        //when button is clicked
        public void ButtonCLickHandler(object sender, EventArgs e)
        {
            ((Button)sender).Visible = false;
            OnCellClick(this, e);
        }

        //when cell is clicked
        protected virtual void OnCellClick(object sender, EventArgs e)
        {
            if (CellClick != null)
            {
                CellClick(sender, e);
            }
        }

        //handles the click of mouse
        public void PerformClick()
        {
            cellButton.PerformClick();
        }
    }
}
