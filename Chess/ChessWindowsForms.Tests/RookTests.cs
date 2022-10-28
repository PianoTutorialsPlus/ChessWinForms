using ChessWindowsForms.Model;
using ChessWindowsForms.Presenter.Pieces;
using ChessWindowsForms.Tests.Infrastructure;
using NUnit.Framework;

namespace ChessWindowseForms.Tests
{
    public class RookTests : TestsBase
    {
        [SetUp]
        public void Setup()
        {
        }
        private static Rook GetRook(int column, int row)
        {
            return A.ChessPiece<Rook>()
                .WithPosition(column, row);
        }
        public class TheMoveToMethod : RookTests
        {
            [TestCase(-1, 0)]
            [TestCase(6, 0)]
            [TestCase(0, -1)]
            [TestCase(0, 6)]
            public void Given_StartPos_At_1_1_When_Piece_MoveTo_Valid_Position_Then_Position_Returns_EndPosition(int row, int column)
            {
                var startPos = new Position(1, 1);
                var piece = GetRook(startPos.Row, startPos.Column);
                var endPos = startPos + new Position(row, column);

                piece.PossiblePositions();
                piece.MoveTo(endPos);

                Assert.AreEqual((endPos.Row, endPos.Column), (piece.Position.Row, piece.Position.Column));
            }
            [TestCase(-1, -1)]
            [TestCase(-1, 1)]
            [TestCase(1, -1)]
            [TestCase(1, 1)]
            public void Given_StartPos_At_2_2_When_Piece_MoveTo_Invalid_Position_Then_Position_Returns_StartPosition(int row, int column)
            {
                var startPos = new Position(2, 2);
                var piece = GetRook(startPos.Row, startPos.Column);
                var endPos = startPos + new Position(row, column);

                piece.MoveTo(endPos);

                Assert.AreEqual((startPos.Row, startPos.Column), (piece.Position.Row, piece.Position.Column));
            }
            [TestCase(-1, 0)]
            [TestCase(-1, -1)]
            [TestCase(0, -1)]
            [TestCase(8, 0)]
            [TestCase(8, 8)]
            [TestCase(0, 8)]
            public void Given_StartPos_At_0_0_When_King_Move_OutOfBounds_Then_Piece_Has_Not_Moved(int row, int column)
            {
                var startPos = new Position(0, 0);
                var piece = GetRook(startPos.Row, startPos.Column);
                var endPos = startPos + new Position(row, column);

                piece.MoveTo(endPos);

                Assert.IsFalse(piece.HasMoved);
            }
        }
        //public class TheValidPositionsMethod : RookTests
        //{
        //   [Test]
        //   public void Given_StartPos_At_0_0_When_Rook_Move_Behind_Position_With_Piece_Then_Piece_Has_Not_Moved()
        //   {
        //       var startPos = new Position(0, 0);
        //       var piece = GetRook(startPos.Row, startPos.Column);
        //       var piece2 = GetRook(1, 0);
        //       var pieces = new object[] { piece, piece2 };
        //       var player = GetPlayer(pieces);

        //       piece.PossiblePositions();
        //       var endPos = startPos + new Position(2, 0);

        //       piece.MoveTo(endPos);

        //       Assert.IsFalse(piece.HasMoved);
        //   }
        //}
    }
}