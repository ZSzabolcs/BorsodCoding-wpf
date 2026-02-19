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
        public VelemenyTabla(string tablaNev, string getURL = "https://localhost:7159/api/Velemeny", string postURL = "https://localhost:7159/api/Velemeny?userName=", string putURL = "https://localhost:7159/api/Velemeny/FromWPF", string delURL = "https://localhost:7159/api/Velemeny/FromWPF?id=") : base(tablaNev, getURL, postURL, putURL, delURL)
        {
        }

        public override CurrentTableRecord GetCurrentTableRecord(object currentTableCollection, int index)
        {
            var collection = currentTableCollection as ObservableCollection<VelemenyMezoi>;
            var record = collection[index];
            CurrentTableRecord currentTableRecord = new()
            {
                Id = record.Id,
                JsonBody = new VelemenyMezoi()
                {
                    Id = record.Id,
                    Ertekeles = record.Ertekeles,
                    Megjegyzes = record.Megjegyzes,
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
                            return JsonSerializer.Deserialize<ObservableCollection<VelemenyMezoi>>(messageElement.ToString(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        }
                    }

                    return new ObservableCollection<UserMezoi>();



                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show(e.Message);
                return new ObservableCollection<UserMezoi>();
            }
        }

        public override void LoadAddDataWindow(string token)
        {
            AddOrUpdateVelemeny addOrUpdateVelemeny = new AddOrUpdateVelemeny(token, this);
            addOrUpdateVelemeny.ShowDialog();
        }

        public override void LoadUpdateDataWindow(string token, object jsonBody)
        {
            AddOrUpdateVelemeny addOrUpdateVelemeny = new AddOrUpdateVelemeny(token, (jsonBody as VelemenyMezoi), this);
            addOrUpdateVelemeny.ShowDialog();
        }
    }
}
