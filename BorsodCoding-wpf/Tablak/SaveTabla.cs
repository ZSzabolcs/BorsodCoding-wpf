using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
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
       
        public override async Task<bool> InsertAData(object jsonBody, string token)
        {
            try
            {
                var sendjsonBody = jsonBody as SaveJsonBody;
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await client.PostAsJsonAsync(ApiURL, sendjsonBody);
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

        public override async Task<bool> DeleteAData(string id, string token)
        {

            try
            {
                ApiURL = $"https://localhost:7036/api/Save?id={id}";
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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

        public override async Task<bool> UpdateAData(object jsonBody, string token)
        {

            try
            {
                ApiURL = "https://localhost:7036/api/Save/FromWPF";
                var sendJsonBody = jsonBody as SaveJsonBody;
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await client.PutAsJsonAsync(ApiURL, sendJsonBody);
                string jsonString = await response.Content.ReadAsStringAsync();

                using (JsonDocument doc = JsonDocument.Parse(jsonString))
                {
                    if (doc.RootElement.TryGetProperty("message", out var messageElement))
                    {
                        string message = messageElement.GetString();
                        MessageBox.Show(message);
                    }
                }
                MessageBox.Show(response.IsSuccessStatusCode.ToString());
                return response.IsSuccessStatusCode;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public override void LoadUpdateWindow(object kivalasztottElem, string userToken, string mode)
        {
            AddOrUpdateSave addOrUpdateSave = new AddOrUpdateSave((kivalasztottElem as SaveJsonBody), mode, userToken);
            addOrUpdateSave.ShowDialog();
        }

        public override void LoadAddWindow(string userToken, string mode)
        {
            AddOrUpdateSave addOrUpdateSave = new AddOrUpdateSave(mode, userToken);
            addOrUpdateSave.ShowDialog();
        }
    }
}
