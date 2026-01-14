using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;

namespace BorsodCoding_WPF_Admin
{
   public class SaveTabla : Tabla
    {
        public SaveTabla()
        {
            TablaNev = "save";
            ApiURL = "http://localhost:5233/api/Save/ToWPF";
            ObjURL = "http://localhost:5233/api/Save";
        }
       
        public override async Task<ObservableCollection<T>> GetDataFromApi<T>()
        {
            return await base.GetDataFromApi<T>();
        }
       
        public override async Task<bool> InsertAData(object jsonBody)
        {
            try
            {
                var sendjsonBody = jsonBody as SaveJsonBody;
                var client = new HttpClient();
                JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
                HttpResponseMessage response = await client.PostAsJsonAsync(ObjURL, jsonBody);
                string jsonString = await response.Content.ReadAsStringAsync();

                using (JsonDocument doc = JsonDocument.Parse(jsonString))
                {
                    if (doc.RootElement.TryGetProperty("message", out var messageElement))
                    {
                        string message = messageElement.GetString();
                        MessageBox.Show(message);
                    }
                }
                return response.IsSuccessStatusCode;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        public override async Task<bool> DeleteAData(string id)
        {

            try
            {
                ObjURL = $"http://localhost:5233/api/Save?id={id}";
                var client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync(ObjURL);

                string jsonString = await response.Content.ReadAsStringAsync();

                using (JsonDocument doc = JsonDocument.Parse(jsonString))
                {
                    if (doc.RootElement.TryGetProperty("message", out var messageElement))
                    {
                        string message = messageElement.GetString();
                        MessageBox.Show(message);
                    }
                }
                return response.IsSuccessStatusCode;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public override async Task<bool> UpdateAData(object jsonBody)
        {

            try
            {
                var sendJsonBody = jsonBody as SaveJsonBody;
                var client = new HttpClient();
                HttpResponseMessage response = await client.PutAsJsonAsync(ObjURL, jsonBody);
                string jsonString = await response.Content.ReadAsStringAsync();

                using (JsonDocument doc = JsonDocument.Parse(jsonString))
                {
                    if (doc.RootElement.TryGetProperty("message", out var messageElement))
                    {
                        string message = messageElement.GetString();
                        MessageBox.Show(message);
                    }
                }
                return response.IsSuccessStatusCode;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
