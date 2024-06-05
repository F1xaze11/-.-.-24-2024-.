using System.Collections.Generic;
using System.Windows.Forms;

namespace Chees
{
    partial class Board : Form
    {
        public int Rows = 8;
        public int Columns = 8;
        public bool _check;
        private Piece[,] _piecesInPlay;
        private PictureBox[,] _pictureBoxes;
        private Position _origin;
        private Color _currentPlayer;
        private int _turn;
        private int _whiteCapturedQuantity;
        private int _blackCapturedQuantity;
        public Board()
        {
            InitializeComponent();
            _piecesInPlay = new Piece[Rows, Columns];
            _pictureBoxes = new PictureBox[8, 8] {
                { pictureBoxA1, pictureBoxA2, pictureBoxA3, pictureBoxA4, pictureBoxA5, pictureBoxA6, pictureBoxA7, pictureBoxA8 },
                { pictureBoxB1, pictureBoxB2, pictureBoxB3, pictureBoxB4, pictureBoxB5, pictureBoxB6, pictureBoxB7, pictureBoxB8 },
                { pictureBoxC1, pictureBoxC2, pictureBoxC3, pictureBoxC4, pictureBoxC5, pictureBoxC6, pictureBoxC7, pictureBoxC8 },
                { pictureBoxD1, pictureBoxD2, pictureBoxD3, pictureBoxD4, pictureBoxD5, pictureBoxD6, pictureBoxD7, pictureBoxD8 },
                { pictureBoxE1, pictureBoxE2, pictureBoxE3, pictureBoxE4, pictureBoxE5, pictureBoxE6, pictureBoxE7, pictureBoxE8 },
                { pictureBoxF1, pictureBoxF2, pictureBoxF3, pictureBoxF4, pictureBoxF5, pictureBoxF6, pictureBoxF7, pictureBoxF8 },
                { pictureBoxG1, pictureBoxG2, pictureBoxG3, pictureBoxG4, pictureBoxG5, pictureBoxG6, pictureBoxG7, pictureBoxG8 },
                { pictureBoxH1, pictureBoxH2, pictureBoxH3, pictureBoxH4, pictureBoxH5, pictureBoxH6, pictureBoxH7, pictureBoxH8 }
            };
            InitializeNewGame();
        }
        private void InitializeNewGame()
        {
            _check = false;
            _whiteCapturedQuantity = 0;
            _blackCapturedQuantity = 0;
            _turn = 1;
            _currentPlayer = Color.White;
            _origin = null;
            UpdateLabels();
            InitializeBoard();
        }

        private void SwitchPlayer()
        {
            _currentPlayer = OpponentColor(_currentPlayer);
        }

        private Color OpponentColor(Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }

        public Piece GetPiece(int row, int column)
        {
            return _piecesInPlay[row, column];
        }
        public Piece GetPiece(Position position)
        {
            return _piecesInPlay[position.Row, position.Column];
        }
        public Piece RemovePiece(Position position)
        {
            if (!PieceExists(position))
            {
                return null;
            }
            Piece aux = GetPiece(position);
            aux.CurrentPosition = null;
            _piecesInPlay[position.Row, position.Column] = null;
            _pictureBoxes[position.Row, position.Column].Image = null;
            return aux;
        }
        public Piece ExecuteMove(Position origin, Position destination)
        {
            Piece piece = RemovePiece(origin);
            piece.IncrementNumberOfMoves();
            Piece capturedPiece = RemovePiece(destination);
            PlacePiece(piece, destination);
            if (piece is Pawn &&
                ((piece.PieceColor == Color.White && destination.Row == 0) ||
                (piece.PieceColor == Color.Black && destination.Row == 7)))
            {
                RemovePiece(destination);
                PlacePiece(new Queen(this, piece.PieceColor), destination);
            }
            if (piece is King && destination.Column == origin.Column + 2)
            {
                Piece rook = RemovePiece(new Position(origin.Row, origin.Column + 3));
                rook.IncrementNumberOfMoves();
                PlacePiece(rook, new Position(origin.Row, origin.Column + 1));
            }
            if (piece is King && destination.Column == origin.Column - 2)
            {
                Piece rook = RemovePiece(new Position(origin.Row, origin.Column - 4));
                rook.IncrementNumberOfMoves();
                PlacePiece(rook, new Position(origin.Row, origin.Column - 1));
            }
            if (piece is Pawn && origin.Column != destination.Column && capturedPiece == null)
            {
                Position pawnPosition = new Position(piece.PieceColor == Color.White ? destination.Row + 1 : destination.Row - 1, destination.Column);
                capturedPiece = RemovePiece(pawnPosition);
            }
            if (capturedPiece != null)
            {
                _ = capturedPiece.PieceColor == Color.White ? _whiteCapturedQuantity++ : _blackCapturedQuantity++;
            }

            return capturedPiece;
        }

