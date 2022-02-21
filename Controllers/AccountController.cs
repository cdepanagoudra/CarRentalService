using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CAR_RENTAL_SERVICE.Infrastructure;
using CAR_RENTAL_SERVICE.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Car_Rental_service.Controllers
{
    
    public class AccountController : Controller
    {
        IUserService _userService;  //add the "using SampleApi.Infrastructure" directive
        public AccountController(IUserService service) => _userService = service;
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn( User model)
        {
            //Check whether all the properties are filled with values and 
            //sent from the client side. All the Required properties should 
            //have values. Else throw error;
            if (!ModelState.IsValid)
                return BadRequest();
            //Invoke the Authenticate Method which will hit the DB and return bool status
            var signInStatus = _userService.Authenticate( model);
            if (signInStatus == false) //user does not exist.
            {
                return NotFound();
            }
            /*************** CHANGES: ****************************************
            Included a call to GetUserRole to get the user role 
            ******************************************************************/
            var role = _userService.GetUserRole(model.Id);
            //BUild a claims Identity and SignIn the User as was done in the Login();
            /*************** CHANGES TO THE CLAIMS ************************************
            *  The claim types are updated to reflect the application requirements. 
            * The first claim added is for the userName, 
            * the second claim is for the NameIdentifier or Id 
            * the third claim is for the Role to which the user belongs.
            ************************************************************************/
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.UserName),
                new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                new Claim(ClaimTypes.Role, role.RoleName)
            };
            /******************** END OF CHANGES  *************************************/
            var claimsIdentity = new ClaimsIdentity(
                claims: claims,
                authenticationType: CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true
            };
            await HttpContext.SignInAsync(
                scheme: CookieAuthenticationDefaults.AuthenticationScheme,
                principal: new ClaimsPrincipal(claimsIdentity),
                properties: authProperties
            );
            // return Ok();
           // return Redirect("/DriverDetail/Create");
            return Redirect("/Car/Get");
        }
        [HttpGet("SignOut")]
        new public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
        // static Dictionary<string, string> users = new Dictionary<string, string>{
        //    {"admin", "admin"}, {"user12", "user12"}
        // };
    }
}