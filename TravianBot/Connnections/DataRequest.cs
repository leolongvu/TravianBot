using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using TravianBot.Library;

namespace TravianBot.Connnections
{
    class DataRequest
    {
        #region Dorf Requests

        public HtmlDocument dorf1Request(string server, Village village)
        {
            string url = "http://" + server + "/dorf1.php" + village.getReference();
            string respond = HttpRequest.sendGetRequest(url, server, true);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(respond);
            return doc;
        }

        public HtmlDocument dorf2Request(string server, Village village)
        {
            string url = "http://" + server + "/dorf2.php" + village.getReference();
            string respond = HttpRequest.sendGetRequest(url, server, true);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(respond);
            return doc;
        }

        #endregion

        #region Troop Request

        public HtmlDocument incomingTroopRequest(string server, int page)
        {
            string url = "http://" + server + "/build.php?gid=16&tt=1&filter=1&tt=1&subfilters=2,3,1&page=" + page;
            string respond = HttpRequest.sendGetRequest(url, server, true);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(respond);
            return doc;
        }

        public HtmlDocument outgoingTroopRequest(string server, int page)
        {
            string url = "http://" + server + "/build.php?gid=16&tt=1&filter=2&tt=1&subfilters=5,4&page=" + page;
            string respond = HttpRequest.sendGetRequest(url, server, true);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(respond);
            return doc;
        }

        public HtmlDocument inVillageTroopRequest(string server)
        {
            string url = "http://" + server + "/build.php?gid=16&tt=1&filter=3";
            string respond = HttpRequest.sendGetRequest(url, server, true);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(respond);
            return doc;
        }

        public HtmlDocument otherTroopRequest(string server)
        {
            string url = "http://" + server + "/build.php?gid=16&tt=1&filter=4";
            string respond = HttpRequest.sendGetRequest(url, server, true);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(respond);
            return doc;
        }

        #endregion
    }
}
