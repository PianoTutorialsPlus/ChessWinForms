using ChessWindowsForms;
using ChessWindowsForms.View.Contracts;
using ChessWindowsForms.View.Helper;
using ChessWindowsForms.View.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessWindowsForms.View.UI
{
    public class ChessBoardModel
    {
        private const int BOARDSIZE = 8;
        private TableLayoutPanel _chessBoard;
        private Size tileSize;
        private List<Marker> _markers;

        public TableLayoutPanel ChessBoard => _chessBoard;

        public ChessBoardModel(
            TableLayoutPanel chessBoard)
        {
            _chessBoard = chessBoard;
        }

        public void DrawBoard()
        {
            int tileWidth = 45;
            int tileHeight = 45;

            tileSize.Width = tileWidth;
            tileSize.Height = tileHeight;

            Queue<Color> queueColor = new Queue<Color>();
            queueColor.Enqueue(Color.White);
            queueColor.Enqueue(Color.Black);

            _chessBoard.Size = new Size(BOARDSIZE * tileWidth, BOARDSIZE * tileHeight);

            for (int i = 0; i < _chessBoard.ColumnCount; i++)
            {
                CreateBoardRow(i, ref queueColor);
                queueColor.Enqueue(queueColor.Dequeue());
            }
        }
        private void CreateBoardRow(int count, ref Queue<Color> queueColor)
        {
            for (int i = 0; i < _chessBoard.RowCount; i++)
            {
                Panel box = new Panel
                {
                    Size = tileSize,
                    Margin = new Padding(0),
                    AllowDrop = true,
                };

                box.BackColor = queueColor.Peek();
                queueColor.Enqueue(queueColor.Dequeue());

                _chessBoard.Controls.Add(box, count, i);
            };
        }
        public void AttachChessPieceToBoard(int column, int row, IChessPieceView chessPiece)
        {
            Panel panel = _chessBoard.GetControlFromPosition(column, row) as Panel;
            chessPiece.AttachTo(panel);
        }
        internal void AttachMarkersToBoard(List<Marker> markers)
        {
            _markers = markers;
            foreach (Marker marker in markers)
            {
                Panel panel = _chessBoard.GetControlFromPosition(
                    marker.Column, marker.Row) as Panel;
                marker.Parent = panel;
            }
        }
        internal void DeleteAllMarkers()
        {
            foreach (Marker marker in _markers)
            {
                Panel panel = _chessBoard.GetControlFromPosition(
                    marker.Column, marker.Row) as Panel;

                panel.Controls.RemoveAt(panel.Controls.Count - 1);
            }
        }
    }
}
