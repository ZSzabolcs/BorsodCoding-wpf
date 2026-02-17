using BorsodCoding_WPF_Admin;
using BorsodCoding_WPF_Admin.Mezok;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;  
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;

namespace BorsodCoding_WPF_Admin.Tablak
{
    public abstract class Tabla
    {
        public Tabla(string tablaNev, string getURL, string postURL,  string putURL, string delURL)
        {
            TablaNev = tablaNev;
            GetURL = getURL;
            PostURL = postURL;
            DelURL = delURL;
            PutURL = putURL;
        }

        protected string GetURL { get; set; }

        public string TablaNev { get; protected set; }

        private string PostURL { get; set; }

        private string DelURL { get; set; }

        private string PutURL { get; set; }


        public abstract void LoadUpdateDataWindow(string token, object jsonBody, Tabla tabla);

        public abstract void LoadAddDataWindow(string token, Tabla tabla);

        public abstract Task<object> GetDataFromApi(string token);
        
        private HttpClient GetOwnClient(string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return httpClient;
        }

        private JsonSerializerOptions GetOwnJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }

        public async  Task<object> DeleteAData(string id, string token)
        {
            try
            {
                var client = GetOwnClient(token);
                HttpResponseMessage response = await client.DeleteAsync(DelURL + id);
                string jsonString = await response.Content.ReadAsStringAsync();
                Database.ShowJsonProperty(jsonString, "message");
                return response;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async  Task<object> InsertAData(object jsonBody, string token)
        {
            try
            {
                var client = GetOwnClient(token);
                var options = GetOwnJsonSerializerOptions();
                HttpResponseMessage response = await client.PostAsJsonAsync(PostURL, jsonBody, options);
                string jsonString = await response.Content.ReadAsStringAsync();

                Database.ShowJsonProperty(jsonString, "message");
                return response;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<object> UpdateAData(object jsonBody, string token)
        {
            try
            {
                var client = GetOwnClient(token);
                var options = GetOwnJsonSerializerOptions();
                HttpResponseMessage response = await client.PutAsJsonAsync(PutURL, jsonBody, options);
                string jsonString = await response.Content.ReadAsStringAsync();

                Database.ShowJsonProperty(jsonString, "message");
                return response;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

    }
}
