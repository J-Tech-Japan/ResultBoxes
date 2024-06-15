namespace ResultBoxes;

public record ResultDto<TValue>(TValue? Value, Exception? Exception, bool IsSuccess)
    where TValue : notnull;