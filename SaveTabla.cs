using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace BorsodCoding_WPF_Admin
{
    class SaveTabla : Tabla
    {
        public SaveTabla()
        {
            TablaNev = "save";
            ApiURL = "http://localhost:5233/api/Save/ToWPF";
            ObjURL = "http://localhost:5233/api/Save";
            JsonBody = null;
        }
       
        public override Task<ObservableCollection<T>> GetDataFromApi<T>()
        {
            return base.GetDataFromApi<T>();
        }
       
        public override async Task<bool> InsertAData(object jsonBody)
        {
            try
            {
                var sendjsonBody = jsonBody as SaveJsonBody;
                var client = new HttpClient();
                JsonBody = sendjsonBody;
                HttpResponseMessage response = await client.PostAsJsonAsync(ObjURL, JsonBody);
                MessageBox.Show(await response.Content.ReadAsStringAsync());
                return response.IsSuccessStatusCode;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            return false;
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
