using JsonWebTokenExample.Logic;
using JsonWebTokenExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace JsonWebTokenExample.Controllers
{
    [ApiController]
    public class JsonWebTokenController : ControllerBase
    {
        private readonly ILogger<JsonWebTokenController> _logger;
        private readonly IJsonWebTokenLogic _logic;

        public JsonWebTokenController(ILogger<JsonWebTokenController> logger, IJsonWebTokenLogic logic)
        {
            _logger = logger;
            _logic = logic;
        }

        [Route("/api/v1/core/generatetoken")]
        [ProducesResponseType(200, Type = typeof(Response<string>))]
        [HttpPost]
        public ActionResult GetNewToken([FromBody] TokenRequest request)
        {
            string token = string.Empty;

            try
            {
                token = _logic.GenerateToken(request.userName, request.password);

                if (token == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return StatusCode((500), new Response<string>
                {
                    Status = "ERROR",
                    ResponseMessage = "ERROR",
                    ResponseData = e.Message
                });
            }

            Token userToken = new Token()
            {
                userToken = token
            };

            return Ok(new Response<Token>
            {
                Status = "200",
                ResponseMessage = "Data Retrieved Successfully",
                ResponseData = userToken
            });
        }

        [Route("/api/v1/core/validateToken")]
        [ProducesResponseType(200, Type = typeof(Response<string>))]
        [HttpPost]
        public ActionResult ValidateToken([FromBody] ValidateTokenRequest validateTokenRequest)
        {
            try
            {
                bool result = _logic.ValidateToken(validateTokenRequest.token);

                if (result == false)
                {
                    return StatusCode((500), new Response<string>
                    {
                        Status = "ERROR",
                        ResponseMessage = "ERROR",
                        ResponseData = "Token Not Valid"
                    });
                }
            }
            catch (Exception e)
            {
                return StatusCode((500), new Response<string>
                {
                    Status = "ERROR",
                    ResponseMessage = "ERROR",
                    ResponseData = e.Message
                });
            }
            return StatusCode((200), new Response<bool>
            {
                Status = "Token Is Valid",
                ResponseMessage = "Token Is Valid",
                ResponseData = true
            });
        }
    }
}
