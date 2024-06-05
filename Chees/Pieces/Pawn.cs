namespace Chees
{
    class Pawn : Piece
    {
        public Pawn(Board board, Color pieceColor) : base(board, pieceColor)
        {
            Image = GetImage(pieceColor == Color.White ? "white_pawn.png" : "black_pawn.png");
        }
        private bool ThereIsEnemy(Position position)
        {
            Piece piece = ChessBoard.GetPiece(position);
            return (piece != null && piece.PieceColor != PieceColor);
        }
        private bool IsFree(Position position)
        {
            return (ChessBoard.GetPiece(position) == null);
        }
        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[ChessBoard.Rows, ChessBoard.Columns];

            Position position = new Position(0, 0);

            if (PieceColor == Color.White)
            {
                position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column);
                if (ValidPosition(position) && IsFree(position))
                {
                    matrix[position.Row, position.Column] = true;
                }

                position.SetPosition(CurrentPosition.Row - 2, CurrentPosition.Column);
                if (ValidPosition(position) && IsFree(position) && NumberOfMoves == 0 &&
                    IsFree(new Position(CurrentPosition.Row - 1, CurrentPosition.Column)))
                {
                    matrix[position.Row, position.Column] = true;
                }

                position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column - 1);
                if (ValidPosition(position) && ThereIsEnemy(position))
                {
                    matrix[position.Row, position.Column] = true;
                }

                position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column + 1);
                if (ValidPosition(position) && ThereIsEnemy(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                if (CurrentPosition.Row == 3)
                {
                    Position left = new Position(CurrentPosition.Row, CurrentPosition.Column - 1);
                    if (ValidPosition(left) && ThereIsEnemy(left))
                    {
                        Piece piece = ChessBoard.GetPiece(left);
                        Position destination = new Position(left.Row - 1, left.Column);
                        if (piece is Pawn && piece.NumberOfMoves == 1 && ChessBoard.GetPiece(destination) == null)
                        {
                            matrix[left.Row - 1, left.Column] = true;
                        }
                    }

                    Position right = new Position(CurrentPosition.Row, CurrentPosition.Column + 1);
                    if (ValidPosition(right) && ThereIsEnemy(right))
                    {
                        Piece piece = ChessBoard.GetPiece(right);
                        Position destination = new Position(left.Row - 1, left.Column);
                        if (piece is Pawn && piece.NumberOfMoves == 1 && ChessBoard.GetPiece(destination) == null)
                        {
                            matrix[right.Row - 1, right.Column] = true;
                        }
                    }
                }
            }
            else
            {
                position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column);
                if (ValidPosition(position) && IsFree(position))
                {
                    matrix[position.Row, position.Column] = true;
                }

                position.SetPosition(CurrentPosition.Row + 2, CurrentPosition.Column);
                if (ValidPosition(position) && IsFree(position) && NumberOfMoves == 0 &&
                    IsFree(new Position(CurrentPosition.Row + 1, CurrentPosition.Column)))
                {
                    matrix[position.Row, position.Column] = true;
                }

                position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column + 1);
                if (ValidPosition(position) && ThereIsEnemy(position))
                {
                    matrix[position.Row, position.Column] = true;
                }

                position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column - 1);
                if (ValidPosition(position) && ThereIsEnemy(position))
                {
                    matrix[position.Row, position.Column] = true;
                }
                if (CurrentPosition.Row == 4)
                {
                    Position left = new Position(CurrentPosition.Row, CurrentPosition.Column - 1);
                    if (ValidPosition(left) && ThereIsEnemy(left))
                    {
                        Piece piece = ChessBoard.GetPiece(left);
                        Position destination = new Position(left.Row + 1, left.Column);
                        if (piece is Pawn && piece.NumberOfMoves == 1 && ChessBoard.GetPiece(destination) == null)
                        {
                            matrix[left.Row + 1, left.Column] = true;
                        }
                    }

                    Position right = new Position(CurrentPosition.Row, CurrentPosition.Column + 1);
                    if (ValidPosition(right) && ThereIsEnemy(right))
                    {
                        Piece piece = ChessBoard.GetPiece(right);
                        Position destination = new Position(right.Row + 1, right.Column);
                        if (piece is Pawn && piece.NumberOfMoves == 1 && ChessBoard.GetPiece(destination) == null)
                        {
                            matrix[right.Row + 1, right.Column] = true;
                        }
                    }
                }
            }

            return matrix;
        }
    }
}
