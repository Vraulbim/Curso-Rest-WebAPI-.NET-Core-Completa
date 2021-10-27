using DevIO.Api.Controllers;
using DevIO.Business.Intefaces;
using Elmah.Io.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DevIO.Api.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/teste")]
    public class TesteController : MainController
    {
        private readonly ILogger _logger;


        public TesteController(INotificador notificador, IUser user, ILogger<TesteController> logger) : base(notificador, user) 
        {
            _logger = logger;
        }

        [HttpGet("log")]
        public string Valor()
        {
            _logger.LogTrace("Log de Trace");
            _logger.LogDebug("Log de Degub");
            _logger.LogInformation("Log de Informação");
            _logger.LogWarning("Log de Aviso");
            _logger.LogError("Log de Erro");
            _logger.LogCritical("Log de problema Crítico");


            return "Versionamento : 2.0";
        }
        
        [HttpGet("throw")]
        public int ForceException()
        {
            try
            {
                int i = 0;
                var result = 10 / i;
            }
            catch (DivideByZeroException exception)
            {
                exception.Ship(HttpContext);
            }

            return 1;
        }

        [HttpGet("throw-v2")]
        public int ForceExceptionV2()
        {
            throw new Exception("Handle report error");
        }
    }
}
