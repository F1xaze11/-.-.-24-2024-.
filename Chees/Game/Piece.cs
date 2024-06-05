using System.Drawing;
using System.IO;

namespace Chees
{
    abstract class Piece
    {
        public Image Image;
        public Position CurrentPosition { get; set; }
        public Color PieceColor { get; protected set; }
        public int NumberOfMoves { get; protected set; }
        public Board ChessBoard { get; protected set; }
        public string FileImagesPath;
        public Piece(Board board, Color color)
        {
            FileImagesPath = "C:\\Users\\adani\\Downloads\\Chees\\Pieces\\images\\";
            CurrentPosition = null;
            ChessBoard = board;
            PieceColor = color;
            NumberOfMoves = 0;
        }
        public Image ShowImage()
        {
            return Image;
        }
        public Image GetImage(string image)
        {
            try
            {
                return Image.FromFile($"{FileImagesPath}/{image}");
            }
            catch
            {
                throw new System.Exception("Error while fetching piece images");
            }
        }
        public void IncrementNumberOfMoves()
        {
            NumberOfMoves++;
        }
        public bool ValidPosition(Position position)
        {
            return position.Row <= 7 && position.Row >= 0 && position.Column <= 7 && position.Column >= 0;
        }
        public bool CanMove(Position position)
        {
            if (ValidPosition(position))
            {
                Piece piece = ChessBoard.GetPiece(position);
                return (piece == null || piece.PieceColor != PieceColor);
            }
            return false;
        }
        public abstract bool[,] PossibleMoves();

    }
}
