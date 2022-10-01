using ChessWindowsForms;
using ChessWindowsForms.View.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessWindowsForms.View.Helper
{
    public class Marker : PictureBox
    {
        public int Row { get; }
        public int Column { get; }

        public Marker(int column, int row)
        {
            Row = row;
            Column = column;

            Margin = new Padding(0);
            Image = Properties.Resources.Marker;
            SizeMode = PictureBoxSizeMode.Zoom;
            Dock = DockStyle.Fill;
        }
    }
}
