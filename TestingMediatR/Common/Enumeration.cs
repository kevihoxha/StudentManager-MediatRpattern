namespace TestingMediatR.Common
{
    public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>
        where TEnum : Enumeration<TEnum?>
    {
        protected Enumeration(int value, string name)
        {
            Value = value;
            Name = name;
        }
        public int Value { get; protected init; }
        public string Name { get; protected init; } = string.Empty;
        public static TEnum? FromValue(int value)
        {
            return default;
        }
        public static TEnum? FromName(string name)
        {
            return default;
        }
        public bool Equals(Enumeration<TEnum>? other)
        {
            if (other is null)
            {
                return false;
            }
            return GetType() == other.GetType() &&
                Value == other.Value;
        }
        public override bool Equals(object? obj)
        {
            return obj is Enumeration<TEnum> other &&
                Equals(other);
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
