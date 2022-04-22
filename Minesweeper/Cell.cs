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
        //size of cell
        int sizeOfCell = 32;
        //location of the cells
        int row = -1;
        int col = -1;

        //NEW CODE
        Label cellLabel = new Label();
        int nearbyBombs = 0;
        bool isBomb = false;


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
            //each cell has a button to be clicked
            this.Controls.Add(cellButton);
            this.BackColor = Color.White;

            //NEW CODE
            cellLabel.Size = new Size(sizeOfCell, sizeOfCell);
            cellLabel.Font = new Font("Calibri", 12);
            cellLabel.Text = "";
            cellLabel.ForeColor = Color.Red;
            cellLabel.Visible = true;
            cellLabel.Location = new Point(6, 6);
            this.Controls.Add(cellLabel);
        }

        //size, button, row, and col getters and setters
        public int SizeOfCell { get => sizeOfCell; }
        public Button button { get => button; }
        public int Row { get => row; set => row = value; }
        public int Col { get => col; set => col = value; }

        //NEW CODE
        public Label Label { get => cellLabel; }
        public int NearbyBombs { get => nearbyBombs; set => nearbyBombs = value; }
        public bool IsBomb { get => isBomb; set => isBomb = value; }
        public virtual System.Drawing.Color ForeColor { get; set; }


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
