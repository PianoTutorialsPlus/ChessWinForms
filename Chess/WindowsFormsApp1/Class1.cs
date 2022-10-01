using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
/// <summary>
/// Currently the only available Game Mode is being implemented in here.
/// </summary>
public sealed partial class CooperativeForm : Form
{
    private readonly Timer timer = new Timer();
    //variable used by the Turn Tracking Feature if checked in the Options menu.
    private static Tuple<Point, Figure, PictureBox> passedTurn;

    // tile size.
    private const int tileWidth = 85;
    private const int tileHeight = 75;

    //figure size.
    private const int figureWidth = 75;
    private const int figureHeight = 65;

    // since there's difference in the tile size and the figure size this helps place it in the middle of the tile it's left on.
    private const int marginsLeftToCenterTile = 5;

    // size of the board 8x8.
    public const int size = 8;

    // all the tiles are pictureboxs
    public static readonly PictureBox[][] DrawnBoard = new PictureBox[size][];

    //boolean board which helps with the checking of where a figure is placed ??
    public static bool[][] Board = new bool[8][];

    // all the figures are stored here.
    public static List<Figure> Figures = new List<Figure>();

    // Figure class doesn't have a PictureBox property so this is basically chained to a specific figure.
    private static readonly PictureBox[] figuresImages = new PictureBox[32];

    // boolean variable to determine wheter the player is dragging a figure.
    private bool isMoved;

    // variable to keep track of the current figure it's updated whenever a figure is being pressed not neccessary to be moved.
    private Figure currentFigure;

    // if the player's movement was unavailable those variables will help return it to it's previous place
    private int previousX;
    private int previousY;

    // this variable keeps track of the current player
    private static FigureColor currentColor = FigureColor.White;

    // keeping track of the current turn
    private static int currentTurn;

    // white and black figures are stored here
    public static readonly List<Figure> WhiteFigures = new List<Figure>();
    public static readonly List<Figure> BlackFigures = new List<Figure>();

    // variables converting the const figure and tile size's into Size variables.
    private static readonly Size figureSize = new Size(figureWidth, figureHeight);
    private static readonly Size tileSize = new Size(tileWidth, tileHeight);

    // taking the user's input for the time length from the resources.
    private readonly uint timerMaxValue = Properties.Settings.Default.TurnTimer;

    // pictureBox which acts as a Board it's currently not useful but will help with future updates
    // because currently figures back color isn't perfect they are not completely transparent they take
    // either black or white depending on what they are standing on. Not supporting if in between.
    private PictureBox parentBoardPictureBox = new PictureBox();

    // variable to keep track of the Turn Helper's highlighted tiles which must be set back to normal color
    // whenever the figure is released.
    private readonly List<Tuple<int, int, Color>> highlightedTiles = new List<Tuple<int, int, Color>>();

    // variables to keep track of the white and black turns which are used by the Turn Tracking Option
    // if checked in the Options Menu.
    private readonly List<List<object>> whiteTurns = new List<List<object>>();
    private readonly List<List<object>> blackTurns = new List<List<object>>();
    private ListView TurnTrackingList = new ListView();
    private static readonly char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

