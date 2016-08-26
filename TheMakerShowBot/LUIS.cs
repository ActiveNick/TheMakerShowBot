using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TheMakerShowBot
{
    // Generic class used to parse user messages with LUIS from Microsoft Cognitive Services
    public class LUISMakerShowClient
    {
        public static async Task<MakerLUIS> ParseUserInput(string strInput)
        {
            string strRet = string.Empty;
            string strEscaped = Uri.EscapeDataString(strInput);

            using (var client = new HttpClient())
            {
                // TO DO: Replace the application ID and the subscription key with the one from your own account since I probably changed mine since posting this
                string uri = "https://api.projectoxford.ai/luis/v1/application?id=e86d239e-7bd2-40ac-8881-03fbd8aa0d29&subscription-key=26e9e3fec3904829983742aa809770e6&q=" + strEscaped;
                HttpResponseMessage msg = await client.GetAsync(uri);

                if (msg.IsSuccessStatusCode)
                {
                    var jsonResponse = await msg.Content.ReadAsStringAsync();
                    var _Data = JsonConvert.DeserializeObject<MakerLUIS>(jsonResponse);
                    return _Data;
                }
            }
            return null;
        }
    }

    public class MakerLUIS
    {
        public string query { get; set; }
        public lIntent[] intents { get; set; }
        public lEntity[] entities { get; set; }
    }

    public class lIntent
    {
        public string intent { get; set; }
        public float score { get; set; }
    }

    public class lEntity
    {
        public string entity { get; set; }
        public string type { get; set; }
        public int startIndex { get; set; }
        public int endIndex { get; set; }
        public float score { get; set; }
    }
}