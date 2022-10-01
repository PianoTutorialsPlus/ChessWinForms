using ChessWindowsForms.Model;
using ChessWindowsForms.View.Helper;
using System.Collections.Generic;

namespace ChessWindowsForms.Presenter.Pieces
{
    public class Knight : ChessPieceFacade
    {
        public Knight(ChessPieceModel model, ChessPieceMover mover) : base(model, mover)
        {
        }

        public override List<Position> PossiblePositions()
        {
            _mover.PossiblePositions.Clear();
            _mover.AddKnightsMove(1, -2);
            _mover.AddKnightsMove(2, -1);
            _mover.AddKnightsMove(2, 1);
            _mover.AddKnightsMove(1, 2);
            _mover.AddKnightsMove(-1, 2);
            _mover.AddKnightsMove(-2, 1);
            _mover.AddKnightsMove(-2, -1);
            _mover.AddKnightsMove(-1, -2);

            return _mover.PossiblePositions;

        }
    }
}
