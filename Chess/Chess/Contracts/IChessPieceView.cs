using ChessWindowsForms;
using ChessWindowsForms.View.Contracts;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ChessWindowsForms.View.Contracts
{
    public interface IChessPieceView
    {
        Color Color { get; }
        PictureBox PictureBox { get; }
        Image Image { get; }

        void AttachTo(Panel panel);
        void Drop(DragDropEffects move);
        void Hide();
        void Show();
    }
}