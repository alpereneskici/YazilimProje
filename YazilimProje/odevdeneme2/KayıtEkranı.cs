using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace odevdeneme2
{
    public partial class KayıtEkranı : Form
    {
        public KayıtEkranı()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CustomerManager accsessmanager = new CustomerManager(new AccesCustomerDAL());
            List<string> dizikontrol = new List<string>();
            Kullanıcı user = new Kullanıcı();
            dizikontrol =accsessmanager.select(textBoxTC.Text, "TC", "Ad", "Login");
                                 user.Ad = TextboxAd.Text;
            user.Soyad = textBoxSoyad.Text;
            user.Tc = textBoxTC.Text;
            user.KullanıcıAdı = textBoxKullanıcıAdı.Text;
            user.Telefon = textBoxTelefon.Text;
            user.Email = textBoxEMail.Text;
            user.Adres = richTextBoxAdres.Text;
            user.UserTypes = comboBoxUserType.Text;
            user.Sifre = textBoxSifre.Text;
            if(textBoxTC.TextLength==11&& textBoxTelefon.TextLength==11)
            {
                if ((textBoxTC.Text == "") || TextboxAd.Text == "" || textBoxSoyad.Text == " " || textBoxKullanıcıAdı.Text == "" || textBoxSifre.Text == " " || textBoxSifreTekrar.Text == "" || textBoxTelefon.Text == "" || textBoxEMail.Text == "" || richTextBoxAdres.Text == "")
                // if blokları boş bırakılıp bırakılmadığını kontrol ediyor
                {
                    MessageBox.Show("Lütfen Tüm Alanları Doldurunuz");
                }
                else if (textBoxSifre.Text == textBoxSifreTekrar.Text)
                {
                    if (accsessmanager.tekselect(user.Tc, "TC", "Ad", "Login") == "Null")
                    {
                        accsessmanager.CustomerAdd(user.Tc, "TC", "Login"); //user tcyi veri tabanına ekler 

                        // tcye göre güncelleme işlemlerini yapar
                        accsessmanager.Update("Login", "Ad", user.Ad, user.Tc, "TC");
                        accsessmanager.Update("Login", "Soyad", user.Soyad, user.Tc, "TC");
                        accsessmanager.Update("Login", "KullanıcıAdı", user.KullanıcıAdı, user.Tc, "TC");
                        accsessmanager.Update("Login", "Telefon", user.Telefon, user.Tc, "TC");
                        accsessmanager.Update("Login", "Email", user.Email, user.Tc, "TC");
                        accsessmanager.Update("Login", "Adres", user.Adres, user.Tc, "TC");
                        accsessmanager.Update("Login", "UserType", user.UserTypes, user.Tc, "TC");
                        accsessmanager.Update("Login", "Sifre", user.Sifre, user.Tc, "TC");
                        accsessmanager.CustomerAdd(user.Tc, "TC", "Banka");
                        accsessmanager.Update("Banka", "UserType", user.UserTypes, user.Tc, "TC");

                        accsessmanager.Update("Banka", "Bakiye", "0", user.Tc, "TC");
                        accsessmanager.Update("Banka", "OnayBekleyenBakiye", "0", user.Tc, "TC");
                        accsessmanager.Update("Banka", "BakiyeOnayDurumu", "0", user.Tc, "TC");
                    }
                    else
                    {
                        MessageBox.Show(user.Tc + " Nolu Tc Sisteme Kayıtlı Lütfen Giriş Yapmayı Deneyiniz");
                    }




                }

                else
                {
                    MessageBox.Show("Girdiğiniz Sifreler Uyuşmamaktadır Lütfen Tekrar Deneyiniz");
                }

            }
            else
            {
                MessageBox.Show("Tc  ve Telefon Numarası 11 Haneli Olmalıdır");
            }
                   
            TextboxAd.Text = "";
           textBoxSoyad.Text ="";
            textBoxSifreTekrar.Text = "";
            comboBoxUserType.Text = "";
            textBoxTC.Text="";
          textBoxTelefon.Text = "";
            textBoxEMail.Text = "";
           richTextBoxAdres.Text = "";
            textBoxKullanıcıAdı.Text = "";
            comboBoxUserType.Text = "";
           textBoxSifre.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void KayıtEkranı_Load(object sender, EventArgs e)
        {

        }
    }
}
