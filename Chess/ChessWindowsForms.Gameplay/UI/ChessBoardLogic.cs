using ChessWindowsForms.Model.Contracts;
using ChessWindowsForms.View.Contracts;
using ChessWindowsForms.View.UI;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ChessWindowsForms.Model.UI
{
    public class ChessBoardLogic
    {
        private IGameplay _gameplay;
        private Queue<IPlayer> _players => _gameplay.Players;
        private IAnalysisBoard _analyisBoard;
        private UserControlChessBoard _userControlChessBoard;
        private IMarkerSpawner _markerSpawner;

        public ChessBoardLogic(
            IAnalysisBoard analysisBoard,
            IGameplay gameplay,
            IMarkerSpawner spawner,
            UserControlChessBoard userControlChessBoard)
        {
            _analyisBoard = analysisBoard;
            _markerSpawner = spawner;
            _gameplay = gameplay;
            _userControlChessBoard = userControlChessBoard;

            AttachToBoard();
        }

        private void AttachToBoard()
        {
            foreach (IPlayer player in _players)
                foreach (IChessPiece piece in player.ChessPieceList)
                    _userControlChessBoard.AttachChessPieceToBoard(
                        piece.Position.Column, piece.Position.Row, piece);
        }
        public void OnDrag(object sender, MouseEventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            var list = _players.Peek().ChessPieceList;

            IChessPiece chessPiece = null;

            foreach (IChessPiece piece in list)
            {
                if (piece.PictureBox.Equals(pictureBox))
                {
                    chessPiece = piece;
                }
            }

            var markers = _markerSpawner.Spawn(chessPiece.PossiblePositions());
            _userControlChessBoard.AttachMarkersToBoard(markers);

        }
        public void OnDrop(object sender, DragEventArgs e)
        {
            Panel panel = sender as Panel;

            IChessPiece chessPiece = e.Data.GetData(e.Data.GetFormats()[0]) as IChessPiece;
            chessPiece.Show();
            _userControlChessBoard.DeleteAllMarkers();

            Position panelPosition = GetPosition(panel);
            chessPiece.MoveTo(panelPosition);
            if (chessPiece.HasMoved)
            {
                _userControlChessBoard.AttachChessPieceToBoard(panelPosition.Column, panelPosition.Row, chessPiece);
                _analyisBoard.UpdateMove(chessPiece);
                _gameplay.EndTurn();
            }
        }
        private Position GetPosition(Panel panel)
        {
            return new Position(_userControlChessBoard.GetCellPosition(panel).Column, _userControlChessBoard.GetCellPosition(panel).Row);
        }
    }
}
