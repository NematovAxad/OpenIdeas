namespace GeneralDomain.Responses;

public class Response<T>
{
    public bool IsSuccess { get; set; } = true;
    public T Result { get; set; }
    public ErrorResponse Error { get; set; }

    public Response()
    {
    }

    public Response(T result)
    {
        Result = result;
        IsSuccess = true;
    }

    public Response(ErrorResponse error)
    {
        Error = error;
        IsSuccess = false;
    }

    public static implicit operator Response<T>(T result)
    {
        return new Response<T>(result);
    }

    public static implicit operator Response<T>(ErrorResponse error)
    {
        return new Response<T>(error);
    }    
}