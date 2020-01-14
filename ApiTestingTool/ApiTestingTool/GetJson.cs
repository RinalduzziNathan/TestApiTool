using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;

namespace ApiTestingTool
{
    class GetJson
    {


        private string GetValue = "";
        public string _url { get; set; }

        public GetJson(string url)
        {
            _url = url;
        }

        public string FetchTheUrl()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
                request.Method = "GET";

                HttpWebResponse response = null;
                response = (HttpWebResponse)request.GetResponse();

                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        GetValue = reader.ReadToEnd();
                    }
                }
                MainPage.debug(response.StatusCode.ToString());

                //parse the string in json for better visual output

                //try to parse the sting in json, if it works return the json
                try
                {
                    JObject json = JObject.Parse(GetValue);
                    return json.ToString();
                }
                catch//if the parse crash, juste return the string
                {
                    return GetValue;
                }
            }
            catch (System.Exception Error)//warning
            {
                MainPage.debug(Error.Message);
                return "You did not enter a good URL";
            }
        }
    }
}
