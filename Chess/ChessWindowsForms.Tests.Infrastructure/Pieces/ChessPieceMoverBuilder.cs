using ChessWindowsForms.Model;
using ChessWindowsForms.Model.Contracts;
using ChessWindowsForms.Presenter.Commands;
using ChessWindowsForms.View.Contracts;

namespace ChessWindowsForms.Tests.Infrastructure
{
    public class ChessPieceMoverBuilder : TestDataBuilder<ChessPieceMover>
    {
        private Position _position;
        private IPlayer _player;
        private ICommandProcessor _commandProcessor;

        public ChessPieceMoverBuilder() : this(A.Position, An.IPlayer.Build(), A.CommandProcessor)
        {
        }

        public ChessPieceMoverBuilder(Position position, IPlayer player, CommandProcessor commandProcessor)
        {
            _position = position;
            _player = player;
            _commandProcessor = commandProcessor;
        }

        public ChessPieceMoverBuilder WithPosition(int column, int row)
        {
            _position = new Position(column, row);
            return this;
        }
        public ChessPieceMoverBuilder WithPlayer(IPlayer player)
        {
            _player = player;
            return this;
        }
        public ChessPieceMoverBuilder WithCommandProcessor(CommandProcessor commander)
        {
            _commandProcessor = commander;
            return this;
        }

        public override ChessPieceMover Build()
        {
            return new ChessPieceMover(_position, _player, _commandProcessor);
        }
    }
}