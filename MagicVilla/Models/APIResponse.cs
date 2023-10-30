using System.Net;

namespace MagicVilla.Models;

public class APIResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; } = true;
    public string Message { get; set; }
    public List<string> ErrorMessages { get; set; }
    public object Result { get; set; }
}