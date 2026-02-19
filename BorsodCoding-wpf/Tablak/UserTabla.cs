using BorsodCoding_WPF_Admin.Tablak;
using BorsodCoding_WPF_Admin.Mezok;
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
using BorsodCoding_WPF_Admin.Dtos;

namespace BorsodCoding_WPF_Admin.Tablak
{
    public class UserTabla : Tabla
    {
        public UserTabla(string tablaNev, string getURL, string postURL, string putURL, string delURL) : base(tablaNev, getURL, postURL, putURL, delURL)
        {
        }

        public override CurrentTableRecord GetCurrentTableRecord(object currentTableCollection, int index)
        {
            var collection = currentTableCollection as ObservableCollection<UserMezoi>;
            var record = collection[index];
            CurrentTableRecord currentTableRecord = new()
            {
                Id = record.Id,
                JsonBody = new UserDto()
                {
                    UserName = record.UserName,
                    Password = "",
                    Email = record.Email
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
                            return JsonSerializer.Deserialize<ObservableCollection<UserMezoi>>(messageElement.ToString(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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
            AddOrUpdateUser addOrUpdateUser = new AddOrUpdateUser(token, this);
            addOrUpdateUser.ShowDialog();
        }

        public override void LoadUpdateDataWindow(string token, object userjsonBody)
        {
            AddOrUpdateUser addOrUpdateUser = new AddOrUpdateUser(token, (userjsonBody as UserDto), this);
            addOrUpdateUser.ShowDialog();
        }

    }
}