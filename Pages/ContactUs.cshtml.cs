using AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Net;
using Home.Services;
using static Google.Rpc.Context.AttributeContext.Types;
namespace Home.Pages
{
    public class ContactUsModel : PageModel
    {
        private readonly ILogger<ContactUsModel> _logger;
        public bool _isCaptchaValid { get; set; }

        public ContactUsModel(ILogger<ContactUsModel> logger)
        {
            _logger = logger;
            _isCaptchaValid = false;

        }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                if (!captchaValidate(Request.Form["g-recaptcha-response"]))
                {
                    ModelState.AddModelError(string.Empty, "You failed the CAPTCHA.");
                    return Page();
                }
            }

            return Page();

        }

        private bool captchaValidate(string gRecaptchaResponse)
        {
            //HttpClient httpClient = new HttpClient();

            //var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret=6LfNjcMpAAAAAOu20s2OfFRj-vKuAi3x0ZoGlUvU={gRecaptchaResponse}").Result;

            //if (res.StatusCode != HttpStatusCode.OK)
            //{
            //    return false;
            //}
            //string JSONres = res.Content.ReadAsStringAsync().Result;
            //dynamic JSONdata = JObject.Parse(JSONres);

            //if (JSONdata.success != "true" || JSONdata.score <= 0.5m)
            //{

            //    return false;
            //}
            //_isCaptchaValid = true;
            //return true;

            var assessment = new GoogleCreateAssessment("sample-app-in-azure", "6LfNjcMpAAAAAAy6svoJhA3qN8nM5HFDtdRJyjeI", gRecaptchaResponse, "submit");
            var CustomerAssesment = assessment.createAssessment();

            // Check if the token is valid.
            if (CustomerAssesment.TokenProperties.Valid == false)
            {
                ModelState.AddModelError(string.Empty, "The CreateAssessment call failed because the token was: " +
                    CustomerAssesment.TokenProperties.InvalidReason.ToString());
                return false;
            }

            return true;

            //// Check if the expected action was executed.
            //if (response.TokenProperties.Action != _recaptchaAction)
            //{
            //    System.Console.WriteLine("The action attribute in reCAPTCHA tag is: " +
            //        response.TokenProperties.Action.ToString());
            //    System.Console.WriteLine("The action attribute in the reCAPTCHA tag does not " +
            //        "match the action you are expecting to score");
            //    return;
            //}

            //// Get the risk score and the reasons.
            //// For more information on interpreting the assessment,
            //// see: https://cloud.google.com/recaptcha-enterprise/docs/interpret-assessment
            //System.Console.WriteLine("The reCAPTCHA score is: " + ((decimal)response.RiskAnalysis.Score));

            //foreach (RiskAnalysis.Types.ClassificationReason reason in response.RiskAnalysis.Reasons)
            //{
            //    System.Console.WriteLine(reason.ToString());
        }

    }
}
