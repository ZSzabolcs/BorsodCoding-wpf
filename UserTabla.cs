using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin
{
    class UserTabla : Tabla
    {
        public UserTabla()
        {
            tablaNev = "user";
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime Date {  get; set; }

        public override void GetAllData()
        {
            throw new NotImplementedException();
        }

        public override void InsertAData()
        {
            throw new NotImplementedException();
        }

        public override void DeleteAData()
        {
            throw new NotImplementedException();
        }

        public override void UpdateAData()
        {
            throw new NotImplementedException();
        }
    }
}
