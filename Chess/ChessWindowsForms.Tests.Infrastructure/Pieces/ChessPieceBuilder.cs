using ChessWindowsForms.Model;
using ChessWindowsForms.Presenter.Commands;
using ChessWindowsForms.View.Helper;
using System;
using System.Drawing;

namespace ChessWindowsForms.Tests.Infrastructure
{
    public class ChessPieceBuilder<T> : TestDataBuilder<T> /*where T : ChessPiece*/
    {
        private Position _position;
        private Color _color = Color.White;
        private CommandProcessor _commandProcessor;
        private ChessPieceModel _model;
        private ChessPieceMover _mover;



        public ChessPieceBuilder<T> WithPosition(int column,int row)
        {
            _position = new Position(column,row);
            return this;
        }

        public ChessPieceBuilder<T> WithColor(Color color)
        {
            _color = color;
            return this;
        }
        public ChessPieceBuilder<T> WithCommandProcessor(CommandProcessor commandProcessor)
        {
            _commandProcessor = commandProcessor;
            return this;
        }
        public ChessPieceBuilder<T> WithModel(ChessPieceModel model)
        {
            _model = model;
            return this;
        }
        public ChessPieceBuilder<T> WithMover(ChessPieceMover mover)
        {
            _mover = mover;
            return this;
        }

        public override T Build()
        {
            return (T)Activator.CreateInstance(
                typeof(T),
                _model ?? A.ChessPieceModel
                    .WithColor(_color),
                _mover ?? A.ChessPieceMover
                    .WithPosition(_position.Column,_position.Row));
        }
    }
}