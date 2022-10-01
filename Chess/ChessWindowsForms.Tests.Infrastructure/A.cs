using ChessWindowsForms.Presenter.Pieces;

namespace ChessWindowsForms.Tests.Infrastructure
{
    public class A
    {
        public static ChessPieceBuilder<T> ChessPiece<T>() where T : 
            ChessPieceFacade => new ChessPieceBuilder<T>();

        public static PlayerBuilder Player => new PlayerBuilder();
        public static ChessPieceMoverBuilder ChessPieceMover => new ChessPieceMoverBuilder();

        public static ChessPieceModelBuilder ChessPieceModel => new ChessPieceModelBuilder();

        public static PositionBuilder Position => new PositionBuilder();

        public static CommandProcessorBuilder CommandProcessor => new CommandProcessorBuilder();
    }

    public class An
    {
        public static IGameplayBuilder IGameplay => new IGameplayBuilder();

        public static IPlayerBuilder IPlayer => new IPlayerBuilder();

        public static AnalysisBoardLogicBuilder AnalysisBoardModel => new AnalysisBoardLogicBuilder();
        public static IAnalysisBoardViewBuilder IAnalysisBoardView => new IAnalysisBoardViewBuilder();
    }
}
