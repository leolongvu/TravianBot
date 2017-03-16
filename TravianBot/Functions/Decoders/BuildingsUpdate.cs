using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using TravianBot.Library;

namespace TravianBot.Functions.Decoders
{ 
    class BuildingsUpdate
    {
        public static string test = "";
        private string resException;
        private string buildException;

        public string getResException()
        {
            return resException;
        }
        public string getBuildException()
        {
            return buildException;
        }

        public void updateResourcesFields(HtmlDocument doc, Village village)
        {
            try
            {
                village.setHasBuildingUpgrade(false);

                var node = doc.DocumentNode.SelectNodes("//map[@name='rx']/area");
                var gidNode = doc.DocumentNode.SelectNodes("//div[@id='village_map']/div");

                for (int i = 0; i < node.Count; i++)
                {
                    if (node[i].Attributes["href"].Value.Split('=').Length == 2)
                    {
                        int woodNeed = 0;
                        int clayNeed = 0;
                        int ironNeed = 0;
                        int grainNeed = 0;
                        byte upgrading = 0;
                        int level = 0;
                        int id = 0;
                        int gid = 0;
                        id = Convert.ToInt32(node[i].Attributes["href"].Value.Split('=')[1]);
                        string[] stringSeparators = new string[] { "&lt;", "&gt;" };
                        string[] parsing = node[i].Attributes["title"].Value.Split(stringSeparators, StringSplitOptions.None);
                        level = Convert.ToInt32(parsing[2].Split(' ')[1]);
                        try
                        {
                            woodNeed = Convert.ToInt32(parsing[12]);
                            clayNeed = Convert.ToInt32(parsing[18]);
                            ironNeed = Convert.ToInt32(parsing[24]);
                            grainNeed = Convert.ToInt32(parsing[30]);
                            upgrading = 0;
                        }
                        catch
                        {
                            try
                            {
                                woodNeed = Convert.ToInt32(parsing[18]);
                                clayNeed = Convert.ToInt32(parsing[24]);
                                ironNeed = Convert.ToInt32(parsing[30]);
                                grainNeed = Convert.ToInt32(parsing[36]);
                                upgrading = 1;
                                village.setHasBuildingUpgrade(true);
                            }
                            catch
                            {
                                woodNeed = 0;
                                clayNeed = 0;
                                ironNeed = 0;
                                grainNeed = 0;
                                upgrading = 0;
                            }
                        }

                        try
                        {
                            gid = Convert.ToInt32(gidNode[i].Attributes["class"].Value.Split(' ')[3].Remove(0, 3));
                        }
                        catch
                        {
                            gid = Convert.ToInt32(gidNode[i].Attributes["class"].Value.Split(' ')[4].Remove(0, 3));
                        }

                        Buildings resourceField = new Buildings(id, gid);
                        resourceField.setLevel(level);
                        resourceField.setWoodNeed(woodNeed);
                        resourceField.setClayNeed(clayNeed);
                        resourceField.setIronNeed(ironNeed);
                        resourceField.setGrainNeed(grainNeed);
                        resourceField.setUpgrading(upgrading);

                        village.updateBuildings(resourceField, id - 1);
                    }
                }                             
                resException = "buildPass";
            }
            catch (Exception)
            {
                resException = "buildConnection";
            }
        }

