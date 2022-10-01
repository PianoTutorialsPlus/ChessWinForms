using ChessWindowsForms.View.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessWindowsForms.View.UI
{
    public partial class UserControlAnalysisBoard : UserControl , IAnalysisBoardView
    {
        public UserControlAnalysisBoard()
        {
            InitializeComponent();
        }

        public void AddEntry(string[] turnData)
        {
            ListViewItem personItem = new ListViewItem(turnData);

            listViewAnalysisBoard.Items.Add(personItem);
        }
        public void RemoveEntry()
        {
            if (listViewAnalysisBoard.Items.Count != 0)
                listViewAnalysisBoard.Items.RemoveAt(listViewAnalysisBoard.Items.Count - 1);
        }
    }
}
