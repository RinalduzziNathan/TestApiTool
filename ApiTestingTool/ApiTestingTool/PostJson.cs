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

        public PostJson(string url,string json)
        {
            _url = url;
            _json = json;
        }

        public string PostTheJson()
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
                    return httpResponse.StatusDescription.ToString();
                }
                
            }
            catch (System.Exception Error)
            {
                MainPage.debug(Error.Message);
                return Error.Message;
            }
            
        }
    }
}
