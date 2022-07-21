using DashboarLaboral.Core.Aplicacion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DashboarLaboral.Controllers
{
    public class StartController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            IActionResult actionResult = null;

            if(User.IsInRole(AccessRoles.Dashboard))
            {
                actionResult = RedirectToAction("Index", "Home");
            } 
            else
            {
                actionResult = View();
            }
            return actionResult;
        }

    }
}
