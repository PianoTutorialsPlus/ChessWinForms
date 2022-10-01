using ChessWindowsForms.View.Contracts;
using NSubstitute;

namespace ChessWindowsForms.Tests.Infrastructure
{
    public class IAnalysisBoardViewBuilder : TestDataBuilder<IAnalysisBoardView>
    {
        public IAnalysisBoardViewBuilder()
        {
        }

        public override IAnalysisBoardView Build()
        {
            var view = Substitute.For<IAnalysisBoardView>();
            
            return view;
        }
    }
}