using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.OleDb;

namespace UyelikSistemiv5._00
{
    public partial class Form2 : DevExpress.XtraEditors.XtraForm
    {
        public Form2(string nKullanici, string nSifre, bool nYonetici, DateTime nTarih, int nID, bool nAs)
        {
            InitializeComponent();
            gID = nID;
            gKullanici = nKullanici;
            gSifre = nSifre;
            gYonetici = nYonetici;
            gTarih = nTarih;
            gAs = nAs;
        }

        public Form1 form1 = new Form1();
        public int gID;
        public string gKullanici;
        public string gSifre;
        public bool gYonetici;
        public bool gAs;
        public DateTime gTarih;
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=kullanicilar.accdb");
        OleDbCommand komut = new OleDbCommand();
        DataSet ds = new DataSet();

        public void listele()
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            komut.Connection = baglanti;
            komut.CommandText = "SELECT * FROM kullanicilar";
            OleDbDataReader okuyucu = komut.ExecuteReader();

            listView1.Columns.Add("ID");
            listView1.Columns.Add("Kullanıcı Adı");
            listView1.Columns.Add("Şifre");
            listView1.Columns.Add("Yönetici");
            listView1.Columns.Add("Tarih");
            listView1.Columns.Add("AsYönetici");
            listView1.Columns.Add("Doku");
            listView1.Columns.Add("Aktiflik");
            listView1.Columns.Add("Yasak");

            int count;
            while (okuyucu.Read())
            {
                count = listView1.Items.Count;
                if (int.Parse(okuyucu["id"].ToString()) == gID)
                {
                    listView1.Items.Add(okuyucu["id"].ToString());
                    listView1.Items[count].SubItems.Add(okuyucu["kullanici"].ToString());
                    listView1.Items[count].SubItems.Add(okuyucu["sifre"].ToString());
                    string nYonetici = Cevir(Convert.ToBoolean(okuyucu["yonetici"].ToString()), "Var", "Yok");
                    listView1.Items[count].SubItems.Add(nYonetici);
                    listView1.Items[count].SubItems.Add(okuyucu["tarih"].ToString());
                    string nAs = Cevir(Convert.ToBoolean(okuyucu["asyonetici"].ToString()), "Var", "Yok");
                    listView1.Items[count].SubItems.Add(nAs);
                    string nDoku = Cevir(Convert.ToBoolean(okuyucu["doku"].ToString()), "Var", "Yok");
                    listView1.Items[count].SubItems.Add(nDoku);
                    string aktifti = "";
                    if (okuyucu["sonaktif"].ToString() == "Şuan Aktif")
                    {
                        aktifti = okuyucu["sonaktif"].ToString();
                    }
                    else if (okuyucu["sonaktif"].ToString() == "")
                    {

                    }
                    else
                    {
                        TimeSpan sonaktif = DateTime.Now - Convert.ToDateTime(okuyucu["sonaktif"].ToString());
                        if (sonaktif.Days == 0)
                        {
                            if (sonaktif.Hours == 0)
                            {
                                if (sonaktif.Minutes == 0)
                                {
                                    if (sonaktif.Seconds == 0)
                                    {
                                        aktifti = "?";
                                    }
                                    else
                                    {
                                        aktifti = sonaktif.Seconds + " saniye önce aktifti";
                                    }
                                }
                                else
                                {
                                    aktifti = sonaktif.Minutes + " dakika, " + sonaktif.Seconds + " saniye önce aktifti";
                                }
                            }
                            else
                            {
                                aktifti = sonaktif.Hours + " saat, " + sonaktif.Minutes + " dakika, " + sonaktif.Seconds + " saniye önce aktifti";
                            }

                        }
                        else
                        {
                            aktifti = sonaktif.Days + " gün, " + sonaktif.Hours + " saat, " + sonaktif.Minutes + " dakika, " + sonaktif.Seconds + " saniye önce aktifti";
                        }
                    }
                    listView1.Items[count].SubItems.Add(aktifti);
                    listView1.Items[count].SubItems.Add(okuyucu["yasakli"].ToString());
                }
                else if (Boolean.Parse(okuyucu["asyonetici"].ToString()) == true)
                {
                    listView1.Items.Add(okuyucu["id"].ToString());
                    listView1.Items[count].SubItems.Add(okuyucu["kullanici"].ToString());
                    listView1.Items[count].SubItems.Add("*****");
                    string nYonetici = Cevir(Convert.ToBoolean(okuyucu["yonetici"].ToString()), "Var", "Yok");
                    listView1.Items[count].SubItems.Add(nYonetici);
                    listView1.Items[count].SubItems.Add(okuyucu["tarih"].ToString());
                    string nAs = Cevir(Convert.ToBoolean(okuyucu["asyonetici"].ToString()), "Var", "Yok");
                    listView1.Items[count].SubItems.Add(nAs);
                    string nDoku = Cevir(Convert.ToBoolean(okuyucu["doku"].ToString()), "Var", "Yok");
                    listView1.Items[count].SubItems.Add(nDoku);
                    string aktifti = "";
                    if (okuyucu["sonaktif"].ToString() == "Şuan Aktif")
                    {
                        aktifti = okuyucu["sonaktif"].ToString();
                    }
                    else
                    {
                        TimeSpan sonaktif = DateTime.Now - Convert.ToDateTime(okuyucu["sonaktif"].ToString());
                        if (sonaktif.Days == 0)
                        {
                            if (sonaktif.Hours == 0)
                            {
                                if (sonaktif.Minutes == 0)
                                {
                                    if (sonaktif.Seconds == 0)
                                    {
                                        aktifti = "?";
                                    }
                                    else
                                    {
                                        aktifti = sonaktif.Seconds + " saniye önce aktifti";
                                    }
                                }
                                else
                                {
                                    aktifti = sonaktif.Minutes + " dakika, " + sonaktif.Seconds + " saniye önce aktifti";
                                }
                            }
                            else
                            {
                                aktifti = sonaktif.Hours + " saat, " + sonaktif.Minutes + " dakika, " + sonaktif.Seconds + " saniye önce aktifti";
                            }

                        }
                        else
                        {
                            aktifti = sonaktif.Days + " gün, " + sonaktif.Hours + " saat, " + sonaktif.Minutes + " dakika, " + sonaktif.Seconds + " saniye önce aktifti";
                        }
                    }
                    listView1.Items[count].SubItems.Add(aktifti);
                    listView1.Items[count].SubItems.Add(okuyucu["yasakli"].ToString());
                }
                else
                {
                    listView1.Items.Add(okuyucu["id"].ToString());
                    listView1.Items[count].SubItems.Add(okuyucu["kullanici"].ToString());
                    listView1.Items[count].SubItems.Add(okuyucu["sifre"].ToString());
                    string nYonetici = Cevir(Convert.ToBoolean(okuyucu["yonetici"].ToString()), "Var", "Yok");
                    listView1.Items[count].SubItems.Add(nYonetici);
                    listView1.Items[count].SubItems.Add(okuyucu["tarih"].ToString());
                    string nAs = Cevir(Convert.ToBoolean(okuyucu["asyonetici"].ToString()), "Var", "Yok");
                    listView1.Items[count].SubItems.Add(nAs);
                    string nDoku = Cevir(Convert.ToBoolean(okuyucu["doku"].ToString()), "Var", "Yok");
                    listView1.Items[count].SubItems.Add(nDoku);
                    string aktifti = "";
                    if (okuyucu["sonaktif"].ToString() == "Şuan Aktif")
                    {
                        aktifti = okuyucu["sonaktif"].ToString();
                    }
                    {
                        TimeSpan sonaktif = DateTime.Now - Convert.ToDateTime(okuyucu["sonaktif"].ToString());
                        if (sonaktif.Days == 0)
                        {
                            if (sonaktif.Hours == 0)
                            {
                                if (sonaktif.Minutes == 0)
                                {
                                    if (sonaktif.Seconds == 0)
                                    {
                                        aktifti = "?";
                                    }
                                    else
                                    {
                                        aktifti = sonaktif.Seconds + " saniye önce aktifti";
                                    }
                                }
                                else
                                {
                                    aktifti = sonaktif.Minutes + " dakika, " + sonaktif.Seconds + " saniye önce aktifti";
                                }
                            }
                            else
                            {
                                aktifti = sonaktif.Hours + " saat, " + sonaktif.Minutes + " dakika, " + sonaktif.Seconds + " saniye önce aktifti";
                            }

                        }
                        else
                        {
                            aktifti = sonaktif.Days + " gün, " + sonaktif.Hours + " saat, " + sonaktif.Minutes + " dakika, " + sonaktif.Seconds + " saniye önce aktifti";
                        }
                    }
                    listView1.Items[count].SubItems.Add(aktifti);
                    listView1.Items[count].SubItems.Add(okuyucu["yasakli"].ToString());
                }
            }
            baglanti.Close();
        }

