using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string KlasorYol;
        string[] dosyalar = null;
        string yol = null;

        private void button1_Click(object sender, EventArgs e)        
        {                
            button1.Text= "Yeniden Resim Seç";
            listBox1.Items.Clear();
            openFileDialog1.Filter = "JPEG |*.jpg; *.jpeg| PNG |*.png| TIFF |*.tif;*tiff| BMP |*.bmp";
            openFileDialog1.Title = "Ölçeklendirilecek Resimi Seçiniz..";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.CheckFileExists = false;
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dosyalar = openFileDialog1.FileNames;
                for (int x = 0; x < dosyalar.Length; x++)
                {
                    listBox1.Items.Add(dosyalar[x]);
                }                  
            }
            label3.Text = "Seçilen Resim Sayısı:" + Convert.ToString(listBox1.Items.Count);
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            label3.Text = "Seçilen Resim Sayısı:" + Convert.ToString(listBox1.Items.Count);
            label1.Text = "İstenilen Yatay Boyut:";
            label2.Text = "İstenilen Dikey Boyut:";
            label4.Text = "Seçilen Resimlerin Bulundukları Dizin ve Dosya İsimleri";
            button1.Text = "Resim Seç";
            button2.Text = "Boyutlandır & Kaydet";
            button3.Text = "Dizin Seç!";
            label5.Text = "Dosyalarınızın Kayıt Edileceği Dizini Seçmelisiniz..!";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            char ayir = '\\';
            yol = folderBrowserDialog1.SelectedPath;
            string[] kyollar = yol.Split(ayir);
            KlasorYol = null;
            foreach (string parca in kyollar)
            {
                KlasorYol = KlasorYol + parca + "\\";
            }
            label5.Text = "Dosyalarınız " + KlasorYol + " Dizinine Kayıt Edilecektir.";
        }



        private void button2_Click(object sender, EventArgs e)
        {
            var donus = DialogResult.Cancel;  
            if(yol == null)
            {
                MessageBox.Show("Lütfen Dosyaların Kayıt Edileceği Dizini Seçiniz.", "Kayıt Alanı Seçilmelidir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (textBox1.Text == "" && textBox2.Text == "")
                {
                    MessageBox.Show("Lütfen Ölçeklendirme Boyutlarını Giriniz.", "Boyut Alanı Boş Geçilemez", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (dosyalar == null)
                    {
                        MessageBox.Show("Lütfen Ölçeklendirmek İstediğiniz Resimleri Seçin.", "Resim Seçmelisiniz", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        Image img = null;
                        Bitmap bmp = null;

                        foreach(string dosya in dosyalar)
                        {
                            string isim = dosya.Substring(dosya.LastIndexOf('\\'));
                            img = Image.FromFile(dosya);
                            string kontrol = KlasorYol + "\\" + isim;
                            if (System.IO.File.Exists(kontrol))
                            {
                                donus = MessageBox.Show("Bu İsimde Bir Dosya Zaten Bulunuyor. Yinede Kaydederek Dosyayı Değiştirmek İstiyor Musunuz?", "Uyarı!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                if (donus == DialogResult.OK)
                                {
                                    bmp = new Bitmap(img, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
                                    bmp.Save(KlasorYol + "\\" + isim, img.RawFormat);
                                    MessageBox.Show("Dosyanız Kayıt Edildi.", "Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Dosya Kayıt İşlemi İptal Edildi.", "İptal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                bmp = new Bitmap(img, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
                                bmp.Save(KlasorYol + "\\" + isim, img.RawFormat);
                                MessageBox.Show("Dosyanız Kayıt Edildi.","Tamamlandı",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            }
                        }

                        if (img != null && bmp != null)
                        {
                            img.Dispose();
                            bmp.Dispose();

                        }
                    }
                }
            }
        }

    }
}
