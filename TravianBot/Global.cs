using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using HtmlAgilityPack;
using TravianBot.Functions;
using TravianBot.Functions.Decoders;
using TravianBot.Connnections;
using TravianBot.Library;

namespace TravianBot
{
    class Global
    {
        public delegate void eventDelegate(object sender, object data, flag myflag, string success);

        [Flags]
        public enum flag : byte
        {
            login_status = 1,
            dorf_handling = 2,
            incoming_troop_handling = 3,
            outgoing_troop_handling = 4,
            map_downloading = 5
        }

        private string server;

        public event eventDelegate delegMessage;

        private BackgroundWorker loginThread;
        private BackgroundWorker dorfThread;
        private BackgroundWorker incomingTroopThread;
        private BackgroundWorker outgoingTroopThread;
        private BackgroundWorker sqlmapThread;

        private DataRequest request;
        private ResourcesUpdate res;
        private BuildingsUpdate build;
        private SqlMap sqlMap;
        public VillageList villageList;

        public Global()
        {         
            villageList = new VillageList();
            request = new DataRequest();
            res = new ResourcesUpdate();
            build = new BuildingsUpdate();
            sqlMap = new SqlMap();

            loginThread = new BackgroundWorker();
            loginThread.WorkerSupportsCancellation = true;
            loginThread.DoWork += new DoWorkEventHandler(loginProgress);

            dorfThread = new BackgroundWorker();
            dorfThread.WorkerSupportsCancellation = true;
            dorfThread.DoWork += new DoWorkEventHandler(dorfProgress);

            incomingTroopThread = new BackgroundWorker();
            incomingTroopThread.WorkerSupportsCancellation = true;
            incomingTroopThread.DoWork += new DoWorkEventHandler(incomingTroopProgress);

            outgoingTroopThread = new BackgroundWorker();
            outgoingTroopThread.WorkerSupportsCancellation = true;
            outgoingTroopThread.DoWork += new DoWorkEventHandler(outgoingTroopProgress);

            sqlmapThread = new BackgroundWorker();
            sqlmapThread.WorkerSupportsCancellation = true;
            sqlmapThread.DoWork += new DoWorkEventHandler(sqlmapProgress);
        }

        #region Delegate methods

        public void doMessage(flag myflag, object message, string success)
        {
            try
            {
                if (delegMessage != null)
                {
                    Control target = delegMessage.Target as Control;

                    if (target != null && target.InvokeRequired)
                    {
                        target.Invoke(delegMessage, new object[] { this, message, myflag , success});
                    }
                    else
                    {
                        delegMessage(this, message, myflag, success);
                    }
                }
            }
            catch (Exception e)
            {
                //Log.
            }
        }

        #endregion

        #region Login

        public void loginExcute(string server, string username, string password)
        {
            string[] parameters = new string[] { server, username, password };

            if (loginThread.IsBusy != true)
            {
                loginThread.RunWorkerAsync(parameters);
            }
        }

        private void loginProgress(object sender, DoWorkEventArgs e)
        {
            string[] parameters = e.Argument as string[];

            Login login = new Login(parameters[0], parameters[1], parameters[2]);

            string loginResult = login.loginRequest();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(login.getRespond());

            sqlMap.getTravianMap(parameters[0]);
            villageList.villageListDecode(doc);

            doMessage(flag.login_status, loginResult, villageList.getException());
        }

        #endregion

        #region Sql Map

        public void sqlmapExcute(string server)
        {
            object[] parameters = new object[] { server };

            if (sqlmapThread.IsBusy != true)
            {
                sqlmapThread.RunWorkerAsync(parameters);
            }
        }

        private void sqlmapProgress(object sender, DoWorkEventArgs e)
        {
            object[] parameters = e.Argument as object[];

            string server = parameters[0].ToString();

            sqlMap.deleteData();            

            doMessage(flag.map_downloading, "", sqlMap.getTravianMap(server));
        }

        #endregion

        #region Dorf Request

        public void dorfExcute(string server, Village village)
        {
            object[] parameters = new object[] { server, village };

            if (dorfThread.IsBusy != true)
            {
                dorfThread.RunWorkerAsync(parameters);
            }
        }

        private void dorfProgress(object sender, DoWorkEventArgs e)
        {
            object[] parameters = e.Argument as object[];

            string server = parameters[0].ToString();
            Village village = (Village)parameters[1];           

            HtmlAgilityPack.HtmlDocument dorf1 = request.dorf1Request(server, village);
            HtmlAgilityPack.HtmlDocument dorf2 = request.dorf2Request(server, village);

            res.updateResources(dorf1, village);
            build.updateResourcesFields(dorf1, village);
            build.updateVillageCenter(dorf2, village);
            string up = build.upgradingDecode(dorf1, village);

            doMessage(flag.dorf_handling, up, res.getException() + ";" + build.getResException() + ";" + build.getBuildException());
        }

        #endregion

        #region Troop

        public void incomingTroopExcute(string server, Village currentVillage)
        {
            object[] parameters = new object[] { server, currentVillage };

            if (incomingTroopThread.IsBusy != true)
            {
                incomingTroopThread.RunWorkerAsync(parameters);
            }
        }

        public void outgoingTroopExcute(string server, Village currentVillage)
        {
            object[] parameters = new object[] { server, currentVillage };

            if (outgoingTroopThread.IsBusy != true)
            {
                outgoingTroopThread.RunWorkerAsync(parameters);
            }
        }

        private void incomingTroopProgress(object sender, DoWorkEventArgs e)
        {
            object[] parameters = e.Argument as object[];

            string server = parameters[0].ToString();
            Village village = (Village)parameters[1];
            village.refreshIncoming();
         
            TroopsMove troop = new TroopsMove();

            string final = "";
            HtmlAgilityPack.HtmlDocument initial = request.incomingTroopRequest(server, 1);
            int count = troop.getRequestNumber(troop.troopCount(initial));

            if (count == 0)
            {
                doMessage(flag.incoming_troop_handling, count, final);
            }
            else if (count == -1)
            {
                doMessage(flag.outgoing_troop_handling, count, "noRally");
            }
            else
            {
                final += troop.troopDecode(initial, village);

                for (int i = 2; i < count; i++)
                {
                    final += troop.troopDecode(request.incomingTroopRequest(server, i), village);
                }
                doMessage(flag.incoming_troop_handling, count, final);
            }
        }

        private void outgoingTroopProgress(object sender, DoWorkEventArgs e)
        {
            object[] parameters = e.Argument as object[];

            string server = parameters[0].ToString();
            Village village = (Village)parameters[1];
            village.refreshOutgoing();

            TroopsMove troop = new TroopsMove();

            string final = "";
            HtmlAgilityPack.HtmlDocument initial = request.outgoingTroopRequest(server, 1);
            int count = troop.getRequestNumber(troop.troopCount(initial));

            if (count == 0)
            {
                doMessage(flag.outgoing_troop_handling, count, final);
            }
            else if (count == -1)
            {
                doMessage(flag.outgoing_troop_handling, count, "noRally");
            }
            else
            {
                final += troop.troopDecode(initial, village);

                for (int i = 2; i < count; i++)
                {
                    final += troop.troopDecode(request.outgoingTroopRequest(server, i), village);
                }
                doMessage(flag.outgoing_troop_handling, count, final);
            }            
        }

        #endregion
    }
}
