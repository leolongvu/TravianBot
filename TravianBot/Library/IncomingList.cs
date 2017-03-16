using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravianBot.Library
{
    class IncomingList
    {
        string from;
        string resources;
        string troops;
        int wood;
        int clay;
        int iron;
        int grain;
        string percent;

        int troop1;
        int troop2;
        int troop3;
        int troop4;
        int troop5;
        int troop6;
        int troop7;
        int troop8;
        int troop9;
        int troop10;
        int hero;

        public static List<IncomingList> incoming = new List<IncomingList>();

        public IncomingList(string from)
        {
            this.from = from;
        }
        public string From
        {
            get { return from; }
            set { from = value; }
        }
        public int Wood
        {
            get { return wood; }
            set { wood = value; }
        }
        public int Clay
        {
            get { return clay; }
            set { clay = value; }
        }
        public int Iron
        {
            get { return iron; }
            set { iron = value; }
        }
        public int Grain
        {
            get { return grain; }
            set { grain = value; }
        }
        public string Resources
        {
            get { return resources; }
            set { resources = value; }
        }
        public string Percent
        {
            get { return percent; }
            set { percent = value; }
        }
        public int Troop1
        {
            get { return troop1; }
            set { troop1 = value; }
        }
        public int Troop2
        {
            get { return troop2; }
            set { troop2 = value; }
        }
        public int Troop3
        {
            get { return troop3; }
            set { troop3 = value; }
        }
        public int Troop4
        {
            get { return troop4; }
            set { troop4 = value; }
        }
        public int Troop5
        {
            get { return troop5; }
            set { troop5 = value; }
        }
        public int Troop6
        {
            get { return troop6; }
            set { troop6 = value; }
        }
        public int Troop7
        {
            get { return troop7; }
            set { troop7 = value; }
        }
        public int Troop8
        {
            get { return troop8; }
            set { troop8 = value; }
        }
        public int Troop9
        {
            get { return troop9; }
            set { troop9 = value; }
        }
        public int Troop10
        {
            get { return troop10; }
            set { troop10 = value; }
        }
        public int Hero
        {
            get { return hero; }
            set { hero = value; }
        }
    }
}
