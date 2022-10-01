using ChessWindowsForms.Model.Commands;

namespace ChessWindowsForms.Model.Contracts
{
    public interface ICommandProcessor
    {
        void Undo();
        void ExecuteCommand(Command moveToCommand);
    }
}