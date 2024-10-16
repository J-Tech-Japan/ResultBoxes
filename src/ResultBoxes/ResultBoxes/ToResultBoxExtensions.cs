namespace ResultBoxes;

public static class ToResultBoxExtensions
{
    public static Task<ResultBox<T>> ToResultBox<T>(this Task<T> source) where T : notnull =>
        ResultBox.FromValue(source);
    public static ResultBox<T> ToResultBox<T>(this T source) where T : notnull => ResultBox.FromValue(source);
    
    public static ResultBox<T> ToResultBoxFromException<T>(this Exception source) where T : notnull => ResultBox.FromException<T>(source);
}