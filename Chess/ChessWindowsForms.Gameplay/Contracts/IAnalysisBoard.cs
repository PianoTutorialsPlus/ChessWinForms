using ChessWindowsForms.View.Contracts;

namespace ChessWindowsForms.Model.Contracts
{
    public interface IAnalysisBoard : IAnalysisBoardModel
    {
        void UpdateMove(IChessPiece chessPiece);
    }
}