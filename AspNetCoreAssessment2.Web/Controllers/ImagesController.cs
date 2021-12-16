using AspNetCoreAssessment2.Foundation.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAssessment2.Web.Controllers
{
    public class ImagesController : Controller
    {
        [HttpGet]
        [Authorize(Policy = AdditionalUserClaims.HasReadRules)]
        public IActionResult Index()
        {
            return View("Images");
        }
    }
}