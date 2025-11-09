using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin
{
    abstract class Tabla
    {
        public readonly string apiUrl = "";
        public string tablaNev = "";
        protected Tabla() { }

        public List<T> GetDataBySQLSelect<T>(MySqlDataReader data) where T : Mezo, new()
        {
            T osztaly = new T();
            List<T> rekordok = new List<T>();

            if (osztaly is UserMezoi)
            {
                while (data.Read())
                {
                    T ujElem = new T();
                    (ujElem as UserMezoi).Id = data.GetInt32("Id");
                    (ujElem as UserMezoi).Name = data.GetString("Name");
                    (ujElem as UserMezoi).Password = data.GetString("Password");
                    (ujElem as UserMezoi).Date = data.GetDateTime("Date");
                    rekordok.Add(ujElem);
                }
            }
            else
            {
                while (data.Read())
                {
                    T ujElem = new T();
                    (ujElem as SaveMezoi).UserId = data.GetInt32("UserId");
                    (ujElem as SaveMezoi).Level = data.GetInt32("Level");
                    (ujElem as SaveMezoi).Language = data.GetString("Language");
                    (ujElem as SaveMezoi).Points = data.GetInt32("Points");
                    (ujElem as SaveMezoi).Date = data.GetDateTime("Date");
                    rekordok.Add(ujElem);

                }
            }
                
            return rekordok;
        }
        /*
        public async Task<List<T>> BeginLoadAsync<T>() where T : Mezo, new()
        {
            T osztaly = new T();
            if (osztaly is UserMezoi)
            {
                var data = await GetDataFromApi<T>(new UserTabla().apiUrl);
                return data;
            }
            else if (osztaly is SaveMezoi) 
            {
                var data = await GetDataFromApi<T>(new SaveTabla().apiUrl);
                return data;
            }

            return new List<T>();

        }
        */

        public virtual async Task<List<T>> GetDataFromApi<T>(string apiUrl) where T : Mezo, new()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // GET kérés küldése
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode(); // Hiba esetén kivételt dob

                    string responseBody = await response.Content.ReadAsStringAsync();

                    // JSON válasz deserializálása List<AdatElem> típusú listává
                    List<T> data = JsonSerializer.Deserialize<List<T>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return data;
                }
                catch (HttpRequestException e)
                {
                    // Hiba kezelése (pl. hálózati probléma, 404 stb.)
                    System.Diagnostics.Debug.WriteLine($"Kérés hiba: {e.Message}");
                    return new List<T>(); // Üres lista visszaadása hiba esetén
                }
            }
        }
        

        public abstract void DeleteAData();

        public abstract void InsertAData();

        public abstract void UpdateAData();

    }
}
