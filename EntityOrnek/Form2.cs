using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityOrnek
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();

        private void BtnLinqEntity2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                var degerler = db.TBLNOTLAR.Where(x => x.SINAV1 < 50);
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton2.Checked)
            {
                var degerler = db.TBLOGRENCI.Where(x => x.AD == "Ali");
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton3.Checked)
            {
                var degerler = db.TBLOGRENCI.Where(x => x.AD == textBox1.Text || x.SOYAD == textBox1.Text);
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton4.Checked)
            {
                var degerler = db.TBLOGRENCI.Select(x => new { Soyad = x.SOYAD });
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton5.Checked)
            {
                var degerler = db.TBLOGRENCI.Select(x => new { Ad = x.AD.ToUpper(), Soyad = x.SOYAD.ToLower() });
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton6.Checked)
            {
                var degerler = db.TBLOGRENCI.Select(x => new { Ad = x.AD.ToUpper() }).Where(x => x.Ad != "Ali");
                dataGridView1.DataSource = degerler.ToList();
            }

            if (radioButton7.Checked)
            {
                var degerler = db.TBLNOTLAR.Select(x => new
                {
                    OgrenciAd = x.OGR,
                    Ortalaması = x.ORTALAMA,
                    Durumu = x.DURUM == true ? "Geçti" : "Kaldı"

                });
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton8.Checked)
            {
                var degerler = db.TBLNOTLAR.SelectMany(x => db.TBLOGRENCI.Where(y => y.ID == x.OGR), (x, y) => new
                {
                    y.AD,
                    x.ORTALAMA
                });
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton9.Checked)
            {
                var degerler = db.TBLOGRENCI.OrderBy(x => x.ID).Take(3);
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton10.Checked)
            {
                var degerler = db.TBLOGRENCI.OrderByDescending(x => x.ID).Take(3);
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton11.Checked)
            {
                var degerler = db.TBLOGRENCI.OrderBy(x => x.AD);
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton12.Checked)
            {
                var degerler = db.TBLOGRENCI.OrderBy(x => x.AD).Skip(5);

                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton13.Checked)
            {
                var degerler = db.TBLOGRENCI.OrderBy(x => x.AD).ThenByDescending(x => x.SOYAD);

                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton14.Checked)
            {
                var degerler = db.TBLOGRENCI.OrderBy(x => x.SEHIR).GroupBy(y => y.SEHIR).Select(z => new { Şehir = z.Key, Toplam = z.Count() });

                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton15.Checked)
            {
                var degerler = db.TBLNOTLAR.GroupBy(y => y.OGR).Select(x => new { Öğrenci_ID = x.Key, Ortalama = x.Max(a => a.ORTALAMA) });
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton16.Checked)
            {
                var degerler = db.TBLOGRENCI.GroupBy(x => x.ID).Select(y => new { Öğrenci_ID = y.Key, Öğrenci_Sayısı = y.Count() });
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton17.Checked)
            {
                var degerler = db.TBLNOTLAR.GroupBy(x => x.DERS).Select(y => new { Ders_ID = y.Key, Ortalama = y.Sum(z => z.ORTALAMA) });
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton18.Checked)
            {
                var degerler = db.TBLNOTLAR.GroupBy(x => x.DERS).Select(y => new { Ders_ID = y.Key, Ortalama = y.Average(z => z.ORTALAMA) });
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton19.Checked)
            {
                var degerler = from x in db.TBLOGRENCI orderby x.AD descending select x.AD;
                dataGridView1.DataSource = degerler.ToList();
            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
