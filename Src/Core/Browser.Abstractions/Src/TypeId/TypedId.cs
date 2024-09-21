namespace Browser.Abstractions.TypeId;

public abstract class TypedId : IEquatable<TypedId>
{
    public string Value { get; }

    protected TypedId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("ID value cannot be empty", nameof(value));
        }

        Value = value;
    }

    public override string ToString() => Value;


    public bool Equals(TypedId? other)
    {
        if (other is null || GetType() != other.GetType())
            return false;

        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is TypedId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value, GetType());
    }
    
    public static bool operator ==(TypedId? left, TypedId? right)
    {
        if (left is null && right is null)
            return true;
        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(TypedId left, TypedId right) => !(left == right);
}
