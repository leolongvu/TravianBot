using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravianBot.Library
{
    class Troops
    {
        private string troopsID;
        private int numberOfTroops;

        public Troops(string troopsID)
        {
            this.troopsID = troopsID;
        }

        public void setTroopsNo(int troopsNo)
        {
            numberOfTroops = troopsNo;
        }
        public string getTroopsID()
        {
            return troopsID;
        }
        public int getTroopsNo()
        {
            return numberOfTroops;
        }
    }

    class TroopsWave
    {
        private Troops[] troopsWave;

        private int type;
        private int desID;
        private int ownID;
        private string desName;

        private int woodCarry;
        private int clayCarry;
        private int ironCarry;
        private int grainCarry;
        private int total;
        private int capacity;
        private Coordinates coordinates;

        public TroopsWave(int type, int desRef, Troops[] troop)
        {
            this.type = type;
            this.desID = desRef;
            this.troopsWave = troop;
        }

        public void setCoordinates(Coordinates coordinates)
        {
            this.coordinates = coordinates;
        }
        public Coordinates getCoordinates()
        {
            return coordinates;
        }
        public void setType(int type)
        {
            this.type = type;
        }
        public void setDesName(string name)
        {
            this.desName = name;
        }
        public string getDesName()
        {
            return desName;
        }
        public void setWoodCarry(int woodCarry)
        {
            this.woodCarry = woodCarry;
        }
        public void setClayCarry(int clayCarry)
        {
            this.clayCarry = clayCarry;
        }
        public void setIronCarry(int ironCarry)
        {
            this.ironCarry = ironCarry;
        }
        public void setGrainCarry(int grainCarry)
        {
            this.grainCarry = grainCarry;
        }
        public int getType()
        {
            return type;
        }
        public void setOwnID(int ID)
        {
            this.ownID = ID;
        }
        public int getOwnID()
        {
            return ownID;
        }
        public int getWoodCarry()
        {
            return woodCarry;
        }
        public int getClayCarry()
        {
            return clayCarry;
        }
        public int getIronCarry()
        {
            return ironCarry;
        }
        public int getGrainCarry()
        {
            return grainCarry;
        }
        public void setDesID(int ID)
        {
            this.desID = ID;
        }
        public int getDesID()
        {
            return desID;
        }
        public void setTotal(int total)
        {
            this.total = total;
        }
        public void setCapacity(int capacity)
        {
            this.capacity = capacity;
        }
        public int getTotal()
        {
            return total;
        }
        public int getCapacity()
        {
            return capacity;
        }
        public Troops[] getWave()
        {
            return troopsWave;
        }
    }
}
