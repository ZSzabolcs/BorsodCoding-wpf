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

namespace BorsodCoding_WPF_Admin.Tablak
{

    public class SaveTabla : Tabla
    {
        public SaveTabla(string tablaNev, string getURL, string postURL, string putURL, string delURL) : base(tablaNev, getURL, postURL, putURL, delURL)
        {
        }

        public override CurrentTableRecord GetCurrentTableRecord(object currentTableCollection, int index)
        {
            var collection = currentTableCollection as ObservableCollection<SaveMezoi>;
            var record = collection[index];
            CurrentTableRecord currentTableRecord = new()
            {
                Id = record.Id,
                JsonBody = new PutSaveDto()
                {
                    Id = record.Id,
                    Points = record.Points,
                    Level = record.Level,
                    Language = record.Language,
                }
            };

            return currentTableRecord;

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
            catch (HttpRequestException e)
            {
                MessageBox.Show(e.Message);
                return new ObservableCollection<SaveMezoi>();
            }
        }

        public override void LoadAddDataWindow(string token)
        {
            AddOrUpdateSave addOrUpdateSave = new AddOrUpdateSave(token, this);
            addOrUpdateSave.ShowDialog();
        }


        public override void LoadUpdateDataWindow(string token, object jsonBody)
        {
            AddOrUpdateSave addOrUpdateUser = new AddOrUpdateSave(token, (jsonBody as PutSaveDto), this);
            addOrUpdateUser.ShowDialog();
        }


    }
}
