using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace ApiTestingTool
{
    class GetJson
    {
        private string _GetValue = string.Empty;
        private string _errorMessage;
        public string _url { get; set; }

        public GetJson(string url)
        {
            _url = url;
            _GetValue = "Waiting";
        }

        public async Task<string> FetchTheUrl()
        {

            Task<string> taskWeb = new Task<string>(ComputeWebRequest);
            taskWeb.Start();

            _GetValue = await taskWeb;//wait the task ends

            MainPage.debug(_errorMessage);
            try//try to parse the sting in json, if it works return the json
            {
                JObject json = JObject.Parse(_GetValue);//parse the string in json for better visual output
                return json.ToString();
            }
            catch//if the parse crashs, juste return the string
            {
                return _GetValue;
            }
        }

        //the logic of the webrequest launched in async
        private string ComputeWebRequest()
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
                        _GetValue = reader.ReadToEnd();
                    }
                }
                _errorMessage = response.StatusCode.ToString();
                
                return _GetValue;
            }  
            catch (System.Exception Error)//warning
            {
                _errorMessage = Error.Message;
                return "You did not enter a good URL";
            }
        }
    }
}
