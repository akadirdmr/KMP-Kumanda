using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace kumanda
{
    partial class hakkinda : Form
    //public class hakkinda : Form
    {
        public hakkinda()
        {
            InitializeComponent();
            this.Text = String.Format("{0}", AssemblyTitle);
            this.labelProductName.Text = String.Format("Ürün :  {0}",AssemblyProduct);
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\vtole.accdb;Jet OLEDB:Database Password=mehmetalbayrak");
        private void hakkinda_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand zapp_say = new OleDbCommand("SELECT Count(zapp) FROM tblole ", baglanti);
            object zapp = zapp_say.ExecuteScalar();
            OleDbCommand class_say = new OleDbCommand("SELECT Count(class) FROM tblole ", baglanti);
            object clas = class_say.ExecuteScalar();
            OleDbCommand swat_say = new OleDbCommand("SELECT Count(swat) FROM tblole ", baglanti);
            object swat = swat_say.ExecuteScalar();
            OleDbCommand ozt_say = new OleDbCommand("SELECT Count(ozt) FROM tblole ", baglanti);
            object ozt = ozt_say.ExecuteScalar();
            OleDbCommand sesarti_say = new OleDbCommand("SELECT Count(sesarti) FROM tblole ", baglanti);
            object sesarti = sesarti_say.ExecuteScalar();
            OleDbCommand seseksi_say = new OleDbCommand("SELECT Count(seseksi) FROM tblole ", baglanti);
            object seseksi = seseksi_say.ExecuteScalar();
            OleDbCommand prgarti_say = new OleDbCommand("SELECT Count(prgarti) FROM tblole ", baglanti);
            object prgarti = prgarti_say.ExecuteScalar();
            OleDbCommand prgeksi_say = new OleDbCommand("SELECT Count(prgeksi) FROM tblole ", baglanti);
            object prgeksi = prgeksi_say.ExecuteScalar();
            OleDbCommand menu_say = new OleDbCommand("SELECT Count(menu) FROM tblole ", baglanti);
            object menu = menu_say.ExecuteScalar();
            OleDbCommand power_say = new OleDbCommand("SELECT Count(power) FROM tblole ", baglanti);
            object power = power_say.ExecuteScalar();
            OleDbCommand id_say = new OleDbCommand("SELECT Count(ukıd) FROM tblole ", baglanti);
            object id = id_say.ExecuteScalar();
            OleDbCommand tv_say = new OleDbCommand("SELECT Count(ukıd) FROM tblole where turu='TELEVİZYON'", baglanti);
            object tv = tv_say.ExecuteScalar();
            OleDbCommand rec_say = new OleDbCommand("SELECT Count(ukıd) FROM tblole where turu='RECEİVER'", baglanti);
            object rec = rec_say.ExecuteScalar();
            OleDbCommand dvd_say = new OleDbCommand("SELECT Count(ukıd) FROM tblole where turu='DVD'", baglanti);
            object dvd = dvd_say.ExecuteScalar();
            OleDbCommand klima_say = new OleDbCommand("SELECT Count(ukıd) FROM tblole where turu='KLİMA'", baglanti);
            object klima = klima_say.ExecuteScalar();
            OleDbCommand sessis_say = new OleDbCommand("SELECT Count(ukıd) FROM tblole where turu='SES SİSTEMİ'", baglanti);
            object sessis = sessis_say.ExecuteScalar();
            OleDbCommand red_say = new OleDbCommand("SELECT Count(red) FROM tblole ", baglanti);
            object red = red_say.ExecuteScalar();
            OleDbCommand green_say = new OleDbCommand("SELECT Count(green) FROM tblole ", baglanti);
            object green = green_say.ExecuteScalar();
            OleDbCommand yellow_say = new OleDbCommand("SELECT Count(yellow) FROM tblole ", baglanti);
            object yellow = yellow_say.ExecuteScalar();
            OleDbCommand blue_say = new OleDbCommand("SELECT Count(blue) FROM tblole ", baglanti);
            object blue = blue_say.ExecuteScalar();
            OleDbCommand verstarihi = new OleDbCommand("SELECT (verstarihi) FROM tblversiyon where versid=1", baglanti);
            object versiyontarihi = verstarihi.ExecuteScalar();
            OleDbCommand versiyonsql = new OleDbCommand("SELECT (versturu) FROM tblversiyon where versid=1", baglanti);
            object versiyon = versiyonsql.ExecuteScalar();
            OleDbCommand vtsahibi1 = new OleDbCommand("SELECT (verssahibi) FROM tblversiyon where versid=1", baglanti);
            object vtsahibi = vtsahibi1.ExecuteScalar();
            baglanti.Close();
            //*****************************************************************************************
            string dosyayolu = Application.StartupPath + "\\resimler\\" ;
            int dosyaSayisi = Directory.GetFiles(dosyayolu, "*.jpg*", SearchOption.AllDirectories).Length;
            //******************************************************************************************
            dataGridView1.Rows.Add("VERİTABANI VERSİYONU :", versiyon);
            dataGridView1.Rows.Add("VERİTABANI SAHİBİ :", vtsahibi);
            dataGridView1.Rows.Add("VERİTABANI TARİHİ :", versiyontarihi);            
            dataGridView1.Rows.Add("TOPLAM KAYIT ADETİ :", id);
            dataGridView1.Rows.Add("RESİM KAYIT ADETİ :", dosyaSayisi);
            dataGridView1.Rows.Add("RECEİVER KUMANDA KAYIT ADETİ :", rec);
            dataGridView1.Rows.Add("TELEVİZYON KUMANDA KAYIT ADETİ :", tv);
            dataGridView1.Rows.Add("KLİMA KUMANDA KAYIT ADETİ :", klima);
            dataGridView1.Rows.Add("DVD KUMANDA KAYIT ADETİ :", dvd);
            dataGridView1.Rows.Add("SES SİTEMİ KUMANDA KAYIT ADETİ :", sessis);
            dataGridView1.Rows.Add("ZAPP KUMANDA KAYIT ADETİ :", zapp);
            dataGridView1.Rows.Add("SWATT KUMANDA KAYIT ADETİ :", swat);
            dataGridView1.Rows.Add("MERTER KUMANDA KAYIT ADETİ :", ozt);
            dataGridView1.Rows.Add("CLASS KUMANDA KAYIT ADETİ :", clas);
            dataGridView1.Rows.Add("SES ARTI KOD KAYIT ADETİ :", sesarti);
            dataGridView1.Rows.Add("SES EKSİ KOD KAYIT ADETİ :", seseksi);
            dataGridView1.Rows.Add("PRG ARTI KOD KAYIT ADETİ :", prgarti);
            dataGridView1.Rows.Add("PRG EKSİ KOD KAYIT ADETİ :", prgeksi);
            dataGridView1.Rows.Add("MENÜ KOD KAYIT ADETİ :", menu);
            dataGridView1.Rows.Add("POWER KOD KAYIT ADETİ :", power);
            dataGridView1.Rows.Add("KIRMIZI KOD KAYIT ADETİ :", red);
            dataGridView1.Rows.Add("YEŞİL KOD KAYIT ADETİ :", green);
            dataGridView1.Rows.Add("SARI KOD KAYIT ADETİ :", yellow);
            dataGridView1.Rows.Add("MAVİ KOD KAYIT ADETİ :", blue);
            dataGridView1.ClearSelection();
            GC.Collect();
            System.GC.SuppressFinalize(this);
        }
    }
}
