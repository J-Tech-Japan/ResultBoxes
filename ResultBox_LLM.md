# ResultBox Explained (for LLMs and Non-Coders) - Comprehensive Guide

## What is ResultBox?

Think of `ResultBox` as a special container or "box" designed to hold the outcome of an operation or task. This outcome can be one of two things:

1.  **Success:** The box contains the actual result or value produced by the operation (e.g., a number, some text, a list of items).
2.  **Failure:** The box contains an error (an "Exception" in programming terms) explaining what went wrong (e.g., "File not found", "Invalid input").

A `ResultBox` *always* holds either a success value *or* a failure error, never both, and never neither. It clearly signals the state of an operation.

## Why Use It? Handling Potential Failures Gracefully

Many processes involve steps that might fail. `ResultBox` provides a structured way to manage this:

*   **Avoid Crashes:** Instead of stopping the whole process, a failing step puts an error into its `ResultBox`.
*   **Clear Signaling:** It's obvious whether a step succeeded or failed just by looking at the type of box returned.
*   **Controlled Flow:** Subsequent steps can easily check the box and decide whether to proceed or bypass their logic if a previous step failed.

## The "Railway Oriented Programming" (ROP) Idea

`ResultBox` enables a pattern often called "Railway Oriented Programming". Imagine two parallel train tracks:

*   **Success Track:** Operations proceed along this track as long as everything goes well. The `ResultBox` carries the successful result.
*   **Failure Track:** If an operation fails, the process switches to the failure track. The `ResultBox` now carries the error. Once on the failure track, subsequent operations on the success track are usually skipped, and the error is carried to the end.

`ResultBox` uses several "helper functions" (extension methods) to manage this flow:

*   **`Conveyor` (or `Then`, `Bind`):** The main engine for the success track. If the incoming box is Success, it runs the next function using the value inside. If the box is Failure, it skips the function and just passes the Failure box along.
*   **`Remap` (or `Map`):** Transforms the value *inside* a Success box without changing the track. If the box is Failure, it does nothing.
*   **`Combine`:** Merges the results from multiple `ResultBox` operations. If *all* operations succeed, it bundles their values into a single Success box. If *any* operation fails, the result is a Failure box containing the *first* error encountered.
*   **`Rescue`:** Allows switching from the Failure track back to the Success track. If the box is Failure, it lets you provide a function to handle the error, potentially returning a default success value. If the box is Success, it does nothing.
*   **`WrapTry`:** A safety net. Runs a piece of code that might throw an unexpected error and automatically catches it, putting it into a Failure box.
*   **`Do` (or `Tap`):** Performs a side effect (like logging) with the value in a Success box without changing the value or the track. Skips if the box is Failure.
*   **`Match`:** Explicitly checks if the box is Success or Failure and allows running different code for each case, often used at the end of a chain to process the final outcome.

## Core Operations & Examples

Let's illustrate the main operations with simplified code concepts, inspired by common usage patterns:

**1. Basic Chaining (`Conveyor`)**

```csharp
// Functions returning ResultBox
ResultBox<int> ParseNumber(string text) { /* ... returns Success(number) or Failure(error) ... */ }
ResultBox<int> Increment(int number) { /* ... returns Success(number + 1) or Failure(error) ... */ }
ResultBox<string> FormatNumber(int number) { /* ... returns Success($"Value: {number}") or Failure(error) ... */ }

// --- Usage ---
string inputText = "10";

ResultBox<string> finalResult = ResultBox.FromValue(inputText) // Start with a Success box
    .Conveyor(ParseNumber)      // If success, parse text to number. If failure, skip next.
    .Conveyor(Increment)        // If success, increment number. If failure, skip next.
    .Conveyor(FormatNumber);    // If success, format number to string.

// finalResult will contain either Success("Value: 11") or a Failure from any step.
```

**2. Transforming a Value (`Remap`)**

```csharp
ResultBox<int> GetUserId() { /* ... returns Success(userId) or Failure(error) ... */ }

// --- Usage ---
ResultBox<string> result = GetUserId()
    .Remap(id => $"User ID is {id}"); // Only runs if GetUserId succeeded.

// result will contain Success("User ID is 123") or the Failure from GetUserId.
```

**3. Combining Results (`Combine`)**

```csharp
ResultBox<string> GetUserName() { /* ... returns Success(name) or Failure(error) ... */ }
ResultBox<string> GetUserEmail() { /* ... returns Success(email) or Failure(error) ... */ }

// --- Usage ---
// Combine needs a starting point, often ResultBox.Start (a neutral success)
ResultBox<TwoValues<string, string>> combinedResult = ResultBox.Start
    .Combine(GetUserName)  // Get the first value
    .Combine(GetUseEmail); // Get the second value

// combinedResult contains Success(TwoValues("Alice", "alice@example.com")) 
// OR Failure if either GetUserName or GetUserEmail failed.
// You can then Remap the TwoValues if needed.
// Async example:
async Task<ResultBox<string>> GetEmailAsync() { /* ... */ }

ResultBox<TwoValues<string, string>> asyncCombined = await ResultBox.Start
    .Combine(GetUserName)       // Sync operation
    .Combine(GetEmailAsync);    // Async operation - Combine handles the mix
```

