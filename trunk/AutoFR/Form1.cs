using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
//using AutoUpdaterDotNET;
using Microsoft.Win32;
using System.Net;
using System.Windows;
using AutoFR;
using System.Threading;

namespace AutoFR
{
    public partial class Form1 : Form
    {
        #region Windows versions defined
        //64 bit
        //Windows 8.1
        string win81core64v = "Win81Core_x64.wim";
        string win81pro64 = "Win81Pro_x64.wim";
        string win81ent64 = "Win81Ent_x64.wim";
        //Windows 8
        string win8rt64 = "Win8RT_x64.wim";
        string win8core64 = "Win8core_x64.wim";
        string win8pro64 = "Win8Pro_x64.wim";
        string win8ent64 = "Win8ent_x64.wim";

        //Windows 7
        string win7start64 = "Win7Start_x64.wim";
        string win7hb64 = "Win7HomeB_x64.wim";
        string win7hp64 = "Win7HomeP_x64.wim";
        string win7pro64 = "Win7Pro_x64.wim";
        string win7ent64 = "Win7Ent_x64.wim";
        string win7ult64 = "Win7Ult_x64.wim";

        //Windows Vista
        string winvistastart64 = "WinVistaStart_x64.wim";
        string winvistahb64 = "WinVistaHomeB_x64.wim";
        string winvistahp64 = "WinVistaHomeP_x64.wim";
        string winvistab64 = "WinVistaBus_x64.wim";
        string winvistaent64 = "WinVistaEnt_x64.wim";
        string winvistault64 = "WinVistaUlt_x64.wim";



        //32 bit
        //Windows 8.1
        string win81core86 = "Win81Core_x86.wim";
        string win81pro86 = "Win81Pro_x86.wim";
        string win81ent86 = "Win81Ent_x86.wim";
        //Windows 8
        string win8rt86 = "Win8RT_x86.wim";
        string win8core86 = "Win8core_x86.wim";
        string win8pro86 = "Win8Pro_x86.wim";
        string win8ent86 = "Win8ent_x86.wim";


        //Windows 7
        string win7start86 = "Win7Start_x86.wim";
        string win7hb86 = "Win7HomeB_x86.wim";
        string win7hp86 = "Win7HomeP_x86.wim";
        string win7pro86 = "Win7Pro_x86.wim";
        string win7ent86 = "Win7Ent_x86.wim";
        string win7ult86 = "Win7Ult_x86.wim";


        //Windows Vista
        string winvistastart86 = "WinVistaStart_x86.wim";
        string winvistahb86 = "WinVistaHomeB_x86.wim";
        string winvistahp86 = "WinVistaHomeP_x86.wim";
        string winvistab86 = "WinVistaBus_x86.wim";
        string winvistaent86 = "WinVistaEnt_x86.wim";
        string winvistault86 = "WinVistaUlt_x86.wim";


        #endregion

        List<string> windowsversionsd = new List<string>();
        List<string> windowsimagefileexisits = new List<string>();
        string customwim = "";
        int appVersion = 4;
        string nextappVersion = "5"; // Name as "AutoDeploy_5.exe"
        Boolean downloadwim = false;
        string filebeingdownloaded = "";
        string filebeingdeployted = "";
        String imagefilepath = "notfound";
        String xDrive = "notfound";
        List<string> avalibleDriveLetters = new List<string>();
        List<string> usedDriveLetters = new List<string>();
        List<string> avaliblewimfordownload = new List<string>();
        String mountdriveas;
        String userselectedvolume = "notSelected";
        String selecteddisknum = "notSelected";
        string deploymentfolderroot = "";
        Boolean deploymentfolderexists = false;
        Boolean isloading = true;
        BackgroundWorker worker = new BackgroundWorker();
        loadingbarForm f4 = new loadingbarForm();
        string bcdbootlocation = "notfound";



        public Form1()
        {
            this.VisibleChanged += new EventHandler(this.Label_VisibleChanged);

            //loadingbarForm f2 = new loadingbarForm();
            InitializeComponent();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            //worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            this.FormClosing += Form1_FormClosing;
            this.Hide();
            Shown += Form1_Shown;




        }
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void Label_VisibleChanged(object sender, EventArgs e)
        {
        }
        private void button2_Click(object sender, EventArgs e)
        {
            WipeInstall(win8pro86);
        }

