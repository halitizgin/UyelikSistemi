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
    public partial class Form3 : DevExpress.XtraEditors.XtraForm
    {
        public Form3(int id, string kullanici)
        {
            InitializeComponent();
            gID = id;
            gKullanici = kullanici;
        }

        int gID = 0;
        string gKullanici = "";

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=kullanicilar.accdb");
        OleDbCommand komut = new OleDbCommand();

        private void Form3_Load(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "MMMM dd, yyyy - dddd - HH:mm:ss";
            textBox1.Text = gID.ToString();
            textBox2.Text = gKullanici;
            this.Text = gKullanici + " - Yasaklama İşlemleri";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && radioButton1.Checked != false || radioButton2.Checked != false || radioButton3.Checked != false)
            {
                if (radioButton1.Checked == true)
                {
                    DateTime girilen = DateTime.Parse(dateTimePicker1.Value.ToString());
                    DateTime bugun = DateTime.Now;
                    int buyuk = DateTime.Compare(girilen, bugun);
                    if (buyuk == 1)
                    {
                        TimeSpan sonuc = girilen - bugun;
                        DialogResult soru = MessageBox.Show(textBox2.Text + " adlı kullanıcıyı " + girilen + " tarihine kadar yasaklamak istediğinize emin misiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (soru == DialogResult.Yes)
                        {
                            baglanti.Open();
                            komut.Connection = baglanti;
                            string kullanici = textBox2.Text;
                            int id = int.Parse(textBox1.Text);
                            komut.CommandText = "UPDATE kullanicilar SET yasakli='" + girilen + "' WHERE id=@id";
                            komut.Parameters.AddWithValue("@id", id);
                            komut.ExecuteNonQuery();
                            komut.Dispose();
                            baglanti.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Girdiğiniz tarih, ileride bir tarih olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (radioButton2.Checked == true)
                {
                    if (textBox3.Text.Trim() != "")
                    {
                        DateTime belirlenecek = DateTime.Now;
                        DateTime bugun = DateTime.Now;
                        long sure = long.Parse(textBox3.Text);
                        if (comboBox1.Text == "Gün")
                        {
                            belirlenecek = bugun.AddDays(sure);
                        }
                        else if (comboBox1.Text == "Saat")
                        {
                            belirlenecek = bugun.AddHours(sure);
                        }
                        else if (comboBox1.Text == "Dakika")
                        {
                            belirlenecek = bugun.AddMinutes(sure);
                        }
                        else
                        {
                            MessageBox.Show("Herhangi bir süre tipi seçmeniz gerekmektedir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        DialogResult soru = MessageBox.Show(textBox2.Text + " adlı kullanıcıyı, " + belirlenecek + " tarihine kadar yasaklamak istediğinize emin misiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (soru == DialogResult.Yes)
                        {
                            baglanti.Open();
                            komut.Connection = baglanti;
                            komut.CommandText = "UPDATE kullanicilar SET yasakli='" + belirlenecek + "' WHERE id=@id";
                            komut.Parameters.AddWithValue("@id", int.Parse(textBox1.Text.Trim()));
                            komut.ExecuteNonQuery();
                            komut.Dispose();
                            baglanti.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Herhangi bir süre belitmeniz gerekmektedir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (radioButton3.Checked == true)
                {
                    DialogResult soru = MessageBox.Show(textBox2.Text + " adlı kullanıcıyı süresiz yasaklamak istediğinize emin misiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (soru == DialogResult.Yes)
                    {
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.CommandText = "UPDATE kullanicilar SET yasakli='SÜRESİZ' WHERE id=@id";
                        komut.Parameters.AddWithValue("@id", int.Parse(textBox1.Text.Trim()));
                        komut.ExecuteNonQuery();
                        komut.Dispose();
                        baglanti.Close();
                    }
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                textBox3.Enabled = true;
                comboBox1.Enabled = true;
                dateTimePicker1.Enabled = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                textBox3.Enabled = false;
                comboBox1.Enabled = false;
                dateTimePicker1.Enabled = true;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                textBox3.Enabled = false;
                comboBox1.Enabled = false;
                dateTimePicker1.Enabled = false;
            }
        }
    }
}