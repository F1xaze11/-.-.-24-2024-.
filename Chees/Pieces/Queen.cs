namespace Chees
{
    class Queen : Piece
    {
        public Queen(Board board, Color pieceColor) : base(board, pieceColor)
        {
            Image = GetImage(pieceColor == Color.White ? "white_queen.png" : "black_queen.png");
        }
        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[ChessBoard.Rows, ChessBoard.Columns];

            Position position = new Position(0, 0);
            position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column);
            while (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if ((ChessBoard.GetPiece(position) != null) && ChessBoard.GetPiece(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.Row -= 1;
            }
            position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column);
            while (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if ((ChessBoard.GetPiece(position) != null) && ChessBoard.GetPiece(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.Row += 1;
            }
            position.SetPosition(CurrentPosition.Row, CurrentPosition.Column + 1);
            while (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if ((ChessBoard.GetPiece(position) != null) && ChessBoard.GetPiece(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.Column += 1;
            }
            position.SetPosition(CurrentPosition.Row, CurrentPosition.Column - 1);
            while (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if ((ChessBoard.GetPiece(position) != null) && ChessBoard.GetPiece(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.Column -= 1;
            }
            position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column - 1);
            while (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if ((ChessBoard.GetPiece(position) != null) && ChessBoard.GetPiece(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.SetPosition(position.Row + 1, position.Column - 1);
            }
            position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column + 1);
            while (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if ((ChessBoard.GetPiece(position) != null) && ChessBoard.GetPiece(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.SetPosition(position.Row + 1, position.Column + 1);
            }
            position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column + 1);
            while (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if ((ChessBoard.GetPiece(position) != null) && ChessBoard.GetPiece(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.SetPosition(position.Row - 1, position.Column + 1);
            }
            position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column - 1);
            while (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if ((ChessBoard.GetPiece(position) != null) && ChessBoard.GetPiece(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.SetPosition(position.Row - 1, position.Column - 1);
            }

            return matrix;
        }
    }
}