        public void extStartWipeInstall(String x)
        {
            WipeInstall(x);
        }
        public void resetdownloadwim()
        {
            downloadwim = true;
        }
        /*
        private void startwipeinstall()
        {
            Form1 f7 = new Form1();
            f7.WipeInstall1(filebeingdeployted);
            f7.Close();

        }
        private void WipeInstall(String x)
        {
            filebeingdeployted = x;
            new Thread(new ThreadStart(startwipeinstall)).Start();

        }
         * */
        private void WipeInstall(String x)
        {
            foreach (string file in windowsversionsd)
            {
                if (File.Exists(imagefilepath + "\\" + file))
                {
                    windowsimagefileexisits.Add(file);
                }
            }
            Boolean needtodownloadfile = true;
            if (downloadwim == true) //downloadwim == true //downloadwimfile //avaliblewimfordownload //windowsimagefileexisits
            {
                downloadwim = false;
                foreach (string l in windowsimagefileexisits) 
                {
                    if (x.Trim() == l.Trim())
                    {
                        needtodownloadfile = false;
                    }
                } //TODO change this, make it look at an array (list) of files that need to be downloaded, then see if the current file is on that list, if so then open downloader. 
                if (needtodownloadfile == true) 
                {
                    downloadwim = true;
                    listdownloads f2 = new listdownloads(); 
                    f2.Show();
                    f2.downloadwimfile(x, deploymentfolderroot + "\\DeploymentImages\\", deployimgChkBx.Checked, false, deployafterdownload.Checked, deploymentfolderexists);
                    //f2.downloadwimfile(,,)
                    return; 
                    //TODO Check if file really was downloaded. 
                    //If it was, remove it from the list, rename button. If not, keep it in list of need to download, but add it to list of failed to download so it does't just keep looping.
                }
            }
            string strCmdText;
            if (WriteMBRChkBox.Checked == true && FormatDriveChkBx.Checked == true && createpartChkbx.Checked == true && DeletePartitionsChkBx.Checked == true)
            {
                try
                {
                    filterselecteddisk();
                    makediskpartscript();
                    strCmdText = "/c diskpart /s " + xDrive + "WipeC.txt";
                    System.Diagnostics.Process.Start("CMD.exe", strCmdText).WaitForExit();
                }
                catch (Exception crash)
                {
                    MessageBox.Show(crash.Message);
                }
            }
            if (deployimgChkBx.Checked == true)
            {
                try
                {
                    if (x != customwim)
                    {
                        strCmdText = String.Format("/c " + xDrive + "imagex.exe /apply {0}\\DeploymentImages\\" + x + " 1 " + mountdriveas, deploymentfolderroot);
                        System.Diagnostics.Process.Start("CMD.exe", strCmdText).WaitForExit();
                        if (WriteBCDChkBox.Checked == true)
                        {
                            strCmdText = "/c " + "bcdboot " + mountdriveas + "windows /s " + mountdriveas.Replace(@"\", "");
                            System.Diagnostics.Process.Start("CMD.exe", strCmdText).WaitForExit();
                        }
                    }
                    else
                    {
                        strCmdText = String.Format("/c " + xDrive + "imagex.exe /apply {0}" + x + " 1 " + mountdriveas, customwim);
                        System.Diagnostics.Process.Start("CMD.exe", strCmdText).WaitForExit();
                    }
                }
                catch (Exception crash)
                {
                    MessageBox.Show(crash.Message);
                }
            }
            else
            {
                MessageBox.Show("Deployment skipped: \n Deploy Image checkbox is unchecked");
            }
            if (autorebootChkBx.Checked == true)
            {
                strCmdText = "/c " + "shutdown /r /f /t 60";
                System.Diagnostics.Process.Start("CMD.exe", strCmdText).WaitForExit();
                this.Close();
            }

        }
        private void button3_Click(object sender, EventArgs e)
        {
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            loader();

            //this.worker.RunWorkerAsync(null);

            //this.Hide();

            //this.Show();

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //(System.Windows.Forms.Application.OpenForms["loadingbarForm"] as loadingbarForm).Close();
            //this.Close();
        }
        private void Form1_Shown(Object sender, EventArgs e)
        {
            /* ***************************
            scanforfolder(); 
            checkimagethenupdate();
            listavalibleDriveLetters();
            getdisks
            chooseunuseddrive();
            filenams();
            checkifautoreboot();

            */ //* ****************************
            //scanforfolder(); 
            /*
            if (imagefilepath != "notfound")
            {
                checkupdates(); //Has to go after "scanforfolder"
            } 
             */
            //listavalibleDriveLetters();
            //getdisks();
            //chooseunuseddrive();
            //filenams();
            /*
            var key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\WinPE");
            if (key == null)
            {
                autorebootChkBx.Checked = false;
            }
             */
            //f4.Close();
        }
        private void loader()//worker_DoWork(object sender, DoWorkEventArgs e)
        {
            f4.Show();

            //(System.Windows.Forms.Application.OpenForms["Form1"] as Form1).Hide();


            int h = 1;
            this.scanforfolder();
            //Thread thread = new Thread(new ThreadStart(loader));
            //thread.Start();
            f4.moveprogressbar(14, "Scanning local drives. Looking for the folder DeploymentImages");

            System.Threading.Thread.Sleep(h);

            //MessageBox.Show("checkimagethenupdate");
            checkimagethenupdate();
            f4.moveprogressbar(14, "Searching Online and local drives for updates");
            System.Threading.Thread.Sleep(h);

            //MessageBox.Show("listavalibleDriveLetters");
            listavalibleDriveLetters();
            f4.moveprogressbar(14, "Compiling list of avalible drive letters");
            System.Threading.Thread.Sleep(h);

            //MessageBox.Show("getdisks");
            getdisks();
            f4.moveprogressbar(14, "Searching for all attached drives");
            System.Threading.Thread.Sleep(h);

            //MessageBox.Show("chooseunuseddrive");
            chooseunuseddrive();
            f4.moveprogressbar(14, "Building lists");
            System.Threading.Thread.Sleep(h);

            //MessageBox.Show("filenams");
            filenams();
            f4.moveprogressbar(14, "Enabling buttons");
            System.Threading.Thread.Sleep(h);

            //MessageBox.Show("checkifautoreboot");
            checkifautoreboot();
            f4.moveprogressbar(16, "Scanning for Pre-installation Environment");
            System.Threading.Thread.Sleep(h);
            //(System.Windows.Forms.Application.OpenForms["Form1"] as Form1).Show();
            f4.Close();
        }

        public void checkifautoreboot()
        {
            var key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\WinPE");
            if (key == null)
            {
                autorebootChkBx.Checked = false;
            }
        }
        public void checkimagethenupdate()
        {
            if (imagefilepath != "notfound")
            {
                checkupdates(); //Has to go after "scanforfolder"
            }
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            WipeInstall(win81pro64);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            WipeInstall(win81core64v);
        }

        private void cMDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("CMD.exe");
        }

