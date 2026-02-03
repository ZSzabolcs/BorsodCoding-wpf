using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;

namespace BorsodCoding_WPF_Admin.Tablak
{
    public class UserTabla : Tabla
    {
        public UserTabla()
        {
            TablaNev = "user";
            ApiURL = "https://localhost:7159/auth";
            ObjURL = "";
        }

        
        public override Task<ObservableCollection<T>> GetDataFromApi<T>(string token)
        {
            return base.GetDataFromApi<T>(token);
        }
        

        public async override Task<bool> InsertAData(object jsonBody, string token)
        {
            try
            {
                ObjURL = "https://localhost:7159/auth/register";
                var sendjsonBody = jsonBody as UserJsonBody;
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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

        public override async Task<bool> DeleteAData(string id, string token)
        {
            try
            {
                ObjURL = $"https://localhost:7159/auth?id={id}";
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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

        public override async Task<bool> UpdateAData(object jsonBody, string token)
        {
            try
            {
                ObjURL = "https://localhost:7159/auth/Modositas";
                var sendjsonBody = jsonBody as UserJsonBody;
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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

        public override void LoadUpdateWindow(object kivalasztottElem, string userToken, string mode)
        {
            AddOrUpdateUser addOrUpdateUser = new AddOrUpdateUser((kivalasztottElem as UserJsonBody), mode, userToken);
            addOrUpdateUser.ShowDialog();
        }

        public override void LoadAddWindow(string userToken, string mode)
        {
            AddOrUpdateUser addOrUpdateUser = new AddOrUpdateUser(mode, userToken);
            addOrUpdateUser.ShowDialog();
        }
    }
}
