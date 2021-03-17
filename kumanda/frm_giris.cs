using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Security.Cryptography;
using Microsoft.Win32;




namespace kumanda
{
    public partial class frm_giris : Form
    {
        public frm_giris()
        {
            InitializeComponent();
        }
        public string xx, yy, xxyy, yyxx, xxx, xxxx;
        public int count, oku, okunan;
        public static string xHash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }
        public static string xxHash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {

                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }
        public static string xxxHash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {

                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }
        public static string xxxxHash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {

                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }
        public static String cpucont()
        {
            String cpucont = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * FROM Win32_Processor");
            ManagementObjectCollection mObject = searcher.Get();

            foreach (ManagementObject obj in mObject)
            {
                cpucont = obj["ProcessorId"].ToString();
            }
            return cpucont;
        }
        private string maccont()
        {
            ManagementClass manager = new ManagementClass("Win32_NetworkAdapterConfiguration");
            foreach (ManagementObject obj in manager.GetInstances())
            {
                if ((bool)obj["IPEnabled"])
                {
                    return obj["MacAddress"].ToString();
                }
            }

            return String.Empty;
        }
        public void keyyaz()
        {
            Properties.Settings.Default.x = maccont().ToString();
            Properties.Settings.Default.y = cpucont().ToString();
            Properties.Settings.Default.Save();
            xx = Properties.Settings.Default.x;
            yy = Properties.Settings.Default.y;
            xxyy = (xx + yy).Trim();
            yyxx = xHash(xxyy);
            xxx = xxHash(yyxx);
            yyxx = xxxHash(xxx);
            xxxx = xxxxHash(yyxx);
            Properties.Settings.Default.z = xxyy.ToString();
            Properties.Settings.Default.yyxxx = xxx.ToString();
            Properties.Settings.Default.Save();
        }
        public void keykontrol()
        {
            if (Properties.Settings.Default.yyxxx != xxx.ToString())
            {
                keyyaz();
            }
        }
        public void fullkontrol()
        {
            if (textBox3.Text == "LİSANSLI FULL SÜRÜM !!")
            {
                button_demo.Visible = false;
               // MessageBox.Show("1");
            }
            else
            {
                button_demo.Visible = true;
               // MessageBox.Show("2");
            }

        }
        public static bool CheckProgram(string ProgramName)
        {
            bool status = false;
            string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(subkey.GetValue("DisplayName"))))
                        {
                            if (Convert.ToString(subkey.GetValue("DisplayName")).Contains(ProgramName))
                                status = true;
                        }
                    }
                }
            }
            return status;
        }
        //FORM VE BUTONLAR
        private void frm_giris_Load(object sender, EventArgs e)
        {
            try
            {
                keyyaz();
                keykontrol();
                string programName = "Microsoft Access database engine 2016";
                string programName1 = "Microsoft Access database engine 2010";
                //bool st = CheckProgram(programName);
                bool st = CheckProgram(programName);
                bool st1 = CheckProgram(programName1);
                //bool st1 = CheckProgram(programName1);
                var x = (st || st1);
                if (x)
                {
                    // MessageBox.Show(programName + " bilgisayarınızda kuruludur.");
                }
                else
                if (MessageBox.Show(programName + "\n" +programName1+"\n"+ "Bilgisayarınızda kurulu değildir. !!" + "\n" + "Microsoft ücretsiz indirme sayfasina yönlendirileceksiniz." + "\n" + "Kurulumdan sonra KUMANDA programını tekrar açınız.", "KUMANDA PROGRAMI BİLGİ !!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //System.Diagnostics.Process.Start("https://www.microsoft.com/en-us/download/details.aspx?id=54920");
                    System.Diagnostics.Process.Start("https://www.microsoft.com/en-us/download/details.aspx?id=13255");
                    Environment.Exit(0);
                }
                else { Environment.Exit(0); }

                if (Properties.Settings.Default.xyz == "HALİSA".ToString())
                {
                    //label1.Visible = false;
                    label2.Visible = false;
                    textBox1.Visible = false;
                    textBox3.Text = "LİSANSLI FULL SÜRÜM !!";
                    button1.Text = "PROGRAMI BAŞLAT";
                    button_demo.Visible = false;
                    textBox3.ForeColor = Color.Green;
                }
                else
                {
                    keyyaz();
                    button1.Text = "KEY KONTROL";
                    textBox3.Text = xxx.ToString().Trim();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "MESAJ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frm_giris_Shown(object sender, EventArgs e)
        {
            fullkontrol();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                keykontrol();
                if (Properties.Settings.Default.xyz == "HALİSA".ToString())
                {
                    GC.Collect();
                    System.GC.SuppressFinalize(this);
                    this.Close();
                }
                else
                {
                    if (textBox1.Text.Length > 0)
                    {
                        if (textBox1.Text == xxxx)
                        {
                            Properties.Settings.Default.xyz = "HALİSA".ToString();
                            Properties.Settings.Default.Save();
                            label1.Visible = false;
                            label2.Visible = false;
                            textBox1.Visible = false;
                            textBox3.Text = "LİSANSLI FUL SÜRÜM !!";
                            textBox3.ForeColor = Color.Green;
                            button1.Text = "PROGRAMI BAŞLAT";
                            button_demo.Visible = false;
                            MessageBox.Show("KEY DOĞRU !!", "BİLGİLENDİRME !!");
                        }
                        else
                        {
                            Properties.Settings.Default.xyz = "".ToString();
                            Properties.Settings.Default.Save();
                            textBox1.ForeColor = Color.Red;
                            MessageBox.Show("KEYİNİZ YANLIŞ !!", "BİLGİLENDİRME !!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("LÜTFEN SERİAL GİRİNİZ. !!", "BİLGİLENDİRME !!");
                        textBox1.Focus();
                    }

                }

            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button2_Click(object sender, EventArgs e) //exıt butonu
        {
            Environment.Exit(0);
        }
        private void button_demo_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.count == 0)
            {
                MessageBox.Show("Demo Kullanım Hakkınız Dolmuştur." + "\n" + "( hedefuydu@gmail.com )" + "\n" + "İletişim Adresimiz.", "BİLGİLENDİRME !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Environment.Exit(0);
            }
            else
            {
                if(MessageBox.Show("Demo Sürüm Kullanacaksınız." + "\n" + "Kalan Giriş Hakkınız : " + Properties.Settings.Default.count, "BİLGİLENDİRME !!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)==DialogResult.OK)
                {
                    Properties.Settings.Default.count--;
                    Properties.Settings.Default.Save();
                    GC.Collect();
                    System.GC.SuppressFinalize(this);
                    this.Close();
                }
                else
                {
                    Environment.Exit(0);
                }
                
                
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("outlook.exe");
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "OUTLOOK KURULU DEĞİL !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.xyz = "".ToString();
            Properties.Settings.Default.Save();
        }
        private void label3_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.count = 15;
            Properties.Settings.Default.Save();
        }
    }
}