namespace BooksLibrary.Domain;

public class Result<T>
    where T : class?
{
    private Result(T value, bool isSuccess, string error)
    {
        Value = value;
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; private set; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; private set; }
    public string Error { get; private set; }

    public static Result<T> Success(T value) => new(value, true, string.Empty);

    public static Result<T?> Failure(string error) => new(null, false, error);
}