using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace odevdeneme2
{
    class CustomerManager
    {
        private ICustomerDAL DAL;

        public CustomerManager(ICustomerDAL DAL)
        {
            this.DAL = DAL;
        }
        public void CustomerAdd(string value,string sutunad,string tabload)
        {
             DAL.Add(value,sutunad,tabload);
        }
        public void delete(int value, string sutunad, string tabload)
        {
            DAL.Delete(value, sutunad, tabload);
        }
        public void CustomerAdd(string value,string value2, string sutunad,string sutunad2, string tabload)
        {
            DAL.Add(value,value2, sutunad,sutunad2, tabload);
        }
        public string tekselect(string value, string sart, string sutunad, string tabload)
        {
            return DAL.tekselect(value, sart, sutunad, tabload);
        }
        public string tekselect(int value, string sart, string sutunad, string tabload)
        {
            return DAL.tekselect(value, sart, sutunad, tabload);
        }
        public string tekselect(string value,string value2, string sart2, string sart, string sutunad, string tabload)
        {
            return DAL.tekselect(value,value2, sart,sart2, sutunad, tabload);
        }
        public List<string> select(string value, string sart, string sutunad, string tabload)
        {
            return DAL.select(value, sart, sutunad, tabload);
        
        }
        public List<string> select(string value, string sart, string sutunad, string tabload,string sıralanacaktablo)
        {
            return DAL.select(value, sart, sutunad, tabload,sıralanacaktablo);

        }
        public void Update(string tabload, string sutunad, string value, string sart, string sart2)
        {
            DAL.Update(tabload,sutunad,value,sart,sart2);
        }
       public void Update(string tabload, string sutunad, string value, int sartdeger, string sart)
        {
            DAL.Update(tabload, sutunad, value, sartdeger, sart);
        }
        public void Update(string tabload, string sutunad, string value, string sartdeger, string sart, string sartdeger2, string sart2)
        {
            DAL.Update(tabload, sutunad, value, sartdeger,sart,sartdeger2,sart2);
        }


    }
}
