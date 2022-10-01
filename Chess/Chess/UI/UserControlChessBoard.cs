using ChessWindowsForms.View.Contracts;
using ChessWindowsForms.View.Helper;
using ChessWindowsForms.View.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessWindowsForms.View.UI
{
    public partial class UserControlChessBoard : UserControl
    {
        private ChessBoardModel _chessBoardModel;
        private IGameplay _gameplay;
        private Queue<IPlayer> _players => _gameplay.Players;

        private Bitmap _bmpLogo;
        private Point _dragStartPoint;

        public event DragEventHandler Dragged;
        public event MouseEventHandler Dropped;

        public UserControlChessBoard(
            IGameplay gameplay,
            ChessBoardModel chessBoardModel)
        {
            _chessBoardModel = chessBoardModel;
            _gameplay = gameplay;
            chessBoard = chessBoardModel.ChessBoard;

            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            _chessBoardModel.DrawBoard();
            AttachDragEvents();
            AttachEvents(_players.Peek());
            ConnectEvents();
        }
        public void AttachDragEvents()
        {
            foreach (Panel control in chessBoard.Controls)
            {
                control.DragEnter += ChessBoard_DragEnter;
                control.DragDrop += ChessBoard_DragDrop;
            }
        }
        public void AttachEvents(IPlayer player)
        {
            foreach (IChessPieceView chessPiece in player.ChessPieceList)
            {
                chessPiece.PictureBox.MouseDown += ChessBoard_MouseDown;
                chessPiece.PictureBox.GiveFeedback += ChessBoard_GiveFeedback;
            }
        }
        private void ConnectEvents()
        {
            _gameplay.OnDetachedEvents += DetachEvents;
            _gameplay.OnAttachedEvents += AttachEvents;
        }

        public void AttachMarkersToBoard(List<Marker> markers)
        {
            _chessBoardModel.AttachMarkersToBoard(markers);
        }
        public void DeleteAllMarkers()
        {
            _chessBoardModel.DeleteAllMarkers();
        }
        public TableLayoutPanelCellPosition GetCellPosition(Panel panel)
        {
            return chessBoard.GetCellPosition(panel);
        }
        public void AttachChessPieceToBoard(int column, int row, IChessPieceView chessPiece)
        {
            _chessBoardModel.AttachChessPieceToBoard(column, row, chessPiece);
        }
        public void DetachEvents(IPlayer player)
        {
            foreach (IChessPieceView chessPiece in player.ChessPieceList)
            {
                chessPiece.PictureBox.MouseDown -= ChessBoard_MouseDown;
                chessPiece.PictureBox.GiveFeedback -= ChessBoard_GiveFeedback;
            }
        }

        private void ChessBoard_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void ChessBoard_DragDrop(object sender, DragEventArgs e)
        {
            Dragged?.Invoke(sender, e);
        }
        private void ChessBoard_MouseDown(object sender, MouseEventArgs e)
        {
            
            PictureBox pictureBox = sender as PictureBox;

            var list = _players.Peek().ChessPieceList;

            IChessPieceView chessPiece = null;

            foreach (IChessPieceView piece in list)
            {
                if (piece.PictureBox.Equals(pictureBox))
                {
                    chessPiece = piece;
                }
            }

            if (e.Button == MouseButtons.Left)
            {
                chessPiece.Hide();
                _dragStartPoint = e.Location;
                _bmpLogo = new Bitmap(chessPiece.Image, new Size(45, 45));
                Dropped?.Invoke(sender, e);

                chessPiece.Drop(DragDropEffects.Move);

                //Debug.WriteLine("Position: " + chessBoard.GetCellPosition(chessPiece.PictureBox.Parent));
            }
        }
        private void ChessBoard_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.UseDefaultCursors = false;
            if (e.Effect == DragDropEffects.Move)
            {
                Cursor.Current = CursorUtility.CreateCursor(
                    _bmpLogo, _dragStartPoint.X, _dragStartPoint.Y);
            }
        }
    }
}
