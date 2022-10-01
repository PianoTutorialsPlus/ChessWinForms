using ChessWindowsForms.Model;
using ChessWindowsForms.Presenter.Pieces;
using ChessWindowsForms.Tests.Infrastructure;
using NUnit.Framework;

namespace ChessWindowseForms.Tests
{
    public class KnightTests
    {
        [SetUp]
        public void Setup()
        {
        }
        private static Knight GetKnight(int column, int row)
        {
            return A.ChessPiece<Knight>()
                .WithPosition(column, row);
        }
        public class TheMoveToMethod : KnightTests
        {
            [TestCase(1, -2)]
            [TestCase(2, -1)]
            [TestCase(2, 1)]
            [TestCase(1, 2)]
            [TestCase(-1, 2)]
            [TestCase(-2, 1)]
            [TestCase(-2, -1)]
            [TestCase(-1, -2)]
            public void Given_StartPos_At_4_4_When_Piece_MoveTo_Valid_Position_Then_Position_Returns_EndPosition(int row, int column)
            {
                var startPos = new Position(4, 4);
                var piece = GetKnight(startPos.Row, startPos.Column);
                var endPos = startPos + new Position(row, column);

                piece.PossiblePositions();
                piece.MoveTo(endPos);

                Assert.AreEqual((endPos.Row, endPos.Column), (piece.Position.Row, piece.Position.Column));
            }
            [TestCase(0, -2)]
            [TestCase(2, 0)]
            [TestCase(0, 2)]
            [TestCase(-2, 0)]
            public void Given_StartPos_At_4_4_When_Piece_MoveTo_Invalid_Position_Then_Position_Returns_StartPosition(int row, int column)
            {
                var startPos = new Position(4, 4);
                var piece = GetKnight(startPos.Row, startPos.Column);
                var endPos = startPos + new Position(row, column);

                piece.MoveTo(endPos);

                Assert.AreEqual((startPos.Row, startPos.Column), (piece.Position.Row, piece.Position.Column));
            }
            [TestCase(-2, -1)]
            [TestCase(-1, -2)]
            public void Given_StartPos_At_1_1_When_Piece_Move_OutOfBounds_Then_Piece_Has_Not_Moved(int row, int column)
            {
                var startPos = new Position(1, 1);
                var piece = GetKnight(startPos.Row, startPos.Column);
                var endPos = startPos + new Position(row, column);

                piece.MoveTo(endPos);

                Assert.IsFalse(piece.HasMoved);
            }
            [TestCase(2, 1)]
            [TestCase(1, 2)]
            public void Given_StartPos_At_6_6_When_Piece_Move_OutOfBounds_Then_Piece_Has_Not_Moved(int row, int column)
            {
                var startPos = new Position(6, 6);
                var piece = GetKnight(startPos.Row, startPos.Column);
                var endPos = startPos + new Position(row, column);

                piece.MoveTo(endPos);

                Assert.IsFalse(piece.HasMoved);
            }
        }
    }
}
