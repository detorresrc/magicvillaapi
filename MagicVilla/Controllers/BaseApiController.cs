using System.Net;
using MagicVilla.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    protected ActionResult<APIResponse> ErrorResponse(
        HttpStatusCode errorCode,
        string errorMessage
    )
    {
        APIResponse response = new();
        response.StatusCode = errorCode;
        response.Message = errorMessage;
        response.IsSuccess = false;
        
        switch (errorCode)
        {
            case HttpStatusCode.NotFound:
                return NotFound(response);
            case HttpStatusCode.BadRequest:
                return BadRequest(response);
            default:
                return BadRequest(response);
        }
    }
    
    protected ActionResult<APIResponseGeneric<T>> ErrorResponseGeneric<T>(
        HttpStatusCode errorCode,
        string errorMessage
    )
    {
        APIResponseGeneric<T> response = new()
        {
            StatusCode = errorCode,
            Message = errorMessage,
            IsSuccess = false
        };

        switch (errorCode)
        {
            case HttpStatusCode.NotFound:
                return NotFound(response);
            case HttpStatusCode.BadRequest:
                return BadRequest(response);
            default:
                return BadRequest(response);
        }
    }
}