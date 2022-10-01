namespace ChessWindowsForms.View.UI
{
    partial class UserControlAnalysisBoard
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.listViewAnalysisBoard = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listViewAnalysisBoard
            // 
            this.listViewAnalysisBoard.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewAnalysisBoard.HideSelection = false;
            this.listViewAnalysisBoard.Location = new System.Drawing.Point(8, 9);
            this.listViewAnalysisBoard.Name = "listViewAnalysisBoard";
            this.listViewAnalysisBoard.Size = new System.Drawing.Size(212, 133);
            this.listViewAnalysisBoard.TabIndex = 1;
            this.listViewAnalysisBoard.UseCompatibleStateImageBehavior = false;
            this.listViewAnalysisBoard.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Nr";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Player 1";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Player 2";
            // 
            // UserControlAnalysisBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewAnalysisBoard);
            this.Name = "UserControlAnalysisBoard";
            this.Size = new System.Drawing.Size(228, 210);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewAnalysisBoard;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}
