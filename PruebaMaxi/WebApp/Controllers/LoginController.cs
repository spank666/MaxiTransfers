using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApp.Dtos;
using WebApp.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        #region Variables
        private IConfiguration _config;
        private ILoginService _loginService;
        #endregion

        #region Constructor
        public LoginController(IConfiguration config, ILoginService loginService)
        {
            _config = config;
            _loginService = loginService;
        }
        #endregion
        // POST api/<LoginController>
        [HttpPost]
        public async Task<IActionResult> Post(CredentialDto credential)
        {

            ResponseDto<string> response = new ResponseDto<string>();

            if (await _loginService.ValidateUser(credential))
            {
                response.Code = 100;
                response.Message = "Usuario autenticado correctamente";
                response.Result = generateToken(credential);
            }
            else
            {
                response.Code = 200;
                response.Message = "El usuario o la contraseña no son validos";
            }

            return Ok(response);

            
        }

        private string generateToken(CredentialDto credential)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("User", credential.User));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt16(_config["Jwt:DurationInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);


            return stringToken;
        }
    }
}
