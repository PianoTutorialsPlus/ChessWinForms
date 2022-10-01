using ChessWindowsForms.View.Contracts;
using System.Collections.Generic;
using System.Drawing;

namespace ChessWindowsForms.Presenter
{
    public class Player : IPlayer
    {
        private List<IChessPieceView> _chessPieceList;
        private Color _color;

        public List<IChessPieceView> ChessPieceList => _chessPieceList;
        public Color Color => _color;

        public Player(
            List<IChessPieceView> chessPieceList,
            Color color) : this(color)
        {
            _chessPieceList = chessPieceList;
            _color = color;

        }
        public Player(Color color)
        {
            _chessPieceList = new List<IChessPieceView>();
            _color = color;
        }
    }
}
