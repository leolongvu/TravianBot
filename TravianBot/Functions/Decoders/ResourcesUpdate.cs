using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using TravianBot.Connnections;
using TravianBot.Library;

namespace TravianBot.Functions.Decoders
{
    class ResourcesUpdate
    {        
        private string exception;

        public string getException()
        {
            return this.exception;
        }

        public void updateResources(HtmlDocument doc, Village village)
        {
            try
            {
                Resources temRes = new Resources();

                temRes.setUpdateTime(DateTime.Now);                
                temRes.setWarehouse(Convert.ToInt32(doc.DocumentNode.SelectSingleNode("//li[@id='stockBarWarehouseWrapper']/span").InnerText));
                temRes.setGranary(Convert.ToInt32(doc.DocumentNode.SelectSingleNode("//li[@id='stockBarGranaryWrapper']/span").InnerText));
                var resources = doc.DocumentNode.SelectNodes("//div[@class='middle']/span");
                foreach (HtmlNode node in resources)
                {
                    if (node.Attributes["id"] != null)
                    {
                        int amount = 0;
                        try
                        {
                            string[] amountStrings = node.InnerText.Split('/');
                            amount = Convert.ToInt32(amountStrings[0]);
                        }
                        catch { }
                        switch (node.Attributes["id"].Value)
                        {
                            case "l1":
                                temRes.setWood(amount);
                                temRes.setBaseWood(amount);
                                break;
                            case "l2":
                                temRes.setClay(amount);
                                temRes.setBaseClay(amount);
                                break;
                            case "l3":
                                temRes.setIron(amount);
                                temRes.setBaseIron(amount);
                                break;
                            case "l4":
                                temRes.setGrain(amount);
                                temRes.setBaseGrain(amount);
                                break;
                            case "stockBarFreeCrop":
                                temRes.setFreecrop(amount);
                                break;
                        }
                    }
                }
                var proNode = doc.DocumentNode.SelectSingleNode("//div[@class='boxes villageList production']");
                if (proNode != null)
                {
                    HtmlNode tBodyNode = proNode.ChildNodes.FindFirst("tbody");

                    HtmlNode[] productionTrNodes = tBodyNode.Descendants("tr").ToArray();

                    HtmlNode woodNode = productionTrNodes[0].Descendants("td").ToArray()[2];
                    HtmlNode clayNode = productionTrNodes[1].Descendants("td").ToArray()[2];
                    HtmlNode ironNode = productionTrNodes[2].Descendants("td").ToArray()[2];
                    HtmlNode grainNode = productionTrNodes[3].Descendants("td").ToArray()[2];
                 
                    temRes.setWoodProduction(Convert.ToInt32(woodNode.InnerText.Split(';')[2].Split('&')[0]));
                    temRes.setClayProduction(Convert.ToInt32(clayNode.InnerText.Split(';')[2].Split('&')[0]));
                    temRes.setIronProduction(Convert.ToInt32(ironNode.InnerText.Split(';')[2].Split('&')[0]));
                    try
                    {
                        temRes.setGrainProduction(Convert.ToInt32(grainNode.InnerText.Split(';')[2].Split('&')[0]));
                    }
                    catch
                    {
                        temRes.setGrainProduction(0 - Convert.ToInt32(grainNode.InnerText.Split(';')[3].Split('&')[0]));
                    }
                }
                village.updateResources(temRes);
                exception = "resPass";
            }
            catch (Exception)
            {
                exception = "resConnection";
            }
        }

        public void refreshResources(Village village)
        {
            DateTime currentTime = DateTime.Now;
            TimeSpan timeDifference = currentTime.Subtract(village.getResources().getTime());

            double tempProduction = village.getResources().getWoodPro();
            double addition = tempProduction * timeDifference.TotalHours;
            village.getResources().setWood(village.getResources().getBaseWood() + Convert.ToInt32(addition));

            tempProduction = village.getResources().getClayPro();
            addition = tempProduction * timeDifference.TotalHours;
            village.getResources().setClay(village.getResources().getBaseClay() + Convert.ToInt32(addition));

            tempProduction = village.getResources().getIronPro();
            addition = tempProduction * timeDifference.TotalHours;
            village.getResources().setIron(village.getResources().getBaseIron() + Convert.ToInt32(addition));

            tempProduction = village.getResources().getGrainPro();
            addition = tempProduction * timeDifference.TotalHours;
            village.getResources().setGrain(village.getResources().getBaseGrain() + Convert.ToInt32(addition));
        }
    }
}
