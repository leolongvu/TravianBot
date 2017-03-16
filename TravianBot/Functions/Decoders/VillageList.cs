using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravianBot.Library;
using HtmlAgilityPack;

namespace TravianBot.Functions.Decoders
{
    class VillageList
    {
        private string exception;

        private List<Village> villageList = new List<Village>();

        public VillageList()
        {
            villageList = new List<Village>();
        }

        public string getException()
        {
            return exception;
        }

        public Village getVillage(int index)
        {
            if (villageList[index] != null)
            {
                return villageList[index];
            }
            return null;
        }

        public int villageCount()
        {
            return villageList.Count;
        }

        public void villageListDecode(HtmlDocument doc)
        {
            try
            {
                var node = doc.GetElementbyId("sidebarBoxVillagelist");
                var childNode = node.Descendants("ul");
                foreach (HtmlNode parsingParent in childNode)
                {
                    var villageNode = parsingParent.Descendants("li");
                    foreach (HtmlNode parsing in villageNode)
                    {
                        string title = parsing.Attributes["title"].Value.Split('&')[0].TrimEnd();
                        var aNode = parsing.SelectSingleNode("./a");
                        string reference = aNode.Attributes["href"].Value.Split('&')[0];
                        int id = Convert.ToInt32(reference.Split('=')[1]);
                        bool active = false;
                        if (aNode.Attributes["class"].Value.Equals("active"))
                        {
                            active = true;
                        }
                        Village village = new Village(title, id, SqlMap.getCoordinatefromVillageID(id));
                        village.setReference(reference);
                        village.setTileID(SqlMap.getTileIDfromVillageID(village.getID()));
                        village.setActive(active);
                        villageList.Add(village);
                    }
                }
                exception = "villagePass";
            }
            catch
            {
                exception = "villageError";
            }       
        }
    }
}
