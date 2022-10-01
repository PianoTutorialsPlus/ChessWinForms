using ChessWindowsForms;
using ChessWindowsForms.View.Contracts;
using ChessWindowsForms.View.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWindowsForms.View.Contracts
{
    public interface IChessBoard
    {
        UserControlChessBoard Model { get; }
    }
}
