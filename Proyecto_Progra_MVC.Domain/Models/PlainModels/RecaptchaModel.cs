using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Domain.Models.PlainModels
{
    public class RecaptchaModel
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("challenge_ts")]
        public DateTime ChallengeTimespan { get; set; }

        public string HostName { get; set; }

        [JsonPropertyName("error-codes")]
        public string[] ErrorCodes { get; set; }
    }
}
