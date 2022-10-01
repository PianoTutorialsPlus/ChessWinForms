using ChessWindowsForms.Model.Contracts;

namespace ChessWindowsForms.Model.Commands
{
    public class MoveToCommand : Command
    {
        private Position _destination;
        private Position _originalPosition;

        public MoveToCommand(IEntity entity, Position destination) : base(entity)
        {
            _destination = destination;
        }

        public override void Execute()
        {
            _originalPosition = _entity.Position;
            _entity.MoveFromTo(_originalPosition, _destination);
        }
        public override void Undo()
        {
            _entity.MoveFromTo(_entity.Position, _originalPosition);
        }
    }
}
