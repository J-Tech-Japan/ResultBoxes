namespace ResultBoxes;

public class ResultsInvalidOperationException(string? msg = null)
    : InvalidOperationException(msg ?? "result value is null");
