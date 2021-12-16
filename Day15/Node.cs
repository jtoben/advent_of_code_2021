namespace Day15
{
    internal sealed record Node (int X, int Y) : IComparable<Node>, IEquatable<Node>
    {
        public int Value { get; set; } = int.MaxValue;

        public bool Equals(Node? other)
        {
            // Optimizing for speed: other is never null, so no null checks.
            return (X, Y).Equals((other.X, other.Y));
        }

        public override int GetHashCode()
        {
            return (X, Y).GetHashCode();
        }

        public int CompareTo(Node? other)
        {
            // Optimizing for speed: other is never null, so no null checks.
            if (Value != other.Value)
            {
                return Value.CompareTo(other.Value);
            }
            else
            {
                return (X, Y).CompareTo((other.X, other.Y));
            }
        }
    }
}
