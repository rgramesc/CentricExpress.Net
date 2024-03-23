namespace BooksLibrary.Domain;

public class Result
{
    private Result(bool isSuccess, string error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; private set; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; private set; }

    public static Result Success() => new(true, string.Empty);

    public static Result Failure(string error) => new(false, error);
}