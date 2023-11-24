namespace AdminAssistant.Framework.Primitives;

public abstract class Id : ValueObject
{
    protected Id()
        => Value = Constants.NewRecordID;
    protected Id(int value)
        => Value = value;

    public int Value { get; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