    /// <summary>
    /// Reduces the flickering caused by dragging a figure/PictureBox.
    /// </summary>
    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= 0x02000000;
            return cp;
        }
    }

    public CooperativeForm()
    {
        InitializeComponent();
        BackColor = Color.SaddleBrown;
        FillBoard();
        DrawBoard();
        DrawLines();

        LoadAllFigures();

        if (Properties.Settings.Default.EnabledTurnTimers)
        {
            pbTimer.Visible = true;
            pbTimer.Maximum = (int)timerMaxValue;
            pbTimer.Value = pbTimer.Maximum;
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
            timer.Start();
        }
    }

    private void timer_Tick(object sender, EventArgs e)
    {
        pbTimer.Value--;
        if (pbTimer.Value <= 0)
        {
            pbTimer.Value = (int)timerMaxValue;
            currentColor = currentColor == FigureColor.Black
                ? FigureColor.White
                : FigureColor.Black;
        }
    }

    /// <summary> Method to load and draw all the figures using several other methods </summary>
    private void LoadAllFigures()
    {
        if (Properties.Settings.Default.EnabledTurnTracking)
        {
            flpBlackPlayerWinnings.Height /= 2;
            flpWhitePlayerWinnings.Height /= 2;
            flpWhitePlayerWinnings.Location = new Point(flpWhitePlayerWinnings.Location.X, flpBlackPlayerWinnings.Location.Y + flpBlackPlayerWinnings.Height + 10);
            EnableTurnTracking();
        }
        if (Figures.Count > 0)
        {
            Restart();
        }
        else
        {
            AddKings();
            AddQueens();
            AddRooks();
            AddBishops();
            AddKnights();
            AddPawns();

            DrawFigures();
        }
    }

    // Methods to add the figures to each team </summary>
    private static void AddPawns()
    {
        IEnumerable<FigureDefinition> blackDefinitions = new PawnDefinitions().BlackPawns;
        IEnumerable<FigureDefinition> whiteDefinitions = new PawnDefinitions().WhitePawns;

        AddPawnsByColor(whiteDefinitions);
        AddPawnsByColor(blackDefinitions);
    }
    private static void AddPawnsByColor(IEnumerable<FigureDefinition> definitions)
    {
        foreach (PawnPiece piece in definitions.Select(definition => new PawnPiece(definition)))
        {
            AddFigure(piece);
        }
    }

    private static void AddKnights()
    {
        IEnumerable<FigureDefinition> blackDefinitions = new KnightDefinitions().BlackKnights;
        IEnumerable<FigureDefinition> whiteDefinitions = new KnightDefinitions().WhiteKnights;

        AddKnightsByColor(whiteDefinitions);
        AddKnightsByColor(blackDefinitions);
    }
    private static void AddKnightsByColor(IEnumerable<FigureDefinition> definitions)
    {
        foreach (KnightPiece piece in definitions.Select(definition => new KnightPiece(definition)))
        {
            AddFigure(piece);
        }
    }

    private static void AddBishops()
    {
        IEnumerable<FigureDefinition> blackDefinitions = new BishopDefinitions().BlackBishops;
        IEnumerable<FigureDefinition> whiteDefinitions = new BishopDefinitions().WhiteBishops;

        AddBishopsByColor(whiteDefinitions);
        AddBishopsByColor(blackDefinitions);
    }
    private static void AddBishopsByColor(IEnumerable<FigureDefinition> definitions)
    {
        foreach (BishopPiece piece in definitions.Select(definition => new BishopPiece(definition)))
        {
            AddFigure(piece);
        }
    }

    private static void AddRooks()
    {
        IEnumerable<FigureDefinition> blackDefinitions = new TopDefinitions().BlackRooks;
        IEnumerable<FigureDefinition> whiteDefinitions = new TopDefinitions().WhiteRooks;

        AddRooksByColor(whiteDefinitions);
        AddRooksByColor(blackDefinitions);
    }
    private static void AddRooksByColor(IEnumerable<FigureDefinition> definitions)
    {
        foreach (RookPiece piece in definitions.Select(definition => new RookPiece(definition)))
        {
            AddFigure(piece);
        }
    }

    private static void AddQueens()
    {
        IEnumerable<FigureDefinition> blackDefinitions = new QueenDefinitions().BlackQueens;
        IEnumerable<FigureDefinition> whiteDefinitions = new QueenDefinitions().WhiteQueens;

        AddQueensByColor(whiteDefinitions);
        AddQueensByColor(blackDefinitions);
    }
    private static void AddQueensByColor(IEnumerable<FigureDefinition> definitions)
    {
        foreach (QueenPiece piece in definitions.Select(definition => new QueenPiece(definition)))
        {
            AddFigure(piece);
        }
    }

    private static void AddKings()
    {
        IEnumerable<FigureDefinition> blackDefinitions = new KingDefinitions().BlackKings;
        IEnumerable<FigureDefinition> whiteDefinitions = new KingDefinitions().WhiteKings;

        AddKingsByColor(whiteDefinitions);
        AddKingsByColor(blackDefinitions);
    }
    private static void AddKingsByColor(IEnumerable<FigureDefinition> definitions)
    {
        foreach (KingPiece piece in definitions.Select(definition => new KingPiece(definition)))
        {
            AddFigure(piece);
        }
    }

    /// <summary> Method to add figure to the  
    /// <seealso cref="Figures"/>
    /// List </summary>
    private static void AddFigure(Figure newFigure)
    {
        Figures.Add(newFigure);
        AddToTeam(newFigure);
    }

    // Methods to add/remove figure/figures to teams

    private static void AddToTeam(Figure newFigure)
    {
        if (newFigure.PieceColor == FigureColor.Black)
        {
            BlackFigures.Add(newFigure);
        }
        else
        {
            WhiteFigures.Add(newFigure);
        }
    }
    private static void AddMultipleFiguresToTeam(params Figure[] inputFigures)
    {
        foreach (var inputFigure in inputFigures)
        {
            AddToTeam(inputFigure);
        }
    }
    private static void RemoveFromTeam(Figure inputFigure)
    {
        if (inputFigure.PieceColor == FigureColor.Black)
        {
            BlackFigures.Remove(inputFigure);
        }
        else
        {
            WhiteFigures.Remove(inputFigure);
        }
    }
    private static void RemoveMultipleFiguresFromTeam(params Figure[] inputFigures)
    {
        foreach (var inputFigure in inputFigures)
        {
            RemoveFromTeam(inputFigure);
        }
    }
    private static void RemoveMultipleFiguresFromFiguresList(params Figure[] inputFigures)
    {
        foreach (var inputFigure in inputFigures)
        {
            Figures.Remove(inputFigure);
        }
    }
    private static void AddMultipleFiguresToFiguresList(params Figure[] inputFigures)
    {
        foreach (var inputFigure in inputFigures)
        {
            Figures.Add(inputFigure);
        }
    }

    private void DrawFigures()
    {
        for (int i = 0; i < Figures.Count; i++)
        {
            int x = Figures[i].StartingPosition.Item1;
            int y = Figures[i].StartingPosition.Item2;
            LoadFigure(i, x, y);
        }
    }
    private void LoadFigure(int i, int x, int y)
    {
        figuresImages[i] = new PictureBox
        {
            Size = figureSize,
            BackgroundImage = Figures[i].PieceImage,
            BackgroundImageLayout = ImageLayout.Stretch,
            AllowDrop = true,
            Location =
                new Point(DrawnBoard[x][y].Location.X + marginsLeftToCenterTile,
                    DrawnBoard[x][y].Location.Y + marginsLeftToCenterTile),
            BackColor = DrawnBoard[x][y].BackColor
        };
        figuresImages[i].MouseDown += Figure_MouseDown;
        figuresImages[i].MouseMove += Figure_MouseMove;
        figuresImages[i].MouseUp += Figure_MouseUp;
        parentBoardPictureBox.Controls.Add(figuresImages[i]);
        figuresImages[i].BringToFront();
    }

    // Creating the Board's lines, rows and columns 

    private void DrawLines()
    {
        DrawLineNumbers();
        DrawLineAlphabet();
    }
    private void DrawLineNumbers()
    {
        const int horizontal = 5;
        int vertical = tileHeight;
        for (int i = 0; i < size; i++)
        {
            Label nextLabel = new Label
            {
                AutoSize = true,
                Location = new Point(horizontal, vertical),
                Text = (i + 1).ToString(),
                Font = new Font("Microsoft Sans Serif", 16),
            };
            vertical += tileHeight;
            Controls.Add(nextLabel);
        }
    }
    private void DrawLineAlphabet()
    {
        const int vertical = tileHeight * 9 - 10;
        int horizontal = tileWidth - 15;
        for (int i = 0; i < size; i++)
        {
            Label nextLabel = new Label
            {
                AutoSize = true,
                Location = new Point(horizontal, vertical),
                Text = letters[i].ToString(),
                Font = new Font("Microsoft Sans Serif", 16),
            };
            horizontal += tileWidth;
            Controls.Add(nextLabel);
        }
    }
    private static void FillBoard()
    {
        for (int i = 0; i < size; i++)
        {
            bool[] row = new bool[size];
            bool value = i == 0 || i == 1 || i == 6 || i == 7;
            for (int j = 0; j < size; j++)
            {
                row[j] = value;
            }
            Board[i] = row;
        }
    }
    private void DrawBoard()
    {
        const int startHorizontal = 40;
        const int startVertical = 580;
        parentBoardPictureBox = new PictureBox
        {
            Location = new Point(startHorizontal, startVertical - 7 * tileHeight),
            Size = new Size(8 * tileWidth, 8 * tileHeight),
        };
        const int startHorizontalTile = 0;
        const int startVerticalTile = 7 * tileHeight;
        Controls.Add(parentBoardPictureBox);
        int horizontal = startHorizontalTile;
        int vertical = startVerticalTile;
        bool white = false;
        for (int i = 0; i < DrawnBoard.Length; i++)
        {
            DrawnBoard[i] = CreateBoardRow(ref horizontal, ref vertical, ref white);
            white = !white;
            vertical -= tileHeight;
            horizontal = startHorizontalTile;
        }
    }
    private PictureBox[] CreateBoardRow(ref int horizontal, ref int vertical, ref bool white)
    {
        PictureBox[] Row = new PictureBox[size];
        for (int i = 0; i < Row.Length; i++)
        {
            Row[i] = new PictureBox
            {
                Size = tileSize,
                Location = new Point(horizontal, vertical),
                BorderStyle = BorderStyle.Fixed3D,
            };
            if (white)
            {
                white = false;
                Row[i].BackColor = Color.White;
            }
            else
            {
                white = true;
                Row[i].BackColor = Color.Black;
            }
            parentBoardPictureBox.Controls.Add(Row[i]);
            horizontal += tileWidth;
        }
        return Row;
    }

    //Mouse events handling the movement of the figures and validates the figure's movement

    private void Figure_MouseDown(object sender, MouseEventArgs e)
    {
        PictureBox thisPB = (PictureBox)sender;
        isMoved = true;
        previousX = thisPB.Location.X;
        previousY = thisPB.Location.Y;
        int newColumn;
        int newRow = GetNewTile(thisPB, out newColumn);
        if (Properties.Settings.Default.EnabledTurnHelper)
        {
            Figure tempFigure = GetFigure(thisPB);
            if (tempFigure.PieceColor == currentColor)
            {
                tempFigure.UpdateMoves(tempFigure);
                foreach (Tuple<int, int> move in tempFigure.Moves)
                {
                    highlightedTiles.Add(new Tuple<int, int, Color>(move.Item1, move.Item2,
                        DrawnBoard[move.Item1][move.Item2].BackColor));
                    DrawnBoard[move.Item1][move.Item2].BackColor = Color.LightGreen;
                }
            }
        }
        passedTurn = new Tuple<Point, Figure, PictureBox>(new Point(previousX, previousY), GetFigure(thisPB), thisPB);
    }
    private void Figure_MouseMove(object sender, MouseEventArgs e)
    {
        if (isMoved)
        {
            PictureBox thisPB = (PictureBox)sender;
            thisPB.BringToFront();
            var temp = GetFigure(thisPB);
            if (temp.PieceColor == currentColor)
            {
                currentFigure = GetFigure(thisPB);
                thisPB.Location = new Point(thisPB.Left + e.X, thisPB.Top + e.Y);
                thisPB.Refresh();
            }
        }
    }
    private void Figure_MouseUp(object sender, MouseEventArgs e)
    {
        foreach (var highlightedTile in highlightedTiles)
        {
            DrawnBoard[highlightedTile.Item1][highlightedTile.Item2].BackColor = highlightedTile.Item3;
        }
        highlightedTiles.Clear();
        isMoved = false;
        KingIsDefeated();
        if (currentFigure == null) return;
        PictureBox thisPB = (PictureBox)sender;
        int newColumn;
        int newRow = GetNewTile(thisPB, out newColumn);
        if (MustReplacePawn(currentFigure, new Tuple<int, int>(newRow, newColumn)))
        {
            DoPawnReplaceMove(newRow, newColumn, thisPB);
            return;
        }
        Rochade rochade = new Rochade();
        Rochade.newKingMove = new Tuple<int, int>(newRow, newColumn);
        rochade.DoRochade(currentFigure);
        if (rochade.RochadeKing != null && rochade.RochadeKing.PieceColor == currentColor) // rochade succed
        {
            DoRochadeMove(rochade, thisPB);
            return;
        }

        currentFigure.UpdateMoves(currentFigure);
        if (currentFigure.IsValidMove(new Tuple<int, int>(newRow, newColumn)) &&
            !currentFigure.WillExposeKing(currentFigure, new Tuple<int, int>(newRow, newColumn)))
        {
            DoStandartMove(newRow, newColumn, thisPB);
        }
        else
        {
            thisPB.Location = new Point(previousX, previousY);
        }
        Color tempTileColor = GetTile(currentFigure).BackColor;
        thisPB.BackColor = tempTileColor;
        currentFigure = null;
        KingIsDefeated();
    }

    // All the different possible moves are being checked here


    private void KingIsDefeated()
    {
        Figure blackKing = BlackFigures.Single(blackFigure => blackFigure.PieceType == FigureType.King);
        Figure whiteKing = WhiteFigures.Single(whiteFigure => whiteFigure.PieceType == FigureType.King);
        int count = blackKing.Moves.Count(blackKingMove => blackKing.WillExposeKing(blackKing, blackKingMove));
        if (count == blackKing.Moves.Count && blackKing.Moves.Count > 0)
        {
            WinningCondition(blackKing);
        }
        count = whiteKing.Moves.Count(whiteKingMove => whiteKing.WillExposeKing(whiteKing, whiteKingMove));
        if (count == whiteKing.Moves.Count && whiteKing.Moves.Count > 0)
        {
            WinningCondition(whiteKing);
        }
    }

    private void DoRochadeMove(Rochade rochade, Control thisPB)
    {
        PassedTurns passedTurns = new PassedTurns();
        UpdateTimerValue();
        List<Figure> figureList = currentFigure.PieceColor == FigureColor.Black
            ? BlackFigures
            : WhiteFigures;
        Figure currentKing = figureList.Single(x => x.PieceType == FigureType.King);
        Figure currentRook = figureList.
            Single(x => x.PieceType == FigureType.Rook &&
                        Equals(x.StartingPosition, rochade.RochadeRook.StartingPosition));
        PictureBox rookPb = GetFigureTile(currentRook);
        RemoveMultipleFiguresFromTeam(currentKing, currentRook);
        RemoveMultipleFiguresFromFiguresList(currentKing, currentRook);
        currentKing = rochade.RochadeKing;
        currentRook = rochade.RochadeRook;
        AddMultipleFiguresToFiguresList(currentKing, currentRook);
        AddMultipleFiguresToTeam(currentKing, currentRook);
        currentKing.UpdateMoves(currentKing);
        currentRook.UpdateMoves(currentRook);
        rookPb.Location =
            new Point(
                DrawnBoard[currentRook.CurrentPosition.Item1][currentRook.CurrentPosition.Item2].Location.X +
                marginsLeftToCenterTile,
                DrawnBoard[currentRook.CurrentPosition.Item1][currentRook.CurrentPosition.Item2].Location.Y +
                marginsLeftToCenterTile);
        UpdateBoard();
        passedTurn = null;
        currentColor = currentColor == FigureColor.Black
            ? FigureColor.White
            : FigureColor.Black;
        currentFigure = currentKing;
        thisPB.Location =
            new Point(
                DrawnBoard[currentKing.CurrentPosition.Item1][currentKing.CurrentPosition.Item2].Location.X +
                marginsLeftToCenterTile,
                DrawnBoard[currentKing.CurrentPosition.Item1][currentKing.CurrentPosition.Item2].Location.Y +
                marginsLeftToCenterTile);
        currentTurn++;
        UpdateTurnTracking(passedTurns, currentFigure, "Rochade");
    }
    private void DoPawnReplaceMove(int newRow, int newColumn, Control thisPB)
    {
        PassedTurns passedTurns = new PassedTurns();
        UpdateTimerValue();
        currentFigure.CurrentPosition = new Tuple<int, int>(newRow, newColumn);
        thisPB.Location = new Point(DrawnBoard[newRow][newColumn].Location.X + marginsLeftToCenterTile,
            DrawnBoard[newRow][newColumn].Location.Y + marginsLeftToCenterTile);
        RemoveFromTeam(currentFigure);
        Figures.Remove(currentFigure);
        currentFigure = ReplacePawn(currentFigure, new Tuple<int, int>(newRow, newColumn), thisPB);
        AddToTeam(currentFigure);
        Figures.Add(currentFigure);
        currentFigure.UpdateMoves(currentFigure);
        passedTurn = null;
        UpdateBoard();
        currentTurn++;
        UpdateAllFiguresMoves();
        UpdateTurnTracking(passedTurns, currentFigure, "Replaced Pawn");
    }
    private void DoStandartMove(int newRow, int newColumn, Control thisPB)
    {
        PassedTurns passedTurns = new PassedTurns();
        UpdateTimerValue();
        currentFigure.WasMoved = true;
        currentFigure.CurrentPosition = new Tuple<int, int>(newRow, newColumn);
        thisPB.Location = new Point(DrawnBoard[newRow][newColumn].Location.X + marginsLeftToCenterTile,
            DrawnBoard[newRow][newColumn].Location.Y + marginsLeftToCenterTile);
        currentColor = currentColor == FigureColor.Black
            ? FigureColor.White
            : FigureColor.Black;
        string tempAction;
        if (currentFigure.WillCollideWithEnemy(currentFigure.CurrentPosition, currentFigure.PieceColor).Item1)
        {
            RemoveFigure(currentFigure);
            tempAction = "Took a Figure";
        }
        else
        {
            tempAction = "Normal";
        }
        UpdateBoard();
        currentTurn++;
        UpdateAllFiguresMoves();
        UpdateTurnTracking(passedTurns, currentFigure, tempAction);
    }

    // Checks and enables the timer if checked in the options

    private void UpdateTimerValue()
    {
        if (Properties.Settings.Default.EnabledTurnTimers)
        {
            pbTimer.Value = (int)timerMaxValue;
        }
    }

    /// <summary>
    /// Updates the Board's boolean array
    /// <seealso cref="Board"/>
    /// </summary>
    private static void UpdateBoard()
    {
        foreach (bool[] row in Board)
        {
            for (int j = 0; j < Board.Length; j++)
            {
                row[j] = false;
            }
        }
        for (int i = 0; i < Board.Length; i++)
        {
            for (int j = 0; j < Board.Length; j++)
            {
                if (figuresImages.TakeWhile(figureImage => figureImage != null)
                        .Any(figureImage => DrawnBoard[i][j].Location == figureImage.Location))
                {
                    Board[i][j] = true;
                }
            }
        }
    }

    /// <summary>
    /// Method to determine where the player has dropped his figure located in the
    /// <seealso cref="DrawnBoard"/>
    /// </summary>
    private static int GetNewTile(Control thisPB, out int newColumn)
    {
        int newRow = 0;
        newColumn = 0;
        int closestX = int.MaxValue;
        int closestY = int.MaxValue;
        for (int i = 0; i < DrawnBoard.Length; i++)
        {
            for (int j = 0; j < DrawnBoard.Length; j++)
            {
                int horizontalDifference = thisPB.Location.X - DrawnBoard[i][j].Location.X;
                int verticalDifference = thisPB.Location.Y - DrawnBoard[i][j].Location.Y;
                if (horizontalDifference <= closestX && verticalDifference <= closestY && horizontalDifference > -1 &&
                    verticalDifference > -1)
                {
                    closestX = thisPB.Location.X - DrawnBoard[i][j].Location.X;
                    closestY = thisPB.Location.Y - DrawnBoard[i][j].Location.Y;
                    newRow = i;
                    newColumn = j;
                }
            }
        }
        return newRow;
    }

    /// <summary>
    /// Method to determine which figure is being moved by comparing it's image to the ones in 
    /// <seealso cref="figuresImages"/>
    /// </summary>
    private static Figure GetFigure(Control sender)
    {
        return (from figuresImage in figuresImages.TakeWhile(figuresImage => figuresImage != null)
                where figuresImage.Location.X == sender.Location.X && figuresImage.Location.Y == sender.Location.Y
                from figure1 in Figures
                where Equals(figure1.PieceImage, figuresImage.BackgroundImage)
                select figure1).FirstOrDefault();
    }

    /// <summary>
    /// Method returning the current tile that the figure was placed on
    /// </summary>
    private static PictureBox GetTile(Figure inputCurrentFigure)
    {
        return DrawnBoard[inputCurrentFigure.CurrentPosition.Item1][inputCurrentFigure.CurrentPosition.Item2];
    }

    /// <summary>
    /// Method returning the current tile that the figure is standing on
    /// </summary>
    private static PictureBox GetFigureTile(Figure inputCurrentFigure)
    {
        return
            figuresImages.TakeWhile(figuresImage => figuresImage != null)
                .FirstOrDefault(
                    figuresImage =>
                        figuresImage.Location.X == GetTile(inputCurrentFigure).Location.X + marginsLeftToCenterTile &&
                        figuresImage.Location.Y == GetTile(inputCurrentFigure).Location.Y + marginsLeftToCenterTile &&
                        Equals(figuresImage.BackgroundImage, inputCurrentFigure.PieceImage));
    }

    //this button is currently disabled usually it rerolls the last turn played 
    //if a figure is removed the turn cant be undone
    private void bUndo_Click(object sender, EventArgs e)
    {
        if (passedTurn == null || currentTurn == 0)
        {
            return;
        }
        TurnTrackingList.Items[TurnTrackingList.Items.Count - 1].Remove();
        Tuple<Point, Figure, PictureBox> previous = passedTurn;
        previous.Item3.Location = previous.Item1;
        int newColumn;
        int newRow = GetNewTile(previous.Item3, out newColumn);
        previous.Item2.CurrentPosition = new Tuple<int, int>(newRow, newColumn);
        currentColor = currentColor == FigureColor.Black
            ? FigureColor.White
            : FigureColor.Black;
        if (Equals(previous.Item2.CurrentPosition, previous.Item2.StartingPosition))
        {
            previous.Item2.WasMoved = false;
        }
        UpdateBoard();
        UpdateAllFiguresMoves();
        currentTurn--;
    }

    /// <summary>
    /// Method removing figures also checking if the removed figure is king if so the game ends.
    /// </summary>
    private void RemoveFigure(Figure inputCurrentFigure)
    {
        Tuple<int, int> turnRoute = inputCurrentFigure.CurrentPosition;

        Figure enemyFigure = inputCurrentFigure.WillCollideWithEnemy(turnRoute, inputCurrentFigure.PieceColor).Item2;
        PictureBox enemyFigurePictureBox = GetFigureTile(enemyFigure);
        enemyFigurePictureBox.MouseMove -= Figure_MouseMove;
        enemyFigurePictureBox.MouseDown -= Figure_MouseDown;
        enemyFigurePictureBox.MouseUp -= Figure_MouseUp;
        enemyFigurePictureBox.BackColor = Color.White;
        if (Properties.Settings.Default.EnabledTurnTracking)
        {
            enemyFigurePictureBox.Width -= enemyFigurePictureBox.Width / 3;
            enemyFigurePictureBox.Height -= enemyFigurePictureBox.Height / 3;
        }
        if (inputCurrentFigure.PieceColor == FigureColor.Black)
        {
            flpBlackPlayerWinnings.Controls.Add(enemyFigurePictureBox);
            WhiteFigures.Remove(enemyFigure);
        }
        else
        {
            flpWhitePlayerWinnings.Controls.Add(enemyFigurePictureBox);
            BlackFigures.Remove(enemyFigure);
        }
        if (enemyFigure.PieceType == FigureType.King)
        {
            WinningCondition(enemyFigure);
            return;
        }
        inputCurrentFigure.WillCollideWithEnemy(turnRoute, inputCurrentFigure.PieceColor).Item2.CurrentPosition =
            new Tuple<int, int>(-1, -1);
        passedTurn = null;

    }

    private void WinningCondition(Figure enemyFigure)
    {
        const string replayText = @"Would You like to play again ?";
        string winningText = @"White Player Wins !";
        if (enemyFigure.PieceColor == FigureColor.White)
        {
            winningText = @"Black Player Wins !";
        }
        DialogResult replayDialog = MessageBox.Show(winningText + Environment.NewLine + replayText,
            @"Congratulations", MessageBoxButtons.YesNo);
        if (replayDialog == DialogResult.Yes)
        {
            Restart();
        }
        else
        {
            DialogResult exitApplicationDialog =
                MessageBox.Show(@"You are about to exit the application are you sure ?", @"Quit",
                    MessageBoxButtons.YesNo);
            if (exitApplicationDialog == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                Close();
            }
        }
    }
    private void Restart()
    {
        passedTurn = null;
        Figures.Clear();
        for (int i = 0; i < figuresImages.Length; i++)
        {
            if (figuresImages[i] == null)
            {
                break;
            }
            figuresImages[i].Dispose();
            figuresImages[i] = null;
        }
        isMoved = false;
        currentFigure = null;
        previousX = 0;
        previousY = 0;
        currentColor = currentColor == FigureColor.Black
            ? FigureColor.White
            : FigureColor.Black;
        currentTurn = 0;
        WhiteFigures.Clear();
        BlackFigures.Clear();

        AddKings();
        AddQueens();
        AddRooks();
        AddBishops();
        AddKnights();
        AddPawns();

        DrawFigures();
    }

    /// <summary>
    /// Whenever one figure is moved all the other figures moves also change so this is called whenever a figure is being moved
    /// </summary>
    private static void UpdateAllFiguresMoves()
    {
        foreach (var figure in Figures)
        {
            figure.UpdateMoves(figure);
        }
    }

    /// <summary>
    /// Method to replace the pawn with selected figure from 
    /// <seealso cref="ReplacePawnForm"/>
    /// </summary>
    private static Figure ReplacePawn(Figure currentFigure, Tuple<int, int> newMove, Control pb)
    {
        ReplacePawnForm replacedPawnForm = new ReplacePawnForm(currentFigure.PieceColor, newMove);
        replacedPawnForm.ShowDialog();
        currentFigure = replacedPawnForm.ReplacedFigure;
        pb.BackgroundImage = currentFigure.PieceImage;
        return currentFigure;
    }

    /// <summary>
    /// Method that determines if a pawn must be replaced i.e it has reached the other end of the board
    /// </summary>
    private static bool MustReplacePawn(Figure currentFigure, Tuple<int, int> newMove)
    {
        if (currentFigure.PieceType != FigureType.Pawn || !currentFigure.Moves.Contains(newMove)) return false;
        if (currentFigure.PieceColor == FigureColor.Black)
        {
            if (newMove.Item1 == 0)
            {
                return true;
            }
        }
        else
        {
            if (newMove.Item1 == 7)
            {
                return true;
            }
        }
        return false;
    }

    //Methods to enable and update turn tracking if it was checked in Options
    private void EnableTurnTracking()
    {
        TurnTrackingList = new ListView
        {
            View = View.Details,
            GridLines = true,
            Location = new Point(flpWhitePlayerWinnings.Location.X, flpWhitePlayerWinnings.Location.Y + flpWhitePlayerWinnings.Height + 40),
            Size = new Size(flpWhitePlayerWinnings.Width, flpWhitePlayerWinnings.Height * 2 - 30),
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = Color.White
        };
        TurnTrackingList.Columns.Add("Turn", -2, HorizontalAlignment.Left);
        TurnTrackingList.Columns.Add("White Player", -2, HorizontalAlignment.Left);
        TurnTrackingList.Columns.Add("Black Player", -2, HorizontalAlignment.Left);
        TurnTrackingList.Columns.Add("Piece Type", -2, HorizontalAlignment.Left);
        TurnTrackingList.Columns.Add("Action", -2, HorizontalAlignment.Left);
        TurnTrackingList.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
        Controls.Add(TurnTrackingList);
    }
    private void UpdateTurnTrackingList(FigureColor figureColor)
    {
        int actualTurn = currentTurn % 2 == 0 ? currentTurn / 2 : currentTurn / 2 + 1;
        ListViewItem turnTrackingRow = new ListViewItem(actualTurn.ToString(), 0); // first item only here
        object[] turnTrackingRowItems = new object[4];
        if (figureColor == FigureColor.Black)
        {
            Tuple<int, int> tempPosition = (Tuple<int, int>)blackTurns[actualTurn - 1][(int)PassedTurns.ItemsOrder.Position];
            int x = tempPosition.Item2;
            int y = size - tempPosition.Item1;
            string formatedPosition = letters[x] + " , " + y;
            turnTrackingRowItems[(int)PassedTurns.ListsOrder.WhitePlayer] = string.Empty;
            turnTrackingRowItems[(int)PassedTurns.ListsOrder.BlackPlayer] = formatedPosition;
            turnTrackingRowItems[(int)PassedTurns.ListsOrder.PieceType] =
                blackTurns[actualTurn - 1][(int)PassedTurns.ItemsOrder.PieceType];
            turnTrackingRowItems[(int)PassedTurns.ListsOrder.Action] =
                blackTurns[actualTurn - 1][(int)PassedTurns.ItemsOrder.Action];
        }
        else
        {
            Tuple<int, int> tempPosition = (Tuple<int, int>)whiteTurns[actualTurn - 1][(int)PassedTurns.ItemsOrder.Position];
            int x = tempPosition.Item2;
            int y = size - tempPosition.Item1;
            string formatedPosition = letters[x] + " , " + y;
            turnTrackingRowItems[(int)PassedTurns.ListsOrder.BlackPlayer] = string.Empty;
            turnTrackingRowItems[(int)PassedTurns.ListsOrder.WhitePlayer] = formatedPosition;
            turnTrackingRowItems[(int)PassedTurns.ListsOrder.PieceType] =
                whiteTurns[actualTurn - 1][(int)PassedTurns.ItemsOrder.PieceType];
            turnTrackingRowItems[(int)PassedTurns.ListsOrder.Action] =
                whiteTurns[actualTurn - 1][(int)PassedTurns.ItemsOrder.Action];
        }
        foreach (object item in turnTrackingRowItems)
        {
            turnTrackingRow.SubItems.Add(item.ToString());
        }
        TurnTrackingList.Items.AddRange(new[] { turnTrackingRow });
    }
    private void UpdateTurnTracking(PassedTurns passedTurns, Figure inputFigure, string inputAction)
    {
        passedTurns.AddNewMove(inputFigure.CurrentPosition, inputFigure.PieceType, inputAction);
        if (inputFigure.PieceColor == FigureColor.Black)
        {
            blackTurns.Add(passedTurns.GetPassedTurns());
        }
        else
        {
            whiteTurns.Add(passedTurns.GetPassedTurns());
        }
        UpdateTurnTrackingList(inputFigure.PieceColor);
    }
}
