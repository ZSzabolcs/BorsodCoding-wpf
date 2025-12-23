using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace BorsodCoding_WPF_Admin
{
    public class UserTabla : Tabla
    {
        public UserTabla()
        {
            TablaNev = "user";
            ApiURL = "http://localhost:5233/api/User/ToWPF";
            ObjURL = "http://localhost:5233/api/User/";
            JsonBody = null;
        }

        
        public override Task<ObservableCollection<T>> GetDataFromApi<T>()
        {
            return base.GetDataFromApi<T>();
        }
        

        public async override Task<bool> InsertAData(object jsonBody)
        {
            try
            {
                var sendjsonBody = jsonBody as UserJsonBody;
                var client = new HttpClient();
                JsonBody = sendjsonBody;
                HttpResponseMessage response = await client.PostAsJsonAsync(ObjURL, JsonBody);
                return response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
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
