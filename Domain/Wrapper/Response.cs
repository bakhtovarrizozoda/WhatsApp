using System.Net;

namespace Domain.Wrapper;

public class Response<T>
{
    public Response(T data)
    {
        Data = data;
        StatusCode = HttpStatusCode.OK;
    }

    public Response(HttpStatusCode statusCode, List<string> errors)
    {
        StatusCode = statusCode;
        Errors = errors;            
    }
    
    public Response(HttpStatusCode statusCode, string error)
    {
        StatusCode = statusCode;
        Errors = new List<string>(){error};        
    }

    public T Data { get; set; }
    public List<string> Errors { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}