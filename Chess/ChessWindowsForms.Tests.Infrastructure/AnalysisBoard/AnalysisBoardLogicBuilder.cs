using ChessWindowsForms.Model.UI;
using ChessWindowsForms.View.Contracts;

namespace ChessWindowsForms.Tests.Infrastructure
{
    public class AnalysisBoardLogicBuilder : TestDataBuilder<AnalysisBoardLogic>
    {
        private IGameplay _gameplay;
        private IAnalysisBoardView _view;

        public AnalysisBoardLogicBuilder() : this(An.IGameplay.Build(),An.IAnalysisBoardView.Build())
        {
        }

        public AnalysisBoardLogicBuilder(IGameplay gameplay, IAnalysisBoardView view)
        {
            _gameplay = gameplay;
            _view = view;
        }
        public AnalysisBoardLogicBuilder WithGameplay(IGameplay gameplay)
        {
            _gameplay = gameplay;
            return this;
        }
        public AnalysisBoardLogicBuilder WithView(IAnalysisBoardView view)
        {
            _view = view;
            return this;
        }

        public override AnalysisBoardLogic Build()
        {
            return new AnalysisBoardLogic(_gameplay, _view);
        }
    }
}