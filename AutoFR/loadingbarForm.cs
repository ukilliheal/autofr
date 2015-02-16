using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoFR;

namespace AutoFR
{
    public partial class loadingbarForm : Form
    {
        //f4.Show();

        Boolean q = true;
        BackgroundWorker worker = new BackgroundWorker();
        //Form1 f4 = new Form1();
        string labeltextpls = "";

        public loadingbarForm()
        {

            InitializeComponent();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged); 
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            this.VisibleChanged += new EventHandler(this.Label_VisibleChanged);
            this.worker.RunWorkerAsync(null);

            Shown += Form1_Shown;

            /*
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate()
            {
                loader(); // Do all the ui thread updates here
            }));
             * */
            //loader();
            //new Thread(new ThreadStart(loader)).Start();
            //thread.Start();
        }
        /*
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            loader();
        }
         */
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // The progress percentage is a property of e

            int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);

            //this.label1.Text = labeltextpls;
            progressBar1.Value = e.ProgressPercentage;

        }
        private void Label_VisibleChanged(object sender, EventArgs e)
        {
            //Application.Run(f4.Show);
            //this.Hide();
            /*
            if (q == false)
            {
                MainForm.Invoke((MethodInvoker)delegate()
                {
                    f4.Show();
                });
            }
             * */

        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            q = false;
            //loadingbarForm objfrmSChild = loadingbarForm.GetChildInstance();
            //Boolean result = (Boolean)Form1.Invoke(Form1.showme);
            //(System.Windows.Forms.Application.OpenForms["Form1"] as Form1).Show();
            //f4.Invoke((MethodInvoker)delegate() {
            //    f4.Show();
            //});
            //this.Hide();
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            labeltextpls = "Scanning local drives. Looking for the folder DeploymentImages";

            //(System.Windows.Forms.Application.OpenForms["Form1"] as Form1).getdisks();

/*
            int h = 14;
            labeltextpls = "Scanning local drives. Looking for the folder DeploymentImages";
            worker.ReportProgress(h);
            //MessageBox.Show("Test");
            f4.scanforfolder();
            //worker.ReportProgress(h, "");
            h = h + 14;
            System.Threading.Thread.Sleep(500);
            labeltextpls = "Searching Online and local drives for updates";
            worker.ReportProgress(h);
            f4.checkimagethenupdate();
            //worker.ReportProgress(h);
            h = h + 14;
            System.Threading.Thread.Sleep(500);
            labeltextpls = "Compiling list of avalible drive letters";
            worker.ReportProgress(h);
            f4.listavalibleDriveLetters();
            //worker.ReportProgress(h);
            h = h + 14;
            System.Threading.Thread.Sleep(500);
            labeltextpls = "Searching for all attached drives";
            worker.ReportProgress(h);
            f4.getdisks();
            //worker.ReportProgress(h);
            h = h + 14;
            System.Threading.Thread.Sleep(500);
            labeltextpls = "Building lists";
            worker.ReportProgress(h);
            f4.chooseunuseddrive();
            //worker.ReportProgress(h);
            h = h + 14;
            System.Threading.Thread.Sleep(500);
            labeltextpls = "Enabling buttons";
            worker.ReportProgress(h);
            f4.filenams();
            //worker.ReportProgress(h);
            h = h + 14;
            System.Threading.Thread.Sleep(500);
            labeltextpls = "Scanning for Pre-installation Environment";
            worker.ReportProgress(h);
            f4.checkifautoreboot();
            labeltextpls = "Resetting the Flux Capacitor";
            worker.ReportProgress(100);
            System.Threading.Thread.Sleep(100);

            //worker.ReportProgress(h);
            //f4.Show();
 * */
        }
        private void Form1_Shown(Object sender, EventArgs e)
        {
        }
        public void moveprogressbar(int x, String h)
        {
            //label1.Text = h;
            progressBar1.Value = progressBar1.Value + x;
        }
    }
}
