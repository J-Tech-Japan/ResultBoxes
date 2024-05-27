namespace ResultBoxes;

public record UnitValue
{
    public static UnitValue None => new();
    public static ResultBox<UnitValue> WrapTry(Action action)
        => ResultBox<UnitValue>.WrapTry(action);
    public static async Task<ResultBox<UnitValue>> WrapTry(Func<Task> action)
        => await ResultBox<UnitValue>.WrapTry(action);
}
