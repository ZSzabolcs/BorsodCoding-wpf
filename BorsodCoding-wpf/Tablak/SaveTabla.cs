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
using static System.Net.WebRequestMethods;

namespace BorsodCoding_WPF_Admin.Tablak
{
   public class SaveTabla : Tabla
   {
        public SaveTabla()
        {
            TablaNev = "save";
            ApiURL = "https://localhost:7036/api/Save";
        }
       
        public override async Task<ObservableCollection<T>> GetDataFromApi<T>(string token)
        {
            return await base.GetDataFromApi<T>(token);
        }
       
        public override async Task<bool> InsertAData(object jsonBody)
        {
            try
            {
                var sendjsonBody = jsonBody as SaveJsonBody;
                var client = new HttpClient();
                JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
                HttpResponseMessage response = await client.PostAsJsonAsync(ApiURL, jsonBody);
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
                ApiURL = $"https://localhost:7036/api/Save?id={id}";
                var client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync(ApiURL);

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
                HttpResponseMessage response = await client.PutAsJsonAsync(ApiURL, jsonBody);
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
