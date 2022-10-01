using ChessWindowsForms.Model.Contracts;
using ChessWindowsForms.View.Contracts;
using System.Linq;

namespace ChessWindowsForms.Model.UI
{
    public class AnalysisBoardLogic
    {
        private int _number = 1;
        private string _whiteTurn;
        private string _blackTurn;
        private IGameplay _gameplay;
        private IAnalysisBoardView _analysisBoard;

        public AnalysisBoardLogic(
            IGameplay gameplay,
            IAnalysisBoardView analysisBoard)
        {
            _gameplay = gameplay;
            _analysisBoard = analysisBoard;
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

                _analysisBoard.RemoveEntry();
            }

            string[] turnData = { number, _whiteTurn, _blackTurn };

            _analysisBoard.AddEntry(turnData);
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
