using ChessWindowsForms.Presenter;
using ChessWindowsForms.View.Contracts;
using System.Collections.Generic;
using System.Drawing;

namespace ChessWindowsForms.Tests.Infrastructure
{
    public class PlayerBuilder : TestDataBuilder<Player>
    {
        private List<IChessPieceView> _pieces = new List<IChessPieceView>();
        private Color _color = Color.White;

        public PlayerBuilder WithPieces(List<IChessPieceView> pieces)
        {
            _pieces.AddRange(pieces);
            return this;
        }
        public PlayerBuilder WithColor(Color color)
        {
            _color = color;
            return this;
        }

        public override Player Build()
        {
            return new Player(_pieces, _color);
        }
    }
}