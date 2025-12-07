using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest()
        {
            return NotFound(); //404
        }
        [HttpGet("servererror")]
        public IActionResult GetServerErrorRequest()
        {
            throw new Exception(); 
            return Ok();  // 500
        }
        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(); //400
        }
        [HttpGet("badrequest/{id}")]
        public IActionResult GetBadRequest(int id)
        {
            return BadRequest(); //400
        }
        [HttpGet("unauthorized")]
        public IActionResult UnauthorizedRequest()
        {
            return Unauthorized(); //400
        }
    }
}
