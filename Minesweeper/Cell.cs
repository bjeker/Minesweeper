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
    public partial class cell : UserControl
    {
        //button
        Button button = new Button();
        //size of cell
        int sizeOfCell = 32;
        //location of the cells
        int row = -1;
        int col = -1;

        public EventHandler CellClick;

        public cell()
        {
            InitializeComponent();
            //set size of cell for x and y
            this.Size = new Size(sizeOfCell, sizeOfCell);
            //removing bebbles, drop shadow???? mo natural padding???
            Button.FlatStyle = FlatStyle.Flat;
            Button.Size = new Size(sizeOfCell, sizeOfCell);
            Button.Location = new Point(0, 0);
            Button.BackColor = Color.LightGray;
            Button.Click += ButtonCLickHandler;
            this.Controls.Add(Button);
        }

        //get size of cell
        public int SizeOfCell { get => sizeOfCell; }
        public Button Button { get => button; }
        public int Row { get => row; set => row = value; }
        public int Col { get => col; set => col = value; }

        //when button is clicked
        public void ButtonCLickHandler(object sender, EventArgs e)
        {
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
    }
}
