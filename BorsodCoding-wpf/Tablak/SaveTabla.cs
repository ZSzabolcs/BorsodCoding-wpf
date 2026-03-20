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
using BorsodCoding_WPF_Admin.Mezok;
using BorsodCoding_WPF_Admin.Dtos;
using BorsodCoding_WPF_Admin.AddOrUpdateWindows;

namespace BorsodCoding_WPF_Admin.Tablak
{

    public class SaveTabla : Tabla
    {
        public SaveTabla(string tablaNev, string getURL, string postURL, string putURL, string delURL, object mezo) : base(tablaNev, getURL, postURL, putURL, delURL, mezo)
        {
        }

        public override object GetPutJson(object currentTableCollection, int index)
        {
            var collection = currentTableCollection as ObservableCollection<SaveMezoi>;
            var record = collection[index];
            var jsonBody = new PutSaveDto()
            {
                Id = record.Id,
                Points = record.Points,
                Level = record.Level,
                Language = record.Language,
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
                            return JsonSerializer.Deserialize<ObservableCollection<SaveMezoi>>(messageElement.ToString(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        }
                    }

                    return new ObservableCollection<SaveMezoi>();



                }
            }
            catch (Exception ex)
            {
                return GetErrorsWhenLoadTable<SaveMezoi>(ex);
            }
        }

        public override string GetSelectedItemId(object currentTableCollection, int index)
        {
            var collection = currentTableCollection as ObservableCollection<SaveMezoi>;
            return collection[index].Id;
        }
    }
}
