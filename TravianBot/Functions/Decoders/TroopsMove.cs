using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using TravianBot.Library;
using System.Diagnostics;

namespace TravianBot.Functions.Decoders
{
    class TroopsMove
    {
        private string exception;
        public static string test = "";
        public int getRequestNumber(int number)
        {
            return Convert.ToInt32(Math.Ceiling((double)number / 10));
        }

        private int getTroopType(string type)
        {
            int troopType;
            switch (type)
            {
                case "inReturn": //Back from attacks
                    troopType = 1;
                    break;
                case "inHero": //Back from adventure
                    troopType = 2;
                    break;
                case "inSupply": //Reinforcement
                    troopType = 3;
                    break;
                case "outAttack": //Normal aatack
                    troopType = 4;
                    break;
                case "outRaid": //Raid
                    troopType = 5;
                    break;
                case "outHero": //Hero on adventure
                    troopType = 6;
                    break;
                case "outSpy": //Scouting
                    troopType = 7;
                    break;
                case "outSupply": //Troop out for reinforcement
                    troopType = 8;
                    break;
                default:
                    troopType = 0; //Troop gain from adventure & Imprisioned
                    break;
            }
            return troopType;
        }       

        public int troopCount(HtmlDocument doc)
        {
            try
            {
                var numberNode = doc.DocumentNode.SelectSingleNode("//div[@class='data']/h4");
                int count = Convert.ToInt32(numberNode.InnerText.Split('(')[1].Split(')')[0]);
                return count;
            }
            catch
            {
                return -1;
            }
        }

