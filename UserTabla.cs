using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin
{
    class UserTabla : Tabla
    {
        public UserTabla()
        {
            TablaNev = "user";
            ApiURL = "http://localhost:5233/api/User/ToWPF";
        }

        
        public override Task<List<T>> GetDataFromApi<T>()
        {
            return base.GetDataFromApi<T>();
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
