﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ControllerBases;
using Shared.Dtos;

namespace FakePayment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment()
        {
            return CreateActionResultInstance(Response<NoContent>.Success(200));
        }
    }
}
