using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace odevdeneme2
{
    interface ICustomerDAL
    {
        string tekselect(string value,string value2,string sart2, string sart, string sutunad, string tabload);
        List<string> select(string value,string sart, string sutunad, string tabload);
        List<string> select(string value, string sart, string sutunad, string tabload, string sıralanacaktablo);

        void Add(string value,string sutunad,string tabload);
        void Add(string value,string value2, string sutunad,string sutunad2, string tabload);
        void Update(string tabload, string sutunad, string value, string sartdeger, string sart, string sartdeger2, string sart2);
        void Update(string tabload, string sutunad, string value, int sartdeger, string sart);
        void Update(string tabload, string sutunad, string value, string sart, string sart2);
        string tekselect(string value, string sart, string sutunad, string tabload);
        string tekselect(int value, string sart, string sutunad, string tabload);
        void Delete(int value, string sutunad, string tabload);


    }
}
