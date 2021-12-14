namespace Day12
{
    internal sealed record Cave (string Identifier)
    {
        public List<Cave> Neighbours { get; init; } = new();

        public bool IsLowercase
        {
            get
            {
                return Identifier.All(x => char.IsLower(x));
            }
        }

        public override int GetHashCode()
        {
            return Identifier.GetHashCode();
        }

        public bool Equals(Cave? other)
        {
            if (other == null)
            {
                return false;
            }

            return other.Identifier.Equals(Identifier);
        }
    }
}
