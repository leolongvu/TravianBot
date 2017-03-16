using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravianBot.Library
{
    class Resources
    {
        private int wood;
        private int clay;
        private int iron;
        private int grain;

        private int woodProduction;
        private int clayProduction;
        private int ironProduction;
        private int grainProduction;

        private DateTime lastUpdateTime;
        private int baseWood;
        private int baseClay;
        private int baseIron;
        private int baseGrain;

        private int warehouse;
        private int granary;
        private int freeCrop;

        public void setWood(int wood)
        {
            this.wood = wood;
        }
        public int getWood()
        {
            return wood;
        }
        public void setClay(int clay)
        {
            this.clay = clay;
        }
        public int getClay()
        {
            return clay;
        }
        public void setIron(int iron)
        {
            this.iron = iron;
        }
        public int getIron()
        {
            return iron;
        }
        public void setGrain(int grain)
        {
            this.grain = grain;
        }
        public int getGrain()
        {
            return grain;
        }
        public void setWoodProduction(int woodPro)
        {
            this.woodProduction = woodPro;
        }
        public int getWoodPro()
        {
            return woodProduction;
        }
        public void setClayProduction(int clayPro)
        {
            this.clayProduction = clayPro;
        }
        public int getClayPro()
        {
            return clayProduction;
        }
        public void setIronProduction(int ironPro)
        {
            this.ironProduction = ironPro;
        }
        public int getIronPro()
        {
            return ironProduction;
        }
        public void setGrainProduction(int grainPro)
        {
            this.grainProduction = grainPro;
        }
        public int getGrainPro()
        {
            return grainProduction;
        }
        public void setBaseWood(int baseWood)
        {
            this.baseWood = baseWood;
        }
        public int getBaseWood()
        {
            return baseWood;
        }
        public void setBaseClay(int baseClay)
        {
            this.baseClay = baseClay;
        }
        public int getBaseClay()
        {
            return baseClay;
        }
        public void setBaseIron(int baseIron)
        {
            this.baseIron = baseIron;
        }
        public int getBaseIron()
        {
            return baseIron;
        }
        public void setBaseGrain(int baseGrain)
        {
            this.baseGrain = baseGrain;
        }
        public int getBaseGrain()
        {
            return baseGrain;
        }
        public void setWarehouse(int warehouse)
        {
            this.warehouse = warehouse;
        }
        public int getWarehouse()
        {
            return warehouse;
        }
        public void setGranary(int granary)
        {
            this.granary = granary;
        }
        public int getGranary()
        {
            return granary;
        }
        public void setFreecrop(int freeCrop)
        {
            this.freeCrop = freeCrop;
        }
        public int getFreecrop()
        {
            return freeCrop;
        }
        public void setUpdateTime(DateTime newTime)
        {
            this.lastUpdateTime = newTime;
        }
        public DateTime getTime()
        {
            return this.lastUpdateTime;
        }
    }
}
