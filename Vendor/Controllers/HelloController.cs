using Microsoft.AspNetCore.Mvc;

namespace Vendor.Controllers
{
    public class HelloController : Controller
    {
        public IActionResult Index()
        {
            var uid = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(uid)) return Redirect("http://localhost:5231");
            return Ok("hello there!, " + uid);
        }
    }
}
