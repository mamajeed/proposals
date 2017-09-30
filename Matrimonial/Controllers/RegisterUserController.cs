using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Matrimonial.Model;
using Matrimonial.Interface;
using Matrimonial.AES256Encryption;
using Matrimonial.Baseapi;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Matrimonial.Controllers
{
    [Route("api/[controller]")]   
    public class RegisterUserController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly IConfiguration _config;

        private IMatrimoneyRepository<RegisterUser> repoUser;
        public RegisterUserController(IMatrimoneyRepository<RegisterUser> repoUser, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IEmailSender emailSender,
        ILogger<AccountController> logger,
        IConfiguration config)
        {
            this.repoUser = repoUser;

            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _config = config;

        }

        //GET: api/values
       //[HttpGet]
       // public IEnumerable<string> Get()
       // {
       //     return new string[] { "value1", "value2" };
       // }

        //GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]RegisterUser value)
        {
            if (ValidateUsername(value))
            {
                return NotFound(new ApiResponse(409, "user already exist with username {value.Username}"));

            }


            value.AddedDate = DateTime.Now;

            // Encrypting Password with AES 256 Algorithm
            value.Password = EncryptionLibrary.EncryptText(value.Password);
            value.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            // Saving User Details in Database

            this.repoUser.Insert(value);

            return Ok(new ApiOkResponse(value));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, RegisterUser value)
        {
            this.repoUser.Update(value);
        }

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
        [NonAction]
        public bool ValidateUsername(RegisterUser registeruser)
        {

            var usercount = repoUser.GetAll().Where(s => s.Username == registeruser.Username).Count();
            
            if (usercount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetLoggedUserID(RegisterUser registeruser)
        {
            var usercount = repoUser.GetAll().Where(User =>
                              User.Username == registeruser.Username &&
                                   User.Password == registeruser.Password
                             ).FirstOrDefault().Id;

            return Convert.ToInt32(usercount);
        }







        public bool ValidateRegisteredUser(RegisterUser registeruser)
        {
            var usercount = repoUser.GetAll().Where(User =>
                              User.Username == registeruser.Username &&
                             User.Password == registeruser.Password
                             ).Count();
            if (usercount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        [HttpPost("login")]
        [AcceptVerbs("POST")]
        public ActionResult Login([FromBody]RegisterUser RegisterUser)
        {
            try
            {

                //if (string.IsNullOrEmpty(RegisterUser.Username) && (string.IsNullOrEmpty(RegisterUser.Password)))
                //{
                //    ModelState.AddModelError("", "Enter Username and Password");
                //}
                //else if (string.IsNullOrEmpty(RegisterUser.Username))
                //{
                //    ModelState.AddModelError("", "Enter Username");
                //}
                //else if (string.IsNullOrEmpty(RegisterUser.Password))
                //{
                //    ModelState.AddModelError("", "Enter Password");
                //}
                //else
                //{

                    RegisterUser.Password = EncryptionLibrary.EncryptText(RegisterUser.Password);

                    if (ValidateRegisteredUser(RegisterUser))
                    {
                        var UserID = GetLoggedUserID(RegisterUser);
                    return Ok(new ApiOkResponse(new object()));
                }
                    else
                    {
                    return NotFound(new ApiResponse(401, "user not found"));
                }
                //}

                
            }
            catch
            {
                return View();
            }
        }
    }
}
