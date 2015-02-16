using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using AutoFR;

namespace AutoFR
{
    public partial class listdownloads : Form
    {
        Stopwatch sw = new Stopwatch();
        WebClient webClient = new WebClient();
        Boolean cancledl = false;
        String ppath = "";
        String x = "";
        Boolean completedDL = false;
        public listdownloads()
        {
            InitializeComponent();
        }
        public void downloadwimfile(string filebeingdownloaded, string deploymentfolderroot, Boolean DeployAD, Boolean hideckbx, Boolean deployafterdownload1, Boolean deploymentfolderexists) // filebeingdownloaded = x
        {
            //MessageBox.Show("filebeingdownloaded: " + filebeingdownloaded + "\ndeploymentfolderroot: " + deploymentfolderroot + "\nDeployAD: " + DeployAD + "\nhideckbx: " + hideckbx + "\ndeployafterdownload1: " + deployafterdownload1 + "\ndeploymentfolderexists: " + deploymentfolderexists);

            if (deploymentfolderexists == false)
            {
                this.Hide();  //resetdownloadwim()
                if (System.Windows.Forms.Application.OpenForms["Form1"] != null)
                {
                    (System.Windows.Forms.Application.OpenForms["Form1"] as Form1).resetdownloadwim();
                }
                MessageBox.Show("Unable to download file. The DeploymentImages folder is missing. \nPlease create this folder on the root of any drive and re-open this program.");
                sw.Stop();
                this.Close();
                return;
            }

            x = filebeingdownloaded;
            if (hideckbx == true)
            {
                deployafterdownload.Checked = false;
                deployafterdownload.Visible = false;
            }
            //TODO Fix this: when you uncheck "deploy after download" on the drop down menu. it still deploys, but it aslo checks and disables the checkbox on this form. Fix it
            //Also, when download finishes, if not auto-deploying, then change cancle button to "Deploy now" button, and leave it opened until user closes download window. 
            if (deployafterdownload1 == true)
            {
                deployafterdownload.Checked = true;
            }
            else
            {
                deployafterdownload.Checked = false;
            }
            this.Text = filebeingdownloaded;
            if (DeployAD == true)
            {
                //deployafterdownload.Checked = true;
            }
            else
            {
                deployafterdownload.Checked = false;
            }
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed2);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged2);
            string url = "HTTP://www.example.com:4029/DeploymentImages/" + filebeingdownloaded;
            sw.Start();
            label1.Text = "Downloading " + filebeingdownloaded;
            ppath = deploymentfolderroot + filebeingdownloaded;
            webClient.DownloadFileAsync(new Uri(url), ppath);
        }
        private void ProgressChanged2(object sender, DownloadProgressChangedEventArgs e)
        {
            int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
            progressBar1.CreateGraphics().DrawString(percent.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(progressBar1.Width / 2 - 10, progressBar1.Height / 2 - 7));
            labelSpeed.Text = string.Format("{0} kb/s", (e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds).ToString("000.00"));
            progressBar1.Value = e.ProgressPercentage;
            labelDownloaded.Text = string.Format("{0} MB's / {1} MB's",
                (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
                (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
        }
        #region Download complete/Check for Cancel
        private void Completed2(object sender, AsyncCompletedEventArgs e)
        {
            sw.Stop();

            if (deployafterdownload.Checked == true && cancledl != true)
            {
                if (System.Windows.Forms.Application.OpenForms["Form1"] != null)
                {
                    (System.Windows.Forms.Application.OpenForms["Form1"] as Form1).extStartWipeInstall(x);
                    this.Close();
                }
            }
            if (cancledl == true)
            {
                this.Close();
            }
            else
            {
                completedDL = true;
                button1.Text = "Deploy now";
            }
            if (deployafterdownload.Visible == false)
            {
                System.Threading.Thread.Sleep(500);
                this.Close();
            }
        }
        #endregion
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (completedDL == false)
            {
                button1.Enabled = false;
                progressBar1.Enabled = false;
                cancledl = true;
                webClient.CancelAsync();
                try
                {
                    System.Threading.Thread.Sleep(2000);
                    File.Delete(ppath);

                }
                catch (Exception crash)
                {
                    MessageBox.Show(crash.Message);
                }
            }
            else
            {
                if (System.Windows.Forms.Application.OpenForms["Form1"] != null)
                {
                    (System.Windows.Forms.Application.OpenForms["Form1"] as Form1).extStartWipeInstall(x);
                }
                this.Hide();
            }

        }
    }
}
