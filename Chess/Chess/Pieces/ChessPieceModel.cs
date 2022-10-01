using ChessWindowsForms;
using ChessWindowsForms.View.Helper;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChessWindowsForms.View.Helper
{
    public class ChessPieceModel : PictureBox
    {
        public Color Color { get; }

        public ChessPieceModel(
            Image image,
            Color colorPieces)
        {
            Color = colorPieces;
            Margin = new Padding(0);
            Image = image;
            SizeMode = PictureBoxSizeMode.Zoom;
            Dock = DockStyle.Fill;
        }

    }
}