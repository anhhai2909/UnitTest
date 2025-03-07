using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UnitTestProject.Interfaces;

namespace UnitTestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet("GetUserList")]
        public IActionResult GetUserList() { 
            var item = _userRepository.GetUserList();
            return ModelState.IsValid ? Ok(item) : BadRequest(ModelState);
        }
        [HttpGet("GetUserByID/{id}")]
        public IActionResult GetUserByID(short id)
        {
            var item = _userRepository.GetUserByID(id);
            return ModelState.IsValid ? Ok(item) : BadRequest(ModelState);
        }
    }
}
