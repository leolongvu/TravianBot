using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravianBot.Library
{
    class Buildings
    {
        private int slotID;
        private int gID;

        private int level;

        private int woodNeed;
        private int clayNeed;
        private int ironNeed;
        private int grainNeed;

        private byte isUpgrading;

        public Buildings(int slotID, int gID)
        {
            this.slotID = slotID;
            this.gID = gID;
        }

        public int getID()
        {
            return slotID;
        }
        public int getgID()
        {
            return gID;
        }
        public void setLevel(int level)
        {
            this.level = level;
        }
        public void setWoodNeed(int woodNeed)
        {
            this.woodNeed = woodNeed;
        }
        public void setClayNeed(int clayNeed)
        {
            this.clayNeed = clayNeed;
        }
        public void setIronNeed(int ironNeed)
        {
            this.ironNeed = ironNeed;
        }
        public void setGrainNeed(int grainNeed)
        {
            this.grainNeed = grainNeed;
        }
        public int getLevel()
        {
            return level;
        }
        public int getWoodNeed()
        {
            return woodNeed;
        }
        public int getClayNeed()
        {
            return clayNeed;
        }
        public int getIronNeed()
        {
            return ironNeed;
        }
        public int getGrainNeed()
        {
            return grainNeed;
        }
        public void setUpgrading(byte upgrade)
        {
            isUpgrading = upgrade;
        }
        public byte getUpgrading()
        {
            return isUpgrading;
        }

        public static string titleFromGID(int gID)
        {
            string name = "";
            switch (gID)
            {
                case 0:
                    name = "Building site";
                    break;
                case 1:
                    name = "Woodcutter";
                    break;
                case 2:
                    name = "Clay pit";
                    break;
                case 3:
                    name = "Iron mine";
                    break;
                case 4:
                    name = "Cropland";
                    break;
                case 5:
                    name = "Sawmill";
                    break;
                case 6:
                    name = "Brickyard";
                    break;
                case 7:
                    name = "Iron foundary";
                    break;
                case 8:
                    name = "Grain mill";
                    break;
                case 9:
                    name = "Bakery";
                    break;
                case 10:
                    name = "Warehouse";
                    break;
                case 11:
                    name = "Granary";
                    break;
                case 12:
                    name = "Blacksmith";
                    break;
                case 13:
                    name = "Armoury";
                    break;
                case 14:
                    name = "Tournament square";
                    break;
                case 15:
                    name = "Main building";
                    break;
                case 16:
                    name = "Rally point";
                    break;
                case 17:
                    name = "Marketplace";
                    break;
                case 18:
                    name = "Embassy";
                    break;
                case 19:
                    name = "Barracks";
                    break;
                case 20:
                    name = "Stable";
                    break;
                case 21:
                    name = "Workshop";
                    break;
                case 22:
                    name = "Academy";
                    break;
                case 23:
                    name = "Cranny";
                    break;
                case 24:
                    name = "Town hall";
                    break;
                case 25:
                    name = "Residence";
                    break;
                case 26:
                    name = "Palace";
                    break;
                case 27:
                    name = "Treasury";
                    break;
                case 28:
                    name = "Trade office";
                    break;
                case 29:
                    name = "Great barrack";
                    break;
                case 30:
                    name = "Great stable";
                    break;
                case 31:
                    name = "City wall";
                    break;
                case 32:
                    name = "Earth wall";
                    break;
                case 33:
                    name = "Palisade";
                    break;
                case 34:
                    name = "Stonemanson";
                    break;
                case 35:
                    name = "Brewery";
                    break;
                case 36:
                    name = "Trapper";
                    break;
                case 37:
                    name = "Hero's mansion";
                    break;
                case 40:
                    name = "Wonder of the world";
                    break;
                case 41:
                    name = "Horse's drinking Trough";
                    break;
            }
            return name;
        }
    }
}
