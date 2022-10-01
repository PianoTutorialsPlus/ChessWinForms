using ChessWindowsForms.View.Contracts;
using System.Collections.Generic;

namespace ChessWindowsForms.Model.Contracts
{
    public interface IChessPiece : IChessPieceView
    {
        List<Position> PossiblePositions();
        Position Position { get; }
        bool HasMoved { get; }
        void MoveTo(Position panelPosition);
    }
}