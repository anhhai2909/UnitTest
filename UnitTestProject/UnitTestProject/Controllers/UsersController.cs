using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UnitTestProject.Interfaces;
using UnitTestProject.Models;

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
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(User user)
        {
            var item = _userRepository.GetUserByID(user.Id);
            if(item == null)
            {
                return BadRequest(ModelState);
            }
            item.PhoneNum = user.PhoneNum;  
            item.Un = user.Un;
            item.Email = user.Email;
            item.Pw = user.Pw;
            return _userRepository.UpdateUser(item) == true ? Ok() : BadRequest(ModelState);
        }
        [HttpPost("CreateUser")]
        public IActionResult CreateUser(User user)
        {
            var item = _userRepository.GetUserByID(user.Id);
            if (item != null)
            {
                return BadRequest(ModelState);
            }
            return _userRepository.CreateUser(user) == true ? Ok() : BadRequest(ModelState);
        }
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            var item = _userRepository.GetUserByID(id);
            if (item == null)
            {
                return BadRequest(ModelState);
            }
            return _userRepository.DeleteUser(id) == true ? Ok() : BadRequest(ModelState);
        }
    }
}
