namespace Chees
{
    class Knight : Piece
    {
        public Knight(Board board, Color pieceColor) : base(board, pieceColor)
        {
            Image = GetImage(pieceColor == Color.White ? "white_knight.png" : "black_knight.png");
        }
        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[ChessBoard.Rows, ChessBoard.Columns];

            Position position = new Position(0, 0);

            position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column - 2);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            position.SetPosition(CurrentPosition.Row - 2, CurrentPosition.Column - 1);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            position.SetPosition(CurrentPosition.Row - 2, CurrentPosition.Column + 1);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column + 2);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column + 2);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            position.SetPosition(CurrentPosition.Row + 2, CurrentPosition.Column + 1);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            position.SetPosition(CurrentPosition.Row + 2, CurrentPosition.Column - 1);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column - 2);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            return matrix;
        }
    }
}
