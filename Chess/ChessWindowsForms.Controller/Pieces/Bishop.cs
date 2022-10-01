using ChessWindowsForms.Model;
using ChessWindowsForms.View.Helper;
using System.Collections.Generic;

namespace ChessWindowsForms.Presenter.Pieces
{
    public class Bishop : ChessPieceFacade
    {
        public Bishop(ChessPieceModel model, ChessPieceMover mover) : base(model, mover)
        {
        }

        public override List<Position> PossiblePositions()
        {
            _mover.PossiblePositions.Clear();

            _mover.AddDiagonal(7);

            return _mover.PossiblePositions;
        }
    }
}
