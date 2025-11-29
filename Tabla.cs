using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace BorsodCoding_WPF_Admin
{
    abstract class Tabla
    {
        protected string ApiURL { get; set; }

        public string TablaNev { get; protected set; }
        protected Tabla() { }

        

        public virtual async Task<List<T>> GetDataFromApi<T>() where T : Mezo, new()
        {
            
           try
           {
                using (HttpClient client = new HttpClient())
                {   
                    HttpResponseMessage response = await client.GetAsync(ApiURL);
                    response.EnsureSuccessStatusCode(); 

                    string responseBody = await response.Content.ReadAsStringAsync();


                    List<T> data = JsonSerializer.Deserialize<List<T>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return data;
                }
           }
           catch (HttpRequestException e)
           {
                MessageBox.Show(e.Message);
                return new List<T>(); 
           }
            
        }
        

        public abstract void DeleteAData();

        public abstract void InsertAData();

        public abstract void UpdateAData();

    }
}
