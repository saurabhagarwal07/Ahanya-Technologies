using AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Mvc;

namespace Home.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ValidateReCaptcha]
        [HttpPost]
        public IActionResult ContactUs()
        {
            //if (!ModelState.IsValid)
            //{
            //    return;
            //}
            ////Read SMTP settings from AppSettings.json.
            //string host = this.Configuration.GetValue("EmailConfiguration:SmtpServer");
            //int port = this.Configuration.GetValue("EmailConfiguration:Port");

            return View();
        }
    }
}
