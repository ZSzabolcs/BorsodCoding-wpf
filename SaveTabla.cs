using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin
{
    class SaveTabla : Tabla
    {
        public SaveTabla()
        {
        }



        public override void GetAllData()
        {
            throw new NotImplementedException();
        }

        public async Task<List<SaveMezoi>> GetDataFromApi()
        {
            const string apiUrl = "http://localhost:5233/api/UserSaveData";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // GET kérés küldése
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode(); // Hiba esetén kivételt dob

                    string responseBody = await response.Content.ReadAsStringAsync();

                    // JSON válasz deserializálása List<AdatElem> típusú listává
                    List<SaveMezoi> data = JsonSerializer.Deserialize<List<SaveMezoi>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return data;
                }
                catch (HttpRequestException e)
                {
                    // Hiba kezelése (pl. hálózati probléma, 404 stb.)
                    System.Diagnostics.Debug.WriteLine($"Kérés hiba: {e.Message}");
                    return new List<SaveMezoi>(); // Üres lista visszaadása hiba esetén
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
