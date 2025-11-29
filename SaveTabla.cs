using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin
{
    class SaveTabla : Tabla
    {
        public SaveTabla()
        {
            TablaNev = "save";
            ApiURL = "http://localhost:5233/api/Save/ToWPF";
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
