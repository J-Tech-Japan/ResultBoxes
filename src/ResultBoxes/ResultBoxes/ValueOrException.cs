namespace ResultBoxes;

public record ValueOrException<TValue>(TValue? Value, bool IsException) where TValue : notnull
{
    public static ValueOrException<TValue> FromValue(TValue value) => new(value, false); 
    public static ValueOrException<TValue> Exception => new(default, true);
    public TValue GetValue() => (IsException == false ? Value : throw new ResultsInvalidOperationException() ) ?? throw new ResultsInvalidOperationException();
    public static implicit operator ValueOrException<TValue>(TValue value) => FromValue(value);
    public static implicit operator ValueOrException<TValue>(ValueOrException exception) => new(default, true);

}