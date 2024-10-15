using System;
using Google.Api.Gax.ResourceNames;
using Google.Cloud.RecaptchaEnterprise.V1;

namespace Home.Services
{
    public class GoogleCreateAssessment
    {
        // Create an assessment to analyze the risk of a UI action.
        // projectID: Google Cloud project ID.
        // recaptchaKey: reCAPTCHA key obtained by registering a domain or an app to use reCAPTCHA Enterprise.
        // token: The token obtained from the client on passing the recaptchaKey.
        // recaptchaAction: Action name corresponding to the token.
        private string _projectID { get; }
        private string _recaptchaKey { get; }
        private string _token { get; }
        private string _recaptchaAction { get; }

        public GoogleCreateAssessment(string ProjectID, string RecaptchaKey, string Token, string RecaptchaAction)
        {
            _projectID = ProjectID;
            _recaptchaKey = RecaptchaKey;
            _token = Token;
            _recaptchaAction = RecaptchaAction;
        }



        public Assessment createAssessment()
        {

            // Create the client.
            // TODO: To avoid memory issues, move this client generation outside
            // of this example, and cache it (recommended) or call client.close()
            // before exiting this method.
            RecaptchaEnterpriseServiceClient client = RecaptchaEnterpriseServiceClient.Create();

            ProjectName projectName = new ProjectName(_projectID);

            // Build the assessment request.
            CreateAssessmentRequest createAssessmentRequest = new CreateAssessmentRequest()
            {
                Assessment = new Assessment()
                {
                    // Set the properties of the event to be tracked.
                    Event = new Event()
                    {
                        SiteKey = _recaptchaKey,
                        Token = _token,
                        ExpectedAction = _recaptchaAction
                    },
                },
                ParentAsProjectName = projectName
            };

            Assessment response = client.CreateAssessment(createAssessmentRequest);

            return response;

            //// Check if the token is valid.
            //if (response.TokenProperties.Valid == false)
            //{
            //    System.Console.WriteLine("The CreateAssessment call failed because the token was: " +
            //        response.TokenProperties.InvalidReason.ToString());
            //    return;
            //}

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
