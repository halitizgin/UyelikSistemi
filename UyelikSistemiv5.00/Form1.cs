using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace UyelikSistemiv5._00
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=kullanicilar.accdb");
        OleDbCommand komut = new OleDbCommand();
        DataSet ds = new DataSet();
        string gKullanici = "";
        string gSifre = "";
        bool gYonetici = false;
        int gID = 0;
        bool gAs = false;
        DateTime gTarih;

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string kullanici = textEdit1.Text;
            string sifre = textEdit1.Text;

            if (kullanici.Trim() != "" && sifre.Trim() != "")
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                komut.Connection = baglanti;
                komut.CommandText = "SELECT * FROM kullanicilar";
                OleDbDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    if (okuyucu["kullanici"].ToString() == kullanici.Trim() && okuyucu["sifre"].ToString() == sifre.Trim())
                    {
                        string nYasakli = okuyucu["yasakli"].ToString();

                        if (nYasakli == "SÜRESİZ")
                        {
                            MessageBox.Show("Yönetici tarafından süresiz olarak sistemden uzaklaştırıldınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                        }
                        else
                        {
                            if (nYasakli != "")
                            {
                                int yasakli = DateTime.Compare(DateTime.Parse(nYasakli), DateTime.Now);
                                if (yasakli == 1)
                                {
                                    MessageBox.Show("Yönetici tarafından " + nYasakli + " tarihine kadar sistemden uzaklaştırıldınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Application.Exit();
                                }
                                else
                                {
                                    gAs = Convert.ToBoolean(okuyucu["asyonetici"]);
                                    OleDbCommand komut2 = new OleDbCommand("SELECT * FROM ayarlar WHERE id=1", baglanti);
                                    OleDbDataReader okuyucu2 = komut2.ExecuteReader();
                                    bool sistem = true;
                                    while (okuyucu2.Read())
                                    {
                                        sistem = Boolean.Parse(okuyucu2["sistem"].ToString());
                                    }

                                    if (sistem == false)
                                    {

                                        if (gAs == false)
                                        {
                                            MessageBox.Show("Sistem yönetici tarafından kapatılmıştır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                        else
                                        {
                                            gID = Convert.ToInt32(okuyucu["id"]);
                                            gKullanici = kullanici.Trim();
                                            gSifre = sifre.Trim();
                                            gTarih = Convert.ToDateTime(okuyucu["tarih"].ToString());
                                            gYonetici = Convert.ToBoolean(okuyucu["yonetici"].ToString());
                                            MessageBox.Show("Sistem yönetici tarafından kapatılmış, ancak siz yönetici olduğunuz için giriş yapabilirsiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            Form2 form2 = new Form2(gKullanici.Trim(), gSifre.Trim(), gYonetici, gTarih, gID, gAs);
                                            form2.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        gID = Convert.ToInt32(okuyucu["id"]);
                                        gKullanici = kullanici.Trim();
                                        gSifre = sifre.Trim();
                                        gTarih = Convert.ToDateTime(okuyucu["tarih"].ToString());
                                        gYonetici = Convert.ToBoolean(okuyucu["yonetici"].ToString());
                                        MessageBox.Show("Giriş işlemi başarıyla tamamlanmıştır.");
                                        Form2 form2 = new Form2(gKullanici.Trim(), gSifre.Trim(), gYonetici, gTarih, gID, gAs);
                                        form2.ShowDialog();
                                    }
                                }
                            }
                            else
                            {
                                gAs = Convert.ToBoolean(okuyucu["asyonetici"]);
                                OleDbCommand komut2 = new OleDbCommand("SELECT * FROM ayarlar WHERE id=1", baglanti);
                                OleDbDataReader okuyucu2 = komut2.ExecuteReader();
                                bool sistem = true;
                                while (okuyucu2.Read())
                                {
                                    sistem = Boolean.Parse(okuyucu2["sistem"].ToString());
                                }

                                if (sistem == false)
                                {

                                    if (gAs == false)
                                    {
                                        MessageBox.Show("Sistem yönetici tarafından kapatılmıştır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                    {
                                        gID = Convert.ToInt32(okuyucu["id"]);
                                        gKullanici = kullanici.Trim();
                                        gSifre = sifre.Trim();
                                        gTarih = Convert.ToDateTime(okuyucu["tarih"].ToString());
                                        gYonetici = Convert.ToBoolean(okuyucu["yonetici"].ToString());
                                        MessageBox.Show("Sistem yönetici tarafından kapatılmış, ancak siz yönetici olduğunuz için giriş yapabilirsiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Form2 form2 = new Form2(gKullanici.Trim(), gSifre.Trim(), gYonetici, gTarih, gID, gAs);
                                        form2.ShowDialog();
                                    }
                                }
                                else
                                {
                                    gID = Convert.ToInt32(okuyucu["id"]);
                                    gKullanici = kullanici.Trim();
                                    gSifre = sifre.Trim();
                                    gTarih = Convert.ToDateTime(okuyucu["tarih"].ToString());
                                    gYonetici = Convert.ToBoolean(okuyucu["yonetici"].ToString());
                                    MessageBox.Show("Giriş işlemi başarıyla tamamlanmıştır.");
                                    Form2 form2 = new Form2(gKullanici.Trim(), gSifre.Trim(), gYonetici, gTarih, gID, gAs);
                                    form2.ShowDialog();
                                }
                            }
                        }
                    }
                }
                baglanti.Close();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            bool hata = false;
            string kullanici = textEdit4.Text.Trim();
            string sifre = textEdit3.Text.Trim();
            if (kullanici.Trim() != "" && sifre.Trim() != "")
            {
                OleDbCommand komut2 = new OleDbCommand("SELECT * FROM kullanicilar", baglanti);
                OleDbDataReader okuyucu = komut2.ExecuteReader();
                while (okuyucu.Read())
                {
                    if (okuyucu["kullanici"].ToString() == kullanici.Trim())
                    {
                        hata = true;
                    }
                }

                if (hata == true)
                {
                    MessageBox.Show("Belirttiğiniz kullanıcı adında zaten üye mevcuttur.\nLütfen başka bir kullanıcı adı seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    hata = false;
                }
                else
                {
                    DateTime tarih = DateTime.Now;
                    komut.Connection = baglanti;
                    komut.CommandText = "INSERT INTO kullanicilar (kullanici, sifre, tarih, sonaktif) VALUES ('" + kullanici + "', '" + sifre + "', '" + tarih + "', '" + tarih + "')";
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                    baglanti.Close();
                    MessageBox.Show("Başarıyla üye oldunuz.\nKullanıcı Adı: " + kullanici + "\nŞifre: " + sifre + "\nKayıt Tarihi: " + tarih.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bool cUyelik = true;
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "SELECT * FROM ayarlar";
            OleDbDataReader okuyucu = komut.ExecuteReader();
            while (okuyucu.Read())
            {
                this.Text = okuyucu["baslik"].ToString();
                cUyelik = Boolean.Parse(okuyucu["uyelik"].ToString());
            }
            baglanti.Close();

            if (cUyelik == false)
            {
                labelControl3.Enabled = false;
                labelControl4.Enabled = false;
                textEdit4.Enabled = false;
                textEdit3.Enabled = false;
                simpleButton2.Enabled = false;
                MessageBox.Show("Üyelikler yönetici tarafından kapatılmıştır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }
    }
}
