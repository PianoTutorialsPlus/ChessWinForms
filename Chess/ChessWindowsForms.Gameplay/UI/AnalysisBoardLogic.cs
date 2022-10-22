using ChessWindowsForms.Model.Contracts;
using ChessWindowsForms.View.Contracts;
using System;
using System.Linq;

namespace ChessWindowsForms.Model.UI
{
    public class AnalysisBoardLogic : IAnalysisBoardM
    {
        private int _number = 1;
        private string _whiteTurn;
        private string _blackTurn;
        private IGameplay _gameplay;

        public event Action OnRemoveEntry;
        public event Action<string[]> OnAddEntry;

        public AnalysisBoardLogic(
            IGameplay gameplay)
        {
            _gameplay = gameplay;
        }

        public void UpdateMove(IChessPiece piece)
        {
            string number = _number.ToString();

            if (piece.Color == _gameplay.ColorWhite)
            {
                _whiteTurn = GetTurnInformation(piece);
                _blackTurn = "";
            }
            else
            {
                number = _number++.ToString();
                _blackTurn = GetTurnInformation(piece);

                OnRemoveEntry.Invoke();
            }

            string[] turnData = { number, _whiteTurn, _blackTurn };

            OnAddEntry?.Invoke(turnData);
        }
        public string GetTurnInformation(IChessPiece piece)
        {
            string firstLetter = GetFirstLetterOfPiece(piece);
            string column = GetColumnName(piece.Position.Column);
            string row = GetRowName(piece.Position.Row);

            return firstLetter + column + row;
        }
        private static string GetFirstLetterOfPiece(IChessPieceView piece)
        {
            var type = piece.GetType().ToString();
            var piecepyte = type.Split('.').Last();

            return piecepyte.First().ToString();
        }
        private string GetRowName(int row)
        {
            return (8 - row).ToString();
        }
        private string GetColumnName(int column)
        {
            char columnAsChar = (char)(column + 97);
            return columnAsChar.ToString();
        }
    }
}
