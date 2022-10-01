namespace ChessWindowsForms.Model
{
    public class Position /*: IEquatable<Position>*/
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Position(int column, int row)
        {
            Row = row;
            Column = column;
        }

        public static Position operator +(Position self, Position other)
            => new Position(self.Column + other.Column, self.Row + other.Row);
        public static bool operator ==(Position self, Position other)
            => self.Column == other.Column && self.Row == other.Row;
        public static bool operator !=(Position self, Position other)
            => !(self == other);

        //public bool Equals(Position other)
        //{
        //    return
        //        Row == other.Row &&
        //        Column == other.Column;
        //}
        public override bool Equals(object obj)
        {
            return
                obj is Position other &&
                Row == other.Row &&
                Column == other.Column;
        }
    }


}