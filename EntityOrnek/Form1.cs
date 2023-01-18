using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EntityOrnek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void BtnOgrenciListele_Click(object sender, EventArgs e)
        {


            dataGridView1.DataSource = db.TBLOGRENCI.ToList();

        }

        private void BtnDersListele_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=YUKSEL\SQLEXPRESS;Initial Catalog=DbSinavOgrenci;Integrated Security=True");
            SqlCommand komut = new SqlCommand("Select * FROM TBLDERSLER", con);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;

            /*
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            */
        }

        private void BtnNotListesi_Click(object sender, EventArgs e)
        {
            var query = from item in db.TBLNOTLAR
                        select new
                        {
                            item.NOTID,
                            item.OGR,
                            item.TBLOGRENCI.AD,
                            item.TBLOGRENCI.SOYAD,
                            item.DERS,
                            item.SINAV1,
                            item.SINAV2,
                            item.SINAV3,
                            item.ORTALAMA
                        };
            dataGridView1.DataSource = query.ToList();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            TBLOGRENCI t = new TBLOGRENCI();
            t.AD = TxtAd.Text;
            t.SOYAD = TxtSoyad.Text;

            db.TBLOGRENCI.Add(t);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Listeye Eklenmiştir.");
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TxtOgrenciId.Text);
            var x = db.TBLOGRENCI.Find(id);
            db.TBLOGRENCI.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Sistemden Silindi.");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TxtOgrenciId.Text);
            var x = db.TBLOGRENCI.Find(id);


            x.AD = TxtAd.Text;
            x.SOYAD = TxtSoyad.Text;
            x.FOTOGRAF = TxtFoto.Text;

            db.SaveChanges();

            MessageBox.Show("Öğrenci Bilgileri Başarıyla Güncellendi.");
        }

        private void BtnProsedur_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.NotListesi();
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.TBLOGRENCI.Where(x => x.AD == TxtAd.Text & x.SOYAD == TxtSoyad.Text).ToList();
        }

        private void TxtAd_TextChanged(object sender, EventArgs e)
        {
            string aranan = TxtAd.Text;
            var degerler = from item in db.TBLOGRENCI
                           where item.AD.Contains(aranan)
                           select item;

            dataGridView1.DataSource = degerler.ToList();
        }

        private void BtnLinqEntity_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                List<TBLOGRENCI> liste1 = db.TBLOGRENCI.OrderBy(p => p.AD).ToList();
                dataGridView1.DataSource = liste1;
            }
            if (radioButton2.Checked)
            {
                List<TBLOGRENCI> liste2 = db.TBLOGRENCI.OrderByDescending(p => p.AD).ToList();
                dataGridView1.DataSource = liste2;
            }
            if (radioButton3.Checked)
            {
                List<TBLOGRENCI> liste3 = db.TBLOGRENCI.OrderBy(p => p.AD).Take(3).ToList();
                dataGridView1.DataSource = liste3;
            }

            if (radioButton4.Checked)
            {
                List<TBLOGRENCI> liste4 = db.TBLOGRENCI.Where(p => p.ID == 6).ToList();
                dataGridView1.DataSource = liste4;
            }
            if (radioButton5.Checked)
            {
                List<TBLOGRENCI> liste5 = db.TBLOGRENCI.Where(p => p.AD.StartsWith("a")).ToList();
                dataGridView1.DataSource = liste5;
            }
            if (radioButton6.Checked)
            {
                List<TBLOGRENCI> liste6 = db.TBLOGRENCI.Where(p => p.AD.EndsWith("a")).ToList();
                dataGridView1.DataSource = liste6;
            }
            if (radioButton7.Checked)
            {
                bool deger = db.TBLOGRENCI.Any();
                if (deger)
                {
                    MessageBox.Show("Öğrenci Tablosu Dolu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question);

                }
                else
                {
                    MessageBox.Show("Öğrenci Tablosu Boş", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question);

                }
            }
            if (radioButton8.Checked)
            {
                int toplam = db.TBLOGRENCI.Count();
                MessageBox.Show(toplam.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question);

            }
            if (radioButton9.Checked)
            {
                var toplam = db.TBLNOTLAR.Sum(p => p.SINAV1);
                MessageBox.Show($"Toplam Sınav1 Puanı: {toplam.ToString()}"
                , "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question);

            }
            if (radioButton10.Checked)
            {
                var ortalama = db.TBLNOTLAR.Average(p => p.SINAV1);
                MessageBox.Show($"Sınav1 Ortalama Puanı: {ortalama.ToString()}"
                , "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            if (radioButton11.Checked)
            {
                var ortalama = db.TBLNOTLAR.Average(p => p.SINAV1);

                var ortalamadan_yuksek = db.TBLNOTLAR.Where(p => p.SINAV1 > ortalama).ToList();
                dataGridView1.DataSource = ortalamadan_yuksek;
            }
            if (radioButton12.Checked)
            {
                var en_yuksek = db.TBLNOTLAR.Max(p => p.SINAV1);
                MessageBox.Show($"En Yüksek Sınav1 Puanı: {en_yuksek.ToString()}"
            , "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }


        }

        private void BtnJoin_Click(object sender, EventArgs e)
        {
            var sorgu = from item1 in db.TBLNOTLAR
                        join item2 in db.TBLOGRENCI
                        on item1.OGR equals item2.ID
                        select new
                        {
                            Öğrenci= item2.AD +" "+ item2.SOYAD,
                            Sınav1=item1.SINAV1,
                            Sınav2=item1.SINAV2,
                            Ortalama=item1.ORTALAMA
                        };

            dataGridView1.DataSource = sorgu.ToList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnForm2Show_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
