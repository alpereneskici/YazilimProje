using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
namespace odevdeneme2
{
    class AccesCustomerDAL : ICustomerDAL
    {
        private const string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/Users/umutr/source/repos/odevdeneme2/odevdeneme2/bin/Debug/Database3.mdb";
        private OleDbConnection baglanti;
        private OleDbCommand komut;
        private OleDbDataReader bilgiokut;
        

        public AccesCustomerDAL()
        {
            baglanti = new OleDbConnection(ConnectionString);
            
        }

        public void Add(string value,string sutunad,string tabload)
        {
            
            baglanti.Open();
            komut = new OleDbCommand(string.Format(@"insert into {0} ({1}) values ('" + value+"')",tabload, sutunad), baglanti);
           
            komut.ExecuteNonQuery();
            baglanti.Close();
        }
        public void Add(string value,string value2, string sutunad,string sutunad2, string tabload)
        {

            baglanti.Open();
            komut = new OleDbCommand(string.Format(@"insert into {0} ({1},{2}) values ('" + value + "','" + value2 + "')", tabload, sutunad,sutunad2), baglanti);

            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        public void Delete(int value,string sutunad,string tabload)
        {
            baglanti.Open();
            komut = new OleDbCommand(string.Format(@"DELETE FROM {0} Where {1}="+value+"",tabload,sutunad), baglanti);

            komut.ExecuteNonQuery();
            baglanti.Close();
        }

     
        public string tekselect(string value, string sart, string sutunad, string tabload) // tek şart ile x sutunu y tablosundan z şartı ile çekmeye yarar 
        {
            baglanti.Open();
            komut = new OleDbCommand(string.Format("SELECT {0} FROM {1} Where {2}='" + value + "'", sutunad, tabload, sart), baglanti); // çekme kodu
            string tut= "Null";
            bilgiokut = komut.ExecuteReader();
         
                while (bilgiokut.Read())
                {
                    tut = bilgiokut[sutunad].ToString();
                }
            
           

            baglanti.Close();
            return tut;
           
        }
        public string tekselect(int value, string sart, string sutunad, string tabload) // tek şart ile x sutunu y tablosundan z şartı ile çekmeye yarar 
        {
            baglanti.Open();
            komut = new OleDbCommand(string.Format("SELECT {0} FROM {1} Where {2}=" + value + "", sutunad, tabload, sart), baglanti); // çekme kodu
            string tut = "Null";
            bilgiokut = komut.ExecuteReader();

            while (bilgiokut.Read())
            {
                tut = bilgiokut[sutunad].ToString();
            }



            baglanti.Close();
            return tut;

        }

        public string tekselect(string value, string value2, string sart2, string sart, string sutunad, string tabload)
        {
            baglanti.Open();
            string tut = "Null";
            komut = new OleDbCommand(string.Format("SELECT {0} FROM {1} Where {2}='" + value + "' AND {3}='" + value2 + "'", sutunad, tabload, sart, sart2), baglanti); // çekme kodu
            bilgiokut = komut.ExecuteReader();

            while (bilgiokut.Read())
            {
                tut = bilgiokut[sutunad].ToString();
            }



            baglanti.Close();
            return tut;
        }

        public List<string> select(string value, string value2, string sart, string sart2, string sutunad, string tabload) // 2 şart ile bilgi çekme 
        {
            baglanti.Open();
            komut = new OleDbCommand(string.Format("SELECT {0} FROM {1} Where {2}='" + value + "' AND {3}='" + value2 + "'", sutunad, tabload, sart, sart2), baglanti); // çekme kodu
            List<string> dizi = new List<string>();
            bilgiokut = komut.ExecuteReader();
            if (bilgiokut.Read())
            {
                while (bilgiokut.Read())
                {
                    dizi.Add(Convert.ToString(bilgiokut.GetString(0)));
                }
            }
            else
            {
                dizi.Add("Null");
            }



            baglanti.Close();
            return dizi;

        }

        public List<string> select(string value, string sart, string sutunad, string tabload)
        {
            baglanti.Open();
            komut = new OleDbCommand(string.Format("SELECT {0} FROM {1} Where {2}='" + value + "'", sutunad, tabload, sart), baglanti); // çekme kodu
            List<string> tut = new List<string>();
            bilgiokut = komut.ExecuteReader();

            while (bilgiokut.Read())
            {
                tut.Add( bilgiokut[sutunad].ToString());
            }



            baglanti.Close();
            return tut;
        }

        public void Update(string tabload, string sutunad, string value, string sart,string sart2)
        {

            baglanti.Open();
            komut = new OleDbCommand(string.Format(@"Update {0} set {1}='"+value+"' Where {2}='" + sart + "'", tabload, sutunad,sart2), baglanti);

            komut.ExecuteNonQuery();
            baglanti.Close();
           
        }
        public void Update(string tabload, string sutunad, string value, string sartdeger, string sart,string sartdeger2,string sart2)
        {

            baglanti.Open();
            komut = new OleDbCommand(string.Format(@"Update {0} set {1}='" + value + "' Where {2}='" + sartdeger + "',{3}='"+ sartdeger2 + "'", tabload, sutunad, sart,sart2), baglanti);

            komut.ExecuteNonQuery();
            baglanti.Close();

        }

        public void Update(string tabload, string sutunad, string value, int sartdeger, string sart)
        {

            baglanti.Open();
            komut = new OleDbCommand(string.Format(@"Update {0} set {1}='" + value + "' Where {2}=" + sartdeger+ "", tabload, sutunad, sart), baglanti);

            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        public List<string> select(string value, string sart, string sutunad, string tabload,string sıralanacaktablo)
        {
            baglanti.Open();
            komut = new OleDbCommand(string.Format("SELECT {0} FROM {1} Where {2}='" + value + "' ORDER BY '"+sıralanacaktablo+"' ASC", sutunad, tabload, sart), baglanti); // çekme kodu
            List<string> tut = new List<string>();
            bilgiokut = komut.ExecuteReader();

            while (bilgiokut.Read())
            {
                tut.Add(bilgiokut[sutunad].ToString());
            }



            baglanti.Close();
            return tut;
        }
    }

       
    }

