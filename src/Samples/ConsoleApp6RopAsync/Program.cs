﻿using ResultBoxes;
namespace ConsoleApp6RopAsync;

internal class Program
{
    public static Task<ResultBox<int>> IncrementAsync(int target) =>
        Task.FromResult<ResultBox<int>>(
            target switch
            {
                > 1000 => new ApplicationException(
                    $"{target} is not allowed for {nameof(IncrementAsync)}"),
                _ => target + 1
            });
    public static Task<ResultBox<int>> DoubleAsync(int target) =>
        Task.FromResult<ResultBox<int>>(
            target switch
            {
                > 1000 => new ApplicationException(
                    $"{target} is not allowed for {nameof(DoubleAsync)}"),
                _ => target * 2
            });
    public static Task<ResultBox<int>> TripleAsync(int target) =>
        Task.FromResult<ResultBox<int>>(
            target switch
            {
                > 1000 => new ApplicationException(
                    $"{target} is not allowed for {nameof(TripleAsync)}"),
                _ => target * 3
            });

    private static async Task Main(string[] args)
    {
        // Error: System.ApplicationException: 1001 is not allowed for IncrementAsync
        await IncrementAsync(1001)
            .Conveyor(DoubleAsync)
            .Conveyor(TripleAsync)
            .Log();
        // Error: System.ApplicationException: 1001 is not allowed for DoubleAsync
        await IncrementAsync(1000)
            .Conveyor(DoubleAsync)
            .Conveyor(TripleAsync)
            .Log();

        // Error: System.ApplicationException: 1202 is not allowed for TripleAsync
        await IncrementAsync(600)
            .Conveyor(DoubleAsync)
            .Conveyor(TripleAsync)
            .Log();
        // Value: 24
        await IncrementAsync(3)
            .Conveyor(DoubleAsync)
            .Conveyor(TripleAsync)
            .Log();
    }
}
