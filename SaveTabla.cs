using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
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
        }
       
        public override Task<ObservableCollection<T>> GetDataFromApi<T>()
        {
            return base.GetDataFromApi<T>();
        }
       
        public override async void InsertAData()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "/UserSaveData");
                var content = new StringContent("{\"name\" : \"{{name}}\",  \"points\" : 0,  \"level\" : 0,\r\n    \"language\" : \"{{lang}}\"}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

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
