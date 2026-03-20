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
        protected readonly object mezo;
        public Tabla(string tablaNev, string getURL, string postURL,  string putURL, string delURL, object mezo)
        {
            TablaNev = tablaNev;
            GetURL = getURL;
            PostURL = postURL;
            DelURL = delURL;
            PutURL = putURL;
            this.mezo = mezo;
        }

        protected static string GetURL { get; set; }

        public string TablaNev { get; protected set; }

        public static string PostURL { get; set; }

        private static string DelURL { get; set; }

        public static  string PutURL { get; set; }


        public abstract CurrentTableRecord GetCurrentTableRecord(object currentTableCollection, int index);

        public abstract void LoadUpdateDataWindow(string token, object jsonBody);

        public abstract void LoadAddDataWindow(string token);

        public abstract Task<object> GetDataFromApi(string token);

        public static ObservableCollection<Mezok> GetErrorsWhenLoadTable<Mezok>(Exception exception)
        {
            if (exception is HttpRequestException)
            {
                MessageBox.Show($"Kapcsolati hiba: {exception.Message}");
                return new ObservableCollection<Mezok>();
            }
            if (exception is JsonException)
            {
                MessageBox.Show($"JSON hiba: {exception.Message}");
                return new ObservableCollection<Mezok>();
            }

            MessageBox.Show($"Hiba: {exception.Message}");
            return new ObservableCollection<Mezok>();
        }

        public static void GetErrorsInRequests(Exception exception)
        {
            if (exception is JsonException)
            {
                MessageBox.Show($"JSON szerkezeti hiba: {exception.Message}");
            }
            else if (exception is ArgumentNullException)
            {
                MessageBox.Show($"JSON hiba: {exception.Message}");
            }
            else
            {
                MessageBox.Show($"Hiba: {exception.Message}");
            }
        }


        private static HttpClient GetOwnClient(string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return httpClient;
        }

        public static JsonSerializerOptions GetOwnJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }
        /// <summary>
        /// Töröl egy kiválasztott rekordot a szerveren.
        /// </summary>
        /// <param name="id">Kiválasztott rekord Id-ja</param>
        /// <param name="token">Token a kéréshez</param>
        /// <returns>Ha kérés teljesült HttpResponseMessage objektumként tér vissza. Hiba esetén false.</returns>

        public async  Task<object> DeleteAData(string id, string token)
        {
            HttpResponseMessage response;
            try
            {
                var client = GetOwnClient(token);
                response = await client.DeleteAsync(DelURL + id);
                string jsonString = await response.Content.ReadAsStringAsync();
                Database.ShowJsonProperty(jsonString, "message");
                return response;


            }
            catch (Exception ex)
            {
                GetErrorsInRequests(ex);
                return false;
            }
        }
        /// <summary>
        /// Hozzáad egy új rekordot a jelenlegi Tábla objektum
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <param name="token">Token a kéréshez</param>
        /// <returns></returns>

        public static async  Task<object> InsertAData(object jsonBody, string token)
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
                GetErrorsInRequests(ex);
                return false;
            }



        }

        public static async Task<object> UpdateAData(object jsonBody, string token)
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
                GetErrorsInRequests(ex);
                return false;
            }
        }

    }
}
