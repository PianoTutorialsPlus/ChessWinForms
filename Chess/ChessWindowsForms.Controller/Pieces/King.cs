using ChessWindowsForms.Model;
using ChessWindowsForms.View.Helper;
using System.Collections.Generic;

namespace ChessWindowsForms.Presenter.Pieces
{
    public class King : ChessPieceFacade
    {
        public King(ChessPieceModel model, ChessPieceMover mover) : base(model, mover)
        {
        }

        public override List<Position> PossiblePositions()
        {
            _mover.PossiblePositions.Clear();

            _mover.AddOrthogonal(1);
            _mover.AddDiagonal(1);

            return _mover.PossiblePositions;
        }
    }
}
