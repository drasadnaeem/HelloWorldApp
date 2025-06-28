using Squirrel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Squirrel;

namespace HelloWorldApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello, World! New Version " + Application.ProductVersion);
        }

        

    private async void CheckForUpdates()
        {
            try
            {
                using (var mgr = await UpdateManager.GitHubUpdateManager("https://github.com/drasadnaeem/HelloWorldApp"))
                {
                    var updateInfo = await mgr.CheckForUpdate();

                    if (updateInfo.ReleasesToApply.Any())
                    {
                        DialogResult result = MessageBox.Show(
                            "A new update is available. Would you like to download and install it now?",
                            "Update Available",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );

                        if (result == DialogResult.Yes)
                        {
                            await mgr.UpdateApp();
                            MessageBox.Show("Update installed. Please restart the application.");
                            Application.Restart();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update check failed: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForUpdates();
        }
    }
}
