namespace MySurveys.Web.ViewModels.Account
{
    using System.Collections.Generic;
    using System.Configuration;
    using Newtonsoft.Json;

    public class ReCaptcha
    {
        private string m_Success;

        [JsonProperty("success")]
        public string Success
        {
            get { return m_Success; }
            set { m_Success = value; }
        }


        private List<string> m_ErrorCodes;

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes
        {
            get { return m_ErrorCodes; }
            set { m_ErrorCodes = value; }
        }

        public static bool Validate(string EncodedResponse)
        {
            var client = new System.Net.WebClient();

            string PrivateKey = ConfigurationManager.AppSettings["ReCaptchaPrivateKey"];

            var GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, EncodedResponse));

            var captchaResponse = JsonConvert.DeserializeObject<ReCaptcha>(GoogleReply);

            return captchaResponse.Success == "true";
        }
    }
}