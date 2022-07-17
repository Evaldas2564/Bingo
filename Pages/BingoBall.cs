namespace Bingo.Pages
{
    public class BingoBall : IComparable<BingoBall>
    {
        public int Id { get; set; }
        public int Num { get; set; }
        public bool Checked { get; set; } = false;
        public int CompareTo(BingoBall other)
        {
            return Num.CompareTo(other.Num);
        }
    }
}
