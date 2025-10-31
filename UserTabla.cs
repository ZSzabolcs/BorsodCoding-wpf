using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin
{
    class UserTabla : Tabla
    {
        public UserTabla()
        {
            tablaNev = "user";
        }



        public async Task<List<UserMezoi>> GetDataFromApi()
        {
            const string apiUrl = "http://localhost:5233/api/UserRegistData";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // GET kérés küldése
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode(); // Hiba esetén kivételt dob

                    string responseBody = await response.Content.ReadAsStringAsync();

                    // JSON válasz deserializálása List<AdatElem> típusú listává
                    List<UserMezoi> data = JsonSerializer.Deserialize<List<UserMezoi>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return data;
                }
                catch (HttpRequestException e)
                {
                    // Hiba kezelése (pl. hálózati probléma, 404 stb.)
                    System.Diagnostics.Debug.WriteLine($"Kérés hiba: {e.Message}");
                    return new List<UserMezoi>(); // Üres lista visszaadása hiba esetén
                }
            }
        }


        public override void InsertAData()
        {
            throw new NotImplementedException();
        }

        public override void DeleteAData()
        {
            throw new NotImplementedException();
        }

        public override void UpdateAData()
        {
            throw new NotImplementedException();
        }
    }
}