        private void listDrivesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getdisks();
        }
        private void makediskpartscript()
        {
            string xpath = xDrive + "WipeC.txt";
            if (File.Exists(@xpath))
            {
                File.Delete(@xpath);
            }

            string clsvar = "";
            string seldsk = "";
            string createparpri = "";
            string assignl = "";
            string activedp = "";
            string formatdp = "";
            //
            if (UseDiskNumChkBx.Checked == true)
            {
                seldsk = "select disk " + selecteddisknum.Trim();
                assignl = "assign letter=" + mountdriveas.Replace(@"\", "").Replace(":", "");

            }
            else if (UseDriveLetterChkBx.Checked == true)
            {
                seldsk = "select volume " + userselectedvolume.Replace(@"\", "").Replace(":", "").Trim();
            }
            //
            if (createpartChkbx.Checked == true)
            {
                createparpri = "create partition primary";
            }
            if (DeletePartitionsChkBx.Checked == true)
            {
                clsvar = "clean";
            }
            if (WriteMBRChkBox.Checked == true)
            {
                activedp = "active";
            }
            if (FormatDriveChkBx.Checked == true)
            {
                formatdp = "format fs=ntfs label=Windows quick";
            }
            if (UseDriveLetterChkBx.Checked == true)
            {
                string[] lines = { seldsk, formatdp };
                System.IO.File.WriteAllLines(@xpath, lines);
            }else if (UseDiskNumChkBx.Checked == true){
                if (createpartChkbx.Checked == false && DeletePartitionsChkBx.Checked == false && WriteMBRChkBox.Checked == false && FormatDriveChkBx.Checked == false)
                {
                    clsvar = "sel part 1";
                }
                string[] lines = { seldsk, clsvar, createparpri, assignl, formatdp, activedp };
                System.IO.File.WriteAllLines(@xpath, lines);
            }
        }
        public void getdisks(){
        string output = string.Empty;
            string error = string.Empty;
            ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd", "/c wmic diskdrive list brief /format:list");
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processStartInfo.UseShellExecute = false;

            Process process = Process.Start(processStartInfo);
            using (StreamReader streamReader = process.StandardOutput)
            {
                output = streamReader.ReadToEnd();
            }
            using (StreamReader streamReader = process.StandardError)
            {
                error = streamReader.ReadToEnd();
            }

            string[] words;
            words = output.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
            String DriveID = "Sk1p";
            String Size = "Sk1p";
            String Caption = "Sk1p";

            foreach (string i in words)
            {
                richTextBox1.AppendText(i);
                richTextBox1.AppendText(Environment.NewLine);

                if (i.Contains("DeviceID"))
                {
                    DriveID = i.Replace("DeviceID=\\\\.\\PHYSICALDRIVE", "");
                }
                if (i.Contains("Caption"))
                {
                    Caption = i.Replace("Caption=", "");
                } 
                if (i.Contains("Size"))
                {
                    int number;
                    string a = i.Replace("Size=", "").Replace(" ", "").Replace(System.Environment.NewLine, "").Trim();
                    char[] charArray = a.ToCharArray();
                    double num = 0;
                    for (int l = 0; l < charArray.Length; l++) //for (int l = charArray.Length - 1; l > 0; l--)

                    {
                        if (Int32.TryParse(charArray[l].ToString(), out number))
                        {
                            double charminl = charArray.Length - l;
                            double zeros = Math.Pow(10.00, charminl);
                            double tpo = number * zeros;
                            num = num + tpo;
                        }
                    }
                    num = num / 10;
                    double c = num / 1073741824;
                    double u = Math.Round(c, 1);
                    Size = u.ToString();
                    richTextBox1.AppendText("-------------");
                    richTextBox1.AppendText(Environment.NewLine);
                }
                if (!DriveID.Contains("Sk1p") & !Size.Contains("Sk1p") & !Caption.Contains("Sk1p")) 
                {
                    comboBox1.Items.Add("DISK" + DriveID + " " + Caption + "  " + Size + " GB");
                    comboBox1.SelectedIndex = 0;
                    DriveID = "Sk1p";
                    Size = "Sk1p";
                    Caption = "Sk1p";
                }
            }
            int q = 1;
            if (comboBox1.SelectedIndex != -1)
            {
                while (!comboBox1.SelectedItem.ToString().Contains("DISK0"))
                {
                    comboBox1.SelectedIndex = q;
                    q++;
                }
            }

            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine("The following error was detected:");
                Console.WriteLine(error);
            }
            Console.Read();
    }
        /* //Original WMIC parsing
           string output = string.Empty;
            string error = string.Empty;

            ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd", "/c wmic diskdrive list brief /format:list");
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
            processStartInfo.UseShellExecute = false;

            Process process = Process.Start(processStartInfo);
            using (StreamReader streamReader = process.StandardOutput)
            {
                output = streamReader.ReadToEnd();
            }
            using (StreamReader streamReader = process.StandardError)
            {
                error = streamReader.ReadToEnd();
            }
            string[] words;
            words = output.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
            String DriveID = "Sk1p";
            String Size = "Sk1p";
            String Caption = "Sk1p";

            foreach (string i in words)
            {

                if (i.Contains("DeviceID"))
                {
                    DriveID = i.Replace("DeviceID=\\\\.\\PHYSICALDRIVE", "");
                }
                if (i.Contains("Caption"))
                {
                    Caption = i.Replace("Caption=", "");
                } 
                if (i.Contains("Size"))
                {
                    int number;
                    string a = i.Replace("Size=", "").Replace(" ", "").Replace(System.Environment.NewLine, "").Trim();
                    char[] charArray = a.ToCharArray();
                    double num = 0;
                    for (int l = 0; l < charArray.Length; l++) //for (int l = charArray.Length - 1; l > 0; l--)

                    {
                        if (Int32.TryParse(charArray[l].ToString(), out number))
                        {
                            double charminl = charArray.Length - l;
                            double zeros = Math.Pow(10.00, charminl);
                            double tpo = number * zeros;
                            num = num + tpo;
                        }
                    }
                    num = num / 10;
                    double c = num / 1073741824;
                    double u = Math.Round(c, 1);
                    Size = u.ToString();
                }
                if (!DriveID.Contains("Sk1p") & !Size.Contains("Sk1p") & !Caption.Contains("Sk1p"))
                {
                    //richTextBox1.AppendText("DISK" + DriveID + " " + Caption + "  " + Size + " GB");
                    //richTextBox1.AppendText(Environment.NewLine);
                    comboBox1.Items.Add("DISK" + DriveID + " " + Caption + "  " + Size + " GB");
                    comboBox1.SelectedIndex = 0;
                    DriveID = "Sk1p";
                    Size = "Sk1p";
                    Caption = "Sk1p";
                }
            }
            string adfda = comboBox1.SelectedItem.ToString();
            //MessageBox.Show("adfda: "+adfda);
            int q = 1;
            while (!comboBox1.SelectedItem.ToString().Contains("DISK0"))
            {
                comboBox1.SelectedIndex = q;
                q++;
            }
            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine("The following error was detected:");
                Console.WriteLine(error);
            }
            Console.Read();
         * */


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void returnSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(comboBox1.SelectedItem.ToString());
        }

