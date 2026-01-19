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
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;

namespace BorsodCoding_WPF_Admin
{
    public class UserTabla : Tabla
    {
        public UserTabla()
        {
            TablaNev = "user";
            ApiURL = "http://localhost:5019/auth";
            ObjURL = "";
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
                HttpResponseMessage response = await client.PostAsJsonAsync(ObjURL, sendjsonBody);
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
                ObjURL = $"http://localhost:5019/api/auth?id={id}";
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
                ObjURL = "http://localhost:5233/api/User";
                var sendjsonBody = jsonBody as UserJsonBody;
                var client = new HttpClient();
                HttpResponseMessage response = await client.PutAsJsonAsync(ObjURL, sendjsonBody);
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
