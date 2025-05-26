namespace Shortly.Core;

public class Result<TValue>
{
    private readonly TValue? _value;
    private readonly Error _error;
    private readonly bool _isSuccess;

    private Result(TValue value)
    {
        _isSuccess = true;
        _value = value;
        _error = Error.None;
    }

    private Result(Error error)
    {
        _isSuccess = false;
        _error = error;
        _value = default;
    }
    
    public bool Succeeded => _isSuccess;
    public TValue? Value => _value;
    public Error Error => _error;

    public static Result<TValue> Success(TValue value) => new(value);
    public static Result<TValue> Failure(Error error) => new(error);
    
    public static implicit operator Result<TValue>(TValue value) => Success(value);
    public static implicit operator Result<TValue>(Error error) => Failure(error);

    public TResult Match<TResult>(Func<TValue, TResult> onSuccess, Func<TResult> onFailure)
    {
        return _isSuccess ? onSuccess(_value!) : onFailure();
    }


}