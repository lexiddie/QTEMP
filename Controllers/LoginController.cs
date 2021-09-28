using Microsoft.AspNetCore.Mvc;

namespace QTEMP.Controllers
{
    [Route("Login")]
    public class LoginController : Controller
    {
        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return PartialView("Index");
        }
    }
}