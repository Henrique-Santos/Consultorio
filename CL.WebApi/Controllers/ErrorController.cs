using CL.Core.Shared.ModelViews.Erro;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CL.WebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)] // O controlador será ignorado pelo Swagger
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public ErrorResponse Error()
        {
            // Pegamos preferencialmente o id do erro se não houver o id da requisição
            var errorId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            Response.StatusCode = 500;
            return new ErrorResponse(errorId);
        }
    }
}
