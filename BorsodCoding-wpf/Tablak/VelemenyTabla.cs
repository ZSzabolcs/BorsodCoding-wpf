using BorsodCoding_WPF_Admin.AddOrUpdateWindows;
using BorsodCoding_WPF_Admin.Dtos;
using BorsodCoding_WPF_Admin.Mezok;
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
    public class VelemenyTabla : Tabla
    {
        public VelemenyTabla(
            string tablaNev,
            string getURL,
            string postURL, 
            string putURL, 
            string delURL,            
            object mezo
            ) : base(tablaNev, getURL, postURL, putURL, delURL, mezo)
        {
        }

        public override object GetPutJson(object currentTableCollection, int index)
        {
            var collection = currentTableCollection as ObservableCollection<VelemenyMezoi>;
            var record = collection[index];
            var jsonBody = new PutVelemeny()
            {
                Id = record.Id,
                Ertekeles = record.Ertekeles,
                Megjegyzes = record.Megjegyzes,
            };

            return jsonBody;
        }

        public async override Task<object> GetDataFromApi(string token)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, GetURL);
                    request.Headers.Add("Authorization", $"Bearer {token}");
                    HttpResponseMessage response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    string jsonBody = await response.Content.ReadAsStringAsync();

                    using (JsonDocument doc = JsonDocument.Parse(jsonBody))
                    {
                        if (doc.RootElement.TryGetProperty("value", out var messageElement))
                        {
                            return JsonSerializer.Deserialize<ObservableCollection<VelemenyMezoi>>(messageElement.ToString(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        }
                    }

                    return new ObservableCollection<UserMezoi>();



                }
            }
            catch (Exception ex)
            {
                return GetErrorsWhenLoadTable<VelemenyMezoi>(ex);
            }
        }

        public override string GetSelectedItemId(object currentTableCollection, int index)
        {
            var collection = currentTableCollection as ObservableCollection<VelemenyMezoi>;
            return collection[index].Id;
        }
    }
}
