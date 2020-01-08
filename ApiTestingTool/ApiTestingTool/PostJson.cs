﻿using System;
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
                    _error = true;
                    return Error.Message;
                }
            });
            if (_error)
            {
                MainPage.debug("Error ! "+ResponseOfTheServer);
            }
            return ResponseOfTheServer;
        }
        private void ComputeWebRequest()
        {

        }
    }
}
