using ChessWindowsForms.Model;

namespace ChessWindowsForms.Tests.Infrastructure
{
    public class PositionBuilder : TestDataBuilder<Position>
    {
        private int _column;
        private int _row;

        public PositionBuilder() : this(0,0)
        {
        }

        public PositionBuilder(int column, int row)
        {
            _column = column;
            _row = row;
        }

        public PositionBuilder WithPosition(int column, int row)
        {
            _column = column;
            _row = row;
            return this;
        }

        public override Position Build()
        {
            return new Position(_column, _row);
;       }
    }
}