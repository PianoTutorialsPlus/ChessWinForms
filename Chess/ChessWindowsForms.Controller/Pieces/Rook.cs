using ChessWindowsForms.Model;
using ChessWindowsForms.View.Helper;
using System.Collections.Generic;

namespace ChessWindowsForms.Presenter.Pieces
{
    public class Rook : ChessPieceFacade
    {
        public Rook(ChessPieceModel model, ChessPieceMover mover) : base(model, mover)
        {
        }

        public override List<Position> PossiblePositions()
        {
            _mover.PossiblePositions.Clear();
            _mover.AddOrthogonal(7);

            return _mover.PossiblePositions;
        }
    }
}
