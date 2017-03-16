using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using TravianBot.Connnections;
using TravianBot.Functions.Decoders;
using TravianBot.Library;
using TravianBot.Functions;

namespace TravianBot
{
    public partial class Main : Form
    {
        private Global global;
        private LoginForm loginForm;
        private Village currentVillage;
        private ImageList imageList;

        private string serverUrl;
        private string username;
        private string password;
        private bool isChecked;

        private string tribeCode;

        public Main()
        {
            InitializeComponent();
            formLoad();
        }

        private void formLoad()
        {
            global = new Global();

            global.delegMessage += new Global.eventDelegate(eventMessage);

            username = "";
            password = "";
            serverUrl = "";

            loadConfigs();

            loadImages();
        }

        public void loadConfigs()
        {
            if (File.Exists(Application.StartupPath + @"\Configs\" + "login" + ".ini"))
            {
                TextReader config_reader = new StreamReader(Application.StartupPath + @"\Configs\" + "login" + ".ini");
                string input;
                while ((input = config_reader.ReadLine()) != "//")
                {
                    isChecked = Convert.ToBoolean(input);
                    if (isChecked)
                    {
                        username = config_reader.ReadLine();
                        password = Security.Decrypt(config_reader.ReadLine());
                        serverUrl = config_reader.ReadLine();
                    }
                }
                config_reader.Close();
            }
        }


        private void loadImages()
        {
            imageList = new ImageList();
            string[] filePaths = Directory.GetFiles(Application.StartupPath + "\\Icons", "*.bmp", SearchOption.TopDirectoryOnly);            
            int i = 0;
            foreach (var file in filePaths)
            {
                //Add images to Imagelist
                imageList.Images.Add(i.ToString(), Image.FromFile(file));
                i++;
            }
            objectListView1.SmallImageList = imageList;
            objectListView2.SmallImageList = imageList;
        }

        private void loadListHeader(string tribe)
        {
            objectListView1.GetColumn(2).HeaderImageKey = "38";
            objectListView1.GetColumn(3).HeaderImageKey = "33";
            objectListView1.GetColumn(4).HeaderImageKey = "35";
            objectListView1.GetColumn(5).HeaderImageKey = "34";
            objectListView1.GetColumn(18).HeaderImageKey = "0";
            objectListView2.GetColumn(13).HeaderImageKey = "0";
            switch (tribe)
            {
                case "nation1": //Romans
                    objectListView1.GetColumn(8).HeaderImageKey = "1";
                    objectListView1.GetColumn(9).HeaderImageKey = "2";
                    objectListView1.GetColumn(10).HeaderImageKey = "3";
                    objectListView1.GetColumn(11).HeaderImageKey = "4";
                    objectListView1.GetColumn(12).HeaderImageKey = "5";
                    objectListView1.GetColumn(13).HeaderImageKey = "6";
                    objectListView1.GetColumn(14).HeaderImageKey = "7";
                    objectListView1.GetColumn(15).HeaderImageKey = "8";
                    objectListView1.GetColumn(16).HeaderImageKey = "9";
                    objectListView1.GetColumn(17).HeaderImageKey = "10";

                    objectListView2.GetColumn(3).HeaderImageKey = "1";
                    objectListView2.GetColumn(4).HeaderImageKey = "2";
                    objectListView2.GetColumn(5).HeaderImageKey = "3";
                    objectListView2.GetColumn(6).HeaderImageKey = "4";
                    objectListView2.GetColumn(7).HeaderImageKey = "5";
                    objectListView2.GetColumn(8).HeaderImageKey = "6";
                    objectListView2.GetColumn(9).HeaderImageKey = "7";
                    objectListView2.GetColumn(10).HeaderImageKey = "8";
                    objectListView2.GetColumn(11).HeaderImageKey = "9";
                    objectListView2.GetColumn(12).HeaderImageKey = "10";
                    break;
                case "nation2": //Teutons
                    objectListView1.GetColumn(8).HeaderImageKey = "11";
                    objectListView1.GetColumn(9).HeaderImageKey = "12";
                    objectListView1.GetColumn(10).HeaderImageKey = "13";
                    objectListView1.GetColumn(11).HeaderImageKey = "14";
                    objectListView1.GetColumn(12).HeaderImageKey = "15";
                    objectListView1.GetColumn(13).HeaderImageKey = "16";
                    objectListView1.GetColumn(14).HeaderImageKey = "17";
                    objectListView1.GetColumn(15).HeaderImageKey = "18";
                    objectListView1.GetColumn(16).HeaderImageKey = "19";
                    objectListView1.GetColumn(17).HeaderImageKey = "20";

                    objectListView2.GetColumn(3).HeaderImageKey = "11";
                    objectListView2.GetColumn(4).HeaderImageKey = "12";
                    objectListView2.GetColumn(5).HeaderImageKey = "13";
                    objectListView2.GetColumn(6).HeaderImageKey = "14";
                    objectListView2.GetColumn(7).HeaderImageKey = "15";
                    objectListView2.GetColumn(8).HeaderImageKey = "16";
                    objectListView2.GetColumn(9).HeaderImageKey = "17";
                    objectListView2.GetColumn(10).HeaderImageKey = "18";
                    objectListView2.GetColumn(11).HeaderImageKey = "19";
                    objectListView2.GetColumn(12).HeaderImageKey = "20";
                    break;
                case "nation3": //Gauls
                    objectListView1.GetColumn(8).HeaderImageKey = "21";
                    objectListView1.GetColumn(9).HeaderImageKey = "22";
                    objectListView1.GetColumn(10).HeaderImageKey = "23";
                    objectListView1.GetColumn(11).HeaderImageKey = "24";
                    objectListView1.GetColumn(12).HeaderImageKey = "25";
                    objectListView1.GetColumn(13).HeaderImageKey = "26";
                    objectListView1.GetColumn(14).HeaderImageKey = "27";
                    objectListView1.GetColumn(15).HeaderImageKey = "28";
                    objectListView1.GetColumn(16).HeaderImageKey = "29";
                    objectListView1.GetColumn(17).HeaderImageKey = "30";

                    objectListView2.GetColumn(3).HeaderImageKey = "21";
                    objectListView2.GetColumn(4).HeaderImageKey = "22";
                    objectListView2.GetColumn(5).HeaderImageKey = "23";
                    objectListView2.GetColumn(6).HeaderImageKey = "24";
                    objectListView2.GetColumn(7).HeaderImageKey = "25";
                    objectListView2.GetColumn(8).HeaderImageKey = "26";
                    objectListView2.GetColumn(9).HeaderImageKey = "27";
                    objectListView2.GetColumn(10).HeaderImageKey = "28";
                    objectListView2.GetColumn(11).HeaderImageKey = "29";
                    objectListView2.GetColumn(12).HeaderImageKey = "30";
                    break;
            }
        }

        private int scoutImage()
        {
            switch (tribeCode)
            {
                case "nation1":
                    return 4;
                case "nation2":
                    return 14;
                case "nation3":
                    return 23;
                default:
                    return 23;
            }
        }

        private void debug(string text)
        {
            if (!text.Equals(""))
            {
                log.AppendText(text + Environment.NewLine);
            }
        }

        private void eventMessage(object sender, object data, Global.flag myflag, string success)
        {
            //try
            {
                if (data == null)
                    return;

                string message = data.ToString();

                switch (myflag)
                {
                    #region Login
                    case Global.flag.login_status:
                        switch (message)
                        {
                            case "connection":
                                status.Text = "Connection Error";
                                break;
                            case "username":
                                status.Text = "Wrong Username Entered";
                                break;
                            case "password":
                                status.Text = "Incorrect Password";
                                break;
                            default:
                                status.Text = "Successfully Login";
                                server.Text = "Server: " + serverUrl;
                                account.Text = "Account: " + username;
                                tribe.Text = "Tribe: " + message.Split(';')[0];
                                tribeCode = message.Split(';')[1];
                                loadListHeader(tribeCode);
                                break;
                        }
                        if (success.Equals("villageError"))
                        {
                            debug("There is a problem with the connection while parsing villages! Please try to sign in again.");
                            status.Text = "Connection Error";
                        }
                        else
                        {
                            for (int i = 0; i < global.villageList.villageCount(); i++)
                            {
                                villageList.Items.Add(String.Format(global.villageList.getVillage(i).getName() + " (" +
                                    (global.villageList.getVillage(i).getCoordinates().getX() + ";" +
                                    global.villageList.getVillage(i).getCoordinates().getY() + ")")));
                                if (global.villageList.getVillage(i).getActive() == true)
                                {
                                    villageList.SelectedIndex = i;
                                    currentVillage = global.villageList.getVillage(i);
                                    global.dorfExcute(serverUrl, currentVillage);
                                }
                            }
                        }
                        break;
                    #endregion

                    #region Dorf
                    case Global.flag.dorf_handling:
                        string[] error = success.Split(';');
                        if (error[0].Equals("resConnection"))
                        {
                            debug("There is a problem with the connection while parsing resources! Please try to sign in again.");
                            status.Text = "Connection Error";
                        }
                        else
                        {
                            wood.Text = "Wood: " + currentVillage.getResources().getWood() + "/" +
                                currentVillage.getResources().getWarehouse() + " + " + currentVillage.getResources().getWoodPro() + " h. " +
                                (int)(currentVillage.getResources().getWood() * 100 / currentVillage.getResources().getWarehouse()) + "%";
                            clay.Text = "Clay: " + currentVillage.getResources().getClay() + "/" +
                                currentVillage.getResources().getWarehouse() + " + " + currentVillage.getResources().getClayPro() + " h. " +
                                (int)(currentVillage.getResources().getClay() * 100 / currentVillage.getResources().getWarehouse()) + "%";
                            iron.Text = "Iron: " + currentVillage.getResources().getIron() + "/" +
                                currentVillage.getResources().getWarehouse() + " + " + currentVillage.getResources().getIronPro() + " h. "
                                + (int)(currentVillage.getResources().getIron() * 100 / currentVillage.getResources().getWarehouse()) + "%";
                            grain.Text = "Grain: " + currentVillage.getResources().getGrain() + "/" +
                                currentVillage.getResources().getGranary() + " + " + currentVillage.getResources().getGrainPro() + " h. "
                                + (int)(currentVillage.getResources().getGrain() * 100 / currentVillage.getResources().getWarehouse()) + "%";
                            freecrop.Text = "Free Wheat: " + currentVillage.getResources().getFreecrop();
                        }
                        if (error[1].Equals("buildConnection") || error[2].Equals("buildConnection"))
                        {
                            debug("There is a problem with the connection while parsing buildings! Please try to sign in again.");
                            status.Text = "Connection Error";
                        }
                        else
                        {
                            for (int i = 0; i < currentVillage.getBuildingCount(); i++)
                            {
                                switch (currentVillage.getBuilding(i).getID())
                                {
                                    case 1:
                                        updateCell(cell1, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 2:
                                        updateCell(cell2, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 3:
                                        updateCell(cell3, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 4:
                                        updateCell(cell4, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 5:
                                        updateCell(cell5, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 6:
                                        updateCell(cell6, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 7:
                                        updateCell(cell7, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 8:
                                        updateCell(cell8, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 9:
                                        updateCell(cell9, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 10:
                                        updateCell(cell10, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 11:
                                        updateCell(cell11, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 12:
                                        updateCell(cell12, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 13:
                                        updateCell(cell13, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 14:
                                        updateCell(cell14, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 15:
                                        updateCell(cell15, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 16:
                                        updateCell(cell16, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 17:
                                        updateCell(cell17, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 18:
                                        updateCell(cell18, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 19:
                                        updateCell(cell19, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 20:
                                        updateCell(cell20, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 21:
                                        updateCell(cell21, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 22:
                                        updateCell(cell22, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 23:
                                        updateCell(cell23, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 24:
                                        updateCell(cell24, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 25:
                                        updateCell(cell25, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 26:
                                        updateCell(cell26, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 27:
                                        updateCell(cell27, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 28:
                                        updateCell(cell28, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 29:
                                        updateCell(cell29, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 30:
                                        updateCell(cell30, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 31:
                                        updateCell(cell31, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 32:
                                        updateCell(cell32, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 33:
                                        updateCell(cell33, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 34:
                                        updateCell(cell34, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 35:
                                        updateCell(cell35, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 36:
                                        updateCell(cell36, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 37:
                                        updateCell(cell37, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 38:
                                        updateCell(cell38, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 39:
                                        updateCell(cell39, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                    case 40:
                                        updateCell(cell40, currentVillage.getBuilding(i).getgID(), currentVillage.getBuilding(i).getLevel(), currentVillage.getBuilding(i).getUpgrading());
                                        break;
                                }
                            }
                            if (currentVillage.getHasBuildingUpgrade() == true)
                            {
                                string[] upgrade = message.Split('*');
                                if (currentVillage.getUpgradingCount() == 1)
                                {
                                    string[] upgrade1 = upgrade[0].Split(',');
                                    upgradestatus1.Text = "Slot " + upgrade1[2] + "  " + upgrade1[0] + "   " + upgrade1[1];
                                    upgradestatus2.Text = "";
                                }
                                else if (currentVillage.getUpgradingCount() == 2)
                                {
                                    string[] upgrade1 = upgrade[0].Split(',');
                                    string[] upgrade2 = upgrade[1].Split(',');
                                    upgradestatus1.Text = "Slot " + upgrade1[2] + "  " + upgrade1[0] + "   " + upgrade1[1];
                                    upgradestatus2.Text = "Slot " + upgrade2[2] + "  " + upgrade2[0] + "   " + upgrade2[1];
                                }
                            }
                            else
                            {
                                upgradestatus1.Text = "";
                                upgradestatus2.Text = "";
                            }
                        }
                        break;
                    #endregion

                    #region Troop

                    case Global.flag.incoming_troop_handling:
                        debug(TroopsMove.test);
                        if (success.Equals("troopError"))
                        {
                            debug("There is a problem with the connection while parsing troop movements!");
                        }
                        else if (success.Equals("villageError"))
                        {
                            debug("Wrong current village! " + currentVillage.getName());
                        }
                        else if (success.Equals("noRally"))
                        {
                            debug("No Rallypoint founded!");
                        }
                        else
                        {
                            IncomingList.incoming.Clear();
                            for (int i = 0; i < currentVillage.getIncoming().Count; i++)
                            {
                                IncomingList incomeTroops = new IncomingList(currentVillage.getIncoming()[i].getDesName() + " (" +
                                                currentVillage.getIncoming()[i].getCoordinates().getX()
                                            + "," + currentVillage.getIncoming()[i].getCoordinates().getY() + ")");
                                incomeTroops.Resources = currentVillage.getIncoming()[i].getTotal().ToString() + "/" +
                                            currentVillage.getIncoming()[i].getCapacity().ToString();
                                try
                                {
                                    incomeTroops.Percent = Convert.ToString((currentVillage.getIncoming()[i].getTotal() * 100) /
                                        currentVillage.getIncoming()[i].getCapacity()) + "%";
                                }
                                catch
                                {
                                    incomeTroops.Percent = "100%";
                                }
                                incomeTroops.Wood = currentVillage.getIncoming()[i].getWoodCarry();
                                incomeTroops.Clay = currentVillage.getIncoming()[i].getClayCarry();
                                incomeTroops.Iron = currentVillage.getIncoming()[i].getIronCarry();
                                incomeTroops.Grain = currentVillage.getIncoming()[i].getGrainCarry();
                                incomeTroops.Troop1 = currentVillage.getIncoming()[i].getWave()[0].getTroopsNo();
                                incomeTroops.Troop2 = currentVillage.getIncoming()[i].getWave()[1].getTroopsNo();
                                incomeTroops.Troop3 = currentVillage.getIncoming()[i].getWave()[2].getTroopsNo();
                                incomeTroops.Troop4 = currentVillage.getIncoming()[i].getWave()[3].getTroopsNo();
                                incomeTroops.Troop5 = currentVillage.getIncoming()[i].getWave()[4].getTroopsNo();
                                incomeTroops.Troop6 = currentVillage.getIncoming()[i].getWave()[5].getTroopsNo();
                                incomeTroops.Troop7 = currentVillage.getIncoming()[i].getWave()[6].getTroopsNo();
                                incomeTroops.Troop8 = currentVillage.getIncoming()[i].getWave()[7].getTroopsNo();
                                incomeTroops.Troop9 = currentVillage.getIncoming()[i].getWave()[8].getTroopsNo();
                                incomeTroops.Troop10 = currentVillage.getIncoming()[i].getWave()[9].getTroopsNo();
                                incomeTroops.Hero = currentVillage.getIncoming()[i].getWave()[10].getTroopsNo();
                                IncomingList.incoming.Add(incomeTroops);
                            }                           
                            objectListView1.GetColumn(1).ImageGetter += delegate (object rowObject)
                            {
                                return 37;
                            };                            
                            objectListView1.SetObjects(IncomingList.incoming);
                        }
                        break;

                    case Global.flag.outgoing_troop_handling:
                        if (success.Equals("troopError"))
                        {
                            debug("There is a problem with the connection while parsing troop movements!");
                        }
                        else if (success.Equals("villageError"))
                        {
                            debug("Wrong current village! " + currentVillage.getName());
                        }
                        else if (success.Equals("noRally"))
                        {
                            debug("No Rallypoint founded!");
                        }
                        else
                        {
                            OutgoingList.outgoing.Clear();
                            for (int i = 0; i < currentVillage.getOutgoing().Count; i++)
                            {
                                OutgoingList outTroops = new OutgoingList(currentVillage.getOutgoing()[i].getDesName() + " (" +
                                                currentVillage.getOutgoing()[i].getCoordinates().getX()
                                            + "," + currentVillage.getOutgoing()[i].getCoordinates().getY() + ")",
                                          currentVillage.getOutgoing()[i].getType());
                                outTroops.Resources = currentVillage.getOutgoing()[i].getTotal().ToString() + "/" +
                                            currentVillage.getOutgoing()[i].getCapacity().ToString();
                                outTroops.Troop1 = currentVillage.getOutgoing()[i].getWave()[0].getTroopsNo();
                                outTroops.Troop2 = currentVillage.getOutgoing()[i].getWave()[1].getTroopsNo();
                                outTroops.Troop3 = currentVillage.getOutgoing()[i].getWave()[2].getTroopsNo();
                                outTroops.Troop4 = currentVillage.getOutgoing()[i].getWave()[3].getTroopsNo();
                                outTroops.Troop5 = currentVillage.getOutgoing()[i].getWave()[4].getTroopsNo();
                                outTroops.Troop6 = currentVillage.getOutgoing()[i].getWave()[5].getTroopsNo();
                                outTroops.Troop7 = currentVillage.getOutgoing()[i].getWave()[6].getTroopsNo();
                                outTroops.Troop8 = currentVillage.getOutgoing()[i].getWave()[7].getTroopsNo();
                                outTroops.Troop9 = currentVillage.getOutgoing()[i].getWave()[8].getTroopsNo();
                                outTroops.Troop10 = currentVillage.getOutgoing()[i].getWave()[9].getTroopsNo();
                                outTroops.Hero = currentVillage.getOutgoing()[i].getWave()[10].getTroopsNo();
                                outTroops.Type = "";
                                OutgoingList.outgoing.Add(outTroops);
                            }
                            objectListView2.GetColumn(2).ImageGetter += delegate (object rowObject)
                            {
                                int imageListIndex = 0;

                                switch (Convert.ToInt32(objectListView2.GetColumn(1).GetValue(rowObject)))
                                {
                                    case 4:
                                    case 5:
                                        imageListIndex = 1;
                                        break;
                                    case 6:
                                        imageListIndex = 0;
                                        break;
                                    case 7:
                                        imageListIndex = scoutImage();
                                        break;
                                    case 8:
                                        imageListIndex = 36;
                                        break;
                                }                                
                                return imageListIndex;
                            };
                            objectListView2.SetObjects(OutgoingList.outgoing);
                        }                       
                        break;
                    #endregion

                    #region Map

                    case Global.flag.map_downloading:
                        if (success != "mapError")
                        {
                            debug("Map data downloaded!");
                        }
                        else
                        {
                            debug("Error downloading map!");
                        }
                        break;

                        #endregion
                }
            }
            //catch
            //{
            //    debug("Unexpected error! Please try again!");
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (LoginForm.isOK && loginForm != null)
            {
                username = loginForm.getUsername();
                password = loginForm.getPassword();
                serverUrl = loginForm.getServer();
            }
            if (username != "")
            {
                global.loginExcute(serverUrl, username, password);
                status.Text = "Logging in...";            
            }
            else
            {
                MessageBox.Show("No login information founded! Try to set them by clicking 'Login info' button.");
            }
        }

        private void updateCell(Button button, int gid, int level, byte isUpgrading)
        {
            switch (gid)
            {
                case 1:
                    button.BackColor = Color.LightGreen;
                    break;
                case 2:
                    button.BackColor = Color.SandyBrown;
                    break;
                case 3:
                    button.BackColor = Color.LightGray;
                    break;
                case 4:
                    button.BackColor = Color.Yellow;
                    break;
                case 0:
                    button.BackColor = Color.White;
                    break;
                default:
                    button.BackColor = Color.LightSkyBlue;
                    break;
            }
            switch (isUpgrading)
            {
                case 0:
                    button.Text = Buildings.titleFromGID(gid) + ", " + level;
                    break;
                case 1:
                    button.BackColor = Color.LightCoral;
                    button.Text = Buildings.titleFromGID(gid) + ", " + level + " -> " + (level + 1);
                    break;
                case 2:
                    button.BackColor = Color.LightCoral;
                    button.Text = Buildings.titleFromGID(gid) + ", " + level + " -> " + (level + 2);
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {                       
            global.incomingTroopExcute(serverUrl, currentVillage);
            global.outgoingTroopExcute(serverUrl, currentVillage);                          
        }        

        private void button6_Click(object sender, EventArgs e)
        {
            if (loginForm == null)
            {
                loginForm = new LoginForm();
            }           
            loginForm.Visible = true;
            loginForm.setUsername(username);
            loginForm.setPassword(password);
            loginForm.setServer(serverUrl);
            loginForm.setChecked(isChecked);
        }

        private void villageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = villageList.SelectedIndex;
            
            currentVillage = global.villageList.getVillage(index);
            global.dorfExcute(serverUrl, currentVillage);

            global.incomingTroopExcute(serverUrl, currentVillage);
            global.outgoingTroopExcute(serverUrl, currentVillage);
        }
    }
}