        public string troopDecode(HtmlDocument doc, Village village)
        {       
            //try 
            {                               
                var node = doc.DocumentNode.SelectNodes("//div[@class='data']/table");
                if (node != null)
                {
                    foreach (HtmlNode wave in node)
                    {
                        Troops[] troopsWave = new Troops[11];
                        int i = 0;
                        int j = 0;
                        int wood = 0;
                        int clay = 0;
                        int iron = 0;
                        int grain = 0;
                        int total = 0;
                        int capacity = 0;
                        int ownRef = 0;
                        int desRef = 0;
                        int x = 0;
                        int y = 0;
                        string timer = "";
                        string type = wave.Attributes["class"].Value.Split(' ')[1];
                        var child = wave.Descendants("td");
                        if (child != null)
                        {
                            foreach (HtmlNode grandChild in child)
                            {
                                if (grandChild.Attributes["class"] != null)
                                {
                                    switch (grandChild.Attributes["class"].Value)
                                    {
                                        case "role":
                                            if (type != "")
                                            {
                                                HtmlNode aNode = grandChild.SelectSingleNode("./a");
                                                ownRef = Convert.ToInt32(aNode.Attributes["href"].Value.Split('=')[1]);
                                                if (!ownRef.Equals(village.getTileID()))
                                                {
                                                    return "villageError";
                                                }
                                            }
                                            break;
                                        case "troopHeadline":
                                            var troopRef = grandChild.SelectSingleNode("./a");
                                            var coorNode = troopRef.Descendants("span");
                                            foreach (HtmlNode coorxNode in coorNode)
                                            {
                                                switch (coorxNode.Attributes["class"].Value)
                                                {
                                                    case "coordinateX":
                                                        string[] parsex = coorxNode.InnerHtml.Split(';');
                                                        string signx = parsex[1];
                                                        if (signx.Equals("&#43"))
                                                        {
                                                            x = Convert.ToInt32(parsex[3].Split('&')[0]);
                                                        }
                                                        else
                                                        {
                                                            x = 0 - Convert.ToInt32(parsex[3].Split('&')[0]);
                                                        }
                                                        break;
                                                    case "coordinateY":
                                                        string[] parsey = coorxNode.InnerHtml.Split(';');
                                                        string signy = parsey[1];
                                                        if (signy.Equals("&#43"))
                                                        {
                                                            y = Convert.ToInt32(parsey[3].Split('&')[0]);
                                                        }
                                                        else
                                                        {
                                                            y = 0 - Convert.ToInt32(parsey[3].Split('&')[0]);
                                                        }
                                                        break;
                                                }                                                                                              
                                            }                                          
                                            desRef = Convert.ToInt32(troopRef.Attributes["href"].Value.Split('=')[1]);
                                            break;
                                        case "uniticon":
                                        case "uniticon last":
                                            var troopID = grandChild.Descendants("img").ToArray()[0];
                                            string ID = troopID.Attributes["class"].Value.Split(' ')[1];
                                            troopsWave[i] = new Troops(ID);
                                            i++;
                                            break;
                                        case "unit":
                                        case "unit none":
                                        case "unit none last":
                                            int numberOfTroops = Convert.ToInt32(grandChild.InnerText);
                                            troopsWave[j].setTroopsNo(numberOfTroops);
                                            j++;
                                            break;
                                    }
                                }
                                else if (grandChild.Attributes["colspan"] != null)
                                {
                                    if (grandChild.Attributes["colspan"].Value.Equals("11"))
                                    {
                                        var divNodes = grandChild.Descendants("div");
                                        foreach (HtmlNode div in divNodes)
                                        {
                                            if (div.Attributes["class"] != null)
                                            {
                                                switch (div.Attributes["class"].Value)
                                                {
                                                    case "res":
                                                        var resources = div.Descendants("span");
                                                        foreach (HtmlNode res in resources)
                                                        {
                                                            int amount = Convert.ToInt32(res.InnerText);
                                                            HtmlNode imgNode = res.SelectSingleNode("./img");
                                                            switch (imgNode.Attributes["class"].Value)
                                                            {
                                                                case "r1":
                                                                    wood = amount;
                                                                    break;
                                                                case "r2":
                                                                    clay = amount;
                                                                    break;
                                                                case "r3":
                                                                    iron = amount;
                                                                    break;
                                                                case "r4":
                                                                    grain = amount;
                                                                    break;
                                                            }
                                                        }
                                                        break;
                                                    case "carry":
                                                        string[] carry = div.InnerText.Split('/');
                                                        total = Convert.ToInt32(carry[0]);
                                                        capacity = Convert.ToInt32(carry[1]);
                                                        break;
                                                    case "in":
                                                        HtmlNode spanNode = div.SelectSingleNode("./span");
                                                        timer = spanNode.InnerText;
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        TroopsWave newWave = new TroopsWave(getTroopType(type), desRef, troopsWave);
                        newWave.setDesName(SqlMap.getNanefromTileID(newWave.getDesID()));
                        if (x == 0 && y == 0)
                        {
                            newWave.setCoordinates(SqlMap.getCoordinatefromTileID(newWave.getDesID()));
                        }
                        else
                        {
                            newWave.setCoordinates(new Coordinates(x, y));
                        }
                        newWave.setWoodCarry(wood);
                        newWave.setClayCarry(clay);
                        newWave.setIronCarry(iron);
                        newWave.setGrainCarry(grain);
                        newWave.setTotal(total);
                        newWave.setCapacity(capacity);

                        village.addTroopWave(newWave);
                    }                    
                }
                exception = "";
            }
            //catch
            //{
            //    exception = "troopError";
            //}
            return exception;
        }

        public static string getTypeString(int type)
        {
            string toReturn = "";
            switch (type)
            {
                case 0:
                    toReturn = "Adventure Troops, Imprisioned";
                    break;
                case 1:
                    toReturn = "Return from attacks";
                    break;
                case 2:
                    toReturn = "Return from adventure";
                    break;
                case 3:
                    toReturn = "Reinforcement";
                    break;
                case 4:
                    toReturn = "Attack";
                    break;
                case 5:
                    toReturn = "Raid";
                    break;
                case 6:
                    toReturn = "Hero on adventure";
                    break;
                case 7:
                    toReturn = "Scouting";
                    break;
                case 8:
                    toReturn = "Reinforce other villages";
                    break;
            }
            return toReturn;
        }
    }
}
