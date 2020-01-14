using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiTestingTool
{
    class PostJson
    {
        public string _url { get; set; }
        public string _json { get; set; }
        private bool _error;//track if the try/catch threw an error

        public PostJson(string url, string json)
        {
            _error = false;
            _url = url;
            _json = json;
        }

        public async Task<string> PostTheJson()
        {
            //Lambda anonymous async
            string ResponseOfTheServer = await Task.Run( () =>
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
                    request.ContentType = "application/json";
                    request.Method = "POST";

                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(_json);
                    }
                    var httpResponse = (HttpWebResponse)request.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        return ("Result of the json posted : "+result +" Status : "+ httpResponse.StatusDescription.ToString());
                    }
                }
                catch (System.Exception Error)//if crash => return the error message 
                {
                    string result = streamReader.ReadToEnd();
                    MainPage.debug(httpResponse.StatusDescription.ToString());
                    //try to parse the sting in json, if it works return the json
                    try
                    {
                        JObject json = JObject.Parse(result);
                        return json.ToString();
                    }
                    catch//if the parse crash, juste return the string
                    {
                        return result;
                    }
                }
                
            }   
            catch (System.Exception Error)
            {
                MainPage.debug("Error ! "+ResponseOfTheServer);
            }
            return ResponseOfTheServer;
        }
      
    }
}
