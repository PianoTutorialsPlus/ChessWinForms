using ChessWindowsForms.Presenter.Commands;

namespace ChessWindowsForms.Tests.Infrastructure
{
    public class CommandProcessorBuilder : TestDataBuilder<CommandProcessor>
    {
        public CommandProcessorBuilder()
        {
        }

        public override CommandProcessor Build()
        {
            return new CommandProcessor();
        }
    }
}