**4. Handling Failures (`Rescue`)**

```csharp
ResultBox<string> GetPrimarySetting() { /* ... might return Failure("Primary not found") ... */ }
string GetDefaultSetting() { return "DefaultValue"; }

// --- Usage ---
ResultBox<string> settingResult = GetPrimarySetting()
    .Rescue(error => {
        // Check if it's the specific error we can handle
        if (error.Message == "Primary not found") {
            // Recover by returning a default value
            return ResultBox.FromValue(GetDefaultSetting()); 
        } else {
            // Can't handle other errors, keep it as Failure
            return error; 
        }
    });

// settingResult will be Success("DefaultValue") if the primary failed but was rescued,
// or the original Success/Failure otherwise.
```

**5. Checking for Nulls (`CheckNull`)**

```csharp
string? GetOptionalValue() { /* ... might return null ... */ }

// --- Usage ---
ResultBox<string> checkedResult = ResultBox.CheckNull(GetOptionalValue(), 
                                                      new ApplicationException("Value was missing!"));

// checkedResult is Success(value) if GetOptionalValue() returned non-null,
// otherwise it's Failure(ApplicationException("Value was missing!")).
// If no custom exception is given, it defaults to ResultValueNullException.
```

**6. Safe Casting (`Cast`)**

```csharp
interface IAnimal { }
record Dog : IAnimal { public void Bark() { /* ... */ } }
record Cat : IAnimal { }

ResultBox<IAnimal> GetAnimal() { /* ... returns Success(new Dog()) or Success(new Cat()) or Failure ... */ }

// --- Usage ---
ResultBox<Dog> dogResult = GetAnimal()
    .Cast<Dog>(); // Attempts to cast the IAnimal to a Dog

// If GetAnimal returned Success(Dog), dogResult is Success(Dog).
// If GetAnimal returned Success(Cat) or Failure, dogResult is Failure (InvalidCastException or original error).
```

**7. Performing Side Effects (`Do`)**

```csharp
ResultBox<int> ProcessData(int data) { /* ... */ }
void LogValue(int value) { Console.WriteLine($"Logging successful value: {value}"); }

// --- Usage ---
ResultBox<int> result = ResultBox.FromValue(10)
    .Do(LogValue) // If Success, calls LogValue(10). Doesn't change the box.
    .Conveyor(ProcessData); // Continues the chain

// LogValue is only called if the box entering .Do is Success.
```

**8. Handling Final Outcome (`Match`)**

```csharp
ResultBox<string> GetFinalData() { /* ... returns Success or Failure ... */ }

// --- Usage ---
// Match is often used at the end to unpack the result and get a single value out.
string message = GetFinalData()
    .Match(
        // Function to run if GetFinalData() returned Success
        successValue => $"Operation succeeded with: {successValue}", 
        // Function to run if GetFinalData() returned Failure
        failureException => $"Operation failed: {failureException.Message}" 
    );
// message will be one of the two strings above, depending on the outcome.

Console.WriteLine(message);
```

**9. Transforming Errors (`RemapException`)**

```csharp
ResultBox<int> RiskyOperation() { /* ... might return Failure(new IOException(...)) ... */ }

// --- Usage ---
// Sometimes you want to convert a specific low-level error into a more meaningful one.
ResultBox<int> result = RiskyOperation()
    .RemapException(error => {
        if (error is IOException) {
            // Convert IOException to a custom ApplicationException
            return new ApplicationException("Failed due to I/O problem", error); 
        } else {
            // Leave other errors unchanged
            return error; 
        }
    });

// If RiskyOperation failed with IOException, result is now Failure(ApplicationException).
// Other failures or successes from RiskyOperation remain unchanged.
```

## Key Benefits Summarized

*   **Clarity:** Makes potential failures explicit in function signatures.
*   **Robustness:** Builds processes resilient to errors.
*   **Readability:** Creates cleaner code flow compared to nested `if`s or many `try/catch` blocks.
*   **Composability:** Chains operations together easily.

## In Simple Terms

`ResultBox` is a container that clearly separates successful outcomes (with their value) from failures (with their error description). It provides tools (`Conveyor`, `Remap`, `Combine`, `Rescue`, etc.) to build step-by-step processes that handle errors predictably and cleanly, like routing a train down either a success track or a failure track.
