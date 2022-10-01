using ChessWindowsForms.View.Helper;
using System;
using System.Drawing;

namespace ChessWindowsForms.Tests.Infrastructure
{
    public class ChessPieceModelBuilder : TestDataBuilder<ChessPieceModel>
    {
        private Image _image;
        private Color _color;

        public ChessPieceModelBuilder() : this(Properties.Resources.King,Color.White)
        {
        }

        public ChessPieceModelBuilder(Image image, Color color)
        {
            _image = image;
            _color = color;
        }
        public ChessPieceModelBuilder WithColor(Color color)
        {
            _color = color;
            return this;
        }

        public override ChessPieceModel Build()
        {
            return new ChessPieceModel(_image,_color);
        }


    }
}