namespace MySurveys.Web.ViewModels.Account
{
    using System.Collections.Generic;
    using System.Configuration;
    using Newtonsoft.Json;

    public class ReCaptcha
    {
        private string m_Success;
        private List<string> m_ErrorCodes;

        [JsonProperty("success")]
        public string Success
        {
            get { return this.m_Success; }
            set { this.m_Success = value; }
        }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes
        {
            get { return this.m_ErrorCodes; }
            set { this.m_ErrorCodes = value; }
        }

        public static bool Validate(string EncodedResponse)
        {
            var client = new System.Net.WebClient();

            string privateKey = ConfigurationManager.AppSettings["ReCaptchaPrivateKey"];

            var GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", privateKey, EncodedResponse));

            var captchaResponse = JsonConvert.DeserializeObject<ReCaptcha>(GoogleReply);

            return captchaResponse.Success == "true";
        }
    }
}