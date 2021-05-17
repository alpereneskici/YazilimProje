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
    public partial class Adminpanel : Form
    {
        public Adminpanel()
        {
            InitializeComponent();
        }
        public string secilitc,secilisira,fiyat; 
       public void alıcı()
        {
            listView1.Columns.Clear();
            listView1.Items.Clear();
            listView1.View = View.Details;
            listView1.Columns.Add("Sıra", 30, HorizontalAlignment.Left);
            listView1.Columns.Add("TC", 70, HorizontalAlignment.Left);
            listView1.Columns.Add("Ad", 70, HorizontalAlignment.Left);
            listView1.Columns.Add("Soyad", 70, HorizontalAlignment.Left);

            listView1.Columns.Add("Bakiye", 70, HorizontalAlignment.Left);
            listView1.Columns.Add("Onay Bekleyen Bakiye", 150, HorizontalAlignment.Left);





            CustomerManager accsessmanager = new CustomerManager(new AccesCustomerDAL());
            List<string> tc = new List<string>();
            List<string> id = new List<string>();
            id = accsessmanager.select("Onay Bekleniyor", "BakiyeOnayDurumu", "id", "Banka");
            List<int> s = new List<int>();

            for (int j = 0; j < id.Count; j++)
            {
                s.Add(Convert.ToInt32(id[j]));

            }

            s.Sort();
            int i = 0;
            foreach (int a in s)
            {
                tc.Add(accsessmanager.tekselect(a, "id", "TC", "Banka"));
                listView1.Items.Add(a.ToString());
                listView1.Items[i].SubItems.Add(tc[i]);
                listView1.Items[i].SubItems.Add(accsessmanager.tekselect(tc[i], "TC", "Ad", "Login"));

                listView1.Items[i].SubItems.Add(accsessmanager.tekselect(tc[i], "TC", "Soyad", "Login"));

                listView1.Items[i].SubItems.Add(accsessmanager.tekselect(a, "id", "Bakiye", "Banka"));
                listView1.Items[i].SubItems.Add(accsessmanager.tekselect(a, "id", "OnayBekleyenBakiye", "Banka"));
              
                i++;


            }












        } // alıcı bilgilerini listeler
        public  void satıcı()
        {

            listView1.View = View.Details;
            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.Columns.Add("Sıra", 30, HorizontalAlignment.Left);
            listView1.Columns.Add("TC", 60, HorizontalAlignment.Left);
            listView1.Columns.Add("Ad", 60, HorizontalAlignment.Left);
            listView1.Columns.Add("Soyad", 60, HorizontalAlignment.Left);
            listView1.Columns.Add("Ürün Türü", 60, HorizontalAlignment.Left);

            listView1.Columns.Add("Ürün Miktarı", 60, HorizontalAlignment.Left);
            listView1.Columns.Add("Onay Bekleyen Ürün Miktarı", 160, HorizontalAlignment.Left);

            listView1.Columns.Add("Sistem Tarafından Belirlenen Satış Fiyatı", 150, HorizontalAlignment.Left);



            CustomerManager accsessmanager = new CustomerManager(new AccesCustomerDAL());
            List<string> id = new List<string>();
            List<string> tc = new List<string>();
            id = accsessmanager.select("Onay Bekleniyor", "UrunOnayDurumu", "id", "Urunler");


            List<int> s = new List<int>();

            for (int j = 0; j < id.Count; j++)
            {
                s.Add(Convert.ToInt32(id[j]));

            }

            s.Sort();
            int i = 0;
            foreach (int a in s)
            {
                tc.Add(accsessmanager.tekselect(a, "id", "TC", "Urunler"));
              
               
                listView1.Items.Add(a.ToString());
                listView1.Items[i].SubItems.Add(tc[i]);
                listView1.Items[i].SubItems.Add(accsessmanager.tekselect(tc[i], "TC", "Ad", "Login"));

                listView1.Items[i].SubItems.Add(accsessmanager.tekselect(tc[i], "TC", "Soyad", "Login"));
                listView1.Items[i].SubItems.Add(accsessmanager.tekselect(a, "id", "UrunTipi", "Urunler"));
                listView1.Items[i].SubItems.Add(accsessmanager.tekselect(a, "id", "UrunMiktarı", "Urunler")+" KG");
                listView1.Items[i].SubItems.Add(accsessmanager.tekselect(a, "id", "OnayBekleyenUrunMiktar", "Urunler"));
            

                listView1.Items[i].SubItems.Add(accsessmanager.tekselect(a,"id", "UrunSatisFiyatı","Urunler") + " TL");
                i++;
               
              
            }






        } // alıcı bilgilerini listeler
        private void button1_Click(object sender, EventArgs e)
        {
            secilisira = listView1.SelectedItems[0].Text; // seçililen kullanıcının idsini alıyor
            ListViewItem item = listView1.SelectedItems[0];
            secilitc = item.SubItems[1].Text; // seçilen kullanıcın tcsini alıyor
         
            CustomerManager accsessmanager = new CustomerManager(new AccesCustomerDAL());
            if (listView1.SelectedIndices.Count > 0)
            {
                
                if (comboBox1.Text=="Alıcı") 
                {
                  
                    string bakiyetemp=accsessmanager.tekselect(secilitc,"TC", "Bakiye","Banka"); // kullanıcının bakiyesini çeker
                    string onaybakiyetemp = accsessmanager.tekselect(secilitc, "TC", "OnayBekleyenBakiye", "Banka"); // kullanıcının onaylanacak bakiyesini çeker 
                    int snc = Convert.ToInt32(bakiyetemp) + Convert.ToInt32(onaybakiyetemp); // onay bekleyen bakiyeyi bakiyeyle toplar 
                    accsessmanager.Update("Banka","Bakiye",snc.ToString(),secilitc,"TC"); // bakiyeyi günceller
                    accsessmanager.Update("Banka", "BakiyeOnayDurumu", "Onaylandı", secilitc, "TC"); // bakiyenin onay durumunu günceller
                    

                    accsessmanager.Update("Banka", "OnayBekleyenBakiye", "0", secilitc, "TC"); //onay bekleyen bakiyeyi 0'lar

                    alıcı();
                }
                else  if (comboBox1.Text == "Satıcı")
                {
                    string uruntemp = accsessmanager.tekselect(Convert.ToInt32(secilisira), "id", "UrunMiktarı", "Urunler"); // urun miktarını tutar
                    string onayuruntemp = accsessmanager.tekselect(Convert.ToInt32(secilisira), "id", "OnayBekleyenUrunMiktar", "Urunler"); // onay miktarını tutar 
                    int snc = Convert.ToInt32(uruntemp) + Convert.ToInt32(onayuruntemp); // ürünmiktarı ve onay bekleyen ürün miktarını toplar
                    accsessmanager.Update("Urunler", "UrunMiktarı", snc.ToString(), Convert.ToInt32(secilisira), "id"); // ürün miktarını günceller
                    accsessmanager.Update("Urunler", "OnayBekleyenUrunMiktar", "0", Convert.ToInt32(secilisira), "id"); // onay bekleyen miktarı 0lar

                    accsessmanager.Update("Urunler", "UrunOnayDurumu", "Onaylandı", Convert.ToInt32(secilisira), "id"); // ürün onay durumunu günceller
                 
                    satıcı();

                }


            }
            else
            {
                MessageBox.Show("Lütfen Listeden Onaylamak istediğiniz kısmı seçiniz");
            }


        }
    

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (comboBox1.SelectedItem.ToString() == "Alıcı")
            {
                alıcı();

               


            }
            else if (comboBox1.SelectedItem.ToString() == "Satıcı")
            {
                satıcı();
         

             }
            else
            {

            }
        }

        private void Adminpanel_Load(object sender, EventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            secilisira = listView1.SelectedItems[0].Text; // seçililen kullanıcının idsini alıyor
            ListViewItem item = listView1.SelectedItems[0];
            secilitc = item.SubItems[1].Text; // seçilen kullanıcın tcsini alıyor

            CustomerManager accsessmanager = new CustomerManager(new AccesCustomerDAL());
            if (listView1.SelectedIndices.Count > 0) // seçilen kullanıcı var mı diye kontrol ediyor
            {
                if (comboBox1.Text == "Alıcı")
                {
                    

                    accsessmanager.Update("Banka", "BakiyeOnayDurumu", "Onaylanmadı", secilitc, "TC"); // bakiyeyi onay durumunu onaylanmadı olarak değiştirir


                    accsessmanager.Update("Banka", "OnayBekleyenBakiye", "0", secilitc, "TC"); // onay bekleyen bakiyeyi 0lar
                   
                    
                    alıcı();
                }
                else if (comboBox1.Text == "Satıcı")
                {
                   
                    accsessmanager.delete(Convert.ToInt32(secilisira), "id", "Urunler"); // ürünlerde şişkinlik olmasın diye kullanıcının onaylanmayan ürününü listeden siler
                    satıcı();

                }
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

         
        }
    }
}
