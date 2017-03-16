using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using TravianBot.Connnections;

namespace TravianBot.Functions
{
    class Login
    {
        private string server;
        private string username;
        private string password;

        private string tribe;
        private string tribeCode;
        private string respond;

        public Login(string server, string username, string password)
        {
            this.server = server;
            this.username = username;
            this.password = password;
        }

        private string getLanguage()
        {
            string[] split = server.Split('.');
            try
            {
                return split[3]; //ts5.travian.co.uk returns uk
            }
            catch
            {
                return "en"; //ts5.travian.com return en
            }
        }

        public string getRespond()
        {
            return respond;
        }

        public string loginRequest()
        {
            string loginRespond = "";
            this.respond = "";

            string url = "http://" + server + "/?lang=" + getLanguage();
            string respond = HttpRequest.sendGetRequest(url, server, true);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(respond);

            string w = null;
            string login = null;

            IEnumerable<HtmlNode> forms = doc.DocumentNode.Descendants("form");
            HtmlNode parent = null;

            foreach (HtmlNode node in forms)
            {
                if (node.Attributes["name"] != null && node.Attributes["name"].Value == "login")
                {
                    parent = node.ParentNode;
                    break;
                }
            }

            if (parent != null)
            {
                IEnumerable<HtmlNode> pNodes = parent.Descendants("input");
                foreach (HtmlNode child in pNodes)
                {
                    if (child.Attributes["type"] != null && child.Attributes["type"].Value == "hidden")
                    {
                        switch (child.Attributes["name"].Value)
                        {
                            case "w":
                                w = child.Attributes["value"].Value;
                                break;
                            case "login":
                                login = child.Attributes["value"].Value;
                                break;
                        }
                    }
                }
            }

            if (w != null && login != null)
            {
                string request = string.Format("name={0}&password={1}&w={2}&login={3}", username,
                password, w, login);
                loginRespond = HttpRequest.sendPostRequest(request, "http://" + server + "/dorf1.php", "http://" + server + "/logout.php");
            }

            string loginStatus = "";

            if (loginRespond.Equals(""))
            {
                loginStatus = "connection";
            }
            else if (loginRespond.IndexOf("Login does not exist") != -1)
            {
                loginStatus = "username";
            }
            else if (loginRespond.IndexOf("Incorrect password") != -1)
            {
                loginStatus = "password";
            }
            else
            {
                HtmlDocument loginDoc = new HtmlDocument();
                loginDoc.LoadHtml(loginRespond);
                tribe = loginDoc.DocumentNode.SelectSingleNode("//div[@class='playerName']/img").Attributes["title"].Value;
                tribeCode = loginDoc.DocumentNode.SelectSingleNode("//div[@class='playerName']/img").Attributes["class"].Value.Split(' ')[1];
                loginStatus = tribe + ";" + tribeCode;
                this.respond = loginRespond;
            }

            return loginStatus;
        }
    }
}
