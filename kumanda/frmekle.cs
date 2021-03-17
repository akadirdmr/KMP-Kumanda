/*
02.06.2020 v2.1  kayıt düzeltme yapılıyor. 
*/

using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO.Ports;
using System.ComponentModel;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Drawing.Imaging;
using System.Threading;


namespace kumanda
{
    public partial class frmekle : Form
    {
        public frmekle()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        hakkinda hakkimda;
        Bitmap image;
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\vtole.accdb;Jet OLEDB:Database Password=mehmetalbayrak");
        string base64Text; //eklenecek kod
        // DEĞİŞKEN TANIMLAMALARI------------------------------------------------
        public string vtvers = "VT4";
        public string prgvers = "V3.0";
        public string turutxt, zapptxt, swattxt, classtxt, ozttxt, sesartihex;
        public string seseksihex, prgartihex, prgeksihex, menuhex, powerhex, modeltxt, redhex, greenhex, bluehex, yellowhex;//kopyalama işlemi değişkenleri
        public string turutxty, zapptxty, swattxty, classtxty, ozttxty, sesartihexy; //yedekleme işlemi değişkenleri
        public string seseksihexy, prgartihexy, prgeksihexy, menuhexy, powerhexy, modeltxty;//yedekleme işlemi değişkenleri
        public string redhexy, greenhexy, bluehexy, yellowhexy, urly, resimy; //yedekleme işlemi değişkenleri
        public void muadillabel()
        {
            string x = toolStripComboBox_ara.Text.ToString();
            label14.ForeColor = Color.Green;
            label14.Text = "( " + x + " )" + " Uyumlu Kumanda Adeti :  " + tbloleBindingSource.Count.ToString();

        }
        public void adetlabel()
        {
            label14.ForeColor = Color.Red;
            label14.Text = "KAYIT ADEDİ :  " + tbloleBindingSource.Count.ToString();
            if (tbloleBindingSource.Count.ToString() == "0")
            {
                pictureBox1.Image = null;
            }
        }
        public void portx()
        {
            try
            {
                if (Properties.Settings.Default.count == 0)
                //if(Properties.Settings.Default.xyz == "HALİSA")
                {
                    Environment.Exit(0);
                }
                else
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void kilitle()
        {
            if (button_duzenle.Text == "DÜZENLE")
            {
                button_ekle.Enabled = true;
                button_gerial.Enabled = true;
                button_guncelle.Enabled = true;
                button_temizle.Enabled = false;
                button_duzenle.Text = "KİLİTLE";
                button_duzenle.ForeColor = Color.Red;
                panel1.Enabled = true;
                dataGridView1.Enabled = false;
                duzenle_ToolStripMenuItem.Text = "KİLİTLE";
                duzenle_ToolStripMenuItem.ForeColor = Color.Red;
                ekle_ToolStripMenuItem.Enabled = true;
                vazgec_ToolStripMenuItem.Enabled = true;
                toolStripMenuItem_sil.Enabled = true;
                kaydet_ToolStripMenuItem.Enabled = true;

            }
            else
            {
                button_ekle.Enabled = false;
                button_kaydet.Enabled = false;
                button_iptal.Enabled = false;
                button_gerial.Enabled = false;
                button_guncelle.Enabled = false;
                button_temizle.Enabled = true;
                dataGridView1.Enabled = true;
                button_duzenle.Text = "DÜZENLE";
                button_duzenle.ForeColor = Color.ForestGreen;

                duzenle_ToolStripMenuItem.Text = "DÜZENLE";
                duzenle_ToolStripMenuItem.ForeColor = Color.ForestGreen;
                panel1.Enabled = false;

                ekle_ToolStripMenuItem.Enabled = false;
                vazgec_ToolStripMenuItem.Enabled = false;
                toolStripMenuItem_sil.Enabled = false;
                kaydet_ToolStripMenuItem.Enabled = false;
            }
        }
        public void kopyala()
        {
            try
            {
                if (MessageBox.Show("BİLGİLER KOPYALANSINMI..??", "BİLGİLENDİRME..!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    sesartihex = textBox_sesarti.Text;
                    seseksihex = textBox_seseksi.Text;
                    prgartihex = textBox_prgarti.Text;
                    prgeksihex = textBox_prgeksi.Text;
                    menuhex = textBox_menu.Text;
                    powerhex = textBox_power.Text;
                    redhex = textBox_red.Text;
                    greenhex = textBox_yellow.Text;
                    yellowhex = textBox_blue.Text;
                    bluehex = textBox_green.Text;
                }
                else
                {

                }


            }
            catch
            {

            }


        }
        public void yapistir()
        {
            try
            {
                if (MessageBox.Show("KOPYALANAN BİLGİLER YAPIŞTIRILSINMI..??", "BİLGİLENDİRME..!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    textBox_sesarti.Text = sesartihex;
                    textBox_seseksi.Text = seseksihex;
                    textBox_prgarti.Text = prgartihex;
                    textBox_prgeksi.Text = prgeksihex;
                    textBox_menu.Text = menuhex;
                    textBox_power.Text = powerhex;
                    textBox_red.Text = redhex;
                    textBox_yellow.Text = greenhex;
                    textBox_green.Text = bluehex;
                    textBox_blue.Text = yellowhex;
                }
                else
                {

                }

            }
            catch
            {

            }

        }
        public void verial()
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    Filter = "KMP Dosyası |*.kmp",
                };
                if (file.ShowDialog() == DialogResult.OK)
                {
                    string DosyaYolu = file.FileName;
                    string DosyaAdi = file.SafeFileName;
                    string[] okunan = System.IO.File.ReadAllLines(DosyaYolu);
                    int satirresim = Array.IndexOf(okunan, "resim:");
                    int satirmodel = Array.IndexOf(okunan, "model:");
                    int topsatir = okunan.Count() - 1;

                    comboBox_turu.Text = okunan[0];
                    textBox_swat.Text = okunan[1];
                    textBox_class.Text = okunan[2];
                    textBox_zapp.Text = okunan[3];
                    textBox_ozt.Text = okunan[4];
                    textBox_sesarti.Text = okunan[5];
                    textBox_seseksi.Text = okunan[6];
                    textBox_prgarti.Text = okunan[7];
                    textBox_prgeksi.Text = okunan[8];
                    textBox_menu.Text = okunan[9];
                    textBox_power.Text = okunan[10];
                    textBox_red.Text = okunan[11];
                    textBox_green.Text = okunan[12];
                    textBox_yellow.Text = okunan[13];
                    textBox_blue.Text = okunan[14];
                    textBox_model.Text = okunan[satirmodel + 1] + Environment.NewLine; ;
                    int a, b;
                    a = satirmodel + 2; ///resim yazısının satırı
                    b = satirresim - 1; // model yazısının satır numarası
                    for (int i = a; i <= b; i++)
                    {
                        textBox_model.Text = textBox_model.Text + okunan[i] + Environment.NewLine;
                    }
                    base64Text = okunan[topsatir];
                    byte[] imageBytes = Convert.FromBase64String(base64Text);
                    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                    ms.Write(imageBytes, 0, imageBytes.Length);
                    Image image = Image.FromStream(ms, true);
                    pictureBox1.Image = image;
                    textBox_model.Focus();
                    Image x = image;
                    int xx = x.Width;
                    textBox_img.Text = xx.ToString();

                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "TEK VERİ AL HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public void veriver()
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                //byte[] resim = ms.GetBuffer();
                //string result = System.Text.Encoding.ASCII.GetString(resim);

                byte[] imageArray = ms.GetBuffer();
                base64Text = Convert.ToBase64String(imageArray); //base64Text must be global but I'll use  richtext
                var x = base64Text;

                string[] ver = { comboBox_turu.Text, textBox_swat.Text, textBox_class.Text, textBox_zapp.Text, textBox_ozt.Text, textBox_sesarti.Text, textBox_seseksi.Text, textBox_prgarti.Text, textBox_prgeksi.Text, textBox_menu.Text, textBox_power.Text, textBox_red.Text, textBox_green.Text, textBox_yellow.Text, textBox_blue.Text, "model:", textBox_model.Text, "resim:", x };
                string dosya_adi = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + " KMP.kmp";
                string dosya_yolu = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string hedef_yol = System.IO.Path.Combine(dosya_yolu, dosya_adi);
                if (System.IO.File.Exists(hedef_yol))
                {
                    //MessageBox.Show("selam");
                    //File.Open(hedef_yol, FileMode.Open).Close();
                    //File.Create(Directory.GetCurrentDirectory() + "\\" + txtDosyaIsmi.Text + ".txt").Dispose();
                }
                else
                {
                    System.IO.File.Create(hedef_yol).Close();
                    System.IO.File.WriteAllLines(hedef_yol, ver);
                    //string[] okunan = System.IO.File.ReadAllLines(hedef_yol);
                    //textBox1.Text = okunan[0];
                    //textBox2.Text = okunan[1];
                    //textBox3.Text = okunan[2];
                    //textBox4.Text = okunan[3];

                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("D" + hata);
            }
        }
        public void geciciyedekle() //datagrid select son verileri yedekle
        {
            turutxty = comboBox_turu.Text;
            zapptxty = textBox_zapp.Text;
            swattxty = textBox_swat.Text;
            classtxty = textBox_class.Text;
            ozttxty = textBox_ozt.Text;
            sesartihexy = textBox_sesarti.Text;
            seseksihexy = textBox_seseksi.Text;
            prgartihexy = textBox_prgarti.Text;
            prgeksihexy = textBox_prgeksi.Text;
            menuhexy = textBox_menu.Text;
            powerhexy = textBox_power.Text;
            modeltxty = textBox_model.Text;
            redhexy = textBox_red.Text;
            greenhexy = textBox_green.Text;
            bluehexy = textBox_blue.Text;
            yellowhexy = textBox_yellow.Text;
            resimy = textBox_img.Text;
        }
        public void yedekgerial()  //yedeklenen veriyi geri al yapıştırma
        {
            comboBox_turu.Text = turutxty;
            textBox_zapp.Text = zapptxty;
            textBox_swat.Text = swattxty;
            textBox_class.Text = classtxty;
            textBox_ozt.Text = ozttxty;
            textBox_sesarti.Text = sesartihexy;
            textBox_seseksi.Text = seseksihexy;
            textBox_prgarti.Text = prgartihexy;
            textBox_prgeksi.Text = prgeksihexy;
            textBox_menu.Text = menuhexy;
            textBox_power.Text = powerhexy;
            textBox_model.Text = modeltxty;
            textBox_red.Text = redhexy;
            textBox_green.Text = greenhexy;
            textBox_blue.Text = bluehexy;
            textBox_yellow.Text = yellowhexy;

        }
        public void doldur()
        {
            this.tbloleTableAdapter.Fill(this.vtoleDataSet.tblole);
            tbloleBindingSource.DataSource = this.vtoleDataSet.tblole;
            //dataGridView1.DataSource = tbloleBindingSource;
        }
        public void otocom()
        {
            if (serialPort1.IsOpen == false)
            {
                string[] portlar = SerialPort.GetPortNames();
                var portsay = portlar.Count();
                if (portsay == 0)
                {
                    com_ToolStripMenuItem.Text = "DONGLE TAKIN.!!";
                    MessageBox.Show("Lütfen Dongle Takınız", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    foreach (string portAdi in portlar)
                    {
                        try
                        {
                            serialPort1.PortName = portAdi;
                            serialPort1.Open();
                            serialPort1.WriteLine("S");
                            string okunan = serialPort1.ReadLine();
                            if (okunan == "KMPON\r")
                            {
                                com_ToolStripMenuItem.Text = "DONGLE BAĞLANDI";
                                com_ToolStripMenuItem.Image = Properties.Resources.iconfinder_Green_ball_131927;
                                baglan_ToolStripMenuItem.Visible = false;
                                baglantikes_ToolStripMenuItem.Visible = true;
                                timer1.Start();
                                textBox_ara.Focus();
                            }
                            else
                            {

                            }
                        }
                        catch (Exception hata)
                        {
                            if (hata is IndexOutOfRangeException || hata is DivideByZeroException || hata is Exception)
                            {
                                serialPort1.Close();
                                timer1.Stop();
                                com_ToolStripMenuItem.Text = "DONGLE TAKIN.!!";
                                com_ToolStripMenuItem.Image = Properties.Resources.iconfinder_Red_ball_132008;

                            }

                        }
                    }

                }

            }
            else
            {

            }

        }
        public void ayarlar()
        {
            var yardim = Properties.Settings.Default.yardim;
            if (yardim != "true")
            {
                toolTip1.Active = false;
                toolStripComboBox_ayar.SelectedIndex = 1;
            }
            else
            {
                Properties.Settings.Default.yardim = "true";
                Properties.Settings.Default.Save();
                toolTip1.Active = true;
                toolStripComboBox_ayar.SelectedIndex = 0;
            }
        } //setting 
        public void txtsil()
        {
            comboBox_turu.Text = "";
            textBox_zapp.Text = "";
            textBox_class.Text = "";
            textBox_swat.Text = "";
            textBox_ozt.Text = "";
            textBox_sesarti.Text = "";
            textBox_seseksi.Text = "";
            textBox_prgarti.Text = "";
            textBox_prgeksi.Text = "";
            textBox_menu.Text = "";
            textBox_power.Text = "";
            textBox_red.Text = "";
            textBox_green.Text = "";
            textBox_yellow.Text = "";
            textBox_blue.Text = "";
            textBox_model.Text = "";
        }
        public void resimkoy()
        {
            try
            {
                GC.Collect();
                var id = dataGridView1.CurrentRow.Cells[0].Value.ToString(); //[0] sütun numarası
                if (string.IsNullOrEmpty(id))
                {
                    pictureBox1.Image = null;
                }
                else
                {
                    string dosyayolu = Application.StartupPath + "\\resimler\\" + id + ".jpg";
                    if (File.Exists(dosyayolu) == true) // dizindeki dosya var mı ?  
                    {
                        pictureBox1.ImageLocation = Application.StartupPath + "\\resimler\\" + id + ".jpg";
                        System.IO.FileInfo dosya = new System.IO.FileInfo(dosyayolu);
                        var boyut = dosya.Length;
                        textBox_img.Text = boyut.ToString(); //resimin boyutunu yaz textboxa

                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }
                }
            }
            catch (Exception hata)
            {
                //MessageBox.Show(hata.Message, "LOAD Mesaj !!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        public void kayitsil()
        {
            try
            {
                if (MessageBox.Show("SEÇİLEN KAYIT TAAMAMIYLA SİLİNECEK !!" + "\n" + "SİLİNEN KAYIT GERİ DÖNDÜRÜLEMEZ !!" + "\n" + "HAYIR SEÇİLİRSE SİLME İPTAL EDİLİR", "DİKKAT !!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    button_sil.Invoke((MethodInvoker)delegate { pictureBox2.Visible = true; });
                    Application.DoEvents();

                    var id = dataGridView1.CurrentRow.Cells[0].Value.ToString(); //[0] sütun numarası
                    string url = Application.StartupPath + "\\resimler\\" + id + ".jpg";
                    if (System.IO.File.Exists(url))
                    {
                        System.IO.File.Delete(url);
                    }
                    baglanti.Close();
                    baglanti.Open();
                    OleDbCommand sil = new OleDbCommand("Delete from tblole where ukıd=@id", baglanti);
                    sil.Parameters.AddWithValue("@id", id);
                    sil.ExecuteNonQuery();
                    baglanti.Close();
                    tbloleTableAdapter.GetData();
                    vtoleDataSet.Reset();
                    this.tbloleTableAdapter.Fill(this.vtoleDataSet.tblole);

                    adetlabel();
                    kilitle();

                    button_sil.Invoke((MethodInvoker)delegate { pictureBox2.Visible = false; });
                    MessageBox.Show(id + "  ID NOLU KAYIT SİLİNDİ !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("SİLME İŞLEMİ İPTAL EDİLDİ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("SİLME İŞLEMİ İPTAL EDİLDİ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }  // silme metodu
        public void kaydet()
        {
            try
            {
                if (String.IsNullOrEmpty(comboBox_turu.Text) &&
                    String.IsNullOrEmpty(textBox_swat.Text) &&
                    String.IsNullOrEmpty(textBox_class.Text) &&
                    String.IsNullOrEmpty(textBox_zapp.Text) &&
                    String.IsNullOrEmpty(textBox_ozt.Text) &&//merter
                    String.IsNullOrEmpty(textBox_sesarti.Text) &&
                    String.IsNullOrEmpty(textBox_seseksi.Text) &&
                    String.IsNullOrEmpty(textBox_prgarti.Text) &&
                    String.IsNullOrEmpty(textBox_prgeksi.Text) &&
                    String.IsNullOrEmpty(textBox_menu.Text) &&
                    String.IsNullOrEmpty(textBox_power.Text) &&
                    string.IsNullOrEmpty(textBox_model.Text))
                {
                    MessageBox.Show("VERİ GİRİŞİ YAPMADINIZ !!" + "\n" + "VERİLER GİRİLMEDEN" + "\n" + "KEYDETME YAPILAMAZ !!", "BİLGİLENDİRME..!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboBox_turu.Focus();
                }
                else
                {
                    if (pictureBox1.Image == null)
                    {
                        MessageBox.Show("LÜTFEN RESİM EKLEYİNİZ.." + "\n" + "RESİMSİZ KAYIT EKLENEMEZ..!!", "BİLGİLENDİRME");
                    }
                    else
                    {
                        button_kaydet.Invoke((MethodInvoker)delegate { pictureBox2.Visible = true; });
                        Application.DoEvents();
                        var x = dataGridView1.CurrentRow.Cells[0].Value.ToString();//datagrid aktif uk ıd no
                        baglanti.Close();
                        baglanti.Open();
                        var swat = textBox_swat.Text;
                        OleDbCommand kmtswat = new OleDbCommand("SELECT (ukıd) FROM tblole where swat= '" + swat + "' ", baglanti);
                        object swatkontrol = kmtswat.ExecuteScalar();
                        kmtswat.Dispose();
                        baglanti.Close();
                        var y = textBox_swat.Text.Length;

                        if (swatkontrol == null || y == 0)
                        {
                            kaydetkmt();
                        }
                        else
                        {
                            string swatkontroltxt = swatkontrol.ToString();
                            int son = Convert.ToInt32(swatkontroltxt);
                            if (son <= 0 || string.IsNullOrEmpty(textBox_swat.Text))
                            {
                                kaydetkmt();
                            }
                            else
                            {
                                button_kaydet.Invoke((MethodInvoker)delegate { pictureBox2.Visible = false; });
                                MessageBox.Show("( " + textBox_swat.Text + " )" + " SWAT KUMANDA KODU KAYITLARINIZDA MEVCUT" + "\n" + "MÜKERRER KAYIT YAPILAMAZ..!!", "BİLGİLENDİRME", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                textBox_swat.Focus();
                                textBox_swat.SelectionStart = 0;
                                textBox_swat.SelectionLength = textBox_swat.Text.Length;
                            }
                        }
                    }
                }
            }
            catch (Exception hata)
            {

                MessageBox.Show("HATA =  " + hata, "BİLGİ KAYDET HATA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        } // KAYDETME METODU
        public void kaydetkmt()//sağa sola bakmaz ne varsa kaydeder.
        {
            textBox_zapp.Text = textBox_zapp.Text.Trim();
            textBox_zapp.Text = textBox_zapp.Text.ToUpper();
            textBox_swat.Text = textBox_swat.Text.Trim();
            textBox_swat.Text = textBox_swat.Text.ToUpper();
            textBox_ozt.Text = textBox_ozt.Text.Trim();
            textBox_ozt.Text = textBox_ozt.Text.ToUpper();
            textBox_class.Text = textBox_class.Text.Trim();
            textBox_class.Text = textBox_class.Text.ToUpper();
            textBox_model.Text = textBox_model.Text.Trim();
            textBox_model.Text = textBox_model.Text.ToUpper();
            var id = dataGridView1.CurrentRow.Cells[0].Value.ToString(); //[0] sütun numarası
            var xx = Application.StartupPath + "\\resimler\\" + id + ".jpg";
            pictureBox1.Image.Save(xx);
            this.Validate();
            this.tbloleBindingSource.EndEdit();
            this.tbloleTableAdapter.Update(this.vtoleDataSet.tblole);
            adetlabel();
            dataGridView1.Enabled = true;
            kilitle();
            //tbloleBindingSource.MoveLast();
            button_kaydet.Invoke((MethodInvoker)delegate { pictureBox2.Visible = false; });
            MessageBox.Show("KAYDEDİLDİ !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void guncelle()
        {
            try
            {
                if (turutxty == comboBox_turu.Text &&
                    swattxty == textBox_swat.Text &&
                    classtxty == textBox_class.Text &&
                    zapptxty == textBox_zapp.Text &&
                    ozttxty == textBox_ozt.Text && //merter
                    sesartihexy == textBox_sesarti.Text &&
                    seseksihexy == textBox_seseksi.Text &&
                    prgartihexy == textBox_prgarti.Text &&
                    prgeksihexy == textBox_prgeksi.Text &&
                    menuhexy == textBox_menu.Text &&
                    powerhexy == textBox_power.Text &&
                    redhexy == textBox_red.Text &&
                    greenhexy == textBox_green.Text &&
                    yellowhexy == textBox_yellow.Text &&
                    bluehexy == textBox_blue.Text &&
                    modeltxty == textBox_model.Text &&
                    resimy == textBox_img.Text)
                {
                    MessageBox.Show("Kayıtlarda Değişiklik Yapmadınız.", "BİLGİ..!!");
                }
                else
                {
                    try
                    {
                        button_guncelle.Invoke((MethodInvoker)delegate { pictureBox2.Visible = true; });
                        Application.DoEvents();
                        baglanti.Close();
                        baglanti.Open();
                        //swat kodu verilen kaydın ukıd numarasını kontrol edelim varmı yokmu.
                        var swat = textBox_swat.Text;
                        OleDbCommand swatkontrolkmt = new OleDbCommand("SELECT (ukıd) FROM tblole where swat= '" + swat + "' ", baglanti);
                        object swatkontrol = swatkontrolkmt.ExecuteScalar();
                        swatkontrolkmt.Dispose();
                        baglanti.Close();
                        //*********************************
                        if (swatkontrol == null)
                        {
                            guncellekmt();//hiç bişey takmaz günceller.
                        }
                        else
                        {
                            string swatkontroltxt = swatkontrol.ToString();
                            var datagridswatid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                            var x = textBox_swat.Text.Length;
                            if (swatkontroltxt == datagridswatid || x == 0)// değişen kr kodu mevcudun id no ile işit ise
                            {
                                guncellekmt();
                            }
                            else
                            {

                                button_guncelle.Invoke((MethodInvoker)delegate { pictureBox2.Visible = false; });
                                MessageBox.Show("( " + textBox_swat.Text.ToUpper() + " )" + "SWAT KODU KAYITLARDA VAR" + "\n" + "KAYITLI SWAT KOD TEKRAR EKLENEMEZ..!!" + "\n" + "( GERİ AL ) BUTONU İLE KAYDINIZI İLK HALİNE GETİREBİLİRSİNİZ..!!", "BİLGİ..!!", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                                textBox_swat.Focus();
                                textBox_swat.SelectionStart = 0;
                                textBox_swat.SelectionLength = textBox_swat.Text.Length;
                            }

                        }

                    }
                    catch (Exception hata)
                    {
                        baglanti.Close();
                        MessageBox.Show("GÜNCELLEME HATA" + hata, "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //guncelle();
                    //MessageBox.Show("Kayıtlarda Değişiklik yapıldı", "BİLGİ..!!");
                }

            }
            catch (Exception hata)
            {


            }
        }
        public void guncellekmt()//hiç birşey takmaz günceller.
        {
            try
            {
                //boşlukları sil ve büyük karaktere çevir.
                textBox_zapp.Text = textBox_zapp.Text.Trim();
                textBox_zapp.Text = textBox_zapp.Text.ToUpper();
                textBox_swat.Text = textBox_swat.Text.Trim();
                textBox_swat.Text = textBox_swat.Text.ToUpper();
                textBox_ozt.Text = textBox_ozt.Text.Trim();
                textBox_ozt.Text = textBox_ozt.Text.ToUpper();
                textBox_class.Text = textBox_class.Text.Trim();
                textBox_class.Text = textBox_class.Text.ToUpper();
                textBox_model.Text = textBox_model.Text.Trim();
                textBox_model.Text = textBox_model.Text.ToUpper();
                //************************************************
                this.Validate();
                this.tbloleBindingSource.EndEdit();
                this.tbloleTableAdapter.Update(this.vtoleDataSet.tblole);
                geciciyedekle();
                var id = dataGridView1.CurrentRow.Cells[0].Value.ToString(); //[0] sütun numarası
                var x = Application.StartupPath + "\\resimler\\" + id + ".jpg";
                pictureBox1.Image.Save(x);
                kilitle();
                button_guncelle.Invoke((MethodInvoker)delegate { pictureBox2.Visible = false; });
                MessageBox.Show("KAYITLAR GÜNCELLENDİ !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception hata)
            {


            }
        }
        protected void yedekle1(string Prmt1, string prmt2, bool prmt3)
        {
            DirectoryInfo DrInf = new DirectoryInfo(Prmt1);
            DirectoryInfo[] DrInfLst = DrInf.GetDirectories();
            //if (!Directory.Exists(prmt2))
            //{
            Directory.CreateDirectory(prmt2);
            //}

            FileInfo[] dosya = DrInf.GetFiles();
            string path1 = "";
            foreach (FileInfo FFF in dosya)
            {
                path1 = Path.Combine(prmt2, FFF.Name);
                FFF.CopyTo(path1, false);
            }
            if (true)
            {
                foreach (DirectoryInfo bilgi in DrInfLst)
                {
                    path1 = Path.Combine(prmt2, bilgi.Name);
                    yedekle1(bilgi.FullName, path1, true);
                }
            }
        }
        private void animasyon_guncelle_thread()
        {
            guncelle();
            button_guncelle.Invoke((MethodInvoker)delegate { pictureBox2.Visible = false; });
        }
        private void animasyon_kaydet_thread()
        {
            kaydet();
            button_kaydet.Invoke((MethodInvoker)delegate { pictureBox2.Visible = false; });
        }
        private void animasyon_sil_thread()
        {
            kayitsil();
            button_sil.Invoke((MethodInvoker)delegate { pictureBox2.Visible = false; });
        }
        //FORM OLAYLARI-------------------------------------------------
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception != null &&
                e.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show(e.Exception.ToString());
            }
        }
        private void frmekle_Load(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dataGridView1_DataError);
                this.label1.Text = "V: 3.0";
                pictureBox2.Visible = false;
                this.tbloleTableAdapter.Fill(this.vtoleDataSet.tblole);
                ayarlar();
                GC.Collect();
                System.GC.SuppressFinalize(this);

            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "LOAD Mesaj !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbloleBindingSource.ResetBindings(false);
            }
        }
        private void frmekle_Shown(object sender, EventArgs e)
        {
            textBox_ara.Focus();
            adetlabel();
            otocom();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string sonuc = serialPort1.ReadExisting();//Seri porttan gelen bilgiyi sonuç değişkenine aktardık.

                if (String.IsNullOrEmpty(sonuc))   //sonuç değişkenini boş boş dolu kontrolu yapıldı.
                {
                    // seri portta bilgi akışı yok ise "sonuc" boşsa yapılacaklar
                }
                else    //sonuc boş değilse yapılacaklar
                {
                    textBox_ara.Text = sonuc.Trim();  //sonuç değişkeninindeki boşlukları sil
                }
            }
            catch (Exception hata)
            {
                if (hata is IndexOutOfRangeException || hata is DivideByZeroException || hata is Exception)
                {
                    serialPort1.Close();
                    timer1.Stop();
                    com_ToolStripMenuItem.Text = "DONGLE TAKIN.!!";
                    com_ToolStripMenuItem.Image = Properties.Resources.iconfinder_Red_ball_132008;
                    baglan_ToolStripMenuItem.Visible = true;
                    baglantikes_ToolStripMenuItem.Visible = false;
                    MessageBox.Show("Lütfen Dongle Takınız" + "\n" + "DONGLE AYGITI ÇIKARILDI..!!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void textBox_ara_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (panel1.Enabled == false)
                {
                    if (textBox_ara.Text.Length < 2)
                    {
                        tbloleBindingSource.RemoveFilter();
                    }
                    else
                    {
                        string filter = string.Format("turu LIKE '%{0}%' OR swat LIKE '%{0}%' OR zapp LIKE '%{0}%'OR class LIKE '%{0}%' OR ozt LIKE '%{0}%'OR sesarti LIKE '%{0}%' OR seseksi LIKE '%{0}%'OR prgarti LIKE '%{0}%' OR prgeksi LIKE '%{0}%'OR menu LIKE '%{0}%' OR power LIKE '%{0}%' OR model LIKE '%{0}%' OR red LIKE '%{0}%' OR green LIKE '%{0}%' OR blue LIKE '%{0}%' OR yellow LIKE '%{0}%'", textBox_ara.Text);
                        tbloleBindingSource.Filter = filter;

                        string aranan = textBox_ara.Text.Trim();
                        for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                foreach (DataGridViewCell cell in dataGridView1.Rows[i].Cells)
                                {
                                    if (cell.Value != null)
                                    {
                                        if (cell.Value.ToString().ToUpper() == aranan)
                                        {
                                            cell.Style.BackColor = Color.DarkTurquoise;
                                            break;
                                        }
                                    }

                                }
                            }
                        }


                    }
                    adetlabel();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmekle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                tbloleBindingSource.MovePrevious();
            }
            if (e.KeyCode == Keys.Down)
            {
                tbloleBindingSource.MoveNext();
            }
            if (e.KeyCode == Keys.Left)
            {

            }
            if (e.KeyCode == Keys.Right)
            {

            }
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("SEÇİLEN KAYIT SİLİNECEK !!" + "\n" + "SİLİNEN KAYIT GERİ DÖNDÜRÜLEMEZ !!", "DİKKAT !!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    tbloleBindingSource.RemoveCurrent();
                    tbloleBindingSource.EndEdit();
                    tbloleTableAdapter.Update(this.vtoleDataSet.tblole);
                }

                MessageBox.Show("KAYIT SİLİNDİ !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        //MENÜ STRİP CLİCK--------------------------------------------------
        private void googleswat_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_ara.Text.Length > 0)
                {
                    //textBox_sesarti.Text = textBox_ara.Text;
                    System.Diagnostics.Process.Start("https://www.google.com/search?q=" + textBox_ara.Text + " site:www.caglarelektronik.com.tr");
                }
                else
                {
                    MessageBox.Show("ARAMA KRİTERİ GİRİLMEMİŞ !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void googletext_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_ara.Text.Length > 0)
                {
                    //textBox_sesarti.Text = textBox_ara.Text;
                    System.Diagnostics.Process.Start("https://www.google.com/search?q=" + textBox_ara.Text + " kumanda");
                }
                else
                {
                    MessageBox.Show("ARAMA KRİTERİ GİRİLMEMİŞ !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Exit_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (MessageBox.Show("PROGRAM KAPANACAK !", "DİKKAT !!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Environment.Exit(0);
                }
                else
                {

                }
            }
            catch
            {

            }
        }
        private void ToolStripMenuItem_sil_Click(object sender, EventArgs e)
        {


        }
        private void Ara_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox_ara.Focus();
        }
        private void Hakkinda_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //hakkinda hakkimda = new hakkinda();
            //hakkinda hakkimda;
            if (hakkimda == null || hakkimda.IsDisposed)
            {
                hakkimda = new hakkinda();
                hakkimda.Show();
            }

        }
        private void Baglan_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // if (serialPort1.IsOpen == false)   // seri port açıksa denetimi yapıldı
            string[] portlar = SerialPort.GetPortNames();
            var portsay = portlar.Count();
            if (portsay == 0)
            {
                com_ToolStripMenuItem.Text = "DONGLE TAKIN.!!";
                MessageBox.Show("Lütfen Dongle Takınız", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            foreach (string portAdi in portlar)
            {
                try
                {
                    serialPort1.PortName = portAdi;
                    serialPort1.Open();
                    serialPort1.WriteLine("S");
                    serialPort1.ReadTimeout = 200;
                    serialPort1.WriteTimeout = 200;
                    string okunan = serialPort1.ReadLine();
                    if (okunan == "KMPON")
                    {
                        MessageBox.Show("1");
                    }
                    else
                    {
                        com_ToolStripMenuItem.Text = "DONGLE BAĞLANDI";
                        com_ToolStripMenuItem.Image = Properties.Resources.iconfinder_Green_ball_131927;
                        baglan_ToolStripMenuItem.Visible = false;
                        baglantikes_ToolStripMenuItem.Visible = true;
                        timer1.Start();
                        textBox_ara.Focus();
                    }
                }
                catch (Exception hata)
                {
                    if (hata is IndexOutOfRangeException || hata is DivideByZeroException || hata is Exception)
                    {
                        serialPort1.Close();
                        timer1.Stop();
                        com_ToolStripMenuItem.Text = "DONGLE TAKIN.!!";
                        com_ToolStripMenuItem.Image = Properties.Resources.iconfinder_Red_ball_132008;
                        //MessageBox.Show("Lütfen Dongle Takınız", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }

            }

        }
        private void baglantikes_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen == true)
                {
                    com_ToolStripMenuItem.Text = "COM KAPANDI";
                    com_ToolStripMenuItem.Image = Properties.Resources.iconfinder_Red_ball_132008;
                    baglantikes_ToolStripMenuItem.Visible = false;
                    baglan_ToolStripMenuItem.Visible = true;
                    timer1.Stop();
                    serialPort1.Close();
                }
                else
                {
                    MessageBox.Show("COM PORT KAPALI..!!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch
            {

            }
        }
        private void muadilgoster_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                button_kritersil.Enabled = true;
                switch (toolStripComboBox_ara.Text)
                {
                    case "TÜMÜ":
                        if (textBox_sesarti.Text.Length > 0 && textBox_seseksi.Text.Length > 0 && textBox_prgarti.Text.Length > 0 && textBox_prgeksi.Text.Length > 0 && textBox_menu.Text.Length > 0 && textBox_power.Text.Length > 0)
                        {

                            string filter1 = "sesarti='" + textBox_sesarti.Text + "'and seseksi='" + textBox_seseksi.Text + "' and prgarti='" + textBox_prgarti.Text + "' and prgeksi='" + textBox_prgeksi.Text + "' and menu='" + textBox_menu.Text + "' and power='" + textBox_power.Text + "'";
                            tbloleBindingSource.Filter = filter1;
                            muadillabel();
                        }
                        else
                        {
                            // adetlabel();
                            MessageBox.Show("( TÜM HEX ) KODLARI TANIMLI DEĞİL !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    case "TÜMÜ VE RENKLER":
                        if (textBox_sesarti.Text.Length > 0 && textBox_seseksi.Text.Length > 0 && textBox_prgarti.Text.Length > 0 && textBox_prgeksi.Text.Length > 0 && textBox_menu.Text.Length > 0 && textBox_power.Text.Length > 0 && textBox_red.Text.Length > 0 && textBox_green.Text.Length > 0 && textBox_yellow.Text.Length > 0 && textBox_blue.Text.Length > 0)
                        {

                            string filter1 = "sesarti='" + textBox_sesarti.Text + "'and seseksi='" + textBox_seseksi.Text + "' and prgarti='" + textBox_prgarti.Text + "' and prgeksi='" + textBox_prgeksi.Text + "' and menu='" + textBox_menu.Text + "' and power='" + textBox_power.Text + "'and red='" + textBox_red.Text + "'and green='" + textBox_green.Text + "'and yellow='" + textBox_yellow.Text + "'and blue='" + textBox_blue.Text + "'";
                            tbloleBindingSource.Filter = filter1;
                            muadillabel();
                        }
                        else
                        {
                            // adetlabel();
                            MessageBox.Show("( TÜM HEX VE RENK ) KODLARI TANIMLI DEĞİL !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    case "SAĞ TUŞ":
                        if ((textBox_sesarti.Text.Length > 0))
                        {
                            string filter2 = "sesarti='" + textBox_sesarti.Text + "'";
                            tbloleBindingSource.Filter = filter2;
                            muadillabel();
                        }
                        else
                        {
                            //adetlabel();
                            MessageBox.Show("( SES ARTI ) KODU TANIMLI DEĞİL !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case "SOL TUŞ":
                        if ((textBox_seseksi.Text.Length > 0))
                        {
                            string filter3 = "seseksi='" + textBox_seseksi.Text + "'";
                            tbloleBindingSource.Filter = filter3;
                            muadillabel();
                        }
                        else
                        {
                            //adetlabel();
                            MessageBox.Show("( SES EKSİ ) KODU TANIMLI DEĞİL !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    case "YUKARI TUŞ":
                        if ((textBox_prgarti.Text.Length > 0))
                        {
                            string filter4 = "prgarti='" + textBox_prgarti.Text + "'";
                            tbloleBindingSource.Filter = filter4;
                            muadillabel();
                        }
                        else
                        {
                            //adetlabel();
                            MessageBox.Show("( PRG ARTI ) KODU TANIMLI DEĞİL !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    case "AŞAĞI TUŞ":
                        if ((textBox_prgeksi.Text.Length > 0))
                        {
                            string filter5 = "prgeksi='" + textBox_prgeksi.Text + "'";
                            tbloleBindingSource.Filter = filter5;
                            muadillabel();
                        }
                        else
                        {
                            //adetlabel();
                            MessageBox.Show("( PRG EKSİ ) KODU TANIMLI DEĞİL !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    case "SAĞ SOL YUKARI AŞAĞI":
                        if ((textBox_menu.Text.Length > 0))
                        {
                            string filter6 = "sesarti='" + textBox_sesarti.Text + "' and seseksi='" + textBox_seseksi.Text + "' and prgarti='" + textBox_prgarti.Text + "' and prgeksi='" + textBox_prgeksi.Text + "'";
                            tbloleBindingSource.Filter = filter6;
                            muadillabel();
                        }
                        else
                        {
                            //adetlabel();
                            MessageBox.Show("( SESARTI & SESEKSİ & PRGARTI & PRGEKSİ ) KODU TANIMLI DEĞİL !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case "MENÜ":
                        if ((textBox_menu.Text.Length > 0))
                        {
                            string filter7 = "menu='" + textBox_menu.Text + "'";
                            tbloleBindingSource.Filter = filter7;
                            muadillabel();
                        }
                        else
                        {
                            //adetlabel();
                            MessageBox.Show("( MENÜ ) KODU TANIMLI DEĞİL !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    case "POWER":
                        if ((textBox_power.Text.Length > 0))
                        {
                            string filter8 = "power='" + textBox_power.Text + "'";
                            tbloleBindingSource.Filter = filter8;
                            muadillabel();
                        }
                        else
                        {
                            //adetlabel();
                            MessageBox.Show("( POWER ) KODU TANIMLI DEĞİL !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    case "MENÜ VE POWER":
                        if (textBox_menu.Text.Length > 0 && textBox_power.Text.Length > 0)
                        {
                            string filter9 = "menu='" + textBox_menu.Text + "'and power='" + textBox_power.Text + "'";
                            tbloleBindingSource.Filter = filter9;
                            muadillabel();
                        }
                        else
                        {
                            //adetlabel();
                            MessageBox.Show("( MENÜ VE POWER ) KODLARI TANIMLI DEĞİL !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    default:
                        MessageBox.Show("LÜTFEN ÖNCE KRİTER SEÇİNİZ. !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                }
                //label14.Text = "MUADİL BULUNAN KAYIT ADEDİ :  " + tbloleBindingSource.Count.ToString();
                //muadilmsj();
                //muadillabel();
            }
            catch (Exception)
            {
                adetlabel();
                MessageBox.Show("KRİTERE UYAN KAYIT YOK.!!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void toolStripMenuItem_yedekle_Click(object sender, EventArgs e)
        {
            try
            {
                string kaynakdosya = "vtole";
                string kaynaktur = ".accdb";
                string kaynakklasor = Application.StartupPath; //KAYNAK DOSYASI PROGRAM KLASÖRÜ
                string kaynakyol = System.IO.Path.Combine(kaynakklasor, kaynakdosya + kaynaktur); // BİRLEŞTİR PATH 
                //---------------HEDEF DOSYA değişkenleri---------------------------------------------
                //string hedefdosya = "vtole";
                string hedeftur = ".accdb";
                //---------------HEDEF KLASÖR OLUŞTURMA değişkenleri ------------------------------------
                string tarih = DateTime.Now.ToString("dd-MM-yyyy HH-mm");
                string hedefklasöradi = "-KMP" + prgvers + " " + vtvers + " YEDEK";
                string hedefstart = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string hedefyol = System.IO.Path.Combine(hedefstart, tarih + hedefklasöradi);



                if (!System.IO.Directory.Exists(hedefyol)) //hedef klasör varmı yokmu kontrol et
                {
                    MessageBox.Show("TÜM KAYITLARINIZIN YEDEĞİ MASAÜSTÜNE" + "\n" + "( " + tarih + hedefklasöradi + ") İSİMLİ KLASÖRDE YEDEKLENECEK", "BİLGİLENDİRME..!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.IO.Directory.CreateDirectory(hedefyol); // klasör yok ise oluştur
                    System.IO.Directory.CreateDirectory(hedefyol + "\\resimler");

                    System.IO.File.Copy(kaynakyol, hedefyol + "\\" + kaynakdosya + hedeftur);//taşı
                    yedekle1(kaynakklasor + "\\resimler", hedefyol + "\\resimler", true);
                    MessageBox.Show(vtvers + " VERİTABANI MASAÜSTÜNE YEDEKLENDİ.");
                }
                else
                {
                    MessageBox.Show("MASAÜSTÜNDE " + "\n" + tarih + hedefklasöradi + "  İSMİNDE KLASÖRÜNÜZ MEVCUT..!!" + "\n" + "MEVCUT YEDEĞİ SİLİNİZ ( DAKİKADA 1 DEFA YEDEK ALINABİLİR. )", "DİKKAT...!!", MessageBoxButtons.OK, MessageBoxIcon.Error);//klasör varsa mesajla bildir
                }
            }
            catch (Exception)
            {
                MessageBox.Show("HATA OLUŞTU..!!");
            }
        }
        private void toolStripMenuItem_yedekal_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog
                {
                    Filter = "KMP VERİTABANI |*.accdb",
                    Title = "( MKP ) VERİTABANINI SEÇİNİZ..!!",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                };
                //file.ShowDialog();
                if (file.ShowDialog() == DialogResult.OK)
                {

                    string DosyaYolu = file.FileName;
                    string DosyaAdi = file.SafeFileName;
                    System.IO.File.Copy(DosyaYolu, Application.StartupPath + "\\" + DosyaAdi, true);
                    bool dizin = Directory.Exists(Application.StartupPath + "\\resimler");
                    if (dizin == true)
                    {
                        Directory.Delete(Application.StartupPath + "\\resimler", true);

                        // MessageBox.Show("KLASOR3 SILINDI");

                    }
                    string sorun = DosyaYolu;
                    string cozum = sorun.Substring(0, sorun.Length - 12);
                    yedekle1(cozum + "\\resimler", Application.StartupPath + "\\resimler", true);
                    tbloleTableAdapter.GetData();
                    vtoleDataSet.Reset();
                    this.tbloleTableAdapter.Fill(this.vtoleDataSet.tblole);
                    tbloleBindingSource.DataSource = this.vtoleDataSet.tblole;
                    adetlabel();
                    MessageBox.Show("YEDEK VERİTABANI KAYDEDİLDİ" + "\n" + "PROGRAM YENİ VERİ TABANINA GÜNCELLENDİ..!!", "BİLGİ..!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("YEDEK GERİ YÜKLEME HATA OLUŞTU..!!");
            }
        }
        private void toolStripMenuItem_resimal_Click(object sender, EventArgs e)
        {
            string tarih = DateTime.Now.ToString("dd-MM-yyyy HH-mm");
            string dosya_adi = tarih + "-KMP.Jpeg";
            string dosya_yolu = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string hedef_yol = System.IO.Path.Combine(dosya_yolu, dosya_adi);
            var frm = Form.ActiveForm;

            using (var bmp = new Bitmap(frm.Width, frm.Height))
            {

                frm.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                bmp.Save(hedef_yol, ImageFormat.Jpeg);
            }
        }
        private void toolStripMenuItem_paste_Click(object sender, EventArgs e)
        {
            yapistir();
        }
        private void eKRANRESMİALToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tarih = DateTime.Now.ToString("dd-MM-yyyy HH-mm");
            string dosya_adi = tarih + "-KMP.Jpeg";
            string dosya_yolu = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string hedef_yol = System.IO.Path.Combine(dosya_yolu, dosya_adi);
            var frm = Form.ActiveForm;

            using (var bmp = new Bitmap(frm.Width, frm.Height))
            {

                frm.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                bmp.Save(hedef_yol, ImageFormat.Jpeg);
            }
        }
        private void button_sil_Click(object sender, EventArgs e)
        {
            ThreadStart threadStart2 = new ThreadStart(animasyon_sil_thread);
            Thread thread2 = new Thread(threadStart2);
            thread2.SetApartmentState(ApartmentState.STA);
            thread2.Start();
        }
        private void toolStripMenuItem_copy_Click(object sender, EventArgs e)
        {
            kopyala();
        }
        private void toolStripMenuItem_al_Click(object sender, EventArgs e)
        {

            verial();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int i;
            var count = dataGridView1.RowCount.ToString();
            var id = dataGridView1.CurrentRow.Cells[0].Value.ToString(); //[0] sütun numarası

            for (i = 1; i <= dataGridView1.RowCount; i++)
            {
                var x = Application.StartupPath + "\\resimler\\" + id + ".jpg";
                pictureBox1.Image.Save(x);
                tbloleBindingSource.MoveNext();
            }
        }
        private void toolStripComboBox_ayar_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBox_ayar.Text)
            {
                case "AÇIK":
                    Properties.Settings.Default.yardim = "true";
                    Properties.Settings.Default.Save();
                    toolTip1.Active = true;
                    //toolStripComboBox_ayar.SelectedIndex = 1;

                    break;
                case "KAPALI":
                    Properties.Settings.Default.yardim = "false";
                    Properties.Settings.Default.Save();
                    toolTip1.Active = false;
                    // toolStripComboBox_ayar.SelectedIndex = 1;

                    break;
            }
        }
        private void ToolStripMenuItem_ver_Click(object sender, EventArgs e)
        {
            veriver();
        }
        //FORM CLİCK-------------------------------------------------------
        private void Button_duzenle_Click(object sender, EventArgs e)
        {
            kilitle();
        }
        private void Button_sesarti_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen == true)
                {
                    if (textBox_ara.Text.Length > 0)
                    {
                        textBox_sesarti.Text = textBox_ara.Text;
                    }
                    else
                    {
                        MessageBox.Show("ÖNCELİKLE KUMANDANIN ( RIGHT : SAĞ TUŞ ) BASARAK !!" + "\n" + "KUMANDANIZIN ( RIGHT: SAĞ TUŞ ) KODUNU OKUTUNUZ !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("ÖNCELİKLE DONGLE BAĞLANIN.. !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Button_seseksi_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen == true)
                {
                    if (textBox_ara.Text.Length > 0)
                    {
                        textBox_seseksi.Text = textBox_ara.Text;
                    }
                    else
                    {
                        MessageBox.Show("ÖNCELİKLE KUMANDANIN ( LEFT : SOL TUŞ ) BASARAK !!" + "\n" + "KUMANDANIZIN ( LEFT : SOL TUŞ ) KODUNU OKUTUNUZ !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("ÖNCELİKLE DONGLE BAĞLANIN.. !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Button_prgarti_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen == true)
                {
                    if (textBox_ara.Text.Length > 0)
                    {
                        textBox_prgarti.Text = textBox_ara.Text;
                    }
                    else
                    {
                        MessageBox.Show("ÖNCELİKLE KUMANDANIN ( UP : YUKARI TUŞ ) BASARAK !!" + "\n" + "KUMANDANIZIN ( UP : YUKARI TUŞ ) KODUNU OKUTUNUZ !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("ÖNCELİKLE DONGLE BAĞLANIN.. !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Button_prgeksi_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen == true)
                {
                    if (textBox_ara.Text.Length > 0)
                    {
                        textBox_prgeksi.Text = textBox_ara.Text;
                    }
                    else
                    {
                        MessageBox.Show("ÖNCELİKLE KUMANDANIN ( DOWN : AŞAĞI TUŞ ) BASARAK !!" + "\n" + "KUMANDANIZIN ( DOWN : AŞAĞI TUŞ ) KODUNU OKUTUNUZ !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("ÖNCELİKLE DONGLE BAĞLANIN.. !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Button_menu_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen == true)
                {
                    if (textBox_ara.Text.Length > 0)
                    {
                        textBox_menu.Text = textBox_ara.Text;
                    }
                    else
                    {
                        MessageBox.Show("ÖNCELİKLE KUMANDANIN ( MENÜ ) TUŞUNA BASARAK !!" + "\n" + "KUMANDANIZIN ( MENÜ ) KODUNU OKUTUNUZ !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("ÖNCELİKLE DONGLE BAĞLANIN.. !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Button_power_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen == true)
                {
                    if (textBox_ara.Text.Length > 0)
                    {
                        textBox_power.Text = textBox_ara.Text;
                    }
                    else
                    {
                        MessageBox.Show("ÖNCELİKLE KUMANDANIN ( POWER ) TUŞUNA BASARAK !!" + "\n" + "KUMANDANIZIN ( POWER ) KODUNU OKUTUNUZ !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("ÖNCELİKLE DONGLE BAĞLANIN.. !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Button_red_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen == true)
                {
                    if (textBox_ara.Text.Length > 0)
                    {
                        textBox_red.Text = textBox_ara.Text;
                    }
                    else
                    {
                        MessageBox.Show("ÖNCELİKLE KUMANDANIN ( KIRMIZI ) TUŞUNA BASARAK !!" + "\n" + "KUMANDANIZIN ( KIRMIZI ) KODUNU OKUTUNUZ !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("ÖNCELİKLE DONGLE BAĞLANIN.. !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Button_green_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen == true)
                {
                    if (textBox_ara.Text.Length > 0)
                    {
                        textBox_green.Text = textBox_ara.Text;
                    }
                    else
                    {
                        MessageBox.Show("ÖNCELİKLE KUMANDANIN ( YEŞİL ) TUŞUNA BASARAK !!" + "\n" + "KUMANDANIZIN ( YEŞİL ) KODUNU OKUTUNUZ !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("ÖNCELİKLE DONGLE BAĞLANIN.. !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Button_blue_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen == true)
                {
                    if (textBox_ara.Text.Length > 0)
                    {
                        textBox_blue.Text = textBox_ara.Text;
                    }
                    else
                    {
                        MessageBox.Show("ÖNCELİKLE KUMANDANIN ( MAVİ ) TUŞUNA BASARAK !!" + "\n" + "KUMANDANIZIN ( MAVİ ) KODUNU OKUTUNUZ !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("ÖNCELİKLE DONGLE BAĞLANIN.. !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Button_yellow_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen == true)
                {
                    if (textBox_ara.Text.Length > 0)
                    {
                        textBox_yellow.Text = textBox_ara.Text;
                    }
                    else
                    {
                        MessageBox.Show("ÖNCELİKLE KUMANDANIN ( SARI ) TUŞUNA BASARAK !!" + "\n" + "KUMANDANIZIN ( SARI ) KODUNU OKUTUNUZ !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("ÖNCELİKLE DONGLE BAĞLANIN.. !!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Button_kaydet_Click(object sender, EventArgs e)
        {
            ThreadStart threadStart1 = new ThreadStart(animasyon_kaydet_thread);
            Thread thread1 = new Thread(threadStart1);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();
        }
        private void button_guncelle_Click(object sender, EventArgs e)
        {
            ThreadStart threadStart = new ThreadStart(animasyon_guncelle_thread);
            Thread thread = new Thread(threadStart);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        private void Button_ekle_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(textBox_sesarti.Text) && String.IsNullOrEmpty(textBox_seseksi.Text) && String.IsNullOrEmpty(textBox_prgarti.Text) && String.IsNullOrEmpty(textBox_prgeksi.Text) && String.IsNullOrEmpty(textBox_menu.Text) && String.IsNullOrEmpty(textBox_power.Text) && String.IsNullOrEmpty(textBox_zapp.Text) && String.IsNullOrEmpty(textBox_class.Text) && String.IsNullOrEmpty(textBox_ozt.Text) && String.IsNullOrEmpty(textBox_swat.Text))
                {
                    MessageBox.Show("VERİ GİRİŞİ YAPMADINIZ !!" + "\n" + "MEVCUT KAYITLAR OLUŞMADAN" + "\n" + "YENİ KAYIT YAPILAMAZ !!");
                    comboBox_turu.Focus();
                }
                else
                {
                    MessageBox.Show("YENİ KUMANDA KAYITI EKLENECEK..!!", "BİLGİLENDİRME..!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button_iptal.Enabled = true;
                    button_kaydet.Enabled = true;
                    button_guncelle.Enabled = false;
                    comboBox_turu.Focus();
                    button_ekle.Enabled = false;
                    tbloleBindingSource.AddNew();
                    dataGridView1.Enabled = false;
                    if (pictureBox1.Image != null)
                    {
                        Image x = pictureBox1.Image;
                        int widht = x.Width;
                        int height = x.Height;
                        int toplam = widht * height;
                        textBox_img.Text = toplam.ToString();
                    }
                    else
                    {
                        Image x = pictureBox1.BackgroundImage;
                        int widht = x.Width;
                        int height = x.Height;
                        int toplam = widht * height;
                        textBox_img.Text = toplam.ToString();
                        //textBox_img.Text = "yok";
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbloleBindingSource.ResetBindings(false);
            }
        }
        private void Button_resim_Click(object sender, EventArgs e)
        {

            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = "RESİM SEÇİN|*.jpg; *jpeg; *jpe",
                Title = "KUMANDA RESMİ SEÇİN..!!",
                RestoreDirectory = true
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            image = new Bitmap(openFileDialog1.FileName);
                            pictureBox1.Image = (Image)image;
                            Image x = pictureBox1.Image;
                            System.IO.FileInfo dosya = new System.IO.FileInfo(openFileDialog1.FileName);
                            var boyut = dosya.Length;
                            textBox_img.Text = boyut.ToString(); //resimin boyutunu yaz textboxa
                        }
                    }
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message, "resim mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void Button_temizle_Click(object sender, EventArgs e)
        {
            textBox_ara.Clear();
            textBox_ara.Focus();
            tbloleBindingSource.RemoveFilter();
            //tbloleBindingSource.ResetBindings(true);
            //dataGridView1.Sort(dataGridView1.Columns[0],ListSortDirection.Descending);//Tersten Sıralar
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);//Normal Sıralama
            tbloleBindingSource.MoveFirst();
            adetlabel();
        } // arama temizle
        private void Button_kritersil_Click(object sender, EventArgs e)
        {
            tbloleBindingSource.RemoveFilter();
            string filter = string.Format("turu LIKE '%{0}%' OR swat LIKE '%{0}%' OR zapp LIKE '%{0}%'OR class LIKE '%{0}%' OR ozt LIKE '%{0}%'OR sesarti LIKE '%{0}%' OR seseksi LIKE '%{0}%'OR prgarti LIKE '%{0}%' OR prgeksi LIKE '%{0}%'OR menu LIKE '%{0}%' OR power LIKE '%{0}%' OR model LIKE '%{0}%'", textBox_ara.Text);
            tbloleBindingSource.Filter = filter;
            button_kritersil.Enabled = false;
            adetlabel();
        }
        private void Button_minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        } //formu tepsiye indirir.
        private void Button_iptal_Click(object sender, EventArgs e)
        {
            try
            {

                if (String.IsNullOrEmpty(textBox_sesarti.Text) && String.IsNullOrEmpty(textBox_seseksi.Text) && String.IsNullOrEmpty(textBox_prgarti.Text) && String.IsNullOrEmpty(textBox_prgeksi.Text) && String.IsNullOrEmpty(textBox_menu.Text) && String.IsNullOrEmpty(textBox_power.Text) && String.IsNullOrEmpty(textBox_zapp.Text) && String.IsNullOrEmpty(textBox_class.Text) && String.IsNullOrEmpty(textBox_ozt.Text) && String.IsNullOrEmpty(comboBox_turu.Text) && String.IsNullOrEmpty(textBox_swat.Text))
                {
                    // MessageBox.Show("VERİ GİRİŞİ YAPMADINIZ !!" + "\n" + "MEVCUT KAYITLAR OLUŞMADAN" + "\n" + "YENİ KAYIT YAPILAMAZ !!");
                    //panel1.Enabled = false;
                    //panel2.Enabled = false;

                    dataGridView1.Enabled = true;
                    button_iptal.Enabled = false;
                    button_kaydet.Enabled = false;
                    button_guncelle.Enabled = true;
                    button_ekle.Enabled = true;
                    tbloleBindingSource.RemoveCurrent();
                    tbloleBindingSource.MoveFirst();
                    tbloleBindingSource.ResetBindings(false);

                }
                else
                {
                    txtsil();
                    dataGridView1.Enabled = true;
                    button_iptal.Enabled = false;
                    button_kaydet.Enabled = false;
                    button_guncelle.Enabled = true;
                    button_ekle.Enabled = true;
                    tbloleBindingSource.RemoveCurrent();
                    tbloleBindingSource.MoveFirst();
                    tbloleBindingSource.ResetBindings(false);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbloleBindingSource.ResetBindings(false);
            }

        }
        private void PictureBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"C:\WINDOWS\system32\Magnify.exe");
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Process islem in Process.GetProcessesByName("Magnify"))
                {
                    islem.Kill();
                }

            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Button_gerial_Click(object sender, EventArgs e)
        {
            //textBox_zapp.Focus();
            //SendKeys.Send("^(z)");7
            yedekgerial();
        }
        //DATA GRİD OLAYLARI-----------------------------------------------
        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    kayitsil();
                }

            }
            catch
            {

            }
        }
        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

                resimkoy();

                geciciyedekle();
            }
            catch
            {

            }
        }
    }
}
