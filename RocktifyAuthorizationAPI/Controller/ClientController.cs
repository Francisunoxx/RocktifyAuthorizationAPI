using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RocktifyAuthorizationAPI.Controller
{
    [RoutePrefix("api/client")]
    public class ClientController : ApiController
    {
        [Authorize]
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/
            return Ok("authorize");
        }
    }
}
