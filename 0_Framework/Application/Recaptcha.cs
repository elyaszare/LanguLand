using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace _0_Framework.Application
{
    public class Recaptcha
    {
        [JsonProperty("success")] public bool Success { get; set; }

        [JsonProperty("challenge_ts")] public string ValidatedDateTime { get; set; }

        [JsonProperty("hostname")] public string HostName { get; set; }

        [JsonProperty("error-codes")] public List<string> ErrorCodes { get; set; }
    }


    public class ReCaptchaAccount
    {
        public OperationResult CheckReCaptcha(IFormCollection form)
        {
            var operation = new OperationResult();
            var urlToPost = "https://www.google.com/recaptcha/api/siteverify";
            var secretKey = "6LebvDEhAAAAAJpq25y-MYg0vI6mUjivylteo-yU"; // change this
            string gRecaptchaResponse = form["g-recaptcha-response"];

            var postData = "secret=" + secretKey + "&response=" + gRecaptchaResponse;

            // send post data
            var request = (HttpWebRequest) WebRequest.Create(urlToPost);
            request.Method = "POST";
            request.ContentLength = postData.Length;
            request.ContentType = "application/x-www-form-urlencoded";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(postData);
            }

            // receive the response now
            var result = string.Empty;
            using (var response = (HttpWebResponse) request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }
            }
            // validate the response from Google reCaptcha

            var captChaesponse = JsonConvert.DeserializeObject<Recaptcha>(result);

            if (!captChaesponse.Success) return operation.Failed(ApplicationMessages.ReCaptchaNotVisible);

            if (captChaesponse.Success) return operation.Success();

            return operation.Success();
        }
    }
}
