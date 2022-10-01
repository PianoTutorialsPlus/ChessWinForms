using ChessWindowsForms.Model;
using ChessWindowsForms.Model.Contracts;
using ChessWindowsForms.View.Helper;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ChessWindowsForms.Presenter.Pieces
{
    public abstract class ChessPieceFacade : IChessPiece
    {
        protected ChessPieceModel _model;
        protected ChessPieceMover _mover;

        public PictureBox PictureBox => _model;
        public Position Position => _mover.Position;
        public bool HasMoved => _mover.HasMoved;
        public Color Color => _model.Color;
        public Image Image => _model.Image;

        protected ChessPieceFacade(
            ChessPieceModel model,
            ChessPieceMover mover)
        {
            _model = model;
            _mover = mover;
        }

        public abstract List<Position> PossiblePositions();

        public void AttachTo(Panel panel)
        {
            _model.Parent = panel;
        }
        public void Show() => _model.Show();
        public void Hide() => _model.Hide();
        public void Drop(DragDropEffects move)
        {
            _model.DoDragDrop(this, DragDropEffects.Move);
        }
        public virtual void MoveTo(Position position)
        {
            _mover.MoveTo(position);
        }
    }
}