using ChessWindowsForms.Model;
using ChessWindowsForms.Presenter.Pieces;
using ChessWindowsForms.Tests.Infrastructure;
using NUnit.Framework;
using System.Drawing;

namespace ChessWindowseForms.Tests
{
    public class KingTests : TestsBase
    {
        [SetUp]
        public void Setup()
        {
        }
        private King GetKing(int column, int row)
        {
            return A.ChessPiece<King>()
                .WithPosition(column, row);
        }
        private King GetKing(int column, int row, Color color)
        {
            return A.ChessPiece<King>()
                .WithPosition(column, row)
                .WithColor(color);
        }


        public class TheMoveToMethod : KingTests
        {
            [Test]
            public void When_Not_Moved_Then_Position_Is_0_0()
            {
                var startPos = new Position(0, 0);
                var piece = GetKing(startPos.Row, startPos.Column);
                piece.MoveTo(new Position(0, 0));

                Assert.AreEqual((0,0), (piece.Position.Row,piece.Position.Column));
            }

            [TestCase(1,0)]
            [TestCase(1, 1)]
            [TestCase(0, 1)]
            [TestCase(-1, 1)]
            [TestCase(-1, 0)]
            [TestCase(-1, -1)]
            [TestCase(0, -1)]
            [TestCase(1, -1)]
            public void Given_StartPos_At_1_1_When_King_MoveTo_Valid_Position_Then_Position_Returns_EndPosition(int row, int column)
            {
                var startPos = new Position(1, 1);
                var piece = GetKing(startPos.Row, startPos.Column);
                var endPos = startPos + new Position(row,column);

                piece.PossiblePositions();
                piece.MoveTo(endPos);

                Assert.AreEqual((endPos.Row, endPos.Column), (piece.Position.Row, piece.Position.Column));
            }

            [TestCase(2, 0)]
            [TestCase(2, 2)]
            [TestCase(0, 2)]
            [TestCase(-2, 2)]
            [TestCase(-2, 0)]
            [TestCase(-2, -2)]
            [TestCase(0, -2)]
            [TestCase(2, -2)]
            public void Given_StartPos_At_4_4_When_King_MoveTo_InValid_Position_Then_Position_Returns_StartPosition(int row, int column)
            {
                var startPos = new Position(4, 4);
                var piece = GetKing(startPos.Row, startPos.Column);
                var endPos = startPos + new Position(row, column);

                piece.PossiblePositions();
                piece.MoveTo(endPos);

                Assert.AreEqual((startPos.Row, startPos.Column), (piece.Position.Row, piece.Position.Column));
            }

            [TestCase(-1, 0)]
            [TestCase(-1, -1)]
            [TestCase(0, -1)]
            public void Given_StartPos_At_0_0_When_King_Move_OutOfBounds_Then_Piece_Has_Not_Moved(int row, int column)
            {
                var startPos = new Position(0, 0);
                var piece = GetKing(startPos.Row, startPos.Column);
                var endPos = startPos + new Position(row, column);

                piece.MoveTo(endPos);

                Assert.IsFalse(piece.HasMoved);
            }
            [TestCase(1, 0)]
            [TestCase(1, 1)]
            [TestCase(0, 1)]
            public void Given_StartPos_At_7_7_When_King_Move_OutOfBounds_Then_Piece_Has_Not_Moved(int row, int column)
            {
                var startPos = new Position(7, 7);
                var piece = GetKing(startPos.Row, startPos.Column);
                var endPos = startPos + new Position(row, column);

                piece.MoveTo(endPos);

                Assert.IsFalse(piece.HasMoved);
            }
        }
        //public class TheValidPositionsMethod : KingTests
        //{
        //    [Test]
        //    public void Given_StartPos_At_0_0_When_2nd_Piece_Is_in_MoveRange_Then_Count_Is_2()
        //    {
        //        var piece = GetKing(0, 0);
        //        var piece2 = GetKing(1, 1);
        //        var pieces = new object[] {piece , piece2};
        //        var player = GetPlayer(pieces);

        //        var positions = piece.PossiblePositions();
        //        Assert.AreEqual(2, positions.Count);
        //    }
        //    [Test]
        //    public void Given_StartPos_At_0_0_When_Black_Piece_Is_in_MoveRange_Then_Count_Is_3()
        //    {
        //        var piece = GetKing(0, 0);
        //        var pieces = new object[] {piece};
        //        GetKing(1, 1,Color.Black);
        //        var player = GetPlayer(pieces);

        //        var positions = piece.PossiblePositions();

        //        Assert.AreEqual(3, positions.Count);
        //    }
        //    [Test]
        //    public void Given_StartPos_At_0_0_When_King_MoveTo_Position_With_Piece_Then_Piece_Has_Not_Moved()
        //    {
        //        var startPos = new Position(0, 0);
        //        var piece = GetKing(startPos.Row, startPos.Column);
        //        var piece2 = GetKing(1, 1);
        //        var pieces = new object[] { piece, piece2 };
        //        var player = GetPlayer(pieces);

        //        //piece.ValidPositions(player.ChessPieceList);
        //        piece.PossiblePositions();
        //        var endPos = startPos + new Position(1, 1);

        //        piece.MoveTo(endPos);

        //        Assert.IsFalse(piece.HasMoved);
        //    }

        //}
    }
}