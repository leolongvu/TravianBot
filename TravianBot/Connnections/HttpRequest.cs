using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace TravianBot.Connnections
{
    class HttpRequest
    {
        private static CookieContainer cookie = new CookieContainer();

        public static string sendPostRequest(string req, string url, string refer)
        {
            var requestData = Encoding.UTF8.GetBytes(req);
            string content = string.Empty;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.CookieContainer = cookie;
                request.Method = "POST";

                //New
                request.Proxy = null;
                request.Timeout = 30000;
                //KeepAlive is True by default

                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:42.0) Gecko/20100101 Firefox/42.0";

                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.Referer = refer;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = requestData.Length;

                Stream s = request.GetRequestStream();
                {
                    s.Write(requestData, 0, requestData.Length);
                }
                s.Close();

                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                cookie.Add(resp.Cookies);

                StreamReader stream = new StreamReader(resp.GetResponseStream());
                content = stream.ReadToEnd();

                resp.Close();
                stream.Close();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    WebResponse resp = e.Response;
                    using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }
            return content;
        }

        public static string sendGetRequest(string url, string host, bool keepAlive)
        {
            string content = string.Empty;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                request.Proxy = null;
                request.Timeout = 30000;
                request.Host = host;

                request.KeepAlive = keepAlive;
                request.UserAgent = "Mozilla / 5.0(Windows NT 10.0; WOW64; rv:42.0) Gecko/20100101 Firefox/42.0";

                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.CookieContainer = cookie;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var stream = new StreamReader(response.GetResponseStream());
                content = stream.ReadToEnd();

                response.Close();
                stream.Close();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse resp = (HttpWebResponse)e.Response;
                    int statCode = (int)resp.StatusCode;

                    if (statCode == 403)
                    {
                        content = "403";
                    }
                    else
                    {
                        using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                        {
                            content = sr.ReadToEnd();
                        }
                    }
                }
            }
            return content;
        }       
    }
}