        public HashSet<Piece> PiecesInPlay(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in _piecesInPlay)
            {
                if (piece != null && piece.PieceColor == color)
                {
                    aux.Add(piece);
                }
            }
            return aux;
        }

        private Position GetKingPosition(Color color)
        {
            for (int column = 0; column < Columns; column++)
            {
                for (int row = 0; row < Rows; row++)
                {
                    if (_piecesInPlay[row, column] is King && _piecesInPlay[row, column].PieceColor == color)
                    {
                        return new Position(row, column);
                    }
                }
            }
            return null;
        }

        public void PlacePiece(Piece piece, Position position)
        {
            _piecesInPlay[position.Row, position.Column] = piece;
            piece.CurrentPosition = position;
            _pictureBoxes[position.Row, position.Column].Image = piece.ShowImage();
        }

        private void InitializeBoard()
        {
            ShowBoard();
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    _piecesInPlay[row, column] = null;
                    _pictureBoxes[row, column].Image = null;
                }
            }
            PlacePiece(new Rook(this, Color.White), new Position(7, 0));
            PlacePiece(new Knight(this, Color.White), new Position(7, 1));
            PlacePiece(new Bishop(this, Color.White), new Position(7, 2));
            PlacePiece(new Queen(this, Color.White), new Position(7, 3));
            PlacePiece(new King(this, Color.White), new Position(7, 4));
            PlacePiece(new Bishop(this, Color.White), new Position(7, 5));
            PlacePiece(new Knight(this, Color.White), new Position(7, 6));
            PlacePiece(new Rook(this, Color.White), new Position(7, 7));
            PlacePiece(new Pawn(this, Color.White), new Position(6, 7));
            PlacePiece(new Pawn(this, Color.White), new Position(6, 6));
            PlacePiece(new Pawn(this, Color.White), new Position(6, 5));
            PlacePiece(new Pawn(this, Color.White), new Position(6, 4));
            PlacePiece(new Pawn(this, Color.White), new Position(6, 3));
            PlacePiece(new Pawn(this, Color.White), new Position(6, 2));
            PlacePiece(new Pawn(this, Color.White), new Position(6, 1));
            PlacePiece(new Pawn(this, Color.White), new Position(6, 0));

            PlacePiece(new Rook(this, Color.Black), new Position(0, 0));
            PlacePiece(new Knight(this, Color.Black), new Position(0, 1));
            PlacePiece(new Bishop(this, Color.Black), new Position(0, 2));
            PlacePiece(new Queen(this, Color.Black), new Position(0, 3));
            PlacePiece(new King(this, Color.Black), new Position(0, 4));
            PlacePiece(new Bishop(this, Color.Black), new Position(0, 5));
            PlacePiece(new Knight(this, Color.Black), new Position(0, 6));
            PlacePiece(new Rook(this, Color.Black), new Position(0, 7));
            PlacePiece(new Pawn(this, Color.Black), new Position(1, 7));
            PlacePiece(new Pawn(this, Color.Black), new Position(1, 6));
            PlacePiece(new Pawn(this, Color.Black), new Position(1, 5));
            PlacePiece(new Pawn(this, Color.Black), new Position(1, 4));
            PlacePiece(new Pawn(this, Color.Black), new Position(1, 3));
            PlacePiece(new Pawn(this, Color.Black), new Position(1, 2));
            PlacePiece(new Pawn(this, Color.Black), new Position(1, 1));
            PlacePiece(new Pawn(this, Color.Black), new Position(1, 0));
        }

        private void UpdateCheckLabel()
        {
            labelCheck.Text = _check ? "Шах!" : " ";
        }
        private void UpdatePlayerLabel()
        {
            labelCurrentPlayer.Text = "Черга: " + (_currentPlayer == Color.White ? "Білих" : "Чорних");
        }
        private void UpdateTurnLabel()
        {
            labelTurn.Text = $"Хід: {_turn}";
        }
        private void UpdateCapturedPiecesLabels()
        {
            labelWhiteCapturedPieces.Text = $"Втрачених фігурок: {_blackCapturedQuantity}";
            labelBlackCapturedPieces.Text = $"Втрачених фігурок: {_whiteCapturedQuantity}";
        }
        private void UpdateLabels()
        {
            UpdatePlayerLabel();
            UpdateTurnLabel();
            UpdateCapturedPiecesLabels();
            UpdateCheckLabel();
        }

        private void IncrementTurn()
        {
            _turn++;
        }

        private void ShowBoard()
        {
            System.Drawing.Color color;
            for (int row = 0; row < Rows; row++)
            {
                color = (row % 2 == 0) ? System.Drawing.Color.Gray : System.Drawing.Color.White;
                for (int column = 0; column < Columns; column++)
                {
                    _pictureBoxes[row, column].BackColor = color;
                    color = (color == System.Drawing.Color.White) ? System.Drawing.Color.Gray : System.Drawing.Color.White;
                }
            }
        }
        private void ShowBoard(bool[,] possiblePositions)
        {
            System.Drawing.Color color;
            ShowBoard();
            for (int row = 0; row < Rows; row++)
            {
                color = (row % 2 == 0) ? System.Drawing.Color.Gray : System.Drawing.Color.White;
                for (int column = 0; column < Columns; column++)
                {
                    _pictureBoxes[row, column].BackColor = possiblePositions[row, column] ?
                        (_pictureBoxes[row, column].BackColor == System.Drawing.Color.White ?
                        System.Drawing.Color.LightCyan : System.Drawing.Color.LightBlue) : color;
                    color = color == System.Drawing.Color.White ? System.Drawing.Color.Gray : System.Drawing.Color.White;
                }
            }
        }
       
        public bool PieceExists(Position position)
        {
            return GetPiece(position) != null;
        }
        private void ProcessPictureBoxClick(Position position)
        {
            if (_origin != null && _origin.CompareTo(position) == 0)
            {
                ShowBoard();
                _origin = null;
                return;
            }
            if (PieceExists(position))
            {
                Piece piece = GetPiece(position);
                if (piece.PieceColor == _currentPlayer)
                {
                    _origin = position;
                    ShowBoard(CheckPossibleMoves(piece));
                    return;
                }
            }
            if ((_pictureBoxes[position.Row, position.Column].BackColor == System.Drawing.Color.LightBlue ||
                _pictureBoxes[position.Row, position.Column].BackColor == System.Drawing.Color.LightCyan) &&
                _origin != null)
            {
                ShowBoard();
                ExecuteMove(_origin, position);
                SwitchPlayer();
                IncrementTurn();
                _origin = null;
                CheckEndOfGame();
                UpdateLabels();
            }
        }
        public bool CheckEndOfGame()
        {
            bool ret;

            _check = CheckCheck(_currentPlayer);
            ret = _check ? CheckCheckmate(_currentPlayer) : false;
            ret = ret ? true : !ret && (!ColorHasWhereToMove() || (NumberOfPiecesInPlay() <= 2));
            if (ret)
            {
                EndOfGame();
            }

            return ret;
        }
        
        public int NumberOfPiecesInPlay()
        {
            return PiecesInPlay(Color.White).Count + PiecesInPlay(Color.Black).Count;
        }
       
        public bool CheckCheck(Color color)
        {
            Position kingPosition = GetKingPosition(color);

            if (kingPosition == null)
            {
                throw new System.Exception($"{color} король не на дошці?!(");
            }

            foreach (Piece piece in PiecesInPlay(OpponentColor(color)))
            {
                bool[,] matrix = piece.PossibleMoves();
                if (matrix[kingPosition.Row, kingPosition.Column])
                {
                    ((King)_piecesInPlay[kingPosition.Row, kingPosition.Column]).ReceivedCheck = true;
                    return true;
                }
            }

            return false;
        }
        public bool[,] CheckPossibleMoves(Piece piece)
        {
            bool[,] possiblePositions = piece.PossibleMoves();
            for (int column = 0; column < Columns; column++)
            {
                for (int row = 0; row < Rows; row++)
                {
                    if (possiblePositions[row, column])
                    {
                        _piecesInPlay[piece.CurrentPosition.Row, piece.CurrentPosition.Column] = null;
                        Piece capturedPiece = GetPiece(row, column);
                        _piecesInPlay[row, column] = piece;
                        if (CheckCheck(piece.PieceColor))
                        {
                            possiblePositions[row, column] = false;
                        }
                        _piecesInPlay[row, column] = capturedPiece;
                        _piecesInPlay[piece.CurrentPosition.Row, piece.CurrentPosition.Column] = piece;
                    }
                }
            }
            return possiblePositions;
        }
        public bool CheckCheckmate(Color color)
        {
            foreach (Piece piece in PiecesInPlay(color))
            {
                bool[,] matrix = CheckPossibleMoves(piece);
                for (int row = 0; row < Rows; row++)
                {
                    for (int column = 0; column < Columns; column++)
                    {
                        if (matrix[row, column])
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public void EndOfGame()
        {
            string message = _check ? $"Мат\nПереможець: {OpponentColor(_currentPlayer)}" : "Draw";
            MessageBox.Show(message);

            if (MessageBox.Show("Бажаєте зіграти ще раз?", "Кінець гри",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                InitializeNewGame();
            }
            else
            {
                Application.Exit();
            }
        }
        public bool ColorHasWhereToMove()
        {
            foreach (Piece piece in PiecesInPlay(_currentPlayer))
            {
                foreach (bool position in CheckPossibleMoves(piece))
                {
                    if (position)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #region Click Picture Box

        private void pictureBoxA1_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(0, 0));
        }

        private void pictureBoxA2_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(0, 1));
        }

        private void pictureBoxA3_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(0, 2));
        }

        private void pictureBoxA4_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(0, 3));
        }

        private void pictureBoxA5_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(0, 4));
        }

        private void pictureBoxA6_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(0, 5));
        }

        private void pictureBoxA7_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(0, 6));
        }

        private void pictureBoxA8_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(0, 7));
        }

        private void pictureBoxB1_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(1, 0));
        }

        private void pictureBoxB2_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(1, 1));
        }

        private void pictureBoxB3_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(1, 2));
        }

        private void pictureBoxB4_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(1, 3));
        }

        private void pictureBoxB5_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(1, 4));
        }

        private void pictureBoxB6_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(1, 5));
        }

        private void pictureBoxB7_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(1, 6));
        }

        private void pictureBoxB8_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(1, 7));
        }

        private void pictureBoxC1_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(2, 0));
        }

        private void pictureBoxC2_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(2, 1));
        }

        private void pictureBoxC3_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(2, 2));
        }

        private void pictureBoxC4_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(2, 3));
        }

        private void pictureBoxC5_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(2, 4));
        }

        private void pictureBoxC6_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(2, 5));
        }

        private void pictureBoxC7_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(2, 6));
        }

        private void pictureBoxC8_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(2, 7));
        }

        private void pictureBoxD1_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(3, 0));
        }

        private void pictureBoxD2_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(3, 1));
        }

        private void pictureBoxD3_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(3, 2));
        }

        private void pictureBoxD4_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(3, 3));
        }

        private void pictureBoxD5_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(3, 4));
        }

        private void pictureBoxD6_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(3, 5));
        }

        private void pictureBoxD7_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(3, 6));
        }

        private void pictureBoxD8_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(3, 7));
        }

        private void pictureBoxE1_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(4, 0));
        }

        private void pictureBoxE2_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(4, 1));
        }

        private void pictureBoxE3_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(4, 2));
        }

        private void pictureBoxE4_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(4, 3));
        }

        private void pictureBoxE5_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(4, 4));
        }

        private void pictureBoxE6_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(4, 5));
        }

        private void pictureBoxE7_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(4, 6));
        }

        private void pictureBoxE8_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(4, 7));
        }

        private void pictureBoxF1_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(5, 0));
        }

        private void pictureBoxF2_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(5, 1));
        }

        private void pictureBoxF3_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(5, 2));
        }

        private void pictureBoxF4_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(5, 3));
        }

        private void pictureBoxF5_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(5, 4));
        }

        private void pictureBoxF6_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(5, 5));
        }

        private void pictureBoxF7_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(5, 6));
        }

        private void pictureBoxF8_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(5, 7));
        }

        private void pictureBoxG1_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(6, 0));
        }

        private void pictureBoxG2_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(6, 1));
        }

        private void pictureBoxG3_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(6, 2));
        }

        private void pictureBoxG4_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(6, 3));
        }

        private void pictureBoxG5_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(6, 4));
        }

        private void pictureBoxG6_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(6, 5));
        }

        private void pictureBoxG7_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(6, 6));
        }

        private void pictureBoxG8_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(6, 7));
        }

        private void pictureBoxH1_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(7, 0));
        }

        private void pictureBoxH2_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(7, 1));
        }

        private void pictureBoxH3_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(7, 2));
        }

        private void pictureBoxH4_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(7, 3));
        }

        private void pictureBoxH5_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(7, 4));
        }

        private void pictureBoxH6_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(7, 5));
        }

        private void pictureBoxH7_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(7, 6));
        }

        private void pictureBoxH8_Click(object sender, System.EventArgs e)
        {
            ProcessPictureBoxClick(new Position(7, 7));
        }

        #endregion
    }
}