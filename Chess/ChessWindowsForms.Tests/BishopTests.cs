using ChessWindowsForms.Model;
using ChessWindowsForms.Presenter.Pieces;
using ChessWindowsForms.Tests.Infrastructure;
using NUnit.Framework;

namespace ChessWindowseForms.Tests
{
    public class BishopTests
    {
        [SetUp]
        public void Setup()
        {
        }
        private static Bishop GetBishop(int column, int row)
        {
            return A.ChessPiece<Bishop>()
                .WithPosition(column, row);
        }
        public class TheMoveToMethod : BishopTests
        {
            [TestCase(-1, -1)]
            [TestCase(6, 6)]

            public void Given_StartPos_At_1_1_When_Piece_MoveTo_Valid_Position_Then_Position_Returns_EndPosition(int row, int column)
            {
                var startPos = new Position(1, 1);
                var piece = GetBishop(startPos.Row, startPos.Column);
                var endPos = startPos + new Position(row, column);

                piece.PossiblePositions();
                piece.MoveTo(endPos);

                Assert.AreEqual((endPos.Row, endPos.Column), (piece.Position.Row, piece.Position.Column));
            }
            [TestCase(-1, 1)]
            [TestCase(6, -6)]

            public void Given_StartPos_At_1_6_When_Piece_MoveTo_Valid_Position_Then_Position_Returns_EndPosition(int row, int column)
            {
                var startPos = new Position(1, 6);
                var piece = GetBishop(startPos.Row, startPos.Column);
                var endPos = startPos + new Position(row, column);

                piece.PossiblePositions();
                piece.MoveTo(endPos);

                Assert.AreEqual((endPos.Row, endPos.Column), (piece.Position.Row, piece.Position.Column));
            }
            [TestCase(-1, 0)]
            [TestCase(0, 1)]
            [TestCase(0, -1)]
            [TestCase(1, 0)]
            public void Given_StartPos_At_1_1_When_Piece_MoveTo_Invalid_Position_Then_Position_Returns_StartPosition(int row, int column)
            {
                var startPos = new Position(1, 1);
                var piece = GetBishop(startPos.Row, startPos.Column);
                var endPos = startPos + new Position(row, column);

                piece.MoveTo(endPos);

                Assert.AreEqual((startPos.Row, startPos.Column), (piece.Position.Row, piece.Position.Column));
            }
            [TestCase(-1, -1)]
            [TestCase(8, 8)]
            public void Given_StartPos_At_0_0_When_Piece_Move_OutOfBounds_Then_Piece_Has_Not_Moved(int row, int column)
            {
                var startPos = new Position(0, 0);
                var piece = GetBishop(startPos.Row, startPos.Column);
                var endPos = startPos + new Position(row, column);

                piece.MoveTo(endPos);

                Assert.IsFalse(piece.HasMoved);
            }
            [TestCase(-1, 1)]
            [TestCase(8, -8)]
            public void Given_StartPos_At_0_7_When_Piece_Move_OutOfBounds_Then_Piece_Has_Not_Moved(int row, int column)
            {
                var startPos = new Position(0, 7);
                var piece = GetBishop(startPos.Row, startPos.Column);
                var endPos = startPos + new Position(row, column);

                piece.MoveTo(endPos);

                Assert.IsFalse(piece.HasMoved);
            }
        }
    }
}

