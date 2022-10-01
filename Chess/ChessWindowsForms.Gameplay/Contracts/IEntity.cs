namespace ChessWindowsForms.Model.Contracts
{
    public interface IEntity
    {
        Position Position { get; }

        void MoveFromTo(Position startPosition, Position endPosition);
    }
}