using ChessWindowsForms;
using ChessWindowsForms.View.Contracts;
using ChessWindowsForms.View.UI;

namespace ChessWindowsForms.View.Contracts
{
    public interface IAnalysisBoardModel
    {
        UserControlAnalysisBoard Model { get; }
    }
}