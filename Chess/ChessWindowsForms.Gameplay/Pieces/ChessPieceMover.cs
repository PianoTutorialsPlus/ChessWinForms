using ChessWindowsForms.Model.Commands;
using ChessWindowsForms.Model.Contracts;
using ChessWindowsForms.View.Contracts;
using System.Collections.Generic;

namespace ChessWindowsForms.Model
{
    public class ChessPieceMover : IEntity
    {
        private IPlayer _player;
        private ICommandProcessor _commandProcessor;
        private Position _position;
        private List<Position> _possiblePositions = new List<Position>();
        private List<IChessPieceView> _chessPieces => _player.ChessPieceList;

        public List<Position> PossiblePositions => _possiblePositions;
        public bool HasMoved { get; internal set; }
        public Position Position
        {
            get => _position;
            set => _position = value;
        }

        public ChessPieceMover(
            Position position,
            IPlayer player,
            ICommandProcessor commandProcessor)
        {
            _player = player;
            _commandProcessor = commandProcessor;
            _position = position;
        }

        private bool TryAddPosition(int column, int row, ref List<Position> result)
        {
            var newPosition = new Position(column, row);
            if (IsPositionBlocked(newPosition)) return false;
            if (!IsPositionInBounds(newPosition)) return false;

            result.Add(newPosition);

            return true;
        }
        public void AddOrthogonal(int amount)
        {
            _possiblePositions.AddRange(AddMoveUp(amount));
            _possiblePositions.AddRange(AddMoveRight(amount));
            _possiblePositions.AddRange(AddMoveDown(amount));
            _possiblePositions.AddRange(AddMoveLeft(amount));
        }
        public void AddDiagonal(int amount)
        {
            _possiblePositions.AddRange(AddMoveUpRight(amount));
            _possiblePositions.AddRange(AddMoveDownRight(amount));
            _possiblePositions.AddRange(AddMoveDownLeft(amount));
            _possiblePositions.AddRange(AddMoveUpLeft(amount));

        }
        public List<Position> AddMoveUp(int amount)
        {
            List<Position> result = new List<Position>();
            for (int i = 1; i <= amount; i++)
            {
                bool valid = TryAddPosition(_position.Column, _position.Row - i, ref result);
                if (!valid) break;
            }

            return result;
        }
        public List<Position> AddMoveUpRight(int amount)
        {
            List<Position> result = new List<Position>();
            for (int i = 1; i <= amount; i++)
            {
                bool valid = TryAddPosition(_position.Column + i, _position.Row - i, ref result);
                if (!valid) break;
            }
            return result;
        }
        public List<Position> AddMoveRight(int amount)
        {
            List<Position> result = new List<Position>();
            for (int i = 1; i <= amount; i++)
            {
                bool valid = TryAddPosition(_position.Column + i, _position.Row, ref result);
                if (!valid) break;
            }
            return result;
        }
        public List<Position> AddMoveDownRight(int amount)
        {
            List<Position> result = new List<Position>();
            for (int i = 1; i <= amount; i++)
            {
                bool valid = TryAddPosition(_position.Column + i, _position.Row + i, ref result);
                if (!valid) break;
            }
            return result;
        }
        public List<Position> AddMoveDown(int amount)
        {
            List<Position> result = new List<Position>();
            for (int i = 1; i <= amount; i++)
            {
                bool valid = TryAddPosition(_position.Column, _position.Row + i, ref result);
                if (!valid) break;
            }
            return result;
        }
        public List<Position> AddMoveDownLeft(int amount)
        {
            List<Position> result = new List<Position>();
            for (int i = 1; i <= amount; i++)
            {
                bool valid = TryAddPosition(_position.Column - i, _position.Row + i, ref result);
                if (!valid) break;
            }
            return result;
        }
        public List<Position> AddMoveLeft(int amount)
        {
            List<Position> result = new List<Position>();
            for (int i = 1; i <= amount; i++)
            {
                bool valid = TryAddPosition(_position.Column - i, _position.Row, ref result);
                if (!valid) break;
            }
            return result;
        }
        public List<Position> AddMoveUpLeft(int amount)
        {
            List<Position> result = new List<Position>();
            for (int i = 1; i <= amount; i++)
            {
                bool valid = TryAddPosition(_position.Column - i, _position.Row - i, ref result);
                if (!valid) break;
            }
            return result;
        }
        public void AddKnightsMove(int column, int row)
        {
            List<Position> result = new List<Position>();
            TryAddPosition(_position.Column + column, _position.Row + row, ref result);
            _possiblePositions.AddRange(result);
        }

        public virtual void MoveTo(Position position)
        {
            if (CanMoveTo(position))
            {
                _commandProcessor.ExecuteCommand(new MoveToCommand(this, position));
            }
        }
        protected bool CanMoveTo(Position position)
        {
            foreach (Position pos in _possiblePositions)
            {
                if (position == pos && IsPositionInBounds(position)) return HasMoved = true;
            }
            return HasMoved = false;
        }
        protected bool IsPositionInBounds(Position newPosition)
        {
            return
                newPosition.Row >= 0 && newPosition.Row <= 7 &&
                newPosition.Column >= 0 && newPosition.Column <= 7;
        }
        private bool IsPositionBlocked(Position newPosition)
        {
            if (_chessPieces != null)
            {
                foreach (IChessPiece piece in _chessPieces)
                {
                    if (piece.Position == newPosition) return true;
                }
            }

            return false;
        }
        public void MoveFromTo(Position startPosition, Position endPosition)
        {
            _position = endPosition;
        }
    }
}