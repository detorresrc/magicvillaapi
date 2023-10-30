using System.Net;

namespace MagicVilla.Models;

public class APIResponseGeneric<T>
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; } = true;
    public string? Message { get; set; }
    public List<string> ErrorMessages { get; set; }
    public T? Result { get; set; }
}