        private void writeDPartScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterselecteddisk();
            makediskpartscript();
        }
        public void listavalibleDriveLetters()
        {
            for (char c = 'A'; c <= 'Z'; c++)
            {
                Boolean addtolist = true;
                foreach (string i in usedDriveLetters)
                {
                    if (i.Contains(c))
                    {
                        addtolist = false;
                    }
                }
                if (addtolist == true)
                {
                    avalibleDriveLetters.Add(c + ":\\");
                    reMapas2.Items.Add(c + ":\\");
                }
             }
        }
        public void chooseunuseddrive()
        {
            // mountdriveas =;
            int a = avalibleDriveLetters.Count;
            if (a >= 1)
            {
                a--;
                mountdriveas = avalibleDriveLetters[a];
                reMapas2.SelectedIndex = a;
            }
            else if (a <= 0)
            {
                MessageBox.Show("All drive letters used. Please free up one\nYou should never see this error. God speed.");
            }
        }
        private void scanforimageX () 
        {

        }
        public void scanforfolder() //TODO Ask user if he wants to make folder now
        {
            string foundimagex = "no";
            //comboBox1.Items.Clear();
            comboBox3.Items.Clear();
            Boolean downloadbcdboot = false;
            Boolean downloadimagex2 = false;
            var key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\WinPE");
            if (key != null) //Checks if we are running in WinPE. if so then the next line of code is exicuted. 
            {
                xDrive = Path.GetPathRoot(Environment.SystemDirectory); // C:\
            }
            Boolean isExists = false;

            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady == true)
                {
                    comboBox3.Items.Add(drive.Name + " " + drive.TotalSize / 1073741824 + " GB");
                    usedDriveLetters.Add(drive.Name);
                    string q = drive.Name + "DeploymentImages";
                    if (Directory.Exists(@q))
                    {
                        imagefilepath = q; //Should be ?:\DeploymentImages
                        deploymentfolderexists = true;
                        deploymentfolderroot = drive.Name; // Should show up as x:\
                    }
                    if (File.Exists(drive.Name + "imagex.exe"))
                    {
                        xDrive = drive.Name;
                        foundimagex = "yes";
                    }
                    if (File.Exists(drive.Name + "bcdboot.exe"))
                    {
                        bcdbootlocation = drive.Name;
                    }

                }
            }

            if (xDrive == "notfound" && imagefilepath == "notfound")
            {
                //MessageBox.Show("File not found: imagex.exe \nFolder not found: DeploymentImages \nPlease choose a drive, and create a folder named DeploymentImages\nPlease choose a drive, and place imagex.exe on the root\nDeployment will not be possible", "AutoDeploy");
                if (!isExists)
                {
                    System.IO.Directory.CreateDirectory(Application.StartupPath + "\\DeploymentImages");
                    imagefilepath = Application.StartupPath + "\\DeploymentImages\\";
                    //xDrive = Application.StartupPath + "\\DeploymentImages";
                    deploymentfolderexists = true;
                }
                if (isExists)
                {
                    imagefilepath = Application.StartupPath + "\\DeploymentImages\\";
                    //xDrive = Application.StartupPath + "\\DeploymentImages";
                    deploymentfolderexists = true;
                }
            }
            if (xDrive == "notfound")
            {
                isExists = System.IO.Directory.Exists(Application.StartupPath + "\\DeploymentImages");
                if (isExists)
                {
                    xDrive = Application.StartupPath + "\\DeploymentImages\\";
                    deploymentfolderroot = Application.StartupPath;
                }
            }
            if (xDrive != "notfound" && imagefilepath == "notfound")
            {
                MessageBox.Show("Warning: Unable to detect deployment image folder. \nDeployment will not be possible without that folder\nPlease create the folder \"DeploymentImages\" on the root of any drive", "AutoDeploy");
                deploymentfolderexists = false;

            }
            if (bcdbootlocation == "notfound" && imagefilepath != "notfound")
            {
                if (!File.Exists(imagefilepath + "bcdboot.exe"))
                {
                    DialogResult result2 = MessageBox.Show("bcdboot.exe not found. Deployment will not be possible. \nWould you like to download it to " + imagefilepath + " ?",
                        "AutoDeploy",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);
                    if (result2 == DialogResult.Yes)
                    {
                        downloadbcdboot = true;
                    }
                    if (result2 == DialogResult.No)
                    {

                    }
                    if (result2 == DialogResult.Cancel)
                    {
                        Environment.Exit(0);
                    }
                }
            }
            System.Threading.Thread.Sleep(500);
            if (!File.Exists(imagefilepath + "imagex.exe") && !File.Exists(deploymentfolderroot + "imagex.exe") && foundimagex == "no")
            {
                //MessageBox.Show(Application.StartupPath);
                DialogResult result2 = MessageBox.Show("imagex.exe not found. Deployment will not be possible. \nWould you like to download it to " + imagefilepath + " ?",
                    "AutoDeploy",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);
                if (result2 == DialogResult.Yes)
                {
                    downloadimagex2 = true;
                }
                if (result2 == DialogResult.No)
                {

                }
                if (result2 == DialogResult.Cancel)
                {
                    Environment.Exit(0);
                }
            }
            if (downloadbcdboot == true)
            {   //downloadwim("imagex.exe");
                listdownloads f2 = new listdownloads();
                f2.Show();
                f2.downloadwimfile("bcdboot.exe", imagefilepath, false, true, deployafterdownload.Checked, deploymentfolderexists);

            }
            if (downloadimagex2 == true)
            {
                //downloadwim("imagex.exe");
                listdownloads f2 = new listdownloads();
                f2.Show();
                f2.downloadwimfile("imagex.exe", imagefilepath, false, true, deployafterdownload.Checked, deploymentfolderexists);
            }
            /*
            string u = "";
            foreach (var drive in DriveInfo.GetDrives())
            {

                if (drive.IsReady == true)
                {
                    comboBox3.Items.Add(drive.Name + " " + drive.TotalSize / 1073741824 + " GB");
                    usedDriveLetters.Add(drive.Name);
                    string q = drive.Name + "DeploymentImages";
                    if (Directory.Exists(@q))
                    {
                        imagefilepath = q; //Should be ?:\DeploymentImages
                        u = drive.Name; // Should show up as x:\
                    }
                    if (File.Exists(drive.Name + "imagex.exe"))
                    {
                        xDrive = drive.Name;
                    }
                }
            }
            var key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\WinPE");
            if (key != null)
            {
                xDrive = Path.GetPathRoot(Environment.SystemDirectory); // C:\
            }
            else
            {
                xDrive = u;
                if (!imagefilepath.Contains("notfound"))
                {
                    MessageBox.Show("Looks like you are running this program outside of WinPE \n Images will be saved in " + imagefilepath);
                }
            }
            if (imagefilepath.Contains("notfound"))
            {

                MessageBox.Show("Warning: Unable to detect deployment image folder. \n It should be called \"DeploymentImages\" on the root of any drive \n Example: D:\\DeploymentImages\\",
		        "Auto Deploy",
		        MessageBoxButtons.OK,
		        MessageBoxIcon.Exclamation,
		        MessageBoxDefaultButton.Button1);
            }
             * */
        }

        public void filenams()
        {
            windowsversionsd.Add(win81core64v);
            windowsversionsd.Add(win81pro64);
            windowsversionsd.Add(win81ent64);
            windowsversionsd.Add(win8rt64);
            windowsversionsd.Add(win8core64);
            windowsversionsd.Add(win8pro64);
            windowsversionsd.Add(win8ent64);
            windowsversionsd.Add(win7start64);
            windowsversionsd.Add(win7hb64);
            windowsversionsd.Add(win7hp64);
            windowsversionsd.Add(win7pro64);
            windowsversionsd.Add(win7ent64);
            windowsversionsd.Add(win7ult64);
            windowsversionsd.Add(winvistastart64);
            windowsversionsd.Add(winvistahb64);
            windowsversionsd.Add(winvistahp64);
            windowsversionsd.Add(winvistab64);
            windowsversionsd.Add(winvistaent64);
            windowsversionsd.Add(winvistault64);
            windowsversionsd.Add(win81core86);
            windowsversionsd.Add(win81pro86);
            windowsversionsd.Add(win81ent86);
            windowsversionsd.Add(win8rt86);
            windowsversionsd.Add(win8core86);
            windowsversionsd.Add(win8pro86);
            windowsversionsd.Add(win8ent86);
            windowsversionsd.Add(win7start86);
            windowsversionsd.Add(win7hb86);
            windowsversionsd.Add(win7hp86);
            windowsversionsd.Add(win7pro86);
            windowsversionsd.Add(win7ent86);
            windowsversionsd.Add(win7ult86);
            windowsversionsd.Add(winvistastart86);
            windowsversionsd.Add(winvistahb86);
            windowsversionsd.Add(winvistahp86);
            windowsversionsd.Add(winvistab86);
            windowsversionsd.Add(winvistaent86);
            windowsversionsd.Add(winvistault86);
            foreach (string file in windowsversionsd)
            {
                if (File.Exists(imagefilepath + "\\"+ file))
                {
                    windowsimagefileexisits.Add(file);
                }
            }
            foreach (string file in windowsimagefileexisits)
            {
                if (file.Contains(win81core64v))
                {
                    wincore8164.Enabled = true;
                }
                if (file.Contains(win81pro64))
                {
                    winpro8164.Enabled = true;
                }
                if (file.Contains(win81ent64))
                {
                    winent8164.Enabled = true;
                }
                if (file.Contains(win8rt64))
                {
                    winrt864.Enabled = true;
                }
                if (file.Contains(win8core64))
                {
                    wincore864.Enabled = true;
                }
                if (file.Contains(win8pro64))
                {
                    winpro864.Enabled = true;
                }
                if (file.Contains(win8ent64))
                {
                    winent864.Enabled = true;
                }
                if (file.Contains(win7start64))
                {
                    winstart764.Enabled = true;
                }
                if (file.Contains(win7hb64))
                {
                    winhomeb764.Enabled = true;
                }
                if (file.Contains(win7hp64))
                {
                    winhomep764.Enabled = true;
                }
                if (file.Contains(win7pro64))
                {
                    winpro764.Enabled = true;
                }
                if (file.Contains(win7ent64))
                {
                    wient764.Enabled = true;
                }
                if (file.Contains(win7ult64))
                {
                    winu764.Enabled = true;
                }
                if (file.Contains(winvistastart64))
                {
                    winstartv64.Enabled = true;
                }
                if (file.Contains(winvistahb64))
                {
                    winhomebv64.Enabled = true;
                }
                if (file.Contains(winvistahp64))
                {
                    winhomepv64.Enabled = true;
                }
                if (file.Contains(winvistab64))
                {
                    winbusv64.Enabled = true;
                }
                if (file.Contains(winvistaent64))
                {
                    winentv64.Enabled = true;
                }
                if (file.Contains(winvistault64))
                {
                    winuv64.Enabled = true;
                }
                if (file.Contains(win8rt86))
                {
                    winrt886.Enabled = true;
                }
                if (file.Contains(win81core86))
                {
                    wincore8186.Enabled = true;
                }
                if (file.Contains(win81pro86))
                {
                    winpro8186.Enabled = true;
                }
                if (file.Contains(win81ent86))
                {
                    winent8186.Enabled = true;
                }
                if (file.Contains(win8rt86))
                {
                    winpro886.Enabled = true;
                }
                if (file.Contains(win8core86))
                {
                    wincore886.Enabled = true;
                }
                if (file.Contains(win8pro86))
                {
                    winpro886.Enabled = true;
                }
                if (file.Contains(win8ent86))
                {
                    winent886.Enabled = true;
                }
                if (file.Contains(win7start86))
                {
                    winstart786.Enabled = true;
                }
                if (file.Contains(win7hb86))
                {
                    winhomeb786.Enabled = true;
                }
                if (file.Contains(win7hp86))
                {
                    winhomp786.Enabled = true;
                }
                if (file.Contains(win7pro86))
                {
                    winpro786.Enabled = true;
                }
                if (file.Contains(win7ent86))
                {
                    winent786.Enabled = true;
                }
                if (file.Contains(win7ult86))
                {
                    winu786.Enabled = true;
                }
                if (file.Contains(winvistastart86))
                {
                    winstartv86.Enabled = true;
                }
                if (file.Contains(winvistahb86))
                {
                    winhomebv86.Enabled = true;
                }
                if (file.Contains(winvistahp86))
                {
                    winhomepv86.Enabled = true;
                }
                if (file.Contains(winvistab86))
                {
                    winbusv86.Enabled = true;
                }
                if (file.Contains(winvistaent86))
                {
                    winentv86.Enabled = true;
                }
                if (file.Contains(winvistault86))
                {
                    winuv86.Enabled = true;
                }
            }

        }

        private void installupdate()
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            string url = "HTTP://www.example.com:4029/AutoDeploy_" + nextappVersion.Trim() + ".exe";
            if (winPEAutoOpenToolStripMenuItem.Checked == true)
            {
                webClient.DownloadFileAsync(new Uri(url), xDrive + "AutoDeploy_" + nextappVersion.Trim() + "_auto.exe");

            }
            else
            {
                webClient.DownloadFileAsync(new Uri(url), xDrive + "AutoDeploy_" + nextappVersion.Trim() + ".exe");
            }
        }
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            string ppath = "";
            if (oepnAfterDownloadToolStripMenuItem.Checked == true)
            {
                if (winPEAutoOpenToolStripMenuItem.Checked == true)
                    {
                        ppath = xDrive + "AutoDeploy_" + nextappVersion.Trim() + "_auto" + ".exe";
                    } 
                else 
                    {
                        ppath = xDrive + "AutoDeploy_" + nextappVersion.Trim() + ".exe";
                    }
                this.Hide();
                System.Diagnostics.Process.Start(ppath).WaitForExit();
                Environment.Exit(0);  

            }
        }

        private void checkonlineforwims()
        {
            int f = progressBar1.Maximum;
            int s = progressBar1.Step;
            int v = progressBar1.Value;
            int wvsl = windowsversionsd.Count();
            progressBar1.Maximum = wvsl;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            downloadwim = true;
            foreach (string file in windowsversionsd)
            {
                progressBar1.Value++;
                Boolean isUpdate = true;
                string url = "HTTP://www.example.com:4029/DeploymentImages/" + file;
                HttpWebRequest request = WebRequest.Create(url.Trim()) as HttpWebRequest;
                request.Method = "HEAD";
                HttpWebResponse response = null;
                try
                {
                    response = request.GetResponse() as HttpWebResponse;
                }
                catch (WebException ex)
                {
                    isUpdate = false;
                }
                finally
                {
                    if (response != null)
                    {
                        response.Close();
                    }
                }
                if (isUpdate == true)
                {
                    avaliblewimfordownload.Add(file);
                }
            }

            //avaliblewimfordownload
            foreach (string file in avaliblewimfordownload)
            {
                Boolean oktodownload = true;
                foreach (string l in windowsimagefileexisits) 
                    {
                        if (file.Contains(l)) 
                        {
                            oktodownload = false;
                        }
                   }
                if (oktodownload == true)
                {

                    if (file.Contains(win81core64v))
                    {
                        wincore8164.Enabled = true;
                        wincore8164.Text = "Download Windows 8.1 Core x64";
                    }
                    if (file.Contains(win81pro64))
                    {
                        winpro8164.Enabled = true;
                        winpro8164.Text = "Download Windows 8.1 Pro x64";
                    }
                    if (file.Contains(win81ent64))
                    {
                        winent8164.Enabled = true;
                        winent8164.Text = "Download Windows 8.1 Enterprise x64";
                    }
                    if (file.Contains(win8rt64))
                    {
                        winrt864.Enabled = true;
                        winrt864.Text = "Download Windows 8 RT x64";
                    }
                    if (file.Contains(win8core64))
                    {
                        wincore864.Enabled = true;
                        wincore864.Text = "Download Windows 8 Core x64";
                    }
                    if (file.Contains(win8pro64))
                    {
                        winpro864.Enabled = true;
                        winpro864.Text = "Download Windows 8 Pro x64";
                    }
                    if (file.Contains(win8ent64))
                    {
                        winent864.Enabled = true;
                        winent864.Text = "Download Windows 8 Enterprise x64";
                    }
                    if (file.Contains(win7start64))
                    {
                        winstart764.Enabled = true;
                        winstart764.Text = "Download Windows 7 Starter x64";
                    }
                    if (file.Contains(win7hb64))
                    {
                        winhomeb764.Enabled = true;
                        winhomeb764.Text = "Download Windows 7 Home x64";
                    }
                    if (file.Contains(win7hp64))
                    {
                        winhomep764.Enabled = true;
                        winhomep764.Text = "Download Windows 7 Home Premium x64";
                    }
                    if (file.Contains(win7pro64))
                    {
                        winpro764.Enabled = true;
                        winpro764.Text = "Download Windows 7 Pro x64";
                    }
                    if (file.Contains(win7ent64))
                    {
                        wient764.Enabled = true;
                        wient764.Text = "Download Windows 7 Enterprize x64";
                    }
                    if (file.Contains(win7ult64))
                    {
                        winu764.Enabled = true;
                        winu764.Text = "Download Windows 7 Ultimate x64";
                    }
                    if (file.Contains(winvistastart64))
                    {
                        winstartv64.Enabled = true;
                        winstartv64.Text = "Download Windows Vista Starter x64";
                    }
                    if (file.Contains(winvistahb64))
                    {
                        winhomebv64.Enabled = true;
                        winhomebv64.Text = "Download Windows Vista Home Basic x64";
                    }
                    if (file.Contains(winvistahp64))
                    {
                        winhomepv64.Enabled = true;
                        winhomepv64.Text = "Download Windows Vista Home Premium x64";
                    }
                    if (file.Contains(winvistab64))
                    {
                        winbusv64.Enabled = true;
                        winbusv64.Text = "Download Windows Vista Business x64";
                    }
                    if (file.Contains(winvistaent64))
                    {
                        winentv64.Enabled = true;
                        winentv64.Text = "Download Windows Vista Enterprise x64";
                    }
                    if (file.Contains(winvistault64))
                    {
                        winuv64.Enabled = true;
                        winuv64.Text = "Download Windows Vista Ultimate x64";
                    }
                    if (file.Contains(win8rt86))
                    {
                        winrt886.Enabled = true;
                        winrt886.Text = "Download Windows 8 RT x86";
                    }
                    if (file.Contains(win81core86))
                    {
                        wincore8186.Enabled = true;
                        wincore8186.Text = "Download Windows 8.1 Core x86";
                    }
                    if (file.Contains(win81pro86))
                    {
                        winpro8186.Enabled = true;
                        winpro8186.Text = "Download Windows 8.1 Pro";
                    }
                    if (file.Contains(win81ent86))
                    {
                        winent8186.Enabled = true;
                        winent8186.Text = "Download Windows 8.1 Enterprise x64";
                    }
                    if (file.Contains(win8rt86))
                    {
                        winpro886.Enabled = true;
                        winpro886.Text = "Download Windows 8 Pro x86";
                    }
                    if (file.Contains(win8core86))
                    {
                        wincore886.Enabled = true;
                        wincore886.Text = "Download Windows 8 Core x86";
                    }
                    if (file.Contains(win8pro86))
                    {
                        winpro886.Enabled = true;
                        winpro886.Text = "Download Windows 8 Pro x86";
                    }
                    if (file.Contains(win8ent86))
                    {
                        winent886.Enabled = true;
                        winent886.Text = "Download Windows 8 Enterprise x86";
                    }
                    if (file.Contains(win7start86))
                    {
                        winstart786.Enabled = true;
                        winstart786.Text = "Download Windows 7 Starter x86";
                    }
                    if (file.Contains(win7hb86))
                    {
                        winhomeb786.Enabled = true;
                        winhomeb786.Text = "Download Windows 7 Home Basic x86";
                    }
                    if (file.Contains(win7hp86))
                    {
                        winhomp786.Enabled = true;
                        winhomp786.Text = "Download Windows 7 Home Premium x86";
                    }
                    if (file.Contains(win7pro86))
                    {
                        winpro786.Enabled = true;
                        winpro786.Text = "Download Windows 7 Pro x86";
                    }
                    if (file.Contains(win7ent86))
                    {
                        winent786.Enabled = true;
                        winent786.Text = "Download Windows 7 Enterprise x86";
                    }
                    if (file.Contains(win7ult86))
                    {
                        winu786.Enabled = true;
                        winu786.Text = "Download Windows 7 Ultimate x86";
                    }
                    if (file.Contains(winvistastart86))
                    {
                        winstartv86.Enabled = true;
                        winstartv86.Text = "Download Windows Vista Starter x86";
                    }
                    if (file.Contains(winvistahb86))
                    {
                        winhomebv86.Enabled = true;
                        winhomebv86.Text = "Download Windows Vista Home Basic x86";
                    }
                    if (file.Contains(winvistahp86))
                    {
                        winhomepv86.Enabled = true;
                        winhomepv86.Text = "Download Windows Vista Home Premium";
                    }
                    if (file.Contains(winvistab86))
                    {
                        winbusv86.Enabled = true;
                        winbusv86.Text = "Download Windows Vista Business x86";
                    }
                    if (file.Contains(winvistaent86))
                    {
                        winentv86.Enabled = true;
                        winentv86.Text = "Download Windows Vista Enterprise x86";
                    }
                    if (file.Contains(winvistault86))
                    {
                        winuv86.Enabled = true;
                        winuv86.Text = "Download Windows Vista Ultimate x86";
                    }
                }
            }

            progressBar1.Maximum = f;
            progressBar1.Step = s;
            progressBar1.Value = v;
        }
        /* origianl downloadwimfile(string x)
        private void downloadwimfile(string x) // filebeingdownloaded = x
        {
            //if (deployafterdownload.Checked == true)
            filebeingdownloaded = x;
            MessageBox.Show("Now Downloading " + x);
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed2);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged2);
            string url = "HTTP://www.example.com:4029/DeploymentImages/" + x;
            webClient.DownloadFileAsync(new Uri(url), xDrive + "DeploymentImages\\" + x);
        }
        private void ProgressChanged2(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        private void Completed2(object sender, AsyncCompletedEventArgs e)
        {
            //string ppath = xDrive + "\\DeploymentImages\\" + filebeingdownloaded;
            if (deployafterdownload.Checked == true)
            {
                WipeInstall(filebeingdownloaded);
            }
        }
        */
        public string returnfilebeingdownloaded()
        {
            return filebeingdownloaded;
        }
        private void checkupdates()
        {
            Boolean isUpdate = true;
            // create the request
            string url = "HTTP://www.example.com:4029/AutoDeploy_" + nextappVersion.Trim() + ".exe";
            HttpWebRequest request = WebRequest.Create(url.Trim()) as HttpWebRequest;

            // instruct the server to return headers only
            request.Method = "HEAD";
            HttpWebResponse response = null;
            try
            {
            // make the connection
            response = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException ex)
            {
                isUpdate = false;
            }
            finally
            {
                // Don't forget to close your response.
                if (response != null)
                {
                    response.Close();
                }
            }
            if (isUpdate == true)
            {
                updateAvailableToolStripMenuItem.Visible = true;
                updateAvailableToolStripMenuItem.Enabled = true;
            }
            DirectoryInfo folder = new DirectoryInfo(@xDrive);
            if (File.Exists(deploymentfolderroot + "AutoDeploy*.exe"))
            {
                FileInfo[] files = folder.GetFiles("AutoDeploy*.exe");
                int value;
                int tmp = 0;
                Boolean autod = false;
                foreach (FileInfo file in files)
                {
                    string r = "";
                    if (file.Name.Contains("auto"))
                    {
                        r = file.Name.Replace("AutoDeploy_", "").Replace(".exe", "").Replace("_auto", "").Trim();
                        autod = true;
                    }
                    else
                    {
                        r = file.Name.Replace("AutoDeploy_", "").Replace(".exe", "").Trim();
                    }
                    int.TryParse(r, out value);
                    if (value > appVersion)
                    {
                            tmp = value;
                    }
                }
                if (tmp > appVersion)
                {
                    string ppath = "";
                    if (autod == true)
                    {
                        ppath = xDrive + "AutoDeploy_" + tmp +"_auto" + ".exe";
                        this.Hide();
                        System.Diagnostics.Process.Start(ppath).WaitForExit();
                        Environment.Exit(0);    
                    }
                    else
                    {
                        ppath = xDrive + "AutoDeploy_" + tmp + ".exe";
                    DialogResult result2 = MessageBox.Show("Update found on " + xDrive + "\n Would you like to open " + ppath + " ?",
                        "AutoDeploy",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);
                    if (result2 == DialogResult.Yes)
                    {
                        this.Hide();
                        System.Diagnostics.Process.Start(ppath).WaitForExit();
                        Environment.Exit(0);                    
                    }
                    if (result2 == DialogResult.No)
                    {

                    }
                    if (result2 == DialogResult.Cancel)
                    {
                        Environment.Exit(0);
                    }
                    }
                }
            }
        }

        private void filterselecteddisk()
        {
            if(UseDiskNumChkBx.Checked == true)
            {
            String y = comboBox1.SelectedItem.ToString().Trim();
            string[] words = y.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
            foreach (string i in words)
            {
                if (i.Contains("DISK"))
                {
                    selecteddisknum = i.Replace("DISK", "").Trim();
                }
            }
            }
            else if (UseDriveLetterChkBx.Checked == true)
            {
                //String y = comboBox3.SelectedItem.ToString().Trim();
                foreach (var drive in DriveInfo.GetDrives())
                {
                    if (comboBox3.SelectedItem.ToString().Contains(drive.Name))
                    {
                        userselectedvolume = drive.Name;
                        //cho
                    }
                }
            }
        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            WipeInstall(win7ult64);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WipeInstall(win7ult86);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            WipeInstall(win8pro64);
        }

        private void scanForFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(mountdriveas);
        }

        private void showImagePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(imagefilepath);
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            WipeInstall(winvistab64); //TODO the same?
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex != -1)
            {
                createpartChkbx.Checked = false;
                createpartChkbx.Enabled = false;
                UseDiskNumChkBx.Checked = false;
                UseDriveLetterChkBx.Checked = true;
                DeletePartitionsChkBx.Checked = false;
                DeletePartitionsChkBx.Enabled = false;
                //WriteMBRChkBox.Checked = false;
                //WriteMBRChkBox.Enabled = false;
                comboBox1.SelectedIndex = -1;

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                createpartChkbx.Checked = true;
                createpartChkbx.Enabled = true;
                UseDriveLetterChkBx.Checked = false;
                UseDiskNumChkBx.Checked = true;
                DeletePartitionsChkBx.Checked = true;
                DeletePartitionsChkBx.Enabled = true;
                //WriteMBRChkBox.Checked = true;
                //WriteMBRChkBox.Checked = true;
                comboBox3.SelectedIndex = -1;
                filterselecteddisk();
            }
 
        }

        private void UseDriveLetterChkBx_CheckedChanged(object sender, EventArgs e)
        {
            if (UseDriveLetterChkBx.Checked == true){
                UseDiskNumChkBx.Checked = false;
                if (comboBox3.SelectedIndex == -1)
                {
                    comboBox3.SelectedIndex = 0;
                }
                comboBox1.SelectedIndex = -1;


                //UseDriveLetterChkBx.Checked = true;
                DeletePartitionsChkBx.Checked = false;
                DeletePartitionsChkBx.Enabled = false;
                WriteMBRChkBox.Checked = false;
                //WriteMBRChkBox.Enabled = false;
            }
            else if (UseDriveLetterChkBx.Checked == false)
            {
                UseDiskNumChkBx.Checked = true;
            }
        }

        private void UseDiskNumChkBx_CheckedChanged(object sender, EventArgs e)
        {
            if (UseDiskNumChkBx.Checked == true)
            {
                if (comboBox1.SelectedIndex == -1)
                {
                    comboBox1.SelectedIndex = 0;
                }
                comboBox3.SelectedIndex = -1;
                //UseDiskNumChkBx.Checked = true;
                UseDriveLetterChkBx.Checked = false;
                DeletePartitionsChkBx.Checked = true;
                DeletePartitionsChkBx.Enabled = true;
                WriteMBRChkBox.Checked = true;
                WriteMBRChkBox.Enabled = true;
            }
            else if (UseDiskNumChkBx.Checked == false)
            {
                UseDriveLetterChkBx.Checked = true;
            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            WipeInstall(win7start64);
        }

        private void rescanDeploymentFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scanforfolder();
        }

        private void reMapas_SelectedIndexChanged(object sender, EventArgs e)
        {
            mountdriveas = reMapas2.SelectedItem.ToString();
        }

        private void showMountdriveasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(xDrive);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            WipeInstall(winvistahp64);
        }

        private void winent8164_Click(object sender, EventArgs e)
        {

        }

        private void winent8164_Click_1(object sender, EventArgs e)
        {
            WipeInstall(win81ent64);
        }

        private void winent8186_Click(object sender, EventArgs e)
        {
            WipeInstall(win81ent86);
        }

        private void winpro8186_Click(object sender, EventArgs e)
        {
            WipeInstall(win81pro86);
        }

        private void wincore8186_Click(object sender, EventArgs e)
        {
            WipeInstall(win81core86);
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void winstartv86_Click(object sender, EventArgs e)
        {
            WipeInstall(winvistastart86);
        }

        private void winstartv64_Click(object sender, EventArgs e)
        {
            WipeInstall(winvistastart64);
        }

        private void winhomebv86_Click(object sender, EventArgs e)
        {
            WipeInstall(winvistahb86);
        }

        private void winhomebv64_Click(object sender, EventArgs e)
        {
            WipeInstall(winvistahb64);
        }

        private void winuv86_Click(object sender, EventArgs e)
        {
            WipeInstall(winvistault86);
        }

        private void winuv64_Click(object sender, EventArgs e)
        {
            WipeInstall(winvistault64);
        }

        private void winentv86_Click(object sender, EventArgs e)
        {
            WipeInstall(winvistaent86);
        }

        private void winentv64_Click(object sender, EventArgs e)
        {
            WipeInstall(winvistaent64);
        }

        private void winhomepv86_Click(object sender, EventArgs e)
        {
            WipeInstall(winvistahp86);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void winstart786_Click(object sender, EventArgs e)
        {
            WipeInstall(win7start86);
        }

        private void winhomeb786_Click(object sender, EventArgs e)
        {
            WipeInstall(win7hb86);
        }

        private void winhomeb764_Click(object sender, EventArgs e)
        {
            WipeInstall(win7hb64);
        }

        private void winent786_Click(object sender, EventArgs e)
        {
            WipeInstall(win7ent86);
        }

        private void wient764_Click(object sender, EventArgs e)
        {
            WipeInstall(win7ent64);
        }

        private void winhomp786_Click(object sender, EventArgs e)
        {
            WipeInstall(win7hp86);
        }

        private void winhomep764_Click(object sender, EventArgs e)
        {
            WipeInstall(win7hp64);
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void winent864_Click(object sender, EventArgs e)
        {
            WipeInstall(win8ent64);
        }

        private void wincore864_Click(object sender, EventArgs e)
        {
            WipeInstall(win8core86);
        }

        private void winrt886_Click(object sender, EventArgs e)
        {
            WipeInstall(win8rt86);
        }

        private void winrt864_Click(object sender, EventArgs e)
        {
            WipeInstall(win8rt64);
        }

        private void winent886_Click(object sender, EventArgs e)
        {
            WipeInstall(win8ent86);
        }

        private void wincore886_Click(object sender, EventArgs e)
        {
            WipeInstall(win8rt86);
        }

        private void testEnableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            winu786.Enabled = true;
        }

        private void winpro764_Click(object sender, EventArgs e)
        {
            WipeInstall(win7pro64);
        }

        private void winpro786_Click(object sender, EventArgs e)
        {
            WipeInstall(win7pro86);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error:\n" + ex.Message.ToString());
                }
            }
        }

        private void URLupdatebutton_Click(object sender, EventArgs e)
        {
        }

        private void winPEAutoOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (winPEAutoOpenToolStripMenuItem.Checked == true)
            {
                winPEAutoOpenToolStripMenuItem.Checked = false;
            } else {
                winPEAutoOpenToolStripMenuItem.Checked = true;
            }
        }

        private void oepnAfterDownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (oepnAfterDownloadToolStripMenuItem.Checked == true)
            {
                oepnAfterDownloadToolStripMenuItem.Checked = false;
            }
            else
            {
                oepnAfterDownloadToolStripMenuItem.Checked = true;
            }
        }

        private void startDownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            installupdate();
        }

        private void checkForAvalibleImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkonlineforwims();

        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (deployafterdownload.Checked == true)
            {
                deployafterdownload.Checked = false;
            }
            else
            {
                deployafterdownload.Checked = true;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "WIM Files|*.wim";
            openFileDialog1.Title = "Select a Windows Image";


            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (String file in openFileDialog1.FileNames) 
                {
                    customfiletxtBx.Text = file;
                    customwim = file;
                }
            }
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            WipeInstall(customwim);
        }
        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            (System.Windows.Forms.Application.OpenForms["loadingbarForm"] as loadingbarForm).Close();
        }

        private void createBootUSBStickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listdownloads f2 = new listdownloads();
            f2.Show();
            f2.downloadwimfile("winpe.wim", deploymentfolderroot, false, true, deployafterdownload.Checked, deploymentfolderexists);

        }

        private void downloadISOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listdownloads f2 = new listdownloads();
            f2.Show();
            f2.downloadwimfile("winpe_x64.iso", deploymentfolderroot, false, true, deployafterdownload.Checked, deploymentfolderexists);

        }

        private void deployWinPEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WipeInstall("WinPE_x64.wim");
        }
        #region this is a test if you want to use it or not
        /*
        private void Test()
        {
            #region safemode
            BootMode mode = SystemInformation.BootMode;
            if (mode != BootMode.Normal)
            {
               
                DialogResult ask = MessageBox.Show("Would you like to enable MSI installer?", "SafeMode Detected", MessageBoxButtons.YesNo);
                if (ask == DialogResult.Yes)
                {
                    string key;
                    key = @"""HKLM\SYSTEM\CurrentControlSet\Control\SafeBoot\Network\MSIServer"" /VE /T REG_SZ /F /D " + @"""Service""";
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.EnableRaisingEvents = false;
                    proc.StartInfo.FileName = "cmd.exe";
                    proc.StartInfo.Arguments = "/C REG ADD " + key;
                    proc.Start();
                    proc.WaitForExit();

                    System.Diagnostics.Process start = new System.Diagnostics.Process();
                    start.EnableRaisingEvents = false;
                    start.StartInfo.FileName = "cmd.exe";
                    start.StartInfo.Arguments = "/C net start msiserver";
                    start.Start();
                    start.WaitForExit();

                }
            }
            #endregion

        } */
        #endregion
    }
}
