using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravianBot.Functions.Decoders;

namespace TravianBot.Library
{
    class Village
    {
        private string name;
        private int ID;

        private List<Buildings> buildings;
        private Resources resources;
        private List<TroopsWave> incomingTroops;
        private List<TroopsWave> outgoingTroops;
        private List<TroopsWave> inVillageTroops;

        private bool isActive;
        private bool hasBuildingUpgrade;
        private int upgradingCount;
        private string timer;
        private string reference;
        private int tileID;
        private Coordinates coordinates;

        public Village(string name, int id, Coordinates coordinates)
        {
            this.name = name;
            this.ID = id;
            buildings = new List<Buildings>();
            resources = new Resources();
            incomingTroops = new List<TroopsWave>();
            outgoingTroops = new List<TroopsWave>();
            inVillageTroops = new List<TroopsWave>();
            this.coordinates = coordinates;
        }      

        public string getName()
        {
            return name;
        }
        public string getReference()
        {
            return reference;
        }      
        public int getID()
        {
            return ID;
        }
        public Coordinates getCoordinates()
        {
            return coordinates;
        }
        public void setActive(bool active)
        {
            isActive = active;
        }
        public bool getActive()
        {
            return isActive;
        }
        public void updateBuildings(Buildings building, int index) 
        {
            if (index <= buildings.Count - 1)
            {
                buildings[index] = building;
            }
            else
            {
                buildings.Add(building);
            }
        }
        public Buildings getBuilding(int index)
        {
            if (buildings[index] != null)
            {
                return buildings[index];
            }
            return null;
        }
        public int getBuildingCount()
        {
            return buildings.Count;
        }
        public void updateResources(Resources resources)
        {
            this.resources = resources;
        }
        public Resources getResources()
        {
            return resources;
        }
        public void setReference(string reference)
        {
            this.reference = reference;
        }
        public int getTileID()
        {
            return tileID;
        }
        public void setTileID(int ID)
        {
            this.tileID = ID;
        }
        public void setHasBuildingUpgrade(bool hasBuildingUpgrade)
        {
            this.hasBuildingUpgrade = hasBuildingUpgrade;
        }
        public bool getHasBuildingUpgrade()
        {
            return hasBuildingUpgrade;
        }
        public void setUpgradingCount(int upgradingCount)
        {
            this.upgradingCount = upgradingCount;
        }
        public int getUpgradingCount()
        {
            return upgradingCount;
        }
        public void setTimer(string timer)
        {
            this.timer = timer;
        }
        public string getTimer()
        {
            return timer;
        }
        public void addTroopWave(TroopsWave troop)
        {
            switch (troop.getType())
            {
                case 0:
                    inVillageTroops.Add(troop);
                    break;
                case 1:
                case 2:
                case 3:
                    incomingTroops.Add(troop);
                    break;
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    outgoingTroops.Add(troop);
                    break;
            }
        }
        public void refreshIncoming()
        {
            incomingTroops.Clear();
        }
        public void refreshOutgoing()
        {
            outgoingTroops.Clear();
        }
        public void refreshInVillage()
        {
            inVillageTroops.Clear();
        }
        public List<TroopsWave> getIncoming()
        {
            return incomingTroops;
        }
        public List<TroopsWave> getOutgoing()
        {
            return outgoingTroops;
        }
        public List<TroopsWave> getInVillage()
        {
            return inVillageTroops;
        }
    }
}
