using ChessWindowsForms.View.Contracts;
using ChessWindowsForms.View.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessWindowsForms.View.UI
{
    public partial class FormGameScreen : Form
    {
        private ChessBoardModel _chessBoard;
        private Queue<IPlayer> _players => _gameplay.Players;
        //private ICommandProcessor _commandProcessor;
        private IGameplay _gameplay;

        public FormGameScreen(
            //ICommandProcessor commandProcessor,
            IChessBoard chessBoard,
            IGameplay gameplay,
            IAnalysisBoardModel analysisBoard)
        {
            userControlChessBoard = chessBoard.Model;
            userControlAnalysisBoard = analysisBoard.Model;
            //_commandProcessor = commandProcessor;
            _gameplay = gameplay;
            InitializeComponent();
        }

        private void buttonUndo_Click(object sender, EventArgs e)
        {
            //_commandProcessor.Undo();
        }
    }
}
