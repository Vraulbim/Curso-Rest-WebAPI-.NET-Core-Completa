﻿using DevIO.Api.Controllers;
using DevIO.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.Api.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/teste")]
    public class TesteController : MainController
    {
        public TesteController(INotificador notificador, IUser user) : base(notificador, user) { }

        [HttpGet]
        public string Valor()
        {
            return "Versionamento : 2.0";
        }
    }
}