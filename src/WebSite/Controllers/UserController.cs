using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;

using BusinessLogic.Services;

namespace WebSite.Controllers
{
    [Authorize(Roles="User")]
    public class UserController : Controller
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Account()
        {
            return View();
        }
    }
}
