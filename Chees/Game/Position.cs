namespace Chees
{
    class Position : System.IComparable
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Position(int row, int column)
        {
            SetPosition(row, column);
        }
        public void SetPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }
        public int CompareTo(object obj)
        {
            Position other = obj as Position;
            return other.Row == Row && other.Column == Column ? 0 : 1;
        }
    }
}
