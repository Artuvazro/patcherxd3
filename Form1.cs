using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;



namespace Sf2español
{

    public partial class form1 : Form
    {
        public form1()
        {
            InitializeComponent();
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.form1_MouseDown);

        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        //Algunas variables que necesitamos...
        //Configurables:
        public string defnamespace = "shiningforceesp"; //Namespace default para los recursos.
        public string patch = "sf2espv20"; //Nombre del parche sin extensión
        public string romtype = "Archivos binarios (.bin)|*bin"; //Extensión de ROM
        public string label = "[español]"; //Texto que se añade al nombre de la ROM parcheada.

        
        //No configurables:
        public string fileName;
        public string fileNameS;
        public string directoryPath;

        private void button2_Click(object sender, EventArgs e)
        {
            if (progressBar1.Value >= 1)
            { 
                progressBar1.Value = 0;
                progressBar1.Visible = false;
            }
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = romtype;
            dlg.FilterIndex = 1;
            dlg.ShowDialog();
            fileName = dlg.FileName;
            fileNameS = dlg.SafeFileName;
            if ((fileName.Length >= 1) && (FileCheck.CheckFile.Checker(fileName) == true))

                {
                    directoryPath = Path.GetDirectoryName(dlg.FileName);
                    textBox1.AppendText(fileName);
                    button1.Enabled = true;
                }
            else 
            {
                textBox1.Clear();
                button1.Enabled = false;
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            button1.Enabled = false;

            XdeltaPatch p = new XdeltaPatch();
            p.Patch(fileName, fileNameS, directoryPath, defnamespace, patch, label);

            for (int i = 0; progressBar1.Value <= 99; i++)
                {
                    progressBar1.Value += 20;
                }
                MessageBox.Show("¡Hecho!");
                
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            ProcessStartInfo sInfo = new ProcessStartInfo("http://nekutranslations.es");
            Process.Start(sInfo);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void form1_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void toolTip1_Popup_1(object sender, PopupEventArgs e)
        {

        }

    }

}
