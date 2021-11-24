using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAssessment2.Web.Controllers
{
    [Authorize]
    public class ImagesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("Images");
        }
    }
}