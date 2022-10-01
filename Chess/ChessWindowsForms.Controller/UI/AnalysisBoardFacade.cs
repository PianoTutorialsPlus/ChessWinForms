using ChessWindowsForms.Model.Contracts;
using ChessWindowsForms.Model.UI;
using ChessWindowsForms.View.UI;

namespace ChessWindowsForms.Presenter.UI
{
    public class AnalysisBoardFacade : IAnalysisBoard
    {
        private readonly UserControlAnalysisBoard _analysisBoardModel;
        private readonly AnalysisBoardLogic _analyisBoardLogic;

        public UserControlAnalysisBoard Model => _analysisBoardModel;

        public AnalysisBoardFacade(
            UserControlAnalysisBoard analysisBoardModel,
            AnalysisBoardLogic analyisBoardLogic)
        {

            _analysisBoardModel = analysisBoardModel;
            _analyisBoardLogic = analyisBoardLogic;
        }

        public void UpdateMove(IChessPiece piece) => _analyisBoardLogic.UpdateMove(piece);
    }
}
