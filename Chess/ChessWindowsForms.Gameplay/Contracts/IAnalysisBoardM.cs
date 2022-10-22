using System;

namespace ChessWindowsForms.Model.Contracts
{
    public interface IAnalysisBoardM
    {
        event Action OnRemoveEntry;
        event Action<string[]> OnAddEntry;

        void UpdateMove(IChessPiece piece);
    }
}