        static bool tCevir(string cevirilecek, string olumlu, string olumsuz)
        {
            bool sonuc = false;
            if (cevirilecek == olumlu)
            {
                sonuc = true;
            }
            else if (cevirilecek == olumsuz)
            {
                sonuc = false;
            }

            return sonuc;
        }

        static string Cevir(bool cevirilecek, string olumlu, string olumsuz)
        {
            string sonuc = "";
            if (cevirilecek == true)
            {
                sonuc = olumlu;
            }
            else if (cevirilecek == false)
            {
                sonuc = olumsuz;
            }

            return sonuc;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }

            komut.Connection = baglanti;
            komut.CommandText = "UPDATE kullanicilar SET sonaktif='Şuan Aktif' WHERE id=@id";
            komut.Parameters.AddWithValue("@id", gID);
            komut.ExecuteNonQuery();
            komut.Dispose();

            if (gYonetici == true)
            {
                listView1.FullRowSelect = true;
                label1.Text = "Hoşgeldiniz " + gKullanici + "(Yönetici)";
                this.Text = gKullanici + "(Yönetici)";
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                textBox1.Visible = true;
                comboBox1.Visible = true;
                comboBox2.Visible = true;
                listView1.Visible = true;
                button8.Visible = true;
                listele();
            }
            else
            {
                label1.Text = "Hoşgeldiniz " + gKullanici + "(Kullanıcı)";
                this.Text = gKullanici + "(Kullanıcı)";
                listView1.FullRowSelect = true;
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                else
                {
                    baglanti.Close();
                    baglanti.Open();
                }
                komut.Connection = baglanti;
                komut.CommandText = "SELECT * FROM kullanicilar";
                OleDbDataReader okuyucu = komut.ExecuteReader();

                listView1.Columns.Add("Kullanıcı Adı");
                listView1.Columns.Add("Aktiflik");

                int count = 0;
                while (okuyucu.Read())
                {
                    if (int.Parse(okuyucu["id"].ToString()) == gID)
                    {

                    }
                    else
                    {
                        count = listView1.Items.Count;
                        listView1.Visible = true;
                        listView1.Items.Add(okuyucu["kullanici"].ToString());
                        string aktifti = "";
                        if (okuyucu["sonaktif"].ToString() == "Şuan Aktif")
                        {
                            aktifti = okuyucu["sonaktif"].ToString();
                        }
                        else
                        {
                            TimeSpan sonaktif = DateTime.Now - Convert.ToDateTime(okuyucu["sonaktif"].ToString());
                            if (sonaktif.Days == 0)
                            {
                                if (sonaktif.Hours == 0)
                                {
                                    if (sonaktif.Minutes == 0)
                                    {
                                        if (sonaktif.Seconds == 0)
                                        {
                                            aktifti = "?";
                                        }
                                        else
                                        {
                                            aktifti = sonaktif.Seconds + " saniye önce aktifti";
                                        }
                                    }
                                    else
                                    {
                                        aktifti = sonaktif.Minutes + " dakika önce aktifti";
                                    }
                                }
                                else
                                {
                                    aktifti = sonaktif.Hours + " saat önce aktifti";
                                }

                            }
                            else
                            {
                                aktifti = sonaktif.Days + " gün önce aktifti";
                            }
                        }
                        listView1.Items[count].SubItems.Add(aktifti);
                    }
                }
            }

            baglanti.Close();
            baglanti.Open();

            OleDbCommand komut2 = new OleDbCommand("SELECT * FROM ayarlar", baglanti);
            OleDbDataReader okuyucu2 = komut2.ExecuteReader();
            string nBaslik = "";
            bool nUyelik = true;
            bool nSistem = true;

            while (okuyucu2.Read())
            {
                nBaslik = okuyucu2["baslik"].ToString();
                nUyelik = Boolean.Parse(okuyucu2["uyelik"].ToString());
                nSistem = Boolean.Parse(okuyucu2["sistem"].ToString());
            }

            textBox1.Text = nBaslik;
            comboBox1.Text = Cevir(nUyelik, "Açık", "Kapalı");
            comboBox2.Text = Cevir(nSistem, "Açık", "Kapalı");

            baglanti.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string nBaslik = textBox1.Text.Trim();
            bool nUyelik = true;
            bool nSistem = true;
            if (comboBox1.Text == "Açık")
            {
                nUyelik = true;
            }
            else if (comboBox1.Text == "Kapalı")
            {
                nUyelik = false;
            }

