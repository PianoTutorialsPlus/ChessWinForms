using ChessWindowsForms;
using ChessWindowsForms.View.Contracts;
using System.Collections.Generic;

namespace ChessWindowsForms.View.Contracts
{
    public interface IPlayer
    {
        List<IChessPieceView> ChessPieceList { get; }
    }
}