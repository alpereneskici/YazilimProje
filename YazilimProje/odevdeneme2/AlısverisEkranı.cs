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
    public partial class AlısverisEkranı : Form
    {
        public AlısverisEkranı()
        {
            InitializeComponent();
        }
        public string giristc { get; set; }
      // satıcı ürünlerini listeler
        public void satıcılis()
        {
            listView1.View = View.Details;
            listView1.Items.Clear();
            listView1.Columns.Clear();

            listView1.Columns.Add("TC", 60, HorizontalAlignment.Left);
            listView1.Columns.Add("Ad", 60, HorizontalAlignment.Left);
            listView1.Columns.Add("Soyad", 60, HorizontalAlignment.Left);
            listView1.Columns.Add("Ürün Türü", 60, HorizontalAlignment.Left);

            listView1.Columns.Add("Ürün Miktarı", 60, HorizontalAlignment.Left);

            listView1.Columns.Add("Birim Satış Fiyatı", 150, HorizontalAlignment.Left);



            CustomerManager accsessmanager = new CustomerManager(new AccesCustomerDAL());
            List<string> TC = new List<string>();
           
            TC = accsessmanager.select(comboBox1.Text, "UrunTipi", "TC", "Urunler", "UrunSatisFiyatı");








            int i = 0;
            foreach(string tc in TC)
            {
                string kontrol = accsessmanager.tekselect(tc, "TC", "UrunMiktarı", "Urunler");
                    if(kontrol!="0")
                {
                    listView1.Items.Add(tc);
                    listView1.Items[i].SubItems.Add(accsessmanager.tekselect(tc, "TC", "Ad", "Login"));
                    listView1.Items[i].SubItems.Add(accsessmanager.tekselect(tc, "TC", "Soyad", "Login"));
                    listView1.Items[i].SubItems.Add(accsessmanager.tekselect(tc, "TC", "UrunTipi", "Urunler"));
                    listView1.Items[i].SubItems.Add(accsessmanager.tekselect(tc, "TC", "UrunMiktarı", "Urunler") + "KG");
                    listView1.Items[i].SubItems.Add(accsessmanager.tekselect(tc, "TC", "UrunSatisFiyatı", "Urunler") + "TL");
                    i++;
                }
              
            }
           

        }
        public void islem()
        {
            listView2.View = View.Details;
            listView2.Items.Clear();
            listView2.Columns.Clear();

            listView2.Columns.Add("İşlem Zamanı", 60, HorizontalAlignment.Left);
            listView2.Columns.Add("Yapılan İşlem Detayları", 1000, HorizontalAlignment.Left);
          
            CustomerManager accsessmanager = new CustomerManager(new AccesCustomerDAL());
          

        }
        private void AlısverisEkranı_Load(object sender, EventArgs e)
        {
            islem();
            CustomerManager accsessmanager = new CustomerManager(new AccesCustomerDAL());
            List<string> a = new List<string>();
         // ürün tiplerini seçim listesine yazar
            a = accsessmanager.select("Onaylandı", "UrunOnayDurumu", "UrunTipi","Urunler");
      
            List<string> b= a.Distinct().ToList();
            foreach(string d in b)
            {
                comboBox1.Items.Add(d);
            }
            satıcılis();
            
           

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            satıcılis();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            islem();
            CustomerManager accsessmanager = new CustomerManager(new AccesCustomerDAL()); // DAL BAĞLANTISI
            List<string> TC = new List<string>(); // TÜM TCLERİ TUTUYOR
            List<string> TCy = new List<string>();// STOK OLAN TCLERİ TUTUYOR
            List<string> toplamstok = new List<string>();
            TC = accsessmanager.select(comboBox1.Text, "UrunTipi", "TC", "Urunler", "UrunSatisFiyatı"); // TÜM TCLERİ ALIYOR
            foreach(string stokkntrol in TC) 
            {
                if((accsessmanager.tekselect(stokkntrol, "TC", "UrunMiktarı", "Urunler"))!="0")// STOK OLAN TCLERİ KONTROL EDİYOR
                {
                    TCy.Add(stokkntrol);
                  }
            }
           
            int urunstok, alınacakmiktar, urunbirimfiyat,sahipolunanpara;
            sahipolunanpara= Convert.ToInt32(accsessmanager.tekselect(giristc, "TC", "Bakiye", "Banka")); // giriş yapan kullanıcının parasını tutuyor
            int a = 0;
            int topstoktut=0;
            toplamstok = accsessmanager.select("Onaylandı","UrunOnayDurumu","UrunMiktarı","Urunler"); // veritabanında ki  stokları alıyor
            foreach (string t in toplamstok) //  stokları topluyor
            {
                topstoktut = topstoktut + Convert.ToInt32(t);
            }
            alınacakmiktar = Convert.ToInt32(textBox1.Text);
            if (topstoktut < Convert.ToInt32(textBox1.Text))
            {
                MessageBox.Show("Elimizde Okadar Mal yok");
                
            }
            else
            {

            
            while (alınacakmiktar != 0)                //alınacak miktar 0 olana kadar dönecek bir döngü oluşturdum 
            {
                urunbirimfiyat = Convert.ToInt32(accsessmanager.tekselect(TCy[a], "TC", "UrunSatisFiyatı", "Urunler")); // ürnün birim fiyatını alıyor

                urunstok = Convert.ToInt32(accsessmanager.tekselect(TCy[a], "TC", "UrunMiktarı", "Urunler")); // ürün stoğunu alıyr
            
               
                if (sahipolunanpara != 0) // para varmı kontrol ediyor
                    
                {
                    if (alınacakmiktar >= urunstok) // alınacak miktar en ucuz ürünün stoğunda var mı kontrol ediyor
                    {

                        if (sahipolunanpara >= (urunstok * urunbirimfiyat)) // paramızın stoktakilerin hepsini almaya  yetiyorsa güncelleme işlemlerini yapmaya yarıyor
                        {
                            alınacakmiktar = alınacakmiktar - urunstok;
                            sahipolunanpara = sahipolunanpara - (urunstok * urunbirimfiyat);
                            
                            string satıcıparatut = accsessmanager.tekselect(TCy[a], "TC", "Bakiye", "Banka");
                            string bakiyee = (Convert.ToInt64(satıcıparatut) + (urunstok * urunbirimfiyat)).ToString();
                            accsessmanager.Update("Banka","Bakiye",bakiyee,TCy[a],"TC");
                            string id = accsessmanager.tekselect(TCy[a],"TC","id","Urunler");
                            accsessmanager.Update("Urunler","UrunMiktarı","0",Convert.ToInt32(id),"id") ;
                            if (TCy[a] == giristc)
                            {

                                MessageBox.Show("Kendinizden ürün aldınız");
                            }
                            else
                            {
                                accsessmanager.Update("Banka", "Bakiye", accsessmanager.tekselect(giristc, "TC", "Bakiye", "Banka"), giristc, "TC");
                            }
                            listView2.Items.Add(DateTime.Now.ToString());
                            listView2.Items[a].SubItems.Add(accsessmanager.tekselect(TCy[a], "TC", "Ad", "Login") + " Kullanıcısından " + " " + urunstok.ToString() + " KG Kadar Ürün Alındı  " + " " + accsessmanager.tekselect(TCy[a], "TC", "Ad", "Login") + " " + accsessmanager.tekselect(giristc, "TC", "Ad", "Login") + " Kullanıcısının Hesabına " + urunstok * urunbirimfiyat + " TL Kadar Para Gönderdi " + "  " + accsessmanager.tekselect(giristc, "TC", "Ad", "Login") + " Kullanıcısında " + accsessmanager.tekselect(giristc, "TC", "Bakiye", "Banka") + " TL Kadar Para Kaldı"); ;

                            a++;
                        }
                        else // paramızın stoktakilerin hepsini almaya yetmiyorsa alınacak miktarı belirleyip  güncelleme işlemlerini yapmaya yarıyor
                        {

                            alınacakmiktar = alınacakmiktar - (sahipolunanpara / urunbirimfiyat);
                           
                            MessageBox.Show("Paranız " + (sahipolunanpara / urunbirimfiyat) + " Tane Almanıza Yetiyorr");
                               string satıcıparatut = accsessmanager.tekselect(TCy[a], "TC", "Bakiye", "Banka");
                           
                            accsessmanager.Update("Banka","Bakiye",(Convert.ToInt32(satıcıparatut)+(sahipolunanpara)).ToString(),TCy[a],"TC");
                            string id = accsessmanager.tekselect(TCy[a],"TC","id","Urunler");
                            accsessmanager.Update("Urunler","UrunMiktarı",((urunstok)-(sahipolunanpara/urunbirimfiyat)).ToString(),Convert.ToInt32(id),"id") ;
                            if (TCy[a] == giristc)
                            {


                            }
                            else
                            {
                                accsessmanager.Update("Banka", "Bakiye", accsessmanager.tekselect(giristc, "TC", "Bakiye", "Banka"), giristc, "TC");
                            }
                            listView2.Items.Add(DateTime.Now.ToString());

                            listView2.Items[a].SubItems.Add(accsessmanager.tekselect(TCy[a], "TC", "Ad", "Login") + " Kullanıcısından " + " " + (sahipolunanpara / urunbirimfiyat).ToString() + " KG Kadar Ürün Alındı  " + " " + accsessmanager.tekselect(TCy[a], "TC", "Ad", "Login") + " " + accsessmanager.tekselect(giristc, "TC", "Ad", "Login") + " Kullanıcısının Hesabına " + sahipolunanpara.ToString() + " TL Kadar Para Gönderdi " + "  " + accsessmanager.tekselect(giristc, "TC", "Ad", "Login") + " Kullanıcısında " + accsessmanager.tekselect(giristc, "TC", "Bakiye", "Banka") + " TL Kadar Para Kaldı");

                            sahipolunanpara = 0;

                        }








                    }
                    else
                    {
                        if (sahipolunanpara >= (alınacakmiktar * urunbirimfiyat)) // sahip olunan para alınacakmiktarın fiyatına yetiyor mu kontrol ediyor ediyorsa güncelleme işlemlerini yapar
                        {
                            urunstok = urunstok - alınacakmiktar;
                            sahipolunanpara = sahipolunanpara - (alınacakmiktar * urunbirimfiyat);
                          
                
                            string satıcıparatut = accsessmanager.tekselect(TCy[a], "TC", "Bakiye", "Banka");

                            accsessmanager.Update("Banka", "Bakiye", (Convert.ToInt32(satıcıparatut) + (alınacakmiktar * urunbirimfiyat)).ToString(), TCy[a], "TC");
                            string id = accsessmanager.tekselect(TCy[a], "TC", "id", "Urunler");
                            accsessmanager.Update("Urunler", "UrunMiktarı",(urunstok).ToString(), Convert.ToInt32(id), "id");
                            if (TCy[a] == giristc)
                            {


                            }
                            else
                            {
                                accsessmanager.Update("Banka", "Bakiye", accsessmanager.tekselect(giristc, "TC", "Bakiye", "Banka"), giristc, "TC");
                            }
                            listView2.Items.Add(DateTime.Now.ToString());
                            listView2.Items[a].SubItems.Add(accsessmanager.tekselect(TCy[a], "TC", "Ad", "Login") + " Kullanıcısından " + " " + alınacakmiktar.ToString() + " KG Kadar Ürün Alındı  " + " " + accsessmanager.tekselect(TCy[a], "TC", "Ad", "Login") + " " + accsessmanager.tekselect(giristc, "TC", "Ad", "Login") + " Kullanıcısının Hesabına " + alınacakmiktar * urunbirimfiyat + " TL Kadar Para Gönderdi " + "  " + accsessmanager.tekselect(giristc, "TC", "Ad", "Login") + " Kullanıcısında " + accsessmanager.tekselect(giristc, "TC", "Bakiye", "Banka") + " TL Kadar Para Kaldı"); ;

                            alınacakmiktar = 0;
                        }
                        else        //sahip olunan para alınacak miktarın parasına yetmiyorsa alınacak miktarı belirleyip güncelleme işlemlerini yapıyor
                        {
                            urunstok = urunstok - (sahipolunanpara / urunbirimfiyat); 


                            MessageBox.Show("Paranız " + (sahipolunanpara / urunbirimfiyat) + " Tane Almanıza Yetiyorr");
                         
                            string satıcıparatut = accsessmanager.tekselect(TCy[a], "TC", "Bakiye", "Banka");

                            accsessmanager.Update("Banka", "Bakiye", (Convert.ToInt32(satıcıparatut) + (sahipolunanpara)).ToString(), TCy[a], "TC");
                            string id = accsessmanager.tekselect(TCy[a], "TC", "id", "Urunler");
                            accsessmanager.Update("Urunler", "UrunMiktarı", ((urunstok) - (sahipolunanpara / urunbirimfiyat)).ToString(), Convert.ToInt32(id), "id");
                          if (TCy[a] == giristc)
                            {
                              

                            }
                            else
                            {
                                accsessmanager.Update("Banka", "Bakiye", accsessmanager.tekselect(giristc, "TC", "Bakiye", "Banka"), giristc, "TC");
                            }
                            listView2.Items.Add(DateTime.Now.ToString());

                            listView2.Items[a].SubItems.Add(accsessmanager.tekselect(TCy[a], "TC", "Ad", "Login") + " Kullanıcısından " + " " + (sahipolunanpara / urunbirimfiyat).ToString() + " KG Kadar Ürün Alındı  " + " " + accsessmanager.tekselect(TCy[a], "TC", "Ad", "Login") + " " + accsessmanager.tekselect(giristc, "TC", "Ad", "Login") + " Kullanıcısının Hesabına " + sahipolunanpara.ToString() + " TL Kadar Para Gönderdi " + "  " + accsessmanager.tekselect(giristc, "TC", "Ad", "Login") + " Kullanıcısında " + accsessmanager.tekselect(giristc, "TC", "Bakiye", "Banka") + " TL Kadar Para Kaldı");

                            sahipolunanpara = 0;
                        }

                    }
                }
                else // sahip olunan paranın yettiği kadar alınacakmiktar'dan alıp döngüyü durduruyor
                {
                    MessageBox.Show("Paranız Bitti");
                    break;
                }
            }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        { 
            // geri dön butonu arapaneli açıyor
            AraPanel f1 = new AraPanel();
            f1.Show();
            this.Hide();
        }
    }
        }
   

