using System.Collections.Generic;
using System;

namespace MidMarket.Entities.Response
{
    public class CaptchaResponse
    {
        [Newtonsoft.Json.JsonProperty("success")]
        public bool Success { get; set; }

        [Newtonsoft.Json.JsonProperty("challenge_ts")]
        public DateTime ChallengeTimestamp { get; set; }

        [Newtonsoft.Json.JsonProperty("hostname")]
        public string Hostname { get; set; }

        [Newtonsoft.Json.JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}
