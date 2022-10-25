using ChessWindowsForms.Presenter;
using ChessWindowsForms.Presenter.Pieces;
using ChessWindowsForms.Tests.Infrastructure;
using ChessWindowsForms.View.Contracts;
using System.Collections.Generic;

namespace ChessWindowseForms.Tests
{
    public class TestsBase
    {
        public Player GetPlayer(object[] pieces)
        {
            return A.Player
                .WithPieces(SetPieces(pieces));
        }
        private List<IChessPieceView> SetPieces(object[] piece)
        {
            var pieces = new List<IChessPieceView>();
            foreach (object pieceItem in piece)
                pieces.Add(pieceItem as ChessPieceFacade);

            return pieces;
        }
    }
}