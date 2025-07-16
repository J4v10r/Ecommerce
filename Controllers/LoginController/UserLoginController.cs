using Microsoft.AspNetCore.Mvc;
using Saas.Services.UserServices;

namespace Saas.Controllers.LoginController
{
    public class UserLoginController : Controller
    {

        private readonly ILogger<UserLoginController> _logger;
        private readonly IUserAuthService _userAuthService;

        public UserLoginController(ILogger<UserLoginController> logger, IUserAuthService userAuthService){
            _logger = logger;
            _userAuthService = userAuthService;
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
