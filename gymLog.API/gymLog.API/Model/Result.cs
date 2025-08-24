using gymLog.API.Model.enums;

namespace gymLog.API.Model.DTO;

public class Result<T>  {
    public bool IsSuccess { get; }
    public string? Message { get; }
    public IEnumerable<ErrorCode>? Errors { get; }
    public T? Data { get; }
    public ErrorCode? Error { get; }

    private Result(bool isSuccess, string? message, IEnumerable<ErrorCode>? errors, T? data = default) {
        IsSuccess = isSuccess;
        Message = message;
        Errors = errors;
        Data = data;
    }
    private Result(bool isSuccess, string? message, ErrorCode error, T? data = default)
    {
        IsSuccess = isSuccess;
        Message = message;
        Error = error;
        Data = data;
    }

    public static Result<T> Success(T data, string? message = null) => new Result<T>(true, message, null, data);

    public static Result<T> Failure(string message, IEnumerable<ErrorCode>? errors = null) =>
        new Result<T>(false, message, errors);

    public static Result<T> Failure(string message, ErrorCode error) =>
        new Result<T>(false, message, error);
}