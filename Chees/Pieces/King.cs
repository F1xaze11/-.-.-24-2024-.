namespace Chees
{
    class King : Piece
    {
        public bool ReceivedCheck;
        public King(Board board, Color pieceColor) : base(board, pieceColor)
        {
            Image = GetImage(pieceColor == Color.White ? "white_king.png" : "black_king.png");
            ReceivedCheck = false;
        }
        private bool CheckRookAvailableForCastling(Position position)
        {
            Piece piece = ChessBoard.GetPiece(position);
            return piece is Rook && piece.PieceColor == PieceColor && piece.NumberOfMoves == 0;
        }
        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[ChessBoard.Rows, ChessBoard.Columns];
            Position position = new Position(0, 0);
            position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column + 1);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            position.SetPosition(CurrentPosition.Row, CurrentPosition.Column + 1);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column - 1);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column - 1);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            position.SetPosition(CurrentPosition.Row, CurrentPosition.Column - 1);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column + 1);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }
            if (NumberOfMoves == 0 && !ReceivedCheck)
            {
                Position positionRook1 = new Position(CurrentPosition.Row, CurrentPosition.Column + 3);
                if (CheckRookAvailableForCastling(positionRook1))
                {
                    Position position1 = new Position(CurrentPosition.Row, CurrentPosition.Column + 1);
                    Position position2 = new Position(CurrentPosition.Row, CurrentPosition.Column + 2);
                    if (ChessBoard.GetPiece(position1) == null &&
                        ChessBoard.GetPiece(position2) == null)
                    {
                        matrix[CurrentPosition.Row, CurrentPosition.Column + 2] = true;
                    }
                }
                Position positionRook2 = new Position(CurrentPosition.Row, CurrentPosition.Column - 4);
                if (CheckRookAvailableForCastling(positionRook2))
                {
                    Position position1 = new Position(CurrentPosition.Row, CurrentPosition.Column - 1);
                    Position position2 = new Position(CurrentPosition.Row, CurrentPosition.Column - 2);
                    Position position3 = new Position(CurrentPosition.Row, CurrentPosition.Column - 3);
                    if (ChessBoard.GetPiece(position1) == null &&
                        ChessBoard.GetPiece(position2) == null &&
                        ChessBoard.GetPiece(position3) == null)
                    {
                        matrix[CurrentPosition.Row, CurrentPosition.Column - 2] = true;
                    }
                }
            }
            return matrix;
        }
    }
}
