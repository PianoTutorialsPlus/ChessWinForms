using ChessWindowsForms.Model.Contracts;
using ChessWindowsForms.Model.UI;
using ChessWindowsForms.View.UI;
using System;

namespace ChessWindowsForms.Presenter.UI
{
    public class AnalysisBoardFacade : IAnalysisBoard
    {
        private readonly UserControlAnalysisBoard _analysisBoardModel;
        private readonly IAnalysisBoardM _analyisBoardLogic;

        public UserControlAnalysisBoard Model => _analysisBoardModel;

        public AnalysisBoardFacade(
            UserControlAnalysisBoard analysisBoardModel,
            IAnalysisBoardM analyisBoardLogic)
        {

            _analysisBoardModel = analysisBoardModel;
            _analyisBoardLogic = analyisBoardLogic;

            ConnectEvents();
        }

        private void ConnectEvents()
        {
            _analyisBoardLogic.OnAddEntry += _analysisBoardModel.AddEntry;
            _analyisBoardLogic.OnRemoveEntry += _analysisBoardModel.RemoveEntry;
        }

        public void UpdateMove(IChessPiece piece) => _analyisBoardLogic.UpdateMove(piece);
    }
}
