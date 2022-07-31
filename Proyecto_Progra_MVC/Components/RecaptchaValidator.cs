using Microsoft.Extensions.Options;
using Proyecto_Progra_MVC.Contracts;
using Proyecto_Progra_MVC.Domain.Models.ConfigurationModels;
using Proyecto_Progra_MVC.Domain.Models.PlainModels;
using System;
using System.Net;
using System.Text.Json;

namespace Proyecto_Progra_MVC.Components
{
    public class RecaptchaValidator : IRecaptchaValidator
    {
        RecaptchaConfiguration Configuration;
        public RecaptchaValidator(IOptions<RecaptchaConfiguration> configuration)
        {
            Configuration = configuration.Value;
        }
        public bool Validate(string token)
        {
            string address = string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", Configuration.SecretKey, token);

            Uri uri = new Uri(address);

            try
            {
                WebClient client = new WebClient();
                string result = client.DownloadString(uri);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                RecaptchaModel model = JsonSerializer.Deserialize<RecaptchaModel>(result, options);

                if (!model.Success)
                {
                    string exceptionMessage = string.Empty;
                    foreach (var ErrorCode in model.ErrorCodes)
                    {
                        exceptionMessage += string.Concat(ErrorCode, "\n");
                    }
                    throw new Exception(exceptionMessage);
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
