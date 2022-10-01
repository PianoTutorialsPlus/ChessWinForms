using ChessWindowsForms;
using ChessWindowsForms.View.Contracts;

namespace ChessWindowsForms.View.Contracts
{
    public interface IAnalysisBoardView
    {
        void AddEntry(string[] turnData);
        void RemoveEntry();
    }
}