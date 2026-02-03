using BorsodCoding_WPF_Admin.Mezok;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace BorsodCoding_WPF_Admin.Tablak
{
    public abstract class Tabla
    {
        protected string ApiURL { get; set; }

        protected string ObjURL { get; set; }

        public string TablaNev { get; protected set; }

        protected Tabla() { }

        

        public virtual async Task<ObservableCollection<T>> GetDataFromApi<T>(string token) where T : Mezo, new()
        {
            
           try
           {
                using (HttpClient client = new HttpClient())
                {   
                    var request = new HttpRequestMessage(HttpMethod.Get, ApiURL);
                    request.Headers.Add("Authorization", $"Bearer {token}");
                    HttpResponseMessage response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode(); 

                    string jsonBody = await response.Content.ReadAsStringAsync();

                    using (JsonDocument doc = JsonDocument.Parse(jsonBody))
                    {
                        if (doc.RootElement.TryGetProperty("value", out var messageElement))
                        {
                            ObservableCollection<T>? data = JsonSerializer.Deserialize<ObservableCollection<T>>(messageElement.ToString(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                            return data;
                        }
                    }

                    return new ObservableCollection<T>();
                    


                }
           }
           catch (HttpRequestException e)
           {
                MessageBox.Show(e.Message);
                return new ObservableCollection<T>(); 
           }
            
        }

        

        public abstract Task<bool> DeleteAData(string id, string token);

        public abstract Task<bool> InsertAData(object jsonBody, string token);

        public abstract Task<bool> UpdateAData(object jsonBody, string token);

    }
}