            if (comboBox2.Text == "Açık")
            {
                nSistem = true;
            }
            else if (comboBox2.Text == "Kapalı")
            {
                nSistem = false;
            }

            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "UPDATE ayarlar SET baslik='" + nBaslik + "', uyelik=" + nUyelik + ", sistem=" + nSistem + " WHERE id=1";
            komut.ExecuteNonQuery();
            komut.Dispose();
            baglanti.Close();
            MessageBox.Show("Değişiklikler kaydedildi.\nDeğişiklikler program kapanıp açıldığında etkin olacaktır.");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (gYonetici == true)
            {
                listView1.Clear();
                listView1.FullRowSelect = true;
                label1.Text = "Hoşgeldiniz " + gKullanici + "(Yönetici)";
                this.Text = gKullanici + "(Yönetici)";
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                textBox1.Visible = true;
                comboBox1.Visible = true;
                comboBox2.Visible = true;
                listView1.Visible = true;
                listele();

                baglanti.Open();

                OleDbCommand komut2 = new OleDbCommand("SELECT * FROM ayarlar", baglanti);
                OleDbDataReader okuyucu2 = komut2.ExecuteReader();
                string nBaslik = "";
                bool nUyelik = true;
                bool nSistem = true;

                while (okuyucu2.Read())
                {
                    nBaslik = okuyucu2["baslik"].ToString();
                    nUyelik = Boolean.Parse(okuyucu2["uyelik"].ToString());
                    nSistem = Boolean.Parse(okuyucu2["sistem"].ToString());
                }

                textBox1.Text = nBaslik;
                comboBox1.Text = Cevir(nUyelik, "Açık", "Kapalı");
                comboBox2.Text = Cevir(nSistem, "Açık", "Kapalı");

                baglanti.Close();

            }
            else if (gYonetici == false)
            {
                listView1.Clear();
                label1.Text = "Hoşgeldiniz " + gKullanici + "(Kullanıcı)";
                this.Text = gKullanici + "(Kullanıcı)";
                listView1.FullRowSelect = true;
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                else
                {
                    baglanti.Close();
                    baglanti.Open();
                }
                komut.Connection = baglanti;
                komut.CommandText = "SELECT * FROM kullanicilar";
                OleDbDataReader okuyucu = komut.ExecuteReader();

                listView1.Columns.Add("Kullanıcı Adı");
                listView1.Columns.Add("Aktiflik");

                int count = 0;
                while (okuyucu.Read())
                {
                    count = listView1.Items.Count;
                    listView1.Visible = true;
                    listView1.Items.Add(okuyucu["kullanici"].ToString());
                    string aktifti = "";
                    if (okuyucu["sonaktif"].ToString() == "Şuan Aktif")
                    {
                        aktifti = okuyucu["sonaktif"].ToString();
                    }
                    else
                    {
                        TimeSpan sonaktif = DateTime.Now - Convert.ToDateTime(okuyucu["sonaktif"].ToString());
                        if (sonaktif.Days == 0)
                        {
                            if (sonaktif.Hours == 0)
                            {
                                if (sonaktif.Minutes == 0)
                                {
                                    if (sonaktif.Seconds == 0)
                                    {
                                        aktifti = "?";
                                    }
                                    else
                                    {
                                        aktifti = sonaktif.Seconds + " saniye önce aktifti";
                                    }
                                }
                                else
                                {
                                    aktifti = sonaktif.Minutes + " dakika önce aktifti";
                                }
                            }
                            else
                            {
                                aktifti = sonaktif.Hours + " saat önce aktifti";
                            }

                        }
                        else
                        {
                            aktifti = sonaktif.Days + " gün önce aktifti";
                        }
                    }
                    listView1.Items[count].SubItems.Add(aktifti);
                }
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool hata = false;
            string kullanici = Microsoft.VisualBasic.Interaction.InputBox("Kullanıcı Adı: ", "Ekle", "", 50, 50);
            if (kullanici.Trim() != "")
            {
                foreach (ListViewItem item in listView1.Items)
                {
                    if (kullanici.Trim() == item.SubItems[1].Text.Trim())
                    {
                        hata = true;
                    }
                }
                if (hata == true)
                {
                    MessageBox.Show("Bu kullanıcı adında zaten bir kullanıcı mevcuttur.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    hata = false;
                }
                else
                {
                    string sifre = Microsoft.VisualBasic.Interaction.InputBox("Şifre: ", "Ekle", "", 50, 50);
                    if (sifre.Trim() != "")
                    {
                        DateTime tarih = DateTime.Now;
                        if (baglanti.State == ConnectionState.Closed)
                        {
                            baglanti.Open();
                        }
                        else
                        {
                            baglanti.Close();
                            baglanti.Open();
                        }
                        komut.Connection = baglanti;
                        komut.CommandText = "INSERT INTO kullanicilar (kullanici, sifre, tarih, sonaktif) VALUES ('" + kullanici.Trim() + "', '" + sifre.Trim() + "', '" + tarih + "', '" + tarih + "')";
                        komut.ExecuteNonQuery();
                        komut.Dispose();
                        baglanti.Close();
                        MessageBox.Show("Kullanıcı ekleme işlemi başarıyla gerçekleşmiştir.\nKullanıcı Adı: " + kullanici.Trim() + "\nŞifre: " + sifre.Trim() + "\nKayıt Tarihi: " + tarih.ToString());
                        listView1.Items.Clear();
                        listView1.Columns.Clear();
                        listele();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                DialogResult soru = MessageBox.Show(listView1.SelectedItems.Count.ToString() + " adet veri seçilmiştir.\nSilmek istediğinize emin misiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (soru == DialogResult.Yes)
                {
                    baglanti.Open();
                    foreach (ListViewItem item in listView1.SelectedItems)
                    {
                        int sID = int.Parse(item.Text);
                        bool asyonetici = tCevir(item.SubItems[5].Text.ToString(), "Var", "Yok");
                        bool doku = tCevir(item.SubItems[6].Text.ToString(), "Var", "Yok");
                        if (gID == sID)
                        {
                            MessageBox.Show("Silmeye çalıştığınız kullanıcı sizsiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (asyonetici == true)
                        {
                            MessageBox.Show("Silmeye çalıştığınız kullanıcı as yöneticidir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (doku == true)
                        {
                            MessageBox.Show("Silmeye çalıştığınız kullanıcı dokunulmazdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            komut.Connection = baglanti;
                            komut.CommandText = "DELETE FROM kullanicilar WHERE id=@id";
                            komut.Parameters.AddWithValue("@id", sID);
                            komut.ExecuteNonQuery();
                            komut.Dispose();
                        }
                    }
                    baglanti.Close();
                    listView1.Items.Clear();
                    listView1.Columns.Clear();
                    listele();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool hata = false;
            if (listView1.SelectedItems.Count == 1)
            {
                bool asyonetici = tCevir(listView1.Items[listView1.SelectedItems[0].Index].SubItems[5].Text, "Var", "Yok");
                bool doku = tCevir(listView1.Items[listView1.SelectedItems[0].Index].SubItems[6].Text, "Var", "Yok");
                int id = int.Parse(listView1.Items[listView1.SelectedItems[0].Index].Text);
                if (id == gID)
                {
                    int cID = int.Parse(listView1.Items[listView1.SelectedItems[0].Index].Text);
                    string cKullanici = listView1.Items[listView1.SelectedItems[0].Index].SubItems[1].Text;
                    string cSifre = listView1.Items[listView1.SelectedItems[0].Index].SubItems[2].Text;
                    string sKullanici = Microsoft.VisualBasic.Interaction.InputBox("Kullanıcı Adı: ", "Düzenle", cKullanici, 50, 50);
                    if (sKullanici.Trim() != "")
                    {
                        foreach (ListViewItem item in listView1.Items)
                        {
                            if (sKullanici.Trim() == item.SubItems[1].Text.Trim())
                            {
                                hata = true;
                            }
                        }
                        if (hata == true)
                        {
                            MessageBox.Show("Bu kullanıcı adında zaten bir kullanıcı mevcuttur.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            hata = false;
                        }
                        else
                        {
                            string sSifre = Microsoft.VisualBasic.Interaction.InputBox("Şifre: ", "Düzenle", cSifre, 50, 50);
                            if (sSifre.Trim() != "")
                            {
                                DateTime cTarih = DateTime.Now;
                                baglanti.Open();
                                komut.Connection = baglanti;
                                komut.CommandText = "UPDATE kullanicilar SET kullanici='" + sKullanici + "', sifre='" + sSifre + "', tarih='" + cTarih + "' WHERE id=@id";
                                komut.Parameters.AddWithValue("@id", cID);
                                komut.ExecuteNonQuery();
                                komut.Dispose();
                                baglanti.Close();
                                listView1.Items.Clear();
                                listView1.Columns.Clear();
                                listele();
                            }
                        }
                    }
                }
                else if (asyonetici == true)
                {
                    MessageBox.Show("Düzenlemeye çalıştığınız kullanıcı as yöneticidir..", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (doku == true)
                {
                    MessageBox.Show("Silmeye çalıştığınız kullanıcı dokunulmazdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int cID = int.Parse(listView1.Items[listView1.SelectedItems[0].Index].Text);
                    string cKullanici = listView1.Items[listView1.SelectedItems[0].Index].SubItems[1].Text;
                    string cSifre = listView1.Items[listView1.SelectedItems[0].Index].SubItems[2].Text;
                    string sKullanici = Microsoft.VisualBasic.Interaction.InputBox("Kullanıcı Adı: ", "Düzenle", cKullanici, 50, 50);
                    if (sKullanici.Trim() != "")
                    {
                        foreach (ListViewItem item in listView1.Items)
                        {
                            if (sKullanici.Trim() == item.SubItems[1].Text.Trim())
                            {
                                hata = true;
                            }
                        }
                        if (hata == true)
                        {
                            MessageBox.Show("Bu kullanıcı adında zaten bir kullanıcı mevcuttur.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            hata = false;
                        }
                        else
                        {
                            string sSifre = Microsoft.VisualBasic.Interaction.InputBox("Şifre: ", "Düzenle", cSifre, 50, 50);
                            if (sSifre.Trim() != "")
                            {
                                DateTime cTarih = DateTime.Now;
                                baglanti.Open();
                                komut.Connection = baglanti;
                                komut.CommandText = "UPDATE kullanicilar SET kullanici='" + sKullanici + "', sifre='" + sSifre + "', tarih='" + cTarih + "' WHERE id=@id";
                                komut.Parameters.AddWithValue("@id", cID);
                                komut.ExecuteNonQuery();
                                komut.Dispose();
                                baglanti.Close();
                                listView1.Items.Clear();
                                listView1.Columns.Clear();
                                listele();
                            }
                        }
                    }
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                baglanti.Open();
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    int cID = Convert.ToInt32(item.Text);
                    if (gID == cID)
                    {
                        MessageBox.Show("Kendinizi yöneticilikten atamazsınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        bool cYonetici = tCevir(item.SubItems[3].Text, "Var", "Yok");
                        if (cYonetici == true)
                        {
                            bool asyonetici = tCevir(item.SubItems[5].Text, "Var", "Yok");
                            bool doku = tCevir(item.SubItems[6].Text, "Var", "Yok");
                            if (asyonetici == true)
                            {
                                MessageBox.Show("As yöneticilerin yöneticilik yetkisini alamazsınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                komut.Connection = baglanti;
                                komut.CommandText = "UPDATE kullanicilar SET yonetici=false WHERE id=@id";
                                komut.Parameters.AddWithValue("@id", cID);
                                komut.ExecuteNonQuery();
                                komut.Dispose();
                            }
                        }
                        else
                        {
                            komut.Connection = baglanti;
                            komut.CommandText = "UPDATE kullanicilar SET yonetici=true WHERE id=@id";
                            komut.Parameters.AddWithValue("@id", cID);
                            komut.ExecuteNonQuery();
                            komut.Dispose();
                        }
                    }
                }
                listView1.Items.Clear();
                listView1.Columns.Clear();
                listele();
                baglanti.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (gAs == false)
                {
                    MessageBox.Show("Sadece as yöneticiler doku verme ve alma işlemleri yapabilir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    baglanti.Open();
                    foreach (ListViewItem item in listView1.SelectedItems)
                    {
                        int cID = Convert.ToInt32(item.Text);
                        if (gID == cID)
                        {
                            MessageBox.Show("Kendinizi dokunulmazlıktan atamazsınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {

                            bool cDoku = tCevir(item.SubItems[6].Text, "Var", "Yok");
                            if (cDoku == true)
                            {
                                bool asyonetici = tCevir(item.SubItems[5].Text, "Var", "Yok");
                                bool doku = tCevir(item.SubItems[6].Text, "Var", "Yok");
                                if (asyonetici == true)
                                {
                                    MessageBox.Show("As yönetici olmadığınız için doku veremez veya almazsınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    komut.Connection = baglanti;
                                    komut.CommandText = "UPDATE kullanicilar SET doku=false WHERE id=@id";
                                    komut.Parameters.AddWithValue("@id", cID);
                                    komut.ExecuteNonQuery();
                                    komut.Dispose();
                                }
                            }
                            else
                            {
                                komut.Connection = baglanti;
                                komut.CommandText = "UPDATE kullanicilar SET doku=true WHERE id=@id";
                                komut.Parameters.AddWithValue("@id", cID);
                                komut.ExecuteNonQuery();
                                komut.Dispose();
                            }
                        }
                    }
                    listView1.Items.Clear();
                    listView1.Columns.Clear();
                    listele();
                    baglanti.Close();
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0].SubItems[8].Text == "")
            {
                if (listView1.SelectedItems[0].SubItems[6].Text == "Yok" && listView1.SelectedItems[0].SubItems[3].Text == "Yok" && listView1.SelectedItems[0].SubItems[5].Text == "Yok")
                {
                    if (listView1.SelectedItems.Count == 1)
                    {
                        int id = int.Parse(listView1.SelectedItems[0].Text);
                        string kullanici = listView1.SelectedItems[0].SubItems[1].Text;
                        Form3 form3 = new Form3(id, kullanici);
                        form3.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Bu kullanıcıyı yasaklayamazsınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                DialogResult soru = MessageBox.Show(listView1.SelectedItems[0].SubItems[1].Text + " adlı kullanıcının yasağını kaldırmak istediğinize emin misiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (soru == DialogResult.Yes)
                {
                    baglanti.Open();
                    komut.Connection = baglanti;
                    komut.CommandText = "UPDATE kullanicilar SET yasakli='' WHERE id=@id";
                    komut.Parameters.AddWithValue("@id", int.Parse(listView1.SelectedItems[0].Text));
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                    baglanti.Close();
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            bool hata = false;
            string kullanici = Microsoft.VisualBasic.Interaction.InputBox("Kullanıcı Adı: ", "Ekle", "", 50, 50);
            if (kullanici.Trim() != "")
            {
                foreach (ListViewItem item in listView1.Items)
                {
                    if (kullanici.Trim() == item.SubItems[1].Text.Trim())
                    {
                        hata = true;
                    }
                }
                if (hata == true)
                {
                    MessageBox.Show("Bu kullanıcı adında zaten bir kullanıcı mevcuttur.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    hata = false;
                }
                else
                {
                    string sifre = Microsoft.VisualBasic.Interaction.InputBox("Şifre: ", "Ekle", "", 50, 50);
                    if (sifre.Trim() != "")
                    {
                        DateTime tarih = DateTime.Now;
                        if (baglanti.State == ConnectionState.Closed)
                        {
                            baglanti.Open();
                        }
                        else
                        {
                            baglanti.Close();
                            baglanti.Open();
                        }
                        komut.Connection = baglanti;
                        komut.CommandText = "INSERT INTO kullanicilar (kullanici, sifre, tarih, sonaktif) VALUES ('" + kullanici.Trim() + "', '" + sifre.Trim() + "', '" + tarih + "', '" + tarih + "')";
                        komut.ExecuteNonQuery();
                        komut.Dispose();
                        baglanti.Close();
                        MessageBox.Show("Kullanıcı ekleme işlemi başarıyla gerçekleşmiştir.\nKullanıcı Adı: " + kullanici.Trim() + "\nŞifre: " + sifre.Trim() + "\nKayıt Tarihi: " + tarih.ToString());
                        listView1.Items.Clear();
                        listView1.Columns.Clear();
                        listele();
                    }
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                DialogResult soru = MessageBox.Show(listView1.SelectedItems.Count.ToString() + " adet veri seçilmiştir.\nSilmek istediğinize emin misiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (soru == DialogResult.Yes)
                {
                    baglanti.Open();
                    foreach (ListViewItem item in listView1.SelectedItems)
                    {
                        int sID = int.Parse(item.Text);
                        bool asyonetici = tCevir(item.SubItems[5].Text.ToString(), "Var", "Yok");
                        bool doku = tCevir(item.SubItems[6].Text.ToString(), "Var", "Yok");
                        if (gID == sID)
                        {
                            MessageBox.Show("Silmeye çalıştığınız kullanıcı sizsiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (asyonetici == true)
                        {
                            MessageBox.Show("Silmeye çalıştığınız kullanıcı as yöneticidir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (doku == true)
                        {
                            MessageBox.Show("Silmeye çalıştığınız kullanıcı dokunulmazdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            komut.Connection = baglanti;
                            komut.CommandText = "DELETE FROM kullanicilar WHERE id=@id";
                            komut.Parameters.AddWithValue("@id", sID);
                            komut.ExecuteNonQuery();
                            komut.Dispose();
                        }
                    }
                    baglanti.Close();
                    listView1.Items.Clear();
                    listView1.Columns.Clear();
                    listele();
                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            bool hata = false;
            if (listView1.SelectedItems.Count == 1)
            {
                bool asyonetici = tCevir(listView1.Items[listView1.SelectedItems[0].Index].SubItems[5].Text, "Var", "Yok");
                bool doku = tCevir(listView1.Items[listView1.SelectedItems[0].Index].SubItems[6].Text, "Var", "Yok");
                int id = int.Parse(listView1.Items[listView1.SelectedItems[0].Index].Text);
                if (id == gID)
                {
                    int cID = int.Parse(listView1.Items[listView1.SelectedItems[0].Index].Text);
                    string cKullanici = listView1.Items[listView1.SelectedItems[0].Index].SubItems[1].Text;
                    string cSifre = listView1.Items[listView1.SelectedItems[0].Index].SubItems[2].Text;
                    string sKullanici = Microsoft.VisualBasic.Interaction.InputBox("Kullanıcı Adı: ", "Düzenle", cKullanici, 50, 50);
                    if (sKullanici.Trim() != "")
                    {
                        foreach (ListViewItem item in listView1.Items)
                        {
                            if (sKullanici.Trim() == item.SubItems[1].Text.Trim())
                            {
                                hata = true;
                            }
                        }
                        if (hata == true)
                        {
                            MessageBox.Show("Bu kullanıcı adında zaten bir kullanıcı mevcuttur.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            hata = false;
                        }
                        else
                        {
                            string sSifre = Microsoft.VisualBasic.Interaction.InputBox("Şifre: ", "Düzenle", cSifre, 50, 50);
                            if (sSifre.Trim() != "")
                            {
                                DateTime cTarih = DateTime.Now;
                                baglanti.Open();
                                komut.Connection = baglanti;
                                komut.CommandText = "UPDATE kullanicilar SET kullanici='" + sKullanici + "', sifre='" + sSifre + "', tarih='" + cTarih + "' WHERE id=@id";
                                komut.Parameters.AddWithValue("@id", cID);
                                komut.ExecuteNonQuery();
                                komut.Dispose();
                                baglanti.Close();
                                listView1.Items.Clear();
                                listView1.Columns.Clear();
                                listele();
                            }
                        }
                    }
                }
                else if (asyonetici == true)
                {
                    MessageBox.Show("Düzenlemeye çalıştığınız kullanıcı as yöneticidir..", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (doku == true)
                {
                    MessageBox.Show("Silmeye çalıştığınız kullanıcı dokunulmazdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int cID = int.Parse(listView1.Items[listView1.SelectedItems[0].Index].Text);
                    string cKullanici = listView1.Items[listView1.SelectedItems[0].Index].SubItems[1].Text;
                    string cSifre = listView1.Items[listView1.SelectedItems[0].Index].SubItems[2].Text;
                    string sKullanici = Microsoft.VisualBasic.Interaction.InputBox("Kullanıcı Adı: ", "Düzenle", cKullanici, 50, 50);
                    if (sKullanici.Trim() != "")
                    {
                        foreach (ListViewItem item in listView1.Items)
                        {
                            if (sKullanici.Trim() == item.SubItems[1].Text.Trim())
                            {
                                hata = true;
                            }
                        }
                        if (hata == true)
                        {
                            MessageBox.Show("Bu kullanıcı adında zaten bir kullanıcı mevcuttur.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            hata = false;
                        }
                        else
                        {
                            string sSifre = Microsoft.VisualBasic.Interaction.InputBox("Şifre: ", "Düzenle", cSifre, 50, 50);
                            if (sSifre.Trim() != "")
                            {
                                DateTime cTarih = DateTime.Now;
                                baglanti.Open();
                                komut.Connection = baglanti;
                                komut.CommandText = "UPDATE kullanicilar SET kullanici='" + sKullanici + "', sifre='" + sSifre + "', tarih='" + cTarih + "' WHERE id=@id";
                                komut.Parameters.AddWithValue("@id", cID);
                                komut.ExecuteNonQuery();
                                komut.Dispose();
                                baglanti.Close();
                                listView1.Items.Clear();
                                listView1.Columns.Clear();
                                listele();
                            }
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                baglanti.Open();
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    int cID = Convert.ToInt32(item.Text);
                    if (gID == cID)
                    {
                        MessageBox.Show("Kendinizi yöneticilikten atamazsınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        bool cYonetici = tCevir(item.SubItems[3].Text, "Var", "Yok");
                        if (cYonetici == true)
                        {
                            bool asyonetici = tCevir(item.SubItems[5].Text, "Var", "Yok");
                            bool doku = tCevir(item.SubItems[6].Text, "Var", "Yok");
                            if (asyonetici == true)
                            {
                                MessageBox.Show("As yöneticilerin yöneticilik yetkisini alamazsınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                komut.Connection = baglanti;
                                komut.CommandText = "UPDATE kullanicilar SET yonetici=false WHERE id=@id";
                                komut.Parameters.AddWithValue("@id", cID);
                                komut.ExecuteNonQuery();
                                komut.Dispose();
                            }
                        }
                        else
                        {
                            komut.Connection = baglanti;
                            komut.CommandText = "UPDATE kullanicilar SET yonetici=true WHERE id=@id";
                            komut.Parameters.AddWithValue("@id", cID);
                            komut.ExecuteNonQuery();
                            komut.Dispose();
                        }
                    }
                }
                listView1.Items.Clear();
                listView1.Columns.Clear();
                listele();
                baglanti.Close();
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (gAs == false)
                {
                    MessageBox.Show("Sadece as yöneticiler doku verme ve alma işlemleri yapabilir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    baglanti.Open();
                    foreach (ListViewItem item in listView1.SelectedItems)
                    {
                        int cID = Convert.ToInt32(item.Text);
                        if (gID == cID)
                        {
                            MessageBox.Show("Kendinizi dokunulmazlıktan atamazsınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {

                            bool cDoku = tCevir(item.SubItems[6].Text, "Var", "Yok");
                            if (cDoku == true)
                            {
                                bool asyonetici = tCevir(item.SubItems[5].Text, "Var", "Yok");
                                bool doku = tCevir(item.SubItems[6].Text, "Var", "Yok");
                                if (asyonetici == true)
                                {
                                    MessageBox.Show("As yönetici olmadığınız için doku veremez veya almazsınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    komut.Connection = baglanti;
                                    komut.CommandText = "UPDATE kullanicilar SET doku=false WHERE id=@id";
                                    komut.Parameters.AddWithValue("@id", cID);
                                    komut.ExecuteNonQuery();
                                    komut.Dispose();
                                }
                            }
                            else
                            {
                                komut.Connection = baglanti;
                                komut.CommandText = "UPDATE kullanicilar SET doku=true WHERE id=@id";
                                komut.Parameters.AddWithValue("@id", cID);
                                komut.ExecuteNonQuery();
                                komut.Dispose();
                            }
                        }
                    }
                    listView1.Items.Clear();
                    listView1.Columns.Clear();
                    listele();
                    baglanti.Close();
                }
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0].SubItems[8].Text == "")
            {
                if (listView1.SelectedItems[0].SubItems[6].Text == "Yok" && listView1.SelectedItems[0].SubItems[3].Text == "Yok" && listView1.SelectedItems[0].SubItems[5].Text == "Yok")
                {
                    if (listView1.SelectedItems.Count == 1)
                    {
                        int id = int.Parse(listView1.SelectedItems[0].Text);
                        string kullanici = listView1.SelectedItems[0].SubItems[1].Text;
                        Form3 form3 = new Form3(id, kullanici);
                        form3.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Bu kullanıcıyı yasaklayamazsınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                DialogResult soru = MessageBox.Show(listView1.SelectedItems[0].SubItems[1].Text + " adlı kullanıcının yasağını kaldırmak istediğinize emin misiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (soru == DialogResult.Yes)
                {
                    baglanti.Open();
                    komut.Connection = baglanti;
                    komut.CommandText = "UPDATE kullanicilar SET yasakli='' WHERE id=@id";
                    komut.Parameters.AddWithValue("@id", int.Parse(listView1.SelectedItems[0].Text));
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                    baglanti.Close();
                }
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            if (gYonetici == true)
            {
                listView1.Clear();
                listView1.FullRowSelect = true;
                label1.Text = "Hoşgeldiniz " + gKullanici + "(Yönetici)";
                this.Text = gKullanici + "(Yönetici)";
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                textBox1.Visible = true;
                comboBox1.Visible = true;
                comboBox2.Visible = true;
                listView1.Visible = true;
                listele();

                baglanti.Open();

                OleDbCommand komut2 = new OleDbCommand("SELECT * FROM ayarlar", baglanti);
                OleDbDataReader okuyucu2 = komut2.ExecuteReader();
                string nBaslik = "";
                bool nUyelik = true;
                bool nSistem = true;

                while (okuyucu2.Read())
                {
                    nBaslik = okuyucu2["baslik"].ToString();
                    nUyelik = Boolean.Parse(okuyucu2["uyelik"].ToString());
                    nSistem = Boolean.Parse(okuyucu2["sistem"].ToString());
                }

                textBox1.Text = nBaslik;
                comboBox1.Text = Cevir(nUyelik, "Açık", "Kapalı");
                comboBox2.Text = Cevir(nSistem, "Açık", "Kapalı");

                baglanti.Close();

            }
            else if (gYonetici == false)
            {
                listView1.Clear();
                label1.Text = "Hoşgeldiniz " + gKullanici + "(Kullanıcı)";
                this.Text = gKullanici + "(Kullanıcı)";
                listView1.FullRowSelect = true;
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                else
                {
                    baglanti.Close();
                    baglanti.Open();
                }
                komut.Connection = baglanti;
                komut.CommandText = "SELECT * FROM kullanicilar";
                OleDbDataReader okuyucu = komut.ExecuteReader();

                listView1.Columns.Add("Kullanıcı Adı");
                listView1.Columns.Add("Aktiflik");

                int count = 0;
                while (okuyucu.Read())
                {
                    count = listView1.Items.Count;
                    listView1.Visible = true;
                    listView1.Items.Add(okuyucu["kullanici"].ToString());
                    string aktifti = "";
                    if (okuyucu["sonaktif"].ToString() == "Şuan Aktif")
                    {
                        aktifti = okuyucu["sonaktif"].ToString();
                    }
                    else
                    {
                        TimeSpan sonaktif = DateTime.Now - Convert.ToDateTime(okuyucu["sonaktif"].ToString());
                        if (sonaktif.Days == 0)
                        {
                            if (sonaktif.Hours == 0)
                            {
                                if (sonaktif.Minutes == 0)
                                {
                                    if (sonaktif.Seconds == 0)
                                    {
                                        aktifti = "?";
                                    }
                                    else
                                    {
                                        aktifti = sonaktif.Seconds + " saniye önce aktifti";
                                    }
                                }
                                else
                                {
                                    aktifti = sonaktif.Minutes + " dakika önce aktifti";
                                }
                            }
                            else
                            {
                                aktifti = sonaktif.Hours + " saat önce aktifti";
                            }

                        }
                        else
                        {
                            aktifti = sonaktif.Days + " gün önce aktifti";
                        }
                    }
                    listView1.Items[count].SubItems.Add(aktifti);
                }
            }
            baglanti.Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            else
            {
                baglanti.Close();
                baglanti.Open();
            }
            komut.Connection = baglanti;
            DateTime tarih = DateTime.Now;
            komut.CommandText = "UPDATE kullanicilar SET sonaktif='" + tarih + "' WHERE id=@id";
            komut.Parameters.AddWithValue("@id", gID);
            komut.ExecuteNonQuery();
            komut.Dispose();
            baglanti.Close();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            string nBaslik = textBox1.Text.Trim();
            bool nUyelik = true;
            bool nSistem = true;
            if (comboBox1.Text == "Açık")
            {
                nUyelik = true;
            }
            else if (comboBox1.Text == "Kapalı")
            {
                nUyelik = false;
            }

            if (comboBox2.Text == "Açık")
            {
                nSistem = true;
            }
            else if (comboBox2.Text == "Kapalı")
            {
                nSistem = false;
            }

            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "UPDATE ayarlar SET baslik='" + nBaslik + "', uyelik=" + nUyelik + ", sistem=" + nSistem + " WHERE id=1";
            komut.ExecuteNonQuery();
            komut.Dispose();
            baglanti.Close();
            MessageBox.Show("Değişiklikler kaydedildi.\nDeğişiklikler program kapanıp açıldığında etkin olacaktır.");
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView1.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    string sYasak = "";
                    string sDoku = "";
                    string sYonetici = "";
                    string yasak = listView1.SelectedItems[0].SubItems[8].Text;
                    string doku = listView1.SelectedItems[0].SubItems[6].Text;
                    string yonetici = listView1.SelectedItems[0].SubItems[3].Text;
                    string asyonetici = listView1.SelectedItems[0].SubItems[5].Text;
                    int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);

                    bool nAsyonetici = tCevir(asyonetici, "Var", "Yok");
                    bool nDoku = tCevir(doku, "Var", "Yok");
                    bool nYonetici = tCevir(yonetici, "Var", "Yok");

                    if (yasak != "")
                    {
                        sYasak = "Yasağı Kaldır";
                    }
                    else if (yasak == "")
                    {
                        sYasak = "Yasakla";
                    }

                    if (nDoku == true)
                    {
                        sDoku = "Doku Al";
                    }
                    else if (nDoku == false)
                    {
                        sDoku = "Doku Ver";
                    }

                    if (nYonetici == true)
                    {
                        sYonetici = "Yöneticiliği Al";
                    }
                    else if (nYonetici == false)
                    {
                        sYonetici = "Yöneticilik Ver";
                    }

                    //MessageBox.Show("sYasak = " + sYasak + "\nsDoku = " + sDoku + "\nsYonetici = " + sYonetici + "\nyasak = " + yasak + "\ndoku = " + doku + "\nyonetici = " + yonetici + "\nnDoku = " + nDoku + "\nnYonetici = " + nYonetici);

                    if (nAsyonetici == true)
                    {
                        contextMenuStrip1.Items[1].Enabled = false;
                        contextMenuStrip1.Items[2].Enabled = false;
                        contextMenuStrip1.Items[6].Enabled = false;
                        contextMenuStrip1.Items[4].Enabled = false;
                        contextMenuStrip1.Items[5].Enabled = false;
                    }
                    else
                    {
                        contextMenuStrip1.Items[1].Enabled = true;
                        contextMenuStrip1.Items[2].Enabled = true;
                        contextMenuStrip1.Items[6].Enabled = true;
                        contextMenuStrip1.Items[4].Enabled = true;
                        contextMenuStrip1.Items[5].Enabled = true;
                    }

                    if (nYonetici == true)
                    {
                        contextMenuStrip1.Items[6].Enabled = false;
                        contextMenuStrip1.Items[5].Enabled = false;
                    }
                    else
                    {
                        contextMenuStrip1.Items[6].Enabled = true;
                        contextMenuStrip1.Items[5].Enabled = true;
                    }

                    if (nDoku == true)
                    {
                        contextMenuStrip1.Items[6].Enabled = false;
                        contextMenuStrip1.Items[1].Enabled = false;
                        contextMenuStrip1.Items[2].Enabled = false;
                    }
                    else
                    {
                        contextMenuStrip1.Items[6].Enabled = true;
                        contextMenuStrip1.Items[1].Enabled = true;
                        contextMenuStrip1.Items[2].Enabled = true;
                    }

                    if (gID == id)
                    {
                        contextMenuStrip1.Items[1].Enabled = false;
                        contextMenuStrip1.Items[2].Enabled = false;
                        contextMenuStrip1.Items[6].Enabled = false;
                        contextMenuStrip1.Items[4].Enabled = false;
                        contextMenuStrip1.Items[5].Enabled = false;
                    }
                    else
                    {
                        contextMenuStrip1.Items[1].Enabled = true;
                        contextMenuStrip1.Items[2].Enabled = true;
                        contextMenuStrip1.Items[6].Enabled = true;
                        contextMenuStrip1.Items[4].Enabled = true;
                        contextMenuStrip1.Items[5].Enabled = true;
                    }

                    contextMenuStrip1.Items[6].Text = sYasak;
                    contextMenuStrip1.Items[4].Text = sYonetici;
                    contextMenuStrip1.Items[5].Text = sDoku;
                    contextMenuStrip1.Show(Cursor.Position);
                }
            } 
        }

        private void ekleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool hata = false;
            string kullanici = Microsoft.VisualBasic.Interaction.InputBox("Kullanıcı Adı: ", "Ekle", "", 50, 50);
            if (kullanici.Trim() != "")
            {
                foreach (ListViewItem item in listView1.Items)
                {
                    if (kullanici.Trim() == item.SubItems[1].Text.Trim())
                    {
                        hata = true;
                    }
                }
                if (hata == true)
                {
                    MessageBox.Show("Bu kullanıcı adında zaten bir kullanıcı mevcuttur.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    hata = false;
                }
                else
                {
                    string sifre = Microsoft.VisualBasic.Interaction.InputBox("Şifre: ", "Ekle", "", 50, 50);
                    if (sifre.Trim() != "")
                    {
                        DateTime tarih = DateTime.Now;
                        if (baglanti.State == ConnectionState.Closed)
                        {
                            baglanti.Open();
                        }
                        else
                        {
                            baglanti.Close();
                            baglanti.Open();
                        }
                        komut.Connection = baglanti;
                        komut.CommandText = "INSERT INTO kullanicilar (kullanici, sifre, tarih, sonaktif) VALUES ('" + kullanici.Trim() + "', '" + sifre.Trim() + "', '" + tarih + "', '" + tarih + "')";
                        komut.ExecuteNonQuery();
                        komut.Dispose();
                        baglanti.Close();
                        MessageBox.Show("Kullanıcı ekleme işlemi başarıyla gerçekleşmiştir.\nKullanıcı Adı: " + kullanici.Trim() + "\nŞifre: " + sifre.Trim() + "\nKayıt Tarihi: " + tarih.ToString());
                        listView1.Items.Clear();
                        listView1.Columns.Clear();
                        listele();
                    }
                }
            }
        }

        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool hata = false;
            if (listView1.SelectedItems.Count == 1)
            {
                bool asyonetici = tCevir(listView1.Items[listView1.SelectedItems[0].Index].SubItems[5].Text, "Var", "Yok");
                bool doku = tCevir(listView1.Items[listView1.SelectedItems[0].Index].SubItems[6].Text, "Var", "Yok");
                int id = int.Parse(listView1.Items[listView1.SelectedItems[0].Index].Text);
                if (id == gID)
                {
                    int cID = int.Parse(listView1.Items[listView1.SelectedItems[0].Index].Text);
                    string cKullanici = listView1.Items[listView1.SelectedItems[0].Index].SubItems[1].Text;
                    string cSifre = listView1.Items[listView1.SelectedItems[0].Index].SubItems[2].Text;
                    string sKullanici = Microsoft.VisualBasic.Interaction.InputBox("Kullanıcı Adı: ", "Düzenle", cKullanici, 50, 50);
                    if (sKullanici.Trim() != "")
                    {
                        foreach (ListViewItem item in listView1.Items)
                        {
                            if (sKullanici.Trim() == item.SubItems[1].Text.Trim())
                            {
                                hata = true;
                            }
                        }
                        if (hata == true)
                        {
                            MessageBox.Show("Bu kullanıcı adında zaten bir kullanıcı mevcuttur.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            hata = false;
                        }
                        else
                        {
                            string sSifre = Microsoft.VisualBasic.Interaction.InputBox("Şifre: ", "Düzenle", cSifre, 50, 50);
                            if (sSifre.Trim() != "")
                            {
                                DateTime cTarih = DateTime.Now;
                                baglanti.Open();
                                komut.Connection = baglanti;
                                komut.CommandText = "UPDATE kullanicilar SET kullanici='" + sKullanici + "', sifre='" + sSifre + "', tarih='" + cTarih + "' WHERE id=@id";
                                komut.Parameters.AddWithValue("@id", cID);
                                komut.ExecuteNonQuery();
                                komut.Dispose();
                                baglanti.Close();
                                listView1.Items.Clear();
                                listView1.Columns.Clear();
                                listele();
                            }
                        }
                    }
                }
                else if (asyonetici == true)
                {
                    MessageBox.Show("Düzenlemeye çalıştığınız kullanıcı as yöneticidir..", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (doku == true)
                {
                    MessageBox.Show("Silmeye çalıştığınız kullanıcı dokunulmazdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int cID = int.Parse(listView1.Items[listView1.SelectedItems[0].Index].Text);
                    string cKullanici = listView1.Items[listView1.SelectedItems[0].Index].SubItems[1].Text;
                    string cSifre = listView1.Items[listView1.SelectedItems[0].Index].SubItems[2].Text;
                    string sKullanici = Microsoft.VisualBasic.Interaction.InputBox("Kullanıcı Adı: ", "Düzenle", cKullanici, 50, 50);
                    if (sKullanici.Trim() != "")
                    {
                        foreach (ListViewItem item in listView1.Items)
                        {
                            if (sKullanici.Trim() == item.SubItems[1].Text.Trim())
                            {
                                hata = true;
                            }
                        }
                        if (hata == true)
                        {
                            MessageBox.Show("Bu kullanıcı adında zaten bir kullanıcı mevcuttur.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            hata = false;
                        }
                        else
                        {
                            string sSifre = Microsoft.VisualBasic.Interaction.InputBox("Şifre: ", "Düzenle", cSifre, 50, 50);
                            if (sSifre.Trim() != "")
                            {
                                DateTime cTarih = DateTime.Now;
                                baglanti.Open();
                                komut.Connection = baglanti;
                                komut.CommandText = "UPDATE kullanicilar SET kullanici='" + sKullanici + "', sifre='" + sSifre + "', tarih='" + cTarih + "' WHERE id=@id";
                                komut.Parameters.AddWithValue("@id", cID);
                                komut.ExecuteNonQuery();
                                komut.Dispose();
                                baglanti.Close();
                                listView1.Items.Clear();
                                listView1.Columns.Clear();
                                listele();
                            }
                        }
                    }
                }
            }
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                DialogResult soru = MessageBox.Show(listView1.SelectedItems.Count.ToString() + " adet veri seçilmiştir.\nSilmek istediğinize emin misiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (soru == DialogResult.Yes)
                {
                    baglanti.Open();
                    foreach (ListViewItem item in listView1.SelectedItems)
                    {
                        int sID = int.Parse(item.Text);
                        bool asyonetici = tCevir(item.SubItems[5].Text.ToString(), "Var", "Yok");
                        bool doku = tCevir(item.SubItems[6].Text.ToString(), "Var", "Yok");
                        if (gID == sID)
                        {
                            MessageBox.Show("Silmeye çalıştığınız kullanıcı sizsiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (asyonetici == true)
                        {
                            MessageBox.Show("Silmeye çalıştığınız kullanıcı as yöneticidir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (doku == true)
                        {
                            MessageBox.Show("Silmeye çalıştığınız kullanıcı dokunulmazdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            komut.Connection = baglanti;
                            komut.CommandText = "DELETE FROM kullanicilar WHERE id=@id";
                            komut.Parameters.AddWithValue("@id", sID);
                            komut.ExecuteNonQuery();
                            komut.Dispose();
                        }
                    }
                    baglanti.Close();
                    listView1.Items.Clear();
                    listView1.Columns.Clear();
                    listele();
                }
            }
        }

        private void yöneticilikVerAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                baglanti.Open();
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    int cID = Convert.ToInt32(item.Text);
                    if (gID == cID)
                    {
                        MessageBox.Show("Kendinizi yöneticilikten atamazsınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        bool cYonetici = tCevir(item.SubItems[3].Text, "Var", "Yok");
                        if (cYonetici == true)
                        {
                            bool asyonetici = tCevir(item.SubItems[5].Text, "Var", "Yok");
                            bool doku = tCevir(item.SubItems[6].Text, "Var", "Yok");
                            if (asyonetici == true)
                            {
                                MessageBox.Show("As yöneticilerin yöneticilik yetkisini alamazsınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                komut.Connection = baglanti;
                                komut.CommandText = "UPDATE kullanicilar SET yonetici=false WHERE id=@id";
                                komut.Parameters.AddWithValue("@id", cID);
                                komut.ExecuteNonQuery();
                                komut.Dispose();
                            }
                        }
                        else
                        {
                            komut.Connection = baglanti;
                            komut.CommandText = "UPDATE kullanicilar SET yonetici=true WHERE id=@id";
                            komut.Parameters.AddWithValue("@id", cID);
                            komut.ExecuteNonQuery();
                            komut.Dispose();
                        }
                    }
                }
                listView1.Items.Clear();
                listView1.Columns.Clear();
                listele();
                baglanti.Close();
            }
        }

        private void dokuVerAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (gAs == false)
                {
                    MessageBox.Show("Sadece as yöneticiler doku verme ve alma işlemleri yapabilir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    baglanti.Open();
                    foreach (ListViewItem item in listView1.SelectedItems)
                    {
                        int cID = Convert.ToInt32(item.Text);
                        if (gID == cID)
                        {
                            MessageBox.Show("Kendinizi dokunulmazlıktan atamazsınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {

                            bool cDoku = tCevir(item.SubItems[6].Text, "Var", "Yok");
                            if (cDoku == true)
                            {
                                bool asyonetici = tCevir(item.SubItems[5].Text, "Var", "Yok");
                                bool doku = tCevir(item.SubItems[6].Text, "Var", "Yok");
                                if (asyonetici == true)
                                {
                                    MessageBox.Show("As yönetici olmadığınız için doku veremez veya almazsınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    komut.Connection = baglanti;
                                    komut.CommandText = "UPDATE kullanicilar SET doku=false WHERE id=@id";
                                    komut.Parameters.AddWithValue("@id", cID);
                                    komut.ExecuteNonQuery();
                                    komut.Dispose();
                                }
                            }
                            else
                            {
                                komut.Connection = baglanti;
                                komut.CommandText = "UPDATE kullanicilar SET doku=true WHERE id=@id";
                                komut.Parameters.AddWithValue("@id", cID);
                                komut.ExecuteNonQuery();
                                komut.Dispose();
                            }
                        }
                    }
                    listView1.Items.Clear();
                    listView1.Columns.Clear();
                    listele();
                    baglanti.Close();
                }
            }
        }

        private void yasaklaKaldırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0].SubItems[8].Text == "")
            {
                if (listView1.SelectedItems[0].SubItems[6].Text == "Yok" && listView1.SelectedItems[0].SubItems[3].Text == "Yok" && listView1.SelectedItems[0].SubItems[5].Text == "Yok")
                {
                    if (listView1.SelectedItems.Count == 1)
                    {
                        int id = int.Parse(listView1.SelectedItems[0].Text);
                        string kullanici = listView1.SelectedItems[0].SubItems[1].Text;
                        Form3 form3 = new Form3(id, kullanici);
                        form3.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Bu kullanıcıyı yasaklayamazsınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                DialogResult soru = MessageBox.Show(listView1.SelectedItems[0].SubItems[1].Text + " adlı kullanıcının yasağını kaldırmak istediğinize emin misiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (soru == DialogResult.Yes)
                {
                    baglanti.Open();
                    komut.Connection = baglanti;
                    komut.CommandText = "UPDATE kullanicilar SET yasakli='' WHERE id=@id";
                    komut.Parameters.AddWithValue("@id", int.Parse(listView1.SelectedItems[0].Text));
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                    baglanti.Close();
                }
            }
        }

        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gYonetici == true)
            {
                listView1.Clear();
                listView1.FullRowSelect = true;
                label1.Text = "Hoşgeldiniz " + gKullanici + "(Yönetici)";
                this.Text = gKullanici + "(Yönetici)";
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                textBox1.Visible = true;
                comboBox1.Visible = true;
                comboBox2.Visible = true;
                listView1.Visible = true;
                listele();

                baglanti.Open();

                OleDbCommand komut2 = new OleDbCommand("SELECT * FROM ayarlar", baglanti);
                OleDbDataReader okuyucu2 = komut2.ExecuteReader();
                string nBaslik = "";
                bool nUyelik = true;
                bool nSistem = true;

                while (okuyucu2.Read())
                {
                    nBaslik = okuyucu2["baslik"].ToString();
                    nUyelik = Boolean.Parse(okuyucu2["uyelik"].ToString());
                    nSistem = Boolean.Parse(okuyucu2["sistem"].ToString());
                }

                textBox1.Text = nBaslik;
                comboBox1.Text = Cevir(nUyelik, "Açık", "Kapalı");
                comboBox2.Text = Cevir(nSistem, "Açık", "Kapalı");

                baglanti.Close();

            }
            else if (gYonetici == false)
            {
                listView1.Clear();
                label1.Text = "Hoşgeldiniz " + gKullanici + "(Kullanıcı)";
                this.Text = gKullanici + "(Kullanıcı)";
                listView1.FullRowSelect = true;
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                else
                {
                    baglanti.Close();
                    baglanti.Open();
                }
                komut.Connection = baglanti;
                komut.CommandText = "SELECT * FROM kullanicilar";
                OleDbDataReader okuyucu = komut.ExecuteReader();

                listView1.Columns.Add("Kullanıcı Adı");
                listView1.Columns.Add("Aktiflik");

                int count = 0;
                while (okuyucu.Read())
                {
                    count = listView1.Items.Count;
                    listView1.Visible = true;
                    listView1.Items.Add(okuyucu["kullanici"].ToString());
                    string aktifti = "";
                    if (okuyucu["sonaktif"].ToString() == "Şuan Aktif")
                    {
                        aktifti = okuyucu["sonaktif"].ToString();
                    }
                    else
                    {
                        TimeSpan sonaktif = DateTime.Now - Convert.ToDateTime(okuyucu["sonaktif"].ToString());
                        if (sonaktif.Days == 0)
                        {
                            if (sonaktif.Hours == 0)
                            {
                                if (sonaktif.Minutes == 0)
                                {
                                    if (sonaktif.Seconds == 0)
                                    {
                                        aktifti = "?";
                                    }
                                    else
                                    {
                                        aktifti = sonaktif.Seconds + " saniye önce aktifti";
                                    }
                                }
                                else
                                {
                                    aktifti = sonaktif.Minutes + " dakika önce aktifti";
                                }
                            }
                            else
                            {
                                aktifti = sonaktif.Hours + " saat önce aktifti";
                            }

                        }
                        else
                        {
                            aktifti = sonaktif.Days + " gün önce aktifti";
                        }
                    }
                    listView1.Items[count].SubItems.Add(aktifti);
                }
            }
            baglanti.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}