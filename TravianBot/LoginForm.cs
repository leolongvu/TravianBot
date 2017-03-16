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

namespace TravianBot
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            isOK = false;
        }

        public static bool isOK;

        public string getUsername()
        {
            return this.username.Text;
        }
        public string getPassword()
        {
            return this.pass.Text;
        }
        public string getServer()
        {
            return this.server.Text;
        }

        public void setServer(string server)
        {
            this.server.Text = server;
        }
        public void setUsername(string username)
        {
            this.username.Text = username;
        }
        public void setPassword(string pass)
        {
            this.pass.Text = pass;
        }
        public void setChecked(bool check)
        {
            this.checkBox1.Checked = check;
        }

        private void saveConfigs()
        {
            if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"\Configs"))
            {
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"\Configs");
            }
            TextWriter config_writer = new StreamWriter(Application.StartupPath + @"\Configs\" + "login" + ".ini");
            {
                config_writer.WriteLine(checkBox1.Checked);
                if (checkBox1.Checked)
                {
                    config_writer.WriteLine(username.Text);
                    config_writer.WriteLine(Security.Encrypt(pass.Text));
                    config_writer.WriteLine(server.Text);
                }
                config_writer.WriteLine("//");
                config_writer.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isOK = true;
            saveConfigs();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
