using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xstorage.Data;
using Xstorage.Entities.Models;
using Xstorage.Entities.ViewModels;
using Xstorage.Shared;
using Xstorage.Repositories;

namespace Xstorage.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly XstorageDbContext context;
        private readonly UserRepository userRepository;
        ILogger<UserController> logger;

        public UserController(ILogger<UserController> logger,
        UserManager<User> userManager,
        XstorageDbContext context,
        UserRepository userRepository)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.context = context;
            this.userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignIn([FromForm] SignInViewModel signInUserData)
        {
            if (!ModelState.IsValid)
            {
                return View(signInUserData);
            }

            bool isOk = await LoginUserAsync(signInUserData);
            if (!isOk)
            {
                return View(signInUserData);
            }

            logger.LogInformation("{string} logon at {date}", signInUserData.Email, DateTime.Now);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUp([FromForm] SignUpViewModel signUpUserData)
        {
            IEnumerable<IdentityError> errors = await userRepository.CreateUserAsync(signUpUserData);
            if (errors.Any())
            {
                string errorDescription = errors.ToList().First().Description;
                logger.LogError("Unable create user {string}", errorDescription);
                ModelState.AddModelError("create_err", errorDescription);
                return View(signUpUserData);
            }

            bool loginUserOk = await LoginUserAsync(new SignInViewModel()
            {
                Email = signUpUserData.Email,
                Password = signUpUserData.Password
            });

            if (!loginUserOk)
            {
                logger.LogError("Unable login user");
                return View(signUpUserData);
            }

            

            logger.LogInformation("{string} registered at {date}", signUpUserData.Email, DateTime.Now);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Login user. Makes validation of data about user
        /// </summary>
        /// <param name="login"></param>
        /// <returns>true - if user is correct. Else false</returns>
        private async Task<bool> LoginUserAsync(SignInViewModel login)
        {
            if (login == null)
            {
                ModelState.AddModelError(string.Empty, "Login user is null");
                return false;
            }

            User? user = await userRepository.GetUserAsync(x => x.Email == login.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User does not exists");
                return false;
            }

            if (!(await userManager.CheckPasswordAsync(user, login.Password)))
            {
                ModelState.AddModelError(string.Empty, "Password is incorrect");
                return false;
            }

            IList<string> roles = await userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login.Email),
                new Claim(ClaimTypes.Role, "Simple"),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme
            );

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return true;
        }
    }
}
