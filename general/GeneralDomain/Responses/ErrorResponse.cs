using System.Net;
using GeneralDomain.Extensions;

namespace GeneralDomain.Responses;

public class ErrorResponse
{
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }

    public ErrorResponse()
    {
    }

    public ErrorResponse(HttpStatusCode statusCode, string errorMessage)
    {
        if (HttpContextHelper.Current?.Response != null)
            HttpContextHelper.Current.Response.StatusCode = (int)statusCode;

        ErrorCode = (int)statusCode;
        ErrorMessage = errorMessage;
    }

    public ErrorResponse(int errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }
}