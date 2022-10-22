using ChessWindowsForms.Controller.UI;
using ChessWindowsForms.Model.UI;
using ChessWindowsForms.Presenter.Commands;
using ChessWindowsForms.Presenter.Factories;
using ChessWindowsForms.Presenter.Pieces;
using ChessWindowsForms.Presenter.UI;
using ChessWindowsForms.Properties;
using ChessWindowsForms.View.Contracts;
using ChessWindowsForms.View.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ChessWindowsForms.Presenter
{
    public class RootComposition
    {
        private MarkerSpawner _markerSpawner;
        private Queue<IPlayer> _players;
        private Color _colorWhitePieces = Color.White;
        private Color _colorBlackPieces = Color.Black;
        private ChessPieceSpawner _spawner;
        private CommandProcessor _commandProcessor;
        private Gameplay _gameplay;

        public Settings _settings;
        private AnalysisBoardFacade _analysisBoard;
        private ChessBoardFacade _chessBoard;

        public RootComposition()
        {
            Main();
        }

        void Main()
        {
            SetupPlayers();
            SetupSettings();
            SetupGameplay();
            SetupAnalysisBoard();
            SetupChessBoard();

            FormGameScreen gameScreen = new FormGameScreen(_chessBoard, _gameplay, _analysisBoard);

            Application.Run(gameScreen);
        }

        private void SetupPlayers()
        {
            _commandProcessor = new CommandProcessor();
            _players = new Queue<IPlayer>();

            SetupPlayers(0, _colorWhitePieces);
            SetupPlayers(7, _colorBlackPieces);
        }
        private void SetupPlayers(int row, Color color)
        {
            List<IChessPieceView> chessPieceList = new List<IChessPieceView>();

            Player player = new Player(chessPieceList, color);
            _spawner = new ChessPieceSpawner(player, _commandProcessor);
            FillChessPieceList(row, chessPieceList);

            _players.Enqueue(player);
        }
        public void FillChessPieceList(int row, List<IChessPieceView> chessPieceList)
        {
            chessPieceList.Add(SpawnChessPiece<King>(3, row, Resources.King));
            chessPieceList.Add(SpawnChessPiece<Rook>(0, row, Resources.Rook));
            chessPieceList.Add(SpawnChessPiece<Bishop>(1, row, Resources.Bishop));
            chessPieceList.Add(SpawnChessPiece<Knight>(2, row, Resources.Knight));
        }
        private T SpawnChessPiece<T>(int column, int row, Bitmap chessFigure) where T : IChessPieceView
        {
            var chessPiece = _spawner.SpawnChessPiece<T>(column, row, chessFigure);

            return chessPiece;
        }
        private void SetupSettings()
        {
            _settings = new Settings();
            _settings.GameplayHandler = new Gameplay.Settings();
            _settings.GameplayHandler.ColorWhite = _colorWhitePieces;
            _settings.GameplayHandler.ColorBlack = _colorBlackPieces;
            _settings.GameplayHandler.Turn = 1;
            _settings.GameplayHandler.Players = _players;
        }

        private void SetupGameplay()
        {
            _gameplay = new Gameplay(_settings.GameplayHandler);
            _markerSpawner = new MarkerSpawner();
        }
        private void SetupAnalysisBoard()
        {
            UserControlAnalysisBoard analysisBoardModel = new UserControlAnalysisBoard();
            AnalysisBoardLogic analysisBoardLogic = new AnalysisBoardLogic(_gameplay);
            _analysisBoard = new AnalysisBoardFacade(analysisBoardModel, analysisBoardLogic);
        }
        private void SetupChessBoard()
        {
            TableLayoutPanel panel = new TableLayoutPanel();
            ChessBoardModel chessBoardModel = new ChessBoardModel(panel);
            UserControlChessBoard userControlChessBoard = new UserControlChessBoard(_gameplay, chessBoardModel);
            ChessBoardLogic chessBoardLogic = new ChessBoardLogic(_gameplay, _markerSpawner, userControlChessBoard);
            _chessBoard = new ChessBoardFacade(_analysisBoard,userControlChessBoard, chessBoardLogic);
        }

        [Serializable]
        public class Settings
        {
            public Gameplay.Settings GameplayHandler;
        }
    }
}
