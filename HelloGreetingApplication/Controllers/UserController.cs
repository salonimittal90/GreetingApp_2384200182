using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model.UserModel;

namespace HelloGreetingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL _userBL;  // ✅ Business Layer Dependency
        private readonly JwtService _jwtService;    

        public UserController(IUserBL userBL, JwtService jwtService)
        {
            _userBL = userBL;
            _jwtService = jwtService;
        }

        // ✅ User Registration API
        [HttpPost("register")]
        public IActionResult Register(RegisterModel model)
        {
            try
            {
                var user = _userBL.Register(model);  // ✅ Calling Business Layer
                return Ok(new { message = "User registered successfully!", user });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // ✅ User Login API
        [HttpPost("login")]
        public IActionResult Login( LoginModel model)
        {
            try
            {
                var user = _userBL.Login(model);
                if (user == null)
                    return Unauthorized(new { message = "Invalid credentials" });

                var token = _jwtService.GenerateToken(user.Email);
                return Ok(new { message = "Login successful!", Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }


   /* [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] ForgetPasswordModel model)
        {
            try
            {
                var token = _userBL.GeneratePasswordResetToken(model.Email);  // ✅ Reset Token Generate Hoga
                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest(new { message = "User not found!" });
                }

                // ✅ Email Send Karne Ka Logic (Future me RabbitMQ yaha implement hoga)
                return Ok(new { message = "Password reset email sent successfully!", token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPasswordModel model)
        {
            try
            {
                bool isReset = _userBL.ResetPassword(model);
                if (!isReset)
                {
                    return BadRequest(new { message = "Invalid token or token expired!" });
                }

                return Ok(new { message = "Password reset successful!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
*/

        [HttpGet("protected")]
        [Authorize]  // ✅ Sirf JWT token wale users access kar sakenge
        public IActionResult GetProtectedData()
        {
            return Ok(new { message = "You accessed a protected API!" });
        }
    }
}