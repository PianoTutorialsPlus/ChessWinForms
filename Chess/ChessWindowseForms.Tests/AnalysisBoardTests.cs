using ChessWindowsForms;
using ChessWindowsForms.Tests.Infrastructure;
using NUnit.Framework;
using System.Drawing;
using System.Windows.Forms;
using NSubstitute;
using ChessWindowsForms.Model.UI;
using ChessWindowsForms.Presenter.Pieces;
using ChessWindowsForms.View.Contracts;

namespace ChessWindowseForms.Tests
{
    public class AnalysisBoardTests
    {
        private IAnalysisBoardView _analysisBoard;
        private AnalysisBoardLogic _analysis;

        [SetUp]
        public void Setup()
        {
            IGameplay gameplay = An.IGameplay
                .WithColor(Color.White).Build();

            _analysisBoard = An.IAnalysisBoardView.Build();
            
            _analysis = An.AnalysisBoardModel
                .WithGameplay(gameplay)
                .WithView(_analysisBoard);
        }

        private static King GetKing(int column, int row, Color color)
        {
            return A.ChessPiece<King>()
                .WithPosition(column, row)
                .WithColor(color);
        }
        private static Rook GetRook(int column, int row, Color color)
        {
            return A.ChessPiece<Rook>()
                .WithPosition(column, row)
                .WithColor(color);
        }

        public class TheUpdateMoveMethod : AnalysisBoardTests
        {
            [Test]
            public void Given_White_King_When_Moved_Then_Nr_Is_1()
            {

                var piece = GetKing(0, 0, Color.White);

                _analysis.UpdateMove(piece);

                string[] text = { "1" , Arg.Any<string>(), Arg.Any<string>() };
                //var turnInfo = _analysisBoard.Items[0].Text;

                _analysisBoard.Received().AddEntry(text);
                //_analysisBoard.Received().AddEntry(Arg.Any<string[]>());
                //Assert.AreEqual("1", turnInfo);
            }
            //[Test]
            //public void Given_Black_King_When_Moved_Then_Count_Is_1()
            //{
            //    var king = GetKing(0, 0, Color.White);
            //    var piece = GetKing(0, 0, Color.Black);

            //    _analysis.UpdateMove(king);
            //    _analysis.UpdateMove(piece);

            //    var turnInfo = _analysisBoard.Items.Count;

            //    Assert.AreEqual(1, turnInfo);
            //}
            //[Test]
            //public void Given_White_King_When_Moved_Twice_Then_Nr_Is_2()
            //{
            //    var king = GetKing(0, 0, Color.Black);
            //    var piece = GetKing(0, 0, Color.White);

            //    _analysis.UpdateMove(piece);
            //    _analysis.UpdateMove(king);
            //    _analysis.UpdateMove(piece);
            //    var turnInfo = _analysisBoard.Items[1].Text;

            //    Assert.AreEqual("2", turnInfo);
            //}
            //[Test]
            //public void Given_White_King_When_Moved_Twice_Then_Count_Is_2()
            //{
            //    var piece = GetKing(0, 0, Color.White);

            //    _analysis.UpdateMove(piece);
            //    _analysis.UpdateMove(piece);
            //    var turnInfo = _analysisBoard.Items.Count;

            //    Assert.AreEqual(2, turnInfo);
            //}
            [Test]
            public void Given_White_King_When_Moved_To_1_1_Then_Text_Is_Kb7()
            {
                var piece = GetKing(1, 1, Color.White);

                _analysis.UpdateMove(piece);

                string[] text = { "1", "Kb7", "" };
                //var turnInfo = _analysisBoard.Items[0].Text;

                _analysisBoard.Received().AddEntry(text);

            }
            //[Test]
            //public void Given_White_Turret_When_Moved_To_0_0_Then_Text_Is_Ra8()
            //{
            //    var piece = GetRook(0, 0, Color.White);

            //    _analysis.UpdateMove(piece);
            //    var turnInfo = _analysisBoard.Items[0].SubItems[1].Text;

            //    Assert.AreEqual("Ra8", turnInfo);
            //}
            //[Test]
            //public void Given_Black_Turret_When_Moved_To_0_0_Then_Text_Is_Ra8()
            //{
            //    var piece = GetRook(0, 0, Color.Black);

            //    _analysis.UpdateMove(piece);
            //    var turnInfo = _analysisBoard.Items[0].SubItems[2].Text;

            //    Assert.AreEqual("Ra8", turnInfo);
            //}
            //[Test]
            //public void Given_White_Turret_When_Moved_Twice_Then_Black_Text_Is_Empty()
            //{
            //    var king = GetKing(1, 1, Color.Black);
            //    var piece = GetRook(0, 0, Color.White);

            //    _analysis.UpdateMove(piece);
            //    _analysis.UpdateMove(king);
            //    _analysis.UpdateMove(piece);
            //    var turnInfo = _analysisBoard.Items[1].SubItems[2].Text;

            //    Assert.AreEqual("", turnInfo);
            //}
        }
    }
}
