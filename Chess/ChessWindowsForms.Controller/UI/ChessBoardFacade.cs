using ChessWindowsForms.Model.Contracts;
using ChessWindowsForms.Model.UI;
using ChessWindowsForms.View.Contracts;
using ChessWindowsForms.View.UI;

namespace ChessWindowsForms.Controller.UI
{
    public class ChessBoardFacade : IChessBoard
    {
        private readonly IAnalysisBoard _analysisBoard;
        private readonly UserControlChessBoard _userControlChessBoard;
        private readonly ChessBoardLogic _chessBoardLogic;

        public UserControlChessBoard Model => _userControlChessBoard;

        public ChessBoardFacade(
            IAnalysisBoard analysisBoard,
            UserControlChessBoard userControlChessBoard, 
            ChessBoardLogic chessBoardLogic)
        {
            _analysisBoard = analysisBoard;
            _userControlChessBoard = userControlChessBoard;
            _chessBoardLogic = chessBoardLogic;

            ConnectEvents();
        }


        private void ConnectEvents()
        {
            _userControlChessBoard.Dragged += _chessBoardLogic.OnDrop;
            _userControlChessBoard.Dropped += _chessBoardLogic.OnDrag;

            _chessBoardLogic.UpdateMove += _analysisBoard.UpdateMove;
        }

    }
}
