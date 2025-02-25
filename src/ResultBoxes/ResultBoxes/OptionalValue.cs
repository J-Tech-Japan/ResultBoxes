namespace ResultBoxes;

public record OptionalValue<TValue>(TValue? Value, bool HasValue = true)
{
    public static OptionalValue<TValue> Empty => new(default, false);
    public static OptionalValue<TValue> Null => new(default, false);
    public static OptionalValue<TValue> None => new(default, false);

    public TValue GetValue() =>
        HasValue && Value is not null ? Value : throw new ResultsInvalidOperationException("no value");

    public static implicit operator OptionalValue<TValue>(TValue value) => new(value);

    public static OptionalValue<TValue> FromValue(TValue value) => new(value);

    public TResult Match<TResult>(Func<TValue, TResult> some, Func<TResult> none) =>
        HasValue ? some(GetValue()) : none();

    public async Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> some, Func<Task<TResult>> none) =>
        HasValue ? await some(GetValue()) : await none();

    public async Task<TResult> Match<TResult>(Func<TValue, TResult> some, Func<Task<TResult>> none) =>
        HasValue ? some(GetValue()) : await none();

    public async Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> some, Func<TResult> none) =>
        HasValue ? await some(GetValue()) : none();

    public TResult Match<TResult>(Func<TValue, TResult> some, TResult none) =>
        HasValue ? some(GetValue()) : none;

    public async Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> some, Task<TResult> none) =>
        HasValue ? await some(GetValue()) : await none;

    public async Task<TResult> Match<TResult>(Func<TValue, TResult> some, Task<TResult> none) =>
        HasValue ? some(GetValue()) : await none;

    public async Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> some, TResult none) =>
        HasValue ? await some(GetValue()) : none;
}

public static class OptionalValue
{
    public static OptionalValue<TValue> FromValue<TValue>(TValue value) where TValue : notnull => new(value);

    public static OptionalValue<TValueRemapped> Remap<TValue, TValueRemapped>(this OptionalValue<TValue> value,
        Func<TValue, TValueRemapped> remapFunc) where TValue : notnull =>
        value.HasValue
            ? new OptionalValue<TValueRemapped>(remapFunc(value.GetValue()))
            : OptionalValue<TValueRemapped>.None;

    public static async Task<OptionalValue<TValueRemapped>> Remap<TValue, TValueRemapped>(
        this OptionalValue<TValue> value,
        Func<TValue, Task<TValueRemapped>> remapFunc) where TValue : notnull =>
        value.HasValue
            ? new OptionalValue<TValueRemapped>(await remapFunc(value.GetValue()))
            : OptionalValue<TValueRemapped>.None;

    public static async Task<OptionalValue<TValueRemapped>> Remap<TValue, TValueRemapped>(
        this Task<OptionalValue<TValue>> value,
        Func<TValue, TValueRemapped> remapFunc) where TValue : notnull => Remap(await value, remapFunc);

    public static async Task<OptionalValue<TValueRemapped>> Remap<TValue, TValueRemapped>(
        this Task<OptionalValue<TValue>> value,
        Func<TValue, Task<TValueRemapped>> remapFunc) where TValue : notnull => await Remap(await value, remapFunc);

    public static async Task<OptionalValue<TValue>> FromValue<TValue>(Task<TValue> value) where TValue : notnull =>
        new(await value);

    public static OptionalValue<TValue> FromNullableValue<TValue>(TValue? value) where TValue : notnull =>
        value is null ? OptionalValue<TValue>.Null : new OptionalValue<TValue>(value);

    public static async Task<OptionalValue<TValue>> FromNullableValue<TValue>(Task<TValue?> value)
        where TValue : notnull =>
        FromNullableValue(await value);

    public static OptionalValue<TValue> FromNullableValue<TValue>(TValue? value) where TValue : struct =>
        value.HasValue ? new OptionalValue<TValue>(value.Value) : OptionalValue<TValue>.Null;

    public static async Task<OptionalValue<TValue>> FromNullableValue<TValue>(Task<TValue?> value)
        where TValue : struct => FromNullableValue(await value);

    public static ResultBox<TValue> GetValueResult<TValue>(this OptionalValue<TValue> optional)
        where TValue : notnull =>
        optional.HasValue && optional.Value is not null
            ? optional.Value
            : new ResultsInvalidOperationException("no value");

    public static async Task<TResult> Match<TValue, TResult>(this Task<OptionalValue<TValue>> optional,
        Func<TValue, TResult> some, Func<TResult> none) where TValue : notnull =>
        (await optional).Match(some, none);
}