        public void updateVillageCenter(HtmlDocument doc, Village village)
        {
            try
            {
                var parentNode = doc.DocumentNode.SelectSingleNode("//div[@id='village_map']/map");
                var node = parentNode.Descendants("area").ToList();
                var gidNode = doc.DocumentNode.SelectNodes("//div[@id='village_map']/img");

                for (int i = 0; i < node.Count; i++)
                {
                    if (node[i].Attributes["href"].Value.Split('=').Length == 2)
                    {
                        int woodNeed = 0;
                        int clayNeed = 0;
                        int ironNeed = 0;
                        int grainNeed = 0;
                        byte upgrading = 0;
                        int level = 0;
                        int id = 0;
                        int gid = 0;
                        id = Convert.ToInt32(node[i].Attributes["href"].Value.Split('=')[1]);
                        string[] stringSeparators = new string[] { "&lt;", "&gt;" };
                        string[] parsing = node[i].Attributes["alt"].Value.Split(stringSeparators, StringSplitOptions.None);
                        if (parsing.Length > 1)
                        {
                            level = Convert.ToInt32(parsing[2].Split(' ')[1]);
                            try
                            {
                                woodNeed = Convert.ToInt32(parsing[12]);
                                clayNeed = Convert.ToInt32(parsing[18]);
                                ironNeed = Convert.ToInt32(parsing[24]);
                                grainNeed = Convert.ToInt32(parsing[30]);
                                upgrading = 0;
                            }
                            catch
                            {
                                try
                                {
                                    woodNeed = Convert.ToInt32(parsing[18]);
                                    clayNeed = Convert.ToInt32(parsing[24]);
                                    ironNeed = Convert.ToInt32(parsing[30]);
                                    grainNeed = Convert.ToInt32(parsing[36]);
                                    upgrading = 1;
                                    village.setHasBuildingUpgrade(true);
                                }
                                catch
                                {
                                    try
                                    {
                                        woodNeed = Convert.ToInt32(parsing[24]);
                                        clayNeed = Convert.ToInt32(parsing[30]);
                                        ironNeed = Convert.ToInt32(parsing[36]);
                                        grainNeed = Convert.ToInt32(parsing[42]);
                                        upgrading = 2;
                                        village.setHasBuildingUpgrade(true);
                                    }
                                    catch
                                    {
                                        woodNeed = 0;
                                        clayNeed = 0;
                                        ironNeed = 0;
                                        grainNeed = 0;
                                        upgrading = 0;
                                    }
                                }                               
                            }
                            try
                            {
                                gid = Convert.ToInt32(gidNode[i].Attributes["class"].Value.Split(' ')[1].Remove(0, 1));
                            }
                            catch
                            {
                                try
                                {
                                    gid = Convert.ToInt32(gidNode[i].Attributes["class"].Value.Split(' ')[1].Remove(0, 1).Remove(2, 3));
                                }
                                catch
                                {
                                    string parse = gidNode[i].Attributes["class"].Value.Split(' ')[1].Remove(0, 1);
                                    gid = Convert.ToInt32(parse.Remove(parse.Length - 1, 1));
                                }                              
                            }

                            Buildings building = new Buildings(id, gid);
                            building.setLevel(level);
                            building.setWoodNeed(woodNeed);
                            building.setClayNeed(clayNeed);
                            building.setIronNeed(ironNeed);
                            building.setGrainNeed(grainNeed);
                            building.setUpgrading(upgrading);

                            village.updateBuildings(building, id - 1);
                        }
                        else
                        {
                            Buildings building = new Buildings(id, 0);
                            village.updateBuildings(building, id - 1);
                        }
                    }
                }
                buildException = "buildPass";
            }
            catch (Exception)
            {
                buildException = "buildConnection";
            }
        }

        public string upgradingDecode(HtmlDocument doc, Village village)
        {
            string upgrade1 = "";
            string upgrade2 = "";
            string toReturn = "";
            int upgradingCount = 0;
            var node = doc.DocumentNode.SelectSingleNode("//div[@class='boxes buildingList']");
            if (node != null)
            {
                HtmlNode ulNode = node.ChildNodes.FindFirst("ul");

                HtmlNode[] productionLiNodes = ulNode.Descendants("li").ToArray();

                for (int i = 0; i < productionLiNodes.Length; i++)
                {
                    upgradingCount++;
                    HtmlNode[] productionDivNodes = productionLiNodes[i].Descendants("div").ToArray();
                    string[] parsing1 = productionDivNodes[1].InnerText.Split(' ');
                    if (parsing1[parsing1.Length - 1].TrimStart().TrimEnd().Equals("am") || parsing1[parsing1.Length - 1].TrimStart().TrimEnd().Equals("pm"))
                    {
                        if (i == 0)
                        {
                            upgrade1 = parsing1[0].TrimStart().TrimEnd() + "," + parsing1[parsing1.Length - 2] + " " + parsing1[parsing1.Length - 1];
                        }
                        if (i == 1)
                        {
                            upgrade2 = parsing1[0].TrimStart().TrimEnd() + "," + parsing1[parsing1.Length - 2] + " " + parsing1[parsing1.Length - 1];
                        }
                    }
                    else
                    {
                        if (i == 0)
                        {
                            upgrade1 = parsing1[0].TrimStart().TrimEnd() + "," + parsing1[parsing1.Length - 1];
                        }
                        if (i == 1)
                        {
                            upgrade2 = parsing1[0].TrimStart().TrimEnd() + "," + parsing1[parsing1.Length - 1];
                        }
                    }  
                }                              
            }
            var durationNode = doc.DocumentNode.SelectNodes("//script[@type='text/javascript']");
            foreach (HtmlNode singleNode in durationNode)
            {
                if (singleNode.InnerText.Contains("bld"))    
                {
                    string[] split = singleNode.InnerText.Split(',');
                    if (upgradingCount == 1)
                    {
                        upgrade1 += "," + split[2].Split('"')[3];
                    }
                    else if (upgradingCount == 2)
                    {
                        upgrade1 += "," + split[2].Split('"')[3];
                        upgrade2 += "," + split[5].Split('"')[3];
                    }
                }
            }
            village.setUpgradingCount(upgradingCount);
            toReturn = upgrade1 + "*" + upgrade2;
            return toReturn;
        }
    }
}
