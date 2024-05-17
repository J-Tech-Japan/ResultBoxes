namespace ResultBoxes;

public record ExceptionOrNone(Exception? Exception = null)
{
    public bool HasException => Exception is not null;
    public static ExceptionOrNone None => new();
    public static ExceptionOrNone FromException(Exception exception) => new(exception);

    public static implicit operator ExceptionOrNone(Exception exception) => new(exception);
    public static implicit operator ExceptionOrNone(UnitValue _) => new();
}
