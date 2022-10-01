using ChessWindowsForms.Model;
using ChessWindowsForms.Presenter.Commands;
using ChessWindowsForms.View.Contracts;
using ChessWindowsForms.View.Helper;
using System;
using System.Drawing;

namespace ChessWindowsForms.Presenter.Factories
{
    public class ChessPieceSpawner
    {
        private Player _player;
        private CommandProcessor _commandProcessor;

        public ChessPieceSpawner(
            Player player,
            CommandProcessor commandProcessor)
        {
            //_settings = settings;
            _player = player;
            _commandProcessor = commandProcessor;
        }

        public T SpawnChessPiece<T>(int column, int row, Bitmap chessFigure) where T : IChessPieceView
        {
            var chessPiece = CreatePiece<T>(column, row, chessFigure);

            return chessPiece;
        }
        private T CreatePiece<T>(int column, int row, Bitmap chessFigure) where T : IChessPieceView
        {
            var position = new Position(column, row);
            var model = CreateModel(chessFigure);
            T chessPiece = (T)Activator.CreateInstance(
                typeof(T),
                model,
                CreateMover(position));

            return chessPiece;
        }
        private ChessPieceModel CreateModel(Bitmap chessFigure)
        {
            return new ChessPieceModel(
                chessFigure,
                _player.Color);
        }
        private ChessPieceMover CreateMover(Position position)
        {
            return new ChessPieceMover(
                position,
                _player,
                _commandProcessor);
        }
    }
}
