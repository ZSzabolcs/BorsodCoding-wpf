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

namespace BorsodCoding_WPF_Admin
{
    public abstract class Tabla
    {
        protected string ApiURL { get; set; }

        protected string ObjURL { get; set; }

        public string TablaNev { get; protected set; }

        public object? JsonBody { get; protected set; } = null;
        protected Tabla() { }

        

        public virtual async Task<ObservableCollection<T>> GetDataFromApi<T>() where T : Mezo, new()
        {
            
           try
           {
                using (HttpClient client = new HttpClient())
                {   
                    HttpResponseMessage response = await client.GetAsync(ApiURL);
                    response.EnsureSuccessStatusCode(); 

                    string responseBody = await response.Content.ReadAsStringAsync();


                    ObservableCollection<T> data = JsonSerializer.Deserialize<ObservableCollection<T>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return data;
                }
           }
           catch (HttpRequestException e)
           {
                MessageBox.Show(e.Message);
                return new ObservableCollection<T>(); 
           }
            
        }

        public virtual void OpenWindowToModifyOrNewObject() { }
        

        public abstract void DeleteAData();

        public abstract Task<bool> InsertAData(object JsonBody);

        public abstract void UpdateAData();

    }
}
